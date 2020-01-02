using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class InitData
    {
        public static OAContext InitBasicData(OAContext context)
        {
            //超级用户
            DictUser dictUser = new DictUser()
            {
                UName = "超级用户",
                UID = "0000",
                Upwd = Common.ContextCode.MD5Encryption("0000"),
                CreateTime = DateTime.Now,
                Remark = "自动创建"
            };

            //管理员角色
            DictRole dictRole = new DictRole()
            {
                Remark = "管理员",
                CreateTime = DateTime.Now,
                RoleName = "管理员",
            };

            //权限管理 一级菜单
            DictMenu menuAuthority = new DictMenu()
            {
                MenuName = "权限管理",
                MenuPath = "/User/Index",
                ImagePath = "/Content/Win-ui/img/icon/UserRole.png",
                ParentID = 0,
                CreateTime = DateTime.Now
            };

            //二级菜单
            DictMenu menuUser = new DictMenu()
            {
                MenuName = "用户管理",
                ParentID = 1,
                MenuPath = "/User/UserManagement",
                CreateTime = DateTime.Now
            };

            //二级菜单
            DictMenu menuRole = new DictMenu()
            {
                MenuName = "角色管理",
                ParentID = 1,
                MenuPath = "/User/RoleManagement",
                CreateTime = DateTime.Now
            };

            //系统设置 一级菜单
            DictMenu menuSys = new DictMenu()
            {
                MenuName = "系统设置",
                ParentID = 0,
                MenuPath = "/Sys/Index",
                ImagePath = "/Content/Win-ui/img/icon/System.png",
                CreateTime = DateTime.Now
            };

            //二级菜单
            DictMenu menuDict = new DictMenu()
            {
                MenuName = "菜单设置",
                ParentID = 4,
                MenuPath = "/Sys/MenuManagement",
                ImagePath = "",
                CreateTime = DateTime.Now
            };

            //二级菜单
            DictMenu menuSystem = new DictMenu()
            {
                MenuName = "参数配置",
                ParentID = 4,
                MenuPath = "/Sys/ParameterMenagement",
                ImagePath = "",
                CreateTime = DateTime.Now
            };

            //报表管理 一级菜单
            DictMenu menuReport = new DictMenu()
            {
                MenuName = "报表管理",
                ParentID = 0,
                MenuPath = "/Report/Index",
                ImagePath = "/Content/Win-ui/img/icon/Report.ico",
                CreateTime = DateTime.Now
            };

            // 二级菜单
            DictMenu menuReportOperation = new DictMenu()
            {
                MenuName = "报表设置",
                ParentID = 7,
                MenuPath = "/Report/ReportDesign",
                ImagePath = "",
                CreateTime = DateTime.Now
            };

            DictMenu menuReportSearchData = new DictMenu()
            {
                MenuName = "查询数据",
                ParentID = 7,
                MenuPath = "/Report/SearchData",
                ImagePath = "",
                CreateTime = DateTime.Now
            };

            DictEducation dictEducBS = new DictEducation()
            {
                EducationName = "博士",
            };

            DictEducation dictEducSS = new DictEducation()
            {
                EducationName = "硕士",
            };

            DictEducation dictEducBK = new DictEducation()
            {
                EducationName = "本科",
            };

            DictEducation dictEducZK = new DictEducation()
            {
                EducationName = "专科",
            };

            DictEducation dictEducGZ = new DictEducation()
            {
                EducationName = "高中",
            };

            DictArgument dateArg = new DictArgument
            {
                Name = "日期",
                Format = "YYYY-MM-DD",
                Remark = "日期格式为：1111-11-11"
            };

            DictArgument timeArg = new DictArgument
            {
                Name = "时间",
                Format = "YYYY-MM-DD HH:MI:SS",
                Remark = "时间格式为：1111-11-11 23:23:23"
            };

            dictRole.DictUser.Add(dictUser);

            dictRole.DictMenu.Add(menuAuthority);
            dictRole.DictMenu.Add(menuUser);
            dictRole.DictMenu.Add(menuRole);
            dictRole.DictMenu.Add(menuSys);
            dictRole.DictMenu.Add(menuDict);
            dictRole.DictMenu.Add(menuSystem);
            dictRole.DictMenu.Add(menuReport);
            dictRole.DictMenu.Add(menuReportOperation);
            dictRole.DictMenu.Add(menuReportSearchData);

            context.DictUser.Add(dictUser);
            context.DictRole.Add(dictRole);
            context.DictMenu.Add(menuAuthority);
            context.DictMenu.Add(menuUser);
            context.DictMenu.Add(menuRole);
            context.DictMenu.Add(menuSys);
            context.DictMenu.Add(menuDict);
            context.DictMenu.Add(menuSystem);
            context.DictMenu.Add(menuReport);
            context.DictMenu.Add(menuReportOperation);
            context.DictMenu.Add(menuReportSearchData);
            context.DictEducation.Add(dictEducBS);
            context.DictEducation.Add(dictEducSS);
            context.DictEducation.Add(dictEducBK);
            context.DictEducation.Add(dictEducZK);
            context.DictEducation.Add(dictEducGZ);
            context.DictArgument.Add(dateArg);
            context.DictArgument.Add(timeArg);
            return context;
        }
    }
}
