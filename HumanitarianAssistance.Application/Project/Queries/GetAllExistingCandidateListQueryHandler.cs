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
    public class GetAllExistingCandidateListQueryHandler : IRequestHandler<GetAllExistingCandidateListQuery, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllExistingCandidateListQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (GetAllExistingCandidateListQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
             try {
                int totalCount = await _dbContext.CandidateDetails.CountAsync (x => x.IsDeleted == false);

                var existingCandidateDetail = (from s in _dbContext.HiringRequestCandidateStatus
                .Where (x => x.IsDeleted == false && x.EmployeeID != null && x.HiringRequestId == request.HiringRequestId && x.ProjectId == request.ProjectId) 
                join cd in _dbContext.EmployeeDetail on s.EmployeeID equals cd.EmployeeID into cdl 
                from cd in cdl.DefaultIfEmpty () 
                join g in _dbContext.JobGrade on cd.GradeId equals g.GradeId into gd 
                from g in gd.DefaultIfEmpty () 
                select new ExistingCandidateDetailsModel {
                        EmployeeId = cd.EmployeeID,
                            EmployeeCode = cd.EmployeeCode,
                            FullName = cd.EmployeeName,
                            Gender = cd.SexId == 1 ? "Male" : cd.SexId == 2 ? "Female" : "Other",
                            CandidateStatus = s.CandidateStatus
                    })
                    .Skip (request.pageSize.Value * request.pageIndex.Value)
                    .Take (request.pageSize.Value)
                    .ToList ();

                response.data.TotalCount = totalCount;
                response.data.ExistingCandidateList = existingCandidateDetail.OrderByDescending (x => x.EmployeeId).ToList ();
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