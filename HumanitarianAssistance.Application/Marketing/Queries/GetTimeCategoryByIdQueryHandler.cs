using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Domain.Entities.Marketing;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetTimeCategoryByIdQueryHandler : IRequestHandler<GetTimeCategoryByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetTimeCategoryByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetTimeCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                TimeCategory obj = await _dbContext.TimeCategories.AsNoTracking().AsQueryable().Where(x => x.TimeCategoryId == request.TimeCategoryId && x.IsDeleted == false).SingleAsync();
                response.data.timeCatergoryById = obj;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
