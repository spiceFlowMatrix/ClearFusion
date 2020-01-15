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
namespace HumanitarianAssistance.Application.HR.Queries {
    public class GetAllEmployeeSalaryBudgetsQueryHandler : IRequestHandler<GetAllEmployeeSalaryBudgetsQuery, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeSalaryBudgetsQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle (GetAllEmployeeSalaryBudgetsQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {

                var employeeHistoryRecord = await (from u in _dbContext.EmployeeSalaryBudget.Where (x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId) 
                join c in _dbContext.CurrencyDetails on u.CurrencyId equals c.CurrencyId
                into cd from c in cd.DefaultIfEmpty () 
                select new EmployeeSalaryBudgetModel {
                    EmployeeSalaryBudgetId = u.EmployeeSalaryBudgetId,
                        EmployeeID = u.EmployeeID,
                        BudgetDisbursed = u.BudgetDisbursed,
                        CurrencyId = u.CurrencyId,
                        SalaryBudget = u.SalaryBudget,
                        Year = u.Year,
                        CurrencyName = c.CurrencyName
                }).ToListAsync ();
                if (employeeHistoryRecord.Count > 0) {
                    response.data.EmployeeSalaryBudgetList = employeeHistoryRecord;

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}