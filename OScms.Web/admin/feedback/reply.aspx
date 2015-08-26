<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reply.aspx.cs" Inherits="OS.Web.manage.feedback.reply" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>回复留言信息</title>
    <link type="text/css" rel="stylesheet" href="../css/default/style.css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="list.aspx" class="back"><i></i><span>返回列表页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <a href="list.aspx"><span>物流配送</span></a>
            <i class="arrow"></i>
            <span>插件管理</span>
            <i class="arrow"></i>
            <span>在线预约申请</span>
            <i class="arrow"></i>
            <span>查看预约申请</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">查看预约申请</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>预约人</dt>
                <dd><%=model.title %></dd>
            </dl>
            <dl>
                <dt>联系电话</dt>
                <dd><%=model.user_tel %></dd>
            </dl>
            <dl>
                <dt>性别</dt>
                <dd><%=model.user_name %></dd>
            </dl>
            <dl>
                <dt>城市</dt>
                <dd><%=model.reply_content %></dd>
            </dl>
            <dl>
                <dt>地址</dt>
                <dd><%=model.content %></dd>
            </dl>
             <dl>
                <dt>预约时间</dt>
                <dd><%=model.add_time %></dd>
            </dl>
            <dl style="display: none">
                <dt>回复留言</dt>
                <dd>
                    <asp:TextBox ID="txtReContent" runat="server" CssClass="input" TextMode="MultiLine" datatype="*" sucmsg=" " /></dd>
            </dl>
        </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <%--<asp:Button ID="btnSubmit" runat="server" Text="回复留言" CssClass="btn" OnClick="btnSubmit_Click" />--%>
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
