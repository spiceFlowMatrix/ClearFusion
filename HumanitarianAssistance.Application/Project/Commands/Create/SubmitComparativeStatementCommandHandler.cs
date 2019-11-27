using System;
using HumanitarianAssistance.Common.Enums;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class SubmitComparativeStatementCommandHandler : IRequestHandler<SubmitComparativeStatementCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProjectServices _iProjectServices;

        public SubmitComparativeStatementCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IProjectServices iProjectServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(SubmitComparativeStatementCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ComparativeStatementSubmission obj = new ComparativeStatementSubmission{
                    LogisticRequestsId = request.RequestId,
                    SupplierIds = request.SupplierIds,
                    Description =  request.Description,
                    CreatedDate = request.CreatedDate,
                    CreatedById = request.CreatedById
                };
                await _dbContext.ComparativeStatementSubmission.AddAsync(obj);
                var logisticRequest = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted==false && x.LogisticRequestsId==request.RequestId);
                if(logisticRequest == null) {
                    throw new Exception("Request not Found!");
                }
                logisticRequest.ComparativeStatus = (int)LogisticComparativeStatus.StatementSubmitted;
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch(Exception ex) 
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}