using Admin.Model.Entitys;
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
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Admin.Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
            // 初始化超级管理员
            User user = new User()
            {
                Name = "admin",
                NickName = "超级管理员",
                PassWord = "123456",
                UserType = 0,
                IsEnable = true,
                Description = "数据库初始化的超级管理员",
                CreateDate = DateTime.Now,
                CreateUserId = 0,
                IsDeleted = 0
            };

            long userId = _db.Insertable(user).ExecuteReturnBigIdentity();
            Menu menuRoot = new Menu()
            {
                Name="菜单管理",
                Index = "menumanager",
                FilePath="../views/admin/menu/MenuManager",
                ParentId = 0,
                Order = 0,
                IsEable = true,
                Description= "数据库初始化的默认菜单",
                CreateDate= DateTime.Now,
                CreateUserId = userId,
                IsDeleted = 0
            };
            _db.Insertable(menuRoot).ExecuteReturnBigIdentity();
            return res;
        }
    }
}
