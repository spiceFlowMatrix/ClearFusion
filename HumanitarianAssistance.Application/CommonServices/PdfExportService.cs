using System;
using System.IO;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using iText.Html2pdf;
using iText.Kernel.Pdf;

namespace HumanitarianAssistance.Application.CommonServices
{
    public class PdfExportService : IPdfExportService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        public PdfExportService(IRazorViewEngine razorViewEngine, IServiceProvider serviceProvider, ITempDataProvider tempDataProvider)
        {
            _razorViewEngine = razorViewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<byte[]> ExportToPdf(object model, string viewName)
        {
            try
            {
                // use for http routing
                var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
                var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

                // used to convert view to string
                using (var stringWriter = new StringWriter())
                {
                    // get view
                    var viewResult = _razorViewEngine.GetView("", "~/" + viewName, false);

                    if (!viewResult.Success)
                    {
                        throw new ArgumentNullException($"{viewName} does not match any available view");
                    }

                    // set the model value for view
                    var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = model
                    };

                    // initialize view
                    var viewContext = new ViewContext(
                        actionContext,
                        viewResult.View,
                        viewDictionary,
                        new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                        stringWriter,
                        new HtmlHelperOptions()
                    );

                    // render view 
                    await viewResult.View.RenderAsync(viewContext);

                    var _stream = new MemoryStream();

                    // convert string to pdf in stream & then destroy the stream 
                    using (var pdfWriter = new PdfWriter(_stream))
                    {
                        pdfWriter.SetCloseStream(false);

                        // CAUTION : Don't remove using block from here, it used to destroy the stream once pdf is generated
                        using (var document = HtmlConverter.ConvertToDocument(stringWriter.ToString(), pdfWriter)) { }
                    }
                    _stream.Position = 0;

                    return _stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}