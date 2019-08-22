using AutoMapper;
using DataAccess.Data;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.WebApi.Extensions;
using HumanitarianAssistance.WebApi.Filter;
using HumanitarianAssistance.WebApi.SignalRHub;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi
{
    public class Startup
    {
        private string defaultCorsPolicyName;
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        public static IHostingEnvironment _Env;


        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile(StaticResource.appsettingJsonFile, optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //get and set environment variable at run time
            string connectionString = Environment.GetEnvironmentVariable("LINUX_DBCONNECTION_STRINGTWO");
            string DefaultsPolicyName = Environment.GetEnvironmentVariable("DEFAULT_CORS_POLICY_NAME");
            string DefaultCorsPolicyUrl = Environment.GetEnvironmentVariable("DEFAULT_CORS_POLICY_URL");

            defaultCorsPolicyName = Configuration["DEFAULT_CORS_POLICY_NAME"];

            // Database connection
            Console.WriteLine("Connection string: {0}\n", connectionString);
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseNpgsql(connectionString));


            // Add Identity
            services.AddIdentity<AppUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();


            // Dependency Injection
            services.AddDependencyInjection(Configuration);

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

            // filter & default response set to json  
            services.AddMvc(c => { c.Filters.Add(typeof(CustomException)); })
            .AddJsonOptions(c =>
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            // update database
            UpdateDatabase(app, userManager, roleManager, logger).Wait();

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
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //   FileProvider = new PhysicalFileProvider(
            //   Path.Combine(Directory.GetCurrentDirectory(), @"Documents")),
            //    RequestPath = new PathString("/Docs")
            //});
            app.UseStaticFiles();

            app.UseCookiePolicy();
            app.UseCors(defaultCorsPolicyName);
            app.UseAuthentication();

            // swagger configuration
            app.UseSwaggerDocumentation();

            // signal-R
            app.UseSignalR(routes =>
            {
               // routes.MapHub<ChatHub>("/chathub");
                routes.MapHub<NotifyHub>("/notifyhub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller}/{action=Index}/{id?}");
            });

            // NOTE: Use to update web.config for redirection redirection 
            // var options = new RewriteOptions()
            //            // .AddRedirect("redirect-rule/(.*)", "redirected/$1")
            //            // .AddRewrite(@"^rewrite-rule/(\d+)/(\d+)", "rewritten?var1=$1&var2=$2", 
            //            //     skipRemainingRules: true)
            //            .AddRedirect("$", "newui")
            //            .AddRewrite(@"^$", "newui", skipRemainingRules: true);

            // app.UseRewriter(options);


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
                    // spa.UseAngularCliServer(npmScript: "start");
                }
                else
                {
                    spa.Options.DefaultPageStaticFileOptions = defaultDist;
                }
            });

        }

        //2011
        private static async Task UpdateDatabase(IApplicationBuilder app, UserManager<AppUser> um, RoleManager<IdentityRole> rm, ILogger<DbInitializer> logger)
        {
            using (var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();

                    if (!context.Users.Any())
                    {
                        await DbInitializer.CreateDefaultUserAndRoleForApplication(um, rm, context, logger);
                    }

                    // check if Contract Content present or not
                    if (!context.ContractTypeContent.Any())
                    {
                        await DbInitializer.AddContractClauses(context);
                    }

                    // check if JobGrade present or not
                    if (!context.JobGrade.Any())
                    {
                        await DbInitializer.AddJobGrades(context);
                    }

                    // check if Categories present or not
                    if (!context.Categories.Any())
                    {
                        await DbInitializer.AddMarketingCategory(context);
                    }
                    if (!context.ActivityStatusDetail.Any())
                    {
                        await DbInitializer.AddActivityStatus(context);
                    }
                }
            }
        }

    }
    
}