using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEmployeeLeaveCommandHandler : IRequestHandler<AddEmployeeLeaveCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly  IMapper _mapper;

        public AddEmployeeLeaveCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddEmployeeLeaveCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            var financialYear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsDefault == true);
            
            if (financialYear != null)
            {
                try
                {
                    foreach (var item in request.AssignEmployeeLeaveList )
                    {
                        var assignleavelist = await _dbContext.AssignLeaveToEmployee.FirstOrDefaultAsync(x => x.IsDeleted == false && x.FinancialYearId == financialYear.FinancialYearId && x.LeaveReasonId == item.LeaveReasonId && x.EmployeeId == item.EmployeeId);

                        if (assignleavelist == null)
                        {
                            AssignLeaveToEmployee obj = _mapper.Map<AssignLeaveToEmployee>(item);

                            obj.FinancialYearId = financialYear.FinancialYearId;
                            obj.IsDeleted = false;

                            await _dbContext.AssignLeaveToEmployee.AddAsync(obj);
                            await _dbContext.SaveChangesAsync();
                            
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Success";
                        }
                        else
                        {
                            response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                            response.Message = "Leave Reason Type already exist for this financial year.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }
            }

            return response;
        }
    }
}