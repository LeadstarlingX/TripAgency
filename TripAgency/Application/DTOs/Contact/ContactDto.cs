using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Contact
{

    public class ContactDto : ContactTypeDto
    {        
        public required string Value { get; set; }
    }

    public class ContactsDto
    {
        public IList<ContactDto> Contacts { get; set; } = [];
    }
}
