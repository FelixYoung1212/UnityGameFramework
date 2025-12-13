using GameFramework.Resource;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 使用SceneManager.LoadSceneAsync加载返回的句柄
    /// </summary>
    public sealed class LoadSceneAsyncOperationHandle : UnityAsyncOperationHandle<AsyncOperation>
    {
        public override AsyncOperationStatus Status
        {
            get
            {
                if (Operation == null)
                {
                    return AsyncOperationStatus.Failed;
                }

                if (!Operation.isDone)
                {
                    return AsyncOperationStatus.None;
                }

                if (SceneManager.GetSceneByName(AssetName).isLoaded)
                {
                    return AsyncOperationStatus.Succeeded;
                }

                return AsyncOperationStatus.Failed;
            }
        }

        public LoadSceneAsyncOperationHandle(string sceneAssetName, AsyncOperation operation) : base(sceneAssetName, operation)
        {
        }
    }
}