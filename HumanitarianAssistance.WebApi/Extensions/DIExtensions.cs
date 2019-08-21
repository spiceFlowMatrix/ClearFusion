using HumanitarianAssistance.Application.CommonServices;
using HumanitarianAssistance.Application.CommonServicesInterface;
using Microsoft.Extensions.DependencyInjection;

namespace HumanitarianAssistance.WebApi.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IAccountBalanceServices, AccountBalanceServices>();
            services.AddTransient<IAccountingServices, AccountingServices>();
            services.AddTransient<IProjectServices, ProjectServices>();
            services.AddTransient<IStoreServices, StoreServices>();

            return services;
        }
    }
}