using System;
using System.Collections.Generic;
using System.Text;

namespace OS.Model.configs
{
    public class urls_config
    {
        private string _name;
        private string _path;
        private string _pattern;
        private string _page;
        private string _querystring;

        public urls_config()
        {

        }

        public urls_config(string name, string path, string pattern, string page, string querystring)
        {
            _path = path;
            _name = name;
            _pattern = pattern;
            _page = page;
            _querystring = querystring;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 匹配替换
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// 表达式
        /// </summary>
        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        /// <summary>
        /// 页面地址
        /// </summary>
        public string Page
        {
            get { return _page; }
            set { _page = value; }
        }

        /// <summary>
        /// 参数变量
        /// </summary>
        public string QueryString
        {
            get { return _querystring; }
            set { _querystring = value; }
        }
    }
}
