using System;
using System.Data;
using System.Collections.Generic;
using  OS.Common;

namespace OS.BLL.plugins
{
	/// <summary>
	/// link
	/// </summary>
	public partial class link
	{
        private readonly Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.plugins.link dal;
		public link()
		{
            dal = new DAL.plugins.link(siteConfig.sysdatabaseprefix);
        }

		#region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Model.plugins.link model)
		{
			return dal.Add(model);
		}

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(Model.plugins.link model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			return dal.Delete(id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.plugins.link GetModel(int id)
		{
			return dal.GetModel(id);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
        public DataSet GetList(int Top, string strWhere)
        {
            return dal.GetList(Top, strWhere);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

		#endregion  Method
	}
}

