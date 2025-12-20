using GameFramework.Resource;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// Unity自带异步加载资源句柄
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnityAsyncOperationHandle<T> : AsyncOperationHandleBase where T : AsyncOperation
    {
        protected T Handle { get; set; }

        public override bool IsValid => Handle != null;

        public override float Progress
        {
            get
            {
                if (Handle == null)
                {
                    return 0f;
                }

                return Handle.progress;
            }
        }

        public override AsyncOperationStatus Status => AsyncOperationStatus.None;

        public override object Result => null;

        public override string ErrorMessage => "";

        public UnityAsyncOperationHandle(string assetName, T handle) : base(assetName)
        {
            Handle = handle;
        }
    }
}