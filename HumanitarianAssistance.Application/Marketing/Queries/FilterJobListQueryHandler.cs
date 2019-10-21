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
    public class FilterJobListQueryHandler : IRequestHandler<FilterJobListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public FilterJobListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(FilterJobListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var JobList1 = await (from j in _dbContext.JobDetails.AsNoTracking().AsQueryable()
                                      join jp in _dbContext.JobPriceDetails on j.JobId equals jp.JobId
                                      where !j.IsDeleted && !jp.IsDeleted
                                      select (new JobDetailsModel
                                      {
                                          JobId = j.JobId,
                                          JobCode = j.JobCode,
                                          JobName = j.JobName,
                                          EndDate = j.EndDate,
                                          IsActive = j.IsActive,
                                          IsApproved = j.IsApproved,
                                          UnitRate = jp.UnitRate,
                                          Units = jp.Units,
                                          FinalRate = jp.FinalRate,
                                          FinalPrice = jp.FinalPrice,
                                          TotalPrice = jp.TotalPrice,
                                          IsInvoiceApproved = jp.IsInvoiceApproved,
                                          ContractId = j.ContractId,
                                          Discount = jp.Discount,
                                          DiscountPercent = jp.DiscountPercent,
                                          Minutes = jp.Minutes
                                      })).ToListAsync();

                if (request != null)
                {
                    if (!string.IsNullOrEmpty(request.JobName))
                    {
                        JobList1 = JobList1.Where(x => x.JobName == request.JobName).ToList();
                    }
                    if (request.JobId != 0)
                    {
                        JobList1 = JobList1.Where(x => x.JobId == request.JobId).ToList();
                    }
                    if (request.IsApproved == true)
                    {
                        JobList1 = JobList1.Where(x => x.IsApproved == Convert.ToBoolean(request.IsApproved)).ToList();
                    }
                    if (request.YesOrNo == "No")
                    {
                        JobList1 = JobList1.Where(x => x.IsApproved == false).ToList();
                    }
                    if (request.ContractId != 0)
                    {
                        JobList1 = JobList1.Where(x => x.ContractId == request.ContractId).ToList();
                    }
                    if (!string.IsNullOrEmpty(request.FilterType))
                    {
                        if (request.FilterType == "Equals")
                        {
                            if (request.UnitRate != 0)
                            {
                                JobList1 = JobList1.Where(x => x.UnitRate == request.UnitRate).ToList();
                            }
                            if (request.TotalPrice != 0)
                            {
                                JobList1 = JobList1.Where(x => x.TotalPrice == request.TotalPrice).ToList();
                            }
                        }
                        if (request.FilterType == "Greater Than")
                        {
                            if (request.UnitRate != 0)
                            {
                                JobList1 = JobList1.Where(x => x.UnitRate > request.UnitRate).ToList();
                            }
                            if (request.TotalPrice != 0)
                            {
                                JobList1 = JobList1.Where(x => x.TotalPrice > request.TotalPrice).ToList();
                            }
                        }
                        if (request.FilterType == "Less Than")
                        {
                            if (request.UnitRate != 0)
                            {
                                JobList1 = JobList1.Where(x => x.UnitRate < request.UnitRate).ToList();
                            }
                            if (request.TotalPrice != 0)
                            {
                                JobList1 = JobList1.Where(x => x.TotalPrice < request.TotalPrice).ToList();
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(request.FilterType))
                    {
                        if (request.UnitRate != 0)
                        {
                            JobList1 = JobList1.Where(x => x.UnitRate == request.UnitRate).ToList();
                        }
                        if (request.TotalPrice != 0)
                        {
                            JobList1 = JobList1.Where(x => x.TotalPrice == request.TotalPrice).ToList();
                        }
                    }
                    response.data.JobPriceDetailList = JobList1;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "No Entries Found.Try Different Filters";
                }
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
