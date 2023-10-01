using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model.Dto.Menu
{
    public class MenuAdd
    {
        public string Name { get; set; }
        public string Index { get; set; }
        public string FilePath { get; set; }
        public int ParentId { get; set; }
        public int Order { get; set; }
        public bool IsEable { get; set; }
    }
}
