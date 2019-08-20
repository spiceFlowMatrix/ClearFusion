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
using HumanitarianAssistance.Domain.Entities.Project;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
        public class GetHiringControlListQueryHandler : IRequestHandler<GetHiringControlListQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetHiringControlListQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetHiringControlListQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var hiringList = await _dbContext.ProjectHiringControl
                                                               .Where(x => x.IsDeleted == false && x.ProjectId == request.projectId)
                                                               .Select(x => new HiringControlViewModel
                                                               {
                                                                   Id = x.Id,
                                                                   ProjectId = x.ProjectId,
                                                                   RoleId = x.RoleId,
                                                                   UserId = x.UserID,
                                                                   DateAdded = x.CreatedDate ?? DateTime.UtcNow
                                                               })
                                                               .ToListAsync();

                response.data.HiringControlList = hiringList;
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
