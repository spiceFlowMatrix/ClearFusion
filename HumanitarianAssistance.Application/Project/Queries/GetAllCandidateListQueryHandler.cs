using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllCandidateListQueryHandler : IRequestHandler<GetAllCandidateListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllCandidateListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetAllCandidateListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var EmployeeDetailListData = await _dbContext
                                                                  .HiringRequestCandidates
                                                                  .Where(x => x.HiringRequestId == request.HiringRequestId && x.IsDeleted == false)
                                                                  //.Include(e => e.EmployeeDetail)
                                                                  .OrderByDescending(i => i.EmployeeID)
                                                                  .Select(x => new ProjectHiringCandidateDetailModel
                                                                  {
                                                                      CandidateId = x.CandidateId,
                                                                      EmployeeID = x.EmployeeID,
                                                                      EmployeeName = x.EmployeeDetail.EmployeeName,
                                                                      EmployeeCode = x.EmployeeDetail.EmployeeCode,
                                                                      EmployeeTypeId = x.EmployeeDetail.EmployeeTypeId,
                                                                      EmployeeTypeName = x.EmployeeDetail.EmployeeType.EmployeeTypeName == "Prospective" ?
                                                                                                                                           "Candidate" :
                                                                                                                                            x.EmployeeDetail.EmployeeType.EmployeeTypeName,
                                                                      Gender = x.EmployeeDetail.SexId == (int)Sex.Male ? "Male" :
                                                                                x.EmployeeDetail.SexId == (int)Sex.Female ? "Female" :
                                                                                x.EmployeeDetail.SexId == (int)Gender.OTHER ? "Other" : null,
                                                                      IsInterViewed = x.EmployeeDetail.InterviewDetails.Any(y => y.EmployeeID == x.EmployeeID && y.IsDeleted == false),
                                                                      IsShortListed = x.IsShortListed,
                                                                      IsSelected = x.IsSelected,



                                                                  }
                                                                  ).ToListAsync();
                response.data.ProjectHiringCandidateDetailModel = EmployeeDetailListData;
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
