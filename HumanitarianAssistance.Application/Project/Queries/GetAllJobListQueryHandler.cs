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

namespace HumanitarianAssistance.Application.Project.Queries {
    public class GetAllJobListQueryHandler : IRequestHandler<GetAllJobListQuery, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllJobListQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (GetAllJobListQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
<<<<<<< HEAD
                var jobDetailList = await _dbContext.ProjectJobHiringDetail.Where (x => x.IsDeleted == false && x.ProjectId == request.ProjectId && x.ProfessionId == request.ProfessionId && x.OfficeId == request.OfficeId).Select (x => new JobHiringDetailModel {
=======
                var jobDetailList = await _dbContext.ProjectJobHiringDetail.Where (x => x.IsDeleted == false).Select(x => new JobHiringDetailModel
                {
>>>>>>> 3a08f5d84cb24116cd7f610248c96694118eea67
                    JobId = x.JobId,
                        JobCode = x.JobCode
                }).ToListAsync ();

                response.data.JobDetailList = jobDetailList;
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