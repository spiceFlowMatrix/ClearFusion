using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Create {
    public class AddProjectHiringRequestCommandHandler : IRequestHandler<AddProjectHiringRequestCommand, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddProjectHiringRequestCommandHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle (AddProjectHiringRequestCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                ProjectHiringRequestDetail hiringRequestDeatil = new ProjectHiringRequestDetail () {
                    HiringRequestCode = request.HiringRequestCode,
                    CreatedById = request.CreatedById,
                    CreatedDate = request.CreatedDate,
                    IsDeleted = false,
                    OfficeId = request.Office,
                    ProjectId = request.ProjectId,
                    TotalVacancies = request.TotalVacancy,
                    AnouncingDate = request.AnouncingDate,
                    PositionId= request.Position,
                    HourlyRate= request.PayHourlyRate,
                    BudgetLineId= request.BudgetLine,
                    JobTypeId= request.JobType,
                    Background = request.Background,
                    JobStatus = request.JobStatus,
                    JobCategoryId = request.JobCategory,
                    KnowladgeAndSkillRequired = request.KnowledgeAndSkillsRequired,
                    SalaryRange = request.SalaryRange,
                    Shift = request.JobShift,
                    ProvinceId = request.ProvinceId,
                    SpecificDutiesAndResponsblities = request.SpecificDutiesAndResponsibilities,
                    SubmissionGuidlines = request.SubmissionGuidelines,
                    ClosingDate = request.ClosingDate,
                    ContractDuration = request.ContractDuration,
                    ContractType = request.ContractType,
                    CountryId = request.Nationality,
                    GenderId = request.Gender,
                    MinimumEducationLevel = request.MinEducationLevel,
                    Experience = request.Experience,
                    Organization = request.Organization,
                    GradeId= request.JobGrade,
                    CurrencyId= request.PayCurrency, 
                    EducationDegreeId= request.EducationDegree,
                    ProfessionId= request.Profession,
                    HiringRequestStatus = (int)HiringRequestStatus.Open
                };
                await _dbContext.ProjectHiringRequestDetail.AddAsync (hiringRequestDeatil);
                await _dbContext.SaveChangesAsync ();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}