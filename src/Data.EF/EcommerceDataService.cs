using Api.Models;
using AutoMapper;
using Data.EF.Infrastructure;
using Data.EF.Models;
using Domain.Abstractions.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF
{
    public class EcommerceDataService : IEcommerceDataService
    {
        private readonly IEcommerceContext _ecommerceContext;
        private readonly IMapper _mapper;

        public EcommerceDataService(
            IMapper mapper,
            IEcommerceContext ecommerceContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _ecommerceContext = ecommerceContext ?? throw new ArgumentNullException(nameof(ecommerceContext));
        }

        public async Task<bool> ActivateMembership(int customerId, string membershipName)
        {
            // Assumes Customer will be assigned a member by default
            // This includes the "None" membership option which should be added upon creating a new Customer

            var memberships = await _ecommerceContext.Memberships.Where(m => m.Name.ToLower().Contains(membershipName.ToLower())).ToListAsync();
            if (memberships.Count == 0 || memberships.Count > 1)
            {
                // throw not found exception if no items found or multiple items found
                throw new ItemNotFoundException();
            }

            int? membershipId = null;
            if (memberships.Count == 1)
            {
                membershipId = memberships.First().MembershipId;
            }

            if (membershipId != null)
            {
                var dbCustomer = await _ecommerceContext.Customers
                    .Where(cm => cm.CustomerId == customerId).FirstOrDefaultAsync();

                if (dbCustomer != null)
                {
                    if (dbCustomer.MembershipId == (int)membershipId)
                    {
                        return true;
                    }

                    dbCustomer.MembershipId = (int)membershipId;
                }

                int? result = null;
                result = await _ecommerceContext.SaveChangesAsync();

                if (result.HasValue && result.Value == 1)
                {
                    return true;
                }
            }
            
            throw new InternalServerErrorException();
        }

        public async Task<string> GetCustomerPostalAddress(int customerId)
        {
            var customers = await _ecommerceContext.Customers.Where(x => x.CustomerId == customerId).ToListAsync();

            if (customers.Count == 0 || customers.Count > 1)
            {
                // throw not found exception if no items found or multiple items found
                throw new ItemNotFoundException();
            }

            if (customers.Count == 1)
            {
                return customers.First().PostalAddress;
            }

            throw new InternalServerErrorException();
        }

        public async Task<bool> AddOrder(int orderId, int customerId, string productName)
        {
            var productId = await GetProductId(productName);

            var newOrder = new Order
            {
                IncomingOrderId = orderId,
                CustomerId = customerId,
                ProductId = productId
            };

            var newOrderResult = _ecommerceContext.Orders.AddAsync(newOrder);

            int? result = null;
            result = await _ecommerceContext.SaveChangesAsync();

            if (result.HasValue && result.Value == 1)
            {
                return true;
            }

            throw new InternalServerErrorException();
        }

        private async Task<int> GetProductId(string productName)
        {
            var products = await _ecommerceContext.Products
                .Where(x => x.Name.ToLower().Contains(productName.ToLower()))
                .ToListAsync();
            
            if (products.Count == 0 || products.Count > 1)
            {
                // throw not found exception if no items found or multiple items found
                throw new ItemNotFoundException();
            }

            if (products.Count == 1)
            {
                return products.First().ProductId;
            }

            throw new InternalServerErrorException();
        }

        public async Task<Api.Models.Membership> GetCustomerMembership(int customerId)
        {
            var membershipQuery =
                from customer in _ecommerceContext.Customers.Where(x => x.CustomerId == customerId)
                join membership in _ecommerceContext.Memberships on customer.MembershipId equals membership.MembershipId
                select membership;

            var membershipResult = await membershipQuery.ToListAsync();

            if (membershipResult.Count == 0 || membershipResult.Count > 1)
            {
                // throw not found exception if no items found or multiple items found
                throw new ItemNotFoundException();
            }

            if (membershipResult.Count == 1)
            {
                return _mapper.Map<Api.Models.Membership>(membershipResult.First());
            }

            return Api.Models.Membership.None;
        }

        public async Task<Api.Models.ProductType> GetTypeForProduct(string productName)
        {
            // not a safe search
            // TO-DO: search with IDs
            var productQuery =
                from product in _ecommerceContext.Products.Where(x => x.Name.ToLower().Contains(productName.ToLower()))
                join productType in _ecommerceContext.ProductTypes on product.TypeId equals productType.ProductTypeId
                select product;

            var productResult = await productQuery.ToListAsync();

            if (productResult.Count == 0 || productResult.Count > 1)
            {
                // throw not found exception if no items found or multiple items found
                throw new ItemNotFoundException();
            }

            if (productResult.Count == 1)
            {
                return _mapper.Map<Api.Models.ProductType>(productResult.First());
            }

            throw new InternalServerErrorException();
        }

        public async Task<Api.Models.ProductMedia> GetMediaForProduct(string productName)
        {
            // not a safe search
            // TO-DO: search with IDs
            var productQuery =
                from product in _ecommerceContext.Products.Where(x => x.Name.ToLower().Contains(productName.ToLower()))
                join productType in _ecommerceContext.ProductTypes on product.TypeId equals productType.ProductTypeId
                select product;

            var productResult = await productQuery.ToListAsync();

            if (productResult.Count == 0 || productResult.Count > 1)
            {
                // throw not found exception if no items found or multiple items found
                throw new ItemNotFoundException();
            }

            if (productResult.Count == 1)
            {
                return _mapper.Map<Api.Models.ProductMedia>(productResult.First());
            }

            throw new InternalServerErrorException();
        }

        //public async Task InserOrder(string )
    }
}
