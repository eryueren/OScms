using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using OS.Web.UI;
using OS.Common;
namespace OS.Web.Admin
{
    public partial class Top : ManagePage
    {
        protected Model.managers.manager admin_info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                admin_info = GetAdminInfo();
            }
        }
        protected string get_menu()
        {
            StringBuilder sb = new StringBuilder();
            BLL.contents.article_category bll = new BLL.contents.article_category();
            DataTable dt = bll.GetList(0, "parent_id=0 and is_lock=0", "sort_id asc,id desc").Tables[0];
            if (dt.Rows != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<li class=\"topMenuIco_" + (i + 1) + "\"><a href=\"Left.aspx?type=" + dt.Rows[i]["id"] + "\" target=\"leftFrame\">" + dt.Rows[i]["title"] + "</a></li>");
                }
            }
            return sb.ToString();
        }

    }
}
