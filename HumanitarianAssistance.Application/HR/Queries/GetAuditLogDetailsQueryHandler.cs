using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries {

    public class GetAuditLogDetailsQueryHandler : IRequestHandler<GetAuditLogDetailsQuery, object> {
        private HumanitarianAssistanceDbContext _dbContext;
        private IActionLogService _logService;
        public GetAuditLogDetailsQueryHandler (HumanitarianAssistanceDbContext dbContext, IActionLogService logService) {
            _dbContext = dbContext;
            _logService = logService;
        }
        public async Task<object> Handle (GetAuditLogDetailsQuery request, CancellationToken cancellationToken) {
            Dictionary<string, object> result = new Dictionary<string, object> ();
            // var logs =_logService.ListHRMLogEntries();
            var query = _dbContext.AuditLog.OrderByDescending (x => x.CreatedById)
                .Where (x => x.IsDeleted == false && x.CreatedById == request.EmployeeId.ToString ())
                .Select (x => new {
                    EmployeeId = x.CreatedById,
                        AuditLogId = x.AuditLogId,
                        TypeOfEntity = ((TypeOfEntity)x.TypeOfEntity).ToString(),
                        EntityId = x.EntityId,
                        ActionType = ((ActionType)x.ActionTypeId).ToString(),
                        ActionDescription = x.ActionDescription
                }).AsQueryable ();

            long count = await query.CountAsync ();
            result.Add ("AuditLogs", query);
            result.Add ("RecordCount", count);
            // result.Add("logs", logs.ToList());
            return result;
        }
    }

}