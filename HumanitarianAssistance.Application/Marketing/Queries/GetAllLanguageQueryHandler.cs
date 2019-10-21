using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HumanitarianAssistance.Domain.Entities;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
        public class GetAllLanguageQueryHandler : IRequestHandler<GetAllLanguageQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetAllLanguageQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ApiResponse> Handle(GetAllLanguageQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                ICollection<LanguageDetail> Languages = await _dbContext.LanguageDetail.AsNoTracking().AsQueryable().Where(x => x.IsDeleted == false).ToListAsync();
                response.data.Languages = Languages;
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
