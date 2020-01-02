using Memcached.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemcachedClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] serverList = { "192.168.199.130:11211" };
            //创建一个Memcache连接池
            SockIOPool sockPool = SockIOPool.GetInstance("MemcachedPool");
            //Set Memcache Server List
            sockPool.SetServers(serverList);
            // Set Servers Initial Connections Number
            sockPool.InitConnections = 3;
            sockPool.MinConnections = 3;
            sockPool.MaxConnections = 5;

            //Set TimeOut of Connection
            sockPool.SocketConnectTimeout = 1000;
            //Set TimeOut of Reads
            sockPool.SocketTimeout = 3000;

            sockPool.MaintenanceSleep = 30;
            sockPool.Failover = true;

            sockPool.Nagle = false;
            sockPool.Initialize();

            MemcachedClient mClient = new MemcachedClient();
            mClient.EnableCompression = false;
            mClient.PoolName = "MemcachedPool";

            mClient.Set("key", "value");
            if (!mClient.KeyExists("key"))
            {
                Console.WriteLine("不存在该key");
            }
            else
            {

                Console.WriteLine(mClient.Get("key").ToString());
                mClient.Delete("key");
                if (!mClient.KeyExists("key"))
                {
                    Console.WriteLine("不存在该key");
                }
                else
                {
                    Console.WriteLine(mClient.Get("key").ToString());
                }
            }
            SockIOPool.GetInstance("MemcachedPool").Shutdown();
        }
    }
}
