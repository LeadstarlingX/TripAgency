using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class SEOMetaData : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? KeyWords { get; set; }
        public string? UrlSlug { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
