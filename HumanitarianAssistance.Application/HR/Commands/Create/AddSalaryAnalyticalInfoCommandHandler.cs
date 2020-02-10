using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create {
    public class AddSalaryAnalyticalInfoCommandHandler : IRequestHandler<AddSalaryAnalyticalInfoCommand, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddSalaryAnalyticalInfoCommandHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle (AddSalaryAnalyticalInfoCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                if (request.EditAnalyticalInfo.Count > 0) {
                    foreach (var item in request.EditAnalyticalInfo) {
                        var data = await _dbContext.EmployeeSalaryAnalyticalInfo.Where (x => x.EmployeeSalaryAnalyticalInfoId == item.EmployeeSalaryAnalyticalInfoId && x.IsDeleted == false).FirstOrDefaultAsync ();
                     //   data.AccountNo = item.AccountCode;
                        data.SalaryPercentage = item.SalaryPercentage;                        
                    }
                    await _dbContext.SaveChangesAsync();
                }
                EmployeeSalaryAnalyticalInfo obj = new EmployeeSalaryAnalyticalInfo ();

                obj.EmployeeSalaryAnalyticalInfoId = 0;
                obj.EmployeeID = request.EmployeeID;
                obj.ProjectId = request.ProjectId;
                obj.BudgetlineId = request.BudgetLineId;
                // obj.AccountNo = request.AccountCode;
                obj.SalaryPercentage = request.SalaryPercentage;

                obj.IsDeleted = false;
                obj.CreatedById = request.CreatedById;
                obj.CreatedDate = DateTime.Now;
                await _dbContext.EmployeeSalaryAnalyticalInfo.AddAsync (obj);
                await _dbContext.SaveChangesAsync ();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}