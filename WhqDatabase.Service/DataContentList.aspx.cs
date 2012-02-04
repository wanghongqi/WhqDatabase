using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Service.Common;
using WhqDatabase.Kernel.DataRow;
using WhqDatabase.Service.UserControl;

namespace WhqDatabase.Service
{
    public partial class DataContentList : System.Web.UI.Page
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        protected DataTableEntity Table;
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int PageSize = 20;
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
                    ColumnBind();
                    ContentBind(1);
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
        /// 数据绑定
        /// </summary>
        protected void ContentBind(int PageNum)
        {
            rptData.DataSource = DataRowManage.Read(Table, Table.UserColumns.Select(info => info.Name).ToArray(), null, null,
                new DataLimitEntity { Start = (PageNum - 1) * PageSize, Length = PageSize });
            rptData.DataBind();
            pageBarTop.PageSize = PageSize;
            pageBarTop.RowCount = DataRowManage.ReadCount(Table);
        }
        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pageBarTop_ChangeNum(object sender, PageBarChangeNumArgs e)
        {
            ContentBind(e.PageNum);
        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    DataRowManage.Delete(Table,
                        new DataWhereEntity[] { new DataWhereEntity { ColumnName = "RowIndex", Predicate = info => Convert.ToInt32(info) ==  Convert.ToInt32(e.CommandArgument) }}
                        );
                    JScript.ShowMessage(this, "内容删除成功");
                    ContentBind(pageBarTop.PageNum);
                    break;
            }
        }
    }
}