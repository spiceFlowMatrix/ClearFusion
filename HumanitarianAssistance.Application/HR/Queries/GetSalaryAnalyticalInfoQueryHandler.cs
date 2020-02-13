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
    public class GetSalaryAnalyticalInfoQueryHandler : IRequestHandler<GetSalaryAnalyticalInfoQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetSalaryAnalyticalInfoQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetSalaryAnalyticalInfoQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeSalaryAnalyticalInfo = await _dbContext.EmployeeSalaryAnalyticalInfo.Include(x => x.ProjectBudgetLine).Where(x => x.IsDeleted == false &&
                                                                                                                x.EmployeeID == request.EmployeeId)
                                                                                                         .ToListAsync();

                if (employeeSalaryAnalyticalInfo.Any())
                {

                    response.data.EmployeeSalaryAnalyticalInfoList = (from esa in employeeSalaryAnalyticalInfo
                                                                      join acc in _dbContext.ChartOfAccountNew on esa.AccountNo equals acc.ChartOfAccountNewId
                                                                      join prj in _dbContext.ProjectDetail on esa.ProjectId equals prj.ProjectId
                                                                      select new EmployeeSalaryAnalyticalInfoModel
                                                                      {
                                                                          EmployeeSalaryAnalyticalInfoId = esa.EmployeeSalaryAnalyticalInfoId,
                                                                          AccountCode = esa.AccountNo,
                                                                          BudgetLineId = esa.BudgetlineId,
                                                                          EmployeeID = esa.EmployeeID,
                                                                          ProjectId = esa.ProjectId,
                                                                          SalaryPercentage = esa.SalaryPercentage,
                                                                          BudgetLineName = esa.ProjectBudgetLine.BudgetName,
                                                                          ProjectName = prj.ProjectCode + '-' + prj.ProjectName,
                                                                          AccountName = acc.ChartOfAccountNewCode + '-' + acc.AccountName

                                                                      }).ToList();


                    // response.data.EmployeeSalaryAnalyticalInfoList = employeeSalaryAnalyticalInfo.Select(x => new EmployeeSalaryAnalyticalInfoModel
                    // {
                    //     EmployeeSalaryAnalyticalInfoId = x.EmployeeSalaryAnalyticalInfoId,
                    //     AccountCode = x.AccountNo,
                    //     BudgetLineId = x.BudgetlineId,
                    //     EmployeeID = x.EmployeeID,
                    //     ProjectId = x.ProjectId,
                    //     SalaryPercentage = x.SalaryPercentage,
                    //     BudgetLineName = x.ProjectBudgetLine.BudgetName,
                    //     ProjectName = x.ProjectDetail.ProjectName


                    // }).ToList();
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