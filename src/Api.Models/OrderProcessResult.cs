using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class OrderProcessResult
    {
        public bool Success { get; set; }

        public string PhysicalAddress { get; set; }
    }
}
