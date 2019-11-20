﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Alading.Taobao.API
{
    class EncryptUtil
    {
        /// <summary>
        /// 生成有效签名
        /// </summary>
        public static string Signature(IDictionary<string, string> @params, string secret)
        {
            string result = null;
            if (@params == null)
            {
                return result;
            }
            IDictionary<string, string> treeMap = new SortedList<string, string>();
            foreach (KeyValuePair<string, string> kvp in @params)
            {
                treeMap.Add(kvp.Key, kvp.Value);
            }
            System.Collections.IEnumerator iter = @treeMap.Keys.GetEnumerator();
            System.Text.StringBuilder orgin = new System.Text.StringBuilder(secret);
            while (iter.MoveNext())
            {
                string name = (string)iter.Current;
                string value = @treeMap[name];

                orgin.Append(name).Append(value);
            }
            orgin.Append(secret);//API2.0增加此句代码
            try
            {
                result = Md5Hex(orgin.ToString());
            }
            catch (System.Exception)
            {
                throw new System.Exception("API参数签名错误。");
            }
            return result;
        }

        /// <summary>
        /// MD5加密并输出十六进制字符串
        /// </summary>
        public static string Md5Hex(string str)
        {
            string dest = "";
            //实例化一个md5对像
            MD5 md5 = MD5.Create();
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是大写的字母
                if (s[i] < 16)
                {
                    dest = dest + "0" + s[i].ToString("X");
                }
                else
                {
                    dest = dest + s[i].ToString("X");
                }
            }
            return dest;
        }


    }
}