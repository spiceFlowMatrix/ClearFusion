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
                            x.IsDeleted == false)
                              join e in _dbContext.EducationDegreeMaster 
                              on cd.EducationDegreeId equals e.Id into ed from e in ed.DefaultIfEmpty () 
                              select new CandidateAllDetailsModel {
                                FullName = $"{cd.FirstName } {cd.LastName}" ,
                                Gender = cd.GenderId,
                                Qualification = e.Name,
                                DateOfBirth = cd.DateOfBirth.ToString("dd/MM/yyyy")
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