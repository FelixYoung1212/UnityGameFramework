#if ADDRESSABLES_SUPPORT
using GameFramework.Resource;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using AsyncOperationStatus = GameFramework.Resource.AsyncOperationStatus;

namespace UnityGameFramework.Runtime
{
    public class AddressableAsyncOperationHandle<T> : AsyncOperationHandleBase
    {
        public AsyncOperationHandle<T> Handle { get; }
        public override bool IsValid => Handle.IsValid();
        public override float Progress => Handle.PercentComplete;
        public override AsyncOperationStatus Status => (AsyncOperationStatus)Handle.Status;
        public override object Result => Handle.Result;
        public override string ErrorMessage => Handle.OperationException?.Message ?? "";

        public AddressableAsyncOperationHandle(string assetName, AsyncOperationHandle<T> handle) : base(assetName)
        {
            Handle = handle;
        }

        public override void Release()
        {
            if (!Handle.IsValid())
            {
                return;
            }

            Addressables.Release(Handle);
        }
    }
}
#endif