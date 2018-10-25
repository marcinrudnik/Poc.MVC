using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Poc.MVC.UserApplicationStatusControl.Services;

namespace Poc.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var applicationStatusesApiConfig = new ApplicationStatusesApiConfig();
            Configuration.GetSection("ApplicationStatusesApi").Bind(applicationStatusesApiConfig);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpClient("applicationStatuses", client =>
                {
                    client.BaseAddress = new Uri(applicationStatusesApiConfig.BaseUrl);
                });

            services.AddTransient<IApplicationStatusesService, ApplicationStatusesService>();

            //Get a reference to the assembly that contains the view components
            var assembly = typeof(UserApplicationStatusControl.UserApplicationStatusesControlViewComponent).GetTypeInfo().Assembly;

            //Create an EmbeddedFileProvider for that assembly
            var embeddedFileProvider = new EmbeddedFileProvider(
                assembly,
                "Poc.MVC.UserApplicationStatusControl"
            );

            //Add the file provider to the Razor view engine
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(embeddedFileProvider);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class ApplicationStatusesApiConfig
    {
        public string BaseUrl { get; set; }
    }
}
