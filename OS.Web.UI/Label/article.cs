using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using  OS.Common;

namespace  OS.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        #region 列表标签======================================

        /// <summary>
        /// 文章分页列表
        public DataTable get_article_list(string channel_name, int page_index, string strwhere, out int totalcount, out string pagelist, string _key, params object[] _params)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                int pagesize = new BLL.channels.category().GetPageSize(channel_name); //自动获得频道分页数量
                dt = new BLL.channels.article().GetList(pagesize, page_index, strwhere, "sort_id asc,add_time desc", out totalcount).Tables[0];
                pagelist = Utils.OutPageList(pagesize, page_index, totalcount, linkurl(_key, _params), 8);
            }
            else
            {
                totalcount = 0;
                pagelist = "";
            }
            return dt;
        }

        /// <summary>
        /// 栏目列表
        public DataTable GetChannel(string strwhere)
        {
            DataTable  dt = new BLL.channels.category().GetList(0, strwhere, "sort_id asc,id desc ").Tables[0];//一级
            return dt;
        }
        /// <summary>
        /// 栏目列表
        public bool ChildChannel(string strwhere)
        {
            bool result = false;
            DataTable dt = new BLL.channels.category().GetList(0, strwhere, "sort_id asc,id desc ").Tables[0];//一级
            if (dt.Rows.Count > 0 && dt.Rows != null)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        //栏目样式调用
        public bool Style(int category_id1,int category_id2)
        {
            bool result = false;
            BLL.contents.article_category bll = new BLL.contents.article_category();
            DataTable dt = bll.GetList(0, "id="+category_id2, "sort_id asc,id desc").Tables[0];
            if (dt.Rows.Count > 0 && dt.Rows != null)
            {
                string  _class_list = dt.Rows[0]["class_list"].ToString();
                if(_class_list.IndexOf(","+category_id1+",")>0)
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 栏目URL
        public string Url(int category_id)
        {
            string _url = string.Empty;
            BLL.contents.article_category bll = new BLL.contents.article_category();
            DataTable dt = bll.GetList(0, "class_list like '%," + category_id + ",%'", "class_layer desc, sort_id asc,id desc").Tables[0];
            if (dt.Rows.Count > 0 && dt.Rows != null)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["link_url"].ToString()))
                {
                    _url = dt.Rows[0]["link_url"].ToString();
                }
                else
                {
                    if (bll.GetModel(category_id).model_id != 0)
                    {
                        string _key1 = BasePage.pageUrl(bll.GetModel(category_id).model_id);
                        _url = new BasePage().linkurl(_key1, category_id);
                    }
                    else
                    {
                        string _key = BasePage.pageUrl(Convert.ToInt32(dt.Rows[0]["model_id"]));
                        string _category_id = dt.Rows[0]["id"].ToString();
                        _url = new BasePage().linkurl(_key, _category_id);
                    }
                }
            }
            return _url.ToString();
        }

        /// <summary>
        /// 栏目URL
        public string Url(string _key,int _id)
        {
            string _url = string.Empty;
            Model.contents.article model = new BLL.contents.article().GetModel(_id);
            if (model != null)
            {
                _url = new BasePage().linkurl(_key, model.category_id,_id);
            }
            return _url.ToString();
        }
        /// <summary>
        /// 返回面包屑
        public string ChannelMenu(int category_id,string _str)
        {
            StringBuilder sb = new StringBuilder();
            BLL.contents.article_category bll = new BLL.contents.article_category();
            DataTable dt = bll.GetList(0, "id=" + category_id, "sort_id asc,id desc").Tables[0];
            if (dt.Rows.Count > 0 && dt.Rows != null)
            {
                string _class_list = dt.Rows[0]["class_list"].ToString();
                string[] arr = _class_list.Replace(",1,", "").Trim(',').ToString().Split(',');
                if (arr.Length > 0)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        sb.Append("" + _str + "<a href=\"" + Url(Convert.ToInt32(arr[i])) + "\">" + bll.GetTitle(Convert.ToInt32(arr[i])) + "</a>");
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 第一级栏目ID
        public int ParentID(int category_id)
        {
            int _pid = 0;
            BLL.contents.article_category bll = new BLL.contents.article_category();
            DataTable dt = bll.GetList(0, "class_list like '%," + category_id + ",%'", "class_layer asc, sort_id asc,id desc").Tables[0];
            if (dt.Rows.Count > 0 && dt.Rows != null)
            {
                string _class_list = dt.Rows[0]["class_list"].ToString();
                string[] arr = _class_list.Replace(",1,", "").Trim(',').ToString().Split(',');
                if (arr.Length > 0)
                {
                    _pid = Convert.ToInt32(arr[0]);
                }
            }
            return _pid;
        }

        #endregion


    }
}
