using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;
using OS.Web.UI;

namespace OS.Web.admin
{
    public partial class Loginout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session[OSKeys.SESSION_ADMIN_INFO] = null;
				Utils.WriteCookie("AdminName", "OS", -14400);
				Utils.WriteCookie("AdminPwd", "OS", -14400);
                Response.Redirect("login.aspx");
            }
        }
    }
}