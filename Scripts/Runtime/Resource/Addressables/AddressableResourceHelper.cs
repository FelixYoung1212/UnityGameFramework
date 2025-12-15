#if ADDRESSABLES_SUPPORT
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace UnityGameFramework.Runtime
{
    public class AddressableResourceHelper : ResourceHelperBase
    {
        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <param name="assetName">要加载资源的名称。</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>异步加载资源句柄</returns>
        public override AsyncOperationHandleBase LoadAsset<T>(string assetName)
        {
            return new AddressableAsyncOperationHandle<T>(assetName, Addressables.LoadAssetAsync<T>(assetName));
        }

        /// <summary>
        /// 卸载资源。
        /// </summary>
        /// <param name="handle">要卸载的资源加载句柄。</param>
        public override void UnloadAsset(AsyncOperationHandleBase handle)
        {
            var addressableHandle = handle as AddressableAsyncOperationHandle<Object>;
            if (addressableHandle == null)
            {
                return;
            }

            Addressables.Release(addressableHandle.Handle);
        }

        /// <summary>
        /// 实例化资源。
        /// </summary>
        /// <param name="asset">要实例化的资源。</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>资源实例</returns>
        public override T Instantiate<T>(object asset) where T : class
        {
            T instance = Instantiate(asset as Object) as T;
            return instance;
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
            return new AddressableAsyncOperationHandle<SceneInstance>(sceneAssetName, Addressables.LoadSceneAsync(sceneAssetName, LoadSceneMode.Additive));
        }

        /// <summary>
        /// 异步卸载场景。
        /// </summary>
        /// <param name="handle">要卸载场景加载句柄。</param>
        /// <returns>异步卸载场景句柄</returns>
        public override AsyncOperationHandleBase UnloadScene(AsyncOperationHandleBase handle)
        {
            var addressableHandle = handle as AddressableAsyncOperationHandle<SceneInstance>;
            if (addressableHandle == null)
            {
                return null;
            }
            
            var unloadHandle = new AddressableAsyncOperationHandle<SceneInstance>(handle.AssetName, Addressables.UnloadSceneAsync(addressableHandle.Handle, false));
            unloadHandle.OnSucceeded += _ => Addressables.Release(unloadHandle.Handle);
            unloadHandle.OnFailed += _ => Addressables.Release(unloadHandle.Handle);
            return unloadHandle;
        }
    }
}
#endif