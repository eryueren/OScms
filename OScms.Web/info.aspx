<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="OScms.Web.info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            var mid = <%=id %>;
            if (mid==160||mid==161||mid==162) {
                document.getElementById('menu136').className = "hover";   
            } else {
                document.getElementById('menu<%=id %>').className = "hover";
            }
            
        } </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="235" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="90" align="center">
                <a href="info.aspx?top=137">
                    <img src="images/cgan.jpg" width="220" height="72" /></a></td>
        </tr>
    </table>
    <td width="757" valign="top">
        <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">

            <tr>
                <td height="30" bgcolor="#F3F0E1">&nbsp;&nbsp;<img src="images/ico01.gif" width="6" height="6" />
                    <strong>首页 &gt; <%= OS.Web.UI.BasePage.GetPageNameUrl(id) %></strong></td>
            </tr>
            <% switch (TemplateId) { %>
            <%--单页--%>
            <% case 1: %>
            <% if (cId == 0) %>
            <%{ %>
            <tr>
                <td>
                    <table width="95%" border="0" align="center" cellpadding="10" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Repeater ID="repDanYe" runat="server">
                                    <ItemTemplate>
                                        <div class="body_int_gk_right">
                                            <%#Eval("content") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                </td>
            </tr>
            <% } %>
            <%else { %>
            <div class="body_int_hd body_int_xw">
                <!--详细页-->
                <tr>
                    <td>
                        <table width="95%" border="0" align="center" cellpadding="10" cellspacing="0">
                            <tr>
                                <asp:Repeater ID="repDetail" runat="server">
                                    <ItemTemplate>
                                        <center> <h2><a><%#Eval("title") %></a></h2></center>
                                        <div class="body_int_xw_p">
                                            <%#Eval("content") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </table>
                        <p>&nbsp;</p>
                    </td>
                </tr>
            </div>
            <% } %>

            <!--单页END-->
            <% break; %>
            <% case 2: %>
            <!--列表-->
            <table width="660" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-left: 10px;">
                <tr>
                    <td>
                        <table width="95%" border="0" align="center" cellpadding="10" cellspacing="0">
                            <tr>
                                <td>
                                    <ul class="listTytp03">
                                        <asp:Repeater ID="repLstNews" runat="server">
                                            <ItemTemplate>
                                                <li><a href="info.aspx?top=<%#Eval("category_id") %>&cid=<%#Eval("id") %>" target="_blank"><%#Eval("title")%> <span><%#Convert.ToDateTime(Eval("add_time")).ToString("yyyy-MM-dd") %></span></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                        <p>&nbsp;</p>
                    </td>
                </tr>
            </table>
            <% break; %>
            <!--列表END-->
            <!--列表(图文)-->
            <% case 3: %>
            <asp:Repeater ID="repLstTuWen" runat="server">
                <ItemTemplate>
                    <table width="660" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-left: 20px;" class="photoList">
                        <tr>
                            <td width="574" bgcolor="#f4f3f3">
                                <table width="98%" border="0" cellspacing="0" cellpadding="0" style="padding-top: 5px;">
                                    <tr>
                                        <td width="101" height="100" align="center" valign="top"><a href="info.aspx?top=<%#Eval("category_id") %>&cid=<%#Eval("id") %>" target="_blank">
                                            <img src="<%#Eval("img_url") %>" width="80" height="100" border="0" /></a>
                                        </td>
                                        <td width="101" valign="top"><strong class="lan"><a href="info.aspx?top=<%#Eval("category_id") %>&cid=<%#Eval("id") %>" target="_blank"><%#Eval("title")%></a></strong><br />
                                            <td width="462" height="100" align="center" valign="top" style="text-align: left"><%#Eval("zhaiyao")%></a>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
            <% break; %>
            <!--列表(图文)END-->
            <% } %>
            <!--页码-->
            <div class="body_int_page" runat="server" id="pageHtml"></div>
            <div class="clear"></div>
        </table>
    </td>
</asp:Content>
