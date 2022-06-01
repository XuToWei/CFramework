using System;

namespace Game
{
    internal sealed class MonoHotfixUIFormLogicHelper : BaseHotfixUIFormLogicHelper
    {
        private Type m_HotfixType;
        private object m_HotfixInstance;
        private Action<object> m_OnInitAction;
        private Action<object> m_OnOpenAction;
        private Action m_OnRecycleAction;
        private Action<bool, object> m_OnCloseAction;
        private Action m_OnPauseAction;
        private Action m_OnResumeAction;
        private Action m_OnCoverAction;
        private Action m_OnRevealAction;
        private Action<object> m_OnRefocusAction;
        private Action<float, float> m_OnUpdateAction;
        private Action<int, int> m_OnDepthChangedAction;
        private Action<bool> m_InternalSetVisibleAction;
        
        protected internal override void OnInit(string hotfixUIFormLogicType, object userData)
        {
            m_HotfixType = GameEntry.Hotfix.Mono.GetHotfixType(hotfixUIFormLogicType);
            m_HotfixInstance = GameEntry.Hotfix.Mono.CreateInstance(m_HotfixType);
            m_OnInitAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<object>>(m_HotfixType, m_HotfixInstance, "OnInit");
            m_OnOpenAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<object>>(m_HotfixType, m_HotfixInstance, "OnOpen");
            m_OnRecycleAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action>(m_HotfixType, m_HotfixInstance, "OnRecycle");
            m_OnCloseAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<bool, object>>(m_HotfixType, m_HotfixInstance, "OnClose");
            m_OnPauseAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action>(m_HotfixType, m_HotfixInstance, "OnPause");
            m_OnResumeAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action>(m_HotfixType, m_HotfixInstance, "OnResume");
            m_OnCoverAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action>(m_HotfixType, m_HotfixInstance, "OnCover");
            m_OnRevealAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action>(m_HotfixType, m_HotfixInstance, "OnReveal");
            m_OnRefocusAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<object>>(m_HotfixType, m_HotfixInstance, "OnRefocus");
            m_OnUpdateAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<float, float>>(m_HotfixType, m_HotfixInstance, "OnUpdate");
            m_OnDepthChangedAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<int, int>>(m_HotfixType, m_HotfixInstance, "OnDepthChanged");
            m_InternalSetVisibleAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<bool>>(m_HotfixType, m_HotfixInstance, "InternalSetVisible");
            
            m_OnInitAction.Invoke(userData);
        }

        protected internal override void OnRecycle()
        {
            m_OnRecycleAction.Invoke();
        }

        protected internal override void OnOpen(object userData)
        {
            m_OnOpenAction.Invoke(userData);
        }

        protected internal override void OnClose(bool isShutdown, object userData)
        {
            m_OnCloseAction.Invoke(isShutdown, userData);
        }

        protected internal override void OnPause()
        {
            m_OnPauseAction.Invoke();
        }

        protected internal override void OnResume()
        {
            m_OnResumeAction.Invoke();
        }

        protected internal override void OnCover()
        {
            m_OnCoverAction.Invoke();
        }

        protected internal override void OnReveal()
        {
            m_OnRecycleAction.Invoke();
        }

        protected internal override void OnRefocus(object userData)
        {
            m_OnRefocusAction.Invoke(userData);
        }

        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_OnUpdateAction.Invoke(elapseSeconds, realElapseSeconds);
        }

        protected internal override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            m_OnDepthChangedAction.Invoke(uiGroupDepth, depthInUIGroup);
        }

        protected internal override void InternalSetVisible(bool visible)
        {
            m_InternalSetVisibleAction.Invoke(visible);
        }

        public override void Clear()
        {
            m_HotfixType = default;
            m_HotfixInstance = default;
            m_OnInitAction = default;
            m_OnOpenAction = default;
            m_OnRecycleAction = default;
            m_OnCloseAction = default;
            m_OnPauseAction = default;
            m_OnResumeAction = default;
            m_OnCoverAction = default;
            m_OnRevealAction = default;
            m_OnRefocusAction = default;
            m_OnUpdateAction = default;
            m_OnDepthChangedAction = default;
            m_InternalSetVisibleAction = default;
        }
    }
}
