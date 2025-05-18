using Domain.Entities;
using Domain.Entities.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class Post : BaseEntity
    {
        public Post()
        {
            Tags = new HashSet<PostTag>();
            MetaDatas = new HashSet<SEOMetaData>();
        }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Body { get; set; }
        public string? Slug { get; set; }
        public long Views { get; set; }
        public PostStatusEnum Status { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Summary { get; set; }
        public long AuthorId { get; set; }
        public Employee? Author { get; set; }
        public int PostTypeId { get; set; }
        public PostType? PostType { get; set; }
        public virtual ICollection<PostTag> Tags { get; set; }
        public virtual ICollection<SEOMetaData> MetaDatas { get; set; }
    }
}
