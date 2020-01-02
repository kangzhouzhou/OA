using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Models
{
    public class ProcessException
    {
        static ProcessException()
        {
            QueueException = new Queue<Exception>();
        }
        public static Queue<Exception> QueueException { get; set; }

    }
}