using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataPage;
using WhqDatabase.Kernel.DataContent;

namespace WhqDatabase.Kernel.DataColumn
{
    /// <summary>
    /// 列实体
    /// </summary>
    public class DataColumnEntity
    {
        /// <summary>
        /// 数据长度
        /// </summary>
        public const int DATA_SIZE = 0x200;
        /// <summary>
        /// 编号开始位置
        /// </summary>
        public const int ID_START = 0;
        /// <summary>
        /// 类型开始位置
        /// </summary>
        public const int TYPE_START = 0x8;
        /// <summary>
        /// 数据类型开始位置
        /// </summary>
        public const int DATATYPE_START = 0x10;
        /// <summary>
        /// 长度开始位置
        /// </summary>
        public const int LENGTH_START = 0x18;
        /// <summary>
        /// 数据分页编号开始位置
        /// </summary>
        public const int DATAPAGEID_START = 0x20;
        /// <summary>
        /// 名字开始位置
        /// </summary>
        public const int NAME_START = 0x100;
        /// <summary>
        /// 名字长度
        /// </summary>
        public const int NAME_LENGTH = 0x100;
        /// <summary>
        /// 所在表
        /// </summary>
        public DataTableEntity Table { get; set; }
        /// <summary>
        /// 所在列分页
        /// </summary>
        public ColumnPageEntity ColumnPage { get; set; }
        /// <summary>
        /// 每页最大数量
        /// </summary>
        public int PageMaxCount { get { return (Config.PAGE_SIZE - ContentPageEntity.CONTENT_START) / DataLength; } }
        /// <summary>
        /// 表编号
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public EColumnType Type { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public EContentType DataType { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }
        /// <summary>
        /// 初始数据分页编号
        /// </summary>
        public long ContentPageID { get; set; }
        /// <summary>
        /// 转成比特数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            byte[] array = new byte[DATA_SIZE];
            BitConverter.GetBytes(ID).CopyTo(array, ID_START);
            BitConverter.GetBytes(Convert.ToInt32(Type)).CopyTo(array, TYPE_START);
            BitConverter.GetBytes(Convert.ToInt32(DataType)).CopyTo(array, DATATYPE_START);
            BitConverter.GetBytes(DataLength).CopyTo(array, LENGTH_START);
            BitConverter.GetBytes(ContentPageID).CopyTo(array, DATAPAGEID_START);
            Encoding.UTF8.GetBytes(Name).CopyTo(array, NAME_START);
            return array;
        }
        /// <summary>
        /// 从byte[]中读取
        /// </summary>
        /// <param name="array"></param>
        public void FromBytes(byte[] array)
        {
            ID = BitConverter.ToInt64(array, ID_START);
            Type = (EColumnType)Enum.Parse(typeof(EColumnType), Enum.GetName(typeof(EColumnType), BitConverter.ToInt32(array, TYPE_START)));
            DataType =ContentType.GetType(BitConverter.ToInt32(array, DATATYPE_START));
            DataLength = BitConverter.ToInt32(array, LENGTH_START);
            ContentPageID = BitConverter.ToInt64(array, DATAPAGEID_START);
            Name = Encoding.UTF8.GetString(array, NAME_START, NAME_LENGTH).TrimEnd('\0');
        }
    }
}
