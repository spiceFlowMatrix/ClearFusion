using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetPolicyPaginatedListQueryHandler : IRequestHandler<GetPolicyPaginatedListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetPolicyPaginatedListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetPolicyPaginatedListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                int totalCount = await _dbContext.PolicyDetails
                                       .Where(v => v.IsDeleted == false)
                                      .AsNoTracking()
                                      .CountAsync();

                var policyList = await _dbContext.PolicyDetails
                                       .Where(v => v.IsDeleted == false).Select(x => new PolicyModel
                                       {
                                           PolicyId = x.PolicyId,
                                           PolicyCode = x.PolicyCode,
                                           PolicyName = x.PolicyName,
                                           MediumName = x.Mediums.MediumName,
                                           MediumId = x.MediumId
                                       }).Skip((request.pageSize * request.pageIndex)).Take(request.pageSize).OrderByDescending(x => x.CreatedDate).AsNoTracking()
                                    .ToListAsync();
                response.data.TotalCount = totalCount;
                response.data.policyFilterList = policyList;
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
