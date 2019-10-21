using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IHRService
    {
         Task<bool> AddEmployeePayrollDetails(int? EmployeeId);
         Task<ApiResponse> AddUser(UserModel request);
    }
}