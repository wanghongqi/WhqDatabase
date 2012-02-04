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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Service.Common;
using WhqDatabase.Kernel.DataColumn;

namespace WhqDatabase.Service
{
    public partial class DataTableInfo : System.Web.UI.Page
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        protected DataTableEntity Table;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["DatabaseID"]) && !string.IsNullOrEmpty(Request.QueryString["DataTableID"]))
            {
                Table = Manage.ListDB.First(info => info.ID == Convert.ToInt32(Request.QueryString["DatabaseID"])).Tables.First(info => info.ID == Convert.ToInt32(Request.QueryString["DataTableID"]));
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
                if (Table != null)
                {
                    txtName.Text = Table.Name;
                    ColumnBind();
                }
            }
        }
        /// <summary>
        /// 绑定列
        /// </summary>
        protected void ColumnBind()
        {
            rptColumn.DataSource = Table.UserColumns;
            rptColumn.DataBind();
        }
        /// <summary>
        /// 表删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            JScript.ResponseScript(this, "alert('表删除成功！');location='DatabaseInfo.aspx?DatabaseID=" + Table.Database.ID + "'");
            DataTableManage.Delete(Table);
        }
        /// <summary>
        /// 列表事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptColumn_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    DataColumnManage.Delete(Table.Columns.First(info => info.ID == Convert.ToInt32(e.CommandArgument)));
                    JScript.ShowMessage(this, "列删除成功");
                    ColumnBind();
                    break;
            }
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //建表
            string[] arrName = Request.Form.GetValues("txtColumnName");
            string[] arrType = Request.Form.GetValues("ddlType");
            string[] arrLength = Request.Form.GetValues("txtLength");
            for (int i = 0; i < arrName.Length; i++)
            {
                if (!string.IsNullOrEmpty(arrName[i]))
                {
                    DataColumnEntity modelDC = new DataColumnEntity
                    {
                        Name = arrName[i],
                        Type = EColumnType.User,
                        DataType = WhqDatabase.Kernel.DataContent.ContentType.GetType(Convert.ToInt32(arrType[i])),
                        DataLength = Convert.ToInt32(arrLength[i])
                    };
                    //重复判断
                    if (Table.UserColumns.Count(info => arrName[i] == info.Name) > 0)
                    {
                        JScript.ShowMessage(this, "存在重名列");
                        ColumnBind();
                        return;
                    }
                    DataColumnManage.Create(Table, modelDC);
                }
            }
            JScript.ShowMessage(this, "修改表成功");
            ColumnBind();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string strName = txtName.Text;
            if (string.IsNullOrEmpty(strName))
            {
                JScript.ShowMessage(this, "表名不能为空");
                return;
            }
            if (Table.Database.Tables.Count(info => info.Name == strName && info.ID != Table.ID) > 0)
            {
                JScript.ShowMessage(this, "表名已存在");
                return;
            }
            Table.Name = strName;
            DataTableManage.Update(Table);
            JScript.ShowMessage(this, "表名修改成功");
        }
    }
}