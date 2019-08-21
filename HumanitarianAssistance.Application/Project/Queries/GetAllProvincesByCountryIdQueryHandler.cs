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
    public class GetAllProvincesByCountryIdQueryHandler: IRequestHandler<GetAllProvincesByCountryIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllProvincesByCountryIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllProvincesByCountryIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var provincelist = await _dbContext.ProvinceDetails.Where(x => x.IsDeleted == false).ToListAsync();
                var Newlist = provincelist.Where(x => request.CountryId.Any(y => x.CountryId == y)).Select(x => new ProvinceDetailModel
                {
                    ProvinceId = x.ProvinceId,
                    ProvinceName = x.ProvinceName
                }).OrderBy(x => x.ProvinceName).ToList();

                response.data.ProvinceDetailsList = Newlist;
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