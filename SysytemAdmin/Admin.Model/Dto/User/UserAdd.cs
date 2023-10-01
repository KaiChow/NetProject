using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model.Dto.User
{
    public class UserAdd
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string PassWord { get; set; }
        public int UserType { get; set; }
        public bool IsEnable { get; set; }
    }
}
