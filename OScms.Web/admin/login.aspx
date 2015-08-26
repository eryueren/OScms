<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="OS.Web.admin.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>管理员登录</title>
    <link rel="stylesheet" href="css/login.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="head">
            <div class="center">
                <!--    	<div class="head_left">
        <a href="#">admin admin</a>
        </div>-->
                <div class="head_right"><a href="../index.aspx">Back to Homepage</a> </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="center">
            <div class="login_contaner">
                <div class="login_header">
                    <h3>后台登陆</h3>
                </div>
                <div class="login_contant">
                    <div class="username">
                        <label>用户名</label>
                        <div>
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="psw">
                        <label>密码</label>
                        <div>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="username">
                        <label>管理认证码</label>
                        <div>
                            <asp:TextBox ID="TxtManageCode" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="remember_me pull_left">
                        <asp:CheckBox ID="remember" runat="server" Text="记住我" Checked="True" />
                    </div>
                    <div class="pull_right">
                        <asp:Button ID="btnSubmit" runat="server" Text="确定" class="btn btn-warning btn-large" OnClick="btnSubmit_Click" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="foot">
                    <p id="msgtip" runat="server">请输入用户名及密码.</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
