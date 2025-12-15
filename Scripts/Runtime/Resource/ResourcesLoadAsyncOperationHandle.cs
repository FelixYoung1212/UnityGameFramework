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
                if (Handle == null)
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
                if (Handle == null)
                {
                    return null;
                }

                return Handle.asset;
            }
        }

        public ResourcesLoadAsyncOperationHandle(string assetName, ResourceRequest handle) : base(assetName, handle)
        {
        }
    }
}