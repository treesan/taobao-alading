using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Taobao.Entity.Extend
{
    public class ItemCatReq:ItemCat
    {
        /// <summary>
        /// 属性id (取类目属性时，传pid，不用同时传PID和parent_pid)
        /// </summary>
        public int pid
        {
            get;
            set;
        }

        /// <summary>
        /// 是否关键属性。可选值:true(是),false(否) 
        /// </summary>
        public Boolean is_key_prop
        {
            get;
            set;
        }

        /// <summary>
        /// 是否销售属性。可选值:true(是),false(否) 
        /// </summary>
        public Boolean is_sale_prop
        {
            get;
            set;
        }

        /// <summary>
        /// 是否颜色属性。可选值:true(是),false(否) 
        /// </summary>
        public  Boolean is_color_prop
        {
            get;
            set;
        }

        /// <summary>
        /// 是否枚举属性
        /// </summary>
        public  Boolean is_enum_prop
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是卖家可以自行输入的属性
        /// </summary>
         public Boolean is_input_prop
         {
             get;
             set;
         }

        /// <summary>
         /// 是否商品属性，这个属性只能放于发布商品时使用。
        /// </summary>
        public Boolean is_item_prop
        {
            get;
            set;
        }

        /// <summary>
        /// 时间戳。格式:yyyy-MM-dd HH:mm:ss

        /// </summary>
        public string datetime
        {
            get;
            set;
        }
    }
}
