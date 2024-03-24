using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Models
{
    public class ProductType
    {
        public int ProductTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> ProductFkNavigation { get; set; }
    }
}
