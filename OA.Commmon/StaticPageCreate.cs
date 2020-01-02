using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OA.Common
{
    public class StaticPageSetting
    {
        public static void StaticPageCreate(string fileName,string staticFileName,Dictionary<string,string> replaceDic)
        {
            string fileContent = File.ReadAllText(HttpContext.Current.Server.MapPath(fileName));
            StringBuilder contentSb = new StringBuilder();
            contentSb.Append(fileContent);
            foreach (var item in replaceDic.Keys)
            {
                contentSb = contentSb.Replace(item, replaceDic[item]);
            }
            File.WriteAllText(HttpContext.Current.Server.MapPath(staticFileName), contentSb.ToString());
        }
    }
}
