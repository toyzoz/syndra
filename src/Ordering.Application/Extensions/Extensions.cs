using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                var v1 = typeof(Extensions).Assembly;
                var v2 = Assembly.GetExecutingAssembly();
                cfg.RegisterServicesFromAssembly(typeof(Extensions).Assembly);
            });
            return services;
        }
    }
}
