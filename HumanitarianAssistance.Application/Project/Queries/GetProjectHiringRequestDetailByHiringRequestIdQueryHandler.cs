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

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectHiringRequestDetailByHiringRequestIdQueryHandler : IRequestHandler<GetProjectHiringRequestDetailByHiringRequestIdQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectHiringRequestDetailByHiringRequestIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectHiringRequestDetailByHiringRequestIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var requestDetail = await (from hr in _dbContext.ProjectHiringRequestDetail
                .Where(x => x.IsDeleted == false && x.HiringRequestId == request.HiringRequestId)
                                           join c in _dbContext.CurrencyDetails on hr.CurrencyId equals c.CurrencyId into h
                                           from c in h.DefaultIfEmpty()
                                           join o in _dbContext.OfficeDetail on hr.OfficeId equals o.OfficeId into od
                                           from o in od.DefaultIfEmpty()
                                           join g in _dbContext.JobGrade on hr.GradeId equals g.GradeId into gd
                                           from g in gd.DefaultIfEmpty()
                                           join p in _dbContext.ProfessionDetails on hr.ProfessionId equals p.ProfessionId into pd
                                           from p in pd.DefaultIfEmpty()
                                           join b in _dbContext.ProjectBudgetLineDetail on hr.BudgetLineId equals b.BudgetLineId into bl
                                           from b in bl.DefaultIfEmpty()
                                           join d in _dbContext.Department on hr.JobCategoryId equals d.DepartmentId into dp
                                           from d in dp.DefaultIfEmpty()
                                           join de in _dbContext.DesignationDetail on hr.PositionId equals de.DesignationId into del
                                           from de in del.DefaultIfEmpty()
                                           select new ProjectHiringRequestModel
                                           {
                                               HiringRequestId = hr.HiringRequestId,
                                               HiringRequestCode = hr.HiringRequestCode,
                                               Office = o.OfficeName,
                                               JobGrade = g.GradeName,
                                               Position = de.Designation,
                                               TotalVacancies = hr.TotalVacancies,
                                               FilledVacancies = hr.FilledVacancies != null ? hr.FilledVacancies : 0,
                                               PayCurrency = c.CurrencyName,
                                               PayRate = hr.HourlyRate,
                                               BudgetName = b.BudgetName,
                                               BudgetLineId = b.BudgetLineId,
                                               JobCategory = d.DepartmentName,
                                               JobType = hr.JobTypeId,
                                               AnouncingDate = hr.AnouncingDate != null ? hr.AnouncingDate.Value.ToString("dd-MM-yyyy") : "",
                                               ClosingDate = hr.ClosingDate != null ? hr.ClosingDate.Value.ToString("dd-MM-yyyy") : "",
                                               ContractType = hr.ContractTypeId,
                                               ContractDuration = hr.ContractDuration,
                                               Shift = hr.Shift,
                                               EducationDegree = hr.EducationDegreeMaster.Name,
                                               Experience = hr.Experience,
                                               Profession = hr.ProfessionDetails.ProfessionName,
                                               KnowledgeAndSkills = hr.KnowladgeAndSkillRequired,
                                               HiringRequestStatus = hr.HiringRequestStatus,
                                               SpecificDutiesAndResponsibilities=hr.SpecificDutiesAndResponsblities,
                                               SubmissionGuidelines = hr.SubmissionGuidlines,
                                               OfficeId = o.OfficeId

                                           }).FirstOrDefaultAsync();
                response.ResponseData = requestDetail;
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