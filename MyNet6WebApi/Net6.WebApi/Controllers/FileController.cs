using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Net6.WebApi.Controllers
{
    /// <summary>
    /// 文件操作
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        // POST <FileController>
        [HttpPost(Name = "File")]
        public JsonResult UploadFile(IFormCollection form)
        {
            return new JsonResult(
                    new
                    {
                        Success = true,
                        Message = "上传成功",
                        FileNme = form.Files.FirstOrDefault()?.FileName

                    }
                );
        }

    }
}
