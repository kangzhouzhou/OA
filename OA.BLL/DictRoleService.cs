using OA.IBLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    public class DictRoleService : BaseService<DictRole>,IDictRoleService
    {
        public override IDAL.IBaseDal<DictRole> SetCurrentDal()
        {
            return this.CurrentDBSession.DictRoleDal;
        }
    }
}
