using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PostalAddress { get; set; }

        public int MembershipId { get; set; }

        public virtual ICollection<Order> OrderFkNavigation { get; set; }
        public virtual Membership MembershipFkNavigation { get; set; }
    }
}
