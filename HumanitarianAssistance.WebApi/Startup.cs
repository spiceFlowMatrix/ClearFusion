using MediatR;
using HumanitarianAssistance.Application.Accounting.Queries;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Infrastructure.Extensions;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.FileProviders;
using System.IO;
using HumanitarianAssistance.WebApi.SignalRHub;

namespace HumanitarianAssistance.WebApi
{
    public class Startup
    {
        private string defaultCorsPolicyName = string.Empty;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //get and set environment variable at run time
            string connectionString = Environment.GetEnvironmentVariable("LINUX_DBCONNECTION_STRING");
            string DefaultCorsPolicyUrl = Environment.GetEnvironmentVariable("DEFAULT_CORS_POLICY_URL");
            // string DefaultsPolicyName = Environment.GetEnvironmentVariable("DEFAULT_CORS_POLICY_NAME");

            defaultCorsPolicyName = Configuration["DEFAULT_CORS_POLICY_NAME"];

            // Database connection
            Console.WriteLine("Connection string: {0}\n", connectionString);
            services.AddDbContextPool<HumanitarianAssistanceDbContext>(options => options.UseNpgsql(connectionString));

            // identity
            services.AddIdentity<AppUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<HumanitarianAssistanceDbContext>().AddDefaultTokenProviders();


            // Mediater Between Send To Handler
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(GetMainLevelAccountQueryHandler).GetTypeInfo().Assembly);

            // Dependency Injection
            services.AddDependencyInjection();

            // AutoMapper will scan our assembly and look for classes that inherit from Profile, then load their mapping configurations.
            services.AddAutoMapper();

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Trust", policy => policy.RequireClaim("Roles", "Admin", "SuperAdmin", "Accounting Manager", "HR Manager", "Project Manager", "Administrator"));
            });


            //For Cors Setting
            services.AddCors(options =>
            {
                options.AddPolicy(defaultCorsPolicyName, p =>
                {
                    //todo: Get from configuration
                    p.WithOrigins(DefaultCorsPolicyUrl).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();

                });
            });


            // set compatibility
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // default response set to json  
            services.AddMvc().AddJsonOptions(c =>
            {
                c.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                c.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddRouting();
            services.AddSignalR();


            // Jwt Config
            services.AddJwtAuthentication(Configuration);

            // swagger configuration
            services.AddSwaggerDocumentation();


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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"Documents")),
                RequestPath = new PathString("/Docs")
            });

            app.UseCookiePolicy();
            app.UseCors(defaultCorsPolicyName);
            app.UseAuthentication();

            // swagger configuration
            app.UseSwaggerDocumentation();

            // signal-R
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotifyHub>("/notifyhub");
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller}/{action=Index}/{id?}");
            });


            #region "Frontend config"

            // for each angular client we want to host. 
            app.Map(new PathString("/oldui"), client =>
            {
                string oldUiPath = env.IsDevelopment() ? "OldUI" : @"OldUI/dist";

                // Each map gets its own physical path for it to map the static files to. 
                StaticFileOptions olduiDist = new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), oldUiPath)
                        )
                };

                // Each map its own static files otherwise it will only ever serve index.html no matter the filename 
                client.UseSpaStaticFiles(olduiDist);

                client.UseSpa(spa =>
                {
                    spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                    spa.Options.SourcePath = "OldUI";

                    if (env.IsDevelopment())
                    {
                        // it will use package.json & will search for start command to run
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                    else
                    {
                        spa.Options.DefaultPageStaticFileOptions = olduiDist;
                    }
                });
            });

            // without map redirect to newui
            string defaultPath = env.IsDevelopment() ? "NewUI" : @"NewUI/dist";
            StaticFileOptions defaultDist = new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), defaultPath)
                    )
            };
            app.UseSpaStaticFiles(defaultDist);
            app.UseSpa(spa =>
            {
                spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                spa.Options.SourcePath = "NewUI";

                if (env.IsDevelopment())
                {
                    // it will use package.json & will search for start command to run
                    spa.UseAngularCliServer(npmScript: "start");
                }
                else
                {
                    spa.Options.DefaultPageStaticFileOptions = defaultDist;
                }
            });

            #endregion


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<h1>Humanitarian Assistance app running ... </h1>");
            });
        }
    }
}
