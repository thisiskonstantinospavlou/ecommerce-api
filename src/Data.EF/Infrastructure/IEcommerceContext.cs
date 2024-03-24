using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.EF.Infrastructure
{
    public interface IEcommerceContext : IDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Membership> Memberships { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductType> ProductTypes { get; set; }
    }
}
