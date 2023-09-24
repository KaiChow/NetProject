using Admin.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Admin.Model.Entitys
{
    public class Role : IEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string RoleName { get; set; }

        /// <summary>
        /// 角色说明
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string RoleDescription { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public bool IsEnable { get; set; }

    }
}
