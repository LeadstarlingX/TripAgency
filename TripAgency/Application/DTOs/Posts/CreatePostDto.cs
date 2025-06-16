using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Posts
{
    public class CreatePostDto
    {
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Body { get; set; }
        public string? Slug { get; set; }
        public long Views { get; set; }
        public PostStatusEnum Status { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Summary { get; set; }
        public long AuthorId { get; set; }
        public int PostTypeId { get; set; }
    }
}
