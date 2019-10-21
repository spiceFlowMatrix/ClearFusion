using HumanitarianAssistance.Application.Infrastructure;
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

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllStatusAtTimeOfIssueQueryHandler : IRequestHandler<GetAllStatusAtTimeOfIssueQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllStatusAtTimeOfIssueQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllStatusAtTimeOfIssueQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var statusAtTimeOfIssueList = await _dbContext.StatusAtTimeOfIssue.Where(x=>x.IsDeleted==false).ToListAsync();

                response.data.StatusAtTimeOfIssueList = statusAtTimeOfIssueList.ToList();
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
