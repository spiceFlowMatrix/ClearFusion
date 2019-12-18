using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Update {
    public class UpdateCandidateStatusByStatusIdCommandHandler : IRequestHandler<UpdateCandidateStatusByStatusIdCommand, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        public UpdateCandidateStatusByStatusIdCommandHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (UpdateCandidateStatusByStatusIdCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {

                if (request.candidateId != 0) {
                    var candidateDetails = await _dbContext.HiringRequestCandidateStatus
                        .Where (x => x.CandidateId == request.candidateId && x.IsDeleted == false && x.HiringRequestId == request.hiringRequestId && x.ProjectId == request.projectId).FirstOrDefaultAsync ();
                    if (request.statusId == (int)CandidateStatus.Rejected) {
                        candidateDetails.CandidateStatus = (int)CandidateStatus.Rejected;
                    } else if (candidateDetails.CandidateStatus != (int)CandidateStatus.Selected || candidateDetails.CandidateStatus != (int)CandidateStatus.Rejected) {
                        candidateDetails.CandidateStatus = candidateDetails.CandidateStatus + 1;
                        candidateDetails.ModifiedById = request.ModifiedById;
                        candidateDetails.ModifiedDate = request.ModifiedDate;
                    }
                    await _dbContext.SaveChangesAsync ();
                    response.data.CandidateStatus = candidateDetails;
                } else if (request.employeeId != 0) {
                    var candidateDetails = await _dbContext.HiringRequestCandidateStatus.Where (x => x.EmployeeID == request.employeeId && x.IsDeleted == false && x.HiringRequestId == request.hiringRequestId && x.ProjectId == request.projectId).FirstOrDefaultAsync ();
                    if (request.statusId == (int)CandidateStatus.Rejected) {
                        candidateDetails.CandidateStatus = (int)CandidateStatus.Rejected;
                    } else if (candidateDetails.CandidateStatus != (int)CandidateStatus.Selected || candidateDetails.CandidateStatus != (int)CandidateStatus.Rejected) {
                        candidateDetails.CandidateStatus = candidateDetails.CandidateStatus + 1;
                        candidateDetails.ModifiedById = request.ModifiedById;
                        candidateDetails.ModifiedDate = request.ModifiedDate;
                    }
                    await _dbContext.SaveChangesAsync ();
                    response.data.CandidateStatus = candidateDetails;
                }
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