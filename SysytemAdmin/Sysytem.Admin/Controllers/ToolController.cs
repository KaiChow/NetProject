using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Sysytem.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        public ISqlSugarClient _db;
        public ToolController(ISqlSugarClient db)
        {
            _db = db;
        }

        [HttpGet]
        public string InitDataBase()
        {
            string res = "OK";
            // 创建数据库
            _db.DbMaintenance.CreateDatabase();
            return res;
        }
    }
}
