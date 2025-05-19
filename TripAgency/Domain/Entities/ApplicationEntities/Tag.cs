using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class Tag : BaseEntity
    {
        public Tag()
        {
            PostTags = new HashSet<PostTag>();
        }
        public string? Name { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}