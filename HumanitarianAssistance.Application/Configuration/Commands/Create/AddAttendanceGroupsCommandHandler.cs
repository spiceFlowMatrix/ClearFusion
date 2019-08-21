using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddAttendanceGroupsCommandHandler : IRequestHandler<AddAttendanceGroupsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddAttendanceGroupsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddAttendanceGroupsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {

                    AttendanceGroupMaster recordExists = await _dbContext.AttendanceGroupMaster.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Name.ToLower().Trim() == request.Name.ToLower().Trim());

                    if (recordExists != null)
                    {
                        throw new Exception($"Attendance Group with Name '{request.Name}' already exists ");
                    }

                    AttendanceGroupMaster attendanceGroupMaster = new AttendanceGroupMaster
                    {
                        CreatedDate = request.CreatedDate,
                        CreatedById = request.CreatedById,
                        Description = request.Description,
                        IsDeleted = false,
                        Name = request.Name
                    };

                    await _dbContext.AttendanceGroupMaster.AddAsync(attendanceGroupMaster);
                    await _dbContext.SaveChangesAsync();
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
