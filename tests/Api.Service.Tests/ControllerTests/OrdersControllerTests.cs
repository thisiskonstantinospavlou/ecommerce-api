using Api.Models;
using Api.Service.Controllers;
using Domain.Abstractions;
using Domain.Abstractions.Exceptions;
using Domain.Logic.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.Tests.ControllerTests
{
    public class OrdersControllerTests
    {
        private Mock<IOrderService> _mockOrderService;
        private Mock<ILogger<OrdersController>> _mockLogger;
        private OrdersController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockOrderService = new Mock<IOrderService>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<OrdersController>>(MockBehavior.Loose);

            _controller = new OrdersController(
                _mockLogger.Object,
                _mockOrderService.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            _mockOrderService.VerifyAll();
            _mockLogger.VerifyAll();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_IfLoggerIsNull()
        {
            // Arrange

            // Act
            Action action = () => new OrdersController(
                null,
                _mockOrderService.Object
            );

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_IfOrderServiceIsNull()
        {
            // Arrange

            // Act
            Action action = () => new OrdersController(
                _mockLogger.Object,
                null
            );

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task PostOrderProcess_ReturnsOk()
        {
            // Arrange
            var orderProcessRequest = new OrderProcessRequest
            {
                CustomerId = 1,
                PurchaseOrderId = 1,
                Total = 32M,
                Items = new List<Product>
                {
                    new Product
                    {
                        Name = "Premium",
                        Type = ProductType.Membership
                    }
                }
            };
            var orderProcessResult = new OrderProcessResult
            {
                Success = true
            };

            _mockOrderService.Setup(x => x.ProcessOrder(orderProcessRequest)).ReturnsAsync(orderProcessResult);

            // Act
            var result = await _controller.PostOrderProcess(orderProcessRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task PostBookingAsync_ThrowsException()
        {
            // Arrange
            var orderProcessRequest = new OrderProcessRequest
            {
                CustomerId = 1,
                PurchaseOrderId = 1,
                Total = 32M,
                Items = new List<Product>
                {
                    new Product
                    {
                        Name = "Premium",
                        Type = ProductType.Membership
                    }
                }
            };

            _mockOrderService.Setup(x => x.ProcessOrder(orderProcessRequest)).Throws(new InternalServerErrorException());
            // Act
            Func<Task> action = async () => await _controller.PostOrderProcess(orderProcessRequest);

            // Assert
            await action.Should().ThrowAsync<InternalServerErrorException>();
        }
    }
}
