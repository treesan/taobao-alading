<%@ Page Title="" Language="C#" MasterPageFile="~/NewSite.Master" AutoEventWireup="true" CodeBehind="HelpList.aspx.cs" Inherits="Alading.Web.HelpList" %>
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

ul#topic_list
{
    margin:0;
    padding:16px 5px;
    list-style:none;
}

ul#topic_list li.s1, ul#topic_list li.s2
{
    height:30px;
    line-height:30px;
    padding-left:16px;
    padding-right:16px;
    font-size:12px;
    overflow:hidden;
}

ul#topic_list li.s2
{
    background:#f1f1f1;
}

ul#topic_list a
{
    color:#1a76a9;
    text-decoration:none;
    background:url('Content/images/topic_icon.png') no-repeat left center;
    padding-left:25px;
}

ul#topic_list a:hover
{
    color:#ff7200;
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
                        <li><a href="HelpList.aspx?cid=<%# Eval("Id") %>"><%# Eval("Name") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div id="part1_foot"></div>
    </div>
    <div id="part2">
        <h3><%# this.CatName %></h3>
        <ul id="topic_list">
            <asp:Repeater ID="TopicRepeater" runat="server">
                <ItemTemplate>
                    <li class="s1"><a href="HelpTopic.aspx?id=<%# Eval("Id") %>#locate"><%# Eval("Title") %></a></li>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <li class="s2"><a href="HelpTopic.aspx?id=<%# Eval("Id") %>#locate"><%# Eval("Title") %></a></li>
                </AlternatingItemTemplate>                
            </asp:Repeater>
        </ul>
    </div>
    <div class="clear"></div>
</div>

<script type="text/javascript">
    var e = document.getElementById("nav_help");
    e.setAttribute("class", "select");
</script>
</asp:Content>
