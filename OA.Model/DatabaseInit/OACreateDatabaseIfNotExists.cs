using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OA.Model
{
    public class OACreateDatabaseIfNotExists:CreateDatabaseIfNotExists<OAContext>
    {
        protected override void Seed(OAContext context)
        {
            context = InitData.InitBasicData(context);
            base.Seed(context);
        }
    }
}