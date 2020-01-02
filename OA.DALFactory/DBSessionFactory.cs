using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OA.DALFactory
{
    public class DBSessionFactory
    {
        public DBSession CreateDBSession()
        {
           DBSession oaDBSession= (DBSession)CallContext.GetData("oaDBSession");
           if (oaDBSession == null)
           {
               oaDBSession = new DBSession();
               CallContext.SetData("oaDBSession",oaDBSession);
           }
           return oaDBSession;
        }
    }
}
