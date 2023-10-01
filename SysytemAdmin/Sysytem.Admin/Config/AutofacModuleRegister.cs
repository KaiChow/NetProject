using Autofac;
using System.Reflection;

namespace Sysytem.Admin.Config
{
    public class AutofacModuleRegister:Autofac.Module
    {
        /// <summary>
        /// 注册接口和实现的关系
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            Assembly interfaceAssembly = Assembly.Load("Admin.Interface");
            Assembly serviceAssembly = Assembly.Load("Admin.Service");
            builder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();
        }

    }
}
