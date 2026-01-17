#if UNITASK_SUPPORT
using System;
using Cysharp.Threading.Tasks;

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
        public async UniTask<T> ShowEntityAsync<T>(int entityId, string entityAssetName, string entityGroupName) where T : EntityLogic
        {
            var tsc = new UniTaskCompletionSource<T>();
            ShowEntity(entityId, typeof(T), entityAssetName, entityGroupName, null, e => { tsc.TrySetResult(e as T); }, e => { tsc.TrySetException(new Exception($"显示实体失败: {e}")); });
            return await tsc.Task;
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityLogicType">实体逻辑类型。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        public async UniTask<EntityLogic> ShowEntityAsync(int entityId, Type entityLogicType, string entityAssetName, string entityGroupName)
        {
            var tsc = new UniTaskCompletionSource<EntityLogic>();
            ShowEntity(entityId, entityLogicType, entityAssetName, entityGroupName, null, e => { tsc.TrySetResult(e); }, e => { tsc.TrySetException(new Exception($"显示实体失败: {e}")); });
            return await tsc.Task;
        }

        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        /// <param name="entityId">实体编号。</param>
        /// <param name="entityAssetName">实体资源名称。</param>
        /// <param name="entityGroupName">实体组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        public async UniTask<T> ShowEntityAsync<T>(int entityId, string entityAssetName, string entityGroupName, object userData) where T : EntityLogic
        {
            var tsc = new UniTaskCompletionSource<T>();
            ShowEntity(entityId, typeof(T), entityAssetName, entityGroupName, userData, e => { tsc.TrySetResult(e as T); }, e => { tsc.TrySetException(new Exception($"显示实体失败: {e}")); });
            return await tsc.Task;
        }
    }
}
#endif