using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RejectEmployeeLeaveCommandHandler: IRequestHandler<RejectEmployeeLeaveCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public RejectEmployeeLeaveCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(RejectEmployeeLeaveCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                foreach (var item in request.AppliedLeave)
                {
                    var existleaverecord = await _dbContext.EmployeeApplyLeave.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ApplyLeaveId == item.ApplyLeaveId);
                    if (existleaverecord != null)
                    {
                        existleaverecord.ApplyLeaveStatusId = (int)ApplyLeaveStatus.Reject;
                        existleaverecord.ModifiedById = request.ModifiedById;
                        existleaverecord.ModifiedDate = DateTime.UtcNow;
                        existleaverecord.IsDeleted = false;
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
                    }
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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