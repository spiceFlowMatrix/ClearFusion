using System;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Domain.Entities.ActionLog;
using HumanitarianAssistance.Persistence;

namespace HumanitarianAssistance.Application.CommonServices {
    public class ActionLogServices : IActionLogService {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public ActionLogServices (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<bool> AuditLog (AuditLogModel model) {
            bool isAuditLogged = false;
            try {

                AuditLog auditDetails = new AuditLog () {
                    TypeOfEntity = model.TypeOfEntity,
                    EntityId = model.EntityId,
                    ActionTypeId = model.ActionTypeId,
                    ActionDescription = model.ActionDescription
                };
                await _dbContext.AuditLog.AddAsync(auditDetails);
                await _dbContext.SaveChangesAsync();
                isAuditLogged = true;
                return isAuditLogged;
            } catch (Exception ex) {
                Console.WriteLine (ex.Message);
                return isAuditLogged;
            }
        }
    }
}