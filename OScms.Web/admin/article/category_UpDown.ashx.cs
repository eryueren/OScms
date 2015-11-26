using System;
using System.Collections.Generic;
using System.Web;
using OS.Web.UI;
using OS.Common;
using System.Text;
using System.Data;


namespace OS.Web.admin.article
{
    /// <summary>
    /// Column_UpDown 的摘要说明
    /// </summary>
    public class category_UpDown : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {

            BLL.contents.article_category bll = new BLL.contents.article_category();
            Model.contents.article_category model = new Model.contents.article_category();
            int id = OSRequest.GetQueryInt("id", -2);
            int type = OSRequest.GetQueryInt("type", -2);   //0上 ；1下
            model = bll.GetModel(id);
            DataTable dt_sx = bll.GetList(0, "parent_id=" + model.parent_id, "sort_id asc,id desc").Tables[0];
            if (dt_sx.Rows != null && dt_sx.Rows.Count > 0)
            {
                for (int i = 0; i < dt_sx.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt_sx.Rows[i]["ID"]) == id)
                    {
                        if (type == 1)
                        {
                            if ((i + 1) < dt_sx.Rows.Count)
                            {
                                int newId = Convert.ToInt32(dt_sx.Rows[i + 1]["ID"]);
                                //位置互换
                                bll.UpdateField(newId, "sort_id=" + model.sort_id + "");
                                bll.UpdateField(id, "sort_id=" + (bll.GetModel(newId).sort_id + 1) + "");
                                context.Response.Write("OK");
                            }
                            else
                            {
                                context.Response.Write("down");
                            }
                        }
                        else if (type == 0)
                        {
                            if ((i - 1) >= 0)
                            {
                                int newId = Convert.ToInt32(dt_sx.Rows[i - 1]["ID"]);
                                //位置互换
                                bll.UpdateField(newId, "sort_id=" + model.sort_id + "");
                                bll.UpdateField(id, "sort_id=" + (bll.GetModel(newId).sort_id - 1) + "");
                                context.Response.Write("OK");
                            }
                            else
                            {
                                context.Response.Write("up");
                            }
                        }
                        else
                        {
                            context.Response.Write("参数错误");
                        }
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}