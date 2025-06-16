using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class BaseDto<T>
    {
        public T Id { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public DateTime UpdatedAt { get; set; } = DateTime.Now;
        //public bool IsActive { get; set; } = true;
        //public bool IsDeleted { get; set; } = false;
    }
}
