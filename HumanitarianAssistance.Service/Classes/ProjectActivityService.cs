using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Project;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
                    projectactivityDetail.StatusId = (int)ProjectPhaseType.Implementation;

                    projectactivityDetail.ModifiedDate = DateTime.UtcNow;
                    projectactivityDetail.ModifiedById = UserId;
                    projectactivityDetail.IsDeleted = false;

                    if (projectactivityDetail.StartDate <= projectactivityDetail.ActualStartDate)
                    {
                        await _uow.ProjectActivityDetailRepository.UpdateAsyn(projectactivityDetail);
                        response.data.DateTime = projectactivityDetail.ActualStartDate.Value.Date;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.invalidDate;
                    }
                  
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

                    response.data.DateTime = projectactivityDetail.ActualEndDate.Value.Date;
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
                    projectactivityDetail.StatusId = (int)ProjectPhaseType.Monitoring;

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
                    projectactivityDetail.StatusId = (int)ProjectPhaseType.Completed;

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

        public async Task<int> GetActivityOnSchedule(long projectId)
        {

            int totalCount = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                   a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                                   a.StartDate == (a.ActualStartDate.Value.Date != null ?
                                                                                   a.ActualStartDate.Value.Date : DateTime.UtcNow.Date));
            return totalCount;
        }

        public async Task<int> GetLateStart(long projectId)
        {

            int totalCount = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                  a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                                  a.StartDate.Value.Date < (a.ActualStartDate != null ?
                                                                                                           a.ActualStartDate.Value.Date :
                                                                                                           DateTime.UtcNow.Date)
                                                                              );
            return totalCount;

        }

        public async Task<int> GetLateEnd(long projectId)
        {

            int totalCount = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                   a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                                   (a.EndDate.Value.Date != null ? a.EndDate.Value.Date : DateTime.UtcNow.Date) < (
                                                                                   a.ActualEndDate.Value.Date != null ?
                                                                                   a.ActualEndDate.Value.Date : DateTime.UtcNow.Date));
            return totalCount;
        }

        public async Task<int> GetSlippage(long projectId)
        {

            int slippage = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                      a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                                      (a.ActualEndDate.Value.Date != null ?
                                                                                      a.ActualEndDate.Value.Date : DateTime.UtcNow.Date) >
                                                                                      (a.EndDate.Value.Date != null ? a.EndDate.Value.Date : DateTime.UtcNow.Date));
            return slippage;
        }

        public async Task<float> GetProgress(long projectId)
        {

            float avg = 0;
            float totalImplementationProgress = 0;
            float totalMonitoringProgrss = 0;
            var slippage = await _uow.GetDbContext().ProjectActivityDetail
                                                    .Where(a => a.IsDeleted == false && a.ProjectBudgetLineDetail.ProjectId == projectId)
                                                    .Select(x => new
                                                    {
                                                        x.ImplementationProgress,
                                                        x.MonitoringProgress
                                                    })
                                                    .ToListAsync();

            if (slippage.Count() > 0)
            {
                totalImplementationProgress = slippage.Sum(x => x.ImplementationProgress ?? 0);
                totalMonitoringProgrss = slippage.Sum(x => x.MonitoringProgress ?? 0);

                avg = (totalImplementationProgress + totalMonitoringProgrss) / (slippage.Count() + slippage.Count());
            }

            return avg;
        }

        public async Task<DateTime?> GetMinimumProjectActivityStartDate(long projectId)
        {
            return await _uow.GetDbContext().ProjectActivityDetail.Where(x => x.ProjectBudgetLineDetail.ProjectId == projectId && x.IsDeleted == false)
                                                                  .MinAsync(x => x.StartDate);
        }
        public async Task<DateTime?> GetMaximumProjectActivityEndDate(long projectId)
        {
            return await _uow.GetDbContext().ProjectActivityDetail.Where(x => x.ProjectBudgetLineDetail.ProjectId == projectId && x.IsDeleted == false)
                                                                  .MaxAsync(x => x.EndDate);
        }

        public async Task<APIResponse> AllProjectActivityStatus(long projectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                Task<DateTime?> minStartDate = GetMinimumProjectActivityStartDate(projectId);
                Task<DateTime?> maxEndDate = GetMaximumProjectActivityEndDate(projectId);

                Task<int> ActivityOnSchedule = GetActivityOnSchedule(projectId);
                Task<int> LateStart = GetLateStart(projectId);
                Task<int> LateEnd = GetLateEnd(projectId);
                Task<float> Progress = GetProgress(projectId);
                Task<int> Slippage = GetSlippage(projectId);

                DateTime minDate = await minStartDate ?? DateTime.UtcNow;
                DateTime maxDate = await maxEndDate ?? DateTime.UtcNow;

                ProjectActivityStatusModel obj = new ProjectActivityStatusModel
                {
                    ProjectDuration = (maxDate.Date - minDate.Date).Days,
                    ActivityOnSchedule = await ActivityOnSchedule,
                    LateStart = await LateStart,
                    LateEnd = await LateEnd,
                    Progress = await Progress,
                    Slippage = await Slippage,
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

        public async Task<APIResponse> UploadDocumentFile(IFormFile file, string UserId, long activityId, string fileName, string logginUserEmailId, string ext,int statusID)
        {
            APIResponse response = new APIResponse();
            try
            {

                ActivityDocumentDetailModel activityModel = new ActivityDocumentDetailModel();
                var projectCode = _uow.GetDbContext().ProjectActivityDetail.Include(x => x.ProjectBudgetLineDetail.ProjectDetail)
                                                                      .FirstOrDefault(x => x.ActivityId == activityId && x.IsDeleted == false);
                string folderName = projectCode.ProjectBudgetLineDetail.ProjectDetail.ProjectCode;

                Console.WriteLine("------Before Credential path Upload----------");

                //read credientials
                string googleCredentialPathFile1 = Path.Combine(Directory.GetCurrentDirectory(), StaticResource.googleCredential + StaticResource.credentialsJsonFile);
                Console.WriteLine(googleCredentialPathFile1);

                Console.WriteLine("------------After Credential path Upload-------------");

                GoogleCredential result = new GoogleCredential();
                using (StreamReader files = File.OpenText(googleCredentialPathFile1))
                using (JsonTextReader reader = new JsonTextReader(files))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);

                    result = o2["GoogleCredential"].ToObject<GoogleCredential>();
                }


                string bucketResponse = GCBucket.UploadDocument(folderName, file, fileName, ext, googleCredentialPathFile1, result).Result;
                //ActivityDocumentsDetail activityExixt = _uow.GetDbContext().ActivityDocumentsDetail.FirstOrDefault(x => x.ActivityId == activityId && x.IsDeleted == false);
                ActivityDocumentsDetail docObj = new ActivityDocumentsDetail();

                if (!string.IsNullOrEmpty(bucketResponse))
                {
                    //if (activityExixt == null)
                    //{
                        docObj.ActivityId = activityId;
                        docObj.ActivityDocumentsFilePath = bucketResponse;
                        docObj.StatusId = statusID;
                        docObj.CreatedById = UserId;
                        docObj.IsDeleted = false;
                        docObj.CreatedDate = DateTime.UtcNow;

                        await _uow.ActivityDocumentsDetailRepository.AddAsyn(docObj);
                        _uow.GetDbContext().SaveChanges();

                    //}

                    //else
                    //{
                    //    activityExixt.ActivityId = activityId;
                    //    activityExixt.ActivityDocumentsFilePath = bucketResponse;
                    //    activityExixt.IsDeleted = false;
                    //    activityExixt.ModifiedById = UserId;
                    //    activityExixt.ModifiedDate = DateTime.UtcNow;
                    //    activityExixt.StatusId = 1;
                    //    _uow.GetDbContext().ActivityDocumentsDetail.Update(activityExixt);
                    //    _uow.GetDbContext().SaveChanges();
                    //}

                }


                _uow.GetDbContext().SaveChanges();
                response.data.activityDocumnentDetail = docObj;

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

        public async Task<APIResponse> GetUploadedDocument(long activityId)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                var listobj = await  _uow.GetDbContext().ActivityDocumentsDetail.Where(x => x.ActivityId == activityId && x.IsDeleted == false)
                    .Select(x => new ActivityDocumentDetailModel()
                    {
                        ActivityId = x.ActivityId,
                        StatusId = x.StatusId,
                        ActivityDocumentsFilePath = StaticResource.uploadUrl + x.ActivityDocumentsFilePath,
                        ActivityDocumentsFileName = x.ActivityDocumentsFilePath.Substring(x.ActivityDocumentsFilePath.LastIndexOf('/')),
                        ActtivityDocumentId = x.ActtivityDocumentId
                    }).ToListAsync();
                if (listobj.Any())
                {
                    apiResponse.data.ActivityDocumentDetailModel = listobj;
                    apiResponse.StatusCode = StaticResource.successStatusCode;
                    apiResponse.Message = StaticResource.SuccessText;
                }
                
                
            }
            catch (Exception ex)
            {
                apiResponse.StatusCode = StaticResource.failStatusCode;
                apiResponse.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return apiResponse;
        }
    }


    #endregion

}
