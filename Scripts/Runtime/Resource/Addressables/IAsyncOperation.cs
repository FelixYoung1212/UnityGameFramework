namespace UnityGameFramework.Runtime
{
    public interface IAsyncOperation
    {
        /// <summary>
        /// 异步加载完成进度
        /// </summary>
        float Progress { get; }

        /// <summary>
        /// 异步加载是否完成
        /// </summary>
        bool IsDone { get; }
    }
}