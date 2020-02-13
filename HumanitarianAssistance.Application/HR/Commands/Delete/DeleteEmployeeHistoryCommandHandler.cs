using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEmployeeHistoryCommandHandler : IRequestHandler<DeleteEmployeeHistoryCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IActionLogService _actionLog;
        public DeleteEmployeeHistoryCommandHandler(HumanitarianAssistanceDbContext dbContext,IActionLogService actionLog)
        {
            _dbContext = dbContext;
            _actionLog = actionLog;
        }
        public async Task<ApiResponse> Handle(DeleteEmployeeHistoryCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var historyinfo = await _dbContext.EmployeeHistoryDetail.FirstOrDefaultAsync(x => x.HistoryID == request.HistoryId && x.IsDeleted == false);
                if (historyinfo != null)
                {
                    historyinfo.IsDeleted = true;
                    historyinfo.ModifiedById = request.ModifiedById;
                    historyinfo.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                    
                    AuditLogModel logs = new AuditLogModel () {
                    EmployeeId = (int) historyinfo.EmployeeID,
                    TypeOfEntity = (int) TypeOfEntity.History,
                    EntityId = null,
                    ActionTypeId = (int) ActionType.Delete,
                    ActionDescription = (TypeOfEntity.History).GetDescription (),
                };
                bool isLoged = await _actionLog.AuditLog (logs);
                }
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