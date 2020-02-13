using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEmployeeRelativeInformationCommandHandler : IRequestHandler<DeleteEmployeeRelativeInformationCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IActionLogService _actionLog;
        public DeleteEmployeeRelativeInformationCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper,IActionLogService actionLog)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _actionLog = actionLog;
        }

        public async Task<ApiResponse> Handle(DeleteEmployeeRelativeInformationCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existRecord = await _dbContext.EmployeeRelativeInfo.FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                 x.EmployeeRelativeInfoId == request.EmployeeRelativeInfoId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = request.ModifiedById;
                    existRecord.ModifiedDate = DateTime.Now;
                    //_mapper.Map(request, existRecord);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";

                    AuditLogModel logs = new AuditLogModel () {
                    EmployeeId = (int) existRecord.EmployeeID,
                    TypeOfEntity = (int) TypeOfEntity.History,
                    EntityId = null,
                    ActionTypeId = (int) ActionType.Delete,
                    ActionDescription = (TypeOfEntity.History).GetDescription (),
                };
                bool isLoged = await _actionLog.AuditLog (logs);
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
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