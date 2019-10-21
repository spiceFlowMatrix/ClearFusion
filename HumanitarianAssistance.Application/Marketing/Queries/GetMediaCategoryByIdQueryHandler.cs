using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Domain.Entities.Marketing;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetMediaCategoryByIdQueryHandler : IRequestHandler<GetMediaCategoryByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetMediaCategoryByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetMediaCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                MediaCategory obj = await _dbContext.MediaCategories.AsNoTracking().AsQueryable().Where(x => x.MediaCategoryId == request.MediaCategoryId && x.IsDeleted == false).SingleAsync();
                response.data.mediaCategoryById = obj;
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
