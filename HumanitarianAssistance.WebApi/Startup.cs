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
using Northwind.WebUI.Common;
using Stackdriver;
using Google.Cloud.Diagnostics.AspNetCore;
using Google.Cloud.Diagnostics.Common;
using Microsoft.Extensions.Logging;
using Google.Cloud.Logging.V2;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0WebApi.Handlers;
using Microsoft.AspNetCore.Authorization;

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
            string DefaultCorsPolicyUrl = Environment.GetEnvironmentVariable("DEFAULT_CORS_POLICY_URL");
            // string connectionString = Environment.GetEnvironmentVariable("LINUX_DBCONNECTION_STRING");
            string connectionString = "Server=35.204.36.109;Port=5432;Database=edg_testing_database;User Id=clearfusion_review_proxyuser;Password=867983learnhave86momenttravellocate54";
            defaultCorsPolicyName = Configuration["DEFAULT_CORS_POLICY_NAME"];

            // Database connection
            Console.WriteLine("Connection string: {0}\n", connectionString);
            services.AddDbContextPool<HumanitarianAssistanceDbContext>(options => options.UseNpgsql(connectionString));
            services.Configure<StackdriverOptions>(
                Configuration.GetSection("Stackdriver")); //Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS")
            var googleCredential = GoogleCredential.FromFile(Directory.GetCurrentDirectory() + "/clear-fusion-193608-0deb8499ba04.json");
            var channel = new Grpc.Core.Channel(
                LoggingServiceV2Client.DefaultEndpoint.Host,
                googleCredential.ToChannelCredentials());
            services.AddGoogleExceptionLogging(options =>
            {
                options.ProjectId = Configuration["Stackdriver:ProjectId"];
                options.ServiceName = Configuration["Stackdriver:ServiceName"];
                options.Version = Configuration["Stackdriver:Version"];
                options.Options = ErrorReportingOptions.Create(
                EventTarget.ForLogging(
                    projectId: Configuration["Stackdriver:ProjectId"],
                    loggingClient: LoggingServiceV2Client.Create(channel)));
            });

            // Add trace service.
            // services.AddGoogleTrace(options =>
            // {
            //     options.ProjectId = Configuration["Stackdriver:ProjectId"];
            //     options.Options = TraceOptions.Create(
            //         bufferOptions: BufferOptions.NoBuffer());
            // });

            // identity
            services.AddIdentity<AppUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 1;

            }).AddEntityFrameworkStores<HumanitarianAssistanceDbContext>().AddDefaultTokenProviders();


            // Mediater Between Send To Handler
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(GetMainLevelAccountQueryHandler).GetTypeInfo().Assembly);

            // Dependency Injection
            services.AddDependencyInjection();

            // AutoMapper will scan our assembly and look for classes that inherit from Profile, then load their mapping configurations.
            services.AddAutoMapper();

            // add authorization
            // services.AddAuthorization();


            //For Cors Setting
            services.AddCors(options =>
            {
                options.AddPolicy(defaultCorsPolicyName, p =>
                {
                    //todo: Get from configuration
                    p.WithOrigins(DefaultCorsPolicyUrl).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("Content-Disposition", "ExMessage");

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

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:ApiIdentifier"];

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // Grab the raw value of the token, and store it as a claim so we can retrieve it again later in the request pipeline
                        // Have a look at the ValuesController.UserInformation() method to see how to retrieve it and use it to retrieve the
                        // user's information from the /userinfo endpoint
                        if (context.SecurityToken is JwtSecurityToken token)
                        {
                            if (context.Principal.Identity is ClaimsIdentity identity)
                            {
                                identity.AddClaim(new Claim("access_token", token.RawData));
                            }
                        }

                        return Task.FromResult(0);
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
                options.AddPolicy("view:vouchers", policy => policy.Requirements.Add(new HasScopeRequirement("view:vouchers", domain)));
                options.AddPolicy("edit:vouchers", policy => policy.Requirements.Add(new HasScopeRequirement("edit:vouchers", domain)));

            });



            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            // // 406 (Not Acceptable) 
            // // use to return files from Controller
            // services.AddMvc(options =>
            // {
            //     options.ReturnHttpNotAcceptable = true;
            // });

            services.AddRouting();
            services.AddSignalR();


            // Jwt configuration
            // services.AddJwtAuthentication(Configuration);

            // swagger configuration
            services.AddSwaggerDocumentation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseCustomExceptionHandler();
            // app.UseStaticFiles(new StaticFileOptions()
            // {
            //     FileProvider = new PhysicalFileProvider(
            //     Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
            //     RequestPath = new PathString("/Docs")
            // });
            var googleCredential = GoogleCredential.FromFile(Directory.GetCurrentDirectory() + "/clear-fusion-193608-0deb8499ba04.json");
            var channel = new Grpc.Core.Channel(
                LoggingServiceV2Client.DefaultEndpoint.Host,
                googleCredential.ToChannelCredentials());
            loggerFactory.AddGoogle(Configuration["Stackdriver:ProjectId"], null, LoggingServiceV2Client.Create(channel));
            app.UseGoogleExceptionLogging();
            // Configure trace service.
            // app.UseGoogleTrace();

            app.UseStaticFiles();
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
                        // spa.UseAngularCliServer(npmScript: "start");
                        spa.Options.DefaultPageStaticFileOptions = olduiDist;
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
                    // spa.Options.DefaultPageStaticFileOptions = defaultDist;
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
