<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template_edit.aspx.cs" Inherits="OS.Web.admin.settings.template_edit" ValidateRequest="false" %>
<%@ Import namespace="OS.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑后台导航</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link href="../css/default/style.css" rel="stylesheet" type="text/css" />
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
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>控制面板</span>
  <i class="arrow"></i>
  <a href="template_list.aspx"><span>模板管理</span></a>
  <i class="arrow"></i>
  <span>编辑模板</span>
</div>
<!--/导航栏-->

<!--内容-->
<div class="content-tab-wrap">
  <div id="floatHead" class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a href="javascript:;" onclick="tabs(this);" class="selected">编辑模板内容</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content"> 

<%if (this._action == "Edit")
  {%>

<dl <%= style%>>
    
    <dt>文件名称</dt>
    <dd>
    <%= menu%>
    </dd>
  </dl>
  <%}
  else
  { %>
   <dl>
    <dt>文件类型</dt>
    <dd>
        <div class="rule-multi-radio">
        <asp:RadioButtonList ID="fileType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"  >
                 <asp:ListItem Value=".html" Selected="True">.html</asp:ListItem>    
                 <asp:ListItem Value=".css">.css</asp:ListItem>     
                 <asp:ListItem Value=".js">.js</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
    <dl>
    <dt>文件名称</dt>
    <dd> <%=this.sl %><asp:TextBox ID="txtFileName" runat="server" CssClass="input txt" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/" sucmsg=" "  ajaxurl="../../ashx/admin_ajax.ashx?action=file_validate"></asp:TextBox><span class="Validform_checktip">*字母、下划线</span></dd>
  </dl>
  <%} %>
  <dl>
    <dt>文件内容</dt>
    <dd>
      <asp:TextBox ID="txtContent" runat="server" CssClass="input" TextMode="MultiLine" style="width:96%;height:450px;"></asp:TextBox>
    </dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
</div>
<!--/工具栏-->
</form>
</body>
</html>
