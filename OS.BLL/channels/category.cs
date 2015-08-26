using System;
using System.Data;
using System.Collections.Generic;

namespace OS.BLL.channels
{
    /// <summary>
    /// 类别业务类
    /// </summary>
    public partial class category
    {
        private readonly Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.channels.category dal;
        public category()
        {
            dal = new DAL.channels.category(siteConfig.sysdatabaseprefix);
        }

        #region  Method


        /// <summary>
        /// 获取分页大小
        /// </summary>
        public int GetPageSize(string channel_name)
        {
            return dal.GetPageSize(channel_name);
        }

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }


        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.contents.article_category GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得前数据
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }


        /// <summary>
        /// 根据栏目的名称查询ID
        /// </summary>
        public int Get_category_id(string channel_name)
        {
            return dal.Get_category_id(channel_name);
        }

        /// <summary>
        /// 根据视图获得查询分页数据
        /// </summary>
        public DataSet GetList(string channel_name, int category_id, int pageIndex, string strWhere, string filedOrder, out int recordCount, out int pageSize)
        {
            pageSize = new BLL.contents.article_category().GetModel(category_id).page_size; //自动获得频道分页数量
            return dal.GetList(channel_name, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion
    }
}