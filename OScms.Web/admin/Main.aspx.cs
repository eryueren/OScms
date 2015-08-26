using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using OS.Web.UI;
namespace OS.Web.Admin
{
    public partial class Main : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["LoinName"]==null && Session["LoinName"].ToString()=="")
            //{
            //    Response.Redirect("login.aspx");
            //}
        }
    }
}
