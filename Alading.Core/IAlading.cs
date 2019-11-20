using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface 
{
    public interface IAlading :  
        ITradeInfo
        ,IArea 
        ,IBrand 
        ,ICode 
        ,IConsumer
        ,IConsumerVisit
        ,IConsumerAddress
        ,IConfig
        ,IEmailTemplate
        ,IEmailTemplateCat
        ,IItem 
        ,IItemCat 
        ,IItemProp 
        ,IItemPropValue 
        ,IItemSellerAuthorize 
        ,IJournalAccount 
        ,ILogisticCompany
        , ILogisticCompanyTemplate
        , ILogisticCompanyTemplateItem 
        ,IPayCharge 
        ,IPermission 
        ,IPostage 
        ,IPostageMode 
        ,IProduct 
        ,IPurchaseOrder 
        //,IPurchaseProduct
        ,IRole 
        ,IRolePermission 
        ,ISellerCat 
        ,IShipping 
        ,IShop 
        ,IStockCat 
        ,IStockDetail 
        ,IStockHouse 
        ,IStockInOut 
        ,IStockItem 
        ,IStockLayout 
        ,IStockPrice 
        ,IStockProduct 
        ,IStockProp 
        ,IStockPropValue 
        ,IStockUnit 
        ,IStockUnitGroup 
        ,ISupplier
        ,ITask
        ,ITrade 
        ,ITradeOrder 
        ,ITradeRate 
        ,ITradeRefund 
        ,ITradeRefundMessage 
        ,IUser 
        ,IUserRole
        ,IUserSalary
        ,IUserShop
        ,IUserStockHouse
        ,IView_UserRole
        ,IView_ItemStock
        ,IView_ItemPropValue
        ,IView_TradeStock
        ,IView_StockDetail
        ,IRateContent
        ,IView_ShopItem        
        , IView_OrderItemStockProduct
        , IView_TradeAssembleStock
        , IView_TradeShop
    {   
       
    }
}
