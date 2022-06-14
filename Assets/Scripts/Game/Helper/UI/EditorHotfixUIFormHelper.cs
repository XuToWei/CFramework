#if UNITY_EDITOR
using System;

namespace Game
{
    internal sealed class EditorUIFormHelper : HotfixUIFormHelperBase
    {
        private Type m_HotfixProxyType;
        private object m_HotfixProxyInstance;
        
        private Action<string, HotfixUIForm, object> m_OnInitAction;
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
        
        protected internal override void OnInit(string hotfixUIFormType, HotfixUIForm hotfixUIForm, object userData)
        { 
            m_HotfixProxyType = GameEntry.Hotfix.Editor.GetHotfixType(HotfixProxyTypeName);
            m_HotfixProxyInstance = GameEntry.Hotfix.Editor.Invoke(m_HotfixProxyType, "Acquire", null, null);
            
            m_OnInitAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action<string, HotfixUIForm, object>>(m_HotfixProxyType, m_HotfixProxyInstance, "OnInit");
            m_OnOpenAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action<object>>(m_HotfixProxyType, m_HotfixProxyInstance, "OnOpen");
            m_OnRecycleAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action>(m_HotfixProxyType, m_HotfixProxyInstance, "OnRecycle");
            m_OnCloseAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action<bool, object>>(m_HotfixProxyType, m_HotfixProxyInstance, "OnClose");
            m_OnPauseAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action>(m_HotfixProxyType, m_HotfixProxyInstance, "OnPause");
            m_OnResumeAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action>(m_HotfixProxyType, m_HotfixProxyInstance, "OnResume");
            m_OnCoverAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action>(m_HotfixProxyType, m_HotfixProxyInstance, "OnCover");
            m_OnRevealAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action>(m_HotfixProxyType, m_HotfixProxyInstance, "OnReveal");
            m_OnRefocusAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action<object>>(m_HotfixProxyType, m_HotfixProxyInstance, "OnRefocus");
            m_OnUpdateAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action<float, float>>(m_HotfixProxyType, m_HotfixProxyInstance, "OnUpdate");
            m_OnDepthChangedAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action<int, int>>(m_HotfixProxyType, m_HotfixProxyInstance, "OnDepthChanged");
            m_InternalSetVisibleAction = GameEntry.Hotfix.Editor.CreateMethodAction<Action<bool>>(m_HotfixProxyType, m_HotfixProxyInstance, "InternalSetVisible");
            
            m_OnInitAction.Invoke(hotfixUIFormType, hotfixUIForm, userData);
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
            m_OnRevealAction.Invoke();
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

            GameEntry.Hotfix.Editor.Invoke(m_HotfixProxyType, "Release", null, m_HotfixProxyInstance);
            m_HotfixProxyType = default;
            m_HotfixProxyInstance = default;
        }
    }
}
#endif
