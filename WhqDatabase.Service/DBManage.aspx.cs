using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhqDatabase.Service.Common;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataRow;
using WhqDatabase.Kernel.Database;
using WhqDatabase.Kernel.DataFile;
using System.IO;

namespace WhqDatabase.Service
{
    public partial class DBManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Refresh();
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        protected void Refresh()
        {
            litStatus.Text = (Manage.IsRun) ? "运行" : "停止";
            btnStart.Enabled = !Manage.IsRun;
            btnStop.Enabled = Manage.IsRun;
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStart_Click(object sender, EventArgs e)
        {
            Manage.Start();
            Refresh();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStop_Click(object sender, EventArgs e)
        {
            Manage.Stop();
            Refresh();
        }
        /// <summary>
        /// 新建库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string strName = txtName.Text;
            string strPath = txtPath.Text;
            if (string.IsNullOrEmpty(strName))
            {
                JScript.ShowMessage(this, "请填写名称");
                return;
            }
            if (string.IsNullOrEmpty(strPath))
            {
                strPath = Path.GetDirectoryName(DBMaster.DB.MasterFile.FilePath) + "\\" + strName + ".whqdata";
            }
            //判断是否重复
            DataTableEntity modelDT = new DataTableEntity { Name = "Database" };
            modelDT = DataTableManage.Read(DBMaster.DB, modelDT);
            if (Manage.ListDB.Where(info => info.Name == strName).Count() > 0)
            {
                JScript.ShowMessage(this, "数据库名已存在");
                return;
            }
            //创建数据库
            DatabaseEntity modelD = new DatabaseEntity
            {
                Name = strName,
                MasterFile = new DataFileEntity { FilePath = strPath }
            };
            DatabaseManage.Create(modelD);
            //添加到主库中
            Manage.DBAdd(modelD);
            JScript.ResponseScript(this, "alert('数据库创建成功！');window.top.menu.location=window.top.menu.location.href;");
        }
        /// <summary>
        /// 附加库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strPath = txtPath.Text;
            if (string.IsNullOrEmpty(strPath))
            {
                JScript.ShowMessage(this, "请填写路径");
                return;
            }
            //打开数据库
            DatabaseEntity modelD = new DatabaseEntity
            {
                MasterFile = new DataFileEntity { FilePath = strPath }
            };
            DatabaseManage.Read(modelD);
            if (Manage.ListDB.Where(info => info.Name == modelD.Name).Count() > 0)
            {
                JScript.ShowMessage(this, "数据库名已存在");
                return;
            }
            //添加到主库中
            Manage.DBAdd(modelD);
            JScript.ResponseScript(this, "alert('数据库附加成功！');window.top.menu.location=window.top.menu.location.href;");
        }
    }
}