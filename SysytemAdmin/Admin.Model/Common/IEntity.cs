﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Admin.Model.Common
{
    public class IEntity : IBase
    {
        /// <summary>
        /// 描述
        /// </summary>

        [SugarColumn(IsNullable = true)]
        public string? Description { get; set; }
        /// <summary>
        /// 创建人id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long CreateUserId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long CreateDate { get; set; }

        /// <summary>
        /// 修改人id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public long ModifyUserId { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public long ModifyDate { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long IsDelete { get; set; }
    }
}
