using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.IDAL;
using OA.IBLL;

namespace OA.BLL
{
    public class StaticPageRecordService : BaseService<StaticPageRecord>, IStaticPageRecordService
    {
        public override IBaseDal<StaticPageRecord> SetCurrentDal()
        {
            return this.CurrentDBSession.StaticPageRecordDal;
        }
    }
}
