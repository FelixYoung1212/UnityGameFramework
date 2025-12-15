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
        public override AsyncOperationHandleBase LoadAsset(string assetName)
        {
            return new AddressableAsyncOperationHandle<Object>(assetName, Addressables.LoadAssetAsync<Object>(assetName));
        }

        public override void UnloadAsset(AsyncOperationHandleBase handle)
        {
            var addressableHandle = handle as AddressableAsyncOperationHandle<Object>;
            if (addressableHandle == null)
            {
                return;
            }

            Addressables.Release(addressableHandle.Handle);
        }

        public override object Instantiate(AsyncOperationHandleBase handle)
        {
            return Instantiate(handle.Result as Object);
        }

        public override void ReleaseInstance(object instance, object asset)
        {
            Destroy(instance as Object);
        }

        public override AsyncOperationHandleBase LoadScene(string sceneAssetName)
        {
            return new AddressableAsyncOperationHandle<SceneInstance>(sceneAssetName, Addressables.LoadSceneAsync(sceneAssetName, LoadSceneMode.Additive));
        }

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