<%@ Page Title="" Language="C#" MasterPageFile="~/NewSite.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Alading.Web.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
#header
{
    background:url('Content/images/product_layout.png') no-repeat center center;
    height:500px;
    margin:60px auto auto auto;
    overflow:hidden;
    display:block;
}

#content_head
{
    height:40px;
    line-height:40px;
    overflow:hidden;
    background:#f0f0f0;
}

#content_head h3
{
    height:40px;
    line-height:40px;
    overflow:hidden;
    margin:0;
    padding:0;
    font-size:15px;
    color:#333;
    background:url('Content/images/core_function_icon.png') no-repeat left center;
    padding-left:38px;
}

#content_head h3 span
{
    color:#ff7200;
    margin-left:10px;
    font-size:14px;
}

#fuction_content
{
    padding:20px 0;
}

#part1
{
    float:left;
    width:500px;
    border:#f00;
}

#part2
{
    float:right;
    width:500px;
    border:#0f0;
}

#part_f1, #part_f2, #part_f3, #part_f4, #part_f5, #part_f6
{
    width:491px;
    height:202px;
    background-position:center center;
    background-repeat:no-repeat;
    border:#666;
    margin:10px 0;
}

#part_f1
{
    background-image:url('Content/images/function_part_1.jpg');
}

#part_f2
{
    background-image:url('Content/images/function_part_2.jpg');
}

#part_f3
{
    background-image:url('Content/images/function_part_3.jpg');
}

#part_f4
{
    background-image:url('Content/images/function_part_4.jpg');
}

#part_f5
{
    background-image:url('Content/images/function_part_5.jpg');
}

#part_f6
{
    background-image:url('Content/images/function_part_6.jpg');
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContentPlaceHolder" runat="server">

<div id="content_head">
    <div class="wrapper">
        <h3>核心功能<span>Core Function</span></h3>
    </div>
</div>

<div id="fuction_content">
<div class="wrapper">
    <div id="part1">
        <div id="part_f1">
        </div>
        <div id="part_f2">
        </div>
        <div id="part_f3">
        </div>
    </div>
    <div id="part2">
        <div id="part_f4">
        </div>
        <div id="part_f5">
        </div>
        <div id="part_f6">
        </div>
    </div>
    <div class="clear"></div>
</div>
</div>

<script type="text/javascript">
    var e = document.getElementById("nav_product");
    e.setAttribute("class", "select");
</script>
</asp:Content>
