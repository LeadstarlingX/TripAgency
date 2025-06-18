using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public partial class Customer
    {
        public Customer()
        {
            Contacts = new HashSet<CustomerContact>();
            Bookings = new HashSet<Booking>();
            Credits = new HashSet<Credit>();
        }
        public long UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public virtual ICollection<CustomerContact> Contacts { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
    }
}
