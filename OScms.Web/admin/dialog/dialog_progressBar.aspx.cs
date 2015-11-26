using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.dialog
{
    public partial class dialog_progressBar : Web.UI.ManagePage
    {
        private int category_id = OSRequest.GetInt("category_id",-2);
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              //ProgressBar.Start(1);
              //for (int i = 0; i < 1000; i++)
              //{
              //    ProgressBar.Roll("共发布" + 1000 + "条信息，正在发布" + (i + 1) + "条。", ((i + 1) * 100 / 1000));
              //}

            //  ProgressBar.Start(2);
            //  int j = 0;
              BLL.contents.article bll = new BLL.contents.article();


                //列表
              DataTable dt = bll.GetList(0, "category_id=" + this.category_id, "sort_id asc,add_time desc,id desc").Tables[0];
              if (dt.Rows != null && dt.Rows.Count > 0)
              {
                  ProgressBar.Start(1);
                  CreatePageHtml(category_id, "/article_list.aspx?category_id=" + category_id, "../../html/list-" + category_id, dt.Rows.Count, 2);
                  ProgressBar.Start(2);
                  for (int i = 0; i < dt.Rows.Count; i++)
                  {
                      CreateIndexHtml("/detail.aspx?category_id=" + category_id + "&id=" + dt.Rows[i]["id"], "../../html/detail-" + category_id + "-" + dt.Rows[i]["id"] + ".html");
                      ProgressBar.Roll("共发布详情" + dt.Rows.Count + "条信息，正在发布" + (i + 1) + "条。", ((i + 1) * 100 / dt.Rows.Count));
                  }
                  ////ProgressBar.Roll("发布信息成功,成功" + j + "个,失败" + (dt.Rows.Count - j) + "条。", 100);
              }



            }
        }


        // 分页
        public string CreatePageHtml(int category_id, string aspxPath, string htmlPath, int totalCount, int pageSize)
        {
            string page = "";
            if (totalCount > pageSize)
            {
                int page_1 = 1;
                for (int j = 0; j < totalCount; j++)
                {
                    if (j >= page_1 * pageSize)
                    {
                        page_1++;
                    }
                }

                for (int z = 0; z <= page_1; z++)
                {
                    if (z == 0)
                    {
                        CreateIndexHtml(aspxPath, htmlPath + ".html");
                        ProgressBar.Roll("共发布列表" + (page_1 + 1) + "条信息，正在发布" + (z + 1) + "条。", ((z + 1) * 100 / (page_1 + 1)));
                    }
                    else
                    {
                        CreateIndexHtml(aspxPath + "&page=" + z + ".aspx", htmlPath + "_" + z + ".html");
                        ProgressBar.Roll("共发布列表" + (page_1 + 1) + "条信息，正在发布" + (z + 1) + "条。", ((z + 1) * 100 / (page_1 + 1)));
                    }
                }
            }
            else
            {
                CreateIndexHtml(aspxPath, htmlPath + ".html");
                ProgressBar.Roll("共发布列表" + 1 + "条信息，正在发布" + 1 + "条。", (1 * 100 / 1));
            }
            return page.ToString();
        }

        // 分页数
        protected  int pageCount(int category_id, int totalCount, int pageSize)
        {
            int page= 0;
            //  int page = 16;//分页个数
            if (totalCount > pageSize)
            {
                int page_1 = 1;
                for (int j = 0; j < totalCount; j++)
                {
                    if (j >= page_1 * pageSize)
                    {
                        page_1++;
                    }
                }
                if (page_1 == 1)
                {
                    page = 1;
                }
                else
                {
                    page = page_1;
                }
            }
            return page;
        }
    }
}