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
    public class UserController : Controller
    {
        public IBLL.IDictMenuService DictMenuService { get; set; }

        public IBLL.IDictUserService DictUserService { get; set; }

        public IBLL.IDictEducationService DictEducationService { get; set; }

        public IBLL.IDictUserInfoService DictUserInfoService { get; set; }

        public IBLL.IDictRoleService DictRoleService { get; set; }

        //
        // GET: /User/

        public ActionResult Index(int parentId)
        {
            ViewBag.ParentId = parentId;
            ViewBag.Path = "/User/LoadMenu";
            return View();
        }

        public ActionResult LoadMenu(int parentId)
        {
            List<DictMenu> menuList = DictMenuService.GetSubMenu((DictUser)Common.SessionOperater.GetValue("UserInfo"), parentId).ToList();
            if (menuList.Count == 0)
            {
                return Content(JsonConvert.SerializeObject(new { state = "0" }));
            }
            else
            {
                return Content(JsonConvert.SerializeObject(new { state = "1", data = menuList }, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
            }
        }

        //用户管理
        public ActionResult UserManagement()
        {
            return View();
        }

        //加载学历字典
        public ActionResult GetDictEducation()
        {
            IQueryable<DictEducation> educationList = DictEducationService.LoadEntities(e => 1 == 1);
            return Json(educationList);
        }

        //加载权限字典
        public ActionResult GetDictRole()
        {
            IQueryable<DictRole> roleList = DictRoleService.LoadEntities(r => 1 == 1);
            return Content(JsonConvert.SerializeObject(roleList, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        //加载用户列表
        public ActionResult LoadUserList(int rows, int page)
        {
            if (rows == 0)
            {
                rows = 20;
            }
            if (page == 0)
            {
                page = 1;
            }
            int pageCount = 0;
            var userList = DictUserService.LoadPageEntities(page, rows, out pageCount, u => true, u => u.ID)
                .Select(u => new
                {
                    ID = u.ID,
                    UID = u.UID,
                    UName = u.UName,
                    Remark = u.Remark,
                    CreateTime = u.CreateTime,
                    DetailFlag = u.DictUserInfo == null ? "无" : "有",
                    DictRoleName = u.DictRole.Select(r => r.RoleName)
                });
            List<object> list = new List<object>();
            foreach (var item in userList)
            {
                list.Add(new
                {

                    ID = item.ID,
                    UID = item.UID,
                    UName = item.UName,
                    Remark = item.Remark,
                    CreateTime = item.CreateTime,
                    DetailFlag = item.DetailFlag,
                    DictRole = string.Join(",", item.DictRoleName)
                });
            }
            return Content(JsonConvert.SerializeObject(new { total = pageCount, rows = list }, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

        }

        public ActionResult Verify(string content, string data)
        {
            bool isSuccessVerify = false;
            switch (content)
            {
                case "UID":
                default:
                    DictUser user = DictUserService.LoadEntities(u => u.UID == data).FirstOrDefault();
                    isSuccessVerify = user == null;
                    break;
            }
            //验证成功1,会转为为true;
            if (isSuccessVerify)
                return Content("1");
            else
                return Content("");
        }


        //增加用户
        [HttpPost]
        public ActionResult AddUser()
        {
            //接收基本数据
            DictUser user = new DictUser();
            user.UID = Request.Form["UID"];
            user.UName = Request.Form["UName"];
            user.Upwd = Common.ContextCode.MD5Encryption(Request.Form["Upwd"]);
            user.Remark = Request.Form["Remark"];
            user.CreateTime = DateTime.Now;

            string[] roleIDArray = Request.Form["DictRole"].Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < roleIDArray.Length; i++)
            {
                int roleId = Convert.ToInt32(roleIDArray[i]);
                user.DictRole.Add(DictRoleService.LoadEntities(r => r.ID == roleId).First());
            }

            DictUserService.AddEntity(user);
            if (Request.Form["DetailTrue"].Length != 0)
            {
                DictUserInfo userInfo = new DictUserInfo();
                userInfo.Birthday = Convert.ToDateTime(Request.Form["Birthday"]);
                userInfo.DictEducationID = Convert.ToInt32(Request.Form["DictEducationID"]);
                userInfo.EntryTime = Convert.ToDateTime(Request.Form["EntryTime"]);
                userInfo.IdentityCard = Request.Form["IdentityCard"];
                userInfo.NativePlace = Request.Form["NativePlace"];
                userInfo.Salary = Convert.ToDecimal(Request.Form["Salary"]);
                userInfo.Sex = Request.Form["Sex"];
                userInfo.Tel = Request.Form["Tel"];

                user.DictUserInfo = userInfo;
                DictUserInfoService.AddEntity(userInfo);
            }
            int affectRow = DictUserService.SaveChange();
            if (affectRow != 0)
                return Content("1");
            else
                return Content("0#用户信息保存失败!");
        }

        //获取修改用户信息
        public ActionResult UserLoad(int id)
        {
            DictUser user = DictUserService.LoadEntities(u => u.ID == id).FirstOrDefault();
            if (user.DictUserInfo == null)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    ID = user.ID,
                    UID = user.UID,
                    UName = user.UName,
                    Remark = user.Remark,
                    DictRole = string.Join(",", user.DictRole.Select(r => r.ID.ToString()).ToArray())
                },
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            else
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    ID = user.ID,
                    UID = user.UID,
                    UName = user.UName,
                    Remark = user.Remark,
                    EntryTime = user.DictUserInfo.EntryTime,
                    IdentityCard = user.DictUserInfo.IdentityCard,
                    Sex = user.DictUserInfo.Sex,
                    Birthday = user.DictUserInfo.Birthday,
                    DictEducationID = user.DictUserInfo.DictEducationID,
                    Tel = user.DictUserInfo.Tel,
                    Salary = user.DictUserInfo.Salary,
                    NativePlace = user.DictUserInfo.NativePlace,
                    DictRole = string.Join(",", user.DictRole.Select(r => r.ID.ToString()).ToArray())
                },
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
        }

        //编辑用户
        public ActionResult EditUser()
        {
            int userID = Convert.ToInt32(Request.Form["ID"]);
            DictUser user = DictUserService.LoadEntities(u => u.ID == userID).First();
            user.UID = Request.Form["UID"];
            user.UName = Request.Form["UName"];
            if (Request.Form["Upwd"] != "" && Request.Form["Upwd"] != null)
            {
                user.Upwd = Common.ContextCode.MD5Encryption(Request.Form["Upwd"]);
            }
            user.Remark = Request.Form["Remark"];
            user.ModifiedTime = DateTime.Now;

            user.DictRole.Clear();
            string[] roleIDArray = Request.Form["DictRole"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < roleIDArray.Length; i++)
            {
                int roleId = Convert.ToInt32(roleIDArray[i]);
                user.DictRole.Add(DictRoleService.LoadEntities(r => r.ID == roleId).First());
            }
            if (Request.Form["DetailTrue"].Length != 0)
            {
                DictUserInfo userInfo = null;
                if (user.DictUserInfo == null)
                {
                    userInfo = new DictUserInfo();
                    DictUserInfoService.AddEntity(userInfo);
                    user.DictUserInfo = userInfo;
                }
                else
                {
                    userInfo = user.DictUserInfo;
                }
                userInfo.Birthday = Convert.ToDateTime(Request.Form["Birthday"]);
                userInfo.DictEducationID = Convert.ToInt32(Request.Form["DictEducationID"]);
                userInfo.EntryTime = Convert.ToDateTime(Request.Form["EntryTime"]);
                userInfo.IdentityCard = Request.Form["IdentityCard"];
                userInfo.NativePlace = Request.Form["NativePlace"];
                userInfo.Salary = Convert.ToDecimal(Request.Form["Salary"]);
                userInfo.Sex = Request.Form["Sex"];
                userInfo.Tel = Request.Form["Tel"];
            }
            int affectRow = DictUserService.SaveChange();
            if (affectRow != 0)
                return Content("1");
            else
                return Content("0#用户信息更新失败!");
        }

        //删除用户
        public ActionResult RemoveUser(int id)
        {
            DictUserService.DeleteEntity(new DictUser() { ID = id });
            int affectRow = DictUserService.SaveChange();
            if (affectRow == 0)
            {
                return Content("0#删除失败");
            }
            else
            {
                return Content("1");
            }
        }

        //角色
        public ActionResult RoleManagement()
        {
            return View();
        }

        //菜单
        public ActionResult GetDictMenu()
        {
            List<DictMenu> dictMenu = DictMenuService.LoadEntities(m => m.ParentID == 0).OrderBy(m => m.ID).ToList<DictMenu>();
            var query = dictMenu.Select(m => new
            {
                m.ID,
                m.MenuName,
                m.MenuPath,
                m.Remark,
                children = DictMenuService.LoadEntities(sm => sm.ParentID == m.ID).Select(sm => new
                {
                    sm.ID,
                    sm.MenuName,
                    sm.MenuPath,
                    sm.Remark
                })
            });
            return Content(JsonConvert.SerializeObject(query, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        //获取指定角色菜单
        public ActionResult GetRoleMenu(int id)
        {
            string menuIdStr = string.Join(",", DictRoleService.LoadEntities(r => r.ID == id).SelectMany(r => r.DictMenu).Select(m => m.ID).ToArray<int>());
            return Content(menuIdStr);
        }

        //增加角色
        public ActionResult AddRole()
        {
            string roleName = Request["RoleName"];
            string remark = Request["RoleRemark"];
            DictRoleService.AddEntity(new DictRole() { RoleName = roleName, Remark = remark, CreateTime = DateTime.Now });
            int affect = DictRoleService.SaveChange();
            if (affect == 0)
            {

                return Content("0");
            }
            else
            {
                return Content("1");
            }
        }

        //编辑角色
        public ActionResult EditRole()
        {
            int roleId = Convert.ToInt32(Request["RoleID"]);
            string roleName = Request["RoleName"];
            string roleRemark = Request["RoleRemark"];
            DictRole dictRole = DictRoleService.LoadEntities(r => r.ID == roleId).First();
            dictRole.RoleName = roleName;
            dictRole.Remark = roleRemark;
            dictRole.ModifiedTime = DateTime.Now;
            int affectRow = DictRoleService.SaveChange();
            if (affectRow == 0)
            {
                return Content("0");
            }
            else
            {
                return Content("1"); 
            }
        }

        //移除角色
        public ActionResult RemoveRole(int roleId)
        {
            DictRoleService.DeleteEntity(new DictRole() { ID = roleId });
            int affectRow = DictRoleService.SaveChange();
            if (affectRow == 0)
            {
                return Content("0");
            }
            else
            {
                return Content("1");
            }
        }

        public ActionResult SaveRoleMenu()
        {
            int roleId = Convert.ToInt32(Request["RoleID"]);
            string[] strArray = Request["MenuArray"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            DictRole dictRole = DictRoleService.LoadEntities(r => r.ID == roleId).First();
            int roleMenuCount = dictRole.DictMenu.Count;
            int affectRow;
            if (roleMenuCount != 0)
            {
                dictRole.DictMenu = new List<DictMenu>();
                affectRow = DictRoleService.SaveChange();
                if (affectRow == 0)
                {
                    return Content("0");
                }
            }
            List<DictMenu> addList = DictMenuService.LoadEntities(m => strArray.Contains(m.ID.ToString())).ToList();
            if (dictRole.DictMenu.Count == 0 && addList.Count == 0)
            {
                return Content("1");
            }
            dictRole.DictMenu = addList;
            affectRow = DictRoleService.SaveChange();
            if (affectRow == 0)
            {
                return Content("0");
            }
            else
            {
                return Content("1");
            }            
        }
    }
}
