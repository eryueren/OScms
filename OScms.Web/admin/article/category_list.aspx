<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="category_list.aspx.cs" Inherits="OS.Web.admin.article.category_list" %>
<%@ Import namespace="OS.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>内容类别</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<link href="../css/default/style.css" rel="stylesheet" type="text/css" />
<script src="../js/layout.js" type="text/javascript"></script>
<script src="../js/dtree0.js" type="text/javascript"></script>
    <script type="text/javascript">
        //栏目排序
        function UpDown(id, type) {
            $.ajax({
                url: "category_UpDown.ashx?id=" + id + "&type=" + type,
                type: "POST",
                dataType: "html",
                error: function () { alert("123"); },
                success: function (data) {
                    if (data == "OK") { window.location.href = "category_list.aspx"; }
                    else if (data == "up") {  $.dialog.alert("已经在最前面了"); }
                    else if (data == "down") { $.dialog.alert("已经在最后面了"); }
                    else { alert(data) };
                }
            });
        }

        //完成订单
        function OrderComplete() {
            var dialog = $.dialog.confirm('订单完成后，订单处理完毕，确认要继续吗？', function () {
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_complete" };
                //发送AJAX请求
                sendAjaxUrl(dialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            });
        }

        //删除栏目
        function del(id) {
            $.dialog.confirm('本操作会删除本栏目和下属子栏目,以及所属栏目下的所有文章，确认要继续吗？', function () {
                window.location.href = "category_list.aspx?id=" + id + "&action=del";
            });
        }

        //移动栏目
        function categoey(id) {
            var dialog = $.dialog({
                title: '移动栏目',
                content: 'url:../dialog/dialog_category.aspx?id=' + id,
                min: false,
                max: false,
                lock: true,
                width: 400,
                height: 350
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
  <span>内容类别</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div class="toolbar-wrap">
  <div id="floatHead" class="toolbar">
    <div class="l-list">
      <ul class="icon-list">
        <li><a class="add" href="category_edit.aspx?action=<%=OSEnums.ActionEnum.Add %>&channel_id=<%=get_channel_id() %>&id=1"><i></i><span>新增栏目</span></a></li>
        <li><a href="javascript: d.openAll();">展开所有菜单</a></li>
        <li><a href="javascript: d.closeAll();">关闭所有菜单</a></li>
         </ul>
    </div>
  </div>
</div>
<!--/工具栏-->
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
    <tr>
        <td style="width: 100%;margin-left:10px;">
            <script type="text/javascript">
              d = new dTree('d');
              d.add(1,-2,'<%=siteConfig.webname %>');
              <asp:Repeater ID="ColumnList" runat="server">
                 <ItemTemplate>                                                   
                      d.add(<%# Eval("Id") %>,<%# Eval("parent_id") %>, '<%# Eval("Title") %>&nbsp;&nbsp;<%# Column_Access(Convert.ToInt32(Eval("ID"))) %>  <%# Column_UpDown(Convert.ToInt32(Eval("ID")))%>  ');
                 </ItemTemplate>
               </asp:Repeater>
              document.write(d);
            </script>
        </td>
    </tr>
</table>

<!--/列表-->
</form>
</body>
</html>
