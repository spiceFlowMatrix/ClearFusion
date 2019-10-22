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
                var requestDetail = await (from hr in _dbContext.ProjectHiringRequestDetail.Where(x => x.IsDeleted == false &&
                                                                                                      x.HiringRequestId == request.HiringRequestId)
                                           join c in _dbContext.CurrencyDetails on hr.CurrencyId equals c.CurrencyId into h
                                           from c in h.DefaultIfEmpty()
                                           join o in _dbContext.OfficeDetail on hr.OfficeId equals o.OfficeId into od
                                           from o in od.DefaultIfEmpty()
                                           join g in _dbContext.JobGrade on hr.GradeId equals g.GradeId into gd
                                           from g in gd.DefaultIfEmpty()
                                           join j in _dbContext.JobHiringDetails on hr.HiringRequestId equals j.HiringRequestId into jd
                                           from j in jd.DefaultIfEmpty()                                           
                                           select new ProjectHiringRequestModel
                                           {
                                               Description = j.JobDescription,
                                               HiringRequestId = hr.HiringRequestId,
                                               Office = o.OfficeName,
                                               JobCode = j.JobCode,
                                               JobGrade = g.GradeName,
                                               Position = hr.Position,
                                               TotalVacancies = hr.TotalVacancies,
                                               FilledVacancies = hr.FilledVacancies != null ? hr.FilledVacancies : 0,
                                               PayCurrency = c.CurrencyName,
                                               PayRate = hr.BasicPay,
                                               Status = hr.IsCompleted == true ? "Completed" : "InProgress"
                                           }).FirstOrDefaultAsync();
                response.data.ProjectHiringRequestDetails = requestDetail;
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