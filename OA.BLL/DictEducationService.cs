using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    public class DictEducationService : BaseService<DictEducation>,IBLL.IDictEducationService
    {
        public override IDAL.IBaseDal<DictEducation> SetCurrentDal()
        {
            return this.CurrentDBSession.DictEducationDal;
        }
    }
}
