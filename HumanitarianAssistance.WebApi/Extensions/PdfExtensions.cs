using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;

namespace HumanitarianAssistance.WebApi.Extensions
{
    public static class PdfExtensions
    {
        public static IServiceCollection AddPdfExtension(this IServiceCollection services)
        {
            // razon services
            services.AddScoped<IRazorLightEngine>(sp =>
            {
                var engine = new RazorLightEngineBuilder()
                    .UseFilesystemProject(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                    .UseMemoryCachingProvider()
                    .Build();
                return engine;
            });

           

            return services;
        }
    }
}