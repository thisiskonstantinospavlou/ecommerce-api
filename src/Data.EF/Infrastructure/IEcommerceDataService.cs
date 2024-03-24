using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Infrastructure
{
    public interface IEcommerceDataService
    {
        Task<bool> ActivateMembership(int customerId, string membershipName);
        Task<string> GetCustomerPostalAddress(int customerId);
        Task<bool> AddOrder(int orderId, int customerId, string productName);
        Task<Api.Models.Membership> GetCustomerMembership(int customerId);
        Task<Api.Models.ProductType> GetTypeForProduct(string productName);
        Task<Api.Models.ProductMedia> GetMediaForProduct(string productName);
    }
}
