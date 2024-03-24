using Api.Models;
using Data.EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic.ProductStrategies
{
    public class PhysicalMediaStrategy : BaseMediaStrategy
    {
        private readonly IEcommerceDataService _ecommerceDataService;

        public PhysicalMediaStrategy(
            IEcommerceDataService ecommerceDataService)
        {
            _ecommerceDataService = ecommerceDataService ?? throw new ArgumentNullException(nameof(ecommerceDataService));
        }

        public override List<ProductMedia> supportedProductTypes => new List<ProductMedia> { ProductMedia.Physical };

        public override async Task<OrderProcessResult> ProcessOrder(int orderId, int customerId, string productName)
        {
            var result = await _ecommerceDataService.AddOrder(orderId, customerId, productName);
            var postalAddress = await _ecommerceDataService.GetCustomerPostalAddress(customerId);

            return new OrderProcessResult
            {
                Success = result,
                PhysicalAddress = postalAddress
            };

            // TO-DO: Any other process to generate the shipping slip
        }
    }
}
