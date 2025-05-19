using Application.IReositosy;
using Application.IUnitOfWork;
using Domain.Context;
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
            .AddInfrastructure(Configuration)
            .AddPresentation(Configuration);
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeeder dataSeeder)
    {
        if (env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        dataSeeder.SeedData();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My_API V1");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}


