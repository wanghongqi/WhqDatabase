/*!
 * WhqDatabase
 * http://longtianyu1.blog.163.com/
 * 
 * Copyright 2012,Wang Hongqi(王洪岐)
 * Dual licensed under the MIT or GPL Version 2 licenses.
 *
 * Author: Wang Hongqi(王洪岐)
 * Date: 2012-02-04
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Service.Common;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase.Kernel.DataRow;
using System.Text;

namespace WhqDatabase.Service
{
    public partial class DataContentEdit : System.Web.UI.Page
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        protected DataTableEntity Table;
        /// <summary>
        /// 行号
        /// </summary>
        protected int RowIndex=-1;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            //Table赋值
            if (!string.IsNullOrEmpty(Request.QueryString["DatabaseID"]) && !string.IsNullOrEmpty(Request.QueryString["DataTableID"]))
            {
                Table = Manage.ListDB.First(info => info.ID == Convert.ToInt32(Request.QueryString["DatabaseID"])).Tables.First(info => info.ID == Convert.ToInt32(Request.QueryString["DataTableID"]));
            }
            //行号
            if (!string.IsNullOrEmpty(Request.QueryString["RowIndex"]))
            {
                RowIndex = Convert.ToInt32(Request.QueryString["RowIndex"]);
            }
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptColumn.DataSource = Table.UserColumns;
                rptColumn.DataBind();
                if (RowIndex !=-1)
                {
                    StringBuilder script = new StringBuilder();
                    DataRowEntity modelDR= DataRowManage.Read(Table, Table.UserColumns.Select(info => info.Name).ToArray(),
                    new DataWhereEntity[] { new DataWhereEntity { ColumnName = "RowIndex", Predicate = info => Convert.ToInt32(info) == RowIndex } }
                    ).First();
                    for (int i = 0; i < modelDR.Columns.Length; i++)
                    {
                        script.Append("$('[name=txt" + modelDR.Columns[i] + "]').val('" + modelDR.Contents[i] + "');\r\n");
                    }
                    JScript.ResponseScript(this, @"$(function(){"+script+"})");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (RowIndex == -1)
            {
                DataRowEntity modelDR = new DataRowEntity { Columns = Table.UserColumns.Select(info => info.Name).ToArray(), Contents = new object[Table.UserColumns.Count] };
                for (int i = 0; i < Table.UserColumns.Count; i++)
                {
                    modelDR.Contents[i] = Request.Form["txt" + Table.UserColumns[i].Name];
                }
                DataRowManage.Create(Table, modelDR);
                JScript.ResponseScript(this, "alert('添加成功！');location=location.href");
            }
            else
            {
                DataRowEntity modelDR = new DataRowEntity { Columns = Table.UserColumns.Select(info => info.Name).ToArray(), Contents = new object[Table.UserColumns.Count] };
                for (int i = 0; i < Table.UserColumns.Count; i++)
                {
                    modelDR.Contents[i] = Request.Form["txt" + Table.UserColumns[i].Name];
                }
                DataRowManage.Update(Table, modelDR,
                    new DataWhereEntity[] { new DataWhereEntity { ColumnName = "RowIndex", Predicate = info => Convert.ToInt32(info) == RowIndex } }
                    );
                JScript.ResponseScript(this, "alert('更新成功！');location=location.href");
            }
        }
    }
}