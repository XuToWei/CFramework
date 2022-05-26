using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using GameFramework;
using GameFramework.Resource;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class MonoHelper : HotfixHelperBase
    {
        private Action m_OnEnter;
        private Action<float,float> m_OnUpdate;
        private Action m_OnShutdown;
        private Action<bool> m_OnApplicationPause;
        private Action m_OnApplicationQuit;
        
        private readonly Dictionary<string, Type> m_HotfixTypeDict = new();
        private object m_HotfixGameEntry;
        public override object HotfixGameEntry => m_HotfixGameEntry;

        public override Type GetHotfixType(string hotfixTypeFullName)
        {
            if (m_HotfixTypeDict.TryGetValue(hotfixTypeFullName, out Type hotfixType))
            {
                return hotfixType;
            }

            throw new GameFrameworkException(Utility.Text.Format("HotfixType [{0}] get fail!", hotfixTypeFullName));
        }
        
        public override async Task Load()
        {
            foreach (var dllName in HotfixConfig.DllNames)
            {
                string dllAssetName = AssetUtility.GetHotfixDllAsset(dllName);
                TextAsset dllAsset = await GameEntry.Resource.LoadAssetAsync<TextAsset>(dllAssetName);
                string pdbAssetName = AssetUtility.GetHotfixPdbAsset(dllName);
                Assembly assembly;
                if (GameEntry.Resource.HasAsset(pdbAssetName) == HasAssetResult.NotExist)
                {
                    assembly = Assembly.Load(dllAsset.bytes);
                }
                else
                {
                    TextAsset pdbAsset = await GameEntry.Resource.LoadAssetAsync<TextAsset>(pdbAssetName);
                    assembly = Assembly.Load(dllAsset.bytes, pdbAsset.bytes);
                }
                foreach (var type in assembly.GetTypes())
                {
                    if (type.FullName != null)
                    {
                        m_HotfixTypeDict[type.FullName] = type;
                    }
                }
            }
            Log.Info("Hotfix load completed!");
        }

#if UNITY_EDITOR
        public void Reload()
        {
            foreach (var dllName in HotfixConfig.ReloadDllNames)
            {
                string dllAssetName = AssetUtility.GetHotfixDllAsset(dllName);
                byte[] dllBytes = System.IO.File.ReadAllBytes(dllAssetName);
                string pdbAssetName = AssetUtility.GetHotfixPdbAsset(dllName);
                Assembly assembly;
                if (!System.IO.File.Exists(pdbAssetName))
                {
                    assembly = Assembly.Load(dllBytes);
                }
                else
                {
                    byte[] pdbBytes = System.IO.File.ReadAllBytes(pdbAssetName);
                    assembly = Assembly.Load(dllBytes, pdbBytes);
                }
                foreach (var type in assembly.GetTypes())
                {
                    if (type.FullName != null)
                    {
                        m_HotfixTypeDict[type.FullName] = type;
                    }
                }
            }
            LoadLogic();
        }
#endif

        private void LoadLogic()
        {
            Type hotfixInit = GetHotfixType(HotfixConfig.EntryTypeFullName);
            m_HotfixGameEntry = Activator.CreateInstance(hotfixInit);
            
            MethodInfo onEnter = hotfixInit.GetMethod("OnEnter", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (onEnter == null)
            {
                throw new GameFrameworkException("HotfixEntry get [OnEnter] method fail!");
            }
            m_OnEnter = (Action)Delegate.CreateDelegate(typeof(Action), m_HotfixGameEntry, onEnter);
            
            MethodInfo onUpdate = hotfixInit.GetMethod("OnUpdate", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (onUpdate == null)
            {
                throw new GameFrameworkException("HotfixEntry get [OnUpdate] method fail!");
            }
            m_OnUpdate = (Action<float, float>)Delegate.CreateDelegate(typeof(Action<float, float>), m_HotfixGameEntry, onUpdate);

            MethodInfo onShutDown = hotfixInit.GetMethod("OnShutdown", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (onShutDown == null)
            {
                throw new GameFrameworkException("HotfixEntry get [OnShutdown] method fail!");
            }
            m_OnShutdown = (Action)Delegate.CreateDelegate(typeof(Action), m_HotfixGameEntry, onShutDown);
            
            MethodInfo onApplicationPause = hotfixInit.GetMethod("OnApplicationPause", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (onApplicationPause == null)
            {
                throw new GameFrameworkException("HotfixEntry get [OnApplicationPause] method fail!");
            }
            m_OnApplicationPause = (Action<bool>)Delegate.CreateDelegate(typeof(Action<bool>), m_HotfixGameEntry, onApplicationPause);
            
            MethodInfo onApplicationQuit = hotfixInit.GetMethod("OnApplicationQuit", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (onApplicationQuit == null)
            {
                throw new GameFrameworkException("HotfixEntry get [OnApplicationQuit] method fail!");
            }
            m_OnApplicationQuit = (Action)Delegate.CreateDelegate(typeof(Action), m_HotfixGameEntry, onApplicationQuit);
        }

        public override void OnEnter()
        {
            LoadLogic();
            m_OnEnter.Invoke();
        }

        public override void OnShutDown()
        {
            m_OnShutdown.Invoke();
        }

        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_OnUpdate.Invoke(elapseSeconds, realElapseSeconds);
        }

        public override object CreateInstance(string typeName)
        {
            Type type = GetHotfixType(typeName);
            object hotfixInstance = Activator.CreateInstance(type);
            return hotfixInstance;
        }

        public override object GetMethod(string typeName, string methodName, int paramCount)
        {
            Type type = GetHotfixType(typeName);
            return type
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.Name == methodName)
                .FirstOrDefault(x => x.GetParameters().Length == paramCount);
        }

        public override object InvokeMethod(object method, object instance, params object[] objects)
        {
            MethodInfo methodInfo = (MethodInfo)method;
            return methodInfo.Invoke(instance, objects);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            m_OnApplicationPause.Invoke(pauseStatus);
        }

        private void OnApplicationQuit()
        {
            m_OnApplicationQuit.Invoke();
        }
    }
}