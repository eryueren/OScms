﻿using System;
using System.Collections.Generic;
using System.Text;
using OS.Common;
using LitJson;

namespace   OS.API.OAuth
{
    public class qq_helper
    {
        public qq_helper()
        { }

        /// <summary>
        /// 取得临时的Access Token
        /// </summary>
        /// <param name="code">临时Authorization Code，官方提示10分钟过期</param>
        /// <param name="state">防止CSRF攻击，成功授权后回调时会原样带回</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_access_token(string code, string state)
        {
            //获得配置信息
            oauth_config config = oauth_helper.get_config("qq");
            string send_url = "https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id=" + config.oauth_app_id + "&client_secret=" + config.oauth_app_key + "&code=" + code + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(config.return_uri);
            //发送并接受返回值
            string result = Utils.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                string[] parm = result.Split('&');
                string access_token = parm[0].Split('=')[1];    //取得access_token
                string expires_in = parm[1].Split('=')[1];      //Access Token的有效期，单位为秒
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("access_token", access_token);
                dic.Add("expires_in", expires_in);
                return dic;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 取得用户openid
        /// </summary>
        /// <param name="access_token">临时的Access Token</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_open_id(string access_token)
        {
            string send_url = "https://graph.qq.com/oauth2.0/me?access_token=" + access_token;
            //发送并接受返回值
            string result = Utils.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            //取得文字出现
            int str_start = result.IndexOf('(') + 1;
            int str_last = result.LastIndexOf(')') - 1;
            //取得JSON字符串
            result = result.Substring(str_start, (str_last - str_start));
            //反序列化JSON
            Dictionary<string, object> dic = JsonMapper.ToObject<Dictionary<string, object>>(result);
            return dic;
        }

        /// <summary>
        /// 获取登录用户自己的基本资料
        /// </summary>
        /// <param name="access_token">临时的Access Token</param>
        /// <param name="open_id">用户openid</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_user_info(string access_token, string open_id)
        {
            //获得配置信息
            oauth_config config = oauth_helper.get_config("qq");
            string send_url = "https://graph.qq.com/user/get_user_info?access_token=" + access_token + "&oauth_consumer_key=" + config.oauth_app_id + "&openid=" + open_id;
            //发送并接受返回值
            string result = Utils.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            //反序列化JSON
            Dictionary<string, object> dic = JsonMapper.ToObject<Dictionary<string, object>>(result);
            return dic;
        }

        /// <summary>
        /// 获取登录用户自己的详细信息
        /// </summary>
        /// <param name="access_token">临时的Access Token</param>
        /// <param name="open_id">用户openid</param>
        /// <returns>Dictionary</returns>
        public static JsonData get_info(string access_token, string open_id)
        {
            //获得配置信息
            oauth_config config = oauth_helper.get_config("qq");
            string send_url = "https://graph.qq.com/user/get_info?access_token=" + access_token + "&oauth_consumer_key=" + config.oauth_app_id + "&openid=" + open_id;
            //发送并接受返回值
            string result = Utils.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                JsonData jd = JsonMapper.ToObject(result);
                if (jd.Count > 0)
                {
                    return jd;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

    }
}
