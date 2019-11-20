<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPwd.aspx.cs" Inherits="Alading.Web.EditPwd" MasterPageFile="~/NewSite.Master" %>
<%@ Register Src="~/Controls/EditPwdControl.ascx" TagName="EditPwdControl" TagPrefix="WebUc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
#content
{
    padding-top:60px;
    padding-bottom:20px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentPlaceHolder" runat="server">
    <div class="wrapper">
        <h2 class="tl">修改密码</h2>
         <div class="part">
            <p style="color:#666;">带<span style="color:#f00;">(*)</span>为必填项</p>
            <WebUc:EditPwdControl ID="EditPwdControl1" runat="server" />
        </div>
    </div>
</asp:Content>