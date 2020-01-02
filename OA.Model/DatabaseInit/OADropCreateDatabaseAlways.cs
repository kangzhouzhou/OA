using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model
{
    public class OADropCreateDatabaseAlways:DropCreateDatabaseIfModelChanges<OAContext>
    {
        protected override void Seed(OAContext context)
        {
            context = InitData.InitBasicData(context);
            base.Seed(context);
        }
    }
}
