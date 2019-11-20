<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="Alading.Web.EditUser" MasterPageFile="~/NewSite.Master" %>
<%@ Register Src="~/Controls/EditUserControl.ascx" TagName="EditUserControl" TagPrefix="WebUc" %>


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
        <h2 class="tl">修改帐号</h2>
         <div class="part">
        <p style="color:#666;">带<span style="color:#f00;">(*)</span>为必填项</p>
        <WebUc:EditUserControl ID="EditUserControl1" runat="server" ></WebUc:EditUserControl>
    </div>
    </div>
</asp:Content>