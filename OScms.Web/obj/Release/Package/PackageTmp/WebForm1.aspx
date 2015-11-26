<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="OScms.Web.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="律师事务所,律师,法律,知识产权,房地产,债务,纠纷,婚姻,继承,民事,刑事,行政,海事,劳动,仲裁,公司法,金融,证券,合同纠纷,域名纠纷,免费咨询">
    <meta name="description" content="北京市广川律师事务所是由中国政法大学著名法学教授、前法律系主任裴广川发起设立的一家专家型、精英型律师事务所，拥有良好的法律专业素养与资源整合联络能力，专业分工细致，部门服务齐全，质量控制严密，律师团队思路开阔、勤勉敬业、务实诚信，能为社会各界提供及时、全面、优质、高效的专业法律服务。">
    <meta name="北京广川律师事务所" content="北京市广川律师事务所">
    <title>北京市广川律师事务所</title>
    <link rel="stylesheet" type="text/css" href="css/css.css" />
</head>

<body>
    <table width="1002" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td bgcolor="#F8F7F2">
                <img src="images/logo.jpg" width="580" height="85" /></td>
        </tr>
        <tr>
            <td>

                <ul id="nav">
                    <li><a href="index.asp?top=1" class="hover">首页</a></li>
                    <li><a href="info.asp?top=2&id=1">本所简介</a></li>
                    <li><a href="info.asp?top=3&id=2">裴广川简介</a></li>
                    <li><a href="photoList.asp?top=4">精英团队</a></li>
                    <li><a href="field.asp?top=5">业务领域</a></li>
                    <li><a href="infoList.asp?top=6">新闻中心</a></li>
                    <li><a href="info.asp?top=7&id=3">成功案例</a></li>
                    <li><a href="info.asp?top=8&id=4">我们的客户</a></li>
                    <li><a href="info.asp?top=9&id=5">收费原则</a></li>
                    <li><a href="js.asp?top=10">诉讼计算器</a></li>
                    <li><a href="info.asp?top=11&id=6">联系我们</a></li>
                </ul>
            </td>
        </tr>
    </table>
    <script language="JavaScript">
<!-- 
    function txt(){
        if (formsuan.textfieldshuru.value=""){
            alert("请输入金额!");
            formsuan.textfieldshuru.focus();
            return false;
        }
        if (!isNaN(formsuan.textfieldshuru.value)){
            alert("输入金额为数字!");
            formsuan.textfieldshuru.focus();
            return false;
        }
    }
    function shou(){
        var result=0;
        var shuru=document.formsuan.textfieldshuru.value;
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
        document.formsuan.textfieldsuan.value=result
    }
    function bao(){
        var result=0;
        var shuru=document.formsuan.textfieldshuru.value;
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
        document.formsuan.textfieldsuan.value=result
    }
    function zhi(){
        var result=0;
        var shuru=document.formsuan.textfieldshuru.value;
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
        document.formsuan.textfieldsuan.value=result
    }
    function shouc(){
        var result=0;
        var shuru=document.formsuan.textfieldshuru.value;
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
        document.formsuan.textfieldfan.value=result
    }
    function baoc(){
        var result=0;
        var shuru=document.formsuan.textfieldshuru.value;
        if (shuru>1020){
            result=(shuru-520)/0.005
        }
        else if ((shuru>30)&&(shuru<=1020)){
            result=(shuru-20)/0.01
        }
        else if (shuru=30){
            result="保全标的<=1000元"
        }
        document.formsuan.textfieldfan.value=result
    }
    function zhic(){
        var result=0;
        var shuru=document.formsuan.textfieldshuru.value;
        if (shuru>2500){
            result=(shuru-2000)/0.001
        }
        else if ((shuru>50)&&(shuru<=2500)){
            result=shuru/0.005
        }
        else if (shuru=50){
            result="执行标的<=10000元"
        }
        document.formsuan.textfieldfan.value=result
    }
    -->
    </script>
    <table width="1002" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <img src="images/banner.jpg" width="1002" height="230" /></td>
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
                        <td width="55" class="title01-2"><a href="photoList.asp" class="hese" target="_blank">更多</a></td>
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
                                    <td bgcolor="#f4f3f3">

                                        <table width="325" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="98" height="100" align="center"><a href="alma_info.asp?id=35" target="_blank">
                                                    <img src="/admin/UploadFiles/2010527151142966.jpg" width="75" height="80" border="0" /></a></td>
                                                <td width="227" align="left" valign="top"><strong class="lan"><a href="alma_info.asp?id=35" target="_blank"></a></strong>
                                                    <br />
                                                    <a href="alma_info.asp?id=35" target="_blank">裴广川，男，中共党员，北京市广川律师事务所主任，合伙人，我国著名刑法学家，中国政法大学教授，从事法学教育研究几十载，并始终坚持以司法实践为基础的理论研究，和以理论为指导...</a>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
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

                                            <li><a href="alma_info.asp?id=34" target="_blank">江平 </a></li>

                                            <li><a href="alma_info.asp?id=3" target="_blank">张晋藩 </a></li>

                                            <li><a href="alma_info.asp?id=14" target="_blank">何秉松 </a></li>

                                            <li><a href="alma_info.asp?id=4" target="_blank">樊崇义 </a></li>

                                            <li><a href="alma_info.asp?id=18" target="_blank">周忠海 </a></li>

                                            <li><a href="alma_info.asp?id=40" target="_blank">王传丽 </a></li>

                                            <li><a href="alma_info.asp?id=41" target="_blank">田  岚 </a></li>

                                            <li><a href="photoList.asp?tj=2" target="_blank">更多.. </a></li>
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

                                            <li><a href="alma_info.asp?id=27" target="_blank">裴愚 </a></li>

                                            <li><a href="alma_info.asp?id=26" target="_blank">薛武 </a></li>

                                            <li><a href="alma_info.asp?id=25" target="_blank">陈晓飞 </a></li>

                                            <li><a href="alma_info.asp?id=24" target="_blank">贺轶民 </a></li>

                                            <li><a href="alma_info.asp?id=23" target="_blank">裴莉莉 </a></li>

                                            <li><a href="alma_info.asp?id=22" target="_blank">张志杰 </a></li>

                                            <li><a href="alma_info.asp?id=21" target="_blank">温  智 </a></li>

                                            <li><a href="photoList.asp?tj=0" target="_blank">更多.. </a></li>
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
                        <td width="55" class="title01-2"><a href="field.asp" class="hese">更多</a></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="325" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td height="100" valign="top">
                                        <ul class="listTytp02">

                                            <li><a href="field.asp?id=51">业务领域 </a></li>

                                            <li><a href="field.asp?id=49">法律顾问 </a></li>

                                            <li><a href="field.asp?id=48">刑事辩护 </a></li>

                                            <li><a href="field.asp?id=47">建筑房产及物业 </a></li>

                                            <li><a href="field.asp?id=46">金融保险及税务 </a></li>

                                            <li><a href="field.asp?id=45">公司及证券 </a></li>

                                            <li><a href="field.asp?id=44">知识产权 </a></li>

                                            <li><a href="field.asp">更多.. </a></li>
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
                        <td width="49"><a href="infoList.asp">更多</a></td>
                    </tr>
                    <tr>
                        <td height="100" colspan="2" align="left" valign="top">
                            <ul class="listTytp03">

                                <li><a href="newinfo.asp?id=97">最高人民法院关于人民法院委托评估、拍卖工作的若干规... <span>2011-9-22 </span></a></li>

                                <li><a href="newinfo.asp?id=96">《关于加强廉政风险防控的指导意见》起草 掌握权力领... <span>2011-9-13 </span></a></li>

                                <li><a href="newinfo.asp?id=95">修订后的《公安机关督察条例》10月起施行 <span>2011-9-9 1</span></a></li>

                                <li><a href="newinfo.asp?id=94">乘公交遇急刹受伤 法院判决获赔万元 <span>2011-9-5 1</span></a></li>

                            </ul>
                        </td>
                    </tr>
                </table>
                <p>&nbsp;</p>
                <table width="441" border="0" cellspacing="0" cellpadding="0">
                    <tr class="title02">
                        <td width="392" height="24">&nbsp;&nbsp;&nbsp;&nbsp;<strong>本所简介</strong></td>
                        <td width="49"><a href="info.asp?top=2&id=1">更多</a></td>
                    </tr>
                    <tr>
                        <td height="100" colspan="2">
                            <img src="images/temp-2.jpg" width="441" height="90" /></td>
                    </tr>

                    <tr>
                        <td height="100" colspan="2" align="left" valign="top">本所理念：“ 广道汇川、德法相济”

北京市广川律师事务所是由中国政法大学著名法学教授、前法律系主任裴广川发起设立的一家专家型、精英型律师事务所，拥有良好的法律专业素养与资源整合联络能力，专业分工细致，部门服务齐全，...</td>
                    </tr>
                </table>
                <table width="441" border="0" cellspacing="0" cellpadding="0">
                    <tr class="title02">
                        <td width="392" height="24">&nbsp;&nbsp;&nbsp;&nbsp;<strong>收费原则</strong></td>
                        <td width="49"><a href="info.asp?id=5">更多</a></td>
                    </tr>
                    <tr>
                        <td height="100" colspan="2" align="left" valign="top">
                            <ul class="listTytp02">
                                <li><a href="info.asp?id=5#1">一审案件 </a></li>
                                <li><a href="info.asp?id=5#2">二审案件 </a></li>
                                <li><a href="info.asp?id=5#3">再审(申诉)案件 </a></li>
                                <li><a href="info.asp?id=5#4">执行案件 </a></li>
                                <li><a href="info.asp?id=5#5">非诉讼法律事务 </a></li>
                                <li><a href="info.asp?id=5#6">法律咨询</a></li>
                                <li><a href="info.asp?id=5#7">代书法律事务文书 </a></li>
                                <li><a href="info.asp?id=5#8">涉外案件 </a></li>
                                <li><a href="info.asp?id=5#9">法律援助案件 </a></li>

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
                                        <form name="formsuan">


                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td height="20" colspan="3" align="center"><b>&nbsp;输入标的金额</b>:</td>
                                                </tr>
                                                <tr>
                                                    <td height="20" colspan="3" align="center">
                                                        <input name="textfieldshuru" /></td>
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
                            name="textfieldsuan" size="10" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="20" colspan="3" align="center">
                                                        <input type="reset" value="清除" name="Submit" /></td>
                                                </tr>
                                            </table>
                                        </form>
                                    </td>
                                </tr>
                            </table>
                            <p>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td><a href="http://115.47.5.3:8016/info.asp?top=7&amp;id=3">
                            <img src="images/cgan.jpg" width="184" height="72" border="0" /></a></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="1002" border="0" align="center" cellpadding="0" cellspacing="0" id="bottom">
        <tr>
            <td height="49" align="right" bgcolor="#393939">Copyright © 2009 Beijingshi guangchuan lushi shiwusuo All Rights Reserved <a href="http://www.sc98.cn/" target="_blank">网站建设</a>：<a href="http://www.sc798.com/" target="_blank">收获成功</a>&nbsp;&nbsp;</td>
        </tr>
    </table>

</body>
</html>
