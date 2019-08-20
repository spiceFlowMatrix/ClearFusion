using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Domain.Entities.Project;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCountryByProjectIdQueryHandler: IRequestHandler<GetCountryByProjectIdQuery, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetCountryByProjectIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetCountryByProjectIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                List<int?> SelectedCountryList = await _dbContext.CountryMultiSelectDetails
                                                                .Where(x => x.ProjectId == request.ProjectId
                                                                        && x.IsDeleted == false)
                                                                .Select(x => x.CountryId)
                                                                .ToListAsync();

                response.data.CountryMultiSelectById = SelectedCountryList;
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