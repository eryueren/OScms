using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.article {
	public partial class article_list : Web.UI.ManagePage {
		protected int totalCount;
		protected int page;
		protected int pageSize;
		protected string category_name = string.Empty;
		protected int category_id = OSRequest.GetQueryInt("category_id");
		protected string property = OSRequest.GetQueryString("property");
		protected string keywords = OSRequest.GetQueryString("keywords");

		protected string prolistview = string.Empty;

		protected void Page_Load(object sender, EventArgs e) {
			if (category_id == 0) {
				PageErrorMsg("栏目参数不正确");
			}
			this.category_name = new BLL.contents.article_category().GetChannelName(this.category_id); //取得频道名称
			this.pageSize = GetPageSize(10); //每页数量
			this.prolistview = Utils.GetCookie("article_list_view"); //显示方式
			if (!Page.IsPostBack) {
				ChkAdminLevel(category_name, OSEnums.ActionEnum.View.ToString()); //检查权限
				RptBind("id>0 and category_id=" + category_id + CombSqlTxt(this.keywords, this.property), "sort_id asc,add_time desc,id desc");
			}
		}

		#region 数据绑定=================================
		private void RptBind(string _strWhere, string _orderby) {
			this.page = OSRequest.GetQueryInt("page", 1);
			this.ddlProperty.SelectedValue = this.property;
			this.txtKeywords.Text = this.keywords;
			//图表或列表显示
			BLL.contents.article bll = new BLL.contents.article();
			switch (this.prolistview) {
				case "Txt":
					this.rptList1.Visible = false;
					this.rptList2.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
					this.rptList2.DataBind();
					break;
				default:
					this.rptList2.Visible = false;
					this.rptList1.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
					this.rptList1.DataBind();
					break;
			}
			//绑定页码
			txtPageNum.Text = this.pageSize.ToString();
			string pageUrl = Utils.CombUrlTxt("article_list.aspx", "category_id={0}&keywords={1}&property={2}&page={3}",
				 category_id.ToString(), this.keywords, this.property, "__id__");
			PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
		}
		#endregion

		#region 组合SQL查询语句==========================
		protected string CombSqlTxt(string _keywords, string _property) {
			StringBuilder strTemp = new StringBuilder();
			_keywords = _keywords.Replace("'", "");
			if (!string.IsNullOrEmpty(_keywords)) {
				strTemp.Append(" and title like '%" + _keywords + "%'");
			}
			if (!string.IsNullOrEmpty(_property)) {
				switch (_property) {
					case "isLock":
						strTemp.Append(" and is_lock=1");
						break;
					case "unIsLock":
						strTemp.Append(" and is_lock=0");
						break;
					case "isMsg":
						strTemp.Append(" and is_msg=1");
						break;
					case "isTop":
						strTemp.Append(" and is_top=1");
						break;
					case "isRed":
						strTemp.Append(" and is_red=1");
						break;
					case "isHot":
						strTemp.Append(" and is_hot=1");
						break;
					case "isSlide":
						strTemp.Append(" and is_slide=1");
						break;
				}
			}
			return strTemp.ToString();
		}
		#endregion

		#region 返回图文每页数量=========================
		private int GetPageSize(int _default_size) {
			int _pagesize;
			if (int.TryParse(Utils.GetCookie("article_page_size"), out _pagesize)) {
				if (_pagesize > 0) {
					return _pagesize;
				}
			}
			return _default_size;
		}
		#endregion

		//设置操作
		protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e) {
			ChkAdminLevel("category_" + this.category_name + "_list", OSEnums.ActionEnum.Edit.ToString()); //检查权限
			int id = Convert.ToInt32(((HiddenField)e.Item.FindControl("hidId")).Value);
			BLL.contents.article bll = new BLL.contents.article();
			Model.contents.article model = bll.GetModel(id);
			switch (e.CommandName) {
				case "lbtnIsMsg":
					if (model.is_msg == 1)
						bll.UpdateField(id, "is_msg=0");
					else
						bll.UpdateField(id, "is_msg=1");
					break;
				case "lbtnIsTop":
					if (model.is_top == 1)
						bll.UpdateField(id, "is_top=0");
					else
						bll.UpdateField(id, "is_top=1");
					break;
				case "lbtnIsRed":
					if (model.is_red == 1)
						bll.UpdateField(id, "is_red=0");
					else
						bll.UpdateField(id, "is_red=1");
					break;
				case "lbtnIsHot":
					if (model.is_hot == 1)
						bll.UpdateField(id, "is_hot=0");
					else
						bll.UpdateField(id, "is_hot=1");
					break;
				case "lbtnIsSlide":
					if (model.is_slide == 1)
						bll.UpdateField(id, "is_slide=0");
					else
						bll.UpdateField(id, "is_slide=1");
					break;
			}
			RptBind("id>0 and category_id=" + category_id + CombSqlTxt(this.keywords, this.property), "sort_id asc,add_time desc,id desc");
		}

		//关健字查询
		protected void btnSearch_Click(object sender, EventArgs e) {
			Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "category_id={0}&keywords={1}&property={2}",
				 this.category_id.ToString(), txtKeywords.Text, this.property));
		}



		//筛选属性
		protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e) {
			Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "category_id={0}&keywords={1}&property={2}",
			   this.category_id.ToString(), this.keywords, ddlProperty.SelectedValue));
		}

		//设置文字列表显示
		protected void lbtnViewTxt_Click(object sender, EventArgs e) {
			Utils.WriteCookie("article_list_view", "Txt", 14400);
			Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "category_id={0}&keywords={1}&property={2}&page={4}",
			   this.category_id.ToString(), this.keywords, this.property, this.page.ToString()));
		}

		//设置图文列表显示
		protected void lbtnViewImg_Click(object sender, EventArgs e) {
			Utils.WriteCookie("article_list_view", "Img", 14400);
			Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "category_id={0}&keywords={1}&property={2}&page={4}",
				 this.category_id.ToString(), this.keywords, this.property, this.page.ToString()));
		}

		//设置分页数量
		protected void txtPageNum_TextChanged(object sender, EventArgs e) {
			int _pagesize;
			if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize)) {
				if (_pagesize > 0) {
					Utils.WriteCookie("article_page_size", _pagesize.ToString(), 43200);
				}
			}
			Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "category_id={0}&keywords={1}&property={2}",
				this.category_id.ToString(), this.keywords, this.property));
		}

		//保存排序
		protected void btnSave_Click(object sender, EventArgs e) {
			ChkAdminLevel("category_" + this.category_name + "_list", OSEnums.ActionEnum.Edit.ToString()); //检查权限
			BLL.contents.article bll = new BLL.contents.article();
			Repeater rptList = new Repeater();
			switch (this.prolistview) {
				case "Txt":
					rptList = this.rptList1;
					break;
				default:
					rptList = this.rptList2;
					break;
			}
			for (int i = 0; i < rptList.Items.Count; i++) {
				int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
				int sortId;
				if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId)) {
					sortId = 99;
				}
				bll.UpdateField(id, "sort_id=" + sortId.ToString());
			}
			AddAdminLog(OSEnums.ActionEnum.Edit.ToString(), "保存" + this.category_name + "频道内容排序"); //记录日志
			Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "category_id={0}&keywords={1}&property={2}",
			   this.category_id.ToString(), this.keywords, this.property));
		}

		//批量删除
		protected void btnDelete_Click(object sender, EventArgs e) {
			ChkAdminLevel(category_name, OSEnums.ActionEnum.Delete.ToString()); //检查权限
			int sucCount = 0; //成功数量
			int errorCount = 0; //失败数量
			BLL.contents.article bll = new BLL.contents.article();
			Repeater rptList = new Repeater();
			switch (this.prolistview) {
				case "Img":
					rptList = this.rptList1;
					break;
				case "Txt":
					rptList = this.rptList2;
					break;
				default:
					rptList = this.rptList1;
					break;
			}
			for (int i = 0; i < rptList.Items.Count; i++) {
				int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
				CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
				if (cb.Checked) {
					if (bll.Delete(id)) {
						sucCount++;
					}
					else {
						errorCount++;
					}
				}
			}
			AddAdminLog(OSEnums.ActionEnum.Edit.ToString(), "删除" + this.category_name + "频道内容成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
			Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "category_id={0}&keywords={1}&property={2}",
			this.category_id.ToString(), this.keywords, this.property));
		}

	}
}