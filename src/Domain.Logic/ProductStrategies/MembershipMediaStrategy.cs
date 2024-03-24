using Api.Models;
using Data.EF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic.ProductStrategies
{
    public class MembershipMediaStrategy : BaseMediaStrategy
    {
        private readonly IEcommerceDataService _ecommerceDataService;

        public MembershipMediaStrategy(
            IEcommerceDataService ecommerceDataService)
        {
            _ecommerceDataService = ecommerceDataService ?? throw new ArgumentNullException(nameof(ecommerceDataService));
        }

        public override List<ProductMedia> supportedProductTypes => new List<ProductMedia> { ProductMedia.Membership };

        public override async Task<OrderProcessResult> ProcessOrder(int orderId, int customerId, string productName)
        {
            var addOrderResult = await _ecommerceDataService.AddOrder(orderId, customerId, productName);
            var activateMembershipResult = await _ecommerceDataService.ActivateMembership(customerId, productName);

            return new OrderProcessResult
            {
                Success = addOrderResult && activateMembershipResult
            };
        }
    }
}
