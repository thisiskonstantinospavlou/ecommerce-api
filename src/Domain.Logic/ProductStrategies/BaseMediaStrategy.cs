using Api.Models;
using Data.EF.Infrastructure;
using Domain.Logic.Provision;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic.ProductStrategies
{
    public abstract class BaseMediaStrategy : IMediaStrategy
    {
        public abstract List<ProductMedia> supportedProductTypes { get; }

        public abstract Task<OrderProcessResult> ProcessOrder(int orderId, int customerId, string productName);

        // TO-DO: Any methods that can be called by the strategies can go here
    }
}
