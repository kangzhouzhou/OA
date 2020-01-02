using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.IDAL;

namespace OA.BLL
{
    public class StaticPageImgReocrdService : BaseService<StaticPageImgReocrd>, IBLL.IStaticPageImgReocrdService
    {
        public override IBaseDal<StaticPageImgReocrd> SetCurrentDal()
        {
            return this.CurrentDBSession.StaticPageImgReocrdDal;
        }
    }
}
