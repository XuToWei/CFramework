using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class HotfixComponent : GameFrameworkComponent
    {
        [SerializeField]
        private string m_HotfixHelperTypeName = "Game.ILRuntimeHelper";
        
        [SerializeField]
        private HotfixHelperBase m_CustomHotfixHelper = null;
        
        private HotfixHelperBase m_HotfixHelper;

        /// <summary>
        /// 更新层生命周期
        /// </summary>
        public HotfixLifeCircle HotfixLifeCircle
        {
            private set;
            get;
        }
        
        public object HotfixGameEntry => m_HotfixHelper.HotfixGameEntry;

        private void Start()
        {
            if (GameEntry.Base.EditorResourceMode && GameEntry.Resource.ResourceMode != ResourceMode.Package)
            {
                m_HotfixHelperTypeName = "Game.ILRuntimeHelper";
            }
            m_HotfixHelper = Helper.CreateHelper(m_HotfixHelperTypeName, m_HotfixHelper);
            if (m_HotfixHelper == null)
            {
                Log.Error("Can not create hotfix helper.");
                return;
            }

            m_HotfixHelper.name = "Hotfix Helper";
            Transform customHelperTrans = m_HotfixHelper.transform;
            customHelperTrans.SetParent(transform);
            customHelperTrans.localPosition = Vector3.zero;
            customHelperTrans.localScale = Vector3.one;
        }

        public void SetLifeCircleAction(Action start, Action<float, float> update,
            Action shutDown, Action<bool> onApplicationPause,
            Action onApplicationQuit)
        {
            HotfixLifeCircle = new HotfixLifeCircle(start, update, shutDown, onApplicationPause, onApplicationQuit);
        }

        public void Enter()
        {
            m_HotfixHelper.Enter();
        }

        public async Task<bool> Load()
        {
            return await m_HotfixHelper.Load();
        }

        public void ShutDown()
        {
            m_HotfixHelper.ShutDown();
        }

        public object CreateInstance(string typeName)
        {
            return m_HotfixHelper.CreateInstance(typeName);
        }

        public object GetMethod(string typeName, string methodName, int paramCount)
        {
            return m_HotfixHelper.GetMethod(typeName, methodName, paramCount);
        }

        public object InvokeMethod(object method, object instance, params object[] objects)
        {
            return m_HotfixHelper.InvokeMethod(method, instance, objects);
        }

        public List<Type> GetAllTypes()
        {
            return m_HotfixHelper.GetAllTypes();
        }

        public Type GetHotfixType(string typeName)
        {
            return m_HotfixHelper.GetHotfixType(typeName);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            HotfixLifeCircle?.OnApplicationPause?.Invoke(pauseStatus);
        }

        private void OnApplicationQuit()
        {
            HotfixLifeCircle?.OnApplicationQuit?.Invoke();
        }

#if ILRuntime
        public ILRuntime.Runtime.Enviorment.InvocationContext BeginInvokeMethod(ILRuntime.CLR.Method.IMethod m)
        {
            return ((ILRuntimeHelper)m_HotfixHelper).BeginInvokeMethod(m);
        }

        public ILRuntime.Runtime.Enviorment.AppDomain AppDomain => ((ILRuntimeHelper)m_HotfixHelper).AppDomain;
#endif
    }
}