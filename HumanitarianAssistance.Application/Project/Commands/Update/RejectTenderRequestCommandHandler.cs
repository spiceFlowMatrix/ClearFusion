using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class RejectTenderRequestCommandHandler : IRequestHandler<RejectTenderRequestCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public RejectTenderRequestCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(RejectTenderRequestCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var logisticreq = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.LogisticRequestsId == request.requestId);
                if(logisticreq == null) {
                    throw new Exception("Request doesn't Exist!");
                }
                logisticreq.TenderStatus = (int)LogisticTenderStatus.Cancelled;
                logisticreq.ModifiedById = request.ModifiedById;
                logisticreq.ModifiedDate = request.ModifiedDate;

                await _dbContext.SaveChangesAsync();
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
