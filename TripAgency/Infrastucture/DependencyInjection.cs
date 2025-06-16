using Application.IReositosy;
using Application.IUnitOfWork;
using Domain.Context;
using Domain.Entities.IdentityEntities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Infrastructure.Seeds;
using Application.IApplicationServices.Authentication;
using Infrastructure.ApplicationServices.Authentication;
using Application.IApplicationServices.Contact;
using Infrastructure.Services.ServicesImplementation;
using Application.IApplicationServices.Customer;
using Infrastructure.ApplicationServices.Customer;
using Application.Serializer;
using Application.IApplicationServices.Car;
using Application.IApplicationServices.Category;
using Infrastructure.ApplicationServices;
using Infrastructure.ApplicationServices;
using Application.IApplicationServices.Booking;
using Application.IApplicationServices;
using Application.IApplicationServices.PaymentTransaction;
using Application.IApplicationServices.Payment;
using Application.IApplicationServices.Booking;
using Application.IApplicationServices.CarBooking;
using Infrastructure.ApplicationServices.CarBooking;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration) =>
       services
           .AddServices()
           .AddDatabase(configuration)
           .AddIdentityOptions();
    

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
            services.AddScoped(typeof(IIdentityAppRepository<>), typeof(IdentityRepository<>));
            services.AddScoped<IJsonFieldsSerializer, JsonFieldsSerializer>();
            services.AddScoped<DataSeeder>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IContactTypeService, ContactTypeService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentTransactionService, PaymentTransactionService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ICarBookingService, CarBookingService>();



            return services;
        }

        private static IServiceCollection AddIdentityOptions(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<IdentityAppDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<IdentityAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            return services;
        }
    } 
}
