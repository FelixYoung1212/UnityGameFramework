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
        protected T Operation { get; private set; }

        public override float Progress
        {
            get
            {
                if (Operation == null)
                {
                    return 0f;
                }

                return Operation.progress;
            }
        }

        public override AsyncOperationStatus Status => AsyncOperationStatus.None;

        public override object Result => null;

        public override string ErrorMessage => "";

        public UnityAsyncOperationHandle(string assetName, T operation) : base(assetName)
        {
            Operation = operation;
        }
    }
}