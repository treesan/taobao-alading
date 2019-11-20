﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.3603
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alading.GetData.ExpressService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="ExpressService.ExpressService")]
    public interface ExpressService {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetExpressList", ReplyAction="urn:ExpressService/GetExpressListResponse")]
        string GetExpressList(int currentPage, int pageSize, int status, int order);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetStatisticInfo", ReplyAction="urn:ExpressService/GetStatisticInfoResponse")]
        string GetStatisticInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/SearchExpressList", ReplyAction="urn:ExpressService/SearchExpressListResponse")]
        string SearchExpressList(int currentPage, int pageSize, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/SearchCustomerExpressList", ReplyAction="urn:ExpressService/SearchCustomerExpressListResponse")]
        string SearchCustomerExpressList(int currentPage, int pageSize, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetUserExpressTrack", ReplyAction="urn:ExpressService/GetUserExpressTrackResponse")]
        string GetUserExpressTrack(string company, string outId);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/UpdateExpressStatus", ReplyAction="urn:ExpressService/UpdateExpressStatusResponse")]
        bool UpdateExpressStatus(int expressId, int status, string statusDesc);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/AddNewExpressTrack", ReplyAction="urn:ExpressService/AddNewExpressTrackResponse")]
        string AddNewExpressTrack(string buyerNick, string receiverName, string receiverPhone, string receiverMobile, string state, string city, string district, string address, string expressCompanyName, string expressOutId, string itemTitle);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetAreas", ReplyAction="urn:ExpressService/GetAreasResponse")]
        string GetAreas(string parentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/ResetValidateCode", ReplyAction="urn:ExpressService/ResetValidateCodeResponse")]
        string ResetValidateCode();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetUserInfo", ReplyAction="urn:ExpressService/GetUserInfoResponse")]
        string GetUserInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetCompanyList", ReplyAction="urn:ExpressService/GetCompanyListResponse")]
        string[] GetCompanyList();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/ChangExpressOutId", ReplyAction="urn:ExpressService/ChangExpressOutIdResponse")]
        string ChangExpressOutId(int expressId, string companyName, string outId);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/UpdateMemo", ReplyAction="urn:ExpressService/UpdateMemoResponse")]
        string UpdateMemo(int expressId, string memo, int memoType);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetSendAndAcceptStat", ReplyAction="urn:ExpressService/GetSendAndAcceptStatResponse")]
        string GetSendAndAcceptStat();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetEfficiencyStat", ReplyAction="urn:ExpressService/GetEfficiencyStatResponse")]
        string GetEfficiencyStat();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetCompanyUseageStat", ReplyAction="urn:ExpressService/GetCompanyUseageStatResponse")]
        string GetCompanyUseageStat();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetGlobalCompanyUseageStat", ReplyAction="urn:ExpressService/GetGlobalCompanyUseageStatResponse")]
        string GetGlobalCompanyUseageStat();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetSelfCompanyUseageStat", ReplyAction="urn:ExpressService/GetSelfCompanyUseageStatResponse")]
        string GetSelfCompanyUseageStat();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetCustomerAreaStat", ReplyAction="urn:ExpressService/GetCustomerAreaStatResponse")]
        string GetCustomerAreaStat();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/Exit", ReplyAction="urn:ExpressService/ExitResponse")]
        string Exit();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetSystemMessage", ReplyAction="urn:ExpressService/GetSystemMessageResponse")]
        string GetSystemMessage();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetAIKeyword", ReplyAction="urn:ExpressService/GetAIKeywordResponse")]
        string[] GetAIKeyword();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetSoldTrades", ReplyAction="urn:ExpressService/GetSoldTradesResponse")]
        string GetSoldTrades(int currentPage, int pageSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ExpressService/GetTradeInfo", ReplyAction="urn:ExpressService/GetTradeInfoResponse")]
        string GetTradeInfo(long tradeId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ExpressServiceChannel : Alading.GetData.ExpressService.ExpressService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class ExpressServiceClient : System.ServiceModel.ClientBase<Alading.GetData.ExpressService.ExpressService>, Alading.GetData.ExpressService.ExpressService {
        
        public ExpressServiceClient() {
        }
        
        public ExpressServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ExpressServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExpressServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExpressServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetExpressList(int currentPage, int pageSize, int status, int order) {
            return base.Channel.GetExpressList(currentPage, pageSize, status, order);
        }
        
        public string GetStatisticInfo() {
            return base.Channel.GetStatisticInfo();
        }
        
        public string SearchExpressList(int currentPage, int pageSize, string key) {
            return base.Channel.SearchExpressList(currentPage, pageSize, key);
        }
        
        public string SearchCustomerExpressList(int currentPage, int pageSize, string key) {
            return base.Channel.SearchCustomerExpressList(currentPage, pageSize, key);
        }
        
        public string GetUserExpressTrack(string company, string outId) {
            return base.Channel.GetUserExpressTrack(company, outId);
        }
        
        public bool UpdateExpressStatus(int expressId, int status, string statusDesc) {
            return base.Channel.UpdateExpressStatus(expressId, status, statusDesc);
        }
        
        public string AddNewExpressTrack(string buyerNick, string receiverName, string receiverPhone, string receiverMobile, string state, string city, string district, string address, string expressCompanyName, string expressOutId, string itemTitle) {
            return base.Channel.AddNewExpressTrack(buyerNick, receiverName, receiverPhone, receiverMobile, state, city, district, address, expressCompanyName, expressOutId, itemTitle);
        }
        
        public string GetAreas(string parentId) {
            return base.Channel.GetAreas(parentId);
        }
        
        public string ResetValidateCode() {
            return base.Channel.ResetValidateCode();
        }
        
        public string GetUserInfo() {
            return base.Channel.GetUserInfo();
        }
        
        public string[] GetCompanyList() {
            return base.Channel.GetCompanyList();
        }
        
        public string ChangExpressOutId(int expressId, string companyName, string outId) {
            return base.Channel.ChangExpressOutId(expressId, companyName, outId);
        }
        
        public string UpdateMemo(int expressId, string memo, int memoType) {
            return base.Channel.UpdateMemo(expressId, memo, memoType);
        }
        
        public string GetSendAndAcceptStat() {
            return base.Channel.GetSendAndAcceptStat();
        }
        
        public string GetEfficiencyStat() {
            return base.Channel.GetEfficiencyStat();
        }
        
        public string GetCompanyUseageStat() {
            return base.Channel.GetCompanyUseageStat();
        }
        
        public string GetGlobalCompanyUseageStat() {
            return base.Channel.GetGlobalCompanyUseageStat();
        }
        
        public string GetSelfCompanyUseageStat() {
            return base.Channel.GetSelfCompanyUseageStat();
        }
        
        public string GetCustomerAreaStat() {
            return base.Channel.GetCustomerAreaStat();
        }
        
        public string Exit() {
            return base.Channel.Exit();
        }
        
        public string GetSystemMessage() {
            return base.Channel.GetSystemMessage();
        }
        
        public string[] GetAIKeyword() {
            return base.Channel.GetAIKeyword();
        }
        
        public string GetSoldTrades(int currentPage, int pageSize) {
            return base.Channel.GetSoldTrades(currentPage, pageSize);
        }
        
        public string GetTradeInfo(long tradeId) {
            return base.Channel.GetTradeInfo(tradeId);
        }
    }
}