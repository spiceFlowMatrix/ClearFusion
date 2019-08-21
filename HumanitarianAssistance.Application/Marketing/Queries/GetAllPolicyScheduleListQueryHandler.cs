using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetAllPolicyScheduleListQueryHandler : IRequestHandler<GetAllPolicyScheduleListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllPolicyScheduleListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetAllPolicyScheduleListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<JobDetailsModel> JobList = await GetJobsList();
                List<ScheduleDetailModel> policyScheduleList = await GetPolicyScheduleList();
                List<PolicyModel> policyDetail = await PolicyList();
                List<ProjectDetailModel> ProjectList = await GetProjectList();
                response.data.ProjectDetailModel = ProjectList;
                response.data.policyList = policyDetail;
                response.data.JobDetailsModel = JobList;
                response.data.scheduleDetailsList = policyScheduleList;
                response.StatusCode = 200;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
        public async Task<List<JobDetailsModel>> GetJobsList()
        {
            var JobList = await (from j in _dbContext.JobDetails
                                 join jp in _dbContext.JobPriceDetails on j.JobId equals jp.JobId
                                 where !j.IsDeleted && !jp.IsDeleted && j.IsApproved
                                 select (new JobDetailsModel
                                 {
                                     JobId = j.JobId,
                                     JobCode = j.JobCode,
                                     JobName = j.JobName,
                                     Description = j.Description,
                                     JobPhaseId = j.JobPhaseId,
                                     StartDate = j.StartDate,
                                     EndDate = j.EndDate,
                                     IsActive = j.IsActive,
                                     IsApproved = j.IsApproved,
                                     UnitRate = jp.UnitRate,
                                     Units = jp.Units,
                                     FinalRate = jp.FinalRate,
                                     FinalPrice = jp.FinalPrice,
                                     TotalPrice = jp.TotalPrice,
                                     CreatedDate = j.CreatedDate,
                                     IsInvoiceApproved = jp.IsInvoiceApproved
                                 })).ToListAsync();
            return JobList;
        }

        public async Task<List<ScheduleDetailModel>> GetPolicyScheduleList()
        {
            var policyScheduleList = await (from j in _dbContext.PolicyTimeSchedules
                                            join mc in _dbContext.PolicyDetails on j.PolicyId equals mc.PolicyId
                                            join pd in _dbContext.PolicyDaySchedules on j.PolicyId equals pd.PolicyId
                                            join po in _dbContext.PolicyOrderSchedules on j.PolicyId equals po.PolicyId
                                            where !j.IsDeleted && !mc.IsDeleted
                                            && !pd.IsDeleted && !po.IsDeleted
                                             && po.StartDate <= DateTime.UtcNow.Date && DateTime.UtcNow.Date <= po.EndDate
                                            //&& !jp.IsDeleted && !me.IsDeleted
                                            //&& !mc.IsDeleted
                                            select (new ScheduleDetailModel
                                            {
                                                PolicyId = mc.PolicyId,
                                                PolicyDayId = pd.Id,
                                                PolicyOrderId = po.Id,
                                                PolicyTimeId = j.Id,
                                                Name = mc.PolicyName,
                                                StartTime = j.StartTime.ToString(@"hh\:mm"),
                                                EndTime = j.EndTime.ToString(@"hh\:mm"),
                                                StartDate = po.StartDate,
                                                EndDate = po.EndDate,
                                                Sunday = pd.Sunday,
                                                Monday = pd.Monday,
                                                Tuesday = pd.Tuesday,
                                                Wednesday = pd.Wednesday,
                                                Thursday = pd.Thursday,
                                                Friday = pd.Friday,
                                                Saturday = pd.Saturday
                                            })).ToListAsync();
            return policyScheduleList;
        }

        public async Task<List<PolicyModel>> PolicyList()
        {
            var policyDetail = await (from j in _dbContext.PolicyDetails
                                      join jp in _dbContext.LanguageDetail on j.LanguageId equals jp.LanguageId
                                      join me in _dbContext.Mediums on j.MediumId equals me.MediumId
                                      join mc in _dbContext.MediaCategories on j.MediaCategoryId equals mc.MediaCategoryId
                                      where !j.IsDeleted && !jp.IsDeleted && !me.IsDeleted
                                      && !mc.IsDeleted
                                      select (new PolicyModel
                                      {
                                          PolicyId = j.PolicyId,
                                          PolicyName = j.PolicyName,
                                          PolicyCode = j.PolicyCode,
                                          Description = j.Description,
                                          LanguageId = jp.LanguageId,
                                          LanguageName = jp.LanguageName,
                                          MediumId = me.MediumId,
                                          MediumName = me.MediumName,
                                          MediaCategoryId = mc.MediaCategoryId,
                                          MediaCategoryName = mc.CategoryName
                                      })).OrderByDescending(x => x.CreatedDate).ToListAsync();
            return policyDetail;
        }

        public async Task<List<ProjectDetailModel>> GetProjectList()
        {
            var ProjectList = await _dbContext.ProjectDetail
                                         .Where(x => x.IsDeleted == false && x.ProjectName != "")
                                         .OrderByDescending(x => x.ProjectId).Select(x => new ProjectDetailModel
                                         {
                                             ProjectId = x.ProjectId,
                                             ProjectCode = x.ProjectCode,
                                             ProjectName = x.ProjectName,
                                             ProjectDescription = x.ProjectDescription
                                         }).ToListAsync();
            return ProjectList;
        }
    }
}
