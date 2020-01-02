using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IDAL
{
    public interface IDBSession
    {
        DbContext OADbContext { get; }

        IDcitUserDal DictUserDal { get; }

        IDictRoleDal DictRoleDal { get; }

        IDictMenuDal DictMenuDal { get; }

        IDictUserInfoDal DictUserInfoDal { get; }

        IDictReportDal DictReportDal { get; }

        IDictEducationDal DictEducationDal { get; }

        IDictArgumentDal DictArgumentDal { get; }


        IDictConfigDal DictConfigDal { get; }

        IStaticPageRecordDal StaticPageRecordDal { get;}

        IStaticPageImgReocrdDal StaticPageImgReocrdDal { get;}

        int SaveChange();
    }
}
