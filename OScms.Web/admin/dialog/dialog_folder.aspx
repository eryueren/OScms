<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_folder.aspx.cs" Inherits="OS.Web.admin.dialog.dialog_folder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>订单发货窗口</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
    <link href="../css/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //窗口API
        var api = frameElement.api, W = api.opener;
        api.button({
            name: '确定',
            focus: true,
            callback: function () {
                submitForm();
                return false;
            }
        }, {
            name: '取消'
        });

        //提交表单处理
        function submitForm() {
            //下一步，AJAX提交表单
            var postData = {
                "folderName": $("#txtFolderName").val(), "_name": $("#_name").val()
            };
           //发送AJAX请求
                W.sendAjaxUrl(api, postData, "../../ashx/admin_ajax.ashx?action=create_folder");
                return false;
        }
    </script>
</head>

<body>
<form id="form1" runat="server">
  <input type="hidden" id="_name"  runat="server" value=""/>
<div class="div-content">
  <dl>
    <dt>文件夹名</dt>
    <dd>
     <input type="text" id="txtFolderName" class="input txt" runat="server"/>
    </dd>
  </dl>
</div>
</form>
</body>
</html>