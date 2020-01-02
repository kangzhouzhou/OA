using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OA.Common
{
    public class CacheOperation
    {
        public static object GetValue(string key)
        {
            return HttpContext.Current.Cache[key];
        }

        public static void SetValue(string key, object obj)
        {
            HttpContext.Current.Cache[key] = obj;
        }

        public static object Remove(string key)
        {
           return HttpContext.Current.Cache.Remove(key);
        }

        public static void InitSetting(DataView dictConfigView)
        {
            HttpContext.Current.Cache["Setting"] = dictConfigView;
        }

        public static string GetSettingValue(string key)
        {
            DataView setView = (DataView)HttpContext.Current.Cache["Setting"];
            setView.RowFilter = string.Format("KEY={0}", key ?? "");
            if (setView.Count == 1)
                return setView[0]["VALUE"].ToString();
            else
                return null;
        }

        public static void UpdateSettingValue(string key, string value,string memo)
        {
            DataView setView = (DataView)HttpContext.Current.Cache["Setting"];
            setView.RowFilter = string.Format("KEY='{0}'", key ?? "");
            if (setView.Count == 0)
            {
                DataRowView newViewRow = setView.AddNew();
                newViewRow["KEY"] = key;
                newViewRow["VALUE"] = value;
                newViewRow["MEMO"] = memo;
            }
            else
            {
                setView[0]["VALUE"] = value;
                setView[0]["MEMO"] = memo;
            }
        }
    }
}
