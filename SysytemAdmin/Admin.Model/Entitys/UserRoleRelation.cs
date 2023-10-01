using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model.Entitys
{
    public class UserRoleRelation
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 角色主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long RoleId { get; set; }

    }
}
