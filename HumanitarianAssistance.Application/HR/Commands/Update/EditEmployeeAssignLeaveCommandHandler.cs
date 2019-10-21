using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeAssignLeaveCommandHandler: IRequestHandler<EditEmployeeAssignLeaveCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditEmployeeAssignLeaveCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditEmployeeAssignLeaveCommand request, CancellationToken cancellationToken)
        {
             ApiResponse response = new ApiResponse();

            try
            {
                var existrecord = await _dbContext.AssignLeaveToEmployee.FirstOrDefaultAsync(x => x.IsDeleted == false && x.LeaveId == request.LeaveId);
                if (existrecord != null)
                {
                    existrecord.AssignUnit = request.AssignUnit;
                    existrecord.ModifiedById = request.ModifiedById;
                    existrecord.ModifiedDate = request.ModifiedDate;
                    existrecord.IsDeleted = request.IsDeleted;

                    _dbContext.AssignLeaveToEmployee.Update(existrecord);
                    await _dbContext.SaveChangesAsync();
                    
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}