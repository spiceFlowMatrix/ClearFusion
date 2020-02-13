using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAttendanceGroupDetailByIdQueryHandler: IRequestHandler<GetAttendanceGroupDetailByIdQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetAttendanceGroupDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAttendanceGroupDetailByIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            try
            {
               var attendanceGroup = await _dbContext.AttendanceGroupMaster.Where(x=>x.IsDeleted == false && x.AttendanceGroupId == request.AttendanceGroupId)
                                        .Select(x=> new {
                                            AttendanceGroupId = x.AttendanceGroupId,
                                            Name = x.Name,
                                            Description = x.Description,
                                            CreatedBy = (x.CreatedById != null) ? _dbContext.UserDetails.Where(y=>y.IsDeleted == false && y.AspNetUserId == x.CreatedById).Select(y=> (y.FirstName +' '+ y.LastName)).FirstOrDefault(): "",
                                            CreatedDate = x.CreatedDate,
                                            ModifiedBy = (x.ModifiedById != null) ? _dbContext.UserDetails.Where(y=>y.IsDeleted == false && y.AspNetUserId == x.ModifiedById).Select(y=> (y.FirstName +' '+ y.LastName)).FirstOrDefault(): "",
                                            ModifiedDate = x.ModifiedDate
                                        }).FirstOrDefaultAsync();
                if(attendanceGroup == null)
                {
                    throw new Exception("Attendance Group not Found!");
                }
                response.Add("AttendanceGroupDetail", attendanceGroup);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}