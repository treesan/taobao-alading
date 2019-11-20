using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alading.Web.Bussiness
{
    public class ServiceException : Exception
    {
        public ServiceException(string message)
            : base(message)
        {
        }
    }
}
