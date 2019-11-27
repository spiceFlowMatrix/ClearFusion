using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries {
    public class GetAllEducationDegreeQueryHandler : IRequestHandler<GetAllEducationDegreeQuery, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEducationDegreeQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle (GetAllEducationDegreeQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var educationDegreeList = await (from c in _dbContext.EducationDegreeDetails where c.IsDeleted == false select new EducationDegreeModel {
                    EducationDegreeId = c.EducationDegreeId,
                        EducationDegreeName = c.EducationDegreeName
                }).ToListAsync ();
                response.data.EducationDegreeList = educationDegreeList.OrderBy (x => x.EducationDegreeId).ToList ();
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