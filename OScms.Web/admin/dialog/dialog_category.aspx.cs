using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.dialog
{
    public partial class dialog_category : Web.UI.ManagePage
    {
        private int id = OSRequest.GetQueryInt("id");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (id == 0)
            {
                PageErrorMsg("传输参数不正确");
            }
            if (!new BLL.contents.article_category().Exists(this.id))
            {
                PageErrorMsg("类别不存在或已被删除");
            }
            if (!Page.IsPostBack)
            {
                ShowInfo(ddlCategory1);
                if (this.id != 0)
                {
                    ddlCategory1.SelectedValue = this.id.ToString();
                }
                ShowInfo(ddlCategory2);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(DropDownList  ddl)
        {
            //BLL.carts.orders bll = new BLL.carts.orders();
            //Model.carts.orders model = bll.GetModel(_order_no);

            //BLL.carts.express bll2 = new BLL.carts.express();
            //DataTable dt = bll2.GetList("").Tables[0];
            //ddlExpressId.Items.Clear();
            //ddlExpressId.Items.Add(new ListItem("请选择配送方式", ""));
            //foreach (DataRow dr in dt.Rows)
            //{
            //    ddlExpressId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            //}

            //txtExpressNo.Text = model.express_no;
            //ddlExpressId.SelectedValue = model.express_id.ToString();


            BLL.contents.article_category bll = new BLL.contents.article_category();
            DataTable dt = bll.GetLists(1, "channel_id>0 ");

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(siteConfig.webname, "1"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 2)
                {
                    ddl.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    ddl.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion
    }
}