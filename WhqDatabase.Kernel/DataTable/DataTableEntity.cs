using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhqDatabase.Kernel.DataPage;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase.Kernel.Database;
using WhqDatabase.Kernel.DataFile;

namespace WhqDatabase.Kernel.DataTable
{
    /// <summary>
    /// 数据表实体
    /// </summary>
    public class DataTableEntity
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
        /// 列信息页编号开始位置
        /// </summary>
        public const int COLUMNPAGEID_START = 8;
        /// <summary>
        /// 名字开始位置
        /// </summary>
        public const int NAME_START = 0x100;
        /// <summary>
        /// 名字长度
        /// </summary>
        public const int NAME_LENGTH = 0x100;
        /// <summary>
        /// 所在表分页
        /// </summary>
        public TablePageEntity TablePage { get; set; }
        /// <summary>
        /// 所在数据库
        /// </summary>
        public DatabaseEntity Database { get { return TablePage.DataPage.Database; } }
        /// <summary>
        /// 所在文件
        /// </summary>
        public DataFileEntity DataFile { get { return TablePage.DataPage.DataFile; } }
        /// <summary>
        /// 列分页
        /// </summary>
        public ColumnPageEntity ColumnPage { get; set; }
        /// <summary>
        /// 包含的列
        /// </summary>
        public List<DataColumnEntity> Columns { get { return ColumnPage.Columns; } }
        /// <summary>
        /// 包含的用户列
        /// </summary>
        public List<DataColumnEntity> UserColumns { get;set;}
        /// <summary>
        /// 表编号
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 列信息的页编号
        /// </summary>
        public long ColumnPageID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 转成比特数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            byte[] array = new byte[DATA_SIZE];
            BitConverter.GetBytes(ID).CopyTo(array, ID_START);
            BitConverter.GetBytes(ColumnPageID).CopyTo(array, COLUMNPAGEID_START);
            Encoding.UTF8.GetBytes(Name).CopyTo(array, NAME_START);
            return array;
        }
        /// <summary>
        /// 从byte[]中读取
        /// </summary>
        /// <param name="arr"></param>
        public void FromBytes(byte[] arr)
        {
            ID = BitConverter.ToInt64(arr, ID_START);
            ColumnPageID = BitConverter.ToInt64(arr, COLUMNPAGEID_START);
            Name = Encoding.UTF8.GetString(arr, NAME_START, NAME_LENGTH).TrimEnd('\0');
        }
    }
}
