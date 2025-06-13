using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ApplicationEntities
{
    public class Employee
    {
        public Employee()
        {
            Posts = new HashSet<Post>();
            Bookings = new HashSet<Booking>();
        }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime HireDate { get; set; }
        public long UserId { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}