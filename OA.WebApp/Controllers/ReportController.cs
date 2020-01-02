using Newtonsoft.Json;
using OA.IBLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;

namespace OA.WebApp.Controllers
{
    [SessionFilter]
    public class ReportController : Controller
    {

        public IBLL.IDictMenuService DictMenuService { get; set; }

        public IBLL.IDictReportService DictReportService { get; set; }

        public IBLL.IDictArgumentService DictArgumentSerivce { get; set; }

        public IDatabaseService ReportDataBaseService { get; set; }


        //
        // GET: /Report/
        public ActionResult Index(int parentId)
        {
            ViewBag.ParentID = parentId;
            ViewBag.Path = "/Report/LoadMenu";
            return View();
        }

        [HttpPost]
        public ActionResult LoadMenu(int parentId)
        {
            List<DictMenu> menuList = DictMenuService.GetSubMenu((DictUser)Common.SessionOperater.GetValue("UserInfo"), parentId).ToList();
            if (menuList.Count == 0)
                return Content(JsonConvert.SerializeObject(new { state = "0" }));
            else
                return Content(JsonConvert.SerializeObject(new { state = "1", data = menuList }, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        //报表设置界面
        public ActionResult ReportDesign()
        {
            return View();
        }

        //获取所有报表信息
        public ActionResult GetReportList()
        {
            var queryDictReport = DictReportService.LoadEntities(r => 1 == 1).Select(r => new
            {
                r.ID,
                r.ReportName,
                r.FuncName,
                ArgumentCount = r.DictReportArgument.Count
            }).OrderBy(nr => nr.ID);
            return Content(JsonConvert.SerializeObject(queryDictReport, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        public ActionResult GetReportInfo()
        {
            return Content("1");
        }

        public ActionResult GetDictArgument()
        {
            List<DictArgument> list = DictArgumentSerivce.LoadEntities(a => 1 == 1).ToList();
            if (list != null)
            {
                return Content(JsonConvert.SerializeObject(new { state = 1, data = list }, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            else
            {
                return Content(JsonConvert.SerializeObject(new { state = 0 }));

            }
        }

        public ActionResult EarchFun(string funName)
        {
            List<FunArgument> list = ReportDataBaseService.GetFunArgumentBll(funName);
            if (list != null)
            {
                return Content(JsonConvert.SerializeObject(new { state = 1, data = list }, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            else
            {
                return Content(JsonConvert.SerializeObject(new { state = 0 }));
            }
        }

        public ActionResult AddReport()
        {
            string funName = Request["funName"];
            string reportName = Request["reportName"];
            string reportFile = Request["reportFile"];
            //进行校验是否存在
            DictReport reportExists = DictReportService.LoadEntities(r => r.FuncName == funName || r.ReportName == reportName || r.ReportFile == reportFile).FirstOrDefault();
            if (reportExists != null)
            {
                if (reportExists.FuncName == funName)
                {
                    return Content("函数已经存在,请函数对应的报表进行编辑即可!");
                }
                else if (reportExists.ReportName == reportName)
                {
                    return Content("报表名已经存在,无法进行保存!");
                }
                else
                {
                    return Content("报表模板文件重复,一个报表只能对应一个模板!");
                }
            }

            //接受基本值
            DictReport dictReport = new DictReport()
            {
                FuncName = funName,
                ReportName = reportName,
                ReportFile = reportFile,
                Remark = Request["remark"]
            };
  
            Dictionary<string, string> dictPostArg = new Dictionary<string, string>();
            //动态获取
            foreach (string item in Request.Form.Keys)
            {
                if (item != "funName" && item != "reportName" && item != "reportFile" && item != "remark")
                {
                    dictPostArg.Add(item, Request.Form[item]);
                }
            }
            Dictionary<string,DictReportArgument> dicArg = new Dictionary<string, DictReportArgument>();
            //进行创建对象
            foreach (string item in dictPostArg.Keys)
            {
                string repItem = item.Replace("【", "").Replace("】", "");
                //如果已经存在了
                if (dicArg.Keys.Contains(repItem))
                {
                    if (item.Contains("【"))
                    {
                        dicArg[repItem].ArgumentText = dictPostArg[item];
                    }
                }
                else
                {
                    if (!item.Contains("【"))
                    {
                        dicArg.Add(item, new DictReportArgument() { ArgumentName = item, DictArgumentID = Convert.ToInt32(dictPostArg[item]) });
                    }
                }
            }
            //进行关联
            dictReport.DictReportArgument = dicArg.Values;
            //进行保存
            DictReportService.AddEntity(dictReport);
            int affect = DictReportService.SaveChange();
            if (affect == 0)
            {
                return Content("自定义报表增加失败");
            }
            else
            {
                return Content("1");
            }
        }

         //CustomReport = new WebReport();
         //   //提取数据
         //  IQueryable<DictUser> dictUser=DictUserService.LoadEntities(u=> 1==1);
         //   CustomReport.Report.Load(Path.Combine(Request.PhysicalApplicationPath + "ReportTemplate\\StaffReport.frx"));
         //   CustomReport.Report.RegisterData(dictUser.ToList(), "Table");
         //   CustomReport.Width = 1300;
         //   CustomReport.Height = 800;

        //   CustomReport.ToolbarIconsStyle = ToolbarIconsStyle.Green;
        //   ViewBag.WebReport = CustomReport;
        //   return View();
    }
}
