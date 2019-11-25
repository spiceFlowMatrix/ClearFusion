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
    public class GetAllHiringRequestDetailForInterviewByHiringRequestIdQueryHandler : IRequestHandler<GetAllHiringRequestDetailForInterviewByHiringRequestIdQuery, ApiResponse> {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllHiringRequestDetailForInterviewByHiringRequestIdQueryHandler (HumanitarianAssistanceDbContext dbContext) {

            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle (GetAllHiringRequestDetailForInterviewByHiringRequestIdQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var hiringRequestDetails = await (from hrd in _dbContext.ProjectHiringRequestDetail
                        .Where (x => x.HiringRequestId == request.HiringRequestId &&
                            x.ProjectId == request.ProjectId &&
                            x.IsDeleted == false) 
                            join p in _dbContext.ProjectJobHiringDetail 
                            on hrd.JobId equals p.JobId into pd from p in pd.DefaultIfEmpty () 
                            join o in _dbContext.OfficeDetail 
                            on hrd.OfficeId equals o.OfficeId into od from o in od.DefaultIfEmpty () 
                            join j in _dbContext.JobGrade 
                            on p.GradeId equals j.GradeId into gd from j in gd.DefaultIfEmpty () 
                            join c in _dbContext.CurrencyDetails 
                            on p.CurrencyId equals c.CurrencyId into cd from c in cd.DefaultIfEmpty () 
                            join pr in _dbContext.ProfessionDetails 
                            on hrd.ProfessionId equals pr.ProfessionId into prd from pr in prd.DefaultIfEmpty () 
                            join b in _dbContext.ProjectBudgetLineDetail 
                            on hrd.BudgetLineId equals b.BudgetLineId into bd from b in bd.DefaultIfEmpty () 
                            select new HiringRequestDetailsModel {
                            Office = o.OfficeName,
                                Position = pr.ProfessionName,
                                JobGrade = j.GradeName,
                                TotalVacancy = hrd.TotalVacancies,
                                FilledVacancy = hrd.FilledVacancies,
                                PayCurrency = c.CurrencyName,
                                PayHourlyRate = p.PayRate,
                                BudgetLine = b.BudgetName,
                                // JobType = hrd.JobType,
                                AnouncingDate = hrd.AnouncingDate,
                                ClosingDate = hrd.ClosingDate,
                                ContractType = hrd.ContractType,
                                ContractDuration = hrd.ContractDuration,
                                JobShift = hrd.Shift == 1 ? "Day" : hrd.Shift == 2 ? "Night" : "Others",                               
                                Profession = pr.ProfessionName,
                                KnowledgeAndSkillsRequired = hrd.KnowladgeAndSkillRequired,
                                EducationDegree = hrd.MinimumEducationLevel,
                                TotalExperienceInYear = hrd.Experience
                        })
                    .FirstOrDefaultAsync ();

                response.data.HiringRequestDetails = hiringRequestDetails;
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