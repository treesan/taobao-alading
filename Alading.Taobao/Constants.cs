using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Taobao
{
    /// <summary>
    /// 公用常量类。
    /// </summary>
    public abstract class Constants
    {

        #region 代码表中CodeCatgory代码

         /// <summary>
        /// 运费承担方式
        /// </summary>
        public const string CODE_POSTFEE_OWNER = "运费承担方式";
        public const string DEFAULT_POSTFEE_OWNER = "buyer";//默认运费承担方式,旺点标准交易


        /// <summary>
        /// 交易类型
        /// </summary>
        public const string CODE_TRADE_TYPE = "交易类型";
        public const string DEFAULT_TRADE_TYPE = "independent_shop_trade";//默认交易类型,旺点标准交易

        /// <summary>
        /// 物流方式
        /// </summary>
        public const string CODE_SHIPPING_TYPE = "物流方式";
        public const string DEFAULT_SHIPPING_TYPE = "express";//默认物流方式,快递

        /// <summary>
        /// 系统代理幅度
        /// </summary>
        public const string CODE_MONEY_RANGE = "系统代理幅度";
        public const string DEFAULT_MONEY_RANGE = "15";//默认系统代理幅度,501-1000

        #endregion

        public const string DEFAULT_SHIPPING_COMPANY = "STO";//默认的物流公司，申通

        /// <summary>
        /// 交易表和订单订单表在DataSet中的默认关系
        /// </summary>
        public const string TRADE_ORDER_RELATION = "订单详细信息";

        /// <summary>
        /// 库存Item和库存Product在DataSet中的默认关系
        /// </summary>
        public const string ITEM_PRODUCT_RELATION = "订单详细信息";

        /// <summary>
        /// 默认下载订单开启的线程数
        /// </summary>
        public const int DEFAULT_THREAD_NUM = 10;

        /// <summary>
        /// 默认交货日期天数
        /// </summary>
        public const double DEFAULT_END_DAYS = 15.0;

        /// <summary>
        /// 默认时间字符串
        /// </summary>
        public const string DEFAULT_TIME = "0001-01-01 00:00:00";

        /// <summary>
        /// TOP默认时间格式。
        /// </summary>
        public const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 授权码
        /// </summary>
        public const string APP_KEY = "12022554";

        /// <summary>
        /// 授权密钥
        /// </summary>
        public const string APP_SECRET="47cce303a491ebfaa7161635a3e5ef8c";
        /// <summary>
        /// TOP获取客户端应用授权码地址。
        /// </summary>
        public const string TOP_AUTH_URL = "http://container.open.taobao.com/container?authcode=";

        /// <summary>
        /// TOP测试环境Url
        /// </summary>
        public const string TOP_SANDBOX_API_URL = "http://gw.api.tbsandbox.com/router/rest";

        /// <summary>
        /// TOP正式环境Url
        /// </summary>
        public const string TOP_API_URL = "http://gw.api.taobao.com/router/rest";

        /// <summary>
        /// 缺少用户名
        /// </summary>
        public const string LACK_NICK = "缺少店铺用户名！";

        /// <summary>
        /// 缺少密码
        /// </summary>
        public const string LACK_PSW = "缺少店铺密码！";

        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        public const string USER_PSW_ERROR = "店铺用户名或密码错误！";

        /// <summary>
        /// 由于需要验证码才能登陆，请到淘宝网上登陆成功再试
        /// </summary>
        public const string Check_CODE_INFO = "由于需要验证码才能登陆，请到淘宝网上登陆成功再试！";

        /// <summary>
        /// 正在加载数据
        /// </summary>
        public const string WAIT_LOAD_DATA = "正在加载数据，请稍候........";

        /// <summary>
        /// 正在处理数据
        /// </summary>
        public const string OPERATE_DB_DATA = "正在处理数据，请稍候........";

        /// <summary>
        /// 正在同步淘宝数据
        /// </summary>
        public const string OPERATE_SYC_DATA = "正在同步淘宝数据，请稍候........";

        /// <summary>
        /// 正在处理数据到淘宝
        /// </summary>
        public const string OPERATE_TBDB_DATA = "正在处理数据到淘宝，请稍候........";

        /// <summary>
        /// 查询中
        /// </summary>
        public const string WAIT_SEARCH_DATA = "查询中，请稍候........";  

        /// <summary>
        /// 店铺信息不存在
        /// </summary>
        public const string NOT_EXISTED_SHOP = "店铺信息不存在!";

        /// <summary>
        /// 系统提示
        /// </summary>
        public const string SYSTEM_PROMPT = "系统提示";

        /// <summary>
        /// 宝贝上传提示
        /// </summary>
        public const string ERROR_REPORT = "该宝贝在库存中不存在，请先在商品添加页面添加该商品，然后上传！";

        /// <summary>
        /// 没有选中任何条目
        /// </summary>
        public const string NOT_SELECT_ITEM = "没有选中任何条目！";
        /// <summary>
        /// 淘宝初始化入库
        /// </summary>
        public const string INIT_FROM_TOP = "淘宝初始化入库";

        /// <summary>
        /// 初始化入库、商家编码更新到淘宝均成功
        /// </summary>
        public const string INIT_UPDATE_SUCCESS = "初始化入库、商家编码更新到淘宝均成功！";

        /// <summary>
        /// 默认仓库
        /// </summary>
        public const string DEFAULT_STOCKHOUSE_NAME = "默认仓库";

        /// <summary>
        /// 默认仓库代码
        /// </summary>
        public const string DEFAULT_STOCKHOUSE_CODE = "0";

        /// <summary>
        /// 默认库位
        /// </summary>
        public const string DEFAULT_STOCKLAYOUT_NAME = "默认库位";

        /// <summary>
        /// 默认库位代码
        /// </summary>
        public const string DEFAULT_STOCKLAYOUT_CODE = "0";

        /// <summary>
        /// 基本单位
        /// </summary>
        public const string DEFAULT_UNIT_NAME = "基本单位";

        /// <summary>
        /// 基本单位
        /// </summary>
        public const string DEFAULT_UNIT_CODE = "0";

        /// <summary>
        /// 默认类目
        /// </summary>
        public const string DEFAULT_STOCKCAT_NAME = "默认类目";

        /// <summary>
        /// 默认类目id
        /// </summary>
        public const string DEFAULT_STOCKCAT_CID = "10000";

        /// <summary>
        /// 必填信息不完整
        /// </summary>
        public const string ERROR_SHORT = "您未填写完整必填信息！";

        /// <summary>
        /// 模板已被删除
        /// </summary>
        public const string POST_DELETED = "错误代码：532，错误信息：Remote service error，错误子代码：postage-delete-service-error，错误子信息：卖家试图删除的邮费模板可能已经被删除!";

        /// <summary>
        /// 宝贝已经被删除
        /// </summary>
        public const string ITEM_DELETED = "错误代码：530，错误信息：Remote service error，错误子代码：item-not-exist，错误子信息：该商品已被删除";
        /// <summary>
        /// 宝贝每页显示条数
        /// </summary>
        public const int ITEM_PAGE_SIZE = 15;

    }
}
