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
using OS.Common;
using OS.Web.UI;
using System.Text;



namespace OS.Web.Admin
{
    public partial class Left : ManagePage
    {
        protected int type = OSRequest.GetInt("type", 1);
        BLL.contents.article_category bll = new BLL.contents.article_category();
        Model.contents.article_category model = new Model.contents.article_category();
        protected string className = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                className = bll.GetTitle(type);
                if (type == 1)
                {
                    this.ColumnListBind();
                }
            }
        }

        protected string BindType()
        {
            StringBuilder sbr = new StringBuilder();
            sbr.Append(" <div class=\"subNav\">");
            sbr.Append("<ul>");
            DataTable dt = bll.GetList(0, "is_lock=0 and parent_id=" + type, "sort_id asc,id desc").Tables[0];
            if (dt.Rows != null && dt.Rows.Count > 0)
            {
                sbr.Append("<ul>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sbr.Append("<li><span><img src=\"images/tu4.gif\" width=\"16px\" height=\"16px\"/></span><a href=\"" + dt.Rows[i]["link_url"] + "\" target=\"mainFrame\">" + dt.Rows[i]["title"] + "</a></li>");
                }
                sbr.Append("</ul>");
            }
            sbr.Append("</div>");
            return sbr.ToString();
        }

        protected void ColumnListBind()
        {
            DataTable dt = bll.GetLists(0, "channel_id>0 ");
            this.ColumnList.DataSource = dt;
            this.ColumnList.DataBind();
        }

        public string GetColumn(int Id)
        {
            StringBuilder sb = new StringBuilder();
            int count = new BLL.contents.article().GetCount("category_id=" + Id + "") > 0 ? new BLL.contents.article().GetCount("category_id=" + Id + "") : 0;
            model = bll.GetModel(Id);
            switch (model.model_id)
            {
                case 1:
                    sb.Append("<a href=\"article/article_list.aspx?category_id=" + Id + "\" target=\"mainFrame\" >" + model.title + "</a>" + "<span class=\"column\">(" + count + ")</span>");
                    break;
                case 2:
                    sb.Append("<a href=\"article/article_list.aspx?category_id=" + Id + "\" target=\"mainFrame\">" + model.title + "</a>" + "<span class=\"column\">(" + count + ")</span>");
                    break;
                case 3:
                    sb.Append("<a href=\"article/article_list.aspx?category_id=" + Id + "\" target=\"mainFrame\" >" + model.title + "</a>" + "<span class=\"column\">(" + count + ")</span>");
                    break;
                case 4:
                    sb.Append("<a href=\"article/article_list.aspx?category_id=" + Id + "\" target=\"mainFrame\" >" + model.title + "</a>" + "<span class=\"column\">(" + count + ")</span>");
                    break;
                case 0:
                    sb.Append(model.title);
                    break;
            }
            return sb.ToString();

        }

        #region 显示广告类型=================================
        protected string GetTypeName(int typeId)
        {
            switch (typeId)
            {
                case 1:
                    return "内容管理";
                case 2:
                    return "用户管理";
                case 3:
                    return "附件管理";
                case 4:
                    return "控制面板";
                default:
                    return "其它";
            }
        }
        #endregion
    }
}
