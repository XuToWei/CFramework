#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using GameFramework;

namespace Game
{
    public class EditorHotfixHelper : HotfixHelperBase
    {
        public override HotfixType HotfixType => HotfixType.Mono;
        
        private readonly Dictionary<string, Type> m_HotfixTypeDict = new();

        private Type m_EntryType;
        private object m_EntryInstance;
        private Action m_OnEnterMethodAction;
        private Action m_ShutDownMethodAction;
        private Action<float, float> m_OnUpdateMethodAction;
        private Action<bool> m_OnApplicationPauseMethodAction;
        private Action<bool> m_OnApplicationFocusMethodAction;
        private Action m_OnApplicationQuitMethodAction;

        public Type GetHotfixType(string hotfixTypeFullName)
        {
            if (m_HotfixTypeDict.TryGetValue(hotfixTypeFullName, out Type hotfixType))
            {
                return hotfixType;
            }
            throw new GameFrameworkException(Utility.Text.Format("HotfixType [{0}] get fail!", hotfixTypeFullName));
        }
        
        private object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public T CreateMethodAction<T>(Type hotfixType, object instance, string methodName) where T : Delegate
        {
            MethodInfo methodInfo = hotfixType.GetMethod(methodName);
            return (T)Delegate.CreateDelegate(typeof(T), instance, methodInfo);
        }

        public object Invoke(Type hotfixType, string methodName, object instance, params object[] p)
        {
            MethodInfo methodInfo = hotfixType.GetMethod(methodName);
            return methodInfo.Invoke(instance, p);
        }

        public override async Task Load()
        {
            
        }
        
        public override void Init()
        {
            foreach (var dllName in HotfixConfig.DllNames)
            {
                Assembly assembly = Assembly.Load(dllName);
                foreach (var type in assembly.GetTypes())
                {
                    if (!string.IsNullOrEmpty(type.FullName))
                    {
                        m_HotfixTypeDict[type.FullName] = type;
                    }
                }
            }
            
            m_EntryType = GetHotfixType(HotfixConfig.EntryTypeFullName);
            m_EntryInstance = CreateInstance(m_EntryType);
            m_OnEnterMethodAction = CreateMethodAction<Action>(m_EntryType, m_EntryInstance, "OnEnter");
            m_ShutDownMethodAction = CreateMethodAction<Action>(m_EntryType, m_EntryInstance, "OnShutDown");
            m_OnUpdateMethodAction = CreateMethodAction<Action<float, float>>(m_EntryType, m_EntryInstance, "OnUpdate");
            m_OnApplicationPauseMethodAction = CreateMethodAction<Action<bool>>(m_EntryType, m_EntryInstance, "OnApplicationPause");
            m_OnApplicationFocusMethodAction = CreateMethodAction<Action<bool>>(m_EntryType, m_EntryInstance, "OnApplicationFocus");
            m_OnApplicationQuitMethodAction = CreateMethodAction<Action>(m_EntryType, m_EntryInstance, "OnApplicationQuit");
        }

        public override void OnEnter()
        {
            m_OnEnterMethodAction.Invoke();
        }

        public override void OnShutDown()
        {
            m_ShutDownMethodAction.Invoke();
        }

        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_OnUpdateMethodAction.Invoke(elapseSeconds, realElapseSeconds);
        }

        public override void OnApplicationPause(bool pauseStatus)
        {
            m_OnApplicationPauseMethodAction?.Invoke(pauseStatus);
        }

        public override void OnApplicationFocus(bool hasFocus)
        {
            m_OnApplicationFocusMethodAction?.Invoke(hasFocus);
        }

        public override void OnApplicationQuit()
        {
            m_OnApplicationQuitMethodAction?.Invoke();
        }
    }
}
#endif
