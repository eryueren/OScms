using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using  OS.Common;



using System.IO;


namespace  OS.Web.UI
{
    public class ManagePage : System.Web.UI.Page
    {
        protected internal Model.configs.siteconfig siteConfig;

        public ManagePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
            siteConfig = new BLL.configs.siteconfig().loadConfig();
        }

        private void ManagePage_Load(object sender, EventArgs e)
        {
            //判断管理员是否登录
            if (!IsAdminLogin())
            {
                Response.Write("<script>parent.location.href='" + siteConfig.webpath + siteConfig.webmanagepath + "/login.aspx'</script>");
                Response.End();
            }
        }

        #region 管理员============================================
        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsAdminLogin()
        {
            //如果Session为Null
            if (Session[YLKeys.SESSION_ADMIN_INFO] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string adminname = Utils.GetCookie("AdminName", "OS");
                string adminpwd = Utils.GetCookie("AdminPwd", "OS");
                if (adminname != "" && adminpwd != "")
                {
                    BLL.managers.manager bll = new BLL.managers.manager();
                    Model.managers.manager model = bll.GetModel(adminname, adminpwd);
                    if (model != null)
                    {
                        Session[YLKeys.SESSION_ADMIN_INFO] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得管理员信息
        /// </summary>
        public Model.managers.manager GetAdminInfo()
        {
            if (IsAdminLogin())
            {
                Model.managers.manager model = Session[YLKeys.SESSION_ADMIN_INFO] as Model.managers.manager;
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="nav_name">菜单名称</param>
        /// <param name="action_type">操作类型</param>
        public void ChkAdminLevel(string nav_name, string action_type)
        {
            Model.managers.manager model = GetAdminInfo();
            BLL.managers.manager_role bll = new BLL.managers.manager_role();
            bool result = bll.Exists(model.role_id, nav_name, action_type);
            if (!result)
            {

             //   string msgbox = "parent.jsdialog(\"错误提示\", \"您没有管理该页面的权限，请勿非法进入！\", \"back\", \"Error\")";
                PageErrorMsg("您没有管理该页面的权限，请勿非法进入！");
              //  Response.Write("<script type=\"text/javascript\">" + msgbox + "</script>");
             //   Response.End();
            }
        }

        /// <summary>
        /// 写入管理日志
        /// </summary>
        /// <param name="action_type"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool AddAdminLog(string action_type, string remark)
        {
            if (siteConfig.logstatus > 0)
            {
                Model.managers.manager model = GetAdminInfo();
                int newId = new BLL.managers.manager_log().Add(model.id, model.user_name, action_type, remark);
                if (newId > 0)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region JS提示============================================
        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        #endregion

        #region  提示

        public static void PageSuccessMsg(string Msg, string Url, string backUrl)
        {
            PageRender(Msg, Url, "Success", backUrl);
        }
        public static void PageErrorMsg(string Msg)
        {
            PageRender(Msg, "", "Error", "");
        }
        static internal void PageRender(string Msg, string Url, string SuccessOrError, string backUrl)
        {
            string STitle = "操作提示";
            string ShowInfo = "";
            string _tmp = "";
            string _msg = "";

            if (SuccessOrError == "Success")
            {
                ShowInfo = "<font color=\"green\">正确提示</font>";
                _tmp = "<img src=\"../images/icon-01.gif\" border=\"0\">";
                _msg = "<font color=\"green\">" + Msg + "</font>";
            }
            else
            {
                ShowInfo = "<font color=\"red\">错误提示</font>";
                _tmp = "<img src=\"../images/icon-02.gif\" border=\"0\">";
                _msg = "<font color=\"red\">" + Msg + "</font>";
            }
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r");
            System.Web.HttpContext.Current.Response.Write("<head>\r");
            System.Web.HttpContext.Current.Response.Write("<title>" + STitle + "</title>\r");
            System.Web.HttpContext.Current.Response.Write("<style>\r");
            System.Web.HttpContext.Current.Response.Write(" .list_link{font-size:16px;}\r");
            System.Web.HttpContext.Current.Response.Write(" .list_link ul{font-size:14px;}\r");
            System.Web.HttpContext.Current.Response.Write(" .list_link ul li{line-height:1.7;}\r");
            System.Web.HttpContext.Current.Response.Write(" .list_link ul li a{ text-decoration:none;color:#666;}\r");
            System.Web.HttpContext.Current.Response.Write(" .list_link ul li a:hover{ text-decoration:none;color:red;}\r");
            System.Web.HttpContext.Current.Response.Write(" .list_link ul li span{word-wrap: bread-word; word-break: break-all; font-size: 14px;}\r");
            System.Web.HttpContext.Current.Response.Write("</style>\r");
            System.Web.HttpContext.Current.Response.Write("</head>\r");
            System.Web.HttpContext.Current.Response.Write("<body style=\"margin-top: 50px; font-size: 12px;\"> \r");
            System.Web.HttpContext.Current.Response.Write("<table border=\"0\" align=\"center\" cellspacing=\"1\" cellpadding=\"5\">\r");
            System.Web.HttpContext.Current.Response.Write(" <tr>\r");
            System.Web.HttpContext.Current.Response.Write(" <td class=\"list_link\" align=\"center\">" + _tmp + "</td>\r");
            System.Web.HttpContext.Current.Response.Write(" <td class=\"list_link\">\r");
            System.Web.HttpContext.Current.Response.Write(" <strong>操作描述：</strong>" + ShowInfo + "\r");
            System.Web.HttpContext.Current.Response.Write(" <ul>\r");
            System.Web.HttpContext.Current.Response.Write(" <li><span>" + _msg + "</span></li>\r");

            if (SuccessOrError == "Success")
            {
                if (!string.IsNullOrEmpty(Url))
                {
                    System.Web.HttpContext.Current.Response.Write(" <li><a href=\"" + Url + "\">继续添加</a></li> \r");
                }
                if (!string.IsNullOrEmpty(backUrl))
                {
                    System.Web.HttpContext.Current.Response.Write(" <li><a href=\"" + backUrl + "\">返回管理</a></li> \r");
                }
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write(" <li><a href='javascript: history.go(-1);' target='_top'>返回上一级</a></li>\r");
            }

            System.Web.HttpContext.Current.Response.Write(" </ul>\r");
            System.Web.HttpContext.Current.Response.Write(" </td>\r");
            System.Web.HttpContext.Current.Response.Write(" </tr>\r");
            System.Web.HttpContext.Current.Response.Write(" </table>\r");
            System.Web.HttpContext.Current.Response.Write("</body>\r");
            System.Web.HttpContext.Current.Response.Write("</html>\r");
            System.Web.HttpContext.Current.Response.End();
        }

        #endregion


                #region 生成静态方法
        /// <summary>
        /// 生成静态文件方法
        /// </summary>

        public static void CreateIndexHtml(string aspxPath, string htmlPath)
        {
            Model.configs.siteconfig config = new BLL.configs.siteconfig().loadConfig();//站点配置
            if (File.Exists(Utils.GetMapPath(config.webpath +"aspx"+ aspxPath.Substring(0, aspxPath.IndexOf(".aspx") + 5))))
            {
                string linkwebsite = HttpContext.Current.Request.Url.Authority;
                System.Net.WebRequest request = System.Net.WebRequest.Create("http://" + linkwebsite + aspxPath);
                System.Net.WebResponse response = request.GetResponse();
                System.IO.Stream stream = response.GetResponseStream();
                System.IO.StreamReader streamreader = new System.IO.StreamReader(stream, System.Text.Encoding.GetEncoding("utf-8"));
                string content = streamreader.ReadToEnd();
                using (StreamWriter sw = new StreamWriter(Utils.GetMapPath(htmlPath), false, Encoding.UTF8))
                {
                    sw.WriteLine(content);
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                HttpContext.Current.Response.Write("1");//找不到生成的模版！
            }
        }



        #endregion

        protected static string page_menu(int category_id)
        {
            string menu = "";
            string class_list = new BLL.contents.article_category().GetModel(category_id).class_list.Trim(',');
            string[] arr = class_list.Split(',');
            int loop = 0;
            if (arr.Length > 0)
            {
                foreach (string i in arr)
                {
                    loop++;
                    string style = loop == arr.Length ? "" : " <i class=\"arrow\"></i> ";
                    menu += new BLL.contents.article_category().GetTitle(Convert.ToInt32(i)) + style;
                }
            }
            return menu.ToString();
        }

        protected static string column_menu(int category_id)
        {
            string menu = "";
            string class_list = new BLL.contents.article_category().GetModel(category_id).class_list.Trim(',');
            string[] arr = class_list.Split(',');
            if (arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    string style = arr.Length == (i + 1) ? "" : "  &gt;  ";
                    menu += new BLL.contents.article_category().GetTitle(Convert.ToInt32(arr[i])) + style;
                }
            }
            return menu.ToString();
        }

    }
}
