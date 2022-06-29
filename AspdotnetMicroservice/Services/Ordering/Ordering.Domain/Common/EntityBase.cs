using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }

        public DateTime CreatedDate { get;  set; }

        public DateTime UpdatedDate { get;  set; }

        public string CreatedBy { get;  set; }

        public string? UpdatedBy { get; set; }
        


    }
}
