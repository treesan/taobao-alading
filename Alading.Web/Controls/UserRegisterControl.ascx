<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserRegisterControl.ascx.cs"
    Inherits="Alading.Web.Controls.UserRegisterControl" %>
<%--<script type="text/javascript">
//    $(document).ready(function() {
//        $.formValidator.initConfig({ formid: "<%= this.Page.Form.ClientID %>", onerror: function(msg) { alert(msg) }, onsuccess: function() { alert('ddd'); return false; } });

//        $("#<%=txUserAccount.ClientID %>").ajaxValidator({
//            type: "get",
//            url: "Check.aspx",
//            datatype: "json",
//            success: function(data) {
//                if (data == "1") {
//                    return true;
//                }
//                else {
//                    return false;
//                }
//            },
//            error: function() { alert("服务器没有返回数据，可能服务器忙，请重试"); },
//            onerror: "该用户名不可用，请更换用户名",
//            onwait: "正在对用户名进行合法性校验，请稍候..."
//        });
//        $("#<%=txPassword.ClientID %>").formValidator({ onshow: "请输入密码", onfocus: "密码不能为空", oncorrect: "密码合法" }).inputValidator({ min: 1, empty: { leftempty: false, rightempty: false, emptyerror: "密码两边不能有空符号" }, onerror: "密码不能为空,请确认" });

    //    });
    var iCallID = null;
    var sUrl = "../Service/WebService.asmx";
    function CheckUser() {
        var oService = document.getElementById("service");

        oService.useService(sUrl, "WS_OpService");
        iCallID = service.WS_OpService.callService("HelloWorld?x=abc");
    }

    function onWebServiceResult() {
        var oResult = window.event.result;

        if (oResult.id == iCallID) {
            var oDiv = document.getElementById("Result");

            if (oResult.error) {
                alert("An error occurred: " + oResult.errorDetail.string);
            } else {
                alert("The result is: " + oResult.value);
            }
        }
    }


</script>
<div id="service" style="behavior:url(webservice.htc)" onresult="onWebServiceResult()"></div> --%>
<div>
    <table border="0">
        <tr>
            <td>
                用户姓名<span style="color: #f00;">(*)</span>：
            </td>
            <td>
                <asp:TextBox ID="txUserName" runat="server" CssClass="textbox2" MaxLength="20"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="用户姓名为必填项"
                    ControlToValidate="txUserName" Display="Dynamic">用户姓名为必填项</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                用户帐号<span style="color: #f00;">(*)</span>：
            </td>
            <td>
                <asp:TextBox ID="txUserAccount" runat="server" CssClass="textbox2" MaxLength="20"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="用户帐号为必填项"
                    ControlToValidate="txUserAccount" Display="Dynamic" ValidationGroup="userAccount">用户帐号为必填项</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="帐号长度为6~20个字符" ControlToValidate="txUserAccount"
                        Display="Dynamic" ValidationExpression="\S{6,16}" ValidationGroup="userAccount"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                帐号密码<span style="color: #f00;">(*)</span>：
            </td>
            <td>
                <asp:TextBox ID="txPassword" runat="server" TextMode="Password" CssClass="textbox2"
                    MaxLength="16"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="帐号密码为必填项"
                    ControlToValidate="txPassword" Display="Dynamic" ValidationGroup="pwd">帐号密码为必填项</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="密码长度为6~16位"
                    Display="Dynamic" ValidationExpression="\S{6,16}" ValidationGroup="pwd" 
                    ControlToValidate="txPassword"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                重复密码<span style="color: #f00;">(*)</span>：
            </td>
            <td>
                <asp:TextBox ID="txRepeat" runat="server" TextMode="Password" CssClass="textbox2"
                    MaxLength="16"></asp:TextBox>
            </td>
            <td>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="密码输入不一致"
                    ControlToValidate="txRepeat" ControlToCompare="txPassword" Display="Dynamic">密码输入不一致</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                固定电话：
            </td>
            <td>
                <asp:TextBox ID="txTel" runat="server" CssClass="textbox2" MaxLength="16"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                移动电话：
            </td>
            <td>
                <asp:TextBox ID="txMobile" runat="server" CssClass="textbox2" MaxLength="16"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                公司名称：
            </td>
            <td>
                <asp:TextBox ID="txCompany" runat="server" CssClass="textbox2" MaxLength="256"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                公司地址：
            </td>
            <td>
                <asp:TextBox ID="txAddress" runat="server" CssClass="textbox2" MaxLength="256"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="完成" />
                <input id="btnReset" type="reset" value="重置" />
            </td>
        </tr>
    </table>
</div>
