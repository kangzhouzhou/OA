using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace OA.DAL
{
    public class DictConfigDal : BaseDal<DictConfig>, IDictConfigDal
    {
        public DataSet GetEntityTable()
        {
            string comStr = "SELECT * FROM DICTCONFIG";
            return SQLHelper.SQLDataSet(comStr);
        }
    }
}
