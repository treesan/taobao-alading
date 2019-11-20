using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace Alading.Core.Helper
{
    public class ConfigHelper
    {
        public string strFileName;
        public string configName;
        public string configValue;
        public ConfigHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public string ReadConfig(string configKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                if (nodes[i].Attributes["key"] != null)
                {
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素
                    if (att.Value == "" + configKey + "")
                    {
                        //对目标元素中的第二个属性赋值
                        att = nodes[i].Attributes["value"];
                        configValue = att.Value;
                        break;
                    }
                }
            }
            return configValue;
        }

        //得到程序的config文件的名称以及其所在的全路径
        public void SetConfigName(string strConfigName)
        {
            configName = strConfigName;
            //获得配置文件的全路径
            GetFullPath();
        }

        public void GetFullPath()
        {
            //获得配置文件的全路径
            strFileName = Application.StartupPath + configName;
        }

        public void SaveConfig(string configKey, string configValue)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                if (nodes[i].Attributes["key"] != null)
                {
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素
                    if (att.Value == "" + configKey + "")
                    {
                        //对目标元素中的第二个属性赋值
                        att = nodes[i].Attributes["value"];
                        att.Value = configValue;
                        break;
                    }
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
        }
    }
}
