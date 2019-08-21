using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Project.Queries
{
        public class GetLogisticsControlListQueryHandler : IRequestHandler<GetLogisticsControlListQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetLogisticsControlListQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetLogisticsControlListQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var logisticsList = await _dbContext.ProjectLogisticsControl
                                                               .Where(x => x.IsDeleted == false && x.ProjectId == request.projectId)
                                                               .Select(x => new LogisticsControlViewModel
                                                               {
                                                                   Id = x.Id,
                                                                   ProjectId = x.ProjectId,
                                                                   RoleId = x.RoleId,
                                                                   UserId = x.UserID,
                                                                   DateAdded = x.CreatedDate ?? DateTime.UtcNow
                                                               })
                                                               .ToListAsync();

                response.data.LogisticsControlList = logisticsList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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
