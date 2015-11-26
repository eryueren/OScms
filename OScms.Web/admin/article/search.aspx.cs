using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.article
{
    public partial class search : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected string keywords = OSRequest.GetQueryString("keywords");

        protected string prolistview = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
          
            this.pageSize = GetPageSize(10); //每页数量
         
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("link", OSEnums.ActionEnum.View.ToString()); //检查权限
             RptBind("id>0"+ CombSqlTxt(this.keywords), "sort_id asc,add_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = OSRequest.GetQueryInt("page", 1);

            this.txtKeywords.Text = this.keywords;
            //图表或列表显示
            BLL.contents.article bll = new BLL.contents.article();

            this.rptList1.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList1.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("search.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }
            
            return strTemp.ToString();
        }
        #endregion

        #region 返回图文每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("search_article_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //设置操作


        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("search.aspx", "keywords={0}", txtKeywords.Text));
        }



        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("search_article_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("search.aspx", "keywords={0}", this.keywords));
        }



        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("link", OSEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            BLL.contents.article bll = new BLL.contents.article();
            Repeater rptList = new Repeater();
              rptList = this.rptList1;
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount++;
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }
                AddAdminLog(OSEnums.ActionEnum.Edit.ToString(), "删除[搜索-"+this.keywords+"]频道内容成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
                Response.Redirect(Utils.CombUrlTxt("search.aspx", "keywords={0}", this.keywords));
        }

        protected string menus(int category_id)
        {
            string menu = "";
            Model.contents.article_category model = new BLL.contents.article_category().GetModel(category_id);
            if (model != null)
            {
                string class_list = model.class_list.Trim(',');
                string[] arr = class_list.Split(',');
                int loop = 0;
                if (arr.Length > 0)
                {
                    foreach (string i in arr)
                    {
                        loop++;
                        string style = loop == arr.Length ? "" : " > ";
                        if (loop != 1)
                        {
                            menu += new BLL.contents.article_category().GetTitle(Convert.ToInt32(i)) + style;
                        }
                    }
                }
            }
            return menu.ToString();
        }
    }
}