using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using  OS.DBUtility;
using  OS.Common;

namespace  OS.DAL.channels
{
    /// <summary>
    /// 数据访问类:article
    /// </summary>
    public partial class article
    {
        private string databaseprefix; //数据库表名前缀
        public article(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 前台模板用到的方法===================================


        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(string channel_name,int Top, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,channel_id,category_id,call_index,title,link_url,img_url,seo_title,seo_keywords,seo_description,zhaiyao,content,sort_id,click,status,groupids_view,vote_id,is_top,is_red,is_hot,is_slide,is_sys,is_msg,user_name,add_time,update_time ");
            strSql.Append(" FROM " + databaseprefix + "article ");
            if (channel_name.Trim() != "")
            {
                string sql1 = Get_category_id(channel_name) != 0 ? "category_id=" + Get_category_id(channel_name) : "category_id=-100";
                string sql2 = !string.IsNullOrEmpty(strWhere) ? " and " + strWhere : "";

                strSql.Append(" where " + sql1 + sql2);
            }
            strSql.Append(" order by sort_id asc,add_time desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,channel_id,category_id,call_index,title,link_url,img_url,seo_title,seo_keywords,seo_description,zhaiyao,content,sort_id,click,status,groupids_view,vote_id,is_top,is_red,is_hot,is_slide,is_sys,is_msg,user_name,add_time,update_time ");
            strSql.Append(" FROM " + databaseprefix + "article ");
            if (strWhere.Trim() != "")
            {

                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by sort_id asc,add_time desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 返回类别名称ID
        /// </summary>
        public int Get_category_id(string channel_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "article_category");
            strSql.Append(" where call_index='" + channel_name.Trim() + "'");
            string id = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(id))
            {
                return 0;
            }
            return int.Parse(id);
        }


        /// <summary>
        /// 分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "article");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

    }
}