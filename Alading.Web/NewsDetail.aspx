<%@ Page Title="" Language="C#" MasterPageFile="~/NewSite.Master" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="Alading.Web.NewsDetail" %>
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
    background:url('Content/images/news_body.jpg') no-repeat center top;
    padding-top:20px;
}

#part1
{
    width:267px;
    float:left;
    margin-bottom:40px;
    margin-left:10px;
}

#part1_head
{
    background:url('Content/images/news_cat_top.jpg') no-repeat center top;
    height:50px;
}

#part1_body
{
    background:url('Content/images/news_cat_body.jpg') repeat-y center center;
}

#part1_foot
{
    background:url('Content/images/news_cat_bottom.jpg') no-repeat center bottom;
    height:18px;
}

ul#cat_list
{
    margin:0;
    padding:10px 33px;
    list-style:none;
}

ul#cat_list li
{
    font-size:12px;
    overflow:hidden;
}

ul#cat_list li.select
{
    background:url('Content/images/cat_select.png') no-repeat center center;
}

ul#cat_list li a
{
    color:#666;
    text-decoration:none;
    display:block;
    width:201px;
    height:40px;
    line-height:40px;
    text-align:center;
    font-weight:bold;
}

ul#cat_list li a:hover
{
    color:#ff7200;
    background:url('Content/images/cat_select.png') no-repeat center center;
}

#part2
{
    width:710px;
    float:right;
    margin-right:10px;
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

#news_data
{
    padding:10px 10px 20px 10px;
}

#news_data .time
{
    float:right;
    font-size:10px;
    color:#999;
    padding-top:22px;
}

#news_data h4
{
    font-size:16px;
    height:50px;
    line-height:56px;
    margin:0;
    padding:0;
}

#news_data .text
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
                        <li id="cat_<%# Eval("Id") %>"><a href="NewsList.aspx?cid=<%# Eval("Id") %>"><%# Eval("Name") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div id="part1_foot"></div>
    </div>
    <div id="part2">
        <a name="locate"></a>
        <h3><%# this.CatName %></h3>
        <div id="news_data">
            <span class="time"><%# this.NewsTime %></span>
            <h4><%# this.NewsTitle %></h4>
            <div class="text"><%# this.NewsContent %></div>
        </div>
    </div>
    <div class="clear"></div>
</div>

<script type="text/javascript">
    var li = document.getElementById("cat_<%# this.NewsCatID %>");
    li.setAttribute("class", "select");
</script>

</asp:Content>
