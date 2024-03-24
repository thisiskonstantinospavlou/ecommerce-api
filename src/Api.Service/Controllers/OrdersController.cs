using Api.Models;
using Domain.Abstractions.Exceptions;
using Domain.Logic.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Service.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;

        public OrdersController(
            ILogger<OrdersController> logger,
            IOrderService orderService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        /// <summary>
        /// This endpoint initiates the order processing.
        /// </summary>
        /// <param name="request">The order process request.</param>
        /// <returns>The order process result.</returns>
        [HttpPost]
        [Route("[controller]/OrderProcess")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostOrderProcess(OrderProcessRequest request)
        {
            _logger.LogInformation($"Entering {nameof(PostOrderProcess)}");

            try
            {
                var success = _orderService.ProcessOrder(request);
                return Ok(success);
            }
            // TO-DO: Needs refactoring to add more exception types
            catch (InternalServerErrorException ex)
            {
                // TO-DO: Needs refactoring to wrap the error around HTTP status codes
                throw new InternalServerErrorException(ex.Message);
            }
            catch (Exception ex)
            {
                // TO-DO: Needs refactoring to wrap the error around HTTP status codes
                throw new Exception(ex.Message);
            }
        }
    }
}
