using Newtonsoft.Json;
using OA.IBLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
    //[SessionFilter]
    public class SysController : Controller
    {
        public IDictMenuService DictMenuService { get; set; }

        public IDictConfigService DictConfigService { get; set; }

        public IStaticPageRecordService StaticPageRecord { get; set; }

        //Sys界面
        public ActionResult Index(int parentId)
        {
            ViewBag.ParentID = parentId;
            ViewBag.Path = "/Sys/LoadMenu";
            return View();
        }

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

        #region 参数配置界面
        public ActionResult ParameterMenagement()
        {
            return View();
        }

        public ActionResult GetDictConfig()
        {
            List<DictConfig> list = DictConfigService.LoadEntities(c => 1 == 1).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }

        //验证配置KEY是否有效
        public ActionResult VerifyConfigKey(string key)
        {
            DictConfig dictConfig = DictConfigService.LoadEntities(c => c.Key == key).FirstOrDefault();
            if (dictConfig == null)
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        //增加配置
        public ActionResult AddConfig(DictConfig config)
        {
            DictConfigService.AddEntity(config);
            int affectRow = DictConfigService.SaveChange();
            if (affectRow == 1)
            {
                //进行更新缓存中配置
                Common.CacheOperation.UpdateSettingValue(config.Key, config.Value, config.Memo);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }
        #endregion

        #region 菜单配置界面
        public ActionResult MenuManagement()
        {
            return View();
        }
        #endregion

        #region 静态页面生成界面
        public ActionResult StaticPageSetting()
        {
            return View();
        }

        public ActionResult SaveImg()
        {
            List<StaticPageImgReocrd> list = new List<StaticPageImgReocrd>();
            //进行循环接受内容
            foreach (string item in Request.Files)
            {
                //获取网络输入流
                System.IO.Stream imgStream = Request.Files[item].InputStream;
                //根据流网络流创建图片
                Image img = Image.FromStream(imgStream);
                //生成图片的文件名,采用GUID方式
                Guid guidName = Guid.NewGuid();
                string filePath = "/StaticPage/Imgs" + "/" + guidName + Path.GetExtension(Request.Files[item].FileName);
                //进行图片保存
                img.Save(Server.MapPath(filePath));
                //保存集合,使用KEY+MEMO获取对应的描述
                list.Add(new StaticPageImgReocrd() { ImgPath = filePath, ImgMemo = Request[item + "Memo"] });
            }
            Session["Imgs"] = list;
            return Content("1");
        }

        public ActionResult SaveActivity()
        {
            //创建静态页信息
            StaticPageRecord pageRecord = new Model.StaticPageRecord();
            pageRecord.Title = Request["title"];
            pageRecord.Content = Request["content"];

            //使用GUID创建静态页的名称
            Guid guidPageName = Guid.NewGuid();
            pageRecord.PageName = guidPageName + ".html";

            //获取静态页对应的图片信息
            List<StaticPageImgReocrd> list = (List<StaticPageImgReocrd>)Session["Imgs"];
            pageRecord.ImgRecord = list;

            //静态页的创建时间
            pageRecord.CreateTime = DateTime.Now;

            //保存静态页
            StaticPageRecord.AddEntity(pageRecord);
            int affectRow = StaticPageRecord.SaveChange();
            if (affectRow > 0)
            {
                //静态页替换文本
                Dictionary<string, string> staticDic = new Dictionary<string, string>();
                staticDic.Add("$title", pageRecord.Title);
                staticDic.Add("$content", pageRecord.Content);
                staticDic.Add("$pageId", pageRecord.ID.ToString());
                staticDic.Add("$img", list[0].ImgPath);

                //生成静态页
                Common.StaticPageSetting.StaticPageCreate("/StaticPage/MasterPage/StaticMaster.html", "/StaticPage/StaticPage/" + guidPageName + ".html", staticDic);

                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        public ActionResult GetImgPath(int pageId)
        {
            List<StaticPageImgReocrd> list = StaticPageRecord.LoadEntities(s => s.ID == pageId).First().ImgRecord.ToList();
            return Content(JsonConvert.SerializeObject(list, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

        }

        public ActionResult DownImg(int pageId)
        {
            List<StaticPageImgReocrd> list = StaticPageRecord.LoadEntities(s => s.ID == pageId).First().ImgRecord.ToList();
            StaticPageImgReocrd item = list[0];
            string imgFormat = "";
            switch (Path.GetExtension(item.ImgPath).ToUpper())
            {
                case ".JGP":
                case ".JPEG":
                case ".JPE":
                default:
                    imgFormat = "image/jpeg";
                    break;
                case ".PBM":
                    imgFormat = "image/x-portable-bitmap";
                    break;
                case ".PGM":
                    imgFormat = "image/x-portable-graymap";
                    break;
                case ".PNM":
                    imgFormat = "image/x-portable-anymap";
                    break;
            }
            return File(Server.MapPath(item.ImgPath), imgFormat, pageId + Path.GetExtension(item.ImgPath));

        }
        #endregion
    }
}
