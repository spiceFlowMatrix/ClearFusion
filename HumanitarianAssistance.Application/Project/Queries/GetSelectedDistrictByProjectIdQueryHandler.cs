using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetSelectedDistrictByProjectIdQueryHandler: IRequestHandler<GetSelectedDistrictByProjectIdQuery, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetSelectedDistrictByProjectIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetSelectedDistrictByProjectIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                List<long> SelectedProvinceList = await _dbContext.DistrictMultiSelect
                                                            .Where(x => x.ProjectId == request.ProjectId && x.IsDeleted == false)
                                                            .Select(x => x.DistrictID).ToListAsync();

                response.data.DistrictMultiSelectById = SelectedProvinceList;
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