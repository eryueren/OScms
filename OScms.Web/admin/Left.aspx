<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="OS.Web.Admin.Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>

    <link rel="stylesheet" type="text/css" href="css/skin.css"/>
    <script src="../scripts/jquery/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/dtree.js" type="text/javascript"></script>


</head>
<body>

<div class="mainLeft">
 <div class ="menuTitle"><span> <%= className%></span></div>

<%if (type == 1)
  { %>
  <ul style="margin-left:15px;">
           <script type="text/javascript">

              d = new dTree('d');
               d.add(1,-2,'<%=siteConfig.webname %>');
              <asp:Repeater ID="ColumnList" runat="server">
                 <ItemTemplate>                                                   
                    d.add(<%# Eval("id") %>,<%# Eval("parent_id") %>, '<%# GetColumn(Convert.ToInt32(Eval("id"))) %>');
                 </ItemTemplate>
               </asp:Repeater>
              document.write(d);
            </script>
</ul>
<%}else{ %>
<%= BindType()%>
<%} %>

</div>
 <script type="text/javascript">
     $(".dtree .dTreeNode:first").css("font-size", "120%")
     $(".dtree .dTreeNode:first").css("font-family", "微软雅黑")
     $(".dtree .dTreeNode:first").css("padding-bottom", "5px")
</script>
</body>

</html>
