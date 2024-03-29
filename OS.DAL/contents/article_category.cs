﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using  OS.DBUtility;
using  OS.Common;
using System.Collections.Generic;

namespace  OS.DAL.contents
{
    /// <summary>
    /// 数据访问类:内容类别
    /// </summary>
    public partial class article_category
    {
        private string databaseprefix; //数据库表名前缀
        public article_category(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法========================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_category");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询是否存在该记录
        /// </summary>
        public bool Exists(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_category");
            strSql.Append(" where call_index=@call_index ");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.VarChar,50)};
            parameters[0].Value = call_index;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据频道的ID查询名称
        /// </summary>
        public string GetChannelName(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 call_index FROM " + databaseprefix + "article_category");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            string name = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            return name;
        }

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
        /// 增加一条数据
        /// </summary>
        public int Add(Model.contents.article_category model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "article_category(");
                        strSql.Append("channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,model_id,is_add_category,is_add_content,is_show_top,is_show_foot,is_albums,is_attach,page_size,link_url,img_url,content,seo_title,seo_keywords,seo_description,nav_type,sub_title,action_type,is_sys,is_lock)");
                        strSql.Append(" values (");
                        strSql.Append("@channel_id,@title,@call_index,@parent_id,@class_list,@class_layer,@sort_id,@model_id,@is_add_category,@is_add_content,@is_show_top,@is_show_foot,@is_albums,@is_attach,@page_size,@link_url,@img_url,@content,@seo_title,@seo_keywords,@seo_description,@nav_type,@sub_title,@action_type,@is_sys,@is_lock)");
                        strSql.Append(";select @@IDENTITY");
                        SqlParameter[] parameters = {
					            new SqlParameter("@channel_id", SqlDbType.Int,4),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
                                new SqlParameter("@call_index", SqlDbType.NVarChar,50),
					            new SqlParameter("@parent_id", SqlDbType.Int,4),
					            new SqlParameter("@class_list", SqlDbType.NVarChar,500),
					            new SqlParameter("@class_layer", SqlDbType.Int,4),
                                new SqlParameter("@sort_id", SqlDbType.Int,4),
                    new SqlParameter("@model_id", SqlDbType.Int,4),
					new SqlParameter("@is_add_category", SqlDbType.Int,4),
					new SqlParameter("@is_add_content", SqlDbType.Int,4),
					new SqlParameter("@is_show_top", SqlDbType.Int,4),
					new SqlParameter("@is_show_foot", SqlDbType.Int,4),
                    new SqlParameter("@is_albums", SqlDbType.TinyInt,1),
					new SqlParameter("@is_attach", SqlDbType.TinyInt,1),
					new SqlParameter("@page_size", SqlDbType.Int,4),
					            new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
					            new SqlParameter("@seo_title", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_keywords", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_description", SqlDbType.NVarChar,255),
                    new SqlParameter("@nav_type", SqlDbType.NChar,50),
					new SqlParameter("@sub_title", SqlDbType.NChar,100),
					new SqlParameter("@action_type", SqlDbType.NChar,500),
					new SqlParameter("@is_sys", SqlDbType.TinyInt,1),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1)};
                        parameters[0].Value = model.channel_id;
                        parameters[1].Value = model.title;
                        parameters[2].Value = model.call_index;
                        parameters[3].Value = model.parent_id;
                        parameters[4].Value = model.class_list;
                        parameters[5].Value = model.class_layer;
                        parameters[6].Value = model.sort_id;

                        parameters[7].Value = model.model_id;
                        parameters[8].Value = model.is_add_category;
                        parameters[9].Value = model.is_add_content;
                        parameters[10].Value = model.is_show_top;
                        parameters[11].Value = model.is_show_foot;
                        parameters[12].Value = model.is_albums;
                        parameters[13].Value = model.is_attach;
                        parameters[14].Value = model.page_size;

                        parameters[15].Value = model.link_url;
                        parameters[16].Value = model.img_url;
                        parameters[17].Value = model.content;
                        parameters[18].Value = model.seo_title;
                        parameters[19].Value = model.seo_keywords;
                        parameters[20].Value = model.seo_description;

                        parameters[21].Value = model.nav_type;
                        parameters[22].Value = model.sub_title;
                        parameters[23].Value = model.action_type;
                        parameters[24].Value = model.is_sys;
                        parameters[25].Value = model.is_lock;

                        object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), parameters); //带事务
                        model.id = Convert.ToInt32(obj);

                        //扩展字段
                        if (model.category_fields != null)
                        {
                            StringBuilder strSql2;
                            foreach (Model.contents.category_field modelt in model.category_fields)
                            {
                                strSql2 = new StringBuilder();
                                strSql2.Append("insert into " + databaseprefix + "category_field(");
                                strSql2.Append("category_id,field_id)");
                                strSql2.Append(" values (");
                                strSql2.Append("@channel_id,@field_id)");
                                SqlParameter[] parameters2 = {
					                    new SqlParameter("@channel_id", SqlDbType.Int,4),
					                    new SqlParameter("@field_id", SqlDbType.Int,4)};
                                parameters2[0].Value = model.id;
                                parameters2[1].Value = modelt.field_id;
                                DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                            }
                        }

                        
                        //添加自定义图片尺寸
                        if (model.imagesize_values != null)
                        {
                            StringBuilder strSqlimage;
                            foreach (Model.contents.article_images_size modelimagesize in model.imagesize_values)
                            {
                                strSqlimage = new StringBuilder();
                                strSqlimage.Append("insert into " + databaseprefix + "article_images_size(");
                                strSqlimage.Append("category_id,height,width)");
                                strSqlimage.Append(" values (");
                                strSqlimage.Append("@category_id,@height,@width)");
                           
                                SqlParameter[] parametersimages = {
					            new SqlParameter("@category_id", SqlDbType.Int,4),
					            new SqlParameter("@height", SqlDbType.NVarChar,50),
					            new SqlParameter("@width", SqlDbType.NVarChar,50)};
                                parametersimages[0].Value = model.id;
                                parametersimages[1].Value = modelimagesize.height;
                                parametersimages[2].Value = modelimagesize.width;
                                DbHelperSQL.ExecuteSql(conn, trans, strSqlimage.ToString(), parametersimages); 
                            }
                        }
                    


                        if (model.parent_id > 0)
                        {
                            Model.contents.article_category model2 = GetModel(conn, trans, model.parent_id); //带事务
                            model.class_list = model2.class_list + model.id + ",";
                            model.class_layer = model2.class_layer + 1;
                        }
                        else
                        {
                            model.class_list = "," + model.id + ",";
                            model.class_layer = 1;
                        }
                        //修改节点列表和深度
                        DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "article_category set class_list='" + model.class_list + "', class_layer=" + model.class_layer + " where id=" + model.id); //带事务
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
            return model.id;
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
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.contents.article_category model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //先判断选中的父节点是否被包含
                        if (IsContainNode(model.id, model.parent_id))
                        {
                            //查找旧数据
                            Model.contents.article_category oldModel = GetModel(model.id);
                            //查找旧父节点数据
                            string class_list = "," + model.parent_id + ",";
                            int class_layer = 1;
                            if (oldModel.parent_id > 0)
                            {
                                Model.contents.article_category oldParentModel = GetModel(conn, trans, oldModel.parent_id); //带事务
                                class_list = oldParentModel.class_list + model.parent_id + ",";
                                class_layer = oldParentModel.class_layer + 1;
                            }
                            //先提升选中的父节点
                            DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "article_category set parent_id=" + oldModel.parent_id + ",class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + model.parent_id); //带事务
                            UpdateChilds(conn, trans, model.parent_id); //带事务
                        }
                        //更新子节点
                        if (model.parent_id > 0)
                        {
                            Model.contents.article_category model2 = GetModel(conn, trans, model.parent_id); //带事务
                            model.class_list = model2.class_list + model.id + ",";
                            model.class_layer = model2.class_layer + 1;
                        }
                        else
                        {
                            model.class_list = "," + model.id + ",";
                            model.class_layer = 1;
                        }


                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "article_category set ");
                        strSql.Append("channel_id=@channel_id,");
                        strSql.Append("title=@title,");
                        strSql.Append("call_index=@call_index,");
                        strSql.Append("parent_id=@parent_id,");
                        strSql.Append("class_list=@class_list,");
                        strSql.Append("class_layer=@class_layer,");
                        strSql.Append("sort_id=@sort_id,");

                        strSql.Append("model_id=@model_id,");
                        strSql.Append("is_add_category=@is_add_category,");
                        strSql.Append("is_add_content=@is_add_content,");
                        strSql.Append("is_show_top=@is_show_top,");
                        strSql.Append("is_show_foot=@is_show_foot,");
                        strSql.Append("is_albums=@is_albums,");
                        strSql.Append("is_attach=@is_attach,");
                        strSql.Append("page_size=@page_size,");

                        strSql.Append("link_url=@link_url,");
                        strSql.Append("img_url=@img_url,");
                        strSql.Append("content=@content,");
                        strSql.Append("seo_title=@seo_title,");
                        strSql.Append("seo_keywords=@seo_keywords,");
                        strSql.Append("seo_description=@seo_description,");

                        strSql.Append("nav_type=@nav_type,");
                        strSql.Append("sub_title=@sub_title,");
                        strSql.Append("action_type=@action_type,");
                        strSql.Append("is_sys=@is_sys,");
                        strSql.Append("is_lock=@is_lock");

                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@channel_id", SqlDbType.Int,4),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
                                new SqlParameter("@call_index", SqlDbType.NVarChar,50),
					            new SqlParameter("@parent_id", SqlDbType.Int,4),
					            new SqlParameter("@class_list", SqlDbType.NVarChar,500),
					            new SqlParameter("@class_layer", SqlDbType.Int,4),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),

                    new SqlParameter("@model_id", SqlDbType.Int,4),
				 	new SqlParameter("@is_add_category", SqlDbType.Int,4),
					new SqlParameter("@is_add_content", SqlDbType.Int,4),
					new SqlParameter("@is_show_top", SqlDbType.Int,4),
					new SqlParameter("@is_show_foot", SqlDbType.Int,4),
                    new SqlParameter("@is_albums", SqlDbType.TinyInt,1),
					new SqlParameter("@is_attach", SqlDbType.TinyInt,1),
				    new SqlParameter("@page_size", SqlDbType.Int,4),

					            new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
					            new SqlParameter("@seo_title", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_keywords", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_description", SqlDbType.NVarChar,255),

                    new SqlParameter("@nav_type", SqlDbType.NChar,50),
					new SqlParameter("@sub_title", SqlDbType.NChar,100),
					new SqlParameter("@action_type", SqlDbType.NChar,500),
					new SqlParameter("@is_sys", SqlDbType.TinyInt,1),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.channel_id;
                        parameters[1].Value = model.title;
                        parameters[2].Value = model.call_index;
                        parameters[3].Value = model.parent_id;
                        parameters[4].Value = model.class_list;
                        parameters[5].Value = model.class_layer;
                        parameters[6].Value = model.sort_id;
                        parameters[7].Value = model.model_id;
                        parameters[8].Value = model.is_add_category;
                        parameters[9].Value = model.is_add_content;
                        parameters[10].Value = model.is_show_top;
                        parameters[11].Value = model.is_show_foot;
                        parameters[12].Value = model.is_albums;
                        parameters[13].Value = model.is_attach;
                        parameters[14].Value = model.page_size;
                        parameters[15].Value = model.link_url;
                        parameters[16].Value = model.img_url;
                        parameters[17].Value = model.content;
                        parameters[18].Value = model.seo_title;
                        parameters[19].Value = model.seo_keywords;
                        parameters[20].Value = model.seo_description;
                        parameters[21].Value = model.nav_type;
                        parameters[22].Value = model.sub_title;
                        parameters[23].Value = model.action_type;
                        parameters[24].Value = model.is_sys;
                        parameters[25].Value = model.is_lock;
                        parameters[26].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);


                        //添加/修改属性
                        if (model.category_fields != null)
                        {
                            new category_field(databaseprefix).DeleteList(conn, trans, model.category_fields, model.id);
                            StringBuilder strSql3;
                            foreach (Model.contents.category_field models in model.category_fields)
                            {
                                strSql3 = new StringBuilder();
                                if (models.id > 0)
                                {
                                    strSql3.Append("update  " + databaseprefix + "category_field set ");
                                    strSql3.Append("category_id=@category_id,");
                                    strSql3.Append("field_id=@field_id");
                                    strSql3.Append(" where id=@id");
                                    SqlParameter[] parameters3 = {
					                        new SqlParameter("@category_id", SqlDbType.Int,4),
					                        new SqlParameter("@field_id", SqlDbType.Int,4),
					                        new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters3[0].Value = models.category_id;
                                    parameters3[1].Value = models.field_id;
                                    parameters3[2].Value = models.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql3.ToString(), parameters3);
                                }
                                else
                                {
                                    strSql3.Append("insert into  " + databaseprefix + "category_field(");
                                    strSql3.Append("category_id,field_id)");
                                    strSql3.Append(" values (");
                                    strSql3.Append("@category_id,@field_id)");
                                    SqlParameter[] parameters3 = {
					                        new SqlParameter("@category_id", SqlDbType.Int,4),
					                        new SqlParameter("@field_id", SqlDbType.Int,4)};
                                    parameters3[0].Value = models.category_id;
                                    parameters3[1].Value = models.field_id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql3.ToString(), parameters3);
                                }
                            }
                        }

                          //添加自定义图片尺寸
                        if (model.imagesize_values != null)
                        { 
                            //删除已删除的图片
                            new article_images_size(databaseprefix).DeleteList(conn, trans, model.imagesize_values, model.id);
                            StringBuilder strSqlimage;
                            foreach (Model.contents.article_images_size modelimagesize in model.imagesize_values)
                            {
                                strSqlimage = new StringBuilder();
                                if (modelimagesize.id > 0)
                                {
                                    strSqlimage.Append("update yl_article_images_size set ");
                                    strSqlimage.Append("category_id=@category_id,");
                                    strSqlimage.Append("height=@height,");
                                    strSqlimage.Append("width=@width");
                                    strSqlimage.Append(" where id=@id");
                                    SqlParameter[] parametersimages = {
					                new SqlParameter("@category_id", SqlDbType.Int,4),
					                new SqlParameter("@height", SqlDbType.NVarChar,50),
					                new SqlParameter("@width", SqlDbType.NVarChar,50),
					                new SqlParameter("@id", SqlDbType.Int,4)};
                                    parametersimages[0].Value = model.id;
                                    parametersimages[1].Value = modelimagesize.height;
                                    parametersimages[2].Value = modelimagesize.width;
                                    parametersimages[3].Value = model.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSqlimage.ToString(), parametersimages);
                                }
                                else
                                {
                                    strSqlimage.Append("insert into " + databaseprefix + "article_images_size(");
                                    strSqlimage.Append("category_id,height,width)");
                                    strSqlimage.Append(" values (");
                                    strSqlimage.Append("@category_id,@height,@width)");
                                    SqlParameter[] parametersimages = {
					                new SqlParameter("@category_id", SqlDbType.Int,4),
					                new SqlParameter("@height", SqlDbType.NVarChar,50),
					                new SqlParameter("@width", SqlDbType.NVarChar,50)};
                                    parametersimages[0].Value = model.id;
                                    parametersimages[1].Value = modelimagesize.height;
                                    parametersimages[2].Value = modelimagesize.width;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSqlimage.ToString(), parametersimages);
                                }
                            }
                        }


                        //更新子节点
                        UpdateChilds(conn, trans, model.id);
                        trans.Commit();
                    }
                    catch(Exception ex)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            DeleteContent(id);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "article_category ");
            strSql.Append(" where class_list like '%," + id + ",%' ");
            DbHelperSQL.Query(strSql.ToString());
        }


        #region 删除栏目 及其子栏目下 所属的内容,图片相册，扩展属性
        public void DeleteContent(int classId)
        {
            article a_dal = new article(databaseprefix);
            category_field c_dal = new category_field(databaseprefix);
            string[] arr = ss(classId).Split(',');
            if (arr.Length > 0)
            {
                for (int l = 0; l < arr.Length; l++)
                {
                    Utils.DeleteFile( GetModel(Convert.ToInt32(arr[l])).img_url);
                    DataTable dt_c = c_dal.GetList(0, "category_id=" + arr[l] + "", "id desc").Tables[0];
                    if (dt_c.Rows != null && dt_c.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt_c.Rows.Count; k++)
                        {
                            int id_c = Convert.ToInt32(dt_c.Rows[k]["id"]);
                            c_dal.Delete(id_c);
                        }
                    }

                    DataTable dt_info = a_dal.GetList(0, "category_id=" + arr[l] + "", "sort_id asc,add_time desc,id desc").Tables[0];
                    if (dt_info.Rows != null && dt_info.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt_info.Rows.Count; j++)
                        {
                            int list_info_Id = Convert.ToInt32(dt_info.Rows[j]["Id"]);
                            a_dal.Delete(list_info_Id);
                        }
                    }
                }
            }

        }
        protected string ss(int classId)
        {
            string str = "";
            DataTable dt = GetList("class_list like '%," + classId + ",%'").Tables[0];
            if (dt.Rows != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += dt.Rows[i]["Id"].ToString() + ",";
                }
            }
            return str.TrimEnd(',').ToString();
        }
        #endregion
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
                strSql.Append(" where " + strWhere);
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
        /// 得到一个对象实体(重载，带事务)
        /// </summary>
        public Model.contents.article_category GetModel(SqlConnection conn, SqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,model_id,is_add_category,is_add_content,is_show_top,is_show_foot,is_albums,is_attach,page_size,link_url,img_url,content,seo_title,seo_keywords,seo_description,nav_type,sub_title,action_type,is_sys,is_lock from " + databaseprefix + "article_category ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.contents.article_category model = new Model.contents.article_category();
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString(), parameters);
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
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取得指定类别下的列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetChildList(int parent_id, int channel_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,model_id,is_add_category,is_add_content,is_show_top,is_show_foot,is_albums,is_attach,page_size,link_url,img_url,content,seo_title,seo_keywords,seo_description,nav_type,sub_title,action_type,is_sys,is_lock from " + databaseprefix + "article_category");
            strSql.Append(" where channel_id=" + channel_id + " and parent_id=" + parent_id + " order by sort_id asc,id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds.Tables[0];
        }
		/// <summary>
		/// 取得指定类别下的列表
		/// </summary>
		/// <param name="parent_id">父ID</param>
		/// <param name="channel_id">频道ID</param>
		/// <returns></returns>
		public DataTable GetChildTopList(int Top, int parent_id, int channel_id) {
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			if (Top > 0) {
				strSql.Append(" top " + Top.ToString());
			}
			strSql.Append(" id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,model_id,is_add_category,is_add_content,is_show_top,is_show_foot,is_albums,is_attach,page_size,link_url,img_url,content,seo_title,seo_keywords,seo_description,nav_type,sub_title,action_type,is_sys,is_lock from " + databaseprefix + "article_category");
			strSql.Append(" where channel_id=" + channel_id + " and parent_id=" + parent_id + " order by sort_id asc,id desc");
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			return ds.Tables[0];
		}
        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetList(int parent_id, int channel_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,model_id,is_add_category,is_add_content,is_show_top,is_show_foot,is_albums,is_attach,page_size,link_url,img_url,content,seo_title,seo_keywords,seo_description,nav_type,sub_title,action_type,is_sys,is_lock from " + databaseprefix + "article_category");
            strSql.Append(" where channel_id=" + channel_id + " order by sort_id asc,id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            GetChilds(oldData, newData, parent_id, channel_id);
            return newData;
        }


        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetLists(int parent_id, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,model_id,is_add_category,is_add_content,is_show_top,is_show_foot,is_albums,is_attach,page_size,link_url,img_url,content,seo_title,seo_keywords,seo_description,nav_type,sub_title,action_type,is_sys,is_lock from " + databaseprefix + "article_category");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by sort_id asc,id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            GetChilds(oldData, newData, parent_id);
            return newData;
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
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取所有频道，所有分类
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,model_id,is_add_category,is_add_content,is_show_top,is_show_foot,is_albums,is_attach,page_size,link_url,img_url,content,seo_title,seo_keywords,seo_descriptio,nav_type,sub_title,action_type,is_sys,is_lock from " + databaseprefix + "article_category");
            strSql.Append(" order by sort_id asc,id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
        
            return ds.Tables[0];
        }


        #region 扩展方法================================
        public int GetParentId(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 parent_id from " + databaseprefix + "article_category");
            strSql.Append(" where id=" + id);
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
        #endregion

        #region 私有方法================================
        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id, int channel_id)
        {
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["id"] = int.Parse(dr[i]["id"].ToString());
                row["channel_id"] = int.Parse(dr[i]["channel_id"].ToString());
                row["title"] = dr[i]["title"].ToString();
                row["call_index"] = dr[i]["call_index"].ToString();
                row["parent_id"] = int.Parse(dr[i]["parent_id"].ToString());
                row["class_list"] = dr[i]["class_list"].ToString();
                row["class_layer"] = int.Parse(dr[i]["class_layer"].ToString());
                row["sort_id"] = int.Parse(dr[i]["sort_id"].ToString());

                row["model_id"] = int.Parse(dr[i]["model_id"].ToString());
                row["is_add_category"] = int.Parse(dr[i]["is_add_category"].ToString());
                row["is_add_content"] = int.Parse(dr[i]["is_add_content"].ToString());
                row["is_show_top"] = int.Parse(dr[i]["is_show_top"].ToString());
                row["is_show_foot"] = int.Parse(dr[i]["is_show_foot"].ToString());
                row["is_albums"] = int.Parse(dr[i]["is_albums"].ToString());
                row["is_attach"] = int.Parse(dr[i]["is_attach"].ToString());
                row["page_size"] = int.Parse(dr[i]["page_size"].ToString());


                row["link_url"] = dr[i]["link_url"].ToString();
                row["img_url"] = dr[i]["img_url"].ToString();
                row["content"] = dr[i]["content"].ToString();
                row["seo_title"] = dr[i]["seo_title"].ToString();
                row["seo_keywords"] = dr[i]["seo_keywords"].ToString();
                row["seo_description"] = dr[i]["seo_description"].ToString();


                row["nav_type"] = dr[i]["nav_type"].ToString();
                row["sub_title"] = dr[i]["sub_title"].ToString();
                row["action_type"] = dr[i]["action_type"].ToString();
                row["is_sys"] = int.Parse(dr[i]["is_sys"].ToString());
                row["is_lock"] = int.Parse(dr[i]["is_lock"].ToString());

                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["id"].ToString()), channel_id);
            }
        }

        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id)
        {
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["id"] = int.Parse(dr[i]["id"].ToString());
                row["channel_id"] = int.Parse(dr[i]["channel_id"].ToString());
                row["title"] = dr[i]["title"].ToString();
                row["call_index"] = dr[i]["call_index"].ToString();
                row["parent_id"] = int.Parse(dr[i]["parent_id"].ToString());
                row["class_list"] = dr[i]["class_list"].ToString();
                row["class_layer"] = int.Parse(dr[i]["class_layer"].ToString());
                row["sort_id"] = int.Parse(dr[i]["sort_id"].ToString());

                row["model_id"] = int.Parse(dr[i]["model_id"].ToString());
                row["is_add_category"] = int.Parse(dr[i]["is_add_category"].ToString());
                row["is_add_content"] = int.Parse(dr[i]["is_add_content"].ToString());
                row["is_show_top"] = int.Parse(dr[i]["is_show_top"].ToString());
                row["is_show_foot"] = int.Parse(dr[i]["is_show_foot"].ToString());
                row["is_albums"] = int.Parse(dr[i]["is_albums"].ToString());
                row["is_attach"] = int.Parse(dr[i]["is_attach"].ToString());
                row["page_size"] = int.Parse(dr[i]["page_size"].ToString());

                row["link_url"] = dr[i]["link_url"].ToString();
                row["img_url"] = dr[i]["img_url"].ToString();
                row["content"] = dr[i]["content"].ToString();
                row["seo_title"] = dr[i]["seo_title"].ToString();
                row["seo_keywords"] = dr[i]["seo_keywords"].ToString();
                row["seo_description"] = dr[i]["seo_description"].ToString();
                row["nav_type"] = dr[i]["nav_type"].ToString();
                row["sub_title"] = dr[i]["sub_title"].ToString();
                row["action_type"] = dr[i]["action_type"].ToString();
                row["is_sys"] = int.Parse(dr[i]["is_sys"].ToString());
                row["is_lock"] = int.Parse(dr[i]["is_lock"].ToString());

                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["id"].ToString()));
            }
        }

        /// <summary>
        /// 修改子节点的ID列表及深度（自身迭代）
        /// </summary>
        /// <param name="parent_id"></param>
        private void UpdateChilds(SqlConnection conn, SqlTransaction trans, int parent_id)
        {
            //查找父节点信息
            Model.contents.article_category model = GetModel(conn, trans, parent_id);
            if (model != null)
            {
                //查找子节点
                string strSql = "select id from " + databaseprefix + "article_category where parent_id=" + parent_id;
                DataSet ds = DbHelperSQL.Query(conn, trans, strSql); //带事务
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //修改子节点的ID列表及深度
                    int id = int.Parse(dr["id"].ToString());
                    string class_list = model.class_list + id + ",";
                    int class_layer = model.class_layer + 1;
                    DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "article_category set class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + id); //带事务

                    //调用自身迭代
                    this.UpdateChilds(conn, trans, id); //带事务
                }
            }
        }

        /// <summary>
        /// 验证节点是否被包含
        /// </summary>
        /// <param name="id">待查询的节点</param>
        /// <param name="parent_id">父节点</param>
        /// <returns></returns>
        private bool IsContainNode(int id, int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_category ");
            strSql.Append(" where class_list like '%," + id + ",%' and id=" + parent_id);
            return DbHelperSQL.Exists(strSql.ToString());
        }

        #endregion

        #endregion
    }
}