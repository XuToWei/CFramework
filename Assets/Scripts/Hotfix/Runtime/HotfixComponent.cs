using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class HotfixComponent : GameFrameworkComponent
    {
        [SerializeField] private string m_HotfixHelperTypeName = "Game.ILRuntimeHelper";
        private HotfixHelperBase m_HotfixHelper;
        
        private void Start()
        {
            m_HotfixHelper = Helper.CreateHelper(m_HotfixHelperTypeName, m_HotfixHelper);
            if (m_HotfixHelper == null)
            {
                Log.Error("Can not create hotfix helper.");
                return;
            }

            m_HotfixHelper.name = "Hotfix Helper";
            Transform customHelperTrans = m_HotfixHelper.transform;
            customHelperTrans.SetParent(transform);
            customHelperTrans.localScale = Vector3.one;
        }

        public void Enter()
        {
            m_HotfixHelper.Initialize();
        }

        private void ShutDown()
        {
            m_HotfixHelper.ShutDown();
        }

        public object CreateHotfixInstance(string typeName)
        {
            return m_HotfixHelper.CreateInstance(typeName);
        }

        public object GetMethod(string typeName, string methodName, int paramCount)
        {
            return m_HotfixHelper.GetMethod(typeName, methodName, paramCount);
        }

        public object InvokeMethod(object method,object instance,params object[] objects)
        {
           return m_HotfixHelper.InvokeMethod(method,instance, objects);
        }
        
        public List<Type> GetAllTypes()
        {
           return m_HotfixHelper.GetAllTypes();
        }
        
        public Type GetHotfixType(string typeName)
        {
            return m_HotfixHelper.GetHotfixType(typeName);
        }

        public object GetHotfixGameEntry()
        {
            return m_HotfixHelper.GetHotfixGameEntry;
        }
        
        public T AddHotfixMonoBehaviour<T>(GameObject go,string hotfixFullTypeName) where T : MonoBehaviour
        {
            return m_HotfixHelper.CreateHotfixMonoBehaviour<T>(go,hotfixFullTypeName);
        }
        
#if ILRuntime
        public ILRuntime.Runtime.Enviorment.InvocationContext BeginInvoke(ILRuntime.CLR.Method.IMethod m)
        {
            return ((ILRuntimeHelper)m_HotfixHelper).BeginInvoke(m);
        }

        public ILRuntime.Runtime.Enviorment.AppDomain GetAppDomain()
        {
            return ((ILRuntimeHelper)m_HotfixHelper).AppDomain;
        }
#endif
    }
}