using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Service.Common;
using WhqDatabase.Kernel.DataRow;

namespace WhqDatabase.Service
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptDatabase.DataSource = Manage.ListDB;
            rptDatabase.DataBind();
        }
    }
}