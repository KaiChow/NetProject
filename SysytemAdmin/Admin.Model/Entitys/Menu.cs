using Admin.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace Admin.Model.Entitys
{
    public class Menu:IEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Name { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Index { get; set; }

        /// <summary>
        /// 页面路径
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string FilePath { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long ParentId { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long Order { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public bool IsEable { get; set; }

    }
}
