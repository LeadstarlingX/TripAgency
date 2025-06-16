using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SeoMetaData
{
    public class CreateSeoMetaDataDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? KeyWords { get; set; }
        public string? UrlSlug { get; set; }
        public int PostId { get; set; }
    }
}
