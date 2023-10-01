using AutoMapper;
using Admin.Model.Dto.Role;
using Admin.Model.Dto.User;
using Admin.Model.Dto.Menu;
using Admin.Model.Entitys;

namespace Sysytem.Admin.Config
{
    /// <summary>
    /// Dto映射配置
    /// </summary>
    public class AutoMapperConfigs : Profile
    {

        public AutoMapperConfigs()
        {
            // 角色
            CreateMap<Role, RoleRes>();
            CreateMap<RoleAdd, Role>();
            CreateMap<RoleEdit, Role>();
            // 用户
            CreateMap<User, UserRes>();
            CreateMap<UserAdd, User>();
            CreateMap<UserEdit, User>();
            // 菜单
            CreateMap<Menu, MenuRes>();
            CreateMap<MenuAdd, Menu>();
            CreateMap<MenuEdit, Menu>();
        }
    }
}
