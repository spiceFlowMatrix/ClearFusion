using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RazorLight;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherSummaryReportPdfQueryHandler : IRequestHandler<GetAllVoucherSummaryReportPdfQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IRazorLightEngine _razorEngine;
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        public GetAllVoucherSummaryReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IRazorLightEngine razorEngine, 
            IRazorViewEngine razorViewEngine, IServiceProvider serviceProvider, ITempDataProvider tempDataProvider)
        {
            _dbContext = dbContext;
            _razorEngine = razorEngine;
            _razorViewEngine = razorViewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<ApiResponse> Handle(GetAllVoucherSummaryReportPdfQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            var viewName = "TestPdf";
            try
            {
                var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
                var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

                using (var sw = new StringWriter())
                {
                    var viewResult = _razorViewEngine.GetView("","~/Pages/TestPdf.cshtml",false);

                    if (viewResult.View == null)
                    {
                        throw new ArgumentNullException($"{viewName} does not match any available view");
                    }

                    var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = null
                    };

                    var viewContext = new ViewContext(
                        actionContext,
                        viewResult.View,
                        viewDictionary,
                        new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                        sw,
                        new HtmlHelperOptions()
                    );

                    await viewResult.View.RenderAsync(viewContext);
                    response.ResponseData = sw.ToString();
                }



                //var voucherSummaryList = await _dbContext.VoucherDetail.Where(x => !x.IsDeleted).ToListAsync();
                //response.ResponseData = voucherSummaryList;
                //response.StatusCode = StaticResource.successStatusCode;
                //response.Message = StaticResource.SuccessText;


                
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}