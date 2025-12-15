#if ADDRESSABLES_SUPPORT
using GameFramework.Resource;
using UnityEngine.ResourceManagement.AsyncOperations;
using AsyncOperationStatus = GameFramework.Resource.AsyncOperationStatus;

namespace UnityGameFramework.Runtime
{
    public class AddressableAsyncOperationHandle<T> : AsyncOperationHandleBase
    {
        public AsyncOperationHandle<T> Handle { get; }
        public override float Progress => Handle.PercentComplete;
        public override AsyncOperationStatus Status => (AsyncOperationStatus)Handle.Status;
        public override object Result => Handle.Result;
        public override string ErrorMessage => Handle.OperationException?.Message ?? "";

        public AddressableAsyncOperationHandle(string assetName, AsyncOperationHandle<T> handle) : base(assetName)
        {
            Handle = handle;
        }
    }
}
#endif