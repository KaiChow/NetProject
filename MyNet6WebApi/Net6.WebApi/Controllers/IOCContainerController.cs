using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNet6.Interfaces;

namespace Net6.WebApi.Controllers
{
    /// <summary>
    /// IOC容器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IOCContainerController : ControllerBase
    {
        private readonly ILogger<IOCContainerController> _logger;
        private readonly ITestServiceA _ITestServiceA;
        private readonly IServiceProvider _IServiceProvider;

        /// <summary>
        /// 服务注入
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="iTestServiceA"></param>
        /// <param name="iServiceProvider"></param>
        public IOCContainerController(ILogger<IOCContainerController> logger, ITestServiceA iTestServiceA, IServiceProvider iServiceProvider)
        {
            _logger = logger;
            _IServiceProvider = iServiceProvider;
            _ITestServiceA = iTestServiceA;
        }

        [HttpGet()]
        public string ShowA([FromServices] ITestServiceA iTestServiceA, [FromServices] IServiceProvider iServiceProvider)
        {
            return _ITestServiceA.ShowA();
        }

    }
}
