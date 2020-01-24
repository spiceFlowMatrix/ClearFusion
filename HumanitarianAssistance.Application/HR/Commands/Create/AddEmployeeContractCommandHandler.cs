using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create {
    public class AddEmployeeContractCommandHandler : IRequestHandler<AddEmployeeContractCommand, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddEmployeeContractCommandHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle (AddEmployeeContractCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();

            try {
                if (request != null) {
                    EmployeeContract obj = new EmployeeContract () {
                    EmployeeId = request.EmployeeId,
                    EmployeeCode = request.EmployeeCode,
                    Designation = request.Designation,
                    ContractStartDate = request.ContractStartDate,
                    ContractEndDate = request.ContractEndDate,
                    DurationOfContract = request.DurationOfContract,
                    Salary = request.Salary,
                    Grade = request.Grade,
                    DutyStation = request.DutyStation,
                    Country = request.Country,
                    Province = request.Province,
                    Project = request.Project,
                    Job = request.Job,
                    BudgetLine = request.BudgetLine,
                    WorkTime = request.WorkTime,
                    WorkDayHours = request.WorkDayHours,
                    IsDeleted = false
                    };
                    await _dbContext.EmployeeContract.AddAsync (obj);
                    await _dbContext.SaveChangesAsync ();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                } else {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model Not Valid";
                }
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}