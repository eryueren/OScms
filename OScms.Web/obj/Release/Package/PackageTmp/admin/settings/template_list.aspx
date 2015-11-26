<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template_list.aspx.cs" Inherits="OS.Web.admin.settings.template_list" %>
<%@ Import namespace="OS.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>模板管理</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<link href="../css/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    //删除栏目
    var u="<%= this.url%>";
    function dels(_fileName) {
        $.dialog.confirm('文件删除后不可恢复，您确定吗？', function () {
            window.location.href = "template_list.aspx?file=" + _fileName + "&action=del"+u;
        });
    }

    function del_folder(_folderName) {
        $.dialog.confirm('删除此文件夹以及此文件夹下所有文件，您确定吗？', function () {
            window.location.href = "template_list.aspx?folder=" + _folderName + "&action=del";
        });
    }

    //创建文件夹
    function createFolder() {
        var dialog = $.dialog({
            title: '创建目录',
            content: 'url:../dialog/dialog_folder.aspx',
            min: false,
            max: false,
            lock: true,
            width: 500,
            height: 100
        });
    }
    //修改文件名称
    function editFolderName(_name) {
        var dialog = $.dialog({
            title: '修改名称',
            content: 'url:../dialog/dialog_folder.aspx?name=' + _name,
            min: false,
            max: false,
            lock: true,
            width: 500,
            height: 100
        });
    }
    //发送AJAX请求
    function sendAjaxUrl(winObj, postData, sendUrl) {
        $.ajax({
            type: "post",
            url: sendUrl,
            data: postData,
            dataType: "json",
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.dialog.alert('尝试发送失败，错误信息：' + errorThrown, function () { }, winObj);
            },
            success: function (data, textStatus) {
                if (data.status == 1) {
                    winObj.close();
                    $.dialog.tips(data.msg, 2, '32X32/succ.png', function () { location.reload(); }); //刷新页面
                } else {
                    $.dialog.alert('错误提示：' + data.msg, function () { }, winObj);
                }
            }
        });
    }

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
</div>
<!--/导航栏-->

<!--工具栏-->
<div class="toolbar-wrap">
  <div id="floatHead" class="toolbar">
    <div class="l-list">
      <ul class="icon-list">
        <%if(string.IsNullOrEmpty(skinName)) {%>
        <li><a href="javascript:createFolder();">创建目录</a></li>
        <%} %>
        <li><a href="template_edit.aspx?action=<%=YLEnums.ActionEnum.Add %><%= url%>">创建文件</a></li>

         <asp:Label ID="lab" runat="server" Font-Bold="True" Font-Size="9pt" ForeColor="Red"></asp:Label>
       </ul> 
         
    </div>
  </div>
</div>
<!--/工具栏-->

<!--列表-->
<asp:Repeater ID="rptList" runat="server">
<HeaderTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
  <tr>
    <th width="%">选择</th>
    <th align="left">文件名称</th>
    <th align="left" width="20%">创建时间</th>
    <th align="left" width="20%">最后修改时间</th>
    <th width="10%">操作</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" />
      <asp:HiddenField ID="hideName" runat="server" Value='<%#Eval("name") %>' />
    </td>
    <td><a href="template_edit.aspx?action=<%=YLEnums.ActionEnum.Edit %>&filename=<%#Eval("name")%>"><%#Eval("name")%></a></td>
    <td><%#Eval("creationtime")%></td>
    <td><%#Eval("updatetime")%></td>
    <td align="center"><a href="template_edit.aspx?action=<%=YLEnums.ActionEnum.Edit %>&filename=<%#Eval("name")%>">编辑</a></td>
  </tr>
</ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暂无文件</td></tr>" : ""%>
</table>
</FooterTemplate>
</asp:Repeater>
<!--/列表-->

<div id="File_List" runat="server"></div>

<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
  <tr>
    <th width="10px"></th>
    <th align="left" width="20%">名称</th>
    <th align="left" width="10%">类型</th>
    <th align="left" width="10%">大小(byte)</th>
    <th align="left" width="20%">最后修改时间</th>
    <th align="left">操作</th>
  </tr>
<%= list()%>

</table>
</form>
</body>
</html>
