/*
 * WhqDatabase
 * http://longtianyu1.blog.163.com/
 * 
 * Copyright 2012,Wang Hongqi(王洪岐,longtianyu1@163.com)
 * Dual licensed under the MIT or GPL Version 2 licenses.
 *
 * Author: Wang Hongqi(王洪岐,longtianyu1@163.com)
 * Date: 2012-02-04
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataContent;
using WhqDatabase.Kernel.DataColumn;
using System.Collections;

namespace WhqDatabase.Kernel.DataRow
{
    /// <summary>
    /// 数据行操作
    /// </summary>
    public class DataRowManage
    {
        #region CURD
        /// <summary>
        /// 创建
        /// </summary>
        public static void Create(DataTableEntity modelDT, DataRowEntity modelDR)
        {
            int RowIndex = DataContentManage.GetFirstEmpty(modelDT).GetRowIndex();
            for (int i = 0; i < modelDR.Columns.Length; i++)
            {
                DataContentManage.Create(
                    modelDT.Columns.Where(info => info.Name == modelDR.Columns[i]).First(),
                    new DataContentEntity { Content = modelDR.Contents[i] },
                    RowIndex);
            }
            DataContentManage.Create(modelDT.Columns[0], new DataContentEntity { Content = 1 }, RowIndex);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public static void Update(DataTableEntity modelDT, DataRowEntity modelDR, DataWhereEntity[] where = null)
        {
            int[] arrDC = ReadRowIndexList(modelDT, where).ToArray();
            string[] Columns = modelDT.UserColumns.Select(info => info.Name).ToArray();
            //以列为维度遍历取值
            object[,] arrContent = new object[arrDC.Count(), Columns.Length];
            for (int i = 0; i < modelDR.Columns.Length; i++)
            {
                //列需要修改
                DataColumnEntity modelDC = DataColumnManage.Read(modelDT, new DataColumnEntity { Name = modelDR.Columns[i] });
                for (int j = 0; j < arrDC.Length; j++)
                {
                    DataContentEntity modelDContent = DataContentManage.GetContent(modelDC, arrDC[j]);
                    modelDContent.Content = modelDR.Contents[i];
                    DataContentManage.Update(modelDContent);
                }
            }
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="modelDT">表</param>
        /// <param name="modelDR">显示的列</param>
        /// <param name="where">是否满足条件的函数</param>
        /// <returns></returns>
        public static List<DataRowEntity> Read(DataTableEntity modelDT, string[] column, DataWhereEntity[] where = null, DataOrderEntity order = null, DataLimitEntity limit = null)
        {
            List<DataRowEntity> listDR = new List<DataRowEntity>();
            int[] arrDC = ReadRowIndexList(modelDT, where).ToArray();
            object[,] arrContent;
            //排序
            if (order != null)
            {
                listDR.Clear();
                //以列为维度遍历取值
                arrContent = new object[arrDC.Count(), order.Columns.Length];
                for (int i = 0; i < order.Columns.Length; i++)
                {
                    //列需要修改
                    DataColumnEntity modelDC = DataColumnManage.Read(modelDT, new DataColumnEntity { Name = order.Columns[i] });
                    for (int j = 0; j < arrDC.Length; j++)
                    {
                        arrContent[j, i] = DataContentManage.GetContent(modelDC, arrDC[j]).Content;
                    }
                }
                //以行为维度赋值
                for (int j = 0; j < arrDC.Length; j++)
                {
                    object[] Contents = new object[order.Columns.Length];
                    for (int i = 0; i < order.Columns.Length; i++)
                    {
                        Contents[i] = arrContent[j, i];
                    }
                    listDR.Add(new DataRowEntity { Columns = order.Columns, Contents = Contents, RowIndex = arrDC[j] });
                }
                listDR.Sort(order.Comparison);
                arrDC = listDR.Select(info => info.RowIndex).ToArray();
                listDR.Clear();
            }
            //截取
            if (limit != null)
            {
                IEnumerable<int> enumDC = arrDC;
                //起始截取
                if (limit.Start < enumDC.Count())
                    enumDC = enumDC.Skip(limit.Start);
                //结束截取
                if (limit.Length > 0 && limit.Length < enumDC.Count())
                    enumDC = enumDC.Take(limit.Length);
                arrDC = enumDC.ToArray();
            }
            //以列为维度遍历取值
            arrContent = new object[arrDC.Count(), column.Length];
            for (int i = 0; i < column.Length; i++)
            {
                //列需要修改
                DataColumnEntity modelDC = DataColumnManage.Read(modelDT, new DataColumnEntity { Name = column[i] });
                for (int j = 0; j < arrDC.Length; j++)
                {
                    arrContent[j, i] = DataContentManage.GetContent(modelDC, arrDC[j]).Content;
                }
            }
            //以行为维度赋值
            for (int j = 0; j < arrDC.Length; j++)
            {
                object[] Contents = new object[column.Length];
                for (int i = 0; i < column.Length; i++)
                {
                    Contents[i] = arrContent[j, i];
                }
                listDR.Add(new DataRowEntity { Columns = column, Contents = Contents, RowIndex = arrDC[j] });
            }
            return listDR;
        }
        /// <summary>
        /// 根据条件返回总数
        /// </summary>
        /// <param name="modelDT">表</param>
        /// <param name="where">是否满足条件的函数</param>
        /// <returns></returns>
        public static int ReadCount(DataTableEntity modelDT, DataWhereEntity[] where = null)
        {
            //无条件返回所有
            if (where == null) return DataTableManage.GetRowIndexList(modelDT).Count();
            else
            {
                //晒选行编号
                IEnumerable<int> listDC = new List<int>();
                foreach (DataWhereEntity modelW in where)
                {
                    DataColumnEntity modelDC = modelDT.Columns.Where(info => info.Name == modelW.ColumnName).First();
                    if (listDC.Count() == 0)//第一个条件
                    {
                        listDC = DataContentManage.Read(modelDC, info => modelW.Predicate(info.Content)).Select(info => info.RowIndex);
                    }
                    else//后几个条件在前一个条件基础上晒选
                    {
                        listDC = DataContentManage.Read(modelDC, info => listDC.Contains(info.RowIndex) && modelW.Predicate(info.Content)).Select(info => info.RowIndex);
                    }
                }
                return listDC.Count();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="modelDT">表</param>
        /// <param name="where">条件</param>
        public static void Delete(DataTableEntity modelDT, DataWhereEntity[] where = null)
        {
            IEnumerable<int> listDC = ReadRowIndexList(modelDT, where);
            //删除行
            foreach (int RowIndex in listDC)
            {
                DataContentManage.Delete(DataContentManage.GetContent(modelDT.Columns[0], RowIndex));
            }
        }
        #endregion

        /// <summary>
        /// 根据条件晒选行编号
        /// </summary>
        /// <param name="modelDT">表</param>
        /// <param name="where">是否满足条件的函数</param>
        /// <returns></returns>
        public static IEnumerable<int> ReadRowIndexList(DataTableEntity modelDT, DataWhereEntity[] where)
        {
            //晒选行编号
            IEnumerable<int> listDC = new List<int>();
            //无条件返回所有
            if (where == null) listDC = DataTableManage.GetRowIndexList(modelDT);
            else
            {
                foreach (DataWhereEntity modelW in where)
                {
                    DataColumnEntity modelDC = modelDT.Columns.Where(info => info.Name == modelW.ColumnName).First();
                    if (listDC.Count() == 0)//第一个条件
                    {
                        if (modelW.ColumnName == EColumnType.RowIndex.ToString())
                        {
                            listDC = DataContentManage.Read(modelDC, info => modelW.Predicate(info.RowIndex)).Select(info => info.RowIndex);
                        }
                        else
                        {
                            listDC = DataContentManage.Read(modelDC, info => modelW.Predicate(info.Content)).Select(info => info.RowIndex);
                        }
                    }
                    else//后几个条件在前一个条件基础上晒选
                    {
                        if (modelW.ColumnName == EColumnType.RowIndex.ToString())
                        {
                            listDC = DataContentManage.Read(modelDC, info => listDC.Contains(info.RowIndex) && modelW.Predicate(info.RowIndex)).Select(info => info.RowIndex);
                        }
                        else
                        {
                            listDC = DataContentManage.Read(modelDC, info => listDC.Contains(info.RowIndex) && modelW.Predicate(info.Content)).Select(info => info.RowIndex);
                        }
                    }
                }
            }
            return listDC;
        }
    }
}
