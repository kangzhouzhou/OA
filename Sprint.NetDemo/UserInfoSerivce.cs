using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint.NetDemo
{
    class UserInfoSerivce:IUserInfoService
    {
        public int Age { get; set; }

        public string UserName { get; set; }

        public string ShowMsg()
        {
            return "hello world " + UserName;
        }
    }
}
