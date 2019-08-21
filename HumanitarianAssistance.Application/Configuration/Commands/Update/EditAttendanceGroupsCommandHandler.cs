using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditAttendanceGroupsCommandHandler : IRequestHandler<EditAttendanceGroupsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditAttendanceGroupsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditAttendanceGroupsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    List<AttendanceGroupMaster> attendanceGroupMasterList = await _dbContext.AttendanceGroupMaster.Where(x => x.IsDeleted == false && x.Name.ToLower().Trim() == request.Name.ToLower().Trim()).ToListAsync();

                    bool isRecordWithSameNameExists = attendanceGroupMasterList.Any(x => x.AttendanceGroupId != request.Id);

                    if (isRecordWithSameNameExists)
                    {
                        throw new Exception($"Attendance Group with Name '{request.Name}' already exists");
                    }

                    AttendanceGroupMaster attendanceGroupMaster = attendanceGroupMasterList.FirstOrDefault(x => x.IsDeleted == false && x.AttendanceGroupId == request.Id);

                    if (attendanceGroupMaster != null)
                    {
                        attendanceGroupMaster.Description = request.Description;
                        attendanceGroupMaster.Name = request.Name;
                        attendanceGroupMaster.ModifiedById = request.ModifiedById;
                        attendanceGroupMaster.ModifiedDate = request.ModifiedDate;
                        
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
