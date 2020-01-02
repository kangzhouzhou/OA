using OA.IBLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    public class DictUserInfoService:BaseService<DictUserInfo>,IDictUserInfoService
    {
        public override IDAL.IBaseDal<DictUserInfo> SetCurrentDal()
        {
            return this.CurrentDBSession.DictUserInfoDal;
        }
    }
}
