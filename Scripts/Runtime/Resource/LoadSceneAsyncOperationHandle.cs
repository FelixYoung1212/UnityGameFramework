using GameFramework.Resource;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 使用SceneManager.LoadSceneAsync加载返回的句柄
    /// </summary>
    public class LoadSceneAsyncOperationHandle : UnityAsyncOperationHandle<AsyncOperation>
    {
        private string m_FixedSceneName;

        public override AsyncOperationStatus Status
        {
            get
            {
                if (Handle == null)
                {
                    return AsyncOperationStatus.Failed;
                }

                if (!Handle.isDone)
                {
                    return AsyncOperationStatus.None;
                }

                if (SceneManager.GetSceneByName(m_FixedSceneName).isLoaded)
                {
                    return AsyncOperationStatus.Succeeded;
                }

                return AsyncOperationStatus.Failed;
            }
        }

        public LoadSceneAsyncOperationHandle(string sceneAssetName, string fixedSceneName, AsyncOperation handle) : base(sceneAssetName, handle)
        {
            m_FixedSceneName = fixedSceneName;
        }
    }
}