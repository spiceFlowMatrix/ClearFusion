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
    public class GetPolicyByIdQueryHandler : IRequestHandler<GetPolicyByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetPolicyByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetPolicyByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var policyList = await _dbContext.PolicyDetails
                                       .Where(v => v.IsDeleted == false && v.PolicyId == request.PolicyId).Select(x => new PolicyModel
                                       {
                                           PolicyId = x.PolicyId,
                                           PolicyCode = x.PolicyCode,
                                           PolicyName = x.PolicyName,
                                           Description = x.Description,
                                           MediumName = x.Mediums.MediumName,
                                           MediumId = x.MediumId,
                                           ProducerId = x.ProducerId,
                                           ProducerName = x.Producers.ProducerName,
                                           MediaCategoryId = x.MediaCategoryId,
                                           MediaCategoryName = x.MediaCategories.CategoryName,
                                           LanguageId = x.LanguageId,
                                           LanguageName = x.Languages.LanguageName
                                       }).AsNoTracking().FirstOrDefaultAsync();
                //response.data.TotalCount = totalCount;
                response.data.policyDetailsById = policyList;
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
