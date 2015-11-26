using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Configuration;
using OS.Common;

namespace OS.Web.UI {
	public partial class BasePage : System.Web.UI.Page {

		public static Model.configs.siteconfig config = new BLL.configs.siteconfig().loadConfig();
		public static Model.configs.userconfig uconfig = new BLL.configs.userconfig().loadConfig();
		public BasePage() {
			//是否关闭网站
			if (config.webstatus == 0) {
				HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode(config.webclosereason));
				return;
			}
		}
		#region 页面通用方法==========================================
		/// <summary>
		/// 返回URL重写统一链接地址
		/// </summary>
		public string linkurl(string _key, params object[] _params) {
			Hashtable ht = new BLL.configs.url_rewrite().GetList(); //获得URL配置列表
			Model.configs.url_rewrite model = ht[_key] as Model.configs.url_rewrite; //查找指定的URL配置节点
			//如果不存在该节点则返回空字符串
			if (model == null) {
				return string.Empty;
			}

			// string requestDomain = HttpContext.Current.Request.Url.Authority.ToLower(); //获得来源域名含端口号
			// string requestFirstPath = GetFirstPath();//获得二级目录(不含站点安装目录)
			string linkStartString = string.Empty; //链接前缀

			//如果URL字典表达式不需要重写则直接返回
			if (model.url_rewrite_items.Count == 0) {
				//检查网站重写状态
				if (config.staticstatus > 0) {
					if (_params.Length > 0) {
						return linkStartString + GetUrlExtension(model.page, config.staticextension) + string.Format("{0}", _params);
					}
					else {
						return linkStartString + GetUrlExtension(model.page, config.staticextension);
					}
				}
				else {
					if (_params.Length > 0) {
						return linkStartString + model.page + string.Format("{0}", _params);
					}
					else {
						return linkStartString + model.page;
					}
				}
			}
			//否则检查该URL配置节点下的子节点
			foreach (Model.configs.url_rewrite_item item in model.url_rewrite_items) {
				//如果参数个数匹配
				if (IsUrlMatch(item, _params)) {
					//检查网站重写状态
					if (config.staticstatus > 0) {
						return linkStartString + string.Format(GetUrlExtension(item.path, config.staticextension), _params);
					}
					else {
						string queryString = Regex.Replace(string.Format(item.path, _params), item.pattern, item.querystring, RegexOptions.None | RegexOptions.IgnoreCase);
						if (queryString.Length > 0) {
							queryString = "?" + queryString;
						}
						return linkStartString + model.page + queryString;
					}
				}
			}

			return string.Empty;
		}

		/// <summary>
		/// 参数个数是否匹配
		/// </summary>
		private bool IsUrlMatch(Model.configs.url_rewrite_item item, params object[] _params) {
			int strLength = 0;
			if (!string.IsNullOrEmpty(item.querystring)) {
				strLength = item.querystring.Split('&').Length;
			}
			if (strLength == _params.Length) {
				//注意__id__代表分页页码，所以须替换成数字才成进行匹配
				if (Regex.IsMatch(string.Format(item.path, _params).Replace("__id__", "1"), item.pattern, RegexOptions.None | RegexOptions.IgnoreCase)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 替换扩展名
		/// </summary>
		private string GetUrlExtension(string urlPage, string staticExtension) {
			return Utils.GetUrlExtension(urlPage, staticExtension);
		}
		#endregion
		#region ADD John
		public static void WriteDataToExcel(DataSet ds, string FileName) {
			HttpResponse resp;
			resp = System.Web.HttpContext.Current.Response;
			resp.Clear();
			resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
			resp.AppendHeader("Content-Disposition", "attachment;filename=" + FileName + ".xls");
			string colHeaders = "", ls_item = "";
			int i = 0;
			//定义表对象与行对像，同时用DataSet对其值进行初始化         
			DataTable dt = ds.Tables[0];
			DataRow[] myRow = dt.Select("");
			//取得数据表各列标题，各标题之间以\t分割，最后一个列标题后加回车符         
			for (i = 0; i < dt.Columns.Count - 1; i++)
				colHeaders += dt.Columns[i].Caption.ToString() + "\t";
			colHeaders += dt.Columns[i].Caption.ToString() + "\n";
			//向HTTP输出流中写入取得的数据信息         
			resp.Write(colHeaders);
			//逐行处理数据         
			foreach (DataRow row in myRow) {
				//在当前行中，逐列获得数据，数据之间以\t分割，结束时加回车符\n         
				for (i = 0; i < row.Table.Columns.Count - 1; i++)
					ls_item += row[i].ToString() + "\t";
				ls_item += row[i].ToString() + "\n";
				//当前行数据写入HTTP输出流，并且置空ls_item以便下行数据         
				resp.Write(ls_item);
				ls_item = "";
			}
			System.IO.File.Delete(FileName + ".xls");//删除临时文件   
			//写缓冲区中的数据到HTTP头文件中         
			resp.End();

		}
		/// <summary>
		/// 加载页面头部图片地址
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetImgUrl(int id) {
			string rStr = string.Empty;
			OS.BLL.contents.article_category bll = new OS.BLL.contents.article_category();
			OS.Model.contents.article_category model = new OS.Model.contents.article_category();
			model = bll.GetModel(id);
			if (model != null) {
				rStr = model.img_url;
			}
			return rStr;
		}
		/// <summary>
		/// 加载外链接
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetLinkUrl(string category_id) {
			string rStr = string.Empty;
			OS.BLL.contents.article bll = new OS.BLL.contents.article();
			OS.Model.contents.article model = new OS.Model.contents.article();
			model = bll.GetModel(category_id);
			if (model != null) {
				rStr = " <a target=\"_blank\"  href=\"" + model.link_url + "\"><img src=\"" + model.img_url + "\" /></a>";
			}
			return rStr;
		}
		/// <summary>
		/// 加载页面头部轮播图片地址
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetLunBoImgUrl(int id) {
			string rStr = string.Empty;
			OS.BLL.contents.article_category bll = new OS.BLL.contents.article_category();
			OS.Model.contents.article_category model = new OS.Model.contents.article_category();
			model = bll.GetModel(id);
			if (model != null) {
				string h = "154";
				if (id == 95) {
					h = "204";
				}
				rStr += "<div class=\"pic\"  id=\"ShadowL\">";
				if (!string.IsNullOrEmpty(model.img_url)) {
					rStr += "<a href=\"javascript:void(0)\" style=\"display: block\" class=\"change\"><img alt=\"\" title=\"\" width=\"828\" height=\"" + h + "\" src=\"" + model.img_url + "\"></a>";
				}
				if (!string.IsNullOrEmpty(model.seo_title)) {
					rStr += "<a href=\"javascript:void(0)\" style=\"display: none\" class=\"change\"><img alt=\"\" title=\"\" width=\"828\" height=\"" + h + "\" src=\"" + model.seo_title + "\"></a>";
				}
				if (!string.IsNullOrEmpty(model.seo_keywords)) {
					rStr += "<a href=\"javascript:void(0)\" style=\"display: none\" class=\"change\"><img alt=\"\" title=\"\" width=\"828\" height=\"" + h + "\" src=\"" + model.seo_keywords + "\"></a>";
				}
				rStr += "</div>";
			}
			return rStr;
		}
		/// <summary>
		/// 获取菜单项
		/// </summary>
		/// <returns></returns>
		public static string GetMenuAndChildLst() {
			StringBuilder rStr = new StringBuilder();
			BLL.contents.article_category bll = new BLL.contents.article_category();
			//一级菜单
			DataTable dt = bll.GetList(-1, " parent_id=1   and  is_lock=0  and model_id<5 ", "sort_id").Tables[0];
			if (dt != null && dt.Rows.Count > 0) {
				for (int i = 0; i < dt.Rows.Count; i++) {
					rStr.Append(" <li  id=\"menu" + dt.Rows[i]["id"] + "\"> <span  class=\"nav_span_f\"><a  href=\"" + dt.Rows[i]["link_url"] + "\">");
					rStr.Append(dt.Rows[i]["title"]);
					rStr.Append(" </span></a>");
					//二级菜单
					DataTable dtChild = bll.GetLists(Convert.ToInt32(dt.Rows[i]["id"]), " parent_id=" + dt.Rows[i]["id"] + "  and  is_lock=0  and id not in(96,97,121,122,128,129,130) and   model_id<5");
					if (dtChild.Rows.Count > 0) {
						rStr.Append("<div>");
					}
					for (int ic = 0; ic < dtChild.Rows.Count; ic++) {
						if (dtChild.Rows[ic]["id"].ToString().Trim() == "167") {
							rStr.Append("<a  id=\"child" + dtChild.Rows[ic]["id"] + "\"  target=\"_blank\"  href=\"" + dtChild.Rows[ic]["link_url"]);
						}
						else {
							rStr.Append("<a  id=\"child" + dtChild.Rows[ic]["id"] + "\" href=\"" + dtChild.Rows[ic]["link_url"]);
						}
						rStr.Append("?id=" + dtChild.Rows[ic]["id"] + "\"><span>" + dtChild.Rows[ic]["title"].ToString() + "</span></a>");
					}
					if (dtChild.Rows.Count > 0) {
						rStr.Append("</div>");
					}
					rStr.Append("</li>");
				}
			}
			return rStr.ToString();
		}
		/// <summary>
		/// 获取菜单项
		/// </summary>
		/// <returns></returns>
		public static string GetMenuAndChildLstByFid(int Fid) {
			StringBuilder rStr = new StringBuilder();
			BLL.contents.article_category bll = new BLL.contents.article_category();
			//一级菜单
			DataTable dt = bll.GetList(-1, " parent_id=" + Fid + "   and  is_lock=0  and model_id<5 ", "sort_id").Tables[0];
			if (dt != null && dt.Rows.Count > 0) {
				for (int i = 0; i < dt.Rows.Count; i++) {
					//二级菜单
					DataTable dtChild = bll.GetLists(Convert.ToInt32(dt.Rows[i]["id"]), " parent_id=" + dt.Rows[i]["id"] + "  and  is_lock=0  and id not in(221,147,154,206,207,251,252,253,254,257) and   model_id<5");
					if (dtChild.Rows.Count > 0) {
						rStr.Append(" <li  id=\"child" + dt.Rows[i]["id"] + "\"> <div> <a><b>");
						rStr.Append(dt.Rows[i]["title"]);
						rStr.Append(" </b> <span class=\"arrow up\"></span></a></div>");
						rStr.Append("<ul class=\"body_int_gk_li_div\" style=\"display: block;\">");
					}
					else {
						rStr.Append(" <li id=\"child" + dt.Rows[i]["id"] + "\"> <a   href=\"" + dt.Rows[i]["link_url"] + "\">");
						rStr.Append(dt.Rows[i]["title"]);
						rStr.Append(" </a>");
					}
					int numChild = 1;
					for (int ic = 0; ic < dtChild.Rows.Count; ic++) {
						rStr.Append("<li><a  id=\"child" + numChild + "\" href=\"" + dtChild.Rows[ic]["link_url"]);
						rStr.Append("\">" + dtChild.Rows[ic]["title"].ToString() + "</a></li>");
						numChild++;
					}
					if (dtChild.Rows.Count > 0) {
						rStr.Append("</ul>");
					}
					rStr.Append("</li>");
				}
			}
			return rStr.ToString();
		}
		/// <summary>
		/// 获取菜单项
		/// </summary>
		/// <returns></returns>
		public static string GetMenuAndChildLstByFidAndId(int Fid, int id) {
			StringBuilder rStr = new StringBuilder();
			BLL.contents.article_category bll = new BLL.contents.article_category();
			//一级菜单
			DataTable dt = bll.GetList(-1, " parent_id=" + Fid + "   and  is_lock=0  and model_id<5 ", "sort_id").Tables[0];
			if (dt != null && dt.Rows.Count > 0) {
				for (int i = 0; i < dt.Rows.Count; i++) {
					string classStr = string.Empty;
					if (id == Convert.ToInt32(dt.Rows[i]["id"])) {
						classStr = "class=\"changechild\"";
					}
					//二级菜单
					DataTable dtChild = bll.GetLists(Convert.ToInt32(dt.Rows[i]["id"]), " parent_id=" + dt.Rows[i]["id"] + "  and  is_lock=0  and id not in(221,147,154,206,207,251,252,253,254,257) and   model_id<5");
					if (dtChild.Rows.Count > 0) {

						rStr.Append(" <li " + classStr + "  id=\"child" + dt.Rows[i]["id"] + "\"> <div> <a><b>");
						rStr.Append(dt.Rows[i]["title"]);
						rStr.Append(" </b> <span class=\"arrow up\"></span></a></div>");
						rStr.Append("<ul class=\"body_int_gk_li_div\" style=\"display: block;\">");
					}
					else {
						rStr.Append(" <li " + classStr + " id=\"child" + dt.Rows[i]["id"] + "\"> <a   href=\"" + dt.Rows[i]["link_url"] + "?id=" + dt.Rows[i]["id"] + "\">");
						rStr.Append(dt.Rows[i]["title"]);
						rStr.Append(" </a>");
					}
					int numChild = 1;
					for (int ic = 0; ic < dtChild.Rows.Count; ic++) {
						rStr.Append("<li><a  id=\"child" + numChild + "\" href=\"" + dtChild.Rows[ic]["link_url"]);
						rStr.Append("?id=" + dtChild.Rows[ic]["id"] + "\">" + dtChild.Rows[ic]["title"].ToString() + "</a></li>");
						numChild++;
					}
					if (dtChild.Rows.Count > 0) {
						rStr.Append("</ul>");
					}
					rStr.Append("</li>");
				}
			}
			return rStr.ToString();
		}
		/// <summary>
		/// 获取菜单项
		/// </summary>
		/// <returns></returns>
		public static string GetMenuLst() {
			StringBuilder rStr = new StringBuilder();
			BLL.contents.article_category bll = new BLL.contents.article_category();
			//一级菜单
			DataTable dt = bll.GetLists(1, " parent_id=1  and  is_lock=0 ");
			if (dt != null && dt.Rows.Count > 0) {
				int num = 1;
				for (int i = 0; i < dt.Rows.Count; i++) {
					rStr.Append(" <li><a id=\"menu" + dt.Rows[i]["id"] + "\" class=\"\"  href=\"" + dt.Rows[i]["link_url"] + "\">");
					rStr.Append(dt.Rows[i]["title"]);
					rStr.Append("</a></li>");
				}
				num++;
			}
			return rStr.ToString();
		}

		/// <summary>
		/// 获取菜单项
		/// </summary>
		/// <returns></returns>
		public static string GetMenuLstOfPhone() {

			StringBuilder rStr = new StringBuilder();
			BLL.contents.article_category bll = new BLL.contents.article_category();
			//一级菜单
			DataTable dt = bll.GetLists(1, " parent_id=1  and  is_lock=0 ");
			if (dt != null && dt.Rows.Count > 0) {
				for (int i = 0; i < dt.Rows.Count; i++) {
					switch (Convert.ToInt32(dt.Rows[i]["id"])) {
						case 123:
							rStr.Append(" <li ><a href=\"index.aspx\">");
							break;
						case 129:
							rStr.Append(" <li ><a href=\"jqg.aspx?fId=" + dt.Rows[i]["id"] + "&cId=137\">");
							break;
						case 134:
							rStr.Append(" <li ><a href=\"jqg.aspx?fId=" + dt.Rows[i]["id"] + "&cId=135\">");
							break;
						default:
							rStr.Append(" <li ><a href=\"jqg.aspx?fId=" + dt.Rows[i]["id"] + "&cId=" + dt.Rows[i]["id"] + "\">");
							break;
					}
					rStr.Append(dt.Rows[i]["title"]);
					rStr.Append("<i></i></a></li>");
				}
			}
			return rStr.ToString();
		}
		/// <summary>
		/// 获取Foot菜单项
		/// </summary>
		/// <returns></returns>
		public static string GetMenuFootLst() {
			StringBuilder rStr = new StringBuilder();
			BLL.contents.article_category bll = new BLL.contents.article_category();
			DataTable dt = bll.GetLists(1, " parent_id=1  and id!=91  and  is_lock=0 ");
			if (dt != null && dt.Rows.Count > 0) {
				int num = 1;
				for (int i = 0; i < dt.Rows.Count; i++) {
					rStr.Append("<dl>");
					rStr.Append(" <dt><a href=\"" + dt.Rows[i]["link_url"] + "\">");
					rStr.Append(dt.Rows[i]["title"]);
					DataTable dtChild = bll.GetLists(Convert.ToInt32(dt.Rows[i]["id"]), " parent_id=" + dt.Rows[i]["id"] + "  and  is_lock=0 ");
					int numChild = 1;
					for (int ic = 0; ic < dtChild.Rows.Count; ic++) {
						rStr.Append("<dd>");
						rStr.Append("<a  id=\"child" + numChild + "\" href=\"" + dtChild.Rows[ic]["link_url"]);
						rStr.Append("?IdF=" + dtChild.Rows[ic]["parent_id"] + "&id=" + dtChild.Rows[ic]["id"] + "&mId=" + numChild + "\">" + dtChild.Rows[ic]["title"].ToString() + "</a>");
						numChild++;
						rStr.Append("</dd>");
					}
					rStr.Append("</a></dt>");
					rStr.Append("</dl>");
					num++;
				}
			}
			return rStr.ToString();
		}
		/// <summary>
		/// 获取二级菜单
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetMenuChild(int id) {
			StringBuilder rStr = new StringBuilder();
			BLL.contents.article_category bll = new BLL.contents.article_category();
			DataTable dt = bll.GetChildList(id, 6);
			if (dt != null && dt.Rows.Count > 0) {
				int num = 1;
				for (int i = 0; i < dt.Rows.Count; i++) {
					rStr.Append("<li class=\"li" + num + "\">");
					rStr.Append("<a href=\"" + dt.Rows[i]["link_url"] + "?fId=" + dt.Rows[i]["parent_id"] + "&cId=" + dt.Rows[i]["id"] + "\">");
					rStr.Append(dt.Rows[i]["title"].ToString());
					rStr.Append("</a>");
					rStr.Append("</li>");
					num++;
				}
			}
			return rStr.ToString();
		}

		/// <summary>
		/// 获取二级菜单
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetMenuChildById(int id) {
			if (id == 123) {
				return string.Empty;
			}
			StringBuilder rStr = new StringBuilder();
			BLL.contents.article_category bll = new BLL.contents.article_category();
			DataTable dt = bll.GetChildList(id, 6);
			if (dt != null && dt.Rows.Count > 0) {
				int num = 1;
				for (int i = 0; i < dt.Rows.Count; i++) {
					if (i < 3) {
						rStr.Append("<li class=\"li" + num + "\">");
						rStr.Append("<a href=\"" + dt.Rows[i]["link_url"] + "?fId=" + dt.Rows[i]["parent_id"] + "&cId=" + dt.Rows[i]["id"] + "\">");
						rStr.Append(dt.Rows[i]["title"].ToString());
						rStr.Append("</a>");
						rStr.Append("</li>");
					}
					else {
						if (i == 3) {
							rStr.Append("<li><a id=\"a\" href=\"#\">更多</a></li>");
							rStr.Append("  <ul id=\"ul1\" class=\"dis\">");
							rStr.Append(" <li><a href=\"" + dt.Rows[i]["link_url"] + "?fId=" + dt.Rows[i]["parent_id"] + "&cId=" + dt.Rows[i]["id"] + "\">" + dt.Rows[i]["title"] + "</a></li>");
						}
						else if (i == dt.Rows.Count - 1) {
							rStr.Append("<li><a href=\"" + dt.Rows[i]["link_url"] + "?fId=" + dt.Rows[i]["parent_id"] + "&cId=" + dt.Rows[i]["id"] + "\">" + dt.Rows[i]["title"] + "</a></li>");
							rStr.Append("</ul>");
						}
						else {
							rStr.Append("<li><a href=\"" + dt.Rows[i]["link_url"] + "?fId=" + dt.Rows[i]["parent_id"] + "&cId=" + dt.Rows[i]["id"] + "\">" + dt.Rows[i]["title"] + "</a></li>");
						}

					}
					if (i < dt.Rows.Count - 1) {
						rStr.Append("| ");
					}
					num++;
				}
			}
			return rStr.ToString();
		}

		/// <summary>
		/// 获取二级菜单English
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetMenuChild_En(int id) {
			StringBuilder rStr = new StringBuilder();
			BLL.contents.article_category bll = new BLL.contents.article_category();
			DataTable dt = bll.GetChildList(id, 6);
			if (dt != null && dt.Rows.Count > 0) {
				int num = 1;
				for (int i = 0; i < dt.Rows.Count; i++) {
					if (!string.IsNullOrEmpty(dt.Rows[i]["link_url"].ToString())) {
						rStr.Append("<dd><a  id=\"child" + num + "\" href=\"" + dt.Rows[i]["link_url"]);
						rStr.Append("?IdF=" + dt.Rows[i]["parent_id"] + "&id=" + dt.Rows[i]["id"] + "&mId=" + num + "\"><i>></i>" + dt.Rows[i]["title"].ToString() + "</a></dd>");
						num++;
					}
				}
			}
			return rStr.ToString();
		}

		public static string GetMenuChildUrl(int cid) {
			BLL.contents.article_category bll = new BLL.contents.article_category();
			int id = bll.GetParentId(cid);
			StringBuilder rStr = new StringBuilder();
			DataTable dt = bll.GetChildList(id, 6);
			if (dt != null && dt.Rows.Count > 0) {
				int num = 1;
				for (int i = 0; i < dt.Rows.Count; i++) {
					if (Convert.ToInt32(dt.Rows[i]["id"]) == cid) {
						rStr.Append(GetPageNameUrl(Convert.ToInt32(dt.Rows[i]["id"])));
						rStr.Append("?IdF=" + dt.Rows[i]["parent_id"] + "&id=" + dt.Rows[i]["id"] + "&mId=" + num);
					}
					num++;
				}
			}
			return rStr.ToString();
		}
		/// <summary>
		/// 加载名称
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetPageName(int id) {
			string rStr = string.Empty;
			BLL.contents.article_category bll = new BLL.contents.article_category();
			Model.contents.article_category model = bll.GetModel(id);
			if (model != null) {
				rStr = model.title;
			}
			return rStr;
		}
		/// <summary>
		/// 加载名称Url
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetPageUrl(int id) {
			string rStr = string.Empty;
			BLL.contents.article_category bll = new BLL.contents.article_category();
			Model.contents.article_category model = bll.GetModel(id);
			if (model != null) {
				rStr = model.link_url;
			}
			return rStr;
		}
		/// <summary>
		/// 加载栏目简介
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetCategoryZhaiYao(int id) {
			string rStr = string.Empty;
			BLL.contents.article_category bll = new BLL.contents.article_category();
			Model.contents.article_category model = bll.GetModel(id);
			if (model != null) {
				rStr = model.content;
			}
			return rStr;
		}
		/// <summary>
		/// 加载名称+Url
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetPageNameUrl(int id) {
			string rStr = string.Empty;
			BLL.contents.article_category bll = new BLL.contents.article_category();
			Model.contents.article_category model = bll.GetModel(id);
			if (model != null) {
				rStr = "<a href=\"" + model.link_url + "\">" + model.title + "</a>";
			}
			return rStr;
		}
		public static string GetArticleNameUrl(int id) {
			string rStr = "";
			BLL.contents.article bll = new BLL.contents.article();
			DataSet ds = bll.GetList(1, " category_id=" + id, "sort_id,add_time");
			if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) {
				rStr += ds.Tables[0].Rows[0]["img_url"].ToString();
			}
			return rStr;
		}
		/// <summary>
		/// 加载摘要
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetPageZhaiYao(int id, int count) {
			string rStr = "";
			BLL.contents.article bll = new BLL.contents.article();
			DataSet ds = bll.GetList(1, " category_id=" + id, "sort_id,add_time");
			if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) {
				if (ds.Tables[0].Rows[0]["zhaiyao"].ToString().Length > count) {
					rStr += rStr.Substring(0, count) + "...";
				}
				else {
					rStr += ds.Tables[0].Rows[0]["zhaiyao"].ToString();
				}
			}
			return rStr;
		}
		/// <summary>
		/// 标题
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetPageTitle(int id) {
			string rStr = "";
			BLL.contents.article bll = new BLL.contents.article();
			DataSet ds = bll.GetList(1, " category_id=" + id, "sort_id,add_time");
			if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) {
				rStr += ds.Tables[0].Rows[0]["title"].ToString();
			}
			return rStr;
		}
		/// <summary>
		/// Img
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetPageImgUrl(int id) {
			string rStr = "";
			BLL.contents.article bll = new BLL.contents.article();
			DataSet ds = bll.GetList(1, " category_id=" + id, "sort_id,add_time");
			if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) {
				rStr += ds.Tables[0].Rows[0]["img_url"].ToString();
			}
			return rStr;
		}
		/// <summary>
		/// 加载广告
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetPageAdv(int id) {
			string rStr = "";
			BLL.contents.article bll = new BLL.contents.article();
			DataSet ds = bll.GetList(1, " category_id=" + id, "sort_id,add_time");
			if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) {
				rStr += " <a href=\"" + ds.Tables[0].Rows[0]["link_url"].ToString() + "\" target=\"_blank\"><img src=\"" + ds.Tables[0].Rows[0]["img_url"].ToString() + "\" width=\"514\" height=\"79\" /></a>";
			}
			return rStr;
		}
		public static string GetPageTime(int id) {
			string rStr = "";
			BLL.contents.article bll = new BLL.contents.article();
			DataSet ds = bll.GetList(1, " category_id=" + id, "sort_id,add_time");
			if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) {
				rStr += ds.Tables[0].Rows[0]["add_time"].ToString();
			}
			return rStr;
		}
		/// <summary>
		/// 获取内容列表
		/// </summary>
		/// <returns></returns>
		public static string GetArticleLst(int top, string strWhere, string order) {
			StringBuilder rStr = new StringBuilder();
			BLL.contents.article bll = new BLL.contents.article();
			DataTable dt = bll.GetList(top, strWhere, order).Tables[0];
			if (dt != null && dt.Rows.Count > 0) {
				int num = 1;
				for (int i = 0; i < dt.Rows.Count; i++) {
					rStr.Append(" <a  target=\"_blank\" href=\"" + dt.Rows[i]["link_url"] + "\">");
					rStr.Append("url(" + dt.Rows[i]["img_url"] + ") top center no-repeat #e1e9eb");
					rStr.Append("</a>");
					num++;
				}
			}
			return rStr.ToString();
		}
		/// <summary>
		/// 返回页面Meta信息
		/// </summary>
		/// <param name="_keywords"></param>
		/// <param name="_description"></param>
		/// <returns></returns>
		public static string MetaInfo(string _keywords, string _description) {
			StringBuilder strTxt = new StringBuilder();
			strTxt.Append("<meta name=\"keywords\" content=\"" + Utils.DropHTML(_keywords, 250).Replace("\"", " ") + "\" />\n");
			strTxt.Append("<meta name=\"description\" content=\"" + Utils.DropHTML(_description, 300).Replace("\"", " ") + "\" />\n");
			return strTxt.ToString();
		}
		#endregion

	}
}
