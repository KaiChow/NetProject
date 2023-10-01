using Admin.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace Admin.Model.Entitys
{
    public class MenuRoleRelation:IBase
    {
        /// <summary>
        /// 菜单主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long MenuId { get; set; }

        /// <summary>
        /// 角色主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long RoleId { get; set; }
    }
}
