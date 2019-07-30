using System.IO;
using DataAccess;
using HumanitarianAssistance.Common.ApplicationSettings;
using HumanitarianAssistance.Service;
using HumanitarianAssistance.Service.Classes;
using HumanitarianAssistance.Service.Classes.AccountingNew;
using HumanitarianAssistance.Service.Classes.Marketing;
using HumanitarianAssistance.Service.Classes.ProjectManagement;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.Service.interfaces.Marketing;
using HumanitarianAssistance.Service.interfaces.ProjectManagement;
using HumanitarianAssistance.WebApi.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace HumanitarianAssistance.WebApi.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddSingleton<IRole, RoleService>();
            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("AuthMessageSenderOptions"));
            services.Configure<WebSiteUrl>(Configuration.GetSection("WEB_SITE_URL"));
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
            services.AddTransient<IProfession, ProfessionService>();
            services.AddTransient<ICode, CodeService>();
            services.AddTransient<ITaskAndActivity, TaskAndActivityService>();
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
            services.AddTransient<ISchedulerService, SchedulerService>();
            services.AddTransient<IAccountBalance, AccountBalanceService>();
            services.AddTransient<IProjectActivityService, ProjectActivityService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IProjectPeopleService, ProjectPeopleService>();
            services.AddTransient<IFileManagement, FileManagementService>();
            services.AddTransient<IHiringRequestService, HiringRequestService>();
            services.AddTransient<IChat, ChatService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}