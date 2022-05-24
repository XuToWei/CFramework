#if ILRuntime
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameFramework.Resource;
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
        private ILTypeInstance m_HotfixGameEntry;
        
        public override object HotfixGameEntry => m_HotfixGameEntry;

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

        public override async Task<bool> Load()
        {
            m_AppDomain = new AppDomain(ILRuntimeJITFlags.JITOnDemand);
            foreach (var dllName in HotfixConfig.DllNames)
            {
                string dllAssetName = AssetUtility.GetHotfixDllAsset(dllName);
                TextAsset dllAsset = await GameEntry.Resource.LoadAssetAsync<TextAsset>(dllAssetName);
                string pdbAssetName = AssetUtility.GetHotfixPdbAsset(dllName);
                if (GameEntry.Resource.HasAsset(pdbAssetName) == HasAssetResult.NotExist)
                {
                    AppDomain.LoadAssembly(new MemoryStream(dllAsset.bytes));
                }
                else
                {
                    TextAsset pdbAsset = await GameEntry.Resource.LoadAssetAsync<TextAsset>(pdbAssetName);
                    AppDomain.LoadAssembly(new MemoryStream(dllAsset.bytes), new MemoryStream(pdbAsset.bytes), new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
                }
            }
            m_HotfixTypes = AppDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToList();
            
            //启动调试服务器
            AppDomain.DebugService.StartDebugService(56789);
            Log.Info("启动ILRuntime调试服务器:56789");
#if DEBUG && !NO_PROFILER
            //设置Unity主线程ID 这样就可以用Profiler看性能消耗了
            AppDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
            System.AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Log.Error(e.ExceptionObject.ToString());
            };
            
            Log.Info("Hotfix load completed!");
            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return await tcs.Task;
        }

        public override void Enter()
        {
            string typeFullName = HotfixConfig.EntryTypeFullName;
            m_HotfixGameEntry = (ILTypeInstance)CreateInstance(typeFullName);
            IMethod ilEnter = (IMethod)GetMethod(typeFullName, "Enter", 0);
            InvokeMethod(ilEnter, m_HotfixGameEntry);
        }

        public override void ShutDown()
        {
            if (m_Shutdown == null)
            {
                return;
            }

            InvokeMethod(m_Shutdown, m_HotfixGameEntry);
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
        
        public InvocationContext BeginInvokeMethod(IMethod m)
        {
            return AppDomain.BeginInvoke(m);
        }
        private void Update()
        {
            if (m_Update == null)
            {
                return;
            }

            using var ctx = BeginInvokeMethod(m_Update);
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

            using var ctx = BeginInvokeMethod(m_ApplicationPause);
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

            InvokeMethod(m_ApplicationQuit, m_HotfixGameEntry);
        }
    }
}
#endif
