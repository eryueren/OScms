using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using  OS.DBUtility;//Please add references
using  OS.Common;
namespace  OS.DAL.contents
{
    /// <summary>
    /// 数据访问类:yl_category_field
    /// </summary>
    public partial class category_field
    {

        private string databaseprefix; //数据库表名前缀
        public category_field(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;

        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<OS.Model.contents.category_field> GetList(int category_id)
        {
            List<OS.Model.contents.category_field> modelList = new List<OS.Model.contents.category_field>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,category_id,field_id from " + databaseprefix + "category_field ");
            strSql.Append(" where category_id=" + category_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                OS.Model.contents.category_field model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.contents.category_field();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["category_id"] != null && dt.Rows[n]["category_id"].ToString() != "")
                    {
                        model.category_id = int.Parse(dt.Rows[n]["category_id"].ToString());
                    }
                    if (dt.Rows[n]["field_id"] != null && dt.Rows[n]["field_id"].ToString() != "")
                    {
                        model.field_id = int.Parse(dt.Rows[n]["field_id"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 查找不存在的图片并删除已删除的图片及数据
        /// </summary>
        public void DeleteList(SqlConnection conn, SqlTransaction trans, List<Model.contents.category_field> models, int category_id)
        {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (Model.contents.category_field modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string id_list = Utils.DelLastChar(idList.ToString(), ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,category_id,field_id from " + databaseprefix + "category_field where category_id=" + category_id);
            if (!string.IsNullOrEmpty(id_list))
            {
                strSql.Append(" and id not in(" + id_list + ")");
            }
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DbHelperSQL.ExecuteSql(conn, trans, "delete from " + databaseprefix + "category_field where id=" + dr["id"].ToString()); //删除数据库           
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,category_id,field_id ");
            strSql.Append(" FROM " + databaseprefix + "category_field  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
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
            strSql.Append(" id,category_id,field_id ");
            strSql.Append(" FROM " + databaseprefix + "category_field  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "category_field  ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

