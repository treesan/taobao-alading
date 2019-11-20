<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditUserControl.ascx.cs"
    Inherits="Alading.Web.Controls.EditUserControl" %>
<div>
    <table border="0">
        <tr>
            <td>
                用户姓名<span style="color:#f00;">(*)</span>：
            </td>
            <td>
                <asp:TextBox ID="txUserName" runat="server" CssClass="textbox2"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="用户姓名为必填项"
                    ControlToValidate="txUserName" Display="Dynamic">用户姓名为必填项</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                固定电话：
            </td>
            <td>
                <asp:TextBox ID="txTel" runat="server" CssClass="textbox2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                移动电话：
            </td>
            <td>
                <asp:TextBox ID="txMobile" runat="server" CssClass="textbox2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                公司名称：
            </td>
            <td>
                <asp:TextBox ID="txCompany" runat="server" CssClass="textbox2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                公司地址：
            </td>
            <td>
                <asp:TextBox ID="txAddress" runat="server" CssClass="textbox2"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td>
                
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="完成" />
                <asp:Button ID="bntCancel" runat="server" Text="取消" UseSubmitBehavior="False" PostBackUrl="~/UserInfo.aspx" CausesValidation="False" />
            </td>
        </tr>
    </table>
</div>
