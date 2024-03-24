using Api.Models;
using Data.EF.Infrastructure;
using Domain.Abstractions;
using Domain.Abstractions.Exceptions;
using Domain.Logic.Infrastructure;
using Domain.Logic.Provision;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IValidator _validator;
        private readonly IEcommerceDataService _ecommerceDataService;
        private readonly IMediaStrategyProvider _MediaStrategyProvider;

        public OrderService(
            ILogger<OrderService> logger,
            IValidator validator,
            IEcommerceDataService ecommerceDataService,
            IMediaStrategyProvider MediaStrategyProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _ecommerceDataService = ecommerceDataService ?? throw new ArgumentNullException(nameof(ecommerceDataService));
            _MediaStrategyProvider = MediaStrategyProvider ?? throw new ArgumentNullException(nameof(MediaStrategyProvider));
        }

        public async Task<OrderProcessResult> ProcessOrder(OrderProcessRequest request)
        {
            _logger.LogInformation($"Entering Process Order...");

            var customerMembership = await _ecommerceDataService.GetCustomerMembership(request.CustomerId);
            _validator.ValidateMembership(customerMembership, request.Items);

            OrderProcessResult? orderProcessResult = null;
            request.Items.ForEach(async item =>
            {
                var media = await _ecommerceDataService.GetMediaForProduct(item.Name);
                orderProcessResult = await _MediaStrategyProvider.GetStrategy(media).ProcessOrder(request.PurchaseOrderId, request.CustomerId, item.Name);
            });

            if (orderProcessResult != null)
            {
                return orderProcessResult;
            }

            throw new InternalServerErrorException();
        }
    }
}
