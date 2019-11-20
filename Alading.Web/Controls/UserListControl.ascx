<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserListControl.ascx.cs"
    Inherits="AladingWeb.Controls.UserListControl" %>
<script type="text/javascript">

    function showUserDialog(userCode) {
        if (confirm('确定删除该用户?')) {
            location.href = "DelUser.aspx?UserCode=" + userCode;
        }
    }
    
</script>
<div>
    <asp:Repeater ID="RepUser" runat="server">
        <HeaderTemplate>
            <table style="width:100%; border:0; border-collapse:collapse;">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="padding:5px 5px 5px 10px;">
                    <%#Eval("UserName") %>
                </td>
                <td style="width:130px; text-align:center; vertical-align:middle;">
                    <a href="EditUser.aspx?UserCode=<%#Eval("UserCode") %>">编辑</a>                    
                    <a onclick="showUserDialog('<%#Eval("UserCode") %>');">删除</a>
                    <a href="EditPwd.aspx?UserCode=<%#Eval("UserCode") %>">修改密码</a>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background:#f1f8f9;">
                <td style="padding:5px 5px 5px 10px;">
                    <%#Eval("UserName") %>
                </td>
                <td style="width:130px; text-align:center; vertical-align:middle;">
                    <a href="EditUser.aspx?UserCode=<%#Eval("UserCode") %>">编辑</a>                    
                    <a onclick="showUserDialog('<%#Eval("UserCode") %>');">删除</a>
                    <a href="EditPwd.aspx?UserCode=<%#Eval("UserCode") %>">修改密码</a>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
