using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OA.Common
{
    public class SessionOperater
    {
        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public static object GetValue(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public static void SetValue(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}
