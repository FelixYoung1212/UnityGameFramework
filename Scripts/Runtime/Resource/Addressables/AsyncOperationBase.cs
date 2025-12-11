namespace UnityGameFramework.Runtime
{
    public abstract class AsyncOperationBase<T> : IAsyncOperation
    {
        private T m_InternalOp;

        /// <summary>
        /// 异步加载完成进度
        /// </summary>
        public virtual float Progress => 0;

        /// <summary>
        /// 异步加载是否完成
        /// </summary>
        public abstract bool IsDone { get; }
    }
}