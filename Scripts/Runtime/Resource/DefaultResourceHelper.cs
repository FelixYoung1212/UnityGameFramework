using System.IO;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 默认资源辅助器
    /// </summary>
    public sealed class DefaultResourceHelper : ResourceHelperBase
    {
        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <returns>异步加载资源句柄</returns>
        public override AsyncOperationHandleBase LoadAsset(string assetName)
        {
            return new ResourcesLoadAsyncOperationHandle(Resources.LoadAsync(assetName.Replace(Path.GetExtension(assetName), "")));
        }

        /// <summary>
        /// 卸载资源。
        /// </summary>
        /// <param name="asset">要卸载的资源。</param>
        public override void UnloadAsset(object asset)
        {
            Resources.UnloadAsset(asset as Object);
        }

        /// <summary>
        /// 实例化资源。
        /// </summary>
        /// <param name="asset">要实例化的资源。</param>
        /// <returns>资源实例</returns>
        public override object Instantiate(object asset)
        {
            return MonoBehaviour.Instantiate(asset as Object);
        }

        /// <summary>
        /// 释放并且销毁实例化资源
        /// </summary>
        /// <param name="instance"></param>
        public override void ReleaseInstance(object instance)
        {
            Destroy(instance as Object);
        }

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">要加载场景资源的名称。</param>
        /// <returns>异步加载场景句柄</returns>
        public override AsyncOperationHandleBase LoadScene(string sceneAssetName)
        {
            string sceneName = sceneAssetName.Replace(Path.GetExtension(sceneAssetName), "").Replace("Assets/", "");
            return new LoadSceneAsyncOperationHandle(sceneName, SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        }

        /// <summary>
        /// 异步卸载场景。
        /// </summary>
        /// <param name="sceneAssetName">要卸载场景资源的名称。</param>
        /// <returns>异步卸载场景句柄</returns>
        public override AsyncOperationHandleBase UnloadScene(string sceneAssetName)
        {
            string sceneName = sceneAssetName.Replace(Path.GetExtension(sceneAssetName), "").Replace("Assets/", "");
            return new UnloadSceneAsyncOperationHandle(sceneName, SceneManager.UnloadSceneAsync(sceneName));
        }
    }
}