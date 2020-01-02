using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.DALFactory
{
    /// <summary>
    /// 数据会话层,就是工厂类,负责完成所有数据操作类的创建,业务层通过数据会话层来获取数据操作类的实例，所以业务层和数据层解耦
    /// 在数据会话层中提供一个方法：连接一次完成所有数据的保存
    /// 使用线程内唯一方式,来实现EF上下文唯一
    /// 由于引用问题,DBContext只能存在于DAL层
    /// </summary>
    public class DBSession:IDBSession
    {

        private DbContext _OADbContext;
        public DbContext OADbContext
        {
            get
            {
                if (_OADbContext == null)
                {
                    _OADbContext = OAContextFactory.CreateContext();
                }
                return _OADbContext;
            }
        }
        
        /// <summary>
        /// 保存所有改变至数据库
        /// </summary>
        /// <returns></returns>
        public int SaveChange()
        {
            return OADbContext.SaveChanges();
        }

        private IDcitUserDal _DictUserDal;
        public OA.IDAL.IDcitUserDal DictUserDal
        {
            get
            {
                if (_DictUserDal == null)
                {
                    _DictUserDal = AbstractFactory.CreateDictUserDal();
                }
                return _DictUserDal;
            }
        }

        private IDictRoleDal _DictRoleDal;
        public IDAL.IDictRoleDal DictRoleDal
        {
            get
            {
                if (_DictRoleDal == null)
                {
                    _DictRoleDal = AbstractFactory.CreateDictRoleDal();
                }
                return _DictRoleDal;
            }
        }

        private IDictMenuDal _DictMenuDal;
        public IDAL.IDictMenuDal DictMenuDal
        {
            get {
                if (_DictMenuDal == null)
                {
                    _DictMenuDal = AbstractFactory.CreateDictMenuDal();
                }
                return _DictMenuDal;
            }
        }

        private IDictUserInfoDal _DictUserInfoDal;
        public IDAL.IDictUserInfoDal DictUserInfoDal
        {
            get
            {
                if (_DictUserInfoDal == null)
                {
                    _DictUserInfoDal = AbstractFactory.CreateDictUserInfoDal();
                }
                return _DictUserInfoDal;
            }
        }


        private IDictReportDal _DictReportDal;
        public IDAL.IDictReportDal DictReportDal
        {
            get
            {
                if (_DictReportDal == null)
                {
                    _DictReportDal = AbstractFactory.CreateDictReportDal();
                }
                return _DictReportDal;
            }
        }

        private IDictEducationDal _DictEducationDal;
        public IDAL.IDictEducationDal DictEducationDal
        {
            get
            {
                if (_DictEducationDal == null)
                {
                    _DictEducationDal = AbstractFactory.CreateDictEducationDal();
                }
                return _DictEducationDal;
            }
        }

        private IDictArgumentDal _DictArgumentDal;
        public IDAL.IDictArgumentDal DictArgumentDal
        {
            get
            {
                if (_DictArgumentDal == null)
                {
                    _DictArgumentDal = AbstractFactory.CreateDictArgumentDal();
                }
                return _DictArgumentDal;
            }
        }

        private IDictConfigDal _DictConfigDal;
        public IDAL.IDictConfigDal DictConfigDal
        {
            get
            {
                if (_DictConfigDal == null)
                {
                    _DictConfigDal = AbstractFactory.CreateDictConfigDal();
                }
                return _DictConfigDal;
            }
        }


        private IStaticPageRecordDal _StaticPageRecordDal;
        public IDAL.IStaticPageRecordDal StaticPageRecordDal
        {
            get
            {
                if (_StaticPageRecordDal == null)
                {
                    _StaticPageRecordDal = AbstractFactory.CreateStaticPageRecordDal();
                }
                return _StaticPageRecordDal;
            }
        }


        private IStaticPageImgReocrdDal _StaticPageImgReocrdDal;
        public IDAL.IStaticPageImgReocrdDal StaticPageImgReocrdDal
        {
            get
            {
                if (_StaticPageImgReocrdDal == null)
                {
                    _StaticPageImgReocrdDal = AbstractFactory.CreateStaticPageImgReocrdDal();
                }
                return _StaticPageImgReocrdDal;
            }
        }

    }
}
