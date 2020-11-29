using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthTest.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _contextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var gar = _contextAccessor.HttpContext.GetUserName();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();
        }

        [Route("getAdminConfirmation")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public string GetAdminInfo()
        {
            return _contextAccessor.HttpContext.User.Identity.Name + " Admin in the house";
        }

        [Route("getEditorConfirmation")]
        [Authorize(Roles = "Editor")]
        [HttpGet]
        public string GetEditorInfo()
        {
            return _contextAccessor.HttpContext.User.Identity.Name + " Editor in the house";
        }
    }
}
