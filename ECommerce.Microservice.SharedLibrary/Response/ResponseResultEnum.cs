using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Microservice.SharedLibrary.Response
{
    public enum ResponseResultEnum
    {
        Success = 1,
        Error = 2,
        CompleteWithError = 3,
    }
}
