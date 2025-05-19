using Domain.Entities.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class ContactType : BaseEntity
    {
        public ContactType()
        {
            Contacts = new HashSet<CustomerContact>();
        }
        public ContactTypeEnum Type { get; set; }
        public virtual ICollection<CustomerContact> Contacts { get; set; }
    }
}
