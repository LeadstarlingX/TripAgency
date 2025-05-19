using Domain.Enum;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Contact
{
    public class CreateContactTypeDto
    {
        public ContactTypeEnum Type { get; set; }
    }
}
