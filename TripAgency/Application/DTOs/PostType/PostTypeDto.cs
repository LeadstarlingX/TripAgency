using Application.Common;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PostType
{
    public class PostTypeDto:BaseDto<int>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
