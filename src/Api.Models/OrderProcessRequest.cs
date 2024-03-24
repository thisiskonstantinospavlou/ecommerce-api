using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class OrderProcessRequest
    {
        public int PurchaseOrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public List<Product> Items { get; set; }
    }
}
