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
    public class GetAllDonorFilterListQueryHandler: IRequestHandler<GetAllDonorFilterListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllDonorFilterListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

         public async Task<ApiResponse> Handle(GetAllDonorFilterListQuery request, CancellationToken cancellationToken)
         {
             ApiResponse response = new ApiResponse();

            try
            {

                int totalCount = await _dbContext.DonorDetail.Where(x => x.IsDeleted == false).AsNoTracking().CountAsync();

                var list = await _dbContext.DonorDetail.Where(x => !x.IsDeleted)
                    .OrderByDescending(x => x.DonorId)
                    .Skip(request.pageSize.Value * request.pageIndex.Value)
                    .Take(request.pageSize.Value)
                    .ToListAsync();

                response.data.DonorDetail = list;
                response.data.TotalCount = totalCount;

                response.StatusCode = 200;
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