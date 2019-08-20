using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class ApproveInvoiceCommandHandler : IRequestHandler<ApproveInvoiceCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public ApproveInvoiceCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(ApproveInvoiceCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var invoiceGeneration = _dbContext.InvoiceGeneration.AsQueryable().Where(x => x.JobId == request.jobId && x.IsDeleted == false).FirstOrDefault();
                if (invoiceGeneration != null)
                {
                    var invoiceDetails = _dbContext.InvoiceApproval.AsQueryable().Where(x => x.JobId == request.jobId).FirstOrDefault();
                    if (invoiceDetails == null)
                    {
                        InvoiceApproval obj = new InvoiceApproval();
                        obj.IsDeleted = false;
                        obj.JobId = request.jobId;
                        obj.IsInvoiceApproved = true;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        await _dbContext.InvoiceApproval.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        var existRecord = await _dbContext.InvoiceApproval.FirstOrDefaultAsync(x => x.IsDeleted == false && x.JobId == request.jobId);
                        if (existRecord != null)
                        {
                            existRecord.IsInvoiceApproved = true;
                            existRecord.IsDeleted = false;
                            existRecord.ModifiedById = request.ModifiedById;
                            existRecord.ModifiedDate = request.ModifiedDate;
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    List<ScheduleDetails> scheduleDetails = await _dbContext.ScheduleDetails.Where(x => x.IsDeleted == false && x.JobId == request.jobId).ToListAsync();

                    scheduleDetails.ForEach(x => x.IsActive = false);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Invoice Approved successfully";
                }
                else
                {
                    response.StatusCode = StaticResource.notFoundCode;
                    response.Message = "Invoice Not yet generated";
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
