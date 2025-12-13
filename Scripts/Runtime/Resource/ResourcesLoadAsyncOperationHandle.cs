using GameFramework.Resource;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 使用Resources.LoadAsync加载返回句柄
    /// </summary>
    public class ResourcesLoadAsyncOperationHandle : UnityAsyncOperationHandle<ResourceRequest>
    {
        public override AsyncOperationStatus Status
        {
            get
            {
                if (Operation == null)
                {
                    return AsyncOperationStatus.Failed;
                }

                if (Result != null)
                {
                    return AsyncOperationStatus.Succeeded;
                }

                return AsyncOperationStatus.Failed;
            }
        }

        public override object Result
        {
            get
            {
                if (Operation == null)
                {
                    return null;
                }

                return Operation.asset;
            }
        }

        public ResourcesLoadAsyncOperationHandle(ResourceRequest operation) : base(operation)
        {
        }
    }
}