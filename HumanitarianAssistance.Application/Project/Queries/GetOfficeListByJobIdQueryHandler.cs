using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetOfficeListByJobIdQueryHandler : IRequestHandler<GetOfficeListByJobIdQuery, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetOfficeListByJobIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetOfficeListByJobIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var OfficeList = await _dbContext.JobHiringDetail
                    .Include(o => o.OfficeDetails)
                    .Where(x => x.JobId == request.ProfessionId && x.IsDeleted == false && x.ProjectId==request.ProjectId).Select(x => new OfficeDetailListModel
                    {
                        OfficeId = x.OfficeDetails.OfficeId,
                        OfficeName = x.OfficeDetails.OfficeName
                    }).ToListAsync();
                response.data.OfficeList = OfficeList;
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
