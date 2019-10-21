using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllProvinceDetailQueryHandler: IRequestHandler<GetAllProvinceDetailQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllProvinceDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllProvinceDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var provincelist = await _dbContext.ProvinceDetails
                                             .Where(x => x.IsDeleted == false)
                                             .Select(x => new ProvinceDetailModel
                                             {
                                                ProvinceId = x.ProvinceId,
                                                ProvinceName = x.ProvinceName
                                             })
                                            .OrderBy(x => x.ProvinceName).ToListAsync();
                
                response.data.ProvinceDetailsList = provincelist;
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