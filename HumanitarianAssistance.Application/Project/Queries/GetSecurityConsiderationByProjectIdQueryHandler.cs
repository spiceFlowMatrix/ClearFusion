using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetSecurityConsiderationByProjectIdQueryHandler: IRequestHandler<GetSecurityConsiderationByProjectIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetSecurityConsiderationByProjectIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetSecurityConsiderationByProjectIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                List<long> SelectedSecurityList = await _dbContext.SecurityConsiderationMultiSelect.Where(x => x.ProjectId == request.ProjectId && x.IsDeleted == false).Select(x => x.SecurityConsiderationId).ToListAsync();
                response.data.SecurityConsiderationMultiSelectById = SelectedSecurityList;
                response.StatusCode = 200;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        
    }
}