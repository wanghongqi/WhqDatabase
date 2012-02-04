using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhqDatabase.Kernel.Database;
using WhqDatabase.Service.Common;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataRow;

namespace WhqDatabase.Service
{
    public partial class DatabaseInfo : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                if (DB != null)
                {
                    txtName.Text = DB.Name;
                    TableBind();
                }
            }
        }
        /// <summary>
        /// 表绑定
        /// </summary>
        protected void TableBind()
        {
            rptTable.DataSource = DB.Tables;
            rptTable.DataBind();
        }
        /// <summary>
        /// 库删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Manage.DBRemove(DB);
            DatabaseManage.Delete(DB);
            JScript.ResponseScript(this, "alert('库删除成功！');window.top.menu.location=window.top.menu.location.href;location='DBManage.aspx';");
        }
        /// <summary>
        /// 分离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Manage.DBRemove(DB);
            JScript.ResponseScript(this, "alert('库移除成功！');window.top.menu.location=window.top.menu.location.href;location='DBManage.aspx';");
        }
        /// <summary>
        /// 列表事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptTable_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    DataTableManage.Delete(DB.Tables.First(info => info.ID == Convert.ToInt32(e.CommandArgument)));
                    JScript.ShowMessage(this, "表删除成功");
                    TableBind();
                    break;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string strName=txtName.Text;
            if (string.IsNullOrEmpty(strName))
            {
                JScript.ShowMessage(this, "数据库名不能为空");
                return;
            }
            if (Manage.ListDB.Count(info => info.Name == strName && info.ID != DB.ID) > 0)
            {
                JScript.ShowMessage(this, "数据库名已存在");
                return;
            }
            DB.Name = strName;
            DatabaseManage.Update(DB);
            JScript.ResponseScript(this, "alert('数据库名修改成功！');window.top.menu.location=window.top.menu.location.href;");
        }
    }
}