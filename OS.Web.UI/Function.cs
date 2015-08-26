using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using  OS.Common;

namespace  OS.Web.UI {
	public class Function {

		// 获得前几行数据
		public static DataRowCollection GetList(int Top, string strWhere) {
			return new BLL.channels.article().GetList(Top, strWhere).Tables[0].Rows;
		}
		// 获得前几行数据
		public static DataRowCollection GetList(string caregory_name, int Top, string strWhere) {
			return new BLL.channels.article().GetList(caregory_name, Top, strWhere).Tables[0].Rows;
		}

		//拓展字段信息
		public static string Files(int _id, string _file) {
			string f = "";
			try {
				Model.contents.article model = new BLL.contents.article().GetModel(_id);

				if (model != null) {
					f = model.fields[_file];
				}
			} catch {
				f = "";
			}
			return f;
		}

		//栏目列表
		public static DataRowCollection GetChannel(string where) {
			return new BasePage().GetChannel(where).Rows;
		}
		//判断是否有子栏目
		public static int ParentID(int _category_id) {
			return new BasePage().ParentID(_category_id);
		}

		//判断是否有子栏目
		public static bool ChildChannel(string where) {
			return new BasePage().ChildChannel(where);
		}

		//得到栏目标题
		public static string GetChannelTitle(int _category_id) {
			return new BLL.contents.article_category().GetTitle(_category_id);
		}

		//栏目样式调用
		public static bool Style(int category_id1, int category_id2) {
			return new BasePage().Style(category_id1, category_id2);
		}

		//栏目URL
		public static string Url(string _name) {
			return new BasePage().linkurl(_name);
		}
		//栏目URL
		public static string Url(int category_id) {
			return new BasePage().Url(category_id);
		}
		//栏目URL
		public static string Url(string _key, int _id) {
			return new BasePage().Url(_key, _id);
		}

		//文字截取
		public static string SubString(string str, int _len) {
			return Utils.CutString(str, _len);
		}
		//文字截取
		public static string DropHTML(string str, int _len) {
			return Utils.DropHTML(str, _len);
		}

		public static string FormatTime(object obj1) {
			return Convert.ToDateTime(obj1).ToString("yyyy-MM-dd");
		}

		//返回类别导航面包屑
		public static string ChannelMenu(int category_id, string _str) {
			return new BasePage().ChannelMenu(category_id, _str);
		}
		// 页面seo信息
		public static string seo(string item) {
			Model.configs.siteconfig config = new BLL.configs.siteconfig().loadConfig();
			string str = "";
			switch (item) {
				case "title":
					str = config.webname;
					break;
				case "keyword":
					str = config.webkeyword;
					break;
				case "description":
					str = config.webdescription;
					break;
				case "copyright":
					str = config.webcopyright;
					break;
				case "code":
					str = config.webcrod;
					break;
				case "fax":
					str = config.webfax;
					break;
				case "mail":
					str = config.webmail;
					break;
				case "logo":
					str = config.weblogo;
					break;
				case "tel":
					str = config.webtel;
					break;
				case "http":
					str = config.weburl;
					break;
				case "countcode":
					str = config.webcountcode;
					break;
				case "address":
					str = config.webaddress;
					break;

			}
			return str;
		}

	}
}
