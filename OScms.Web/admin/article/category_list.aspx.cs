using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.article
{
    public partial class category_list : Web.UI.ManagePage
    {
        protected string channel_name = string.Empty; //频道名称
         BLL.contents.article_category bll = new BLL.contents.article_category();
         Model.contents.article_category model = new Model.contents.article_category();
         protected int channel_id = YLRequest.GetQueryInt("id");
         protected string _action = YLRequest.GetString("action");
         protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("category", YLEnums.ActionEnum.View.ToString()); //检查权限
                if (channel_id !=0 && _action == "del")
                {
                    ChkAdminLevel("category", YLEnums.ActionEnum.Delete.ToString()); //检查权限
                    bll.Delete(channel_id);
                }
                this.ColumnListBind();
            }
        }

        protected void ColumnListBind()
        {
            DataTable dt = bll.GetLists(0, "channel_id>0 ");
            this.ColumnList.DataSource = dt;
            this.ColumnList.DataBind();
        }

        public string Column_Access(int Id)
        {
            if (Id != -1)
            {
                model = bll.GetModel(Id);
                //允许添加子类
                string is_zilei = model.is_add_category == 1 ? " <a style=\"padding-left:30px;\" href=\"category_edit.aspx?action=" + YLEnums.ActionEnum.Add + "&channel_id=" + model.channel_id + "&id=" + Id + "\"/>添加子类</a>" : "";
                //默认最多只能添加三级("model.class_layer == 3"深度)
                string is_zilei_there = model.class_layer == 4 ? "" : is_zilei;
                string style = is_zilei_there == "" ? "style=\"padding-left:20px;\"" : "style=\"padding-left:10px;\"";
                return is_zilei_there + "<a " + style + " href=\"category_edit.aspx?action=" + YLEnums.ActionEnum.Edit + "&channel_id=" + model.channel_id + "&id=" + Id + "\"/>编辑</a>   <a style=\"padding-left:10px;\"  href=\"javascript:del(" + Id + ")\" class=\"del\">删除</a>   <a  style=\"padding-left:10px;\" href=\"javascript:categoey(" + Id + ")\" >移动</a>";
            }
            return "";
        }

        public string Column_UpDown(int Id)
        {
            string str = string.Empty;
            str = "<a style=\"padding-left:10px;\" onclick=\"return UpDown(" + Id + ",0)\"  href=\"javascript:void(0);\"><img src=\"../images/up.gif\"/></a><a style=\"padding-left:10px;\" onclick=\"return UpDown(" + Id + ",1)\" href=\"javascript:void(0);\"><img  src=\"../images/down.gif\"/></a>";
            return str;
        }

        protected int get_channel_id()
        {
            int strid = 1;
            DataTable dt = bll.GetList(0, "parent_id=0", "id").Tables[0];
            if (dt.Rows != null && dt.Rows.Count > 0)
            { strid = dt.Rows.Count; }
            return strid+1;
        }

    }
}