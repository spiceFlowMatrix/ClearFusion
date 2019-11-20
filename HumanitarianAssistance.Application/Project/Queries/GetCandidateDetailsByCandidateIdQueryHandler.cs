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

    public class GetCandidateDetailsByCandidateIdQueryHandler : IRequestHandler<GetCandidateDetailsByCandidateIdQuery, ApiResponse> {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetCandidateDetailsByCandidateIdQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (GetCandidateDetailsByCandidateIdQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var candidateDetails = await (from cd in _dbContext.CandidateDetails
                        .Where (x => x.CandidateId == request.CandidateId &&
                            x.IsDeleted == false) join o in _dbContext.OfficeDetail on cd.OfficeId equals o.OfficeId into od from o in od.DefaultIfEmpty () join e in _dbContext.EducationDegreeDetails on cd.EducationDegreeId equals e.EducationDegreeId into ed from e in ed.DefaultIfEmpty () select new CandidateAllDetailsModel {
                            FullName = cd.FirstName + ' ' + cd.LastName,
                                DutyStation = o.OfficeName,
                                Gender = cd.GenderId == 1 ? "Male" : cd.GenderId == 2 ? "Female" : "Other",
                                Qualification = e.EducationDegreeName,
                                DateOfBirth = cd.DateOfBirth
                        })
                    .FirstOrDefaultAsync ();

                response.data.CandidateDetails = candidateDetails;
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