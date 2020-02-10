using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IActionLogService
    {
        Task<bool> AuditLog(AuditLogModel AuditLogModel);
    }
}