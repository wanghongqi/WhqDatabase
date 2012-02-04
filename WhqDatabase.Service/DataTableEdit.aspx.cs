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
using WhqDatabase.Kernel.Database;
using WhqDatabase.Service.Common;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase.Kernel.DataContent;

namespace WhqDatabase.Service
{
    public partial class DataTableEdit : System.Web.UI.Page
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        protected DatabaseEntity DB;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["DatabaseID"]))
            {
                DB = Manage.ListDB.First(info => info.ID == Convert.ToInt32(Request.QueryString["DatabaseID"]));
            }
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //建表
            DataTableEntity modelDT = new DataTableEntity { Name = txtName.Text };
            DataTableManage.Create(DB, modelDT);
            string[] arrName= Request.Form.GetValues("txtColumnName");
            string[] arrType=Request.Form.GetValues("ddlType");
            string[] arrLength=Request.Form.GetValues("txtLength");
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
                    if (modelDT.UserColumns.Count(info => arrName[i] == info.Name) > 0)
                    {
                        JScript.ShowMessage(this, "存在重名列");
                        DataTableManage.Delete(modelDT);
                        return;
                    }
                    DataColumnManage.Create(modelDT, modelDC);
                }
            }
            JScript.ResponseScript(this, "alert('建表成功！');location='DatabaseInfo.aspx?DatabaseID=" + DB.ID + "'");
        }
    }
}