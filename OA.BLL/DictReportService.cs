using OA.IBLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    public class DictReportService:BaseService<DictReport>,IDictReportService
    {
        public override IDAL.IBaseDal<DictReport> SetCurrentDal()
        {
            return this.CurrentDBSession.DictReportDal;
        }

    }
}
