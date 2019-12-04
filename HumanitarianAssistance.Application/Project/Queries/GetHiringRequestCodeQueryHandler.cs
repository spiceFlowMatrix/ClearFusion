using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {

    public class GetHiringRequestCodeQueryHandler : IRequestHandler<GetHiringRequestCodeQuery, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetHiringRequestCodeQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (GetHiringRequestCodeQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var hiringRequestLastId = await _dbContext.ProjectHiringRequestDetail
                    .Where (x => x.ProjectId == request.ProjectId && x.IsDeleted == false)
                    .Select (x => x.HiringRequestId).CountAsync();
                    string hiringRequestCode = "HR-" + (hiringRequestLastId+1);
                response.data.HiringRequestCode = hiringRequestCode;
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