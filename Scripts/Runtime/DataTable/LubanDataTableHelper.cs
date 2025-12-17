using System;
using GameFramework;
using GameFramework.DataTable;
using Luban;
using SimpleJSON;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    public class LubanDataTableHelper : DataTableHelperBase
    {
        private static readonly string BytesAssetExtension = ".bytes";

        private ResourceComponent m_ResourceComponent = null;

        /// <summary>
        /// 读取数据表。
        /// </summary>
        /// <param name="dataTable">数据表。</param>
        /// <param name="dataTableAssetName">数据表资源名称。</param>
        /// <param name="dataTableAsset">数据表资源。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否读取数据表成功。</returns>
        public override bool ReadData(DataTableBase dataTable, string dataTableAssetName, object dataTableAsset, object userData)
        {
            TextAsset dataTableTextAsset = dataTableAsset as TextAsset;
            if (dataTableTextAsset != null)
            {
                if (dataTableAssetName.EndsWith(BytesAssetExtension, StringComparison.Ordinal))
                {
                    return dataTable.ParseData(dataTableTextAsset.bytes, userData);
                }
                else
                {
                    return dataTable.ParseData(dataTableTextAsset.text, userData);
                }
            }

            Log.Warning("Data table asset '{0}' is invalid.", dataTableAssetName);
            return false;
        }

        /// <summary>
        /// 读取数据表。
        /// </summary>
        /// <param name="dataTable">数据表。</param>
        /// <param name="dataTableAssetName">数据表资源名称。</param>
        /// <param name="dataTableBytes">数据表二进制流。</param>
        /// <param name="startIndex">数据表二进制流的起始位置。</param>
        /// <param name="length">数据表二进制流的长度。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否读取数据表成功。</returns>
        public override bool ReadData(DataTableBase dataTable, string dataTableAssetName, byte[] dataTableBytes, int startIndex, int length, object userData)
        {
            if (dataTableAssetName.EndsWith(BytesAssetExtension, StringComparison.Ordinal))
            {
                return dataTable.ParseData(dataTableBytes, startIndex, length, userData);
            }
            else
            {
                return dataTable.ParseData(Utility.Converter.GetString(dataTableBytes, startIndex, length), userData);
            }
        }

        /// <summary>
        /// 解析数据表。
        /// </summary>
        /// <param name="dataTable">数据表。</param>
        /// <param name="dataTableString">要解析的数据表字符串。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否解析数据表成功。</returns>
        public override bool ParseData(DataTableBase dataTable, string dataTableString, object userData)
        {
            try
            {
                JSONNode dataTableNode = JSONNode.Parse(dataTableString);
                foreach (JSONNode dataRowNode in dataTableNode.Children)
                {
                    if (!dataRowNode.IsObject)
                    {
                        throw new SerializationException();
                    }

                    var dataRow = Activator.CreateInstance(dataTable.Type, (object)dataRowNode);
                    dataTable.AddDataRow(dataRow as IDataRow);
                }

                return true;
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse data table string with exception '{0}'.", exception);
                return false;
            }
        }

        /// <summary>
        /// 解析数据表。
        /// </summary>
        /// <param name="dataTable">数据表。</param>
        /// <param name="dataTableBytes">要解析的数据表二进制流。</param>
        /// <param name="startIndex">数据表二进制流的起始位置。</param>
        /// <param name="length">数据表二进制流的长度。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否解析数据表成功。</returns>
        public override bool ParseData(DataTableBase dataTable, byte[] dataTableBytes, int startIndex, int length, object userData)
        {
            try
            {
                ByteBuf dataTableBuf = new ByteBuf(dataTableBytes);
                for (int n = dataTableBuf.ReadSize(); n > 0; --n)
                {
                    var dataRow = Activator.CreateInstance(dataTable.Type, dataTableBuf);
                    dataTable.AddDataRow(dataRow as IDataRow);
                }

                return true;
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse dictionary bytes with exception '{0}'.", exception);
                return false;
            }
        }

        /// <summary>
        /// 释放数据表资源。
        /// </summary>
        /// <param name="dataTable">数据表。</param>
        /// <param name="dataTableAsset">要释放的数据表资源。</param>
        public override void ReleaseDataAsset(DataTableBase dataTable, object dataTableAsset)
        {
            m_ResourceComponent.UnloadAsset(dataTableAsset);
        }

        private void Start()
        {
            m_ResourceComponent = GameEntry.GetComponent<ResourceComponent>();
            if (m_ResourceComponent == null)
            {
                Log.Fatal("Resource component is invalid.");
                return;
            }
        }
    }
}