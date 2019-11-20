using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Core.Enum;
using Newtonsoft.Json;
using Alading.Business;
using System.Linq;
using Alading.Taobao.Entity.Extend;
using Alading.Utils;
using Alading.Taobao.API;
using System.Threading;
using Alading.Taobao;

namespace Alading.Controls.Init
{
    public partial class InputStock : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        private List<ViewShopItemInherit> vsiiList = new List<ViewShopItemInherit>();

        /// <summary>
        /// 入库失败列表,初始化时为全部待入库商品列表
        /// </summary>
        public List<ViewShopItemInherit> inPutFailedViewItemList = new List<ViewShopItemInherit>();
          
        /// <summary>
        /// 商家编码更新到淘宝失败列表，初始值
        /// </summary>
        public SortedList<string, Dictionary<string, string>> failedItemList = new SortedList<string, Dictionary<string, string>>();
        
        /// <summary>
        /// 线程完成个数
        /// </summary>
        private int completedNum = 0;

        public InputStock()
        {
            InitializeComponent();
        }

        public InputStock(List<ViewShopItemInherit> vsiiList)
        {
            InitializeComponent();
            this.vsiiList.AddRange(vsiiList);
            this.inPutFailedViewItemList.AddRange(vsiiList);
        }


        private void InputStock_Load(object sender, EventArgs e)
        {
            this.workerInput.RunWorkerAsync(vsiiList);
            SortedList<string, Dictionary<string, string>> sortItemList = BuildUpLoadItemList(vsiiList);
            this.workerUpdate.RunWorkerAsync(sortItemList);
        }

        private void workerInput_DoWork(object sender, DoWorkEventArgs e)
        {
            List<ViewShopItemInherit> viewList = e.Argument as List<ViewShopItemInherit>;
            InPutStock(viewList, e);
        }

        private void workerInput_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBarInput.Position = e.ProgressPercentage;
        }

        private void workerInput_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.completedNum++;
            if (completedNum == 2)
            {
                this.Close();
                int updateFailedItemsNum = 0;
                foreach (string nick in failedItemList.Keys)
                {
                    updateFailedItemsNum += failedItemList[nick].Count;
                }
                if (updateFailedItemsNum == 0 && inPutFailedViewItemList.Count == 0)
                {
                    XtraMessageBox.Show(Constants.INIT_UPDATE_SUCCESS, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (updateFailedItemsNum > 0)
                    {
                        DialogResult result = XtraMessageBox.Show(string.Format("您有{0}个商品商家编码没有成功更新到淘宝，需要后台重新更新吗？", updateFailedItemsNum), Constants.SYSTEM_PROMPT,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            /*必须重新赋值为1*/
                            completedNum = 1;
                            /*必须重新实例化一个，并把failedItemList清空*/
                            SortedList<string, Dictionary<string, string>> newItemList = new SortedList<string, Dictionary<string, string>>();
                            foreach (string nick in failedItemList.Keys)
                            {
                                newItemList.Add(nick, failedItemList[nick]);
                            }
                            failedItemList.Clear();
                            workerUpdate.RunWorkerAsync(newItemList);
                        }
                    }
                    else if (inPutFailedViewItemList.Count > 0)
                    {
                        XtraMessageBox.Show(string.Format("您有{0}个商品没有成功入库！", inPutFailedViewItemList.Count), Constants.SYSTEM_PROMPT,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }//else
            }
        }

        private void workerUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            SortedList<string, Dictionary<string, string>> sortItemList = e.Argument as SortedList<string, Dictionary<string, string>>;
            foreach (string nick in sortItemList.Keys)
            {
                if (workerUpdate.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                if (sortItemList[nick].Count == 0)
                {
                    continue;
                }
                try
                {
                    Dictionary<string, string> faileditemsDic = UpdateOuterId(nick, sortItemList[nick], e);
                    failedItemList.Add(nick, faileditemsDic);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private void workerUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBarUpdate.Position = e.ProgressPercentage;
        }

        private void workerUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.completedNum++;
            if (completedNum == 2)
            {
                this.Close();
                int updateFailedItemsNum = 0;
                foreach (string nick in failedItemList.Keys)
                {
                    updateFailedItemsNum += failedItemList[nick].Count;
                }
                if (updateFailedItemsNum == 0 && inPutFailedViewItemList.Count == 0)
                {
                    XtraMessageBox.Show(Constants.INIT_UPDATE_SUCCESS, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (updateFailedItemsNum > 0)
                    {
                        DialogResult result = XtraMessageBox.Show(string.Format("您有{0}个商品商家编码没有成功更新到淘宝，需要后台重新更新吗？", updateFailedItemsNum), Constants.SYSTEM_PROMPT,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            /*必须重新赋值为1*/
                            completedNum = 1;
                            /*必须重新实例化一个，并把failedItemList清空*/
                            SortedList<string, Dictionary<string, string>> newItemList = new SortedList<string, Dictionary<string, string>>();
                            foreach (string nick in failedItemList.Keys)
                            {
                                newItemList.Add(nick, failedItemList[nick]);
                            }
                            failedItemList.Clear();
                            workerUpdate.RunWorkerAsync(newItemList);
                        }
                    }
                    else if (inPutFailedViewItemList.Count >0)
                    {
                        XtraMessageBox.Show(string.Format("您有{0}个商品没有成功入库！", inPutFailedViewItemList.Count), Constants.SYSTEM_PROMPT,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }//else
            }
        }

        /// <summary>
        /// 初始化入库，将outer_id相同的商品作为同种商品处理，将成功的商品
        /// 从失败列表failedViewItemList里删除，并更新成功商品的IsAssociate为TRUE
        /// </summary>
        /// <param name="ViewItemlist"></param>
        private void InPutStock(List<ViewShopItemInherit> vItemlist, DoWorkEventArgs e)
        {
            if (vItemlist == null || vItemlist.Count == 0)
            {
                return;
            }
            //存放需要更新和关联的商品iid，outer_id
            Dictionary<string, string> itemOuterIdDic = new Dictionary<string, string>();

            //去除outer_id相同的商品
            List<ViewShopItemInherit> ViewItemlist = vItemlist.Distinct(new ShopItemComparer()).ToList();

            /*查找与数据库重复的商品，将其从入库列表中跳过*/
            List<string> dRepeatedOuterIdList = StockItemService.GetWhereInOuterIds( vItemlist.Select(v => v.outer_id).Distinct().ToList());
            ViewItemlist=ViewItemlist.SkipWhile(v => dRepeatedOuterIdList.Contains(v.outer_id)).ToList();
            /*将跳过入库的商品信息加进itemOuterIdDic中*/
            foreach (string outer_id in dRepeatedOuterIdList)
            {
                //失败列表删除与当前商品outer_id相同的所有商品
                inPutFailedViewItemList.RemoveAll(v => v.outer_id == outer_id);
                 //查找所有outer_id下的商品iid并加入iidlist中
                  vItemlist.FindAll(v => v.outer_id == outer_id).ForEach(a => itemOuterIdDic.Add(a.iid,a.outer_id));
            }
            if (ViewItemlist.Count>0)
            {
                //获取cid下的所有属性值
                List<View_ItemPropValue> viewPropValueList = ItemPropValueService.GetView_ItemPropValueList(ViewItemlist.First().cid, "-1", "-1");

                /*销售属性是否有必填项*/
                bool salePropHasMust = false;
                if (viewPropValueList.Where(v => v.is_sale_prop && v.must).ToList().Count > 0)
                {
                    salePropHasMust = true;
                }

                //获取该类目下所有销售属性Id
                List<string> salePidList = viewPropValueList.Where(v => v.is_sale_prop).Select(v => v.pid).Distinct().ToList();

                /*StockInOut*/
                StockInOut stockIntOut = new StockInOut();
                #region 赋值stockInOut
                stockIntOut.AmountTax = 0;
                stockIntOut.DiscountFee = 0;
                stockIntOut.DueFee = 0;
                stockIntOut.FreightCode = string.Empty;
                stockIntOut.FreightCompany = string.Empty;
                stockIntOut.IncomeTime = DateTime.Now;
                stockIntOut.InOutCode = System.Guid.NewGuid().ToString();
                stockIntOut.InOutStatus = (int)InOutStatus.AllReach;
                stockIntOut.InOutTime = DateTime.Now;
                stockIntOut.InOutType = (int)InOutType.InitInput;
                stockIntOut.IsSettled = true;
                /*操作人*/
                stockIntOut.OperatorCode = string.Empty;
                stockIntOut.OperatorName = string.Empty;
                stockIntOut.PayTerm = 0;
                stockIntOut.PayThisTime = 0;
                stockIntOut.PayType = (int)PayType.OTHER;
                /*模糊查询字段*/
                stockIntOut.SearchText = string.Empty;
                stockIntOut.TradeOrderCode = string.Empty;
                stockIntOut.TransportCode = string.Empty;
                #endregion

                //进度报告暂存值
                int temp = 0;
                #region for循环入库
                for (int j = 0; j < ViewItemlist.Count; j++)
                {
                    if (workerInput.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    ViewShopItemInherit viewItem = ViewItemlist[j];

                    StockItem stock = new StockItem();
                    stock.HasSaleProps = !string.IsNullOrEmpty(viewItem.skus);
                    #region 赋值StockItem
                    stock.Name = viewItem.title;
                    stock.SimpleName = viewItem.title;
                    stock.OuterID = viewItem.outer_id;
                    stock.TotalQuantity = (double)viewItem.TotalQuantity;//总数量,默认值
                    stock.ProductID = viewItem.product_id;
                    stock.CatName = viewItem.name ?? string.Empty;
                    stock.Cid = viewItem.cid ?? string.Empty;
                    stock.Created = DateTime.Now;
                    stock.InputPids = viewItem.input_pids ?? string.Empty;
                    stock.InputStr = viewItem.input_str ?? string.Empty;
                    stock.IsConsignment = false;
                    stock.KeyProps = viewItem.KeyProps ?? string.Empty;
                    stock.Modified = DateTime.Now;
                    stock.NotKeyProps = viewItem.NotKeyProps ?? string.Empty;
                    stock.PicUrl = viewItem.pic_url ?? string.Empty;
                    stock.Property_Alias = viewItem.property_alias ?? string.Empty;
                    stock.Props = viewItem.props ?? string.Empty;
                    stock.SaleProps = viewItem.StockProps ?? string.Empty;
                    /*库存类目*/
                    stock.StockCatName = viewItem.StockCatName;
                    stock.StockCid = viewItem.StockCid;
                    stock.StockItemCode = System.Guid.NewGuid().ToString();
                    stock.StockItemDesc = viewItem.desc ?? string.Empty;
                    stock.StockItemImgs = viewItem.item_imgs ?? string.Empty;
                    stock.StockItemRemark = Constants.INIT_FROM_TOP;
                    stock.StockItemType = (int)Alading.Core.Enum.StockItemType.FinishGoods;
                    stock.UnitCode = viewItem.UnitCode;  //单位

                    /*空值字段*/
                    stock.SearchText = string.Empty;
                    stock.Specification = string.Empty;
                    stock.Model = string.Empty;
                    stock.Tax = string.Empty;
                    stock.TaxName = string.Empty;
                    stock.StockProps = string.Empty;//自定义属性
                    stock.StockCheckUrl = string.Empty;
                    /*辅助字段*/
                    stock.IsSelected = false;
                    #endregion

                    List<StockProduct> stockProductList = new List<StockProduct>();
                    List<StockDetail> stockDetailList = new List<StockDetail>();
                    List<StockHouseProduct> stockHouseProductList = new List<StockHouseProduct>();
                    int stockTotalQuantity = 0;
                    if (!string.IsNullOrEmpty(viewItem.props))
                    {
                        List<Taobao.Entity.Sku> skuList = new List<Taobao.Entity.Sku>();

                        SortedDictionary<string, List<string>> propValueDic = new SortedDictionary<string, List<string>>();
                        #region 按照;分割，同时去掉空白项,每一项是一个pid:vid,值放进字典propValueDic里
                        List<string> propsList = viewItem.props.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (string pv in propsList)
                        {
                            string[] pvArr = pv.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (pvArr.Length == 2)
                            {
                                if (!propValueDic.Keys.Contains(pvArr[0]))
                                {
                                    List<string> vidlist = new List<string>();
                                    vidlist.Add(pvArr[1]);
                                    propValueDic.Add(pvArr[0], vidlist);
                                }
                                else
                                {
                                    propValueDic[pvArr[0]].Add(pvArr[1]);
                                }
                            }
                        }
                        #endregion

                        /*销售属性有两个，根据条件组合Sku*/
                        if (salePidList.Count == 2)
                        {
                            //保存有值的销售属性个数
                            int salePropValueCount = 0;
                            //将props里存在值的属性串的pid放进列表itemPidlist
                            List<string> itemPidList = propValueDic.Keys.ToList();
                            //求itemPidList与salePropIdList的交集，结果的个数表示销售属性选择的个数
                            salePropValueCount = itemPidList.Intersect(salePidList).ToList().Count;

                            #region 如果销售属性包含必选项但填写项数不为2，或者不包含必选项但选择项个数为1时，组合SKU
                            //用于存放两组vid列表
                            SortedList<string, List<string>> saleValueList = new SortedList<string, List<string>>();
                            if ((salePropHasMust && salePropValueCount != 2) || !salePropHasMust && salePropValueCount == 1)
                            {
                                #region 查询属性值放进saleValueList
                                for (int i = 0; i < 2; i++)
                                {
                                    string pid = salePidList[i];
                                    /*如果商该品选择了此销售属性，则把其值加进字典里*/
                                    if (propValueDic.Keys.Contains(pid))
                                    {
                                        saleValueList.Add(pid, propValueDic[pid]);
                                    }
                                    /*该商品没有选则此销售属性，则把此属性下的所有属性值加进字典*/
                                    else
                                    {
                                        List<string> vidlist = new List<string>();
                                        viewPropValueList.Where(v => v.is_sale_prop && v.pid == pid).Select(v => v.vid).Distinct().ToList().ForEach(a => vidlist.Add(a));
                                        saleValueList.Add(pid, vidlist);
                                    }
                                }
                                #endregion

                                #region 组合Sku,加进skuList
                                if (saleValueList.Count == 2)
                                {
                                    foreach (string vid0 in saleValueList.Values[0])
                                    {
                                        foreach (string vid1 in saleValueList.Values[1])
                                        {
                                            Taobao.Entity.Sku sku = new Taobao.Entity.Sku();
                                            sku.SkuProps = string.Format("{0}:{1};{2}:{3}", saleValueList.Keys[0], vid0, saleValueList.Keys[1], vid1);
                                            sku.Quantity = 0;
                                            sku.Price = "0";
                                            /*sku.OuterId在这里不编码，最后统一编码*/
                                            skuList.Add(sku);
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion

                        }

                        Alading.Taobao.Entity.Extend.Skus skus = JsonConvert.DeserializeObject<Alading.Taobao.Entity.Extend.Skus>(viewItem.skus);
                        if (skus != null && skus.Sku != null)
                        {
                            skuList.AddRange(skus.Sku);
                        }
                        if (skuList.Count == 0)
                        {
                            Taobao.Entity.Sku sku = new Taobao.Entity.Sku();
                            sku.SkuProps = string.Empty;
                            sku.Price = viewItem.price;
                            sku.Quantity = (int)stock.TotalQuantity;
                            /*sku.OuterId在这里不编码，最后统一编码*/
                            skuList.Add(sku);
                        }
                        //根据SkuProps去重
                        skuList = skuList.Distinct(new SkuComparer()).ToList();
                        for (int i = 0; i < skuList.Count; i++)
                        {
                            Alading.Taobao.Entity.Sku sku = skuList[i];
                            StockProduct product = new StockProduct();
                            #region 赋值StockProduct
                            product.LastStockPrice = product.AvgStockPrice = 0;
                            product.CommissionPrice = 0;
                            product.MaxSkuPrice = 0;
                            product.MinSkuPrice = 0;
                            product.OccupiedQuantity = 0;
                            product.OuterID = stock.OuterID;
                            product.SkuQuantity = sku.Quantity;
                            //重新计算库存总量
                            stockTotalQuantity += sku.Quantity;

                            product.SkuPrice = product.MarketPrice = sku.Price == null ? 0 : double.Parse(sku.Price);
                            //由于sku.OuterId是不允许编辑的，所有系统编码不会有重复
                            product.SkuOuterID = string.Format("{0}-{1}", product.OuterID, i + 1);
                            product.SkuProps = sku.SkuProps;
                            //分割属性查找
                            #region 翻译销售属性组合
                            List<string> skuPropsList = product.SkuProps.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            string propstr = string.Empty;
                            //每个PV值都为pid:vid格式
                            foreach (string pv in skuPropsList)
                            {
                                string[] pvArr = pv.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                if (pvArr.Length == 2)
                                {
                                    View_ItemPropValue viewProp = viewPropValueList.SingleOrDefault(vv => vv.pid == pvArr[0] && vv.vid == pvArr[1]);
                                    if (viewProp != null)
                                    {
                                        //重新命名销售属性,如：1627207:28341:黑色;1627207:3232481:棕色
                                        if (!string.IsNullOrEmpty(viewItem.property_alias) && viewItem.property_alias.Contains(pv))
                                        {
                                            List<string> propertyAliasList = viewItem.property_alias.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                            foreach (string propertyAlias in propertyAliasList)
                                            {
                                                if (propertyAlias.Contains(pv))
                                                {
                                                    propstr += string.Format("{0}:{1};", viewProp.prop_name, propertyAlias.Substring(propertyAlias.LastIndexOf(':') + 1));
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            propstr += string.Format("{0}:{1};", viewProp.prop_name, viewProp.name_alias);
                                        }
                                    }
                                }
                            }
                            propstr = propstr.TrimEnd(';');
                            if (string.IsNullOrEmpty(propstr))
                            {
                                product.SkuProps_Str = string.Empty;
                            }
                            else
                            {
                                /*重新分割,按默认排序进行排序*/
                                skuPropsList = propstr.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                skuPropsList.Sort();
                                foreach (string property in skuPropsList)
                                {
                                    product.SkuProps_Str += string.Format("{0};", property);
                                }
                                product.SkuProps_Str = product.SkuProps_Str.TrimEnd(';');
                            }
                            #endregion

                            product.ProductStatus = 0;//
                            product.HighestNum = 0;
                            product.IsUsingWarn = false;
                            product.LowestNum = 0;
                            product.PropsAlias = string.Empty;
                            product.StockProductRemark = Constants.INIT_FROM_TOP;
                            product.WarningCount = 0;
                            product.WholeSalePrice = 0;
                            product.ProductTimeStamp = new byte[] { };
                            #endregion
                            /*加入product列表*/
                            stockProductList.Add(product);

                            StockDetail stockDetail = new StockDetail();
                            #region 赋值StockDetail
                            stockDetail.DetailRemark = string.Empty;
                            stockDetail.DetailType = (int)DetailType.InitInput;
                            stockDetail.DurabilityDate = DateTime.Now;
                            /*仓库名称*/
                            stockDetail.HouseName = viewItem.StockHouseName;
                            stockDetail.LayoutName = viewItem.StockLayoutName;
                            stockDetail.StockHouseCode = viewItem.StockHouseCode;
                            stockDetail.StockLayOutCode = viewItem.StockLayoutCode;

                            stockDetail.InOutCode = stockIntOut.InOutCode;
                            stockDetail.Price = float.Parse(product.SkuPrice.ToString());
                            stockDetail.ProductSkuOuterId = product.SkuOuterID;
                            stockDetail.Quantity = product.SkuQuantity;
                            /*搜索字段*/
                            stockDetail.SearchText = string.Empty;
                            stockDetail.StockDetailCode = System.Guid.NewGuid().ToString();
                            stockDetail.Tax = string.Empty;
                            stockDetail.TotalFee = float.Parse((product.SkuPrice * stockDetail.Quantity).ToString());
                            #endregion
                            /*加入stockDetail列表*/
                            stockDetailList.Add(stockDetail);

                            StockHouseProduct houseProduct = new StockHouseProduct();
                            #region 赋值StockHouseProduct
                            houseProduct.HouseCode = viewItem.StockHouseCode;
                            houseProduct.HouseName = viewItem.StockHouseName;
                            houseProduct.HouseProductCode = System.Guid.NewGuid().ToString();
                            houseProduct.LayoutCode = viewItem.StockLayoutCode;
                            houseProduct.LayoutName = viewItem.StockLayoutName;
                            houseProduct.Num = product.SkuQuantity;
                            houseProduct.SkuOuterID = product.SkuOuterID;
                            #endregion
                            stockHouseProductList.Add(houseProduct);
                        }
                    }
                    //库存总量重新赋值
                    stock.TotalQuantity = stockTotalQuantity;
                    if (StockItemService.InitInput(stock, stockProductList, stockDetailList, stockHouseProductList) == ReturnType.Success)
                    {
                        StockInOutService.AddStockInOut(stockIntOut);
                        //成功一个，失败列表删除与当前商品outer_id相同的所有商品
                        inPutFailedViewItemList.RemoveAll(v => v.outer_id == viewItem.outer_id);
                        //查找所有outer_id下的商品iid并加入iidlist中
                        vItemlist.FindAll(v => v.outer_id == viewItem.outer_id).ForEach(a => itemOuterIdDic.Add(a.iid, a.outer_id));
                    }
                    //进度报告
                    if (workerInput != null)
                    {
                        int propgress = (int)((float)(j + 1) / ViewItemlist.Count * 100);
                        if (propgress > temp)
                        {
                            workerInput.ReportProgress(propgress, null);
                        }
                        temp = propgress;
                    }
                }//for 
                #endregion
            }// if (ViewItemlist.Count>0)
           
            /*更新入库成功商品的Outer_id及IsAssociate*/
            if (itemOuterIdDic.Count > 0)
            {
                ItemService.UpdateItemsOuterId(itemOuterIdDic,true);
            }
        }

        /// <summary>
        /// 更新商家编码，返回失败的商品,同时置成功商品的IsUpdate为TRUE,标识已同步到淘宝
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="itemdic"></param>
        /// <returns></returns>
        private Dictionary<string, string> UpdateOuterId(string nick, Dictionary<string, string> itemdic, DoWorkEventArgs e)
        {
            Dictionary<string, string> failedItemsDic = new Dictionary<string, string>();
            List<string> iidlist = itemdic.Keys.ToList();
            ItemReq req = new ItemReq();
            ItemRsp response = new ItemRsp();
            string session = SystemHelper.GetSessionKey(nick);
            float n = iidlist.Count;
            int temp = 0;//作用是避免进度值propgress没有改变时得重复报告
            for (int i = 0; i < n; i++)
            {
                if(workerUpdate.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                if (workerUpdate.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                string iid = iidlist[i];
                req.Iid = iid;
                req.OuterId = itemdic.SingleOrDefault(it => it.Key == iid).Value;
                try
                {
                    response = TopService.ItemUpdate(session, req);
                    if (response.Item == null)
                    {
                        failedItemsDic.Add(req.Iid, req.OuterId);
                    }
                    else
                    {
                        ItemService.UpdateItemIsUpdate(iid, true);
                    }
                }
                catch (System.Exception ex)
                {
                    failedItemsDic.Add(req.Iid, req.OuterId);
                    continue;
                }
                //进度报告
                int propgress = (int)((float)(i + 1) / n * 100);
                if (propgress > temp)
                {
                    workerUpdate.ReportProgress(propgress, null);
                }
                temp = propgress;
            }
            return failedItemsDic;
        }

        /// <summary>
        /// 构造上传商品iid及outer_id
        /// </summary>
        /// <param name="ViewItemlist"></param>
        /// <returns></returns>
        private SortedList<string, Dictionary<string, string>> BuildUpLoadItemList(List<ViewShopItemInherit> ViewItemlist)
        {
            SortedList<string, Dictionary<string, string>> itemSortList = new SortedList<string, Dictionary<string, string>>();
            IEnumerator<IGrouping<string, ViewShopItemInherit>> iter = ViewItemlist.GroupBy(v => v.nick).GetEnumerator();
            while (iter.MoveNext())
            {
                IGrouping<string, ViewShopItemInherit> itemGroup = iter.Current;
                string nick = itemGroup.Key;
                Dictionary<string, string> itemdic = new Dictionary<string, string>();
                foreach (ViewShopItemInherit item in itemGroup.ToList())
                {
                    if (!itemdic.Keys.Contains(item.iid))
                    {
                        itemdic.Add(item.iid, item.outer_id);
                    }
                }
                itemSortList.Add(nick, itemdic);
            }
            return itemSortList;
        }

        /// <summary>
        /// ViewShopItemInherit自定义比较器，根据outer_id比较两个是否为同种商品
        /// </summary>
        public class ShopItemComparer:IEqualityComparer<ViewShopItemInherit>
        {
            public bool Equals(ViewShopItemInherit x, ViewShopItemInherit y)
             {
                 if (x == null && y == null)
                     return false;
                 return x.outer_id == y.outer_id;
             }
            public int GetHashCode(ViewShopItemInherit obj)
            {
                return obj.ToString().GetHashCode();
            }
        }

        /// <summary>
        /// View_ItemPropValue自定义比较器,根据cid和pid
        /// </summary>
        public class PropValueComparer : IEqualityComparer<View_ItemPropValue>
        {
            public bool Equals(View_ItemPropValue x, View_ItemPropValue y)
            {
                if (x == null && y == null)
                    return false;
                return x.cid == y.cid && x.pid == y.pid;
            }
            public int GetHashCode(View_ItemPropValue obj)
            {
                return obj.ToString().GetHashCode();
            }
        }

        public class SkuComparer : IEqualityComparer<Taobao.Entity.Sku>
        {
            public bool Equals(Taobao.Entity.Sku x, Taobao.Entity.Sku y)
            {
                if (x == null && y == null)
                    return false;
                return x.SkuProps == y.SkuProps;
            }
            public int GetHashCode(Taobao.Entity.Sku obj)
            {
                return obj.ToString().GetHashCode();
            }
        }

        private void InputStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (workerInput.IsBusy)
            {
                workerInput.CancelAsync();
            }
            if (workerUpdate.IsBusy)
            {
                workerUpdate.CancelAsync();
            }
        }

    }
}