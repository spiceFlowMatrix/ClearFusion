using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Create
{
    public class GenerateInvoiceCommandHandler : IRequestHandler<GenerateInvoiceCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public GenerateInvoiceCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(GenerateInvoiceCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var scheduleList = await _dbContext.ScheduleDetails.Where(x => x.JobId == request.jobId && x.IsDeleted == false).ToListAsync();
                var playout = await _dbContext.PlayoutMinutes.Where(x => scheduleList.Any(s => s.ScheduleId == x.ScheduleId)).ToListAsync();
                var JobList = await (from j in _dbContext.JobDetails.AsNoTracking().AsQueryable()
                                     join jp in _dbContext.JobPriceDetails on j.JobId equals jp.JobId
                                     join cd in _dbContext.ContractDetails on j.ContractId equals cd.ContractId
                                     join cur in _dbContext.CurrencyDetails on cd.CurrencyId equals cur.CurrencyId
                                     where !j.IsDeleted && !jp.IsDeleted && j.JobId == request.jobId
                                     select (new JobPriceModel
                                     {
                                         JobId = j.JobId,
                                         EndDate = j.EndDate,
                                         JobName = j.JobName,
                                         ClientName = cd.ClientName,
                                         TotalPrice = jp.TotalPrice,
                                         Minutes = jp.Minutes,
                                         CurrencyCode = cur.CurrencyCode
                                     })).FirstOrDefaultAsync();
                InvoiceModel invoiceDetails = new InvoiceModel();
                if (scheduleList.Count > 0)
                {
                    if (playout.Count > 0)
                    {
                        var TotalMinutes = (from n in playout
                                            group n by 1 into og
                                            select new
                                            {
                                                TotalTransacted = og.Sum(item => item.TotalMinutes),
                                            }).FirstOrDefault().TotalTransacted;
                        invoiceDetails.TotalRunningMinutes = TotalMinutes;
                    }
                    else
                    {
                        var minutes = from n in scheduleList
                                      select new
                                      {
                                          TotalTransacted = ((n.EndTime - n.StartTime).TotalHours) * 60,
                                      };
                        var TotalMinutes = (from n in minutes
                                            group n by 1 into og
                                            select new
                                            {
                                                TotalTransacted = og.Sum(item => item.TotalTransacted),
                                            }).FirstOrDefault().TotalTransacted;
                        invoiceDetails.TotalRunningMinutes = Convert.ToInt32(TotalMinutes);
                    }
                    invoiceDetails.JobId = JobList.JobId;
                    invoiceDetails.EndDate = JobList.EndDate;
                    invoiceDetails.JobRate = JobList.TotalPrice;
                    invoiceDetails.CurrencyCode = JobList.CurrencyCode;
                    invoiceDetails.TotalMinutes = JobList.Minutes;
                    invoiceDetails.JobName = JobList.JobName;
                    invoiceDetails.ClientName = JobList.ClientName;
                    invoiceDetails.FinalPrice = (invoiceDetails.JobRate / invoiceDetails.TotalMinutes) * invoiceDetails.TotalRunningMinutes;
                    var currencyDetails = _dbContext.CurrencyDetails.AsQueryable().Where(x => x.CurrencyCode == invoiceDetails.CurrencyCode).FirstOrDefault();
                    long? invoiceId = 0;
                    InvoiceGeneration obj = new InvoiceGeneration();
                    obj.IsDeleted = false;
                    obj.CurrencyId = currencyDetails.CurrencyId;
                    obj.JobPrice = Convert.ToInt32(invoiceDetails.JobRate);
                    obj.PlayoutMinutes = invoiceDetails.TotalRunningMinutes;
                    obj.TotalMinutes = invoiceDetails.TotalMinutes;
                    obj.TotalPrice = invoiceDetails.FinalPrice;
                    obj.JobId = request.jobId;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;
                    await _dbContext.InvoiceGeneration.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    invoiceId = obj.InvoiceId;

                    invoiceDetails = new InvoiceModel
                    {
                        InvoiceId = invoiceId,
                        ClientName = invoiceDetails.ClientName,
                        CurrencyCode = currencyDetails.CurrencyCode,
                        EndDate = invoiceDetails.EndDate,
                        FinalPrice = invoiceDetails.FinalPrice,
                        JobId = request.jobId,
                        JobName = invoiceDetails.JobName,
                        JobRate = invoiceDetails.JobRate,
                        TotalMinutes = invoiceDetails.TotalMinutes,
                        TotalRunningMinutes = invoiceDetails.TotalRunningMinutes
                    };
                    response.StatusCode = StaticResource.successStatusCode;
                    response.data.invoiceDetails = invoiceDetails;
                    response.Message = "Success";
                }
                else
                {
                    response.Message = "Schedule does not exist";
                    response.StatusCode = StaticResource.notFoundCode;
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
