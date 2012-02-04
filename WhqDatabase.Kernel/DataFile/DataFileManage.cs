using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WhqDatabase.Kernel.DataPage;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase.Kernel.DataContent;

namespace WhqDatabase.Kernel.DataFile
{
    /// <summary>
    /// 数据文件管理
    /// </summary>
    public class DataFileManage
    {
        /// <summary>
        /// 创建文件
        /// </summary>
        public static void Create(DataFileEntity model)
        {
            FileStream fs = new FileStream(model.FilePath, FileMode.CreateNew);
            model.FileStream = fs;
            model.MaxPageID = 2;
            //写入0页
            DataPageManage.Write(new DataPageEntity { DataFile = model, ID = Config.DATABASE_PAGE_ID, Content = model.Database.ToBytes() });
            //写入1页
            DataPageManage.Write(new DataPageEntity { DataFile = model, ID = Config.DATAFILE_PAGE_ID, Content = model.ToBytes() });
            //写入2页
            TablePageEntity modelTP = new TablePageEntity { Tables = new List<DataTable.DataTableEntity>() };
            DataPageEntity modelDP= new DataPageEntity { DataFile = model, ID = Config.DATATABLE_PAGE_ID, Content = modelTP.ToBytes() };
            DataPageManage.Write(modelDP);
            //赋值
            modelTP.DataPage = modelDP;
            model.TablePage = modelTP;
        }
        /// <summary>
        /// 修改
        /// </summary>
        public static void Update(DataFileEntity model)
        {
            DataPageManage.Write(new DataPageEntity { DataFile = model, ID = Config.DATAFILE_PAGE_ID, Content = model.ToBytes() });
        }
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="modelDF"></param>
        public static void Read(DataFileEntity model)
        {
            model.FileStream = new FileStream(model.FilePath, FileMode.Open);
            //读文件信息
            DataPageEntity modelDP = new DataPageEntity { DataFile = model, ID = Config.DATAFILE_PAGE_ID };
            DataPageManage.Read(modelDP);
            model.FromBytes(modelDP.Content);
            //读表信息
            DataPageEntity modelDPT = new DataPageEntity { DataFile = model, ID = Config.DATATABLE_PAGE_ID };
            DataPageManage.Read(modelDPT);
            model.TablePage = new TablePageEntity();
            model.TablePage.DataPage = modelDPT;
            model.TablePage.FromBytes(modelDPT.Content);
            //读取列
            foreach (DataTableEntity modelDT in model.TablePage.Tables)
            {
                DataPageEntity modelDPTemp = new DataPageEntity { DataFile = model, ID = modelDT.ColumnPageID };
                DataPageManage.Read(modelDPTemp);
                modelDT.ColumnPage = new ColumnPageEntity();
                modelDT.ColumnPage.DataPage = modelDPTemp;
                modelDT.ColumnPage.Table = modelDT;
                modelDT.ColumnPage.FromBytes(modelDPTemp.Content);
                //表用户列
                modelDT.UserColumns = new List<DataColumnEntity>();
                modelDT.UserColumns.AddRange(modelDT.ColumnPage.Columns.Where(info => info.Type == EColumnType.User));
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        public static void Delete(DataFileEntity model)
        {
            if (model.FileStream != null) model.FileStream.Close();
            File.Delete(model.FilePath);
        }
        /// <summary>
        /// 获取空分页编号
        /// </summary>
        /// <param name="model"></param>
        public static long GetEmptyPageID(DataFileEntity model)
        {
            //遍历已有页找空页（0-2必定不为空）
            for (int i = 3; i <= model.MaxPageID; i++)
            {
                model.FileStream.Seek(i * Config.PAGE_SIZE, SeekOrigin.Begin);
                byte[] arr = new byte[1];
                model.FileStream.Read(arr, 0, 1);
                if (arr[0] == Convert.ToInt32(EPageType.Empty))
                {
                    return i;
                }
            }
            //没有空页，添加新页
            model.MaxPageID++;
            Update(model);
            return model.MaxPageID;
        }
    }
}
