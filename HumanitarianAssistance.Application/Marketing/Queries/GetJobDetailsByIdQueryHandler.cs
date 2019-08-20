using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Application.Marketing.Models;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetJobDetailsByIdQueryHandler : IRequestHandler<GetJobDetailsByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetJobDetailsByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetJobDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var JobList = await (from j in _dbContext.JobDetails.AsNoTracking().AsQueryable()
                                     join jp in _dbContext.JobPriceDetails on j.JobId equals jp.JobId
                                     join cd in _dbContext.ContractDetails on j.ContractId equals cd.ContractId
                                     join cur in _dbContext.CurrencyDetails on cd.CurrencyId equals cur.CurrencyId
                                     where !j.IsDeleted && !jp.IsDeleted && j.JobId == request.jobId
                                     select (new JobPriceModel
                                     {
                                         JobId = j.JobId,
                                         JobName = j.JobName,
                                         JobCode = j.JobCode,
                                         UnitRate = jp.UnitRate,
                                         FinalRate = jp.FinalRate,
                                         FinalPrice = jp.FinalPrice,
                                         TotalPrice = jp.TotalPrice,
                                         Discount = jp.Discount,
                                         DiscountPercent = jp.DiscountPercent,
                                         EndDate = j.EndDate,
                                         StartDate = cd.StartDate,
                                         ContractId = j.ContractId,
                                         Minutes = jp.Minutes,
                                         IsApproved = j.IsApproved,
                                         IsAgreementApproved = j.IsAgreementApproved,
                                         ClientId = cd.ClientId,
                                         ClientName = cd.ClientName,
                                         JobPriceId = jp.JobPriceId,
                                         CurrencyCode = cur.CurrencyCode

                                     })).FirstOrDefaultAsync();
                response.data.JobPriceDetail = JobList;
                response.StatusCode = 200;
                response.Message = "Success";
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
