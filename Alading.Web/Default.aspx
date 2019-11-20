<%@ Page Title="" Language="C#" MasterPageFile="~/NewSite.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Alading.Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
#header
{
    background:url('Content/images/index_header.jpg') no-repeat center center;
    height:327px;
    margin:0 auto auto auto;
    overflow:hidden;
    display:block;
}

#content
{
    background:url('Content/images/index_body.jpg') no-repeat top center;
    margin:0 auto auto auto;
    overflow:hidden;
    display:block;
}

#logo2
{
    background:url('Content/images/aladdin_logo_2.png') no-repeat top center;
    width:178px;
    height:220px;
    margin-top:20px;
    float:left;
}

#part1
{
    float:left;
    width:510px;
    margin-top:20px;
    margin-bottom:20px;
    margin-right:10px;
}

#part1 p
{
    font-size:12px;
    color:#333;
    padding-left:6px;
}

#part2
{
    background:url('Content/images/arrow_bg.jpg') no-repeat top left;
    width:57px;
    height:226px;
    float:left;
}

#part3
{
    float:right;
    width:240px;
    height:200px;
    margin-top:20px;
}

#part3 h3
{
    color:#666;
    margin:0;
    padding:10px 0;
    font-size:16px;
}

#part3 p
{
    font-size:12px;
    color:#333;
    padding-right:6px;
}

#part4
{
    background:url('Content/images/index_info_bg.png') no-repeat left top;
    width:734px;
    height:210px;
    overflow:hidden;
    float:left;
    margin-bottom:20px;
}

#part4_1
{
    width:204px;
    height:130px;
    overflow:hidden;
    float:left;
    padding-left:36px;
    padding-right:20px;
    padding-top:60px;
}

#part4_2
{
    width:190px;
    height:130px;
    overflow:hidden;
    float:left;
    padding-top:60px;
    padding-left:20px;
    padding-right:20px;
}

#part4_3
{
    width:193px;
    height:130px;
    overflow:hidden;
    float:left;
    padding-top:60px;
    padding-left:20px;
    padding-right:20px;
}

#part5
{
    background:url('Content/images/index_info_bg2.png') no-repeat left top;
    width:211px;
    height:130px;
    overflow:hidden;
    float:right;
    margin-bottom:20px;
    padding:60px 25px 20px 25px;
}

.tx_list
{
    margin:0;
    padding:0;
    list-style:none;
    overflow:hidden;
}

.tx_list li
{
    height:22px;
    line-height:22px;
    font-size:12px;
    overflow:hidden;
}

.tx_list li a
{
    text-decoration:none;
    color:#333;
}

.tx_list li a:hover
{
    color:#ff7200;
}

.tx_right
{
    text-align:right;
}

a.more
{
    z-index:2;
    float:right;
    display:block;
    width:36px;
    height:16px;
    position:relative;
    top:-43px;
    right:-3px;
}

a.more2
{
    z-index:2;
    float:right;
    display:block;
    width:36px;
    height:16px;
    position:relative;
    top:-43px;
    right:8px;
}
</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContentPlaceHolder" runat="server">

<div class="wrapper">
    <div id="logo2"></div>
    <div id="part1">
        <img src="Content/images/aladdin_title_2.png" />
        <p>阿拉丁为多店铺、多用户卖家提供了一套综合解决方案，涉及到库存、商品、交易、打印、配/发货、物流追踪、短信通知、客户、评价、盘点、综合统计等环节。将扫描枪、打印机、短信猫等设备结合，极大地提升了卖家的管理水平和效率。配合软件的自动同步本地和淘宝数据、错误库存管理、自动登录淘宝、定时获取订单、定时批量打印快递单和配货单、缺货单生成、发货短信通知、经营分析等特色功能。大大地节约了人力成本、防止误发货。</p>
        <p class="tx_right"><a href="#"><img src="Content/images/client_service.png" /></a></p>
    </div>
    <div id="part2"></div>
    <div id="part3">
        <h3>解决方案</h3>
        <p>阿拉丁为多店铺、多用户卖家提供了一套综合解决方案，涉及到库存、商品、交易、打印、配/发货、物流追踪、短信通知、客户、评价、盘点、综合统计等环节。将扫描枪、打印机、短信猫等设备结合，极大地提升了卖家的管理水平和效率。配合软件的自动同步本地和淘宝数据、错误库存管理、自动登录淘宝。</p>
    </div>
    <div class="clear"></div>
</div>

<div class="wrapper">
    <div id="part4">        
        <div id="part4_1">
            <a href="NewsList.aspx?cid=4" class="more2"></a>
            <ul class="tx_list">
                
            </ul>
        </div>
        <div id="part4_2">
            <a href="javascript:;" class="more"></a>
            <ul class="tx_list">
               
            </ul>
        </div>
        <div id="part4_3">
            <a href="javascript:;" class="more"></a>
            <ul class="tx_list">
               
            </ul>
        </div>
        <div class="clear"></div>
    </div>
    <div id="part5">
        <div id="part5_1">
            <ul class="tx_list">
                
            </ul>
        </div>
    </div>
    <div class="clear"></div>
</div>
<script type="text/javascript">
    var e = document.getElementById("nav_home");
    e.setAttribute("class", "select");
</script>
</asp:Content>
