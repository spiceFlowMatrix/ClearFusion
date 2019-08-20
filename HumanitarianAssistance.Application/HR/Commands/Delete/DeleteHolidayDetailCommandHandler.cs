using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteHolidayDetailCommandHandler: IRequestHandler<DeleteHolidayDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public DeleteHolidayDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteHolidayDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var existrecord = await _dbContext.HolidayDetails.FirstOrDefaultAsync(x => x.HolidayId == request.HolidayId);
                
                if (existrecord != null)
                {
                    existrecord.IsDeleted = true;
                    existrecord.ModifiedById = request.ModifiedById;
                    existrecord.ModifiedDate = request.ModifiedDate;

                    _dbContext.HolidayDetails.Update(existrecord);
                    await _dbContext.SaveChangesAsync();

                    var existempattendance = await _dbContext.EmployeeAttendance.Where(x => x.HolidayId == request.HolidayId).ToListAsync();
                    
                    if (existempattendance.Count > 0)
                    {
                        List<EmployeeAttendance> empattendancelist = new List<EmployeeAttendance>();
                        
                        foreach (var list in existempattendance)
                        {
                            EmployeeAttendance empattendance = new EmployeeAttendance();
                            empattendance.EmployeeId = list.EmployeeId;
                            empattendance.AttendanceId = list.AttendanceId;
                            empattendance.IsDeleted = false;
                            empattendance.ModifiedById = request.ModifiedById;
                            empattendance.ModifiedDate = request.ModifiedDate;
                            empattendancelist.Add(empattendance);
                        }
                        _dbContext.EmployeeAttendance.UpdateRange(empattendancelist);
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