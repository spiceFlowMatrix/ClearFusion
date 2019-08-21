using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetExchangeGainLossVoucherListQueryHandler: IRequestHandler<GetExchangeGainLossVoucherListQuery, ApiResponse>
    {
       private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetExchangeGainLossVoucherListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public async Task<ApiResponse> Handle(GetExchangeGainLossVoucherListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var gainLossVouchers = await _dbContext.VoucherDetail
                                                        .Where(x => x.IsDeleted == false &&
                                                               x.IsExchangeGainLossVoucher == true)
                                                        .Select(x => new GainLossVoucherListModel
                                                        {
                                                            VoucherId = x.VoucherNo,
                                                            VoucherName = x.ReferenceNo,
                                                            JournalName = x.JournalDetails.JournalName,
                                                            VoucherDate = x.VoucherDate,
                                                        })
                                                        .ToListAsync();                

                response.data.GainLossVoucherList = gainLossVouchers;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}