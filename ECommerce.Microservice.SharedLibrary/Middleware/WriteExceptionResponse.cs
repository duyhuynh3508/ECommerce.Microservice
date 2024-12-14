using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Microservice.SharedLibrary.Middleware
{
    public static class WriteExceptionResponse
    {
        public static async Task ChangeHeader(HttpContext context, string title, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
            {
                Title = title,
                Status = statusCode,
                Detail = message
            }), CancellationToken.None);

            return;
        }
    }
}
