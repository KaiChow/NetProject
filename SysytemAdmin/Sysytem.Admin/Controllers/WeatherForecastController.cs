
using Admin.Service.Jwt;
using Microsoft.AspNetCore.Mvc;
using Admin.Model.Dto.User;
using NetTaste;

namespace Sysytem.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public ICustomJWTService _iCustomJWTService;

        private static readonly string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool" };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICustomJWTService iCustomJWTService)
        {
            _logger = logger;
            _iCustomJWTService = iCustomJWTService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            string Token = _iCustomJWTService.GetToken(new UserRes { Id = 1.ToString(), Name = "Õ²µ¤", NickName = "Kevin", UserType = 0 });
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                Token = Token
            })
            .ToArray();
        }
    }
}