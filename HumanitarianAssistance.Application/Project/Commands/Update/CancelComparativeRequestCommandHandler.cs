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
    public class CancelComparativeRequestCommandHandler : IRequestHandler<CancelComparativeRequestCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public CancelComparativeRequestCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(CancelComparativeRequestCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var logisticrequest = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted==false && x.LogisticRequestsId==request.RequestId);
                if(logisticrequest==null){
                    throw new Exception("No request found!");
                }
                logisticrequest.Status=(int)LogisticRequestStatus.Cancelled;
                logisticrequest.ComparativeStatus=(int)LogisticComparativeStatus.Cancelled;
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
