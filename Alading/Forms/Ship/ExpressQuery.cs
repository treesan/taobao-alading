using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Express_Query
{
   public class ExpressQuery
    {
       public static ResultInfo Query(string company, string querynum)
       {
           company = company.ToLower();
           if (string.Compare(company, "CCES", true) == 0)////////CCES快递
           {
               CCES queryEx = new CCES(querynum);
               return queryEx.query();
           }
           if (string.Compare(company, "DeBang", true) == 0)////德邦快递
           {
               DeBang queryEx = new DeBang(querynum);
               return queryEx.query();
           }
           if (string.Compare(company, "EMS",true)==0)//////////EMS快递
           {
               //EMS queryEx = new EMS(querynum);
               //return queryEx.queryEmail();
           }
           if (string.Compare(company, "HuiTong", true)==0)////////汇通快递
           {
               HuiTong queryEx = new HuiTong(querynum);
               return queryEx.query();
           }
           if (string.Compare(company, "ZJS", true) == 0)//////////宅急送快递
           {
               ZJS_express queryEx = new ZJS_express(querynum);
               return queryEx.query();
           }
           if (string.Compare(company, "ShengTong", true) == 0)////////申通快递
           {
               ShengTong queryEx = new ShengTong(querynum);
               return queryEx.query();
           }
           if(string.Compare(company,"ShunFeng",true)==0)////////顺丰快递
           {
               ShunfengQuery queryEx=new ShunfengQuery(querynum);
               return queryEx.query();
           }
           if (string.Compare(company, "TianTian", true) == 0)/////////天天快递
           {
               TianTian queryEx = new TianTian(querynum);
               return queryEx.query();
           }
           if (string.Compare(company, "YiBang",true) == 0)///////一邦快递
           {
               YiBang queryEx = new YiBang(querynum);
               return queryEx.query();
           }
           if (string.Compare(company, "YuanTong", true)==0)/////圆通快递
           {
               YuantongEmail queryEx = new YuantongEmail(querynum);
               return  queryEx.query();
           }
           if (string.Compare(company, "YunDa", true)==0)///////韵达快递
           {
               YunDa queryEx = new YunDa(querynum);
               return queryEx.query();
           }
           if (string.Compare(company, "ZhongTie", true)==0)/////////中铁快递
           {
               ZhongTie queryEx = new ZhongTie(querynum);
               return queryEx.query();
           }
           if(string.Compare(company,"ZhongTong",true)==0)//////////中通快递
           {
               ZhongTong queryEx=new ZhongTong(querynum);
               return  queryEx.query();
           }
           return null;
       }

      /* //用法样例如下----看完删除
       public static void Main()
       {
           ResultInfo back1 = Query("CCES", "2168450324");
           ResultInfo back2 = Query("DeBang", "22689510");
         ResultInfo back3 = Query("Ems", "ED809618702CS");
           ResultInfo back4 = Query("HuiTong", "0000093705412");
           ResultInfo back5 = Query("ShengTong", "268963628677");
           ResultInfo back6 = Query("ShunFeng", "536025494011");
          ResultInfo back7 = Query("TianTian", "1200193498473");///////////此单号已不能用，请另找
           ResultInfo back8 = Query("YiBang", "8033053107");
           ResultInfo back9 = Query("YuanTong", "2221570573");
           ResultInfo back10 = Query("YunDa", "1200174512868");
           ResultInfo back11 = Query("ZhongTie", "K2100200443165");
          ResultInfo back12 = Query("ZhongTong", "200855352403");
           ResultInfo back13 = Query("ZJS", "2696084473");
           Console.WriteLine("dasfd");
           
       }*/
    }
}
