<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddShop.aspx.cs" Inherits="Alading.Web.AddShop" MasterPageFile="~/NewSite.Master" %>
<%@ Register Src ="~/Controls/AddShopControl.ascx" TagName="AddShopControl" TagPrefix="WebUc"  %>

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
        <h2 class="tl">
            添加商店</h2>
        <div class="part">
        <p style="color:#666;">带<span style="color:#f00;">(*)</span>为必填项</p>
        <WebUc:AddShopControl ID ="AddShopControl1" runat="server" ></WebUc:AddShopControl>
    </div>
    </div>
</asp:Content>