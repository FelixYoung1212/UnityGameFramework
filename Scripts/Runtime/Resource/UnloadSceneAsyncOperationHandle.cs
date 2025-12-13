using GameFramework.Resource;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 使用SceneManager.UnloadSceneAsync加载返回的句柄
    /// </summary>
    public sealed class UnloadSceneAsyncOperationHandle : UnityAsyncOperationHandle<AsyncOperation>
    {
        private string m_FixedSceneName;

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

                if (!SceneManager.GetSceneByName(m_FixedSceneName).isLoaded)
                {
                    return AsyncOperationStatus.Succeeded;
                }

                return AsyncOperationStatus.Failed;
            }
        }

        public UnloadSceneAsyncOperationHandle(string sceneAssetName, string fixedSceneName, AsyncOperation operation) : base(sceneAssetName, operation)
        {
            m_FixedSceneName = fixedSceneName;
        }
    }
}