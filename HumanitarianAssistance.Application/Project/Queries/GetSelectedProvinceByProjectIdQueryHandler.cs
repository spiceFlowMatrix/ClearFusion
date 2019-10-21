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
    public class GetSelectedProvinceByProjectIdQueryHandler: IRequestHandler<GetSelectedProvinceByProjectIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetSelectedProvinceByProjectIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetSelectedProvinceByProjectIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {

                List<int> SelectedProvinceList = await _dbContext.ProvinceMultiSelect
                                                                 .Where(x => x.ProjectId == request.ProjectId
                                                                            && x.IsDeleted == false)
                                                                 .Select(x => x.ProvinceId)
                                                                 .ToListAsync();

                response.data.ProvinceMultiSelectById = SelectedProvinceList;
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