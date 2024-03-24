using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Models
{
    public class Membership
    {
        public int MembershipId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Customer> CustomerFkNavigation { get; set; } 
    }
}
