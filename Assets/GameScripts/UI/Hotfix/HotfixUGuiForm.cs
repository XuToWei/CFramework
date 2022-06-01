using GameFramework;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 热更新层UGUI界面
    /// </summary>
    [DisallowMultipleComponent]
    public class HotfixUGuiForm : UGuiForm
    {
        /// <summary>
        /// 对应的热更新层UGUI界面类名
        /// </summary>
        [SerializeField] private string m_HotfixUIFormLogicType;

        private BaseHotfixUIFormLogicHelper m_UIFormLogicHelper;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            if (GameEntry.Hotfix.HotfixType == HotfixType.Mono)
            {
                m_UIFormLogicHelper = ReferencePool.Acquire<MonoHotfixUIFormLogicHelper>();
            }
#if ILRuntime
            else if (GameEntry.Hotfix.HotfixType == HotfixType.ILRuntime)
            {
                m_UIFormLogicHelper = ReferencePool.Acquire<ILRuntimeUIFormLogicHelper>();
            }
#endif
            m_UIFormLogicHelper.OnInit(m_HotfixUIFormLogicType, userData);
        }
        
        private void OnDestroy()
        {
            ReferencePool.Release(m_UIFormLogicHelper);
            m_UIFormLogicHelper = null;
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();
            m_UIFormLogicHelper.OnRecycle();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_UIFormLogicHelper.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            m_UIFormLogicHelper.OnClose(isShutdown, userData);
        }

        protected override void OnPause()
        {
            base.OnPause();
            m_UIFormLogicHelper.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            m_UIFormLogicHelper.OnResume();
        }

        protected override void OnCover()
        {
            base.OnCover();
            m_UIFormLogicHelper.OnCover();
        }

        protected override void OnReveal()
        {
            base.OnReveal();
            m_UIFormLogicHelper.OnReveal();
        }

        protected override void OnRefocus(object userData)
        {
            base.OnRefocus(userData);
            m_UIFormLogicHelper.OnRefocus(userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_UIFormLogicHelper.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
            m_UIFormLogicHelper.OnDepthChanged(uiGroupDepth, depthInUIGroup);
        }

        protected override void InternalSetVisible(bool visible)
        {
            base.InternalSetVisible(visible);
            m_UIFormLogicHelper.InternalSetVisible(visible);
        }
    }
}