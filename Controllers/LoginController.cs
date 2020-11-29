using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthTest.Model;
using AuthTest.Authentication;

namespace AuthTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = NegotiateDefaults.AuthenticationScheme)]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginController(
            IConfiguration config,
            IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _contextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult Login()
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser();

            if (user != null)
            {
                var tokenString = new TokenGenerator(_config).GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private UserModel AuthenticateUser()
        {
            UserModel user = null;
  
            if (_contextAccessor.HttpContext.User.Identity.Name.Contains("gar"))
            {
                user = new UserModel { Username = _contextAccessor.HttpContext.User.Identity.Name, EmailAddress = "mail@gar.com" };
            }

            return user;
        }
    }
}