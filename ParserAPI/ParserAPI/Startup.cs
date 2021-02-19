using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParserAPI.Core;
using ParserAPI.Core.Infrastructure;
using ParserAPI.Extractors;

namespace ParserAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBasicInfoExtractor, BasicInfoExtractor>();
            services.AddScoped<ICertificationExtractor, CertificationExtractor>();
            services.AddScoped<IDateExtractor, DateExtractor>();
            services.AddScoped<IDelimiterRepository, DelimiterRepository>();
            services.AddScoped<IEducationExtractor, EducationExtractor>();
            services.AddScoped<IEmploymentExtractor, EmploymentExtractor>();
            services.AddScoped<IExperienceCalculator, ExperienceCalculator>();
            services.AddScoped<IExtractorFacade, ExtractorFacade>();
            services.AddScoped<ISectionExtractor, SectionExtractor>();
            services.AddScoped<ISectionTypeAndIndexRepositoryBuilder, SectionTypeAndIndexRepositoryBuilder>();
            services.AddScoped<ISkillExtractor, SkillExtractor>();
            services.AddScoped<ITypeConverterTool, TypeConverterTool>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseBrowserLink();
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //    }

        //    app.UseStaticFiles();

        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "default",
        //            template: "{controller=Home}/{action=Index}/{id?}");
        //    });
        //}
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });

        }
    }
}
