using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
    public class LoginController : Controller
    {
        IBLL.IDictUserService UserService { get; set; }

        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();            
        }

        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidateCode()
        {
            Common.ValidateCode validateCodeModule = new Common.ValidateCode();
            string code = validateCodeModule.CreateValidateCode(6);
            OA.Common.SessionOperater.SetValue("ValidateCode", code);
            return File(validateCodeModule.CreateValidateGraphic(code), "image/jpeg");
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult UserLogin(DictUser user)
        {            

            //进行验证码的判断
            string code = Request.Form["Code"];
            if (string.IsNullOrEmpty(code))
                return Content("0:验证码错误!");


            string validateCode = OA.Common.SessionOperater.GetValue("ValidateCode") == null ? "" : OA.Common.SessionOperater.GetValue("ValidateCode").ToString();

            if (validateCode.Length == 0)
                return Content("0:验证码错误!");

            if (!validateCode.Equals(code, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("0:验证码错误!");
            }
            else
            {
                OA.Common.SessionOperater.Remove("ValidateCode");
                DictUser userInfo = UserService.LoadEntities(u => u.UID == user.UID).FirstOrDefault();
                if (userInfo == null)
                {
                    return Content("0:账号不存在!");
                }
                else if (userInfo.Upwd != Common.ContextCode.MD5Encryption(user.Upwd))
                {
                    return Content("0:密码错误!");
                }
                else
                {
                    Common.SessionOperater.SetValue("UserInfo", userInfo);
                    return Content("1:/Home/Index");
                }
            }
        }
    }
}
