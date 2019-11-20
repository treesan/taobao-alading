<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="Alading.Web.UserInfo"
    MasterPageFile="~/NewSite.Master" %>

<%@ Register Src="~/Controls/ShopListControl.ascx" TagName="ShopListControl" TagPrefix="WebUc" %>
<%@ Register Src="~/Controls/UserListControl.ascx" TagName="UserListControl" TagPrefix="WebUc" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
a.user
{
    background:url('img/front/user.png') no-repeat left center;
    padding-left:18px;
    margin-right:10px;
}
a.lock
{
    background:url('img/front/lock.png') no-repeat left center;
    padding-left:18px;
    margin-right:10px;
}
a.add
{
    background:url('img/front/add.png') no-repeat left center;
    padding-left:18px;
    margin-right:10px;
    float:right;
    height:23px;
    line-height:23px;
    display:inline;
    margin-top:4px;
}
h3
{
    background:#efefef;
    border-top:#ccc 1px solid;
    padding:8px;
    margin:0;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <span style="float:right;height:40px;line-height:40px;padding-right:20px;color:#fff;">欢迎&nbsp;<%# this.userName %>&nbsp;登录用户中心</span>
    <h2 class="tl">用户中心</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="part2">
        <div>
            <p>
            <a class="user" href="EditUser.aspx?UserCode=<%#this.userCode %>">修改主号信息</a>
            <a class="lock" href="EditPwd.aspx?UserCode=<%#this.userCode %>"> 修改主号密码</a></p></div>            
        <div>   
            <div style="float:left; width:49%;">
                <asp:HyperLink runat="server" Text="添加店铺" Visible="<%# this.visibleAddShop %>" 
                    ID="link_add_shop" CssClass="add" NavigateUrl="AddShop.aspx"></asp:HyperLink>
                <h3>店铺列表</h3>
                <WebUc:ShopListControl ID="ShopListControl1" runat="server" />
                <p style="padding-left:10px;">还可以加<span><%#this.shopRemain %></span>个店铺</p>
            </div>
            
            <div style="float:right; width:49%;">
                <asp:HyperLink Visible="<%#this.visibleAddUser %>" ID="link_add_user" runat="server" Text="添加员工"  NavigateUrl="AddUser.aspx" CssClass="add"></asp:HyperLink>
                <h3>员工列表</h3>
                <WebUc:UserListControl ID="UserListControl1" runat="server" />
                <p style="padding-left:10px;">还可以加<span><%#this.userRemain %></span>个员工</p>
            </div>
            <div style="clear:both;"></div>
        </div> 
    </div>
</asp:Content>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
#content
{
    padding-top:60px;
    padding-bottom:20px;
}
a
{
    text-decoration:none;
}
a.user
{
    background:url('Content/images/user.png') no-repeat left center;
    padding-left:18px;
    margin-right:10px;
}
a.lock
{
    background:url('Content/images/lock.png') no-repeat left center;
    padding-left:18px;
    margin-right:10px;
}
a.add
{
    background:url('Content/images/add.png') no-repeat left center;
    padding-left:18px;
    margin-right:10px;
    float:right;
    height:23px;
    line-height:23px;
    display:inline;
    margin-top:5px;
}
#content h3
{
    background:#efefef;
    border-top:#ccc 1px solid;
    padding:8px;
    margin:0;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentPlaceHolder" runat="server">
    <div class="wrapper">
        <div class="part2">
        <div>
            <p>
            <a class="user" href="EditUser.aspx?UserCode=<%#this.userCode %>">修改主号信息</a>
            <a class="lock" href="EditPwd.aspx?UserCode=<%#this.userCode %>"> 修改主号密码</a></p></div>            
        <div>   
            <div style="float:left; width:49%;">
                <asp:HyperLink runat="server" Text="添加店铺" Visible="<%# this.visibleAddShop %>" 
                    ID="link_add_shop" CssClass="add" NavigateUrl="AddShop.aspx"></asp:HyperLink>
                <h3>店铺列表</h3>
                <WebUc:ShopListControl ID="ShopListControl1" runat="server" />
                <p style="padding-left:10px;">还可以加<span><%#this.shopRemain %></span>个店铺</p>
            </div>
            
            <div style="float:right; width:49%;">
                <asp:HyperLink Visible="<%#this.visibleAddUser %>" ID="link_add_user" runat="server" Text="添加员工"  NavigateUrl="AddUser.aspx" CssClass="add"></asp:HyperLink>
                <h3>员工列表</h3>
                <WebUc:UserListControl ID="UserListControl1" runat="server" />
                <p style="padding-left:10px;">还可以加<span><%#this.userRemain %></span>个员工</p>
            </div>
            <div style="clear:both;"></div>
        </div> 
    </div>
    </div>
</asp:Content>