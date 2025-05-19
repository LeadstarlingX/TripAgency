using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            Cars = new HashSet<Car>();
        }
        public string Title { get; set; } = string.Empty;
        public virtual ICollection<Car> Cars { get; set; }
    }
}