using OA.IBLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.IDAL;
using System.Data;

namespace OA.BLL
{
    public class DictConfigService : BaseService<DictConfig>, IDictConfigService
    {
        public DataTable GetEntityTable()
        {
            DataSet querySet = this.CurrentDBSession.DictConfigDal.GetEntityTable();
            if (querySet != null && querySet.Tables.Count == 1)
                return querySet.Tables[0];
            else
                return null;
        }

        public override IBaseDal<DictConfig> SetCurrentDal()
        {
            return this.CurrentDBSession.DictConfigDal;
        }
    }
}
