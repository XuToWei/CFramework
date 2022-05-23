#if ILRuntime
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameFramework;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;
using UnityGameFramework.Runtime;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace Game
{
    public class ILRuntimeHelper : HotfixHelperBase
    {
        private AppDomain m_AppDomain;

        /// <summary>
        /// ILRuntime入口对象
        /// </summary>
        public AppDomain AppDomain => m_AppDomain;

        private IMethod m_Update;
        private IMethod m_Shutdown;
        private IMethod m_ApplicationPause;
        private IMethod m_ApplicationQuit;

        private List<Type> m_HotfixTypes;
        private bool m_IsStarted;
        private Action m_RunOnStarted;
        private ILTypeInstance m_HotfixGameEntry;
        
        public override T CreateHotfixMonoBehaviour<T>(GameObject go,string hotfixFullTypeName)
        {
            var appDomain = GameEntry.Hotfix.GetAppDomain();
            ILType type = appDomain.LoadedTypes[hotfixFullTypeName] as ILType;
            if (type == null)
            {
                throw new Exception($"Can not find hotfix mono behaviour {hotfixFullTypeName}");
            }

            //热更DLL内的类型比较麻烦。首先我们得自己手动创建实例
            var ilInstance = new ILTypeInstance(type, false); //手动创建实例是因为默认方式会new MonoBehaviour，这在Unity里不允许
            //接下来创建Adapter实例
            Type adapterType = type.FirstCLRBaseType.TypeForCLR;
            T clrInstance = go.AddComponent(adapterType) as T;
            //unity创建的实例并没有热更DLL里面的实例，所以需要手动赋值
            if (clrInstance is IAdapterProperty adapterProperty)
            {
                adapterProperty.ILInstance = ilInstance;
                adapterProperty.AppDomain = appDomain;
            }

            //这个实例默认创建的CLRInstance不是通过AddComponent出来的有效实例，所以得手动替换
            ilInstance.CLRInstance = clrInstance;
            return clrInstance;
        }

        /// <summary>
        /// 获取热更新层类的Type对象
        /// </summary>
        public override Type GetHotfixType(string hotfixTypeFullName)
        {
            return m_HotfixTypes.Find(x => x.FullName != null && x.FullName.Equals(hotfixTypeFullName));
        }

        /// <summary>
        /// 获取所有热更新层类的Type对象
        /// </summary>
        public override List<Type> GetAllTypes()
        {
            return m_HotfixTypes;
        }

        public override object GetHotfixGameEntry => m_HotfixGameEntry;

        public override void LoadAssembly(byte[] dllBytes, byte[] pdbBytes)
        {
            m_AppDomain = new AppDomain(ILRuntimeJITFlags.JITOnDemand);

            if (pdbBytes == null)
            {
                AppDomain.LoadAssembly(new MemoryStream(dllBytes));
            }
            else
            {
                AppDomain.LoadAssembly(new MemoryStream(dllBytes), new MemoryStream(pdbBytes), new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            }
            Log.Info("Hotfix load assembly completed!");
        }

        public void Start()
        {
            StartDebugService();
#if DEBUG && !NO_PROFILER
            //设置Unity主线程ID 这样就可以用Profiler看性能消耗了
            AppDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
            m_HotfixTypes = AppDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToList();
            Log.Info("Hotfix start!");
        }

        private void StartDebugService()
        {
            //启动调试服务器
            AppDomain.DebugService.StartDebugService(56789);
            Log.Info("启动ILRuntime调试服务器:56789");
        }

        public override void Enter()
        {
            string typeFullName = HotfixConfig.EntryTypeFullName;
            IType type = AppDomain.LoadedTypes[typeFullName];
            m_HotfixGameEntry = (ILTypeInstance)CreateInstance(typeFullName);
            IMethod ilEnter = type.GetMethod("Enter", 0);
            AppDomain.Invoke(ilEnter, m_HotfixGameEntry, null);
        }

        public override void ShutDown()
        {
            if (m_Shutdown == null)
            {
                return;
            }

            AppDomain.Invoke(m_Shutdown, m_HotfixGameEntry, null);
        }

        public override object CreateInstance(string typeName)
        {
            IType type = AppDomain.LoadedTypes[typeName];
            object instance = ((ILType)type).Instantiate();
            return instance;
        }

        public override object GetMethod(string typeName, string methodName, int paramCount)
        {
            IType type = AppDomain.LoadedTypes[typeName];
            return type.GetMethod(methodName, paramCount);
        }

        public override object InvokeMethod(object method, object instance, params object[] objects)
        {
           return AppDomain.Invoke((IMethod)method, instance, objects);
        }
        
        public InvocationContext BeginInvoke(IMethod m)
        {
            return AppDomain.BeginInvoke(m);
        }
        private void Update()
        {
            if (m_Update == null)
            {
                return;
            }

            using var ctx = AppDomain.BeginInvoke(m_Update);
            ctx.PushObject(m_HotfixGameEntry);
            ctx.PushFloat(Time.deltaTime);
            ctx.PushFloat(Time.unscaledDeltaTime);
            ctx.Invoke();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (m_ApplicationPause == null)
            {
                return;
            }

            using var ctx = AppDomain.BeginInvoke(m_ApplicationPause);
            ctx.PushObject(m_HotfixGameEntry);
            ctx.PushBool(pauseStatus);
            ctx.Invoke();
        }

        private void OnApplicationQuit()
        {
            if (m_ApplicationQuit == null)
            {
                return;
            }

            AppDomain.Invoke(m_ApplicationQuit, m_HotfixGameEntry, null);
        }
    }
}
#endif
