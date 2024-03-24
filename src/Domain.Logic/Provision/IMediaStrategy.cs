using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic.Provision
{
    public interface IMediaStrategy
    {
        List<ProductMedia> supportedProductTypes { get; }

        Task<OrderProcessResult> ProcessOrder(int orderId, int customerId, string productName);
    }
}
