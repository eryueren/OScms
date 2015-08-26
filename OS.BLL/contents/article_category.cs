using System;
using System.Data;
using System.Collections.Generic;

namespace OS.BLL.contents {
	/// <summary>
	/// 类别业务类
	/// </summary>
	public partial class article_category {
		private readonly Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig(); //获得站点配置信息
		private readonly DAL.contents.article_category dal;
		public article_category() {
			dal = new DAL.contents.article_category(siteConfig.sysdatabaseprefix);
		}

		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id) {
			return dal.Exists(id);
		}

		/// <summary>
		/// 返回类别名称
		/// </summary>
		public string GetTitle(int id) {
			return dal.GetTitle(id);
		}
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.contents.article_category model) {
			return dal.Add(model);
		}
		/// <summary>
		/// 查询名称是否存在
		/// </summary>
		public bool Exists(string call_index) {
			return dal.Exists(call_index);
		}

		/// <summary>
		/// 根据频道的ID查询名称
		/// </summary>
		public string GetChannelName(int id) {
			return dal.GetChannelName(id);
		}
		/// <summary>
		/// 修改一列数据
		/// </summary>
		public void UpdateField(int id, string strValue) {
			dal.UpdateField(id, strValue);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.contents.article_category model) {
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id) {
			dal.Delete(id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.contents.article_category GetModel(int id) {
			return dal.GetModel(id);
		}

		/// <summary>
		/// 取得指定类别下的列表
		/// </summary>
		/// <param name="parent_id">父ID</param>
		/// <param name="channel_id">频道ID</param>
		/// <returns></returns>
		public DataTable GetChildList(int parent_id, int channel_id) {
			return dal.GetChildList(parent_id, channel_id);
		}
		/// <summary>
		/// 取得指定类别下的列表
		/// </summary>
		/// <param name="parent_id">父ID</param>
		/// <param name="channel_id">频道ID</param>
		/// <returns></returns>
		public DataTable GetChildTopList(int Top, int parent_id, int channel_id) {
			return dal.GetChildTopList(Top, parent_id, channel_id);
		}
		/// <summary>
		/// 取得所有类别列表
		/// </summary>
		/// <param name="parent_id">父ID</param>
		/// <param name="channel_id">频道ID</param>
		/// <returns></returns>
		public DataTable GetList(int parent_id, int channel_id) {
			return dal.GetList(parent_id, channel_id);
		}


		/// <summary>
		/// 取得所有类别列表
		/// </summary>
		/// <param name="parent_id">父ID</param>
		/// <param name="channel_id">频道ID</param>
		/// <returns></returns>
		public DataTable GetLists(int parent_id, string where) {
			return dal.GetLists(parent_id, where);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top, string strWhere, string filedOrder) {
			return dal.GetList(Top, strWhere, filedOrder);
		}

		/// <summary>
		/// 获取所有频道，所有分类
		/// </summary>
		/// <returns></returns>
		public DataTable GetAllList() {
			return dal.GetAllList();
		}





		#region 扩展方法================================
		/// <summary>
		/// 取得父节点的ID
		/// </summary>
		public int GetParentId(int id) {
			return dal.GetParentId(id);
		}
		#endregion

		#endregion
	}
}