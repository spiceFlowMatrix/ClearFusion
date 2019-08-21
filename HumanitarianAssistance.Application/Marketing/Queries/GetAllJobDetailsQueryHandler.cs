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
    public class GetAllJobDetailsQueryHandler : IRequestHandler<GetAllJobDetailsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllJobDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllJobDetailsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                int count = await _dbContext.JobDetails.CountAsync(x => x.IsDeleted == false);
                var JobList = await (from j in _dbContext.JobDetails.AsNoTracking().AsQueryable()
                                     join jp in _dbContext.JobPriceDetails on j.JobId equals jp.JobId
                                     where !j.IsDeleted && !jp.IsDeleted
                                     select (new JobDetailsModel
                                     {
                                         JobId = j.JobId,
                                         JobCode = j.JobCode,
                                         JobName = j.JobName,
                                         Description = j.Description,
                                         JobPhaseId = j.JobPhaseId,
                                         StartDate = j.StartDate,
                                         EndDate = j.EndDate,
                                         IsActive = j.IsActive,
                                         IsApproved = j.IsApproved,
                                         UnitRate = jp.UnitRate,
                                         Units = jp.Units,
                                         FinalRate = jp.FinalRate,
                                         FinalPrice = jp.FinalPrice,
                                         TotalPrice = jp.TotalPrice,
                                         CreatedDate = j.CreatedDate,
                                         IsInvoiceApproved = jp.IsInvoiceApproved
                                     })).OrderByDescending(x => x.JobId).Take(10).Skip(0).ToListAsync();

                //var list = await _uow.JobDetailsRepository.FindAllAsync(x => !x.IsDeleted);
                response.data.JobDetailsModel = JobList;
                response.data.jobListTotalCount = count;
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
