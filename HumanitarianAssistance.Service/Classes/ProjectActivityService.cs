using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Project;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class ProjectActivityService : IProjectActivityService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        private IHostingEnvironment _hostingEnvironment;
        public ProjectActivityService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        #region "projectActivity"
        public async Task<APIResponse> GetallProjectActivityDetail()
        {

            APIResponse response = new APIResponse();
            try
            {
                var activityList = await Task.Run(() =>
                        _uow.GetDbContext().ProjectActivityDetail
                                          .Include(p => p.ProjectBudgetLineDetail)
                                          .Include(e => e.EmployeeDetail)
                                          .Include(o => o.OfficeDetail)
                                          .Include(s => s.ActivityStatusDetail)
                                          .Where(v => v.IsDeleted == false).OrderBy(x => x.ActivityId).ToList()
                                         );
                var activityDetaillist = activityList.Select(b => new ProjectActivityModel
                {
                    ActivityId = b.ActivityId,
                    ActivityName = b.ActivityName,
                    ActivityDescription = b.ActivityDescription,
                    BudgetLineId = b.ProjectBudgetLineDetail?.BudgetLineId ?? null,
                    BudgetName = b.ProjectBudgetLineDetail?.BudgetName ?? null,
                    ActualStartDate = b.ActualStartDate,
                    OfficeId = b.OfficeDetail?.OfficeId ?? null,
                    OfficeName = b.OfficeDetail?.OfficeName ?? null,
                    EmployeeID = b.EmployeeDetail?.EmployeeID ?? null,
                    EmployeeName = b.EmployeeDetail?.EmployeeName ?? null,
                    StatusId = b.ActivityStatusDetail?.StatusId ?? null,
                    StatusName = b.ActivityStatusDetail?.StatusName ?? null,
                    ActualEndDate = b.ActualEndDate,
                    EndDate = b.EndDate,
                    ExtensionEndDate = b.ExtensionEndDate,
                    ExtensionStartDate = b.ExtensionStartDate,
                    ImplementationChalanges = b.ImplementationChalanges,
                    ImplementationMethod = b.ImplementationMethod,
                    ImplementationProgress = b.ImplementationProgress,
                    ImplementationStatus = b.ImplementationStatus,
                    MonitoringChallenges = b.MonitoringChallenges,
                    MonitoringFrequency = b.MonitoringFrequency,
                    MonitoringProgress = b.MonitoringProgress,
                    MonitoringScore = b.MonitoringScore,
                    MonitoringStatus = b.MonitoringStatus,
                    OvercomingChallanges = b.OvercomingChallanges,
                    Recommendation = b.Recommendation,
                    Recurring = b.Recurring,
                    RecurringCount = b.RecurringCount,
                    RecurrinTypeId = b.RecurrinTypeId,
                    StartDate = b.StartDate,
                    Strengths = b.Strengths,
                    VerificationSource = b.VerificationSource,
                    Weeknesses = b.Weeknesses

                }).ToList();
                response.data.ProjectActivityModel = activityDetaillist.OrderByDescending(x => x.ActivityId).ToList();
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


        public async Task<APIResponse> AddProjectActivityDetail(ProjectActivityModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ProjectActivityDetail obj = _mapper.Map<ProjectActivityModel, ProjectActivityDetail>(model);
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                await _uow.ProjectActivityDetailRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditProjectActivityDetail(ProjectActivityModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProjectactivityDetail = _uow.ProjectActivityDetailRepository.FindAsync(x => x.ActivityId == model.ActivityId);
                if (ProjectactivityDetail != null)
                {
                    ProjectActivityDetail obj = _mapper.Map<ProjectActivityModel, ProjectActivityDetail>(model);
                    obj.ModifiedDate = DateTime.UtcNow;
                    obj.ModifiedById = UserId;
                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(obj);
                    response.StatusCode = StaticResource.successStatusCode;
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


        public async Task<APIResponse> DeleteProjectActivity(long activityId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var activityDetail = await _uow.ProjectActivityDetailRepository.FindAsync(c => c.ActivityId == activityId);

                if (activityDetail != null)
                {
                    activityDetail.IsDeleted = true;
                    activityDetail.ModifiedById = userId;
                    activityDetail.ModifiedDate = DateTime.UtcNow;

                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(activityDetail);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }

                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.VoucherNotPresent;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public int GetActivityOnSchedule()
        {

            int totalCount = _uow.GetDbContext().ProjectActivityDetail.Where(a => a.IsDeleted == false && a.StartDate == (a.ActualStartDate.Value.Date != null ?
                                                                                  a.ActualStartDate.Value.Date :
                                                                                  DateTime.UtcNow.Date))
                                                                            .Count();
            return totalCount;
        }

        public int GetLateStart()
        {

            int totalCount = _uow.GetDbContext().ProjectActivityDetail.Count(a => a.IsDeleted == false &&
                                                                                  a.StartDate.Value.Date < (a.ActualStartDate != null ?
                                                                                                           a.ActualStartDate.Value.Date :
                                                                                                           DateTime.UtcNow.Date)
                                                                              );
            return totalCount;

        }

        public int GetLateEnd()
        {

            int totalCount = _uow.GetDbContext().ProjectActivityDetail.Where(a => a.IsDeleted == false &&
                                                                                   (a.EndDate.Value.Date != null ? a.EndDate.Value.Date : DateTime.UtcNow.Date) < (
                                                                                   a.ActualEndDate.Value.Date != null ?
                                                                                   a.ActualEndDate.Value.Date : DateTime.UtcNow.Date))
                                                                            .Count();
            return totalCount;
        }

        public int  GetSlippage()
        {

            int slippage =  _uow.GetDbContext().ProjectActivityDetail.Where(a => a.IsDeleted == false &&
                                                                                      (a.ActualEndDate.Value.Date !=null 
                                                                                      ? a.ActualEndDate.Value.Date
                                                                                      :DateTime.UtcNow.Date)
                                                                                    > (a.EndDate.Value.Date != null 
                                                                                      ? a.EndDate.Value.Date
                                                                                      : DateTime.UtcNow.Date))
                                                                                 .Count();
            return slippage;
        }

        public int GetProgress()
        {
          
            int avg = 0;
                int totalImplementationProgress = 0;
                int totalMonitoringProgrss = 0;
                var slippage = _uow.GetDbContext().ProjectActivityDetail.Where(a => a.IsDeleted == false).ToList();
                if (slippage != null)
                {
                    totalImplementationProgress = slippage.Sum(x => x.ImplementationProgress).Value;
                    totalMonitoringProgrss = slippage.Sum(x => x.MonitoringProgress).Value;
                    avg = (totalImplementationProgress + totalMonitoringProgrss) / 2;
                }

            return avg;
        }

        public async Task<APIResponse> AllProjectActivityStatus()
        {
            APIResponse response = new APIResponse();
            try
            {
                ProjectActivityStatusModel obj = new ProjectActivityStatusModel
                {
                    ActivityOnSchedule = GetActivityOnSchedule(),
                    LateStart = GetLateStart(),
                    LateEnd = GetLateEnd(),
                    Progress = GetProgress(),
                    Slippage = GetSlippage(),
                };

                response.data.ProjectActivityStatusModel = obj;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;


        }
    }

    #endregion

}
