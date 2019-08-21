using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEmployeeAppliedLeaveCommandHandler: IRequestHandler<DeleteEmployeeAppliedLeaveCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteEmployeeAppliedLeaveCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteEmployeeAppliedLeaveCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var existleaverecord = await _dbContext.EmployeeApplyLeave.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ApplyLeaveId == request.AppliedLeaveId);
                
                if (existleaverecord != null)
                {
                    existleaverecord.ModifiedById = request.ModifiedById;
                    existleaverecord.ModifiedDate = DateTime.UtcNow;
                    existleaverecord.IsDeleted = true;
                    _dbContext.EmployeeApplyLeave.Update(existleaverecord);
                    await _dbContext.SaveChangesAsync();

                    var existassignempleave = await _dbContext.AssignLeaveToEmployee.FirstOrDefaultAsync(x => x.IsDeleted == false && x.LeaveReasonId == existleaverecord.LeaveReasonId && x.EmployeeId == existleaverecord.EmployeeId);
                    
                    if (existassignempleave != null)
                    {

                        existassignempleave.UsedLeaveUnit = existassignempleave.UsedLeaveUnit - 1;
                        existassignempleave.ModifiedById = request.ModifiedById;
                        existassignempleave.ModifiedDate = DateTime.UtcNow;
                        existassignempleave.IsDeleted = false;
                        _dbContext.AssignLeaveToEmployee.Update(existassignempleave);
                        await _dbContext.SaveChangesAsync();
                    }
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