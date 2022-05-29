//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;
using Game;

namespace Hotfix.Framework
{
    /// <summary>
    /// 实体逻辑基类。
    /// </summary>
    public abstract class EntityLogic
    {
        private bool m_Available = false;
        private bool m_Visible = false;
        private Entity m_Entity = null;
        private Transform m_CachedTransform = null;
        private int m_OriginalLayer = 0;
        private Transform m_OriginalTransform = null;
        private HotfixEntityLogic m_GameEntityLogic;

        /// <summary>
        /// 获取实体。
        /// </summary>
        public Entity Entity => m_GameEntityLogic.Entity;

        /// <summary>
        /// 获取或设置实体名称。
        /// </summary>
        public string Name
        {
            get => m_GameEntityLogic.Name;
            set => m_GameEntityLogic.Name = value;
        }

        /// <summary>
        /// 获取实体是否可用。
        /// </summary>
        public bool Available => m_GameEntityLogic.Available;

        /// <summary>
        /// 获取或设置实体是否可见。
        /// </summary>
        public bool Visible
        {
            get => m_GameEntityLogic.Visible;
            set => m_GameEntityLogic.Visible = value;
        }

        /// <summary>
        /// 获取已缓存的 Transform。
        /// </summary>
        public Transform CachedTransform => m_GameEntityLogic.CachedTransform;

        /// <summary>
        /// 实体初始化。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected internal abstract void OnInit(object userData);

        /// <summary>
        /// 实体回收。
        /// </summary>
        protected internal abstract void OnRecycle();

        /// <summary>
        /// 实体显示。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected internal abstract void OnShow(object userData);

        /// <summary>
        /// 实体隐藏。
        /// </summary>
        /// <param name="isShutdown">是否是关闭实体管理器时触发。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected internal abstract void OnHide(bool isShutdown, object userData);

        /// <summary>
        /// 实体附加子实体。
        /// </summary>
        /// <param name="childEntity">附加的子实体。</param>
        /// <param name="parentTransform">被附加父实体的位置。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected internal abstract void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData);

        /// <summary>
        /// 实体解除子实体。
        /// </summary>
        /// <param name="childEntity">解除的子实体。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected internal abstract void OnDetached(EntityLogic childEntity, object userData);

        /// <summary>
        /// 实体附加子实体。
        /// </summary>
        /// <param name="parentEntity">被附加的父实体。</param>
        /// <param name="parentTransform">被附加父实体的位置。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected internal abstract void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData);

        /// <summary>
        /// 实体解除子实体。
        /// </summary>
        /// <param name="parentEntity">被解除的父实体。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected internal abstract void OnDetachFrom(EntityLogic parentEntity, object userData);

        /// <summary>
        /// 实体轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal abstract void OnUpdate(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 设置实体的可见性。
        /// </summary>
        /// <param name="visible">实体的可见性。</param>
        protected abstract void InternalSetVisible(bool visible);
    }
}
