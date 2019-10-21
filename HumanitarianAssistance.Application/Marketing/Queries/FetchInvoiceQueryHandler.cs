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
    public class FetchInvoiceQueryHandler : IRequestHandler<FetchInvoiceQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public FetchInvoiceQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(FetchInvoiceQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                InvoiceModel model = new InvoiceModel();
                var invoiceDetails = _dbContext.InvoiceGeneration.AsNoTracking().AsQueryable().Where(x => x.JobId == request.jobId && x.IsDeleted == false).OrderByDescending(x => x.InvoiceId).FirstOrDefault();
                var invoiceApproval = await _dbContext.InvoiceApproval.AsNoTracking().AsQueryable().FirstOrDefaultAsync(x => x.JobId == request.jobId && x.IsDeleted == false);
                var JobList = await (from j in _dbContext.JobDetails.AsNoTracking().AsQueryable()
                                     join jp in _dbContext.JobPriceDetails on j.JobId equals jp.JobId
                                     join cd in _dbContext.ContractDetails on j.ContractId equals cd.ContractId
                                     join cur in _dbContext.CurrencyDetails on cd.CurrencyId equals cur.CurrencyId
                                     where !j.IsDeleted && !jp.IsDeleted && j.JobId == request.jobId
                                     select (new JobPriceModel
                                     {
                                         EndDate = j.EndDate,
                                         ClientName = cd.ClientName,
                                         CurrencyCode = cur.CurrencyCode
                                     })).FirstOrDefaultAsync();

                if (invoiceDetails != null)
                {
                    var jobDetails = await _dbContext.JobDetails.AsNoTracking().AsQueryable().FirstOrDefaultAsync(x => x.JobId == request.jobId & x.IsDeleted == false);
                    model.JobName = jobDetails.JobName;
                    model.JobId = request.jobId;
                    model.ClientName = JobList.ClientName;
                    model.CurrencyCode = JobList.CurrencyCode;
                    model.EndDate = JobList.EndDate;
                    model.FinalPrice = invoiceDetails.TotalPrice;
                    model.InvoiceId = invoiceDetails.InvoiceId;
                    model.JobRate = Convert.ToInt32(invoiceDetails.JobPrice);
                    model.TotalRunningMinutes = invoiceDetails.PlayoutMinutes;
                    model.TotalMinutes = invoiceDetails.TotalMinutes;

                    response.StatusCode = StaticResource.successStatusCode;
                    response.data.invoiceDetails = model;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.notFoundCode;
                    response.Message = "Data not found";
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
