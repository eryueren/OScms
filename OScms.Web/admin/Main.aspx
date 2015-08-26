<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="OS.Web.Admin.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <%=siteConfig.webname %>-后台管理</title>
</head>
<frameset rows="91,*" cols="*" frameborder="no" border="0" framespacing="0">
  <frame src="Top.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" />
  <frameset cols="230,*" frameborder="no" border="0" framespacing="0">
    <frame src="left.aspx" name="leftFrame"  noresize="noresize" id="leftFrame" title="leftFrame" />
    <frame src="center.aspx" name="mainFrame" id="mainFrame" title="mainFrame" />
  </frameset>
</frameset>
<noframes>
</noframes>
</html>
