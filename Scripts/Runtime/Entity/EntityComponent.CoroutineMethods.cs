using System;
using System.Collections;

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
        public IEnumerator ShowEntityCoroutine<T>(int entityId, string entityAssetName, string entityGroupName) where T : EntityLogic
        {
            var isComplete = false;
            ShowEntity(entityId, typeof(T), entityAssetName, entityGroupName, null, e => { isComplete = true; }, e => { isComplete = true; });
            while (!isComplete)
            {
                yield return null;
            }
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        public IEnumerator ShowEntityCoroutine(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName)
        {
            var isComplete = false;
            ShowEntity(entityId, entityLogicType, entityAssetName, entityGroupName, null, e => { isComplete = true; }, e => { isComplete = true; });
            while (!isComplete)
            {
                yield return null;
            }
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        public IEnumerator ShowEntityCoroutine<T>(int entityId, string entityAssetName, string entityGroupName, object userData) where T : EntityLogic
        {
            var isComplete = false;
            ShowEntity(entityId, typeof(T), entityAssetName, entityGroupName, userData, e => { isComplete = true; }, e => { isComplete = true; });
            while (!isComplete)
            {
                yield return null;
            }
        }
    }
}