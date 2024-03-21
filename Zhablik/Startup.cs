using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Hosting.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Zhablik.Data;

namespace Zhablik;

public class Startup
{
    public IConfiguration Configuration { get; }
    private readonly IWebHostEnvironment _env;
    
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        _env = env;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        var migrationAssembly = typeof(AppDbContext).Assembly.GetName().Name;
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"],
                opt => opt.MigrationsAssembly(migrationAssembly)));

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
