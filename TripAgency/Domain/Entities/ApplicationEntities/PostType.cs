using Domain.Entities.Common;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PostType : BaseEntity
    {
        public PostType()
        {
            Posts = new HashSet<Post>();
        }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
