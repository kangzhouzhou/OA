using Memcached.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OA.Common
{
    public class MemCachedHelper
    {
        static MemCachedHelper()
        {
            #region MemCached配置文件读取
            XmlDocument xml = new XmlDocument();
            xml.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs/MemCachedConfig.xml"));
            XmlElement xmlRootElement = xml.DocumentElement;
            if (xmlRootElement.InnerText == "MemCachedServers")
            {
                XmlNodeList nodeList = xmlRootElement.ChildNodes;
                if (nodeList.Count > 0)
                {
                    ServersList = new List<string>();
                    foreach (XmlNode item in nodeList)
                    {
                        if (item.GetType() == typeof(XmlElement))
                        {
                            ServersList.Add(item.Attributes["IP"].Value + ":11211");
                        }
                    }
                }
            }
            #endregion

            #region 初始化SookIOPool
            //创建MemeCachedSookIOPool,如果设置名字后,MemCached也必须设置名称
            Servers = SockIOPool.GetInstance();
            Servers.InitConnections = 3;
            Servers.MinConnections = 3;
            Servers.MaxConnections = 5;

            Servers.SocketConnectTimeout = 1000;
            Servers.SocketTimeout = 3000;

            Servers.MaintenanceSleep = 30;
            Servers.Failover = true;

            Servers.Nagle = false;
            Servers.Initialize();
            #endregion

            #region 初始化MemCachedClient
            MemClient = new MemcachedClient();

            //MemClient.PoolName = "";
            
            //禁止存储被压缩的数据
            MemClient.EnableCompression = false;
            #endregion

        }

        private static List<string> ServersList { get; set; }

        private static SockIOPool Servers { get; set; }

        private static MemcachedClient MemClient { get; set; }

        public static bool Set(string key, object objValue)
        {
            return MemClient.Set(key, objValue);
        }

        public static bool Set(string key, object objValue, DateTime expiry)
        {
            return MemClient.Set(key, objValue, expiry);
        }

        public static bool Set(string key, object objValue, int expiryTimeValue)
        {
            return MemClient.Set(key, objValue, DateTime.Now.AddSeconds(expiryTimeValue));
        }

        public static object Get(string key)
        {
            if (MemClient.KeyExists(key))
                return MemClient.Get(key);
            else
                return null;
        }

        public static bool Delete(string key)
        {
            if (MemClient.KeyExists(key))
                return MemClient.Delete(key);
            else
                return true;
        }
    }
}
