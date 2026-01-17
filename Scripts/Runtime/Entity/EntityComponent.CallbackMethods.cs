using System;

namespace UnityGameFramework.Runtime
{
    public sealed partial class EntityComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="onSucceeded">成功回调</param>
        public void ShowEntity<T>(int entityId, string entityAssetName, string entityGroupName, Action<T> onSucceeded) where T : EntityLogic
        {
            ShowEntity(entityId, typeof(T), entityAssetName, entityGroupName, null, e => { onSucceeded?.Invoke(e.Entity.GetComponent<T>()); }, null);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="onSucceeded">成功回调</param>
        /// <param name="onFailed">失败回调</param>
        public void ShowEntity<T>(int entityId, string entityAssetName, string entityGroupName, Action<T> onSucceeded, Action<string> onFailed) where T : EntityLogic
        {
            ShowEntity(entityId, typeof(T), entityAssetName, entityGroupName, null, e => { onSucceeded?.Invoke(e.Entity.GetComponent<T>()); }, e => { onFailed?.Invoke(e.ErrorMessage); });
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="onSucceeded">成功回调</param>
        public void ShowEntity(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName, Action<EntityLogic> onSucceeded)
        {
            ShowEntity(entityId, entityLogicType, entityAssetName, entityGroupName, null, e => { onSucceeded?.Invoke(e.Entity.GetComponent<EntityLogic>()); }, null);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="onSucceeded">成功回调</param>
        /// <param name="onFailed">失败回调</param>
        public void ShowEntity(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName, Action<EntityLogic> onSucceeded, Action<string> onFailed)
        {
            ShowEntity(entityId, entityLogicType, entityAssetName, entityGroupName, null, e => { onSucceeded?.Invoke(e.Entity.GetComponent<EntityLogic>()); }, e => { onFailed?.Invoke(e.ErrorMessage); });
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="onSucceeded">成功回调</param>
        public void ShowEntity<T>(int entityId, string entityAssetName, string entityGroupName, object userData, Action<T> onSucceeded) where T : EntityLogic
        {
            ShowEntity(entityId, typeof(T), entityAssetName, entityGroupName, userData, e => { onSucceeded?.Invoke(e.Entity.GetComponent<T>()); }, null);
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="onSucceeded">成功回调</param>
        /// <param name="onFailed">失败回调</param>
        public void ShowEntity<T>(int entityId, string entityAssetName, string entityGroupName, object userData, Action<T> onSucceeded, Action<string> onFailed) where T : EntityLogic
        {
            ShowEntity(entityId, typeof(T), entityAssetName, entityGroupName, userData, e => { onSucceeded?.Invoke(e.Entity.GetComponent<T>()); }, e => { onFailed?.Invoke(e.ErrorMessage); });
        }
    }
}