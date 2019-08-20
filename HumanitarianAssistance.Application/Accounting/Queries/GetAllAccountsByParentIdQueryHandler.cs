using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllAccountsByParentIdQueryHandler : IRequestHandler<GetAllAccountsByParentIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllAccountsByParentIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllAccountsByParentIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var mainLevelList = await _dbContext.ChartOfAccountNew
                                                    .Where(x => x.ChartOfAccountNewId != request.ParentId &&
                                                                x.ParentID == request.ParentId &&
                                                                x.IsDeleted == false)
                                                    .OrderBy(x => x.ChartOfAccountNewId)
                                                    .ToListAsync();

                response.data.AllAccountList = mainLevelList;
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