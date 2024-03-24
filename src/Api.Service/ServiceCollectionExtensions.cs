using Data.EF;
using Data.EF.Infrastructure;
using Domain.Abstractions;
using Domain.Logic;
using Domain.Logic.Infrastructure;
using Domain.Logic.ProductStrategies;
using Domain.Logic.Provision;
using Microsoft.EntityFrameworkCore;

namespace Api.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AppDb");

        services.AddDbContext<EcommerceContext>(options =>
        {
            options.UseSqlite(connectionString);
            options.EnableSensitiveDataLogging();
        })
            .AddScoped<IEcommerceContext, EcommerceContext>();

        services.AddAutoMapper(expression =>
        {
            expression.AllowNullCollections = true;
        }, AppDomain.CurrentDomain.GetAssemblies());

        services
            .AddScoped<IEcommerceDataService, EcommerceDataService>();

        services
            .AddScoped<IMediaStrategyProvider, MediaStrategyProvider>()
            .AddScoped<IMediaStrategy, ElectronicMediaStrategy>()
            .AddScoped<IMediaStrategy, PhysicalMediaStrategy>()
            .AddScoped<IMediaStrategy, MembershipMediaStrategy>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IValidator, Validator>();

        return services;
    }
}