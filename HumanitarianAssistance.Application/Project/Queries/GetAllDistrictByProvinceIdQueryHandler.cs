using System;
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
    public class GetAllDistrictByProvinceIdQueryHandler: IRequestHandler<GetAllDistrictByProvinceIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        
        public GetAllDistrictByProvinceIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllDistrictByProvinceIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var DistrictDetailList = await _dbContext.DistrictDetail.Where(x => x.IsDeleted == false).ToListAsync();

                var Newlist = DistrictDetailList.Where(x => request.ProvinceId.Any(y => x.ProvinceID == y)).ToList();
                response.data.Districtlist = Newlist;
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