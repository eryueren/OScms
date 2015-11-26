<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="OS.Web.Admin.Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/skin.css"/>
</head>
<body>
<form id="form1" runat="server">
<div class="top">
	<div class="logo"><img src="images/logo.gif" alt="logo" /></div>    
    <div class="topMenu">
        <ul>
            <%= get_menu()%>
            <li class="topMenuIco_6"><a href="Loginout.aspx" target="_top" >安全退出</a></li>
        </ul>
    </div><br />
	<div class="loginCustom" >您好，<span > <%=admin_info.user_name %></span><i>(<%=new OS.BLL.managers.manager_role().GetTitle(admin_info.role_id) %>)</i>，欢迎进入后台管理中心</div> 
</div>
</form>
</body>
</html>
