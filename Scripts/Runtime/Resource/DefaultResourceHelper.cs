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
        /// <typeparam name="T"></typeparam>
        /// <returns>异步加载资源句柄</returns>
        public override AsyncOperationHandleBase<T> LoadAsset<T>(string assetName)
        {
            return new ResourcesLoadAsyncOperationHandle(assetName, Resources.LoadAsync(assetName.Replace(Path.GetExtension(assetName), "")));
        }
        
        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <returns>异步加载资源句柄</returns>
        public override AsyncOperationHandleBase LoadAsset(string assetName)
        {
            return new ResourcesLoadAsyncOperationHandle(assetName, Resources.LoadAsync(assetName.Replace(Path.GetExtension(assetName), "")));
        }

        /// <summary>
        /// 卸载资源。
        /// </summary>
        /// <param name="handle">要卸载的资源加载句柄。</param>
        public override void UnloadAsset(AsyncOperationHandleBase handle)
        {
            Resources.UnloadAsset(handle.Result as Object);
        }

        /// <summary>
        /// 实例化资源。
        /// </summary>
        /// <param name="asset">要实例化的资源。</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>资源实例</returns>
        public override T Instantiate<T>(object asset)
        {
            return Instantiate(asset) as T;
        }

        /// <summary>
        /// 实例化资源。
        /// </summary>
        /// <param name="asset">要实例化的资源。</param>
        /// <returns>资源实例</returns>
        public override object Instantiate(object asset)
        {
            return Object.Instantiate(asset as Object);
        }

        /// <summary>
        /// 释放并且销毁实例化资源
        /// </summary>
        /// <param name="instance">资源实例</param>
        /// <param name="asset">原始资源</param>
        public override void ReleaseInstance(object instance, object asset)
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
            string sceneName = SceneComponent.GetSceneName(sceneAssetName);
            return new LoadSceneAsyncOperationHandle(sceneAssetName, sceneName, SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        }

        /// <summary>
        /// 异步卸载场景。
        /// </summary>
        /// <param name="handle">要卸载场景加载句柄。</param>
        /// <returns>异步卸载场景句柄</returns>
        public override AsyncOperationHandleBase UnloadScene(AsyncOperationHandleBase handle)
        {
            string sceneName = SceneComponent.GetSceneName(handle.AssetName);
            return new UnloadSceneAsyncOperationHandle(handle.AssetName, sceneName, SceneManager.UnloadSceneAsync(sceneName));
        }
    }
}