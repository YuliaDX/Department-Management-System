using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class User: BaseEntity
    {
        public string FullName { get; set; }

        public string Position { get; set; }

        public virtual Department Department { get; set; }

        public int DepartmentId { get; set; }
    }
}
