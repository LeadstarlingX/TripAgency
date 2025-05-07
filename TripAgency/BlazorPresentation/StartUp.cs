using Application.IReositosy;
using Application.IUnitOfWork;
using DataAccessLayer.Context;
using Domain.Entities.IdentityEntities;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.Seeds;
using Presentation;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services           
            .AddPresentation()
            .AddInfrastructure(Configuration);       
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeeder dataSeeder)
    {
        dataSeeder.SeedDataAsync();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // Authentication/Authorization Middleware
        app.UseAuthentication();
        app.UseAuthorization();

        // 🔽 Add this before app.UseEndpoints or app.UseRouter (if using older ASP.NET Core version)
        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect("/swagger");
                return;
            }
            await next();
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}


