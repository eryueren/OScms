using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.SessionState;
using OS.Web.UI;
using OS.Common;
using LitJson;

namespace OS.Web.ashx
{
    /// <summary>
    /// AJAX提交处理
    /// </summary>
    public class submit_ajax : IHttpHandler, IRequiresSessionState
    {
        Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig();
        Model.configs.userconfig userConfig = new BLL.configs.userconfig().loadConfig();
        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = OSRequest.GetQueryString("action");

            switch (action)
            {
   
          

            }
        }


        #region 通用外理方法OK=================================
        //校检网站验证码
        private string verify_code(HttpContext context,string strcode)
        {
            if (string.IsNullOrEmpty(strcode))
            {
                return "{\"status\":0, \"msg\":\"对不起，请输入验证码！\"}";
            }
            if (context.Session[OSKeys.SESSION_CODE] == null)
            {
                return "{\"status\":0, \"msg\":\"对不起，验证码超时或已过期！\"}";
            }
            if (strcode.ToLower() != (context.Session[OSKeys.SESSION_CODE].ToString()).ToLower())
            {
                return "{\"status\":0, \"msg\":\"您输入的验证码与系统的不一致！\"}";
            }
            context.Session[OSKeys.SESSION_CODE] = null;
            return "success";
        }
        //校检网站验证码
        private string verify_sms_code(HttpContext context, string strcode)
        {
            if (string.IsNullOrEmpty(strcode))
            {
                return "{\"status\":0, \"msg\":\"对不起，请输入验证码！\"}";
            }
            if (context.Session[OSKeys.SESSION_SMS_CODE] == null)
            {
                return "{\"status\":0, \"msg\":\"对不起，验证码超时或已过期！\"}";
            }
            if (strcode.ToLower() != (context.Session[OSKeys.SESSION_SMS_CODE].ToString()).ToLower())
            {
                return "{\"status\":0, \"msg\":\"您输入的验证码与系统的不一致！\"}";
            }
            context.Session[OSKeys.SESSION_SMS_CODE] = null;
            return "success";
        }
        #endregion END通用方法=================================================

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}