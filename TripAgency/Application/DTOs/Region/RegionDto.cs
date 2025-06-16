using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Region
{
    public class RegionDto : BaseDto<int>
    {
        public string? Name { get; set; }
    }
}
