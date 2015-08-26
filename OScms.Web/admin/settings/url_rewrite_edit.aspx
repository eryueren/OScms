<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="url_rewrite_edit.aspx.cs" Inherits="OS.Web.admin.settings.url_rewrite_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑URL配置</title>
<script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<link href="../css/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //添加按钮(点击绑定)
        $("#itemAddButton").click(function () {
            showUrlDialog();
        });
    });

    //创建窗口
    function showUrlDialog(obj) {
        var objNum = arguments.length;
        var m = $.dialog({
            fixed: true,
            lock: true,
            max: false,
            min: false,
            title: "重写表达式",
            content: 'url:../dialog/dialog_rewrite.aspx',
            width: 750
        });
        //如果是修改状态，将对象传进去
        if (objNum == 1) {
            m.data = obj;
        }
    }

    //删除节点
    function delUrlNode(obj) {
        $(obj).parent().parent().remove();
    }
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="url_rewrite_list.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>URL配置管理</span>
  <i class="arrow"></i>
  <span>编辑URL配置</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div class="content-tab-wrap">
  <div id="floatHead" class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a href="javascript:;" onclick="tabs(this);" class="selected">编辑URL配置</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">

  <dl>
    <dt>调用名称</dt>
    <dd><asp:TextBox ID="txtName" runat="server" CssClass="input txt" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*调用该条URL配置信息的名称，不可重复</span></dd>
  </dl>
  <dl>
    <dt>源文件名</dt>
    <dd>
      <asp:TextBox ID="txtPage" runat="server" CssClass="input txt" datatype="*" sucmsg=" " />
      <span class="Validform_checktip">*源文件扩展名必须为.aspx</span>
    </dd>
  </dl>
    <dl>
    <dt>模板文件名</dt>
    <dd>
      <asp:TextBox ID="txtTemplet" runat="server" CssClass="input txt" />
      <span class="Validform_checktip">*该页面的模板名称，扩展名一般是.html</span>
    </dd>
  </dl>
  <dl>
    <dt>URL表达式</dt>
    <dd>
      <a id="itemAddButton" class="icon-btn add"><i></i><span>添加表达式</span></a>
      <span class="Validform_checktip">*注意，不添加任何表达式则视为不重写</span>
    </dd>
  </dl>
  <dl>
    <dt></dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <thead>
          <tr>
            <th width="30%">重写表达式</th>
            <th width="30%">正则表达式</th>
            <th width="30%">传输参数</th>
            <th width="10%">操作</th>
          </tr>
        </thead>
        <tbody id="var_box">
          <asp:Repeater ID="rptList" runat="server">
          <ItemTemplate>
          <tr class="td_c">
            <td>
              <input type="text" name="itemPath" class="td-input" value="<%#Eval("path")%>" style="width:90%;" readonly="readonly" />
            </td>
            <td>
              <input type="text" name="itemPattern" class="td-input" value="<%#Eval("pattern")%>" style="width:90%;" readonly="readonly" />
            </td>
            <td>
              <input type="text" name="itemQuerystring" class="td-input" value="<%#Eval("querystring")%>" style="width:90%;" readonly="readonly" />
            </td>
            <td>
              <a title="编辑" class="img-btn edit operator" onclick="showUrlDialog(this);">编辑</a>
              <a title="删除" class="img-btn del operator" onclick="delUrlNode(this);">删除</a>
            </td>
          </tr>
          </ItemTemplate>
          </asp:Repeater>
        </tbody>
      </table>
    </dd>
  </dl>
</div>
<!--/内容-->


<!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
</div>
<!--/工具栏-->
</form>
</body>
</html>
