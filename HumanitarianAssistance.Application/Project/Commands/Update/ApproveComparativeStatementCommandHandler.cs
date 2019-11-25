using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using HumanitarianAssistance.Common.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class ApproveComparativeStatementCommandHandler : IRequestHandler<ApproveComparativeStatementCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public ApproveComparativeStatementCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(ApproveComparativeStatementCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var logisticReq = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.LogisticRequestsId == request.requestId);
                if (logisticReq == null) {
                    throw new Exception("Request doesn't exists!");
                }
                logisticReq.Status = (int)LogisticRequestStatus.IssuePurchaseOrder;
                logisticReq.ComparativeStatus = (int)LogisticComparativeStatus.ApproveStatement;
                logisticReq.ModifiedById = request.ModifiedById;
                logisticReq.ModifiedDate = request.ModifiedDate;
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
