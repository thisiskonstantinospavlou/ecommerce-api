using Api.Models;
using Data.EF.Infrastructure;
using Domain.Abstractions;
using Domain.Logic.Infrastructure;
using Domain.Logic.ProductStrategies;
using Domain.Logic.Provision;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace Domain.Logic.Tests
{
    public class OrderServiceTests
    {
        private Mock<ILogger<OrderService>> _mockLogger;
        private Mock<IValidator> _mockValidator;
        private Mock<IEcommerceDataService> _mockEcommerceDataService;
        private Mock<IMediaStrategyProvider> _mockMediaStrategyProvider;
        private Mock<IMediaStrategy> _mockMembershipStrategy;
        private Mock<IMediaStrategy> _mockElectronicStrategy;
        private Mock<IMediaStrategy> _mockPhysicalStrategy;

        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<OrderService>>(MockBehavior.Loose);
            _mockValidator = new Mock<IValidator>(MockBehavior.Loose);
            _mockEcommerceDataService = new Mock<IEcommerceDataService>(MockBehavior.Strict);
            _mockMediaStrategyProvider = new Mock<IMediaStrategyProvider>(MockBehavior.Loose);
            _mockMembershipStrategy = new Mock<IMediaStrategy>(MockBehavior.Strict);
            _mockElectronicStrategy = new Mock<IMediaStrategy>(MockBehavior.Strict);
            _mockPhysicalStrategy = new Mock<IMediaStrategy>(MockBehavior.Strict);

            _orderService = new OrderService(
                _mockLogger.Object,
                _mockValidator.Object,
                _mockEcommerceDataService.Object,
                _mockMediaStrategyProvider.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockLogger.VerifyAll();
            _mockValidator.VerifyAll();
            _mockEcommerceDataService.VerifyAll();
            _mockMediaStrategyProvider.VerifyAll();
            _mockMembershipStrategy.VerifyAll();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_IfLoggerIsNull()
        {
            // Arrange

            // Act
            Action action = () => new OrderService(
                null,
                _mockValidator.Object,
                _mockEcommerceDataService.Object,
                _mockMediaStrategyProvider.Object);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_IfValidatorIsNull()
        {
            // Arrange

            // Act
            Action action = () => new OrderService(
                _mockLogger.Object,
                null,
                _mockEcommerceDataService.Object,
                _mockMediaStrategyProvider.Object);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_IfEcommerceDataServiceIsNull()
        {
            // Arrange

            // Act
            Action action = () => new OrderService(
                _mockLogger.Object,
                _mockValidator.Object,
                null,
                _mockMediaStrategyProvider.Object);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_IfMediaStrategyProviderIsNull()
        {
            // Arrange

            // Act
            Action action = () => new OrderService(
                _mockLogger.Object,
                _mockValidator.Object,
                _mockEcommerceDataService.Object,
                null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task ProcessOrer_ReturnsOrderProcessResult()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Premium",
                    Type = ProductType.Membership
                }
            };
            var orderProcessRequest = new OrderProcessRequest
            {
                CustomerId = 1,
                PurchaseOrderId = 1,
                Total = 32M,
                Items = products
            };
            var orderProcessResult = new OrderProcessResult
            {
                PhysicalAddress = null,
                Success = true
            };

            _mockEcommerceDataService.Setup(x => x.GetCustomerMembership(orderProcessRequest.CustomerId)).ReturnsAsync(Membership.None);
            _mockValidator.Setup(x => x.ValidateMembership(Membership.None, products));
            _mockEcommerceDataService.Setup(x => x.GetMediaForProduct(products.First().Name)).ReturnsAsync(ProductMedia.Membership);
            _mockMediaStrategyProvider.Setup(s => s.GetStrategy(ProductMedia.Membership))
                .Returns(_mockMembershipStrategy.Object);
            _mockMembershipStrategy.Setup(x => x.ProcessOrder(orderProcessRequest.PurchaseOrderId, orderProcessRequest.CustomerId, products.First().Name))
                .ReturnsAsync(orderProcessResult);

            // Act
            var result = await _orderService.ProcessOrder(orderProcessRequest);

            // Assert
            result.Should().BeEquivalentTo(orderProcessResult);
        }
    }
}