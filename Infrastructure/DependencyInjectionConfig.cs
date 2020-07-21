using ApplicationCore.Domain.Interfaces.Cache;
using ApplicationCore.Domain.Interfaces.Db;
using ApplicationCore.Domain.Interfaces.Serializers;
using ApplicationCore.Interfaces.Db;
using ApplicationCore.Interfaces.File;
using Infrastructure.CachingServices;
using Infrastructure.Data;
using Infrastructure.Data.EntityFramework;
using Infrastructure.FileServices;
using Infrastructure.Serializers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDb"));
            }
            else
            {
                // TODO: validate .env current environment
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(
                        configuration.GetConnectionString("DevelopmentConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<ITodoListRepository, TodoListRepository>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            // https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            // Adding redis cache
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration =
                    configuration.GetConnectionString("ConexaoRedis");
                options.InstanceName = "APICotacoes-";
            });

            services.AddScoped<ICacheHandler, RedisService>();

            services.AddTransient(typeof(IJsonSerializer<>), typeof(NewtonsoftSerializer<>));

            return services;
        }
    }
}