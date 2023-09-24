using Autofac;
using Autofac.Extensions.DependencyInjection;
using SqlSugar;

namespace Sysytem.Admin.Config
{
    public static class HostBuilderExtend
    {
        public static void Register(this WebApplicationBuilder app)
        {
            app.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            app.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                #region 注册 sqlsugar
                builder.Register<ISqlSugarClient>(context =>
                {
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                    {

                        ConnectionString = "Data Source=localhost;Initial Catalog=SysAdmin;User ID=sa;Password=sa123456;",
                        DbType = DbType.SqlServer,
                        IsAutoCloseConnection = true
                    });
                    // 支持sql的输出，方便排查
                    db.Aop.OnLogExecuted = (sql, par) =>
                    {
                        Console.WriteLine("\r\n");
                        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},sql语句：${sql}");
                        Console.WriteLine("=======================================");
                    };
                    return db;
                });

                #endregion

            });
        }
    }
}
