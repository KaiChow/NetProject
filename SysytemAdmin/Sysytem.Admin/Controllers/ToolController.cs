using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Reflection;

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
            // 创建表
            string nspace = "Admin.Model.Entitys";
            // 通过反射读取我们想要的类
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory+"Admin.Model.dll").GetTypes().Where(p=>p.Namespace == nspace).ToArray();
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
            return res;
        }
    }
}
