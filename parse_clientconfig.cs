using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace COMtest
{
    public class parse_config
    {
        public parse_config(string strfilepath)
        {
            m_strFilePath = strfilepath;
            loadfile();//获得文件
        }

        //解析客户端XML文件
        public bool loadfile()
        {
            try
            {
                XmlDocument doc = new XmlDocument();//解析init.xml文档

                doc.Load(m_strFilePath);

                //解析  得到根节点
                XmlNode root = doc.SelectSingleNode("root");
                foreach (XmlNode item in root)//遍历整个xml文件
                {
                    XmlElement tempelement = (XmlElement)item;//实例化每个元素
                    string strname = tempelement.Name;
                    if (strname == "扫码器配置")
                    {
                        m_Scaner_COM_config = tempelement.InnerText;
                    }
                    else if (strname == "测试仪配置")
                    {
                        m_TestMachine_Config = tempelement.InnerText;
                    }
                    else if (strname == "PLC配置")
                    {
                        m_PLC_IP_config = tempelement.InnerText;
                    }
                 
                }
            }
            catch (Exception exx)
            {
                throw new Exception(exx.Message);
            }
            finally
            {

            }
            return true;
        }

        public string m_Scaner_COM_config = "";
        public string m_TestMachine_Config = "";
        public string m_PLC_IP_config = "";
     

        public string m_strFilePath
        {
            get;
            set;
        }

  
    }
}
