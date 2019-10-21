using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAttendanceGroupsQueryHandler : IRequestHandler<GetAttendanceGroupsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAttendanceGroupsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAttendanceGroupsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response.data.AttendanceGroupMasterList = await _dbContext.AttendanceGroupMaster
                                                                    .Where(x => x.IsDeleted == false)
                                                                    .OrderByDescending(x => x.CreatedDate)
                                                                    .Select(x => new AttendanceGroupMasterModel
                                                                    {
                                                                        Description = x.Description,
                                                                        Id = x.AttendanceGroupId,
                                                                        Name = x.Name
                                                                    }).ToListAsync();

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
