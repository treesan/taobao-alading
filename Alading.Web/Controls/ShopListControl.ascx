<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShopListControl.ascx.cs"
    Inherits="AladingWeb.Controls.ShopListControl" %>
   <script type="text/javascript">

    function showShopDialog(shopCode) {
        if (confirm('确定删除该商店?')) {
            location.href = "DelShop.aspx?ShopCode=" + shopCode;
        }
    }
    
</script>
<div>
    <asp:Repeater ID="RepShop" runat="server">
        <HeaderTemplate>
            <table style="width:100%; border:0; border-collapse:collapse;">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="padding:5px 5px 5px 10px;">
                    <%#Eval("ShopNick") %>
                </td>
                <td style="width:70px; text-align:center; vertical-align:middle;">
                    <a href="EditShop.aspx?ShopCode=<%#Eval("ShopCode") %>">编辑</a>
                    <a onclick="showShopDialog('<%#Eval("ShopCode") %>');">删除</a>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background:#f1f8f9;">
                <td style="padding:5px 5px 5px 10px;">
                    <%#Eval("ShopNick") %>
                </td>
                <td style="width:70px; text-align:center; vertical-align:middle;">
                    <a href="EditShop.aspx?ShopCode=<%#Eval("ShopCode") %>">编辑</a>
                    <a onclick="showShopDialog('<%#Eval("ShopCode") %>');">删除</a>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
