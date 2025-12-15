using GameFramework.Resource;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 资源辅助器基类
    /// </summary>
    public abstract class ResourceHelperBase : MonoBehaviour, IResourceHelper
    {
        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>异步加载资源句柄</returns>
        public abstract AsyncOperationHandleBase<T> LoadAsset<T>(string assetName) where T : class;
        
        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <returns>异步加载资源句柄</returns>
       public abstract AsyncOperationHandleBase LoadAsset(string assetName);

        /// <summary>
        /// 卸载资源。
        /// </summary>
        /// <param name="handle">要卸载的资源加载句柄。</param>
        public abstract void UnloadAsset(AsyncOperationHandleBase handle);

        /// <summary>
        /// 实例化资源。
        /// </summary>
        /// <param name="asset">要实例化的资源。</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>资源实例</returns>
        public abstract T Instantiate<T>(object asset) where T : class;

        /// <summary>
        /// 实例化资源。
        /// </summary>
        /// <param name="asset">要实例化的资源。</param>
        /// <returns>资源实例</returns>
        public abstract object Instantiate(object asset);

        /// <summary>
        /// 释放并且销毁实例化资源
        /// </summary>
        /// <param name="instance">资源实例</param>
        /// <param name="asset">原始资源</param>
        public abstract void ReleaseInstance(object instance, object asset);

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">要加载场景资源的名称。</param>
        /// <returns>异步加载场景句柄</returns>
        public abstract AsyncOperationHandleBase LoadScene(string sceneAssetName);

        /// <summary>
        /// 异步卸载场景。
        /// </summary>
        /// <param name="handle">要卸载场景加载句柄。</param>
        /// <returns>异步卸载场景句柄</returns>
        public abstract AsyncOperationHandleBase UnloadScene(AsyncOperationHandleBase handle);
    }
}