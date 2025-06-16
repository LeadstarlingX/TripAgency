using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Category
{
    public class UpdateCategoryDto:BaseDto<int>
    {
        public string Title { get; set; }
    }
}
