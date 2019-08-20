using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetPolicyListQueryHandler : IRequestHandler<GetPolicyListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetPolicyListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetPolicyListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                int count = await _dbContext.PolicyDetails.CountAsync(x => x.IsDeleted == false);
                var policyDetail = await (from j in _dbContext.PolicyDetails
                                          join jp in _dbContext.LanguageDetail on j.LanguageId equals jp.LanguageId
                                          join me in _dbContext.Mediums on j.MediumId equals me.MediumId
                                          join mc in _dbContext.MediaCategories on j.MediaCategoryId equals mc.MediaCategoryId
                                          where !j.IsDeleted && !jp.IsDeleted && !me.IsDeleted
                                          && !mc.IsDeleted
                                          select (new PolicyModel
                                          {
                                              PolicyId = j.PolicyId,
                                              PolicyName = j.PolicyName,
                                              PolicyCode = j.PolicyCode,
                                              Description = j.Description,
                                              LanguageId = jp.LanguageId,
                                              LanguageName = jp.LanguageName,
                                              MediumId = me.MediumId,
                                              MediumName = me.MediumName,
                                              MediaCategoryId = mc.MediaCategoryId,
                                              MediaCategoryName = mc.CategoryName
                                          })).Take(10).Skip(0).OrderByDescending(x => x.CreatedDate).ToListAsync();

                response.data.policyList = policyDetail;
                response.data.TotalCount = count;
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
