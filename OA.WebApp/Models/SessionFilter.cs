using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp
{
    public class SessionFilter:FilterAttribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.Url.ToString().Contains("StaticPage"))
                return;
            if (Common.SessionOperater.GetValue("UserInfo") == null)
            {
                //这样保证了不执行控制器下的方法。应为比许返回一个ActionResult
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}