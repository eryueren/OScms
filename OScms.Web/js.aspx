<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="js.aspx.cs" Inherits="OScms.Web.js" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        window.onload=function(){
            document.getElementById('menu140').className = "hover";
        } </script>
    <script language="JavaScript">
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="235" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="90" align="center">
                <img src="images/cgan.jpg" width="220" height="72" /></td>
        </tr>
    </table>
    <td width="757" valign="top">
        <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">

            <tr>
                <td height="30" bgcolor="#F3F0E1">&nbsp;&nbsp;<img src="images/ico01.gif" width="6" height="6" />
                    <strong>首页 &gt; 诉讼计算器</strong></td>
            </tr>
            <tr>
                <td>
                    <table width="95%" border="0" align="center" cellpadding="10" cellspacing="0">
                        <tr>
                            <td align="center" valign="top">
                                <div name="formsuan">
                                    <b>
                                        <br>
                                        <table width="300" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td height="30" colspan="3" align="center"><b>&nbsp;输入标的金额</b>(诉讼/保全/执行标的):</td>
                                            </tr>
                                            <tr>
                                                <td height="30" colspan="3" align="center">
                                                    <input name="textfieldshuru" id="textfieldshuru" /></td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="center">
                                                    <input onclick="shou()" type="button" value="受理费用" name="buttonshou" /></td>
                                                <td height="30" align="center">
                                                    <input onclick="bao()" type="button" value="保全费用" name="buttonbao" /></td>
                                                <td height="30" align="center">
                                                    <input onclick="zhi()" type="button" value="执行费用" name="buttonzhi" /></td>
                                            </tr>
                                            <tr>
                                                <td height="30" colspan="3" align="left"><strong>费用计算结果：</strong>
                                                    <input
                                                        name="textfieldsuan" id="textfieldsuan" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" colspan="3" align="center">
                                                    <input type="reset" value="清除" name="Submit" /></td>
                                            </tr>
                                        </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                </td>
            </tr>
        </table>
    </td>
</asp:Content>
