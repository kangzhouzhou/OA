using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    /// <summary>
    /// 负责创建EF数据操作上下文实例,必须保证线程内唯一
    /// </summary>
    public class OAContextFactory
    {
        public static DbContext CreateContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("oaDB"); 
            if (dbContext == null)
            {
                dbContext = new OA.Model.OAContext();
                CallContext.SetData("oaDB", dbContext);
            }
            return dbContext;
        }
    }
}
