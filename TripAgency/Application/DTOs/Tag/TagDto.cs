using Application.Common;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Tag
{
    public class TagDto:BaseDto<int>
    {
        public string? Name { get; set; }
    }
}
