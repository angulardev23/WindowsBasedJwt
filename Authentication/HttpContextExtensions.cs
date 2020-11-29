using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTest.Authentication
{
    public static class HttpContextExtensions
    {
        public static string GetUserName(this HttpContext context)
        {
            return context.User.Claims.Where(x => x.Type.Contains("nameidentifier")).FirstOrDefault().Value;
        }
    }
}
