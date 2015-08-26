using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using  OS.Common;

namespace  OS.Web.UI.Page
{
    public partial class article_list : Web.UI.BasePage
    {
        protected int page = YLRequest.GetQueryInt("page", 1);         //当前页码
        protected int totalcount;   //OUT数据总数
        protected string pagelist;  //分页页码
        protected int category_id = YLRequest.GetInt("category_id", -2);
        VelocityHelper vh = new VelocityHelper(new Function());
        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {

            Model.contents.article_category model = new BLL.channels.category().GetModel(this.category_id);
            if (model != null)
            {
                string _name = model.call_index;
                string _class_list = model.class_list;
                string _key = BasePage.pageUrl(model.model_id);
                string _where = "status=0 and  category_id=" + this.category_id;
                DataRowCollection list = new BasePage().get_article_list(_name, page, _where, out totalcount, out pagelist, _key, this.category_id, "__id__").Rows;
                vh.Put("channel_id", category_id);
                vh.Put("list", list);
                vh.Put("page", pagelist);
                vh.Display("../Template/newList.html");
            }
          
        }
    }
}
