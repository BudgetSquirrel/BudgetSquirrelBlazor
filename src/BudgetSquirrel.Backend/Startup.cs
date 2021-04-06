using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Backend.Auth;
using BudgetSquirrel.Backend.Configuration;
using BudgetSquirrel.Backend.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BudgetSquirrel.Backend
{
  public class Startup
  {
    readonly string AllowFrontendOrigin = "_allowFrontendOrigin";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddHttpContextAccessor();

      DalLocalDbServicesRegistry.ConfigureAuthDal(services);
      GateKeeperServicesRegistry.AddGateKeeper(services, this.Configuration);

      ConfigureHostingLayer(services);
    }

    private void ConfigureHostingLayer(IServiceCollection services)
    {
      HostingConfiguration hostingConfig = this.Configuration.GetSection("Hosting").Get<HostingConfiguration>();
      
      AuthInfrastructureServicesRegistry.AddAuthInfrastructure(services, hostingConfig);

      services.AddCors(options =>
          options.AddPolicy(name: AllowFrontendOrigin,
              builder =>
              {
                builder.WithOrigins(hostingConfig.FrontendOrigin).AllowAnyHeader().AllowAnyMethod();
              }));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors(AllowFrontendOrigin);

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller}/{action=Index}/{id?}");
      });
    }
  }
}
