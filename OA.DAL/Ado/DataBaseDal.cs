using OA.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.DAL
{
    public class DatabaseDal:IDatabaseDal
    {
        public DataSet GetFunArgumentDal(string funName)
        {
            string comStr = @"SELECT  col.name AS ArgumentName,(SELECT name FROM systypes xt WHERE xt.type = col.xtype) AS ArgumentType FROM syscolumns col WHERE   ID = (SELECT ID FROM dbo.sysobjects obj WHERE OBJECTPROPERTY(id,N'IsProcedure') = 1 AND obj.name =@FunName)";
            return OA.Model.SQLHelper.SQLDataSet(comStr, new SqlParameter("FunName", funName));
        }
    }
}
