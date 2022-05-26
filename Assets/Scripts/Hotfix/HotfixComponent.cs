using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFramework;
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
            if (!GameEntry.Base.EditorResourceMode)
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

        public void OnEnter()
        {
            m_HotfixHelper.OnEnter();
        }
        
        public void OnShutDown()
        {
            m_HotfixHelper.OnShutDown();
        }

        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_HotfixHelper.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        public async Task Load()
        {
            await m_HotfixHelper.Load();
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

        public Type GetHotfixType(string typeName)
        {
            return m_HotfixHelper.GetHotfixType(typeName);
        }

#if UNITY_EDITOR
        public bool IsMonoHelper()
        {
            return m_HotfixHelperTypeName == "Game.MonoHelper";
        }
        
        public void Reload()
        {
            if (!IsMonoHelper())
            {
                throw new GameFrameworkException(Utility.Text.Format("[0] can't reload, can use Game.MonoHelper to reload!", m_HotfixHelperTypeName));
            }
            ((MonoHelper)m_HotfixHelper).Reload();
        }
#endif

#if ILRuntime
        public ILRuntime.Runtime.Enviorment.InvocationContext BeginInvokeMethod(ILRuntime.CLR.Method.IMethod m)
        {
            return ((ILRuntimeHelper)m_HotfixHelper).BeginInvokeMethod(m);
        }

        public ILRuntime.Runtime.Enviorment.AppDomain AppDomain => ((ILRuntimeHelper)m_HotfixHelper).AppDomain;
#endif
    }
}