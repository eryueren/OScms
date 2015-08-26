using System;
using System.Collections.Generic;
using System.Text;

namespace  OS.Web.UI {
	using System.Text.RegularExpressions;
	using System.Web;
	public class HtmlSafe {

		#region 执行安全检查
		/// <summary>  
		/// 执行安全检查  
		/// </summary>  
		public static void Procress() {
			const string errmsg =
				"<div style='position:fixed;top:0px;width:100%;height:100%;background-color:white;color:green;font-weight:bold;border-bottom:5px solid #999;'><br>您的提交带有不合法参数,谢谢合作!</div>";

			if (RawUrl()) {
				HttpContext.Current.Response.Write(errmsg);
				HttpContext.Current.Response.End();
			}

			if (CookieData()) {
				HttpContext.Current.Response.Write(errmsg);
				HttpContext.Current.Response.End();
			}

			if (HttpContext.Current.Request.UrlReferrer != null) {
				if (Referer()) {
					HttpContext.Current.Response.Write(errmsg);
					HttpContext.Current.Response.End();
				}
			}

			if (HttpContext.Current.Request.RequestType.ToUpper() == "POST") {
				if (PostData()) {
					HttpContext.Current.Response.Write(errmsg);
					HttpContext.Current.Response.End();
				}
			}
			if (HttpContext.Current.Request.RequestType.ToUpper() == "GET") {
				if (GetData()) {
					HttpContext.Current.Response.Write(errmsg);
					HttpContext.Current.Response.End();
				}
			}
		}

		#endregion

		#region 安全检查正则

		/// <summary>  
		/// 安全检查正则  
		/// </summary>  
		private const string StrRegex =
			@"<[^>]+?style=[\w]+?:expression\(|\b(alert|confirm|prompt)\b|^\+/v(8|9)|<[^>]*?=[^>]*?&#[^>]*?>|\b(and|or)\b.{1,6}?(=|>|<|\bin\b|\blike\b)|/\*.+?\*/|<\s*script\b|<\s*img\b|\bEXEC\b|UNION.+?SELECT|UPDATE.+?SET|INSERT\s+INTO.+?VALUES|(SELECT|DELETE).+?FROM|(CREATE|ALTER|DROP|TRUNCATE)\s+(TABLE|DATABASE)";

		#endregion

		#region 检查Post数据

		/// <summary>  
		/// 检查Post数据  
		/// </summary>  
		/// <returns></returns>  
		private static bool PostData() {
			bool result = false;

			for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++) {
				result = CheckData(HttpContext.Current.Request.Form[i]);
				if (result) {
					break;
				}
			}
			return result;
		}

		#endregion

		#region 检查Get数据

		/// <summary>  
		/// 检查Get数据  
		/// </summary>  
		/// <returns></returns>  
		private static bool GetData() {
			bool result = false;

			for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++) {
				result = CheckData(HttpContext.Current.Request.QueryString[i]);
				if (result) {
					break;
				}
			}
			return result;
		}

		#endregion

		#region 检查Cookie数据

		/// <summary>  
		/// 检查Cookie数据  
		/// </summary>  
		/// <returns></returns>  
		private static bool CookieData() {
			bool result = false;
			for (int i = 0; i < HttpContext.Current.Request.Cookies.Count; i++) {
				result = CheckData(HttpContext.Current.Request.Cookies[i].Value.ToLower());
				if (result) {
					break;
				}
			}
			return result;
		}

		#endregion

		#region 检查Referer

		/// <summary>  
		/// 检查Referer  
		/// </summary>  
		/// <returns></returns>  
		private static bool Referer() {
			return CheckData(HttpContext.Current.Request.UrlReferrer.ToString());
		}

		#endregion

		#region 检查当前请求路径

		/// <summary>  
		/// 检查当前请求路径  
		/// </summary>  
		/// <returns></returns>  
		private static bool RawUrl() {
			return CheckData(HttpContext.Current.Request.RawUrl);
		}

		#endregion

		#region 正则匹配

		/// <summary>  
		/// 正则匹配  
		/// </summary>  
		/// <param name="inputData"></param>  
		/// <returns></returns>  
		private static bool CheckData(string inputData) {
			return Regex.IsMatch(inputData, StrRegex);
		}

		#endregion
	}
}
