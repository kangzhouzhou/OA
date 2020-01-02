using OA.IBLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    public class DictMenuService : BaseService<DictMenu>, IDictMenuService
    {
        public override IDAL.IBaseDal<DictMenu> SetCurrentDal()
        {
            return this.CurrentDBSession.DictMenuDal;
        }

        public IEnumerable<DictMenu> GetDictMenu(DictUser user)
        {
            return this.CurrentDBSession.DictMenuDal.GetDictMenu(user);
        }

        public IEnumerable<DictMenu> GetSubMenu(DictUser user, int parentId)
        {
            return this.CurrentDBSession.DictMenuDal.GetSubMenu(user, parentId);
        }
    }
}
