using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sample.Core.Interfaces;
using Sample.Infrastructure.Context;
using Sample.Infrastructure.Mapping;
using Sample.Infrastructure.UnitOfWork;
using Sample.SharedKernal;
using Sample.SharedKernal.Localization;
using Sample.Web.Contollers;

namespace Sample.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string AllowedOrigins { get; } = "AllowedOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            #region Add Controllers + As Service To Implment Property Injection
            services.AddControllersWithViews().AddControllersAsServices().AddNewtonsoftJson();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(ProductMapping).Assembly);
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Vision Sample API", Version = "v1" });

                c.DescribeAllParametersInCamelCase();
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
            });
            #endregion

            #region CORS
            IConfigurationSection originsSection = Configuration.GetSection(AllowedOrigins);
            string[] origns = originsSection.AsEnumerable().Where(s => s.Value != null).Select(a => a.Value).ToArray();

            services.AddCors(options =>
            {
                options.AddPolicy(AllowedOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")

                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                    });
            });
            #endregion


        }
        //AutoFac Container
        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region Register DB Context
            builder.Register(c =>
            {
                return new SampleContext(new DbContextOptionsBuilder<SampleContext>().UseSqlServer(Configuration.GetConnectionString("DBConString")).Options);
            }).InstancePerLifetimeScope();
            #endregion

            #region Register Unit Of Work
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            #endregion

            #region Register The Genric Output Port for all response
            builder.RegisterGeneric(typeof(OutputPort<>)).PropertiesAutowired();
            builder.RegisterGeneric(typeof(OutputPort<>)).As(typeof(IOutputPort<>)).InstancePerLifetimeScope().PropertiesAutowired();
            #endregion

            #region Register HttpContextAccessor In Order To Access The Http Context Inside A Class Library (Sample.Core Project)
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance().PropertiesAutowired();
            #endregion

            #region Register Localization
            builder.RegisterType<LocalizationReader>().As<ILocalizationReader>().SingleInstance();
            #endregion

            //registering all usecases & repositories by reflection
            #region Register All Repositories & UseCases
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).PublicOnly().Where(t => t.IsClass && t.Name.ToLower().EndsWith("usecase")).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).PublicOnly().Where(t => t.IsClass && t.Name.ToLower().EndsWith("repository")).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            #endregion

            #region Register Controller For Property DI 
            Type controllerBaseType = typeof(BaseController);

            //Add Controllers +As Service To Implment Property Injection
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType).PropertiesAutowired();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region CORS
            app.UseCors(AllowedOrigins);
            #endregion

            #region AppBuilder
            app.UseAppMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
        Path.Combine(env.ContentRootPath, "documents")),
                RequestPath = "/documents"
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Swagger
            IConfigurationSection SwaggerBasePath = Configuration.GetSection("SwaggerBasePath");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{ SwaggerBasePath.Value}/swagger/v1/swagger.json", "E-Vision Sample API V1");
                c.RoutePrefix = string.Empty;
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
