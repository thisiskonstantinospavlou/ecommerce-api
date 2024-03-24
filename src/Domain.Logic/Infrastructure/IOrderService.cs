using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic.Infrastructure
{
    public interface IOrderService
    {
        Task<OrderProcessResult> ProcessOrder(OrderProcessRequest request);
    }
}
