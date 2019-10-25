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

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetallHiringRequestDetailQueryHandler : IRequestHandler<GetallHiringRequestDetailQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetallHiringRequestDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetallHiringRequestDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                int totalCount = await _dbContext.ProjectHiringRequestDetail.CountAsync(x => x.IsDeleted == false && x.ProjectId == request.ProjectId);

                // var requestDetail = (from hr in _dbContext.ProjectHiringRequestDetail.Where(x => x.IsDeleted == false &&
                //                                                                                  x.ProjectId == request.ProjectId)
                //                      join u in _dbContext.UserDetails on hr.CreatedById equals u.AspNetUserId
                //                      select new ProjectHiringRequestModel
                //                      {
                //                          Description = hr.Description,
                //                          ProfessionId = hr.ProfessionId,
                //                          Position = hr.Position,
                //                          TotalVacancies = hr.TotalVacancies,
                //                          BasicPay = hr.BasicPay,
                //                          BudgetLineId = hr.BudgetLineId,
                //                          OfficeId = hr.OfficeId,
                //                          GradeId = hr.GradeId,
                //                          ProjectId = hr.ProjectId,
                //                          CurrencyId = hr.CurrencyId,
                //                          JobCategory = hr.JobCategory,
                //                          MinimumEducationLevel = hr.MinimumEducationLevel,
                //                          Organization = hr.Organization,
                //                          ProvinceId = hr.ProvinceId,
                //                          ContractType = hr.ContractType,
                //                          ContractDuration = hr.ContractDuration,
                //                          GenderId = hr.GenderId,
                //                          SalaryRange = hr.SalaryRange,
                //                          AnouncingDate = hr.AnouncingDate,
                //                          ClosingDate = hr.ClosingDate,
                //                          CountryId = hr.CountryId,
                //                          JobType = hr.JobType,
                //                          Shift = hr.Shift,
                //                          JobStatus = hr.JobStatus,
                //                          Experience = hr.Experience,
                //                          Background = hr.Background,
                //                          SpecificDutiesAndResponsblities = hr.SpecificDutiesAndResponsblities,
                //                          KnowladgeAndSkillRequired = hr.KnowladgeAndSkillRequired,
                //                          SubmissionGuidlines = hr.SubmissionGuidlines,
                //                          HiringRequestId = hr.HiringRequestId,    
                //                          FilledVacancies = hr.FilledVacancies,
                //                          IsCompleted = hr.IsCompleted,
                //                          RequestedBy = u.FirstName + " " + u.LastName
                //                      }).Skip(request.pageSize.Value * request.pageIndex.Value)
                //                      .Take(request.pageSize.Value)
                //                      .ToList();

                var requestDetail = (from hr in _dbContext.ProjectHiringRequestDetail.Where(x => x.IsDeleted == false &&
                                                                                                        x.ProjectId == request.ProjectId)
                                     join j in _dbContext.ProjectJobHiringDetail on hr.JobId equals j.JobId into jd
                                     from j in jd.DefaultIfEmpty()
                                     join c in _dbContext.CurrencyDetails on j.CurrencyId equals c.CurrencyId into h
                                     from c in h.DefaultIfEmpty()                               
                                     join g in _dbContext.JobGrade on j.GradeId equals g.GradeId into gd
                                     from g in gd.DefaultIfEmpty()
                                     join p in _dbContext.ProfessionDetails on hr.ProfessionId equals p.ProfessionId into pd
                                     from p in pd.DefaultIfEmpty()
                                         // join e in _dbContext.EmployeeDetail on hr.EmployeeID equals e.EmployeeID into ed
                                         // from e in ed.DefaultIfEmpty()
                                        
                                         // join u in _dbContext.UserDetails on hr.CreatedById equals u.AspNetUserId into ud
                                         // from u in ud.DefaultIfEmpty()
                                     select new ProjectHiringRequestModel
                                     {
                                         HiringRequestId = hr.HiringRequestId,
                                         JobCode = j.JobCode,
                                         JobGrade = g.GradeName,
                                         Position = p.ProfessionName,
                                         TotalVacancies = hr.TotalVacancies,
                                         FilledVacancies = hr.FilledVacancies != null ? hr.FilledVacancies : 0,
                                         PayCurrency = c.CurrencyName,
                                         PayRate = j.PayRate,
                                         Status = hr.IsCompleted == true ? "Completed" : "InProgress"
                                     })
                                    .Skip(request.pageSize.Value * request.pageIndex.Value)
                                    .Take(request.pageSize.Value)
                                    .ToList();

                response.data.TotalCount = totalCount;
                response.data.ProjectHiringRequestModel = requestDetail.OrderByDescending(x => x.HiringRequestId).ToList();
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
