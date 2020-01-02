using OA.IBLL;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    public class DictUserService : BaseService<DictUser>, IDictUserService
    {
        public override IBaseDal<DictUser> SetCurrentDal()
        {
            return this.CurrentDBSession.DictUserDal;
        }

        public bool DeleteEntities(List<int> list)
        {
            var userInfoList = CurrentDal.LoadEntities(u => list.Contains(u.ID));
            foreach (var item in userInfoList)
            {
                CurrentDal.DeleteEntity(item);
            }
            return CurrentDBSession.SaveChange() == list.Count;
        }
    }
}
