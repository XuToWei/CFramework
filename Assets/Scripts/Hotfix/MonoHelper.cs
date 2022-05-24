using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class MonoHelper : HotfixHelperBase
    {
        private Assembly m_EnterAssembly;
        private Action<float,float> m_Update;
        private MethodInfo m_Shutdown;
        private Action<bool> m_ApplicationPause;
        private MethodInfo m_ApplicationQuit;
        private readonly List<Type> m_HotfixTypes = new List<Type>();
        private object m_HotfixGameEntry;
        public override object HotfixGameEntry => m_HotfixGameEntry;

        public override Type GetHotfixType(string hotfixTypeFullName)
        {
            return m_HotfixTypes.Find(x => x.FullName != null && x.FullName.Equals(hotfixTypeFullName));
        }

        public override List<Type> GetAllTypes()
        {
            return m_HotfixTypes;
        }

        public override async Task<bool> Load()
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
                
                m_HotfixTypes.AddRange(assembly.GetTypes());
                if (string.Equals(dllName, "Framework"))
                {
                    m_EnterAssembly = assembly;
                }
            }

            Log.Info("Hotfix load completed!");
            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return await tcs.Task;
        }

        public override void Enter()
        {
            Type hotfixInit = m_EnterAssembly.GetType(HotfixConfig.EntryTypeFullName);
            m_HotfixGameEntry = Activator.CreateInstance(hotfixInit);
            MethodInfo start = hotfixInit.GetMethod("Start", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            MethodInfo update = hotfixInit.GetMethod("Update", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (update != null)
                m_Update = (Action<float, float>)Delegate.CreateDelegate(typeof(Action<float, float>), m_HotfixGameEntry, update);
            m_Shutdown = hotfixInit.GetMethod("Shutdown", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            MethodInfo applicationPause = hotfixInit.GetMethod("OnApplicationPause", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (applicationPause != null)
                m_ApplicationPause = (Action<bool>)Delegate.CreateDelegate(typeof(Action<bool>), m_HotfixGameEntry, applicationPause);
            m_ApplicationQuit = hotfixInit.GetMethod("OnApplicationQuit", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            start?.Invoke(m_HotfixGameEntry, null);
        }

        public override void ShutDown()
        {
            if (m_Shutdown == null)
            {
                return;
            }

            m_Shutdown.Invoke(m_HotfixGameEntry, null);
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

        private void Update()
        {
            m_Update?.Invoke(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (m_ApplicationPause == null)
            {
                return;
            }

            m_ApplicationPause.Invoke(pauseStatus);
        }

        private void OnApplicationQuit()
        {
            if (m_ApplicationQuit == null)
            {
                return;
            }

            m_ApplicationQuit.Invoke(m_HotfixGameEntry, null);
        }
        
    }
}