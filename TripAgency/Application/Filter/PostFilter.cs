using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filter
{
    public class PostFilter
    {
        public string? Title { get; set; }
       
        public string? Body { get; set; }
        public string? Slug { get; set; }
        public long Views { get; set; }
      
        public string? Summary { get; set; }
        
    }
}

