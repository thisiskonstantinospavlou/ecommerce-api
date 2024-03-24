using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public bool IsPhysical { get; set; }

        public virtual ICollection<Order> OrderFkNavigation { get; set; }
        public virtual ProductType ProductTypeFkNavigation { get; set; }
    }
}
