<%@ Page Title="" Language="C#" MasterPageFile="~/NewSite.Master" AutoEventWireup="true" CodeBehind="HelpTopic.aspx.cs" Inherits="Alading.Web.HelpTopic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
#header
{
    background:url('Content/images/help_header.jpg') no-repeat center center;
    height:330px;
    margin:0 auto auto auto;
    overflow:hidden;
    display:block;
}

#content
{
    background:url('Content/images/help_body.jpg') no-repeat center top;
    padding-top:20px;
}

#part1
{
    width:267px;
    float:left;
    margin-bottom:20px;
}

#part1_head
{
    background:url('Content/images/help_topic_top.jpg') no-repeat center top;
    height:50px;
}

#part1_body
{
    background:url('Content/images/help_topic_body.jpg') repeat-y center center;
}

#part1_foot
{
    background:url('Content/images/help_topic_bottom.jpg') no-repeat center bottom;
    height:18px;
}

ul#cat_list
{
    margin:0;
    padding:10px 16px;
    list-style:none;
}

ul#cat_list li
{
    height:30px;
    line-height:30px;
    font-size:12px;
    padding-left:8px;
    padding-right:8px;
    overflow:hidden;
}

ul#cat_list li a
{
    color:#666;
    text-decoration:none;
}

ul#cat_list li a:hover
{
    color:#ff7200;
}

#part2
{
    width:710px;
    float:right;
    margin-bottom:20px;
}

#part2 h3
{
    width:670px;
    margin:0;
    padding:0;
    height:48px;
    line-height:48px;
    background:url('Content/images/topic_top.jpg') no-repeat center center;
    color:#666;
    font-size:12px;
    padding-left:45px;
}

#topic_data
{
    padding:10px 10px 20px 10px;
}

#topic_data h4
{
    font-size:16px;
    height:50px;
    line-height:56px;
    margin:0;
    padding:0;
}

#topic_data .text
{
    padding:8px 0;
    font-size:12px;
    color:#666;
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentPlaceHolder" runat="server">
<div class="wrapper">
    <div id="part1">
        <div id="part1_head"></div>
        <div id="part1_body">
            <ul id="cat_list">
                <asp:Repeater ID="CatRepeater" runat="server">
                    <ItemTemplate>
                        <li><a id="cat_<%# Eval("Id") %>" href="HelpList.aspx?cid=<%# Eval("Id") %>"><%# Eval("Name") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div id="part1_foot"></div>
    </div>
    <div id="part2">
        <a name="locate"></a>     
        <h3><%# this.CatName %></h3>
        <div id="topic_data">
            <h4><%# this.TopicTitle %></h4>
            <div class="text"><%# this.TopicContent %></div>
        </div>
    </div>
    <div class="clear"></div>
</div>

<script type="text/javascript">
    var e = document.getElementById("nav_help");
    e.setAttribute("class", "select");
</script>
</asp:Content>
