﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model.Dto.Role
{
    public class RoleAdd
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public bool IsEnable { get; set; }
    }
}
