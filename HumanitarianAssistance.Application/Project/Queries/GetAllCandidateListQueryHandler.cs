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
    public class GetAllCandidateListQueryHandler : IRequestHandler<GetAllCandidateListQuery, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllCandidateListQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (GetAllCandidateListQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var candidateDetail = await (from s in _dbContext.HiringRequestCandidateStatus
                .Where(x => x.IsDeleted == false && x.CandidateId != null && x.HiringRequestId == request.HiringRequestId && x.ProjectId == request.ProjectId)
                                             join cd in _dbContext.CandidateDetails on s.CandidateId equals cd.CandidateId into cdl
                                             from cd in cdl.DefaultIfEmpty()
                                             join p in _dbContext.ProfessionDetails on cd.ProfessionId equals p.ProfessionId into pd
                                             from p in pd.DefaultIfEmpty()
                                             join e in _dbContext.EducationDegreeMaster on cd.EducationDegreeId equals e.Id into ed
                                             from e in ed.DefaultIfEmpty()
                                             join c in _dbContext.CountryDetails on cd.CountryId equals c.CountryId into cod
                                             from c in cod.DefaultIfEmpty()
                                             join pr in _dbContext.ProvinceDetails on cd.ProvinceId equals pr.ProvinceId into prd
                                             from pr in prd.DefaultIfEmpty()
                                             join d in _dbContext.DistrictDetail on cd.DistrictID equals d.DistrictID into dd
                                             from d in dd.DefaultIfEmpty()
                                             join emd in _dbContext.EmployeeDetail on cd.EmployeeID equals emd.EmployeeID into edl
                                             from emd in edl.DefaultIfEmpty()
                                             select new CandidateDetailsModel
                                             {
                                                 CandidateId = cd.CandidateId,
                                                 FirstName = cd.FirstName,
                                                 LastName = cd.LastName,
                                                 Email = cd.Email,
                                                 PhoneNumber = cd.PhoneNumber,
                                                 Gender = cd.GenderId,
                                                 DateOfBirth = cd.DateOfBirth,
                                                 EducationDegree = e.Name,
                                                 Profession = p.ProfessionName,
                                                 Country = c.CountryName,
                                                 Province = pr.ProvinceName,
                                                 District = d.District,
                                                 InterviewId = s.InterviewId != null ? s.InterviewId : 0,
                                                 CandidateStatus = s.CandidateStatus,
                                                 RelevantExperienceInYear = cd.RelevantExperienceInYear,
                                                 IrrelevantExperienceInYear = cd.IrrelevantExperienceInYear,
                                                 EmployeeCode = emd.EmployeeCode,
                                                 EmployeeId = emd.EmployeeID,
                                                 EmployeeName =  emd.EmployeeName
                                             }).AsQueryable()
                    .Skip(request.pageSize.Value * request.pageIndex.Value)
                    .Take(request.pageSize.Value)
                    .ToListAsync();

                response.data.TotalCount = candidateDetail.Count;
                response.data.CandidateList = candidateDetail.OrderByDescending (x => x.CandidateId).ToList ();
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