<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="OScms.Web.index" %>

<%@ Import Namespace="OS.Web.UI" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= BasePage.config.webname %></title>
    <%= BasePage.MetaInfo( BasePage.config.webkeyword,BasePage.config.webdescription)  %>
    <%--<link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon" />--%>
    <link href="css/css.css" rel="stylesheet" />
    <script language="JavaScript">
        window.onload=function(){
            document.getElementById('menu95').className="hover";}
<!-- 
    function txt(){
        var textfieldshuru=eval(document.getElementById('textfieldshuru')).value;
        if (textfieldshuru=""){
            alert("请输入金额!");
            return false;
        }
        if (!isNaN(textfieldshuru)){
            alert("输入金额为数字!");
            return false;
        }
    }
        function shou(){
            var result=0;
            var shuru=eval(document.getElementById('textfieldshuru')).value;
       
            shuru= parseInt(shuru);
            if ((shuru>0)&&(shuru<=10000)){
                result=50;
            }
            else if ((shuru>10000)&&(shuru<=100000)){
                result=shuru*0.025-200;
            }
            else if ((shuru>100000)&&(shuru<=200000)){
                result=shuru*0.02+300
            }
            else if ((shuru>200000)&&(shuru<=500000)){
                result=shuru*0.015+1300
            }
            else if ((shuru>500000)&&(shuru<=1000000)){
                result=shuru*0.01+3800
            }
            else if ((shuru>1000000)&&(shuru<=2000000)){
                result=shuru*0.009+4800
            }
            else if ((shuru>2000000)&&(shuru<=5000000)){
                result=shuru*0.008+6800
            }
            else if ((shuru>5000000)&&(shuru<=10000000)){
                result=shuru*0.007+11800
            }
            else if ((shuru>10000000)&&(shuru<=20000000)){
                result=shuru*0.006+21800
            }
            else if (shuru>20000000){
                result=shuru*0.005+41800
            }
            document.getElementById("textfieldsuan").value=result;
        }
        function bao(){
            var result=0;
            var shuru=eval(document.getElementById('textfieldshuru')).value;
            shuru= parseInt(shuru);
            if ((shuru>0)&&(shuru<=1000)){
                result=30
            }
            else if ((shuru>1000)&&(shuru<=100000)){
                result=shuru*0.01+20
            }
            else if (shuru>100000){
                result=shuru*0.005+520
            }
            if ((result>=5000)){
                result=5000
            }
            document.getElementById("textfieldsuan").value=result;
        }
        function zhi(){
            var result=0;
            var shuru=eval(document.getElementById('textfieldshuru')).value;
            shuru= parseInt(shuru);
            if ((shuru>0)&&(shuru<10000)){
                result=50
            }
            else if ((shuru>=10000)&&(shuru<=500000)){
                result=shuru*0.015-100
            }
            else if ((shuru>=500000)&&(shuru<=5000000)){
                result=shuru*0.01+2400
            }
            else if (shuru>10000000){
                result=shuru*0.001+67400
            }
            document.getElementById("textfieldsuan").value=result;
        }
        function shouc(){
            var result=0;
            var shuru=eval(document.getElementById('textfieldshuru')).value;
            shuru= parseInt(shuru);
            if (shuru>15010){
                result=(shuru-10010)/0.005;
            }
            else if ((shuru>10010)&&(shuru<=15010)){
                result=(shuru-5010)/0.01;
            }
            else if ((shuru>5510)&&(shuru<=10010)){
                result=(shuru-2510)/0.015
            }
            else if ((shuru>3010)&&(shuru<=5510)){
                result=(shuru-1510)/0.02
            }
            else if ((shuru>2010)&&(shuru<=3510)){
                result=(shuru-1510)/0.03
            }
            else if ((shuru>50)&&(shuru<=2010)){
                result=(shuru-10)/0.04
            }
            else if (shuru=50){
                result="诉讼标的<=1000元"
            }
            document.getElementById("textfieldsuan").value=result;
        }
        function baoc(){
            var result=0;
            var shuru=eval(document.getElementById('textfieldshuru')).value;
            shuru= parseInt(shuru);
            if (shuru>1020){
                result=(shuru-520)/0.005
            }
            else if ((shuru>30)&&(shuru<=1020)){
                result=(shuru-20)/0.01
            }
            else if (shuru=30){
                result="保全标的<=1000元"
            }
            document.getElementById("textfieldsuan").value=result;
        }
        function zhic(){
            var result=0;
            var shuru=eval(document.getElementById('textfieldshuru')).value;
            if (shuru>2500){
                result=(shuru-2000)/0.001
            }
            else if ((shuru>50)&&(shuru<=2500)){
                result=shuru/0.005
            }
            else if (shuru=50){
                result="执行标的<=10000元"
            }
            document.getElementById("textfieldsuan").value=result;
        }
        -->
    </script>
</head>
<body>
    <form id="form1" runat="server" name="formsuan">
        <table width="1002" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td bgcolor="#F8F7F2">
                    <img src="images/logo.jpg" width="580" height="85" /></td>
            </tr>
            <tr>
                <td>
                    <ul id="nav">
                        <%--获取菜单项--%>
                        <%= BasePage.GetMenuLst() %>
                    </ul>
                </td>
            </tr>
        </table>
        <table width="1002" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <%=theFirstPhoto %>
                </td>
            </tr>
            <tr>
                <td height="5"></td>
            </tr>
        </table>
        <table width="1002" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="350" valign="top">
                    <table width="337" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="282" height="28" class="title01">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>精英团队</strong></td>
                            <td width="55" class="title01-2"><a href="info.aspx?top=134" class="hese" target="_blank">更多</a></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="325" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="25">
                                            <p><strong class="hese">我们的主任</strong></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <asp:Repeater ID="repZhuRen" runat="server">
                                            <ItemTemplate>
                                                <td bgcolor="#f4f3f3">
                                                    <table width="325" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="98" height="100" align="center"><a href="info.aspx?top=<%#Eval("id")%>" target="_blank">
                                                                <img src="<%#Eval("img_url") %>" width="75" height="80" border="0" /></a></td>
                                                            <td width="227" align="left" valign="top"><strong class="lan"><a href="info.aspx?top=<%#Eval("id")%>" target="_blank"><%#Eval("title") %></a></strong><br />
                                                                <a href="info.aspx?top=<%#Eval("id")%>" target="_blank"><%#Eval("zhaiyao") %></a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </tr>
                                </table>
                                <table width="325" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="25">
                                            <p><strong class="hese">我们的专家顾问</strong></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="60" valign="top" bgcolor="#F4F3F3">
                                            <ul class="listTytp01">
                                                <asp:Repeater ID="guwen" runat="server">
                                                    <ItemTemplate>
                                                        <li><a href="info.aspx?top=134&cid=<%#Eval("id")%>" target="_blank"><%#Eval("title")%> </a></li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <li><a href="info.aspx?top=134" target="_blank">更多.. </a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                </table>
                                <table width="325" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="25">
                                            <p><strong class="hese">我们的队伍</strong></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="60" valign="top" bgcolor="#F4F3F3">
                                            <ul class="listTytp01">
                                                <asp:Repeater ID="duiwu" runat="server">
                                                    <ItemTemplate>
                                                        <li><a href="info.aspx?top=134&cid=<%#Eval("id")%>" target="_blank"><%#Eval("title")%> </a></li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <li><a href="info.aspx?top=134" target="_blank">更多.. </a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                    <table width="337" border="0" cellspacing="0" cellpadding="0">
                        <tr class="title01">
                            <td width="282" height="28" class="title01">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>业务领域</strong></td>
                            <td width="55" class="title01-2"><a href="info.aspx?top=142" class="hese">更多</a></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="325" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="100" valign="top">
                                            <ul class="listTytp02">
                                                <%= BasePage.GetMenuAndChildLstByFid(135) %>
                                                <li><a href="info.aspx?top=142">更多.. </a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="455" valign="top">
                    <table width="441" border="0" cellspacing="0" cellpadding="0">
                        <tr class="title02">
                            <td width="392" height="24">&nbsp;&nbsp;&nbsp;&nbsp;<strong>最新新闻</strong></td>
                            <td width="49"><a href="info.aspx?top=136">更多</a></td>
                        </tr>
                        <tr>
                            <td height="100" colspan="2" align="left" valign="top">
                                <ul class="listTytp03">
                                    <asp:Repeater ID="repNews" runat="server">
                                        <ItemTemplate>
                                            <li><a href="info.aspx?top=<%#Eval("category_id") %>&cid=<%#Eval("id") %>" target="_blank"><%#Eval("title")%> <span><%#Convert.ToDateTime(Eval("add_time")).ToString("yyyy-MM-dd") %></span></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                    <table width="441" border="0" cellspacing="0" cellpadding="0">
                        <tr class="title02">
                            <td width="392" height="24">&nbsp;&nbsp;&nbsp;&nbsp;<strong>本所简介</strong></td>
                            <td width="49"><a href="info.aspx?top=132">更多</a></td>
                        </tr>
                        <tr>
                            <td height="100" colspan="2">
                                <img src="<%= BasePage.GetArticleNameUrl(132) %>" width="441" height="90" /></td>
                        </tr>
                        <tr>
                            <td height="100" colspan="2" align="left" valign="top"><%= BasePage.GetPageZhaiYao(132,255) %></td>
                        </tr>
                    </table>
                    <table width="441" border="0" cellspacing="0" cellpadding="0">
                        <tr class="title02">
                            <td width="392" height="24">&nbsp;&nbsp;&nbsp;&nbsp;<strong>收费原则</strong></td>
                            <td width="49"><a href="info.aspx?top=139">更多</a></td>
                        </tr>
                        <tr>
                            <td height="100" colspan="2" align="left" valign="top">
                                <ul class="listTytp02">
                                    <li><a href="info.aspx?top=139#1">一审案件 </a></li>
                                    <li><a href="info.aspx?top=139#2">二审案件 </a></li>
                                    <li><a href="info.aspx?top=139#3">再审(申诉)案件 </a></li>
                                    <li><a href="info.aspx?top=139#4">执行案件 </a></li>
                                    <li><a href="info.aspx?top=139#5">非诉讼法律事务 </a></li>
                                    <li><a href="info.aspx?top=139#6">法律咨询</a></li>
                                    <li><a href="info.aspx?top=139#7">代书法律事务文书 </a></li>
                                    <li><a href="info.aspx?top=139#8">涉外案件 </a></li>
                                    <li><a href="info.aspx?top=139#9">法律援助案件 </a></li>

                                </ul>
                            </td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                </td>
                <td valign="top">
                    <table width="97%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <img src="images/tel.jpg" width="183" height="175" /></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <p>&nbsp;</p>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <img src="images/jsq.jpg" width="182" height="31" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td height="20" colspan="3" align="center"><b>&nbsp;输入标的金额</b>:</td>
                                                </tr>
                                                <tr>
                                                    <td height="20" colspan="3" align="center">
                                                        <input name="textfieldshuru" id="textfieldshuru" /></td>
                                                </tr>
                                                <tr>
                                                    <td height="20" align="center">
                                                        <input onclick="shou()" type="button" value="受理费" name="buttonshou" /></td>
                                                    <td height="20" align="center">
                                                        <input onclick="bao()" type="button" value="保全费" name="buttonbao" /></td>
                                                    <td height="20" align="center">
                                                        <input onclick="zhi()" type="button" value="执行费" name="buttonzhi" /></td>
                                                </tr>
                                                <tr>
                                                    <td height="20" colspan="3" align="left">计算结果：
                       <input
                           name="textfieldsuan" id="textfieldsuan" size="10" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="20" colspan="3" align="center">
                                                        <input type="reset" value="清除" name="Submit" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <p>&nbsp;</p>
                            </td>
                        </tr>
                        <tr>
                            <td><a href="info.aspx?top=137">
                                <img src="images/cgan.jpg" width="184" height="72" border="0" /></a></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="1002" border="0" align="center" cellpadding="0" cellspacing="0" id="bottom">
            <tr>
                <td height="49" align="right" bgcolor="#393939"><%= BasePage.config.webcopyright %><a href="http://www.sc98.cn/" target="_blank">网站建设</a>：<a href="http://www.sc798.com/" target="_blank">收获成功</a>&nbsp;&nbsp;</td>
            </tr>
        </table>

    </form>
</body>
</html>
