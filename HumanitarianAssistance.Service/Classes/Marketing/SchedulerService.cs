using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.Marketing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.ViewModels.Models.Project;

namespace HumanitarianAssistance.Service.Classes.Marketing
{
    public class SchedulerService : ISchedulerService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public SchedulerService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<List<JobDetailsModel>> GetJobsList()
        {
            var JobList = await (from j in _uow.GetDbContext().JobDetails
                                join jp in _uow.GetDbContext().JobPriceDetails on j.JobId equals jp.JobId
                                where !j.IsDeleted.Value && !jp.IsDeleted.Value
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
            var policyScheduleList = await (from j in _uow.GetDbContext().PolicyTimeSchedules
                                           join mc in _uow.GetDbContext().PolicyDetails on j.PolicyId equals mc.PolicyId
                                           join pd in _uow.GetDbContext().PolicyDaySchedules on j.PolicyId equals pd.PolicyId
                                           join po in _uow.GetDbContext().PolicyOrderSchedules on j.PolicyId equals po.PolicyId
                                           where !j.IsDeleted.Value && !mc.IsDeleted.Value
                                            && !pd.IsDeleted.Value && !po.IsDeleted.Value
                                            && po.StartDate<=DateTime.UtcNow.Date && DateTime.UtcNow.Date<=po.EndDate
                                           //&& !jp.IsDeleted.Value && !me.IsDeleted.Value
                                           //&& !mc.IsDeleted.Value
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
            var policyDetail = await (from j in _uow.GetDbContext().PolicyDetails
                                     join jp in _uow.GetDbContext().LanguageDetail on j.LanguageId equals jp.LanguageId
                                     join me in _uow.GetDbContext().Mediums on j.MediumId equals me.MediumId
                                     join mc in _uow.GetDbContext().MediaCategories on j.MediaCategoryId equals mc.MediaCategoryId
                                     where !j.IsDeleted.Value && !jp.IsDeleted.Value && !me.IsDeleted.Value
                                     && !mc.IsDeleted.Value
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

        public async Task<List<ProjectDetailNewModel>> GetProjectList()
        {
            var ProjectList = await _uow.GetDbContext().ProjectDetail
                                         .Where(x => !x.IsDeleted.Value)
                                         .OrderByDescending(x => x.ProjectId).Select(x => new ProjectDetailNewModel
                                         {
                                             ProjectId = x.ProjectId,
                                             ProjectCode = x.ProjectCode,
                                             ProjectName = x.ProjectName,
                                             ProjectDescription = x.ProjectDescription,
                                             IsWin = _uow.GetDbContext().WinProjectDetails.Where(y => y.ProjectId == x.ProjectId).Select(y => y.IsWin).FirstOrDefault(),
                                             IsCriteriaEvaluationSubmit = x.IsCriteriaEvaluationSubmit,
                                             ProjectPhase = x.ProjectPhaseDetailsId == x.ProjectPhaseDetails.ProjectPhaseDetailsId ? x.ProjectPhaseDetails.ProjectPhase.ToString() : "",
                                             TotalDaysinHours = x.EndDate == null ? (Convert.ToString(Math.Round(DateTime.Now.Subtract(x.StartDate.Value).TotalHours, 0) + ":" + DateTime.Now.Subtract(x.StartDate.Value).Minutes)) : (Convert.ToString(Math.Round(x.EndDate.Value.Subtract(x.StartDate.Value).TotalHours, 0) + ":" + x.EndDate.Value.Subtract(x.StartDate.Value).Minutes))
                                         }).ToListAsync();
            return ProjectList;
        }
        public async Task<APIResponse> GetAllPolicyScheduleList()
        {
            APIResponse response = new APIResponse();
            try
            {
                List<JobDetailsModel> JobList = await GetJobsList();
                List<ScheduleDetailModel> policyScheduleList = await GetPolicyScheduleList();
                List<PolicyModel> policyDetail = await PolicyList();
                List<ProjectDetailNewModel> ProjectList = await GetProjectList();
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

        public async Task<APIResponse> GetScheduleDetailsById(ScheduleDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var scheduleById = await (from j in _uow.GetDbContext().PolicyTimeSchedules
                                          join mc in _uow.GetDbContext().PolicyDetails on j.PolicyId equals mc.PolicyId
                                          join pd in _uow.GetDbContext().PolicyDaySchedules on j.PolicyId equals pd.PolicyId
                                          join po in _uow.GetDbContext().PolicyOrderSchedules on j.PolicyId equals po.PolicyId
                                          where !j.IsDeleted.Value && !mc.IsDeleted.Value
                                          && !pd.IsDeleted.Value && !po.IsDeleted.Value && j.Id == model.PolicyTimeId && po.Id == model.PolicyOrderId && mc.PolicyId == j.PolicyId
                                          select (new ScheduleDetailModel
                                          {
                                              PolicyId = mc.PolicyId,
                                              PolicyDayId = pd.Id,
                                              PolicyOrderId = po.Id,
                                              PolicyName = mc.PolicyName,
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
                                          })).FirstOrDefaultAsync();
               if(scheduleById != null)
                {
                    var policyList = await _uow.GetDbContext().PolicyDaySchedules
                                       .Where(v => v.IsDeleted == false && v.PolicyId == scheduleById.PolicyId).Select(x => new PolicyTimeScheduleModel
                                       {
                                           Id = x.Id,
                                           PolicyId = x.PolicyId,
                                           Monday = x.Monday,
                                           Tuesday = x.Tuesday,
                                           Wednesday = x.Wednesday,
                                           Thursday = x.Thursday,
                                           Friday = x.Friday,
                                           Saturday = x.Saturday,
                                           Sunday = x.Sunday
                                       }).AsNoTracking().FirstOrDefaultAsync();
                    List<string> repeatDays = new List<string>();
                    if (policyList != null)
                    {
                        if (policyList.Monday == true)
                        {
                            repeatDays.Add("MON");
                        }
                        if (policyList.Tuesday == true)
                        {
                            repeatDays.Add("TUE");
                        }
                        if (policyList.Wednesday == true)
                        {
                            repeatDays.Add("WED");
                        }
                        if (policyList.Thursday == true)
                        {
                            repeatDays.Add("THU");
                        }
                        if (policyList.Friday == true)
                        {
                            repeatDays.Add("FRI");
                        }
                        if (policyList.Saturday == true)
                        {
                            repeatDays.Add("SAT");
                        }
                        if (policyList.Sunday == true)
                        {
                            repeatDays.Add("SUN");
                        }
                        policyList.repeatDays = repeatDays;
                    }
                    response.data.policyTimeDetailsById = policyList;
                    response.data.scheduleDetails = scheduleById;
                    response.StatusCode = 200;
                    response.Message = "Success";
                }                
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
