using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Alading.Taobao
{
    /// <summary>
    /// Top错误码。
    /// </summary>
    public static class TopError
    {
        public static Hashtable ErrorTable
        {
            get 
            {
                Hashtable table = new Hashtable();
                /*系统级错误*/
                table.Add("3", "图片上传失败");
                table.Add("4", "用户调用次数超限");
                table.Add("5", "会话调用次数超限");
                table.Add("6", "合作伙伴调用次数超限");
                table.Add("7", "应用调用次数超限");
                table.Add("8", "应用调用频率超限");
                table.Add("9", "HTTP方法被禁止（请用大写的POST或GET）");
                table.Add("10", "服务不可用");
                table.Add("11", "开发者权限不足");
                table.Add("12", "用户权限不足");
                table.Add("13", "合作伙伴权限不足");
                table.Add("15", "远程服务出错");
                table.Add("21", "缺少方法名参数");
                table.Add("22", "不存在的方法名");
                table.Add("23", "非法数据格式");
                table.Add("24", "缺少签名参数");
                table.Add("25", "非法签名");
                table.Add("26", "缺少SessionKey参数");
                table.Add("27", "无效的SessionKey参数");
                table.Add("28", "缺少AppKey参数");
                table.Add("29", "非法的AppKe参数");
                table.Add("30", "缺少时间戳参数");
                table.Add("31", "非法的时间戳参数");
                table.Add("32", "缺少版本参数");
                table.Add("33", "非法的版本参数");
                table.Add("34", "不支持的版本号");
                table.Add("40", "缺少必选参数");
                table.Add("41", "非法的参数");
                table.Add("42", "请求被禁止");
                table.Add("43", "参数错误");

                /*业务级错误*/
                table.Add("501", "语句不可索引");
                table.Add("502", "数据服务不可用");
                table.Add("503", "无法解释TBQL语句");
                table.Add("504", "需要绑定用户昵称");
                table.Add("505", "缺少参数");
                table.Add("506", "参数错误");
                table.Add("507", "参数格式错误");
                table.Add("508", "获取信息权限不足");
                table.Add("550", "用户服务不可用");
                table.Add("551", "商品服务不可用");
                table.Add("552", "商品图片服务不可用");
                table.Add("553", "商品更新服务不可用");
                table.Add("554", "商品删除失败");
                table.Add("555", "用户没有订购图片服务");
                table.Add("556", "图片URL错误");
                table.Add("557", "商品视频服务不可用");
                table.Add("560", "交易服务不可用");//Trade Service Unavailable 
                table.Add("561", "交易服务不可用");//Trade TC Service Unavailable
                table.Add("562 ", "交易不存在");
                table.Add("563", "非法交易");
                table.Add("564", "没有权限添加或更新交易备注");
                table.Add("565", "交易备注超出长度限制");
                table.Add("566", "交易备注已经存在");
                table.Add("567", "没有权限添加或更新交易信息");
                table.Add("568", "交易没有子订单");
                table.Add("569", "交易关闭错误");
                table.Add("570", "物流服务不可用");
                table.Add("571", "非法的邮费");
                table.Add("572", "非法的物流公司编号");
                table.Add("580", "评价服务不可用");
                table.Add("581", "添加评价服务错误");
                table.Add("582", "获取评价服务错误");
                table.Add("590", "店铺服务不可用");
                table.Add("591", "店铺剩余橱窗推荐服务不可用");
                table.Add("592", "卖家自定义类目服务不可用");
                table.Add("594", "卖家自定义类目添加错误");
                table.Add("595", "卖家自定义类目更新错误");
                table.Add("596", "用户没有店铺");
                table.Add("597", "卖家自定义父类目错误");
                table.Add("540", "交易统计服务不可用");
                table.Add("541", "类目统计服务不可用");
                table.Add("542", "商品统计服务不可用");
                table.Add("601", "用户不存在");
                table.Add("610", "产品服务不可用");
                table.Add("710", "淘宝客服务不可用");
                table.Add("611", "产品数据格式错误");
                table.Add("612", "产品ID错误");
                table.Add("613", "删除产品图片错误");
                table.Add("614", "没有权限添加产品");
                table.Add("615", "收货地址服务不可用 ");
                table.Add("620", "邮费服务不可用");
                table.Add("621", "邮费模板类型错误");
                table.Add("622", "缺少参数：post, express或ems");
                table.Add("623", "邮费模板参数错误");
                table.Add("630", "收费服务不可用");
                table.Add("650", "退款服务不可用");
                table.Add("651", "非法的退款编号");
                table.Add("652", "退款服务不可用");
                table.Add("653", "退款不存在");
                table.Add("654", "没有权限获取退款信息");
                table.Add("655", "没有权限添加退款留言");
                table.Add("656", "无法添加退款留言");
                table.Add("542", "商品统计服务不可用");
                table.Add("656", "用户不存在");
                table.Add("657", "退款留言内容太长");
                table.Add("658", "退款留言内容不能为空");
                table.Add("659", "非法的交易订单（或子订单）ID");
                table.Add("660", "商品扩展服务不可用");
                table.Add("661", "商品扩展信息不存在");
                table.Add("662", "没有权限更新商品扩展信息");
                table.Add("663", "缺少物流参数 ");
                table.Add("664", "物流参数错误");
                table.Add("670", "佣金服务不可用");
                table.Add("671", "佣金交易不存在");
                table.Add("672", "淘宝客报表服务不可用");
                table.Add("673", "备案服务不可用");
                table.Add("674", "应用服务不可用");
                table.Add("900", "远程连接错误");
                table.Add("901", "远程服务超时");
                table.Add("902", "远程服务错误");

                /*容器级错误*/
                table.Add("100", "授权码已经过期");
                table.Add("101", "授权码在缓存里不存在，一般是用同样的authcode两次获取sessionkey");
                table.Add("103", "appkey或者tid（插件ID）参数必须至少传入一个");
                table.Add("104", "appkey或者tid对应的插件不存在");
                table.Add("105", "插件的状态不对，不是上线状态或者正式环境下测试状态");
                table.Add("106", "没权限调用此app，由于插件不是所有用户都默认安装，所以需要用户和插件进行一个订购关系");
                table.Add("108", "由于app有绑定昵称，而登陆的昵称不是绑定昵称，所以没权限访问");
                table.Add("109", "服务端在生成参数的时候出了问题（一般是tair有问题）");
                table.Add("111", "服务端在生成参数的时候出了问题（一般是tair有问题）");
                table.Add("110", "服务端在写出参数的时候出了问题");
                return table;
            }
        }
    }
}
