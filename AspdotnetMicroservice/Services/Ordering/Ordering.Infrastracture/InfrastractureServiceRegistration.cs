using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Infrastracture.Mail;
using Ordering.Infrastracture.Presistence;
using Ordering.Infrastracture.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastracture
{
    public static class InfrastractureServiceRegistration
    {
        public static IServiceCollection AddInfrastractureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(o => o.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));
          //  services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            //   services.AddTransient<IEmailService, EmailService>();
            return services;
        }


    }
}
