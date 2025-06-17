using Application.Mapping;
using Application.Mapping.CarProfile;
using Application.Mapping.CategoryProfile;
using Application.Mapping.PaymentProfile;
using Application.Mapping.PaymentTransactionProfile;
using Application.Mapping.PostTypeProfile;
using Application.Mapping.PymentMethodProfile;
using Application.Mapping.SeoMetaData;
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
            services.AddAutoMapper(typeof(PaymentTransactionProfile).Assembly);
            services.AddAutoMapper(typeof(PaymentProfile).Assembly);
            services.AddAutoMapper(typeof(PostProfile).Assembly);
            services.AddAutoMapper(typeof(PostTypeProfile).Assembly);
            services.AddAutoMapper(typeof(TagProfile).Assembly);
            services.AddAutoMapper(typeof(SeoMetaDataProfile).Assembly);
            services.AddAutoMapper(typeof(PostTagProfile).Assembly);



            return services;
        }
    }
}
