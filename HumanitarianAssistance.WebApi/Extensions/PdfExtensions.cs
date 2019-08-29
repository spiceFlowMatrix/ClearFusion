using System;
using System.IO;
using System.Reflection;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;

namespace HumanitarianAssistance.WebApi.Extensions
{
    public static class PdfExtensions
    {
        public static IServiceCollection AddPdfExtension(this IServiceCollection services)
        {
            var processSufix = "32bit";
            if (Environment.Is64BitProcess && IntPtr.Size == 8)
            {
                processSufix = "64bit";
            }

            var pdfcontext = new CustomPdfContext();
            pdfcontext.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), $"PDFNative/{processSufix}/libwkhtmltox.dll"));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            // // razon services
            // services.AddScoped<IRazorLightEngine>(sp =>
            // {
            //     var engine = new RazorLightEngineBuilder()
            //         .UseFilesystemProject(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
            //         .UseMemoryCachingProvider()
            //         .Build();
            //     return engine;
            // });

           

            return services;
        }
    }
}