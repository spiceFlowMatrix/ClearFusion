using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVouchersByOfficeIdQueryHandler : IRequestHandler<GetAllVouchersByOfficeIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllVouchersByOfficeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllVouchersByOfficeIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                var voucherList = await _dbContext.VoucherDetail
                                                            .Where(v => v.IsDeleted == false && v.OfficeId == request.OfficeId)
                                                            .OrderBy(x => x.ReferenceNo)
                                                            .Select(v => new VoucherDetailModel
                                                            {
                                                                VoucherNo = v.VoucherNo,
                                                                ReferenceNo = v.ReferenceNo,
                                                                VoucherDate = v.VoucherDate
                                                            })
                                                            .ToListAsync();

                response.data.VoucherDetailList = voucherList;
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