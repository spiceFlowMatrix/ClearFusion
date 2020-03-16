using AutoMapper;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEmployeeHistoryCommandHandler : IRequestHandler<AddEmployeeHistoryCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IActionLogService _actionLog;
        public AddEmployeeHistoryCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IActionLogService actionLog)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _actionLog = actionLog;
        }
        public async Task<ApiResponse> Handle(AddEmployeeHistoryCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                EmployeeHistoryDetail obj = _mapper.Map<EmployeeHistoryDetail>(request);
                obj.IsDeleted = false;
                await _dbContext.EmployeeHistoryDetail.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                    AuditLogModel logs = new AuditLogModel () {
                        EmployeeId = (int)obj.EmployeeID,
                        //TypeOfEntity = (int) TypeOfEntity.History,
                        TypeOfEntityName = TypeOfEntity.History.ToString(),
                        PerformedBy = request.CreatedById,
                        // EntityId = null,
                        // ActionTypeId = (int) ActionType.Add,
                        // ActionTypeIdName = ActionType.Add.ToString(),
                        // ActionDescription = String.Format("Employee history added for EmployeeCode: {0}", "E"+(int)obj.EmployeeID),
                    };
                    // await _actionLog.AuditLog (logs);
                    _actionLog.HRMAuditLogService(logs,request);
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