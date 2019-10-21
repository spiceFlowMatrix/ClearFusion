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
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetOpportunityControlListQueryHandler : IRequestHandler<GetOpportunityControlListQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetOpportunityControlListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetOpportunityControlListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var opportunityList = await _dbContext.ProjectOpportunityControl
                                                               .Where(x => x.IsDeleted == false && x.ProjectId == request.projectId)
                                                               .Select(x => new OpportunityControlViewModel
                                                               {
                                                                   Id = x.Id,
                                                                   ProjectId = x.ProjectId,
                                                                   RoleId = x.RoleId,
                                                                   UserId = x.UserID,
                                                                   DateAdded = x.CreatedDate ?? DateTime.UtcNow
                                                               })
                                                               .ToListAsync();

                response.data.OpportunityControlList = opportunityList;
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
