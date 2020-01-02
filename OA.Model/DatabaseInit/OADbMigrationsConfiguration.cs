using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    /// <summary>
    /// 用于迁移数据库配置
    /// </summary>
    public class OADbMigrationsConfiguration : DbMigrationsConfiguration<OAContext>
    {
        public OADbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "OA.Model.OAContext";
        }
    }
}
