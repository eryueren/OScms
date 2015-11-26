using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OScms.Web {
	public partial class info : System.Web.UI.Page {
		public int cId = OS.Common.OSRequest.GetInt("cId", 0), fId = OS.Common.OSRequest.GetInt("fId", 132), totalCount, id = OS.Common.OSRequest.GetInt("top", 132);
		public int page = OS.Common.OSRequest.GetQueryInt("page", 1), TemplateId, num = OS.Common.OSRequest.GetInt("mId", 1);

		OS.BLL.contents.article_category bllcategory = new OS.BLL.contents.article_category();
		OS.BLL.contents.article bll = new OS.BLL.contents.article();
		OS.Model.contents.article model = null;
		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				GetDataBand();
			}
		}
		//判断模板
		protected void GetDataBand() {
			System.Data.DataTable dt = bllcategory.GetList(1, " id=" + id + " and is_lock=0 ", "id").Tables[0];
			if (dt != null && dt.Rows.Count > 0) {
				TemplateId = Convert.ToInt32(dt.Rows[0]["model_id"]);
				//单页显示（单页模板/详细页）
				if (TemplateId == 1) {
					if (dt != null) {
						GetDataDefault(id);
					}
				}
				//新闻列表
				else if (TemplateId == 2) {
					if (cId == 0) {
						get_list_page();
					}
					else {
						GetDataDefault(0);
					}
				}
				//图文列表
				else if (TemplateId == 3) {
					if (cId == 0) {
						get_list_page();
					}
					else {
						GetDataDefault(0);
					}
				}
			}
		}
		//列表模板，分页
		protected void get_list_page() {
			int pageSize = 15;
			System.Data.DataSet ds = bll.GetList(pageSize, page, " category_id=" + id + " and status=0 ", "sort_id asc,add_time desc", out totalCount);
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			//计算页数
			sb.Append("" + OS.Common.Utils.OutPageList(pageSize, this.page, this.totalCount, "info.aspx?top=" + id + "&page=__id__", 10) + "");
			if (ds != null) {
				if (TemplateId == 2) {
					repLstNews.DataSource = ds;
					repLstNews.DataBind();
				}
				else if (TemplateId == 3) {
					repLstTuWen.DataSource = ds;
					repLstTuWen.DataBind();
				}
				pageHtml.InnerHtml = " <ul>" + sb.ToString() + "</ul>";
			}
		}
		//内容详细页
		protected void GetDataDefault(int _id) {
			List<OS.Model.contents.article> modelLst = new List<OS.Model.contents.article>();
			model = new OS.Model.contents.article();
			TemplateId = 1;
			//详细页
			if (cId != 0 && _id == 0) {
				model = bll.GetModel(cId);
				modelLst.Add(model);
				if (modelLst != null) {
					repDetail.DataSource = modelLst;
					repDetail.DataBind();
				}
			}
			//单页模板的内容
			else {
				System.Data.DataSet ds = bll.GetList(1, " category_id=" + id + " and status=0 ", "sort_id asc,add_time desc");
				if (ds != null) {
					repDanYe.DataSource = ds;
					repDanYe.DataBind();
				}
			}

		}
	}
}