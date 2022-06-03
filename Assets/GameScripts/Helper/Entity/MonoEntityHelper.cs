using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    internal sealed class MonoEntityHelper : HotfixEntityHelperBase
    {
        private Type m_HotfixType;
        private object m_HotfixInstance;
        private Action<object> m_OnInitAction;
        private Action<object> m_OnShowAction;
        private Action m_OnRecycleAction;
        private Action<bool, object> m_OnHideAction;
        private Action<EntityLogic, Transform, object> m_OnAttachedAction;
        private Action<EntityLogic, object> m_OnDetachedAction;
        private Action<EntityLogic, Transform, object> m_OnAttachToAction;
        private Action<EntityLogic, object> m_OnDetachFromAction;
        private Action<float, float> m_OnUpdateAction;
        private Action<bool> m_InternalSetVisibleAction;

        protected internal override void OnInit(string hotfixEntityType, object userData)
        {
            m_HotfixType = GameEntry.Hotfix.Mono.GetHotfixType(hotfixEntityType);
            m_HotfixInstance = GameEntry.Hotfix.Mono.CreateInstance(m_HotfixType);
            m_OnInitAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<object>>(m_HotfixType, m_HotfixInstance, "OnInit");
            m_OnShowAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<object>>(m_HotfixType, m_HotfixInstance, "OnShow");
            m_OnRecycleAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action>(m_HotfixType, m_HotfixInstance, "OnRecycle");
            m_OnHideAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<bool, object>>(m_HotfixType, m_HotfixInstance, "OnHide");
            m_OnAttachedAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<EntityLogic, Transform, object>>(m_HotfixType, m_HotfixInstance, "OnAttached");
            m_OnDetachedAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<EntityLogic, object>>(m_HotfixType, m_HotfixInstance, "OnDetached");
            m_OnAttachToAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<EntityLogic, Transform, object>>(m_HotfixType, m_HotfixInstance, "OnAttachTo");
            m_OnDetachFromAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<EntityLogic, object>>(m_HotfixType, m_HotfixInstance, "OnDetachFrom");
            m_OnUpdateAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<float, float>>(m_HotfixType, m_HotfixInstance, "OnUpdate");
            m_InternalSetVisibleAction = GameEntry.Hotfix.Mono.CreateMethodAction<Action<bool>>(m_HotfixType, m_HotfixInstance, "InternalSetVisible");
            
            m_OnInitAction.Invoke(userData);
        }
        
        protected internal override void OnShow(object userData)
        {
            m_OnShowAction.Invoke(userData);
        }
        
        protected internal override void OnRecycle()
        {
            m_OnRecycleAction.Invoke();
        }

        protected internal override void OnHide(bool isShutdown, object userData)
        {
            m_OnHideAction.Invoke(isShutdown, userData);
        }

        protected internal override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            m_OnAttachedAction.Invoke(childEntity, parentTransform, userData);
        }

        protected internal override void OnDetached(EntityLogic childEntity, object userData)
        {
            m_OnDetachedAction.Invoke(childEntity, userData);
        }

        protected internal override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            m_OnAttachToAction.Invoke(parentEntity, parentTransform, userData);
        }

        protected internal override void OnDetachFrom(EntityLogic parentEntity, object userData)
        {
            m_OnDetachFromAction.Invoke(parentEntity, userData);
        }

        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_OnUpdateAction.Invoke(elapseSeconds, realElapseSeconds);
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
            m_OnShowAction = default;
            m_OnRecycleAction = default;
            m_OnUpdateAction = default;
            m_OnHideAction = default;
            m_OnAttachedAction = default;
            m_OnAttachToAction = default;
            m_OnDetachedAction = default;
            m_OnDetachFromAction = default;
            m_InternalSetVisibleAction = default;
        }
    }
}
