using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Taobao.Entity.Extend
{
    public class ProductReq:Product
    {
       /// <summary>
        /// 产品的非关键属性列表.格式:pid:vid;pid:vid. 
       /// </summary>
        public string Binds
        {
            get;
            set;
        }

        /// <summary>
        /// 商品类目ID
        /// </summary>
        public int cid
        {
            get;
            set;
        }

        /// <summary>
        /// 搜索的关键词是用来搜索产品的title以及关键属性值的名称
        /// </summary>
        public string q
        {
            get;
            set;
        }

        /// <summary>
        /// 每页条数.每页返回最多返回100条,默认值为40.
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 页码.传入值为1代表第一页,传入值为2代表第二页,依此类推
        /// </summary>
        public int PageNo
        {
            get;
            set;
         }

        /// <summary>
        /// 用户自定义属性,结构：pid1:value1;pid2:value2
        /// </summary>
        public  string CustomerProps
          {
              get;
              set;
          }

        /// <summary>
        /// 图片内容.图片最大为2M,只支持JPG,GIF. 
        /// </summary>
        public byte[] Image
        {
            get;
            set;
        }

        /// <summary>
        /// image的或propImage的id
        /// </summary>
        public int id
       {
           get;
           set;
       }

        /// <summary>
        /// 图片序号 
        /// </summary>
        public int Position
       {
            get;
            set;
        }

        /// <summary>
        /// 是否将该图片设为主图.可选值:true,false;默认值:false
        /// </summary>
        public Boolean IsMajor
        {
            get;
            set;
        }
    }
}
