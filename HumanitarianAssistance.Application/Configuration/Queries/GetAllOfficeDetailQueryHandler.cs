using System;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;
using HumanitarianAssistance.Application.Configuration.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllOfficeDetailQueryHandler: IRequestHandler<GetAllOfficeDetailQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetAllOfficeDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllOfficeDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var officelist = await (from o in _dbContext.OfficeDetail
                                  where o.IsDeleted == false
                                  select new OfficeDetailModel
                                  {
                                      OfficeId = o.OfficeId,
                                      OfficeCode = o.OfficeCode,
                                      OfficeName = o.OfficeName,
                                      SupervisorName = o.SupervisorName,
                                      PhoneNo = o.PhoneNo,
                                      FaxNo = o.FaxNo,
                                      OfficeKey = o.OfficeKey,
                                      CreatedById = o.CreatedById,
                                      CreatedDate = o.CreatedDate,
                                      ModifiedById = o.ModifiedById,
                                      ModifiedDate = o.ModifiedDate
                                  }).OrderBy(x => x.OfficeCode).ToListAsync();

                response.data.OfficeDetailsList = officelist;
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