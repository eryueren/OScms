<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="category_edit.aspx.cs" Inherits="OS.Web.admin.article.category_edit" ValidateRequest="false" %>

<%@ Import Namespace="OS.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title>编辑类别</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type='text/javascript' src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <script type="text/javascript" src="../js/pinyin.js"></script>
    <link href="../css/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();

            //添加按钮(点击绑定)
            $("#itemAddButton").click(function () {
                showChannelDialog();
            });

            $("#btnVarAdd").click(function () {
                varHtml = createVarHtml();
                $("#tr_box").append(varHtml);

            });

            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../ashx/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
            });
            //初始化编辑器
            var editorMini = KindEditor.create('#txtContent', {
                width: '98%',
                height: '250px',
                resizeType: 1,
                uploadJson: '../../ashx/upload_ajax.ashx?action=EditorFile&IsWater=1',
                items: [
                    'source', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'image', 'link', 'fullscreen']
            });
        });



        function change2cn(en, cninput) {
            cninput.value = getSpell(en, "");
        }

    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="category_list.aspx" class="back"><i></i><span>返回列表页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <a href="category_list.aspx"><span>内容类别</span></a>
            <i class="arrow"></i>
            <span>编辑分类</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                        <li><a href="javascript:;" onclick="tabs(this);">扩展选项</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>所属父类别</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlParentId" runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>排序数字</dt>
                <dd>
                    <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
                    <span class="Validform_checktip">*数字，越小越向前</span>
                </dd>
            </dl>
            <dl>
                <dt>类别名称</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" onBlur="change2cn(this.value, this.form.txtCallIndex)" CssClass="input normal" datatype="*1-100" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*类别中文名称，100字符内</span></dd>
            </dl>
            <dl>
                <dt>英文名称</dt>
                <dd>
                    <asp:TextBox ID="txtCallIndex" runat="server" CssClass="input normal" datatype="/^\s*$|^[a-zA-Z0-9\-\_]{2,50}$/" errormsg="请填写正确的别名" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">类别的调用别名，只允许字母、数字、下划线</span>
                </dd>
            </dl>
            <dl>
                <dt>模板选择</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="0" Selected="True">无类型</asp:ListItem>
                            <asp:ListItem Value="1">单页模板</asp:ListItem>
                            <asp:ListItem Value="2">新闻列表</asp:ListItem>
                            <asp:ListItem Value="3">图文列表</asp:ListItem>
                            <asp:ListItem Value="4">其他</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>显示状态</dt>
                <dd>
                    <div class="rule-multi-porp">
                        <asp:CheckBoxList ID="cblItem" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">添加栏目</asp:ListItem>
                            <asp:ListItem Value="1">添加内容</asp:ListItem>
                            <asp:ListItem Value="1">头部显示</asp:ListItem>
                            <asp:ListItem Value="1">底部显示</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>开启相册功能</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cbIsAlbums" runat="server" />
                    </div>
                    <span class="Validform_checktip">*开启相册功能后可上传多张图片</span>
                </dd>
            </dl>
            <dl>
                <dt>开启附件功能</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cbIsAttach" runat="server" />
                    </div>
                    <span class="Validform_checktip">*开启附件功能后可上传多个附件。</span>
                </dd>
            </dl>
            <dl>
                <dt>分页数量</dt>
                <dd>
                    <asp:TextBox ID="txtPageSize" runat="server" CssClass="input small" datatype="n" sucmsg=" ">10</asp:TextBox>
                    <span class="Validform_checktip">*列表页每页显示数据数量</span>
                </dd>
            </dl>
            <dl>
                <dt>选择字段</dt>
                <dd>
                    <div class="rule-multi-porp">
                        <asp:CheckBoxList ID="cblAttributeField" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                    </div>
                </dd>
            </dl>
        </div>
        <div class="tab-content" style="display: none">
            <dl>
                <dt>URL链接</dt>
                <dd>
                    <asp:TextBox ID="txtLinkUrl" runat="server" MaxLength="255" CssClass="input normal" />
                    <span class="Validform_checktip">填写后直接跳转到该网址</span>
                </dd>
            </dl>
            <dl>
                <dt>显示图片1</dt>
                <dd>
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                </dd>
            </dl>
            <dl>
                <dt>显示图片2</dt>
                <dd>
                    <asp:TextBox ID="txtSeoTitle" runat="server" CssClass="input normal upload-path" sucmsg=" " />
                    <div class="upload-box upload-img"></div>
                </dd>
            </dl>
            <dl>
                <dt>显示图片3</dt>
                <dd>
                    <asp:TextBox ID="txtSeoKeywords" runat="server" CssClass="input normal upload-path"></asp:TextBox>
                    <div class="upload-box upload-img"></div>
                </dd>
            </dl>
            <dl>
                <dt>类别介绍</dt>
                <dd>
                    <textarea id="txtContent" class="editor" style="visibility: hidden;" runat="server"></textarea>
                </dd>

            </dl>

            <dl>
                <dt>SEO描述</dt>
                <dd>
                    <asp:TextBox ID="txtSeoDescription" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>
                    <span class="Validform_checktip">255个字符以内</span>
                </dd>
            </dl>
        </div>

        <!--/内容-->
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
