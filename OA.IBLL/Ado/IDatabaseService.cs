﻿using OA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IBLL
{
    public interface IDatabaseService
    {
        List<FunArgument> GetFunArgumentBll(string funName);
    }
}
