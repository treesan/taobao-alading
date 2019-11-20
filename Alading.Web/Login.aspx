<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AladingWeb.Login" MasterPageFile="~/NewSite.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
#content
{
    padding-top:60px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentPlaceHolder" runat="server">
    <div class="wrapper">
        <h2 class="tl">登录</h2>
        <div class="part">
        <p style="color:#666;">带<span style="color:#f00;">(*)</span>为必填项</p>
        <div>
        <table border="0">
            <tr>
                <td>
                    用户帐号<span style="color:#f00;">(*)</span>：
                </td>
                <td>
                    <asp:TextBox ID="txUserAccount" runat="server" CssClass="textbox2" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="帐号为必填项"
                        Display="Dynamic" ControlToValidate="txUserAccount">帐号为必填项</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    密码<span style="color:#f00;">(*)</span>：
                </td>
                <td>                    
                    <asp:TextBox ID="txPassword" runat="server" TextMode="Password" CssClass="textbox2"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="密码为必填项"
                        Display="Dynamic" ControlToValidate="txPassword">密码为必填项</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr align="right">
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="Button1" runat="server" Text="登录" OnClick="btnLogin_Click" />
                </td>
            </tr>
        </table>
    </div>
    </div>
    </div>
</asp:Content>