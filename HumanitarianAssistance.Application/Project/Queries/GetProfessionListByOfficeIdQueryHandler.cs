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
    public class GetProfessionListByOfficeIdQueryHandler : IRequestHandler<GetProfessionListByOfficeIdQuery, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetProfessionListByOfficeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProfessionListByOfficeIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var ProfessionList = await _dbContext.JobHiringDetail
                    .Include(o => o.ProfessionDetails)
                    .Where(x => x.OfficeId == request.ProfessionId && x.IsDeleted == false && x.ProjectId==request.ProjectId).Select(x => new ProfessionListModel
                    {
                        ProfessionId = x.ProfessionDetails.ProfessionId,
                        ProfessionName = x.ProfessionDetails.ProfessionName
                    }).ToListAsync();
                response.data.ProfessionDetailList = ProfessionList;
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
