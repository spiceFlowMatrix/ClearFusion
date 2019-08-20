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


                var requestDetail = (from hr in _dbContext.ProjectHiringRequestDetail.Where(x => x.IsDeleted == false &&
                                                                                                         x.ProjectId == request.ProjectId)
                                     join c in _dbContext.CurrencyDetails on hr.CurrencyId equals c.CurrencyId into h
                                     from c in h.DefaultIfEmpty()
                                     join b in _dbContext.ProjectBudgetLineDetail on hr.BudgetLineId equals b.BudgetLineId into bl
                                     from b in bl.DefaultIfEmpty()
                                     join o in _dbContext.OfficeDetail on hr.OfficeId equals o.OfficeId into od
                                     from o in od.DefaultIfEmpty()
                                     join g in _dbContext.JobGrade on hr.GradeId equals g.GradeId into gd
                                     from g in gd.DefaultIfEmpty()
                                     join e in _dbContext.EmployeeDetail on hr.EmployeeID equals e.EmployeeID into ed
                                     from e in ed.DefaultIfEmpty()
                                     join p in _dbContext.ProfessionDetails on hr.ProfessionId equals p.ProfessionId into pd
                                     from p in pd.DefaultIfEmpty()
                                     join u in _dbContext.UserDetails on hr.CreatedById equals u.AspNetUserId into ud
                                     from u in ud.DefaultIfEmpty()
                                     select new ProjectHiringRequestModel
                                     {
                                         HiringRequestId = hr.HiringRequestId,
                                         HiringRequestCode = hr.HiringRequestCode,
                                         CurrencyId = c.CurrencyId,
                                         BudgetLineId = b.BudgetLineId,
                                         OfficeId = o.OfficeId,
                                         GradeId = g.GradeId,
                                         BasicPay = hr.BasicPay,
                                         BudgetName = b.BudgetName,
                                         Description = hr.Description,
                                         CurrencyName = c.CurrencyName,
                                         EmployeeID = e.EmployeeID,
                                         EmployeeName = e.EmployeeName,
                                         FilledVacancies = hr.FilledVacancies,
                                         GradeName = g.GradeName,
                                         IsCompleted = hr.IsCompleted,
                                         OfficeName = o.OfficeName,
                                         Position = hr.Position,
                                         ProjectId = hr.ProjectId,
                                         ProfessionId = hr.ProfessionId,
                                         ProfessionName = p.ProfessionName,
                                         TotalVacancies = hr.TotalVacancies,
                                         RequestedBy = ud.Select(x => new
                                         {
                                             FullName = x.FirstName + " " + x.LastName,
                                             x.AspNetUserId,
                                             x.IsDeleted
                                         }).FirstOrDefault(a => a.AspNetUserId == hr.CreatedById && a.IsDeleted == false).FullName

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
