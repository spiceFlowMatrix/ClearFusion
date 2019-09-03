using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RazorLight;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherSummaryReportPdfQueryHandler : IRequestHandler<GetAllVoucherSummaryReportPdfQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IRazorLightEngine _razorEngine;

        public GetAllVoucherSummaryReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IRazorLightEngine razorEngine)
        {
            _dbContext = dbContext;
            _razorEngine = razorEngine;
        }

        public async Task<ApiResponse> Handle(GetAllVoucherSummaryReportPdfQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var voucherSummaryList = await _dbContext.VoucherDetail.Where(x => !x.IsDeleted).ToListAsync();



                response.ResponseData = voucherSummaryList;
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