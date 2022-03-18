using System;
using System.Threading.Tasks;
using Game;
using UnityGameFramework.Runtime;
using GameEntry = Game.GameEntry;

namespace Hotfix
{
    public static class EntityExtension
    {
        public static void HideEntity(this EntityComponent entityComponent, Entity entityLogic)
        {
            entityComponent.HideEntity(entityLogic);
        }

        public static EntityLogic GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (EntityLogic)entity.Logic;
        }

        public static void HideEntity(this EntityComponent entityComponent, EntityLogic entityLogic)
        {
            entityComponent.HideEntity(entityLogic.Entity);
        }

        public static void AttachEntity(this EntityComponent entityComponent, EntityLogic entityLogic, int ownerId,
            string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entityLogic.Entity, ownerId, parentTransformPath, userData);
        }

        /// <summary>
        /// 显示实体
        /// </summary>
        private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup,
            int priority, EntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            DREntity drEntity = GameEntry.DataTableExtension.GetDataRow<DREntity>(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup,
                priority, data);
        }

        /// <summary>
        /// 显示实体
        /// </summary>
        public static void ShowEntity(this EntityComponent entityComponent, Type logicType, int priority,
            EntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            DREntity drEntity = GameEntry.DataTableExtension.GetDataRow<DREntity>(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName),
                drEntity.EntityGroupName, priority, data);
        }

        /// <summary>
        /// 显示实体（可等待）
        /// </summary>
        public static Task<Entity> ShowEntityAsync(this EntityComponent entityComponent, Type logicType, int priority,
            EntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return (Task<Entity>)Task.FromException(new Exception("Data is invalid."));
            }

            DREntity drEntity = GameEntry.DataTableExtension.GetDataRow<DREntity>(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return (Task<Entity>)Task.FromException(
                    new Exception($"Can not load entity id '{data.TypeId.ToString()}' from data table."));
            }

            return entityComponent.ShowEntityAsync(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName),
                drEntity.EntityGroupName, priority, data);
        }
    }
}