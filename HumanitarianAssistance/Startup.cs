using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using HumanitarianAssistance.Service;
using Microsoft.AspNetCore.Identity;
using HumanitarianAssistance.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HumanitarianAssistance.WebAPI.Auth;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.Service.Classes;
using HumanitarianAssistance.Common.ApplicationSettings;
using HumanitarianAssistance.WebAPI.Extensions;
using DataAccess.DbEntities;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using HumanitarianAssistance.WebAPI;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using Google.Apis.Logging;
using HumanitarianAssistance.WebAPI.ChaHub;
using HumanitarianAssistance.Service.Classes.Marketing;
using HumanitarianAssistance.Service.interfaces.Marketing;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.Service.Classes.AccountingNew;
using Microsoft.Extensions.Logging;
using HumanitarianAssistance.WebAPI.Filter;
using Newtonsoft.Json;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance
{
  public class Startup
  {
    private string DefaultCorsPolicyName;
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
      .AddEnvironmentVariables() ;
      string sAppPath = env.ContentRootPath; //Application Base Path
      string swwwRootPath = env.WebRootPath;  //wwwroot folder path
      Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      DefaultCorsPolicyName = Configuration["DefaultCorsPolicyName:PolicyName"];
      string DefaultCorsPolicyUrl = Configuration["DefaultCorsPolicyName:PolicyUrl"];
      string connectionString = Configuration.GetConnectionString("linuxdb");


      services.AddDbContextPool<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

      services.AddSwaggerGen(p =>
      {
        p.SwaggerDoc("v1", new Info { Title = "CHA Core API", Description = "Swagger API" });
        p.AddSecurityDefinition("Bearer", new ApiKeyScheme
        {
          Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
          Name = "Authorization",
          In = "header",
          Type = "apiKey"
        });
      });


      //services.AddDefaultIdentity<ApplicationUser>()
      // .AddEntityFrameworkStores<ApplicationDbContext>()
      // .AddDefaultTokenProviders();

      // ===== Add Identity ========
      services.AddIdentity<AppUser, IdentityRole>(o =>
      {
        o.Password.RequireDigit = false;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 6;

      }).AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();


      // services.AddI
      //.AddUserStore<ApplicationUserStore>() //this one provides data storage for user.
      //.AddRoleStore<ApplicationRoleStore>()
      //.AddUserManager<ApplicationUserManager>()
      //.AddRoleManager<ApplicationRoleManager>()
      services.AddSingleton<IFileProvider>(
               new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
      services.AddSingleton<IRole, RoleService>();
      services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("AuthMessageSenderOptions"));
      services.Configure<WebSiteUrl>(Configuration.GetSection("WebSiteUrl"));
      services.Configure<SwaggerEndPoint>(Configuration.GetSection("SwaggerEndPoint"));
      services.AddSingleton<IJwtFactory, JwtFactory>();
      services.AddTransient<IPermissions, PermissionService>();
      services.AddTransient<IOfficeDetails, OfficeDetailsService>();
      services.AddTransient<IPermissionsInRoles, PermissionsInRolesService>();
      services.AddTransient<IUserDetails, UserDetailsService>();
      services.AddTransient<ICurrency, CurrencyService>();
      services.AddTransient<IJournalDetail, JournalDetailService>();
      services.AddTransient<IEmailSetting, EmailSettingService>();
      services.AddTransient<IChartAccoutDetail, ChartAccountDetailService>();
      services.AddTransient<IVoucherDetail, VoucherDetailService>();
      services.AddTransient<IExchangeRate, ExchangeRateService>();
      services.AddTransient<IHREmployee, HREmployeeService>();
      services.AddTransient<IDesignation, DesignationService>();
      services.AddTransient<IAccountBalance, AccountBalanceService>();
      //services.AddTransient<IProjectBudget, ProjectBudgetService>();
      //services.AddTransient<IProjectDetails, ProjectDetailService>();
      services.AddTransient<IProfession, ProfessionService>();
      services.AddTransient<ICode, CodeService>();
      services.AddTransient<ITaskAndActivity, TaskAndActivityService>();
      //services.AddTransient<IProjectPipeLining, ProjectPipeLiningService>();
      services.AddTransient<IStore, StoreService>();
      services.AddTransient<INotificationManager, NotificationManagerService>();
      services.AddTransient<IEmployeeDetail, EmployeeDetailService>();
      services.AddTransient<IEmployeeHR, EmployeeHRService>();
      services.AddTransient<IAccountRecords, AccountReportsService>();
      services.AddTransient<IProject, ProjectService>();
      services.AddTransient<IMasterPageService, MasterPageService>();
      services.AddTransient<IJobDetailsService, JobDetailsService>();
      services.AddTransient<IContractDetailsService, ContractDetailService>();
      services.AddTransient<IChartOfAccountNewService, ChartOfAccountNewService>();
      services.AddTransient<IPolicyService, PolicyService>();
      services.AddTransient<IClientDetails, ClientDetailsService>();
      services.AddTransient<IVoucherNewService, VoucherNewService>();

      services.AddTransient<IAccountBalance, AccountBalanceService>();
      services.AddTransient<IProjectActivityService, ProjectActivityService>();

      //services.AddTransient<UserManager<AppUser>>();

      var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
      // Configure JwtIssuerOptions

      services.Configure<JwtIssuerOptions>(options =>
      {
        options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
        options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
      });

      var tokenValidationParameters = new TokenValidationParameters
      {
        // The signing key must match!
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = _signingKey,
        // Validate the JWT Issuer (iss) claim
        ValidateIssuer = true,
        ValidIssuer = Configuration.GetSection("JwtIssuerOptions:Issuer").Value,
        // Validate the JWT Audience (aud) claim
        ValidateAudience = true,
        ValidAudience = Configuration.GetSection("JwtIssuerOptions:Audience").Value,
        // Validate the token expiry
        ValidateLifetime = true,
        // If you want to allow a certain amount of clock drift, set that here:
        ClockSkew = TimeSpan.Zero
      };


      //services.AddTransient<IAccountNoteDetails, AccountNoteService>();

      //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

      services
          .AddAuthentication(options =>
          {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

          })
          .AddJwtBearer(cfg =>
          {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
              ValidIssuer = Configuration["JwtIssuerOptions:Issuer"],
              ValidAudience = Configuration["JwtIssuerOptions:Audience"],
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
              RequireExpirationTime = true,
              ClockSkew = TimeSpan.Zero


            };
          });

      // api user claim policy
      services.AddAuthorization(options =>
      {
        //  options.AddPolicy("Trust", policy => policy.RequireClaim("Permission", "dashboardhome"));
        options.AddPolicy("Trust", policy => policy.RequireClaim("Roles", "Admin", "SuperAdmin", "Accounting Manager", "HR Manager", "Project Manager", "Administrator"));
        options.AddPolicy("DepartmentUser", policy => policy.RequireClaim("OfficeCode"));
        options.AddPolicy("HRManager", policy => policy.RequireClaim("Roles", "HR Manager"));
        options.AddPolicy("Accounting", policy => policy.RequireClaim("Roles", "Accounting Manager"));

        options.AddPolicy("DepartmentUser", policy => policy.RequireClaim("DepartmentId"));
        //options.AddPolicy("Trust", policy => policy.RequireClaim("OfficeCode"));
        //options.AddPolicy("AdministratorPolicy", policy =>
        //          policy.RequireRole("SuperAdmin"));

        //options.AddPolicy("dashboardhome", policy => policy.RequireClaim("Permission", "dashboardhome"));
        //options.AddPolicy("home", policy => policy.RequireClaim("Permission", "home"));
        //options.AddPolicy("registercompany", policy => policy.RequireClaim("Permission", "registercompany"));
        //options.AddPolicy("registercompanycontact", policy => policy.RequireClaim("Permission", "registercompanycontact"));
      });
      var config1 = new AutoMapper.MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new AutoMapperProfile());
      });

      var mapper = config1.CreateMapper();
      services.AddSingleton(mapper);

      //For Cors Setting
      services.AddCors(options =>
      {
        options.AddPolicy(DefaultCorsPolicyName, p =>
        {
          //todo: Get from confiuration
          p.WithOrigins(DefaultCorsPolicyUrl).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
        });
      });
      services.AddTransient<IUnitOfWork, UnitOfWork>();
      services.AddMvc()
          .AddJsonOptions(config =>
          {
            // config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            config.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
          });
      services.AddMvc(
               config => { config.Filters.Add(typeof(CustomException)); }
               ).AddJsonOptions(a => a.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
      //Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
      services.AddRouting();

      services.AddSignalR();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbcontext, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager, ILogger<DbInitializer> logger)
    {

      UpdateDatabase(app, _userManager, _roleManager, logger).Wait();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        
        app.UseExceptionHandler(
            builder =>
            {
              builder.Run(
                 async context =>
                 {
                   context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                   context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                   var error = context.Features.Get<IExceptionHandlerFeature>();
                   if (error != null)
                   {
                    
                     var err = $"<h1>Error: {error.Error.Message}</h1>{error.Error.StackTrace }";
                     // context.Response.AddApplicationError(error.Error.Message);
                     await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                   }
                 });
            });

        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseCookiePolicy();

      app.UseCors(DefaultCorsPolicyName);
      app.UseAuthentication();

      // new DbInitializer(_userManager, _roleManager).Initialize(dbcontext);
      //app.UseStaticFiles();
      app.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(
          Path.Combine(Directory.GetCurrentDirectory(), @"Documents")),
        RequestPath = new PathString("/Docs")
      });


      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "CHA Core API");
      });

      app.UseSignalR(routes =>
      {
        routes.MapHub<ProjectChatHub>("/chathub");
      });

      app.UseMvc(routes =>
      {
        // SwaggerGen won't find controllers that are routed via this technique.
        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");

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
