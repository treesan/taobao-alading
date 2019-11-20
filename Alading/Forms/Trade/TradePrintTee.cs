using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Utils;
using Alading.Business;

namespace Alading.Forms.Trade
{
    public class TradePrintTee
    {
        /// <summary>
        /// 当前交易的CustomTid
        /// </summary>
        private string _customTid = string.Empty;

        /// <summary>
        /// 当前交易时间戳记录
        /// </summary>
        private byte[] _tradeTimeStamp=null;

        /// <summary>
        /// 收货人的姓名
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收货人的所在省份
        /// </summary>
        public string ReceiverState { get; set; }

        /// <summary>
        /// 收货人的所在城市
        /// </summary>
        public string ReceiverCity { get; set; }

        /// <summary>
        /// 收货人的所在地区
        /// </summary>
        public string ReceiverDistrict { get; set; }

        /// <summary>
        /// 收货人的详细地址
        /// </summary>
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// 收货人的邮编
        /// </summary>
        public string ReceiverZip { get; set; }

        /// <summary>
        /// 收货人的手机号码
        /// </summary>
        public string ReceiverMobile { get; set; }

        /// <summary>
        /// 收货人的电话号码
        /// </summary>
        public string ReceiverPhone { get; set; }

        /// <summary>
        /// 邮费模板
        /// </summary>
        public string CompanyCode ;

        public TradePrintTee(Alading.Entity.Trade trade)
        {
            SetValues(trade);
        }

        /// <summary>
        /// 赋值函数
        /// </summary>
        /// <param name="trade"></param>
        private void SetValues(Alading.Entity.Trade trade)
        {
            _customTid = trade.CustomTid;
            _tradeTimeStamp = trade.TradeTimeStamp;
            ReceiverName = trade.receiver_name;
            ReceiverState = trade.receiver_state;
            ReceiverCity = trade.receiver_city;
            ReceiverDistrict = trade.receiver_district;
            ReceiverAddress = trade.receiver_address;
            ReceiverZip = trade.receiver_zip;
            ReceiverMobile = trade.receiver_mobile;
            ReceiverPhone = trade.receiver_phone;
            CompanyCode = trade.LogisticCompanyCode;
        }

        /// <summary>
        /// 获得和当前数据库数据匹配的数据
        /// </summary>
        /// <returns></returns>
        public TradePrintTee MatchCurrentData()
        {
            Alading.Entity.Trade trade = TradeService.GetTrade(_customTid);
             //调用时间戳比较函数判断是否需要重新赋值
             if(SystemHelper.CompareTimeStamp(trade.TradeTimeStamp, _tradeTimeStamp)==false)
             {
                 //TODO  是否弹出提示框
                 SetValues(trade);
             }
             return this;
        }
    }
}
