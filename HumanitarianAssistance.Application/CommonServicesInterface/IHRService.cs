using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IHRService
    {
         Task<bool> AddEmployeePayrollDetails(int? EmployeeId);
    }
}