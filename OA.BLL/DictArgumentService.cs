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
    public class DictArgumentService : BaseService<DictArgument>, IDictArgumentService
    {
        public override IBaseDal<DictArgument> SetCurrentDal()
        {
            return this.CurrentDBSession.DictArgumentDal;
        }
    }
}
