using Newtonsoft.Json;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
    [SessionFilter]
    public class HomeController : Controller
    {
        IBLL.IDictMenuService MenuSerivce { get; set; }
        //
        // GET: /Home/
        public ActionResult Index()
        {         
            return View();
        }

        [HttpPost]
        public ActionResult DictMenu()
        {
            DictUser userInfo = (DictUser)Common.SessionOperater.GetValue("UserInfo");
            List<DictMenu> dictMenu = MenuSerivce.GetDictMenu(userInfo).ToList();
            if (dictMenu != null && dictMenu.Count == 0)
                return Content(JsonConvert.SerializeObject(new {state="0"}));
            else
            {
                return Content(JsonConvert.SerializeObject(new {state="1",data=dictMenu},new JsonSerializerSettings(){ ReferenceLoopHandling= ReferenceLoopHandling.Ignore}));
            }
        }
    }
}
