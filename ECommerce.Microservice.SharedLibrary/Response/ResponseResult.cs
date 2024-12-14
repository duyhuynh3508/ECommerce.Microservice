using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Microservice.SharedLibrary.Response
{
    public record ResponseResult(
        ResponseResultEnum responseResult = 0,
        string message = "", object? data = null,
        IEnumerable<object>? collection = null
        );
}
