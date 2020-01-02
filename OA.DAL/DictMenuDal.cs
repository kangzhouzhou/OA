using OA.DAL;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.DAL
{
    public class DictMenuDal : BaseDal<DictMenu>,IDictMenuDal
    {
        /// <summary>
        /// 通过用户ID,获取所有的菜单信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<DictMenu> GetDictMenu(DictUser user)
        {
            return OAContextFactory.CreateContext().Set<DictUser>().Where(u => u.ID == user.ID).SelectMany(u => u.DictRole).SelectMany(r => r.DictMenu).Where(m => m.ParentID == 0).Distinct().OrderBy(m => m.ID);
        }

        public IEnumerable<DictMenu> GetSubMenu(DictUser user, int parentId)
        {
            return OAContextFactory.CreateContext().Set<DictUser>().Where(u => u.ID == user.ID).SelectMany(u => u.DictRole).SelectMany(r => r.DictMenu).Where(m => m.ParentID == parentId).Distinct().OrderBy(m => m.ID);
        }
    }
}
