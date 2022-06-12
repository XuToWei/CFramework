#if ILRuntime
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;

namespace Game
{
    internal sealed class ILRuntimeUIFormHelper : HotfixUIFormHelperBase
    {
        private ILType m_HotfixType;
        private object m_HotfixInstance;
        
        private IMethod m_OnInitMethod;
        private IMethod m_OnRecycleMethod;
        private IMethod m_OnOpenMethod;
        private IMethod m_OnCloseMethod;
        private IMethod m_OnPauseMethod;
        private IMethod m_OnResumeMethod;
        private IMethod m_OnCoverMethod;
        private IMethod m_OnRevealMethod;
        private IMethod m_OnRefocusMethod;
        private IMethod m_OnUpdateMethod;
        private IMethod m_OnDepthChangedMethod;
        private IMethod m_InternalSetVisibleMethod;
        
        protected internal override void OnInit(string hotfixUIFormType, object userData)
        {
            m_HotfixType = GameEntry.Hotfix.ILRuntime.AppDomain.LoadedTypes[hotfixUIFormType] as ILType;
            m_HotfixInstance = m_HotfixType.Instantiate();
            m_OnInitMethod = m_HotfixType.GetMethod("OnInit");
            m_OnRecycleMethod = m_HotfixType.GetMethod("OnRecycle");
            m_OnOpenMethod = m_HotfixType.GetMethod("OnOpen");
            m_OnCloseMethod = m_HotfixType.GetMethod("OnClose");
            m_OnPauseMethod = m_HotfixType.GetMethod("OnPause");
            m_OnResumeMethod = m_HotfixType.GetMethod("OnResume");
            m_OnCoverMethod = m_HotfixType.GetMethod("OnCover");
            m_OnRevealMethod = m_HotfixType.GetMethod("OnReveal");
            m_OnRefocusMethod = m_HotfixType.GetMethod("OnRefocus");
            m_OnUpdateMethod = m_HotfixType.GetMethod("OnUpdate");
            m_OnDepthChangedMethod = m_HotfixType.GetMethod("OnDepthChanged");
            m_InternalSetVisibleMethod = m_HotfixType.GetMethod("InternalSetVisible");
            
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnInitMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushObject(userData);
            ctx.Invoke();
        }

        protected internal override void OnRecycle()
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnRecycleMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.Invoke();
        }

        protected internal override void OnOpen(object userData)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnOpenMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushObject(userData);
            ctx.Invoke();
        }

        protected internal override void OnClose(bool isShutdown, object userData)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnCloseMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushBool(isShutdown);
            ctx.PushObject(userData);
            ctx.Invoke();
        }

        protected internal override void OnPause()
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnPauseMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.Invoke();
        }

        protected internal override void OnResume()
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnResumeMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.Invoke();
        }

        protected internal override void OnCover()
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnCoverMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.Invoke();
        }

        protected internal override void OnReveal()
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnRevealMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.Invoke();
        }

        protected internal override void OnRefocus(object userData)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnRefocusMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.Invoke();
        }

        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnUpdateMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushFloat(elapseSeconds);
            ctx.PushFloat(realElapseSeconds);
            ctx.Invoke();
        }

        protected internal override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnDepthChangedMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushInteger(uiGroupDepth);
            ctx.PushInteger(depthInUIGroup);
            ctx.Invoke();
        }

        protected internal override void InternalSetVisible(bool visible)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_InternalSetVisibleMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushBool(visible);
            ctx.Invoke();
        }

        public override void Clear()
        {
            m_HotfixType = null;
            m_HotfixInstance = null;
            m_OnInitMethod = null;
            m_OnRecycleMethod = null;
            m_OnOpenMethod = null;
            m_OnCloseMethod = null;
            m_OnPauseMethod = null;
            m_OnResumeMethod = null;
            m_OnCoverMethod = null;
            m_OnRevealMethod = null;
            m_OnRefocusMethod = null;
            m_OnUpdateMethod = null;
            m_OnDepthChangedMethod = null;
            m_InternalSetVisibleMethod = null;
        }
    }
}
#endif
