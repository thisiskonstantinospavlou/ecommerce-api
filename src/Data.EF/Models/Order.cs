using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int IncomingOrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        
        public virtual Customer CustomerFkNavigation { get; set; }
        public virtual ICollection<Product> ProductFkNavigation { get; set; }
    }
}
