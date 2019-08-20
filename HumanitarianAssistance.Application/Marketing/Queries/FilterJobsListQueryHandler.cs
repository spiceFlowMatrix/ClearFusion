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
    public class FilterJobsListQueryHandler : IRequestHandler<FilterJobsListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public FilterJobsListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(FilterJobsListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            string finalPriceValue = null;
            string jobIdValue = null;
            string jobNameValue = null;
            string ApprovedValue = null;

            if (!string.IsNullOrEmpty(request.Value))
            {
                finalPriceValue = request.FinalPrice ? request.Value.ToLower().Trim() : null;
                jobIdValue = request.JobId ? request.Value.ToLower().Trim() : null;
                jobNameValue = request.JobName ? request.Value.ToLower().Trim() : null;
                ApprovedValue = request.Approved ? request.Value.ToLower().Trim() : null;
            }
            try
            {
                var voucherList = await _dbContext.JobPriceDetails.AsNoTracking().AsQueryable()
                                    .Where(v => v.IsDeleted == false &&
                                          (!string.IsNullOrEmpty(request.Value) ?
                                             (
                                                   v.JobDetails.JobId.ToString().Trim().ToLower().Contains(jobIdValue) ||
                                                   v.FinalPrice.ToString().Trim().ToLower().Contains(finalPriceValue) ||
                                                   v.JobDetails.JobName.Trim().ToLower().Contains(jobNameValue) ||
                                                   v.JobDetails.IsApproved.ToString().Trim().ToLower().Contains(ApprovedValue)
                                              ) : true
                                           )
                                     )
                                    .OrderByDescending(x => x.JobDetails.CreatedDate)
                                    .Select(x => new JobDetailsModel
                                    {
                                        JobId = x.JobId,
                                        JobCode = x.JobDetails.JobCode,
                                        JobName = x.JobDetails.JobName,
                                        EndDate = x.JobDetails.EndDate,
                                        IsActive = x.JobDetails.IsActive,
                                        IsApproved = x.JobDetails.IsApproved,
                                        UnitRate = x.UnitRate,
                                        Units = x.Units,
                                        FinalRate = x.FinalRate,
                                        FinalPrice = x.FinalPrice,
                                        TotalPrice = x.TotalPrice,
                                        IsInvoiceApproved = x.IsInvoiceApproved,
                                        ContractId = x.JobDetails.ContractId,
                                        Discount = x.Discount,
                                        DiscountPercent = x.DiscountPercent,
                                        Minutes = x.Minutes
                                    })
                                    .AsNoTracking()
                                    .ToListAsync();
                response.data.jobListTotalCount = voucherList.Count();
                response.data.JobPriceDetailList = voucherList;
                response.StatusCode = StaticResource.successStatusCode;
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
