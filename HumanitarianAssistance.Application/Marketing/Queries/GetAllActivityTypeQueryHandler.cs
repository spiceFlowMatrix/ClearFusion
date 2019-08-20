using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Domain.Entities.Marketing;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
        public class GetAllActivityTypeQueryHandler : IRequestHandler<GetAllActivityTypeQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetAllActivityTypeQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ApiResponse> Handle(GetAllActivityTypeQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                ICollection<ActivityType> activityTypes = await _dbContext.ActivityTypes.AsNoTracking().AsQueryable().Where(x => x.IsDeleted == false).ToListAsync();
                response.data.ActivityTypes = activityTypes;
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
