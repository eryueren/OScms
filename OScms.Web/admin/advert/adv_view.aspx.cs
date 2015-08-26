using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using OS.Common;

namespace OS.Web.manage.advert
{
    public partial class adv_view : OS.Web.UI.ManagePage
    {
        protected int id = 0;
        protected Model.plugins.advert model = new Model.plugins.advert();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = YLRequest.GetQueryInt("id", 0);
            if (this.id < 1)
            {
                PageErrorMsg("传输参数不正确");
            }

            if (!new BLL.plugins.advert().Exists(this.id))
            {
                PageErrorMsg("信息不存在或已被删除");
            }
            if (!Page.IsPostBack)
            {
                model = new BLL.plugins.advert().GetModel(id);
            }
        }

        #region 显示广告类型=================================
        protected string GetTypeName(int typeId)
        {
            switch (typeId)
            {
                case 1:
                    return "文字";
                case 2:
                    return "图片";
                case 3:
                    return "幻灯片";
                case 4:
                    return "动画";
                case 5:
                    return "FLV视频";
                case 6:
                    return "代码";
                default:
                    return "其它";
            }
        }
        #endregion

    }
}