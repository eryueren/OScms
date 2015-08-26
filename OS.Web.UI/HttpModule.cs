using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Configuration;
using System.Xml;
using  OS.Common;

namespace  OS.Web.UI
{
    /// <summary>
    /// DTCMS的HttpModule类
    /// </summary>
    public class HttpModule : System.Web.IHttpModule
    {
        /// <summary>
        /// 实现接口的Init方法
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(KillSqlFilter);
            context.BeginRequest += new EventHandler(ReUrl_BeginRequest);
        }

        /// <summary>
        /// 实现接口的Dispose方法
        /// </summary>
        public void Dispose()
        {

        }

        #region 过滤SQL注入危险字符
        /// <summary>
        /// 过滤SQL注入危险字符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KillSqlFilter(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;
          //  Model.WebSet webConfig = new BLL.WebSet().loadConfig(Utils.GetXmlMapPath("Configpath"));
            Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig(); //获得站点配置信息
            string killSqlFilter = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|cmd";

            //遍历参数，管理目录和隐藏域除外
            if (context.Request.Url.ToString().ToLower().IndexOf(siteConfig.webmanagepath.ToLower()) < 0)
            {
                //遍历Post参数
                foreach (string i in context.Request.Form)
                {
                    if (i == "__VIEWSTATE") continue;
                    if (Common.Utils.SqlFilter(killSqlFilter, context.Request.Form[i].ToString()))
                    {
                        context.Response.Write("<script>window.alert('您提交的参数中含有非法字符!');history.back();" + " </" + "script>");
                    }

                }
                //遍历Get参数。
                foreach (string i in context.Request.QueryString)
                {
                    if (Common.Utils.SqlFilter(killSqlFilter, context.Request.QueryString[i].ToString()))
                    {
                        context.Response.Write("<script>window.alert('您提交的参数中含有非法字符!');history.back();" + " </" + "script>");
                    }
                }
            }
        }
        #endregion

        #region 重写Url
        /// <summary>
        /// 重写Url
        /// </summary>
        /// <param name="sender">事件的源</param>
        /// <param name="e">包含事件数据的 EventArgs</param>
        private void ReUrl_BeginRequest(object sender, EventArgs e)
        {
            string aspxPath = "Aspx"; //站点aspx文件目录
            HttpContext context = ((HttpApplication)sender).Context;
            string requestPath = context.Request.Path.ToLower();

            Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig(); //获得站点配置信息
            string patternPath = siteConfig.webpath;
            //当使用伪地址时
            if (siteConfig.staticstatus == 1)
            {            //遍历URL字典
                foreach (Model.configs.url_rewrite model in SiteUrls.GetSiteUrls().Urls)
                {
                   //遍历URL字典的子节点
                    foreach (Model.configs.url_rewrite_item item in model.url_rewrite_items)
                    {
                        string newPattern = Utils.GetUrlExtension(item.pattern, siteConfig.staticextension); //替换扩展名
                        //如果与URL节点匹配则重写
                        if (Regex.IsMatch(requestPath, string.Format("^{0}{1}$", patternPath, newPattern), RegexOptions.None | RegexOptions.IgnoreCase))
                        {
                            string queryString = Regex.Replace(requestPath, string.Format("{0}{1}", patternPath, newPattern), item.querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                            context.RewritePath(string.Format("{0}{1}/{2}", siteConfig.webpath, YLKeys.DIRECTORY_REWRITE_ASPX, model.page), string.Empty, queryString);
                            return;
                        }

                    }
                }
            }
            else if (siteConfig.staticstatus == 2)
            { 
                         //遍历URL字典
                foreach (Model.configs.url_rewrite model in SiteUrls.GetSiteUrls().Urls)
                {
                   //遍历URL字典的子节点
                    foreach (Model.configs.url_rewrite_item item in model.url_rewrite_items)
                    {
                        string newPattern = Utils.GetUrlExtension(item.pattern, siteConfig.staticextension); //替换扩展名
                        //如果与URL节点匹配则重写
                        if (Regex.IsMatch(requestPath, string.Format("^{0}{1}$", patternPath, newPattern), RegexOptions.None | RegexOptions.IgnoreCase))
                        {
                            string queryString = Regex.Replace(requestPath, string.Format("{0}{1}", patternPath, newPattern), item.querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                            context.RewritePath(string.Format("{0}{1}/{2}", siteConfig.webpath, "html", model.page), string.Empty, queryString);
                            return;
                        }

                    }
                }
            }

            //以下为兼容net4.0
            bool isAspxFile = false;
            if (requestPath.LastIndexOf(".") >= 0)
            {
                if (requestPath.Substring(requestPath.LastIndexOf(".")) == ".aspx")
                {
                    isAspxFile = true;
                }
            }
            //以下重写URL
            if (!requestPath.StartsWith(siteConfig.webpath + "ashx/") && !requestPath.StartsWith(siteConfig.webpath + siteConfig.webmanagepath.ToLower() + "/") && isAspxFile)
            {
                context.RewritePath(siteConfig.webpath + aspxPath + requestPath);
                return;
            }
        }
        #endregion

    }



    #region 站点URL字典信息类===================================================
    /// <summary>
    /// 站点伪Url信息类
    /// </summary>
    public class SiteUrls
    {
        //属性声明
        private static object lockHelper = new object();
        private static volatile SiteUrls instance = null;
        private ArrayList _urls;
        public ArrayList Urls
        {
            get { return _urls; }
            set { _urls = value; }
        }
        //构造函数
        private SiteUrls()
        {
            Urls = new ArrayList();
            BLL.configs.url_rewrite bll = new BLL.configs.url_rewrite();
            List<Model.configs.url_rewrite> ls = bll.GetList("");
            foreach (Model.configs.url_rewrite model in ls)
            {
                foreach (Model.configs.url_rewrite_item item in model.url_rewrite_items)
                {
                    item.querystring = item.querystring.Replace("^", "&");
                }
                Urls.Add(model);
            }
        }
        //返回URL字典
        public static SiteUrls GetSiteUrls()
        {
            SiteUrls _cache = CacheHelper.Get<SiteUrls>(YLKeys.CACHE_SITE_HTTP_MODULE);
            lock (lockHelper)
            {
                if (_cache == null)
                {
                    CacheHelper.Insert(YLKeys.CACHE_SITE_HTTP_MODULE, new SiteUrls(), Utils.GetXmlMapPath(YLKeys.FILE_URL_XML_CONFING));
                    instance = CacheHelper.Get<SiteUrls>(YLKeys.CACHE_SITE_HTTP_MODULE);
                }
            }
            return instance;
        }

    }
    #endregion

    //////////////////////////////////////////////////////////////////////
    
    //#region 站点伪Url信息类
    ///// <summary>
    ///// 站点伪Url信息类
    ///// </summary>
    //public class SiteUrls
    //{
    //    #region 内部属性和方法
    //    private static object lockHelper = new object();
    //    private static volatile SiteUrls instance = null;

    //    string SiteUrlsFile = Utils.GetXmlMapPath("Urlspath");
    //    private System.Collections.ArrayList _Urls;
    //    public System.Collections.ArrayList Urls
    //    {
    //        get { return _Urls; }
    //        set { _Urls = value; }
    //    }

    //    private System.Collections.Specialized.NameValueCollection _Paths;
    //    public System.Collections.Specialized.NameValueCollection Paths
    //    {
    //        get { return _Paths; }
    //        set { _Paths = value; }
    //    }

    //    private SiteUrls()
    //    {
    //        Urls = new System.Collections.ArrayList();
    //        Paths = new System.Collections.Specialized.NameValueCollection();

    //        XmlDocument xml = new XmlDocument();

    //        xml.Load(SiteUrlsFile);

    //        XmlNode root = xml.SelectSingleNode("urls");
    //        foreach (XmlNode n in root.ChildNodes)
    //        {
    //            if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "rewrite")
    //            {
    //                XmlAttribute name = n.Attributes["name"];
    //                XmlAttribute path = n.Attributes["path"];
    //                XmlAttribute page = n.Attributes["page"];
    //                XmlAttribute querystring = n.Attributes["querystrings"];
    //                XmlAttribute pattern = n.Attributes["pattern"];

    //                if (name != null && path != null && page != null && querystring != null && pattern != null)
    //                {
    //                    Paths.Add(name.Value, path.Value);
    //                    Urls.Add(new URLRewrite(name.Value, pattern.Value, page.Value.Replace("^", "&"), querystring.Value.Replace("^", "&")));
    //                }
    //            }
    //        }
    //    }
    //    #endregion

    //    public static SiteUrls GetSiteUrls()
    //    {
    //        if (instance == null)
    //        {
    //            lock (lockHelper)
    //            {
    //                if (instance == null)
    //                {
    //                    instance = new SiteUrls();
    //                }
    //            }
    //        }
    //        return instance;

    //    }

    //    public static void SetInstance(SiteUrls anInstance)
    //    {
    //        if (anInstance != null)
    //            instance = anInstance;
    //    }

    //    public static void SetInstance()
    //    {
    //        SetInstance(new SiteUrls());
    //    }


    //    /// <summary>
    //    /// 重写伪地址
    //    /// </summary>
    //    public class URLRewrite
    //    {
    //        #region 成员变量
    //        private string _Name;
    //        public string Name
    //        {
    //            get { return _Name; }
    //            set { _Name = value; }
    //        }

    //        private string _Pattern;
    //        public string Pattern
    //        {
    //            get { return _Pattern; }
    //            set { _Pattern = value; }
    //        }

    //        private string _Page;
    //        public string Page
    //        {
    //            get { return _Page; }
    //            set { _Page = value; }
    //        }

    //        private string _QueryString;
    //        public string QueryString
    //        {
    //            get { return _QueryString; }
    //            set { _QueryString = value; }
    //        }
    //        #endregion

    //        #region 构造函数
    //        public URLRewrite(string name, string pattern, string page, string querystring)
    //        {
    //            _Name = name;
    //            _Pattern = pattern;
    //            _Page = page;
    //            _QueryString = querystring;
    //        }
    //        #endregion
    //    }
    //}
    //#endregion

}