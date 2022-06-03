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
        [SerializeField] private string m_HotfixUIFormType;

        private HotfixUIFormHelperBase m_HotfixUIFormHelper;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            if (GameEntry.Hotfix.HotfixType == HotfixType.Mono)
            {
                m_HotfixUIFormHelper = ReferencePool.Acquire<MonoHotfixUIFormHelper>();
            }
#if ILRuntime
            else if (GameEntry.Hotfix.HotfixType == HotfixType.ILRuntime)
            {
                m_HotfixUIFormHelper = ReferencePool.Acquire<ILRuntimeUIFormHelper>();
            }
#endif
            m_HotfixUIFormHelper.OnInit(m_HotfixUIFormType, userData);
        }
        
        private void OnDestroy()
        {
            ReferencePool.Release(m_HotfixUIFormHelper);
            m_HotfixUIFormHelper = null;
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();
            m_HotfixUIFormHelper.OnRecycle();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_HotfixUIFormHelper.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            m_HotfixUIFormHelper.OnClose(isShutdown, userData);
        }

        protected override void OnPause()
        {
            base.OnPause();
            m_HotfixUIFormHelper.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            m_HotfixUIFormHelper.OnResume();
        }

        protected override void OnCover()
        {
            base.OnCover();
            m_HotfixUIFormHelper.OnCover();
        }

        protected override void OnReveal()
        {
            base.OnReveal();
            m_HotfixUIFormHelper.OnReveal();
        }

        protected override void OnRefocus(object userData)
        {
            base.OnRefocus(userData);
            m_HotfixUIFormHelper.OnRefocus(userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            m_HotfixUIFormHelper.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
            m_HotfixUIFormHelper.OnDepthChanged(uiGroupDepth, depthInUIGroup);
        }

        protected override void InternalSetVisible(bool visible)
        {
            base.InternalSetVisible(visible);
            m_HotfixUIFormHelper.InternalSetVisible(visible);
        }
    }
}