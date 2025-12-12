using GameFramework.Resource;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityGameFramework.Runtime
{
    public sealed class LoadSceneAsyncOperationHandle : UnityAsyncOperationHandle<AsyncOperation>
    {
        private string m_SceneAssetName;

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

                if (SceneManager.GetSceneByName(m_SceneAssetName).isLoaded)
                {
                    return AsyncOperationStatus.Succeeded;
                }

                return AsyncOperationStatus.Failed;
            }
        }

        public LoadSceneAsyncOperationHandle(string sceneAssetName, AsyncOperation operation) : base(operation)
        {
            m_SceneAssetName = sceneAssetName;
        }
    }
}