using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {
    public class GetRemainingVacancyByJobIdQueryHandler : IRequestHandler<GetRemainingVacancyByJobIdQuery, ApiResponse> {

        private HumanitarianAssistanceDbContext _dbContext;
        public GetRemainingVacancyByJobIdQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle (GetRemainingVacancyByJobIdQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var data = await _dbContext.ProjectJobHiringDetail.Where (x => x.JobId == request.JobId && x.IsDeleted == false).FirstOrDefaultAsync ();
                response.data.FilledVacancies = data.FilledVacancies;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";

            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}