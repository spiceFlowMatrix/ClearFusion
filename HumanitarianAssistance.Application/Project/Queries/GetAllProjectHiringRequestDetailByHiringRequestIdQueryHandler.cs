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
    public class GetAllProjectHiringRequestDetailByHiringRequestIdQueryHandler : IRequestHandler<GetAllProjectHiringRequestDetailByHiringRequestIdQuery, ApiResponse> {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllProjectHiringRequestDetailByHiringRequestIdQueryHandler (HumanitarianAssistanceDbContext dbContext) {

            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle (GetAllProjectHiringRequestDetailByHiringRequestIdQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var requestDetail = await _dbContext.ProjectHiringRequestDetail
                .Where (x => x.HiringRequestId == request.HiringRequestId && x.IsDeleted == false)
                .Select (s => new ProjectHiringRequestDetailModel {
                        HiringRequestId = s.HiringRequestId,
                        HiringRequestCode = s.HiringRequestCode,
                        ProjectId = s.ProjectId,
                        JobCategory = s.JobCategoryId,
                        MinEducationLevel = s.MinimumEducationLevel,
                        TotalVacancy = s.TotalVacancies,
                        Position = s.PositionId,
                        Organization = s.Organization,
                        Office = s.OfficeId,
                        ContractType = s.ContractTypeId,
                        ContractDuration = s.ContractDuration,
                        Gender = s.GenderId,
                        Nationality = s.CountryId,
                        SalaryRange = s.SalaryRange,
                        AnouncingDate = s.AnouncingDate,
                        ClosingDate = s.ClosingDate,
                        Country = s.CountryId,
                        Province = s.ProvinceId,
                        JobType = s.JobTypeId,
                        JobShift = s.Shift,
                        JobStatus = s.JobStatus,
                        Experience = s.Experience,
                        Background = s.Background,
                        SpecificDutiesAndResponsibilities = s.SpecificDutiesAndResponsblities,
                        KnowledgeAndSkillsRequired = s.KnowladgeAndSkillRequired,
                        SubmissionGuidelines = s.SubmissionGuidlines,
                        PayCurrency= s.CurrencyId,
                        BudgetLine= s.BudgetLineId,
                       JobGrade = s.GradeId,
                       EducationDegree= s.EducationDegreeId,
                       Profession= s.ProfessionId,
                       PayHourlyRate= s.HourlyRate
                }).FirstOrDefaultAsync ();
                response.data.ProjectHiringRequestAllDetail = requestDetail;
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