using Application.IReositosy;
using Application.IUnitOfWork;
using DataAccessLayer.Context;
using Domain.Entities.IdentityEntities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddControllers();
            services.AddMvc();
            services.AddEndpointsApiExplorer();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;
        }
    }
}
