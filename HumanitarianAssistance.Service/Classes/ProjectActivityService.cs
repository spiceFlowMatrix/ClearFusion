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
        public async Task<APIResponse> GetallProjectActivityDetail(long projectId)

        {

            APIResponse response = new APIResponse();
            try
            {
                var activityList = await _uow.GetDbContext().ProjectActivityDetail
                                          .Include(p => p.ProjectBudgetLineDetail)
                                          .Include(e => e.EmployeeDetail)
                                          .Include(o => o.OfficeDetail)
                                          .Include(s => s.ActivityStatusDetail)
                                          .Where(v => v.IsDeleted == false &&
                                                      v.ProjectBudgetLineDetail.ProjectId == projectId
                                          )
                                          .OrderBy(x => x.ActivityId)
                                          .ToListAsync();
                var activityDetaillist = activityList.Select(b => new ProjectActivityModel
                {
                    ActivityId = b.ActivityId,
                    ActivityName = b.ActivityName,
                    ActivityDescription = b.ActivityDescription,
                    BudgetLineId = b.ProjectBudgetLineDetail?.BudgetLineId,
                    BudgetName = b.ProjectBudgetLineDetail?.BudgetName,
                    ActualStartDate = b.ActualStartDate,
                    OfficeId = b.OfficeDetail?.OfficeId,
                    OfficeName = b.OfficeDetail?.OfficeName,
                    EmployeeID = b.EmployeeDetail?.EmployeeID,
                    EmployeeName = b.EmployeeDetail?.EmployeeName,
                    StatusId = b.ActivityStatusDetail?.StatusId,
                    StatusName = b.ActivityStatusDetail?.StatusName,
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
                    Weeknesses = b.Weeknesses,
                    Comments = b.Comments

                }).ToList();
                response.data.ProjectActivityList = activityDetaillist.OrderByDescending(x => x.ActivityId).ToList();
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
                var projectactivityDetail = await _uow.ProjectActivityDetailRepository.FindAsync(x => x.ActivityId == model.ActivityId && x.IsDeleted == false);
                if (projectactivityDetail != null)
                {
                    _mapper.Map(model, projectactivityDetail);

                    projectactivityDetail.ModifiedDate = DateTime.UtcNow;
                    projectactivityDetail.ModifiedById = UserId;
                    projectactivityDetail.IsDeleted = false;

                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(projectactivityDetail);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                } 
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ActivityNotFound;
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
                    response.Message = StaticResource.ActivityNotFound;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> StartProjectActivity(long activityId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var projectactivityDetail = await _uow.ProjectActivityDetailRepository.FindAsync(x => x.ActivityId == activityId && x.IsDeleted == false);
                if (projectactivityDetail != null)
                {
                    // Actual start date
                    projectactivityDetail.ActualStartDate = DateTime.UtcNow;

                    projectactivityDetail.ModifiedDate = DateTime.UtcNow;
                    projectactivityDetail.ModifiedById = UserId;
                    projectactivityDetail.IsDeleted = false;

                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(projectactivityDetail);

                    response.data.DateTime = projectactivityDetail.ActualStartDate.Value;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ActivityNotFound;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EndProjectActivity(long activityId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var projectactivityDetail = await _uow.ProjectActivityDetailRepository.FindAsync(x => x.ActivityId == activityId && x.IsDeleted == false);
                if (projectactivityDetail != null)
                {
                    // Actual end date
                    projectactivityDetail.ActualEndDate = DateTime.UtcNow;

                    projectactivityDetail.ModifiedDate = DateTime.UtcNow;
                    projectactivityDetail.ModifiedById = UserId;
                    projectactivityDetail.IsDeleted = false;

                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(projectactivityDetail);

                    response.data.DateTime = projectactivityDetail.ActualStartDate.Value;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ActivityNotFound;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> MarkImplementationAsCompleted(long activityId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var projectactivityDetail = await _uow.ProjectActivityDetailRepository.FindAsync(x => x.ActivityId == activityId && x.IsDeleted == false);
                if (projectactivityDetail != null)
                {
                    // Actual end date
                    projectactivityDetail.ImplementationStatus = !projectactivityDetail.ImplementationStatus;

                    projectactivityDetail.ModifiedDate = DateTime.UtcNow;
                    projectactivityDetail.ModifiedById = UserId;
                    projectactivityDetail.IsDeleted = false;

                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(projectactivityDetail);

                    response.data.ImplementationStatus = projectactivityDetail.ImplementationStatus.Value;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ActivityNotFound;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> MarkMonitoringAsCompleted(long activityId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var projectactivityDetail = await _uow.ProjectActivityDetailRepository.FindAsync(x => x.ActivityId == activityId && x.IsDeleted == false);
                if (projectactivityDetail != null)
                {
                    // Actual end date
                    projectactivityDetail.MonitoringStatus = !projectactivityDetail.MonitoringStatus;

                    projectactivityDetail.ModifiedDate = DateTime.UtcNow;
                    projectactivityDetail.ModifiedById = UserId;
                    projectactivityDetail.IsDeleted = false;

                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(projectactivityDetail);

                    response.data.MonitoringStatus = projectactivityDetail.MonitoringStatus.Value;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ActivityNotFound;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #endregion
    }
}
