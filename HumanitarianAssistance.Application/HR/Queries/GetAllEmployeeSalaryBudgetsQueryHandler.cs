using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace HumanitarianAssistance.Application.HR.Queries
{
<<<<<<< HEAD
    public class GetAllEmployeeSalaryBudgetsQueryHandler : IRequestHandler<GetAllEmployeeSalaryBudgetsQuery, ApiResponse>
=======
    public class GetAllEmployeeSalaryBudgetsQueryHandler : IRequestHandler<GetAllEmployeeOtherSkillsQuery, ApiResponse>
>>>>>>> 283e4aad2a4f32a5e1f5bbe3a3e24be5174a685f
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeSalaryBudgetsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

<<<<<<< HEAD
        public async Task<ApiResponse> Handle(GetAllEmployeeSalaryBudgetsQuery request, CancellationToken cancellationToken)
=======
        public async Task<ApiResponse> Handle(GetAllEmployeeOtherSkillsQuery request, CancellationToken cancellationToken)
>>>>>>> 283e4aad2a4f32a5e1f5bbe3a3e24be5174a685f
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeHistoryRecord = await _dbContext.EmployeeSalaryBudget.Where(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeSalaryBudgetList = employeeHistoryRecord.Select(x => new EmployeeSalaryBudgetModel
                    {
                        EmployeeSalaryBudgetId = x.EmployeeSalaryBudgetId,
                        EmployeeID = x.EmployeeID,
                        BudgetDisbursed = x.BudgetDisbursed,
                        CurrencyId = x.CurrencyId,
                        SalaryBudget = x.SalaryBudget,
                        Year = x.Year
                    }).ToList();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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
