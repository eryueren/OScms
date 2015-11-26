using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OScms.Web {
	public partial class index : System.Web.UI.Page {
		OS.BLL.contents.article bll = new OS.BLL.contents.article();
		public string theFirstPhoto = string.Empty;
		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				GetLunBoLst();
				GeZhuRenLst();
				GetNewsLst();
				GetGuWenLst();
				GetDuiWuLst();
			}
		}
		//首页轮播图
		protected void GetLunBoLst() {
			DataSet ds = bll.GetList(1, " category_id=96 ", "sort_id,add_time");
			if (ds != null && ds.Tables[0].Rows.Count > 0) {
				theFirstPhoto = " <img src=\"" + ds.Tables[0].Rows[0]["img_url"].ToString() + "\" width=\"1002\" height=\"230\" />";
			}
		}
		//我们主任
		protected void GeZhuRenLst() {
			DataSet ds = bll.GetList(1, " category_id=157 ", "sort_id,add_time");
			if (ds != null && ds.Tables[0].Rows.Count > 0) {
				repZhuRen.DataSource = ds;
				repZhuRen.DataBind();
			}
		}
		//新闻
		protected void GetNewsLst() {
			DataSet ds = bll.GetList(5, " category_id=136 and is_red=1 ", "sort_id,add_time");
			if (ds != null && ds.Tables[0].Rows.Count > 0) {
				repNews.DataSource = ds;
				repNews.DataBind();
			}
		}
		//我们的专家顾问	
		protected void GetGuWenLst() {
			DataSet ds = bll.GetList(12, " category_id=158 ", "sort_id,add_time");
			if (ds != null && ds.Tables[0].Rows.Count > 0) {
				guwen.DataSource = ds;
				guwen.DataBind();
			}
		}
		//我们的专家顾问	
		protected void GetDuiWuLst() {
			DataSet ds = bll.GetList(12, " category_id=159 ", "sort_id,add_time");
			if (ds != null && ds.Tables[0].Rows.Count > 0) {
				duiwu.DataSource = ds;
				duiwu.DataBind();
			}
		}
	}
}