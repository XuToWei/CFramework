#if ILRuntime
using GameFramework;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    internal sealed class ILRuntimeEntityLogicHelper : BaseHotfixEntityLogicHelper
    {
        private ILType m_HotfixType;
        private object m_HotfixInstance;
        
        private IMethod m_OnInitMethod;
        private IMethod m_OnShowMethod;
        private IMethod m_OnHideMethod;
        private IMethod m_OnRecycleMethod;
        private IMethod m_OnAttachedMethod;
        private IMethod m_OnDetachedMethod;
        private IMethod m_OnAttachToMethod;
        private IMethod m_OnDetachFromMethod;
        private IMethod m_OnUpdateMethod;
        private IMethod m_InternalSetVisibleMethod;

        protected internal override void OnInit(string hotfixType, object userData)
        {
            m_HotfixType = GameEntry.Hotfix.ILRuntime.AppDomain.LoadedTypes[hotfixType] as ILType;
            m_OnInitMethod = m_HotfixType.GetMethod("OnInit");
            m_OnShowMethod = m_HotfixType.GetMethod("OnShow");
            m_OnHideMethod = m_HotfixType.GetMethod("OnHide");
            m_OnRecycleMethod = m_HotfixType.GetMethod("OnRecycle");
            m_OnAttachedMethod = m_HotfixType.GetMethod("OnAttached");
            m_OnDetachedMethod = m_HotfixType.GetMethod("OnDetached");
            m_OnAttachToMethod = m_HotfixType.GetMethod("OnAttachTo");
            m_OnDetachFromMethod = m_HotfixType.GetMethod("OnDetachFrom");
            m_OnUpdateMethod = m_HotfixType.GetMethod("OnUpdate");
            m_InternalSetVisibleMethod = m_HotfixType.GetMethod("InternalSetVisible");

            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnInitMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushObject(userData);
            ctx.Invoke();
        }

        protected internal override void OnShow(object userData)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnShowMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushObject(userData);
            ctx.Invoke();
        }

        protected internal override void OnHide(bool isShutdown, object userData)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnHideMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushBool(isShutdown);
            ctx.PushObject(userData);
            ctx.Invoke();
        }

        protected internal override void OnRecycle()
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnRecycleMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.Invoke();
        }
        
        protected internal override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnAttachedMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushObject(childEntity);
            ctx.PushObject(parentTransform);
            ctx.PushObject(userData);
            ctx.Invoke();
        }

        protected internal override void OnDetached(EntityLogic childEntity, object userData)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnDetachedMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushObject(childEntity);
            ctx.PushObject(userData);
            ctx.Invoke();
        }

        protected internal override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnAttachToMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushObject(parentEntity);
            ctx.PushObject(parentTransform);
            ctx.PushObject(userData);
            ctx.Invoke();
        }

        protected internal override void OnDetachFrom(EntityLogic parentEntity, object userData)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnDetachFromMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushObject(parentEntity);
            ctx.PushObject(userData);
            ctx.Invoke();
        }

        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            using InvocationContext ctx = GameEntry.Hotfix.ILRuntime.AppDomain.BeginInvoke(m_OnUpdateMethod);
            ctx.PushObject(m_HotfixInstance);
            ctx.PushObject(elapseSeconds);
            ctx.PushObject(realElapseSeconds);
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
            m_HotfixType = default;
            m_OnInitMethod = default;
            m_OnShowMethod = default;
            m_OnHideMethod = default;
            m_OnRecycleMethod = default;
            m_OnAttachedMethod = default;
            m_OnDetachedMethod = default;
            m_OnAttachToMethod = default;
            m_OnDetachFromMethod = default;
            m_OnUpdateMethod = default;
            m_InternalSetVisibleMethod = default;
        }
    }
}
#endif
