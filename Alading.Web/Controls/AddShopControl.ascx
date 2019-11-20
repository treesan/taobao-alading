<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddShopControl.ascx.cs"
    Inherits="Alading.Web.Controls.AddShopControl" %>
<div>
<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    <table border="0">        
        <tr>
            <td>
                卖家昵称<span style="color:#f00;">(*)</span>：
            </td>
            <td>
                <asp:TextBox ID="txShopNick" runat="server" Width="180px" CssClass="textbox2"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="商店昵称为必填项" ControlToValidate="txShopNick">商店昵称为必填项</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>商店类型<span style="color:#f00;">(*)</span>：</td>
            <td>
                <asp:DropDownList ID="ddlShopType" runat="server" Width="180px" CssClass="dropdownlist">
                    <asp:ListItem Value="1">淘宝商城店（B店）</asp:ListItem>
                    <asp:ListItem Value="2">淘宝商城店（C店）</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <tr>
            <td>省份：</td>
            <td>
                <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="true"
                    onselectedindexchanged="ddlProvince_SelectedIndexChanged" Width="180px" CssClass="dropdownlist">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>城市：</td>
            <td>
                <asp:DropDownList ID="ddlCity" runat="server"  AutoPostBack="true"
                    onselectedindexchanged="ddlCity_SelectedIndexChanged" Width="180px" CssClass="dropdownlist"
                    >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>地区：</td>
            <td><asp:DropDownList ID="ddlArea" runat="server" Width="180px" CssClass="dropdownlist">
                </asp:DropDownList></td>
        </tr>
        </ContentTemplate>
        </asp:UpdatePanel>
        <tr>
            <td>地址：</td>
            
            <td>
                <asp:TextBox ID="txAddress" runat="server" Width="180px" CssClass="textbox2"></asp:TextBox></td>
        </tr>
        <tr>
            <td>电话：</td>
            <td>
                <asp:TextBox ID="txTel" runat="server" Width="180px" CssClass="textbox2"></asp:TextBox></td>
        </tr>
        <tr align="left">
            <td>
                
            </td>
            <td>
                <asp:Button ID="btnAddShop" runat="server" Text="完成" />
                <input id="btnReset" type="reset" value="重置" />
            </td>
        </tr>
    </table>
</div>
