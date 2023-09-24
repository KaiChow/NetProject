using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Net6.WebApi.Controllers
{
    /// <summary>
    /// 参数的修饰
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        // GET: api/<ParameterController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
