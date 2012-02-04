using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase.Kernel.DataPage;
using WhqDatabase.Kernel.DataFile;
using System.IO;
using WhqDatabase.Kernel.DataTable;

namespace WhqDatabase.Kernel.DataContent
{
    /// <summary>
    /// 数据内容管理
    /// </summary>
    public class DataContentManage
    {
        #region CURD
        /// <summary>
        /// 创建
        /// </summary>
        public static void Create(DataColumnEntity modelDT, DataContentEntity modelDC, int RowIndex)
        {
            //赋值
            modelDC.ContentPage = GetPage(modelDT, RowIndex);
            modelDC.ContentIndex = RowIndex % ((Config.PAGE_SIZE - ContentPageEntity.CONTENT_START) / modelDT.DataLength);
            modelDC.Column = modelDT;
            //写入列
            modelDC.ContentPage.Contents.Add(modelDC);
            //写入文件
            modelDT.Table.DataFile.FileStream.Seek(modelDC.ContentPage.PageID * Config.PAGE_SIZE+ ContentPageEntity.CONTENT_START + modelDC.ContentIndex * modelDT.DataLength, SeekOrigin.Begin);
            modelDT.Table.DataFile.FileStream.Write(modelDC.ToBytes(), 0, modelDT.DataLength);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public static void Update(DataContentEntity modelDC)
        {
            //写入文件
            modelDC.Table.DataFile.FileStream.Seek(modelDC.ContentPage.PageID * Config.PAGE_SIZE + ContentPageEntity.CONTENT_START + modelDC.ContentIndex * modelDC.Column.DataLength, SeekOrigin.Begin);
            modelDC.Table.DataFile.FileStream.Write(modelDC.ToBytes(), 0, modelDC.Column.DataLength);
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="modelDT">列</param>
        /// <param name="predicate">是否满足条件的函数</param>
        /// <returns></returns>
        public static List<DataContentEntity> Read(DataColumnEntity modelDC, Func<DataContentEntity, bool> predicate)
        {
            List<DataContentEntity> listDC = new List<DataContentEntity>();
            int PageIndex = 0;
            ContentPageEntity modelCP = ReadContentPage(new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = modelDC.ContentPageID }, new ContentPageEntity { Column = modelDC, PageIndex = PageIndex });
            //遍历找
            listDC.AddRange(modelCP.Contents.Where(predicate));
            //去下一页找
            while (modelCP.NextPageID > 0)
            {
                PageIndex++;
                modelCP = ReadContentPage(new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = modelCP.NextPageID }, new ContentPageEntity { Column = modelDC, PageIndex = PageIndex });
                //遍历找
                listDC.AddRange(modelCP.Contents.Where(predicate));
            }
            return listDC;
        }
        /// <summary>
        /// 删除
        /// </summary>
        public static void Delete(DataContentEntity modelDC)
        {
            modelDC.ContentPage.Contents.Remove(modelDC);
            //写入文件
            modelDC.Table.DataFile.FileStream.Seek(modelDC.ContentPage.PageID * Config.PAGE_SIZE + ContentPageEntity.CONTENT_START + modelDC.ContentIndex * modelDC.Column.DataLength, SeekOrigin.Begin);
            modelDC.Table.DataFile.FileStream.Write(new byte[modelDC.Column.DataLength], 0, modelDC.Column.DataLength);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取列中第一个空位
        /// </summary>
        /// <param name="modelDC"></param>
        /// <returns></returns>
        public static ContentPoint GetFirstEmpty(DataTableEntity modelDT)
        {
            ContentPoint cpReturn = new ContentPoint { ContentIndex = -1 };
            DataColumnEntity modelDC = modelDT.Columns[0];
            ContentPageEntity modelCP = ReadContentPage(new DataPageEntity { DataFile = modelDT.DataFile, ID = modelDC.ContentPageID }, new ContentPageEntity { Column = modelDC, PageIndex = cpReturn.PageIndex });
            //遍历找空位
            for (int i = 0; i < modelDC.PageMaxCount; i++)
            {
                if (modelCP.Contents.Where(info => info.ContentIndex == i).Count() == 0)
                {
                    cpReturn.ContentPageID = modelCP.PageID;
                    cpReturn.PageIndex = 0;
                    cpReturn.ContentIndex = i;
                    break;
                }
            }
            //没有空位去下一页找
            while (cpReturn.ContentIndex == -1 && modelCP.NextPageID > 0)
            {
                cpReturn.PageIndex++;
                modelCP = ReadContentPage(new DataPageEntity { DataFile = modelDT.DataFile, ID = modelCP.NextPageID }, new ContentPageEntity { Column = modelDC, PageIndex = cpReturn.PageIndex });
                //遍历找空位
                for (int i = 0; i < modelDC.PageMaxCount; i++)
                {
                    if (modelCP.Contents.Where(info => info.ContentIndex == i).Count() == 0)
                    {
                        cpReturn.ContentPageID = modelCP.PageID;
                        cpReturn.ContentIndex = i;
                        break;
                    }
                }
            }
            //没有空位创建新页
            if (cpReturn.ContentIndex == -1)
            {
                cpReturn.PageIndex++;
                cpReturn.ContentIndex = 0;
                modelCP = NewContentPage(modelDC, modelCP.PageID, cpReturn.PageIndex);
                cpReturn.ContentPageID = modelCP.PageID;
            }
            return cpReturn;
        }
        /// <summary>
        /// 根据行号获取某数据分页
        /// </summary>
        /// <param name="modelDC"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ContentPageEntity GetPage(DataColumnEntity modelDC, int RowIndex)
        {
            long ContentPageID = modelDC.ContentPageID;
            int PageIndex = 0;
            ContentPageEntity modelCP = new ContentPageEntity();
            if (RowIndex / modelDC.PageMaxCount == 0)//第一页
            {
                modelCP = ReadContentPage(new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = ContentPageID },new ContentPageEntity { Column = modelDC, PageIndex = PageIndex });
            }
            else//不是第一页
            {
                //遍历寻找前一页编号
                for (int i = 0; i <= RowIndex / modelDC.PageMaxCount - 2; i++)
                {
                    ContentPageID = ReadNextPageID(new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = ContentPageID });
                    PageIndex++;
                }
                long NextPageID = ReadNextPageID(new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = ContentPageID });
                if (NextPageID > 0)//有当前页
                {
                    ContentPageID = NextPageID;
                    modelCP = ReadContentPage(new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = ContentPageID }, new ContentPageEntity { Column = modelDC, PageIndex = PageIndex });
                }
                else//页不存在创建新页
                {
                    modelCP = NewContentPage(modelDC, ContentPageID, PageIndex);
                }
            }
            return modelCP;
        }
        /// <summary>
        /// 根据行号获取某数据内容
        /// </summary>
        /// <param name="modelDC"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DataContentEntity GetContent(DataColumnEntity modelDC, int RowIndex)
        {
            ContentPageEntity modelCP = GetPage(modelDC, RowIndex);
            return modelCP.Contents.Where(info => info.ContentIndex == RowIndex % modelDC.PageMaxCount).First();
        }
        /// <summary>
        /// 读取内容页
        /// </summary>
        /// <param name="modelDP"></param>
        /// <returns></returns>
        public static ContentPageEntity ReadContentPage(DataPageEntity modelDP,ContentPageEntity modelCP)
        {
            DataPageManage.Read(modelDP);
            modelCP.DataPage = modelDP;
            modelCP.FromBytes(modelDP.Content);
            modelDP.Content = null;
            return modelCP;
        }
        /// <summary>
        /// 新建内容页
        /// </summary>
        /// <param name="modelDC">所在列</param>
        /// <param name="PageID">前一页页编号</param>
        /// <param name="PageIndex">页号</param>
        /// <returns></returns>
        public static ContentPageEntity NewContentPage(DataColumnEntity modelDC, long PageID, int PageIndex)
        {
            long NextPageID = DataFileManage.GetEmptyPageID(modelDC.Table.DataFile);
            //修改前一页
            WriteNextPageID(new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = PageID }, NextPageID);
            //创建数据分页
            byte[] array = new byte[Config.PAGE_SIZE];
            array[0] = Convert.ToByte(EPageType.DataContent);
            DataPageEntity modelDP = new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = NextPageID, Content = array };
            DataPageManage.Write(modelDP);
            modelDP.Content = null;
            //创建实体
            ContentPageEntity modelCP = new ContentPageEntity { Column = modelDC, DataPage=modelDP, PageIndex = PageIndex, Contents = new List<DataContentEntity>() };
            return modelCP;
        }
        /// <summary>
        /// 读取下一页编号
        /// </summary>
        /// <param name="modelDP"></param>
        /// <returns></returns>
        public static long ReadNextPageID(DataPageEntity modelDP)
        {
            DataPageManage.Read(modelDP, ContentPageEntity.NEXTPAGEID_START, sizeof(Int64));
            return BitConverter.ToInt64(modelDP.Content, 0);
        }
        /// <summary>
        /// 写下一页编号
        /// </summary>
        /// <param name="modelDP"></param>
        /// <returns></returns>
        public static void WriteNextPageID(DataPageEntity modelDP, long NextPageID)
        {
            modelDP.Content = BitConverter.GetBytes(NextPageID);
            DataPageManage.Write(modelDP, ContentPageEntity.NEXTPAGEID_START, sizeof(Int64));
        }
        #endregion
    }
}
