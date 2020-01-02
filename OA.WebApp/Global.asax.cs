using log4net;
using OA.BLL;
using OA.IBLL;
using OA.Model;
using OA.WebApp.Models;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OA.WebApp
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    //SpringMvcApplication//
    public class MvcApplication : SpringMvcApplication//System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //函数重载，表示自动读取应用程序配置文件中的相关配置
            //log4net.Config.XmlConfigurator.Configure();

            //配置DbContext
            string dbConfig = ConfigurationManager.AppSettings["DbInitType"];
            switch (dbConfig)
            {
                case "1":
                    //只有在没有数据库的时候才会根据数据库连接配置创建新的数据库
                    Database.SetInitializer(new OACreateDatabaseIfNotExists());
                    break;
                case "2":
                    //当数据库发生变化,根据配置新建数据库，并增加基础数据 创建了OADropCreateDatabaseIfModelChanges 子类进行实现。
                    Database.SetInitializer(new OADropCreateDatabaseIfModelChanges());         
                    break;
                case "3":
                    Database.SetInitializer(new OADropCreateDatabaseAlways());
                    break;
                case "4":
                    Database.SetInitializer(new MigrateDatabaseToLatestVersion<OAContext, OADbMigrationsConfiguration>());
                    break;
            }

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //初始化配置字典
            IDictConfigService dictConfigService = new DictConfigService();
            DataTable configTable = dictConfigService.GetEntityTable();
            Common.CacheOperation.InitSetting(configTable.DefaultView);

            string logFilePath = Server.MapPath("/log");
            #region //开启线程处理错误日志
            ThreadPool.QueueUserWorkItem((path) =>
            {
                while (true)
                {
                    if (ProcessException.QueueException.Count() != 0)
                    {
                        ////取出队列中数据
                        Exception controllerException = ProcessException.QueueException.Dequeue();
                        //检索或获取一个错误记录器,参数为记录器名称,表明是由哪个错误记录器记录的
                        //ILog logger = LogManager.GetLogger("errorMsg");
                        //logger.Error(controllerException);

                        #region 自写日志保存
                        ////取出队列中数据
                        StringBuilder errorSb = new StringBuilder();
                        errorSb.Append("-----------------------------------------错误记录----------------------------------------\r\n");
                        errorSb.Append("错误时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\r\n");
                        errorSb.Append("错误信息：" + controllerException.Message + "\r\n");
                        errorSb.Append("错 误 源：" + controllerException.Source + "\r\n");
                        errorSb.Append("错误堆栈：" + controllerException.StackTrace + "\r\n");
                        File.AppendAllText(Path.Combine(path.ToString(), DateTime.Now.ToString("yyyy-MM-dd") + ".log"), errorSb.ToString());
                        #endregion
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
            }, logFilePath);
            #endregion
        }

        protected void Application_Error()
        {
            ProcessException.QueueException.Enqueue(Server.GetLastError());
            Context.Response.Redirect("/Error.html");
        }
    }
}