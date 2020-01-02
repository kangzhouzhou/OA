using OA.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OA.DALFactory
{
    /// <summary>
    /// 创建类的实例
    /// </summary>
    public class AbstractFactory
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];

        private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];

        private static object CreateInstance(string fullClassName)
        {
            Assembly ass = Assembly.Load(AssemblyPath);
            return ass.CreateInstance(fullClassName);
        }

        public static IDcitUserDal CreateDictUserDal()
        {
            string fullClassName = NameSpace + ".DictUserDal";
            return (IDcitUserDal)CreateInstance(fullClassName);
        }

        public static IDictRoleDal CreateDictRoleDal() 
        {
            string fullClassName = NameSpace + ".DictRoleDal";
            return (IDictRoleDal)CreateInstance(fullClassName);
        }

        public static IDictMenuDal CreateDictMenuDal()
        {
            string fullClassName = NameSpace + ".DictMenuDal";
            return (IDictMenuDal)CreateInstance(fullClassName);
        }

        public static IDictUserInfoDal CreateDictUserInfoDal()
        {
            string fullClassName = NameSpace + ".DictUserInfoDal";
            return (IDictUserInfoDal)CreateInstance(fullClassName);
        }

        public static IDictReportDal CreateDictReportDal()
        {
            string fullClassName = NameSpace + ".DictReportDal";
            return (IDictReportDal)CreateInstance(fullClassName);
        }

        public static IDictEducationDal CreateDictEducationDal()
        {
            string fullClassName = NameSpace + ".DictEducationDal";
            return (IDictEducationDal)CreateInstance(fullClassName);
        }

        public static IDatabaseDal CreateDatabaseDal()
        {
            string fullClassName= NameSpace + ".DatabaseDal";
            return (IDatabaseDal)CreateInstance(fullClassName);
        }

        public static IDictArgumentDal CreateDictArgumentDal()
        {
            string fullClassName = NameSpace + ".DictArgumentDal";
            return (IDictArgumentDal)CreateInstance(fullClassName);
        }

        public static IDictConfigDal CreateDictConfigDal()
        {
            string fullClassName = NameSpace + ".DictConfigDal";
            return (IDictConfigDal)CreateInstance(fullClassName);
        }

        public static IStaticPageRecordDal CreateStaticPageRecordDal()
        {
            string fullClassName = NameSpace + ".StaticPageRecordDal";
            return (IStaticPageRecordDal)CreateInstance(fullClassName);
        }

        public static IStaticPageImgReocrdDal CreateStaticPageImgReocrdDal()
        {
            string fullClassName = NameSpace + ".StaticPageImgReocrdDal";
            return (IStaticPageImgReocrdDal)CreateInstance(fullClassName);
        }
    }
}
