//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 数据表行基类。
    /// </summary>
    public abstract class DataRowBase : IDataRow
    {
        /// <summary>
        /// 获取数据表行的编号。
        /// </summary>
        public abstract int Id
        {
            get;
        }
    }
}
