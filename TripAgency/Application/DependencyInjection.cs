using Application.Mapping.CarProfile;
using Application.Mapping.CategoryProfile;
using Application.Mapping.PymentMethodProfile;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(CarProfile).Assembly);
            services.AddAutoMapper(typeof(CategoryProfile).Assembly);
            services.AddAutoMapper(typeof(PaymentMethodProfile).Assembly);


            return services;
        }
    }
}
