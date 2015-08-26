using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using  OS.DBUtility;
using  OS.Common;
using System.Collections.Generic;

namespace  OS.DAL.channels
{
    /// <summary>
    /// 数据访问类:内容类别
    /// </summary>
    public partial class category
    {
        private string databaseprefix; //数据库表名前缀
        public category(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法========================================

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "article_category");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }


        /// <summary>
        /// 获取分页大小
        /// </summary>
        public int GetPageSize(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 page_size FROM " + databaseprefix + "article_category");
            strSql.Append(" where call_index=@call_index");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.VarChar,50)};
            parameters[0].Value = call_index;

            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_category set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public int Get_category_id(string  channel_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "article_category");
            strSql.Append(" where call_index='" + channel_name + "'");
            string id = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(id))
            {
                return 0;
            }
            return int.Parse(id);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,model_id,is_add_category,is_add_content,is_show_top,is_show_foot,is_albums,is_attach,page_size,link_url,img_url,content,seo_title,seo_keywords,seo_description,nav_type,sub_title,action_type,is_sys,is_lock ");
            strSql.Append(" from " + databaseprefix + "article_category ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where parent_id=1 and channel_id>0  and nav_type='WebSite' and " + strWhere);
            }
            else
            {
                strSql.Append(" where parent_id=1 and channel_id>0  and nav_type='WebSite'");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.contents.article_category GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,model_id,is_add_category,is_add_content,is_show_top,is_show_foot,is_albums,is_attach,page_size,link_url,img_url,content,seo_title,seo_keywords,seo_description,nav_type,sub_title,action_type,is_sys,is_lock from " + databaseprefix + "article_category ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.contents.article_category model = new Model.contents.article_category();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["channel_id"] != null && ds.Tables[0].Rows[0]["channel_id"].ToString() != "")
                {
                    model.channel_id = int.Parse(ds.Tables[0].Rows[0]["channel_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["call_index"] != null && ds.Tables[0].Rows[0]["call_index"].ToString() != "")
                {
                    model.call_index = ds.Tables[0].Rows[0]["call_index"].ToString();
                }
                if (ds.Tables[0].Rows[0]["parent_id"] != null && ds.Tables[0].Rows[0]["parent_id"].ToString() != "")
                {
                    model.parent_id = int.Parse(ds.Tables[0].Rows[0]["parent_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["class_list"] != null && ds.Tables[0].Rows[0]["class_list"].ToString() != "")
                {
                    model.class_list = ds.Tables[0].Rows[0]["class_list"].ToString();
                }
                if (ds.Tables[0].Rows[0]["class_layer"] != null && ds.Tables[0].Rows[0]["class_layer"].ToString() != "")
                {
                    model.class_layer = int.Parse(ds.Tables[0].Rows[0]["class_layer"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["model_id"] != null && ds.Tables[0].Rows[0]["model_id"].ToString() != "")
                {
                    model.model_id = int.Parse(ds.Tables[0].Rows[0]["model_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_add_category"] != null && ds.Tables[0].Rows[0]["is_add_category"].ToString() != "")
                {
                    model.is_add_category = int.Parse(ds.Tables[0].Rows[0]["is_add_category"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_add_content"] != null && ds.Tables[0].Rows[0]["is_add_content"].ToString() != "")
                {
                    model.is_add_content = int.Parse(ds.Tables[0].Rows[0]["is_add_content"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_show_top"] != null && ds.Tables[0].Rows[0]["is_show_top"].ToString() != "")
                {
                    model.is_show_top = int.Parse(ds.Tables[0].Rows[0]["is_show_top"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_show_foot"] != null && ds.Tables[0].Rows[0]["is_show_foot"].ToString() != "")
                {
                    model.is_show_foot = int.Parse(ds.Tables[0].Rows[0]["is_show_foot"].ToString());
                }

                if (ds.Tables[0].Rows[0]["is_albums"].ToString() != "")
                {
                    model.is_albums = int.Parse(ds.Tables[0].Rows[0]["is_albums"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_attach"].ToString() != "")
                {
                    model.is_attach = int.Parse(ds.Tables[0].Rows[0]["is_attach"].ToString());
                }
                if (ds.Tables[0].Rows[0]["page_size"].ToString() != "")
                {
                    model.page_size = int.Parse(ds.Tables[0].Rows[0]["page_size"].ToString());
                }

                if (ds.Tables[0].Rows[0]["link_url"] != null && ds.Tables[0].Rows[0]["link_url"].ToString() != "")
                {
                    model.link_url = ds.Tables[0].Rows[0]["link_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["img_url"] != null && ds.Tables[0].Rows[0]["img_url"].ToString() != "")
                {
                    model.img_url = ds.Tables[0].Rows[0]["img_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["content"] != null && ds.Tables[0].Rows[0]["content"].ToString() != "")
                {
                    model.content = ds.Tables[0].Rows[0]["content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["seo_title"] != null && ds.Tables[0].Rows[0]["seo_title"].ToString() != "")
                {
                    model.seo_title = ds.Tables[0].Rows[0]["seo_title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["seo_keywords"] != null && ds.Tables[0].Rows[0]["seo_keywords"].ToString() != "")
                {
                    model.seo_keywords = ds.Tables[0].Rows[0]["seo_keywords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["seo_description"] != null && ds.Tables[0].Rows[0]["seo_description"].ToString() != "")
                {
                    model.seo_description = ds.Tables[0].Rows[0]["seo_description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["nav_type"] != null && ds.Tables[0].Rows[0]["nav_type"].ToString() != "")
                {
                    model.nav_type = ds.Tables[0].Rows[0]["nav_type"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sub_title"] != null && ds.Tables[0].Rows[0]["sub_title"].ToString() != "")
                {
                    model.sub_title = ds.Tables[0].Rows[0]["sub_title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["action_type"] != null && ds.Tables[0].Rows[0]["action_type"].ToString() != "")
                {
                    model.action_type = ds.Tables[0].Rows[0]["action_type"].ToString();
                }
                if (ds.Tables[0].Rows[0]["is_sys"] != null && ds.Tables[0].Rows[0]["is_sys"].ToString() != "")
                {
                    model.is_sys = int.Parse(ds.Tables[0].Rows[0]["is_sys"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                model.category_fields = new DAL.contents.category_field(databaseprefix).GetList(id); //扩展属性
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,model_id,is_add_category,is_add_content,is_show_top,is_show_foot,is_albums,is_attach,page_size,link_url,img_url,content,seo_title,seo_keywords,seo_description,nav_type,sub_title,action_type,is_sys,is_lock ");
            strSql.Append(" from " + databaseprefix + "article_category");
            if (strWhere.Trim() != "")                                     
            {
                strSql.Append(" where  channel_id>0  and nav_type='WebSite' and " + strWhere);
            }
            else
            {
                strSql.Append(" where channel_id>0  and nav_type='WebSite'");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据视图获得查询分页数据
        /// </summary>
        public DataSet GetList(string channel_name, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM view_channel_" + channel_name);
            strSql.Append(" where datediff(d,add_time,getdate())>=0");
            if (category_id > 0)
            {
                strSql.Append(" and category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }


        #endregion
    }
}