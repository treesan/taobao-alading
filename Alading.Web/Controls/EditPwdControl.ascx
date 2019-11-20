<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditPwdControl.ascx.cs" Inherits="Alading.Web.Controls.EditPwdControl" %>
<div>
    
    <table border="0">
        <tr>
            <td align="right">
                原始密码<span style="color:#f00;">(*)</span>：
            </td>
            <td>
                <asp:TextBox ID="txPassword" runat="server" TextMode="Password" CssClass="textbox2"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="帐号密码为必填项"
                    ControlToValidate="txPassword" Display="Dynamic">帐号密码为必填项</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                新密码<span style="color:#f00;">(*)</span>：
            </td>
            <td>
                <asp:TextBox ID="txNewPwd" runat="server" TextMode="Password" CssClass="textbox2"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="帐号密码为必填项"
                    ControlToValidate="txNewPwd" Display="Dynamic" EnableClientScript="False" 
                    ValidationGroup="newpwd">帐号密码为必填项</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="密码长度为6~16位" ControlToValidate="txNewPwd" Display="Dynamic" 
                    ValidationExpression="\S{6,16}" ValidationGroup="newpwd"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                重新密码<span style="color:#f00;">(*)</span>：
            </td>
            <td>
                <asp:TextBox ID="txRepeat" runat="server" TextMode="Password" CssClass="textbox2"></asp:TextBox>
            </td>
            
            <td>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ErrorMessage="密码输入不一致" ControlToCompare="txNewPwd" ControlToValidate="txRepeat" 
                    Display="Dynamic"></asp:CompareValidator></td>
        </tr>
        <tr align="left">
            <td>                
            </td>
            <td>
                <asp:Button ID="btnOK" runat="server" Text="完成" />
                <asp:Button ID="btnCan" runat="server" Text="取消" UseSubmitBehavior="False" PostBackUrl="~/UserInfo.aspx" CausesValidation="False" />
            </td>
        </tr>
    </table>
    
    </div>