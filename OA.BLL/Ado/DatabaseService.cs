using OA.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OA.Model;

namespace OA.BLL
{
    public class DatabaseService : IDatabaseService
    {
        public List<FunArgument> GetFunArgumentBll(string funName)
        {
            IDAL.IDatabaseDal dataBaseDal = OA.DALFactory.AbstractFactory.CreateDatabaseDal();
            DataSet dSet = dataBaseDal.GetFunArgumentDal(funName);
            List<FunArgument> listArg = null;
            if (dSet != null && dSet.Tables.Count == 1)
            {
                listArg = new List<FunArgument>();
                foreach (DataRow item in dSet.Tables[0].Rows)
                {
                    listArg.Add(new FunArgument
                    {
                        ArgumentName = item["ArgumentName"].ToString(),
                        ArgumentType = item["ArgumentType"].ToString()
                    });
                }

            }
            return listArg;
        }
    }
}
