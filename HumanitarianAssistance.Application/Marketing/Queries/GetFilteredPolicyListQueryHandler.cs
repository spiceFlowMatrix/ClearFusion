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
    class GetFilteredPolicyListQueryHandler : IRequestHandler<GetFilteredPolicyListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredPolicyListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetFilteredPolicyListQuery request, CancellationToken cancellationToken)
        {
            string policyIdValue = null;
            string policyNameValue = null;
            string mediumValue = null;

            if (!string.IsNullOrEmpty(request.Value))
            {
                policyIdValue = request.PolicyId ? request.Value.ToLower().Trim() : null;
                policyNameValue = request.PolicyName ? request.Value.ToLower().Trim() : null;
                mediumValue = request.Medium ? request.Value.ToLower().Trim() : null;
            }

            ApiResponse response = new ApiResponse();
            try
            {
                var policyList = await _dbContext.PolicyDetails
                                    .Where(v => v.IsDeleted == false &&
                                          (!string.IsNullOrEmpty(request.Value) ?
                                             (
                                               v.PolicyId.ToString().Trim().ToLower().Contains(policyIdValue) ||
                                               v.PolicyName.Trim().ToLower().Contains(policyNameValue) ||
                                               v.Mediums.MediumName.Trim().ToLower().Contains(mediumValue)
                                              ) : true
                                           )
                                     )
                                    .OrderByDescending(x => x.CreatedDate)
                                    .Select(x => new PolicyModel
                                    {
                                        PolicyId = x.PolicyId,
                                        PolicyName = x.PolicyName,
                                        MediumName = x.Mediums.MediumName,
                                        MediumId = x.MediumId,
                                        PolicyCode = x.PolicyCode
                                    })
                                    .AsNoTracking()
                                    .ToListAsync();
                // response.data.jobListTotalCount = voucherList.Count();
                response.data.PolicyFilteredList = policyList;
                response.StatusCode = StaticResource.successStatusCode;
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
