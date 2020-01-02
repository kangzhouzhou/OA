using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IDAL
{
    public interface IDictMenuDal : IBaseDal<DictMenu>
    {
        IEnumerable<DictMenu> GetDictMenu(DictUser user);

        IEnumerable<DictMenu> GetSubMenu(DictUser user, int parentId);
    }
}
