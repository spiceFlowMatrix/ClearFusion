using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IActionLogService
    {
        Task<bool> AuditLog(AuditLogModel AuditLogModel);
        void HRMAuditLogService(AuditLogModel model, dynamic request);
        dynamic ListHRMLogEntries();
    }
}