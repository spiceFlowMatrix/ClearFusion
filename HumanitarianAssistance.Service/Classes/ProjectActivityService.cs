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
                                          .Where(v => v.IsDeleted == false &&
                                                      v.ParentId == null &&
                                                      v.ProjectBudgetLineDetail.ProjectId == projectId
                                          )
                                          .OrderByDescending(x => x.ActivityId)
                                          .Select(b => new ProjectActivityModel {

                                              ActivityId = b.ActivityId,
                                              ActivityName = b.ActivityName,
                                              ActivityDescription = b.ActivityDescription,
                                              BudgetLineId = b.ProjectBudgetLineDetail.BudgetLineId,
                                              BudgetName = b.ProjectBudgetLineDetail.BudgetName,
                                              EmployeeID = b.EmployeeDetail.EmployeeID,
                                              EmployeeName = b.EmployeeDetail.EmployeeName,
                                              StatusId = b.ActivityStatusDetail.StatusId,
                                              StatusName = b.ActivityStatusDetail.StatusName,
                                              PlannedStartDate = b.PlannedStartDate,
                                              PlannedEndDate = b.PlannedEndDate,
                                              Recurring = b.Recurring,
                                              RecurringCount = b.RecurringCount,
                                              RecurrinTypeId = b.RecurrinTypeId,
                                              ProvinceId = b.ProjectActivityProvinceDetail.Select(x=>x.ProvinceId),
                                              DistrictID = b.ProjectActivityProvinceDetail.Select(x => x.DistrictID)
                                          })
                                          .ToListAsync();
              
                response.data.ProjectActivityList = activityList;
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


                if (model.ProvinceId!= null)
                {
                    List<ProjectActivityProvinceDetail> activityProvienceList = new List<ProjectActivityProvinceDetail>();

                    var districts = _uow.GetDbContext().DistrictDetail.Where(x => x.IsDeleted == false && model.ProvinceId.Contains(x.ProvinceID.Value)).ToList();

                    var selectedDistrict = districts.Where(x => model.DistrictID.Contains(x.DistrictID))
                                                                     .Select(x=> new ProjectActivityProvinceDetail
                                                                     {
                                                                     DistrictID= x.DistrictID,
                                                                     ProvinceId= x.ProvinceID.Value
                                                                     }).ToList();

                    // var provincesWithNoDistrict= selectedDistrict.Where(x => !model.ProvinceId.Contains(x.ProvinceId));

                    var provincesWithNoDistrict = model.ProvinceId.Where(x => !selectedDistrict.Select(y => y.ProvinceId).Contains(x)).ToList();

                    foreach (var item in provincesWithNoDistrict)
                    {
                        ProjectActivityProvinceDetail projectActivityProvinceDetail = new ProjectActivityProvinceDetail();
                        projectActivityProvinceDetail.ProvinceId = item;
                        selectedDistrict.Add(projectActivityProvinceDetail);
                    }

                    foreach (var item in selectedDistrict)
                    {

                        item.ActivityId = obj.ActivityId;
                        item.CreatedById = UserId;
                        item.CreatedDate = DateTime.UtcNow;
                        item.IsDeleted = false;
                    }
                   // await _uow.ProjectActivityProvinceDetailRepository.A(obj);
                   await _uow.GetDbContext().ProjectActivityProvinceDetail.AddRangeAsync(selectedDistrict);
                    await _uow.GetDbContext().SaveChangesAsync();

                }

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

                    if (model.ProvinceId.Any())
                    {
                        var projectActivityProvinceDetailExist = _uow.GetDbContext().ProjectActivityProvinceDetail.Where(x => x.ActivityId == model.ActivityId && x.IsDeleted == false);

                        if (projectActivityProvinceDetailExist.Any())
                        {
                            _uow.GetDbContext().ProjectActivityProvinceDetail.RemoveRange(projectActivityProvinceDetailExist);
                            _uow.GetDbContext().SaveChanges();
                        }
                        
                            List<ProjectActivityProvinceDetail> activityProvienceList = new List<ProjectActivityProvinceDetail>();


                            var districts = _uow.GetDbContext().DistrictDetail.Where(x => x.IsDeleted == false && model.ProvinceId.Contains(x.ProvinceID.Value)).ToList();

                            var selectedDistrict = districts.Where(x => model.DistrictID.Contains(x.DistrictID))
                                                                             .Select(x => new ProjectActivityProvinceDetail
                                                                             {
                                                                                 DistrictID = x.DistrictID,
                                                                                 ProvinceId = x.ProvinceID.Value
                                                                             }).ToList();


                            var provincesWithNoDistrict = model.ProvinceId.Where(x => !selectedDistrict.Select(y => y.ProvinceId).Contains(x)).ToList();

                            foreach (var item in provincesWithNoDistrict)
                            {
                                ProjectActivityProvinceDetail projectActivityProvince = new ProjectActivityProvinceDetail();
                                projectActivityProvince.ProvinceId = item;
                                selectedDistrict.Add(projectActivityProvince);
                            }

                            foreach (var item in selectedDistrict)
                            {

                                item.ActivityId = projectactivityDetail.ActivityId;
                                item.ModifiedById = UserId;
                                item.ModifiedDate = DateTime.UtcNow;
                                item.IsDeleted = false;
                            }
                            await _uow.GetDbContext().ProjectActivityProvinceDetail.AddRangeAsync(selectedDistrict);
                            await _uow.GetDbContext().SaveChangesAsync();
                        
                    }

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

        //public async Task<APIResponse> StartProjectActivity(long activityId, string UserId)
        //{
        //    APIResponse response = new APIResponse();
        //    try
        //    {
        //        var projectactivityDetail = await _uow.ProjectActivityDetailRepository.FindAsync(x => x.ActivityId == activityId && x.IsDeleted == false);

        //        if (projectactivityDetail != null)
        //        {
        //            // Actual start date
        //            projectactivityDetail.ActualStartDate = DateTime.UtcNow;
        //            projectactivityDetail.StatusId = (int)ProjectPhaseType.Implementation;

        //            projectactivityDetail.ModifiedDate = DateTime.UtcNow;
        //            projectactivityDetail.ModifiedById = UserId;
        //            projectactivityDetail.IsDeleted = false;

        //            if (projectactivityDetail.StartDate <= projectactivityDetail.ActualStartDate)
        //            {
        //                await _uow.ProjectActivityDetailRepository.UpdateAsyn(projectactivityDetail);
        //                response.data.DateTime = projectactivityDetail.ActualStartDate.Value.Date;
        //                response.StatusCode = StaticResource.successStatusCode;
        //                response.Message = StaticResource.SuccessText;
        //            }
        //            else
        //            {
        //                response.StatusCode = StaticResource.failStatusCode;
        //                response.Message = StaticResource.invalidDate;
        //            }

        //        }
        //        else
        //        {
        //            response.StatusCode = StaticResource.failStatusCode;
        //            response.Message = StaticResource.ActivityNotFound;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = StaticResource.failStatusCode;
        //        response.Message = StaticResource.SomethingWrong + ex.Message;
        //    }
        //    return response;
        //}

        //public async Task<APIResponse> EndProjectActivity(long activityId, string UserId)
        //{
        //    APIResponse response = new APIResponse();
        //    try
        //    {
        //        var projectactivityDetail = await _uow.ProjectActivityDetailRepository.FindAsync(x => x.ActivityId == activityId && x.IsDeleted == false);
        //        if (projectactivityDetail != null)
        //        {
        //            // Actual end date
        //            projectactivityDetail.ActualEndDate = DateTime.UtcNow;

        //            projectactivityDetail.ModifiedDate = DateTime.UtcNow;
        //            projectactivityDetail.ModifiedById = UserId;
        //            projectactivityDetail.IsDeleted = false;

        //            await _uow.ProjectActivityDetailRepository.UpdateAsyn(projectactivityDetail);

        //            response.data.DateTime = projectactivityDetail.ActualEndDate.Value.Date;
        //            response.StatusCode = StaticResource.successStatusCode;
        //            response.Message = StaticResource.SuccessText;
        //        }
        //        else
        //        {
        //            response.StatusCode = StaticResource.failStatusCode;
        //            response.Message = StaticResource.ActivityNotFound;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = StaticResource.failStatusCode;
        //        response.Message = StaticResource.SomethingWrong + ex.Message;
        //    }
        //    return response;
        //}

        //public async Task<APIResponse> MarkImplementationAsCompleted(long activityId, string UserId)
        //{
        //    APIResponse response = new APIResponse();
        //    try
        //    {
        //        var projectactivityDetail = await _uow.ProjectActivityDetailRepository.FindAsync(x => x.ActivityId == activityId && x.IsDeleted == false);
        //        if (projectactivityDetail != null)
        //        {
        //            // Actual end date
        //            projectactivityDetail.ImplementationStatus = !projectactivityDetail.ImplementationStatus;
        //            projectactivityDetail.StatusId = (int)ProjectPhaseType.Monitoring;

        //            projectactivityDetail.ModifiedDate = DateTime.UtcNow;
        //            projectactivityDetail.ModifiedById = UserId;
        //            projectactivityDetail.IsDeleted = false;

        //            await _uow.ProjectActivityDetailRepository.UpdateAsyn(projectactivityDetail);

        //            response.data.ImplementationStatus = projectactivityDetail.ImplementationStatus.Value;
        //            response.data.ProjectActivityDetails = _mapper.Map<ProjectActivityDetail, ProjectActivityModel>(projectactivityDetail);
        //            response.StatusCode = StaticResource.successStatusCode;
        //            response.Message = StaticResource.SuccessText;
        //        }
        //        else
        //        {
        //            response.StatusCode = StaticResource.failStatusCode;
        //            response.Message = StaticResource.ActivityNotFound;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = StaticResource.failStatusCode;
        //        response.Message = StaticResource.SomethingWrong + ex.Message;
        //    }
        //    return response;
        //}

        //public async Task<APIResponse> MarkMonitoringAsCompleted(long activityId, string UserId)
        //{
        //    APIResponse response = new APIResponse();
        //    try
        //    {
        //        var projectactivityDetail = await _uow.ProjectActivityDetailRepository.FindAsync(x => x.ActivityId == activityId && x.IsDeleted == false);
        //        if (projectactivityDetail != null)
        //        {
        //            // Actual end date
        //            projectactivityDetail.MonitoringStatus = !projectactivityDetail.MonitoringStatus;
        //            projectactivityDetail.StatusId = (int)ProjectPhaseType.Completed;

        //            projectactivityDetail.ModifiedDate = DateTime.UtcNow;
        //            projectactivityDetail.ModifiedById = UserId;
        //            projectactivityDetail.IsDeleted = false;

        //            await _uow.ProjectActivityDetailRepository.UpdateAsyn(projectactivityDetail);

        //            response.data.MonitoringStatus = projectactivityDetail.MonitoringStatus.Value;
        //            response.data.ProjectActivityDetails = _mapper.Map<ProjectActivityDetail, ProjectActivityModel>(projectactivityDetail);
        //            response.StatusCode = StaticResource.successStatusCode;
        //            response.Message = StaticResource.SuccessText;
        //        }
        //        else
        //        {
        //            response.StatusCode = StaticResource.failStatusCode;
        //            response.Message = StaticResource.ActivityNotFound;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = StaticResource.failStatusCode;
        //        response.Message = StaticResource.SomethingWrong + ex.Message;
        //    }
        //    return response;
        //}

        public async Task<int> GetActivityOnSchedule(long projectId)
        {

            int totalCount = await _uow.GetDbContext().ProjectActivityDetail
                                                      .CountAsync(a => a.IsDeleted == false &&
                                                                       a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                       a.ParentId == null &&
                                                                       a.PlannedStartDate ==
                                                                       (a.ProjectSubActivityList.Min(y => y.ActualStartDate.Value.Date) != null ?
                                                                            a.ProjectSubActivityList.Min(y => y.ActualStartDate.Value.Date) : DateTime.UtcNow.Date) &&
                                                                       a.PlannedEndDate >= (a.ProjectSubActivityList.Max(y => y.ActualEndDate.Value.Date) != null ?
                                                                            a.ProjectSubActivityList.Max(y => y.ActualEndDate.Value.Date) : DateTime.UtcNow.Date)
                                                                       );
            return totalCount;
        }

        public async Task<int> GetLateStart(long projectId)
        {
            //NOTE: PlannedStart < ActualStartDate
            int totalCount = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                  a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                                  a.ParentId == null &&
                                                                                  a.PlannedStartDate.Value.Date < (a.ProjectSubActivityList.Min(x => x.ActualStartDate.Value.Date) != null ?
                                                                                                                   a.ProjectSubActivityList.Min(x => x.ActualStartDate.Value.Date) : DateTime.UtcNow.Date)
                                                                              );
            return totalCount;
        }

        public async Task<int> GetLateEnd(long projectId)
        {
            //NOTE: PlannedEndDate < ActualEndDate
            int totalCount = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                   a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                                   a.ParentId == null &&
                                                                                   a.PlannedEndDate.Value.Date < (a.ProjectSubActivityList.Min(x => x.ActualEndDate.Value.Date) != null ?
                                                                                                                  a.ProjectSubActivityList.Min(x => x.ActualEndDate.Value.Date) : DateTime.UtcNow.Date)
                                                                              );
            return totalCount;
        }

        public async Task<int> GetSlippage(long projectId)
        {
            // NOTE: PlannedEndDate - ActualEndDate
            int slippage = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                      a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                                      a.ParentId == null
                                                                                      &&
                                                                                      (a.ActualEndDate != null ? a.ActualEndDate.Value.Date : DateTime.UtcNow.Date) >
                                                                                      (a.PlannedEndDate != null ? a.PlannedEndDate.Value.Date : DateTime.UtcNow.Date)
                                                                                      );
            return slippage;
        }

        public async Task<float> GetProgress(long projectId)
        {
            float avg = 0;

            Task<long> totalProjectsTask = _uow.GetDbContext().ProjectActivityDetail
                                                    .LongCountAsync(a => a.IsDeleted == false && 
                                                                         a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                         a.ParentId == null);
            Task<long> completedProjectsTask = _uow.GetDbContext().ProjectActivityDetail
                                                    .LongCountAsync(a => a.IsDeleted == false &&
                                                                         a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                         a.ParentId == null &&
                                                                         a.StatusId == (int)ProjectPhaseType.Completed);
            long totalProjects = await totalProjectsTask;
            long completedProjects = await completedProjectsTask;
            if (totalProjects == 0 || completedProjects == 0)
            {
                avg = 0;
            }
            else
            {
                //Note: Here typecasting is important, else it will always return 0 
                avg = (float)completedProjects / totalProjects * 100;
            }
            return avg;
        }

        public async Task<DateTime?> GetMinimumProjectActivityStartDate(long projectId)
        {
            return await _uow.GetDbContext().ProjectActivityDetail.Where(x => x.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                              x.ParentId == null &&
                                                                              x.IsDeleted == false)
                                                                  .MinAsync(x => x.PlannedStartDate);
        }

        public async Task<DateTime?> GetMaximumProjectActivityEndDate(long projectId)
        {
            return await _uow.GetDbContext().ProjectActivityDetail.Where(x => x.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                              x.ParentId == null &&
                                                                              x.IsDeleted == false)
                                                                  .MaxAsync(x => x.PlannedEndDate);
        }

        public async Task<APIResponse> AllProjectActivityStatus(long projectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                //Task<DateTime?> minStartDate = GetMinimumProjectActivityStartDate(projectId);
                //Task<DateTime?> maxEndDate = GetMaximumProjectActivityEndDate(projectId);

                //Task<int> ActivityOnSchedule = GetActivityOnSchedule(projectId);
                //Task<int> LateStart = GetLateStart(projectId);
                //Task<int> LateEnd = GetLateEnd(projectId);
                //Task<float> Progress = GetProgress(projectId);
                //Task<int> Slippage = GetSlippage(projectId);

                //DateTime minDate = await minStartDate ?? DateTime.UtcNow;
                //DateTime maxDate = await maxEndDate ?? DateTime.UtcNow;

                //ProjectActivityStatusModel obj = new ProjectActivityStatusModel
                //{
                //    ProjectDuration = (maxDate.Date - minDate.Date).Days,
                //    ActivityOnSchedule = await ActivityOnSchedule,
                //    LateStart = await LateStart,
                //    LateEnd = await LateEnd,
                //    Progress = await Progress,
                //    Slippage = await Slippage,
                //};

                ProjectActivityStatusModel obj = new ProjectActivityStatusModel
                {
                    ProjectDuration = 0,
                    ActivityOnSchedule = 0,
                    LateStart = 0,
                    LateEnd = 0,
                    Progress = 0,
                    Slippage = 0
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

        /// <summary>
        /// Upload project activity documents 28/03/2019 pk
        /// </summary>
        /// <param name="file"></param>
        /// <param name="UserId"></param>
        /// <param name="activityId"></param>
        /// <param name="fileName"></param>
        /// <param name="logginUserEmailId"></param>
        /// <param name="ext"></param>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public async Task<APIResponse> UploadProjectActivityDocumentFile(IFormFile file, string UserId, long activityId, string fileName, string logginUserEmailId, string ext, int statusID)
        {
            APIResponse response = new APIResponse();
            try
            {
                ActivityDocumentDetailModel activityModel = new ActivityDocumentDetailModel();
                var projectDetail = await _uow.GetDbContext().ProjectActivityDetail
                                                           .Include(x => x.ProjectBudgetLineDetail.ProjectDetail)
                                                           .FirstOrDefaultAsync(x => x.ActivityId == activityId && x.IsDeleted == false);

                string folderName = projectDetail?.ProjectBudgetLineDetail.ProjectDetail.ProjectCode;

                string googleApplicationCredentail = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
                if (googleApplicationCredentail == null)
                {
                    string GoogleServiceAccountDirectory = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                    GoogleServiceAccountDirectory = GoogleServiceAccountDirectory.Replace(@"\", "/");
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", GoogleServiceAccountDirectory);
                }
                using (Stream objStream = new FileStream(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"), FileMode.Open, FileAccess.Read))
                {
                    string bucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");
                    if (bucketName != null)
                    {
                        ActivityDocumentsDetail docObj = new ActivityDocumentsDetail();

                        string folderWithProposalFile = StaticResource.ProjectsFolderName + "/" + folderName + "/" + fileName;
                        string uploadedFileResponse = await GCBucket.UploadOtherProposalDocuments(bucketName, folderWithProposalFile, file, fileName, ext);
                        if (!string.IsNullOrEmpty(uploadedFileResponse))
                        {
                            docObj.ActivityId = activityId;
                            docObj.ActivityDocumentsFilePath = uploadedFileResponse;
                            docObj.StatusId = statusID;
                            docObj.CreatedById = UserId;
                            docObj.IsDeleted = false;
                            docObj.CreatedDate = DateTime.UtcNow;

                            await _uow.ActivityDocumentsDetailRepository.AddAsyn(docObj);
                        }

                        ActivityDocumentsDetailModel obj = new ActivityDocumentsDetailModel
                        {
                            ActtivityDocumentId = docObj.ActtivityDocumentId,
                            ActivityDocumentsFilePath = docObj.ActivityDocumentsFilePath,
                            ActivityDocumentsFileName = docObj.ActivityDocumentsFilePath.Split('/').Last(),
                            StatusId = docObj.StatusId,
                            ActivityId = docObj.ActivityId,
                        };

                        response.data.activityDocumnentDetail = obj;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.BucketNameNotFound;
                    }
                }
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
                var listobj = await _uow.GetDbContext().ActivityDocumentsDetail.Where(x => x.ActivityId == activityId && x.IsDeleted == false)
                    .Select(x => new ActivityDocumentDetailModel()
                    {
                        ActivityId = x.ActivityId,
                        StatusId = x.StatusId,
                        ActivityDocumentsFilePath = x.ActivityDocumentsFilePath,
                        ActivityDocumentsFileName = x.ActivityDocumentsFilePath.Substring(x.ActivityDocumentsFilePath.LastIndexOf('/') + 1),
                        ActtivityDocumentId = x.ActtivityDocumentId
                    }).ToListAsync();

                apiResponse.data.ActivityDocumentDetailModel = listobj;
                apiResponse.StatusCode = StaticResource.successStatusCode;
                apiResponse.Message = StaticResource.SuccessText;

            }
            catch (Exception ex)
            {
                apiResponse.StatusCode = StaticResource.failStatusCode;
                apiResponse.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return apiResponse;
        }

        /// <summary>
        /// Its a test API to check file upload functionality (DEMO)
        /// NOT FOR PRODUCTION
        /// </summary>
        /// <param name="filesData"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<APIResponse> UploadFileDemo(IFormFile filesData, string userId, string userName)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                var file = filesData;
                string fileName = filesData.FileName;
                string ext = Path.GetExtension(fileName).ToLower();

                // validate file type 
                if (ext == ".doc" || ext == ".docx" || ext == ".txt" || ext == ".xlsx" || ext == ".pdf")
                {
                    // Auth 
                    var webRootfilePath = _hostingEnvironment.WebRootPath;

                    var path = Path.Combine(webRootfilePath, "demo.xlsx");


                    string GoogleServiceAccountDirectory = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                    GoogleServiceAccountDirectory = GoogleServiceAccountDirectory.Replace(@"\", "/");

                    Console.WriteLine($"Linux GoogleServiceAccountDirectory : {GoogleServiceAccountDirectory}");

                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", GoogleServiceAccountDirectory);
                    using (Stream objStream = new FileStream(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"), FileMode.Open, FileAccess.Read))
                    {
                        Console.WriteLine($"obj stream:{"GOOGLE_APPLICATION_CREDENTIALS"}");
                        //UploadFile(StaticResource.BucketName, @"D:\poonam\newdoc.docx", "abc");

                        var intdata = GCBucket.UploadFile(Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME"), path);
                    }

                    // Upload

                }
                else
                {
                    apiResponse.StatusCode = StaticResource.failStatusCode;
                    apiResponse.Message = StaticResource.FileText;
                }


            }
            catch (Exception ex)
            {
                apiResponse.StatusCode = StaticResource.failStatusCode;
                apiResponse.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return apiResponse;
        }

        /// <summary>
        /// Delete Activity Document 29/03/2019
        /// </summary>
        /// <param name="activityDocumentId"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteActivityDocument(long activityDocumentId, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                string bucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");
                if (bucketName != null)
                {
                    // Get document
                    var documentDetail = await _uow.ActivityDocumentsDetailRepository.FindAsync(x => x.ActtivityDocumentId == activityDocumentId);

                    if (documentDetail != null)
                    {
                        if (await GCBucket.DeleteObject(bucketName, documentDetail.ActivityDocumentsFilePath))
                        {
                            documentDetail.IsDeleted = true;
                            documentDetail.ModifiedById = userId;
                            documentDetail.ModifiedDate = DateTime.UtcNow;

                            await _uow.ActivityDocumentsDetailRepository.UpdateAsyn(documentDetail);

                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = StaticResource.SuccessText;
                        }
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.BucketNameNotFound;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex;
            }
            return response;
        }


        #endregion

        #region GetProjectActivityAdvanceFilterList
        public APIResponse GetProjectActivityAdvanceFilterList(ActivityAdvanceFilterModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                var activityList = _uow.GetDbContext().ProjectActivityDetail
                                          .Where(v => v.IsDeleted == false &&
                                                      v.ProjectBudgetLineDetail.ProjectId == model.ProjectId &&
                                                      v.ParentId == null
                                          )
                                          .OrderBy(x => x.ActivityId)
                                          .AsQueryable();

                activityList = FilterAdvanceList(activityList, model);



                var activityDetaillist = activityList.Select(b => new ProjectActivityModel
                {
                    ActivityId = b.ActivityId,
                    ActivityName = b.ActivityName,
                    ActivityDescription = b.ActivityDescription,
                    BudgetLineId = b.ProjectBudgetLineDetail.BudgetLineId,
                    BudgetName = b.ProjectBudgetLineDetail.BudgetName,
                    EmployeeID = b.EmployeeDetail.EmployeeID,
                    EmployeeName = b.EmployeeDetail.EmployeeName,
                    StatusId = b.ActivityStatusDetail.StatusId,
                    StatusName = b.ActivityStatusDetail.StatusName,
                    PlannedStartDate = b.PlannedStartDate,
                    PlannedEndDate = b.PlannedEndDate,
                    Recurring = b.Recurring,
                    RecurringCount = b.RecurringCount,
                    RecurrinTypeId = b.RecurrinTypeId,
                }).OrderByDescending(x => x.ActivityId)
                  .ToList();
                response.data.ProjectActivityList = activityDetaillist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex;
            }
            return response;
        }
        public IQueryable<ProjectActivityDetail> FilterAdvanceList(IQueryable<ProjectActivityDetail> activityList, ActivityAdvanceFilterModel model)
        {
            if (model.PlannedStartDate.HasValue)
            {
                activityList = activityList.Where(x => x.PlannedStartDate.Value.Date >= model.PlannedStartDate.Value.Date);
            }
            if (model.PlannedEndDate.HasValue)
            {
                activityList = activityList.Where(x => x.PlannedEndDate.Value.Date <= model.PlannedEndDate.Value.Date);
            }
            if (model.ActualStartDate.HasValue)
            {
                activityList = activityList.Where(x => x.ActualStartDate.Value.Date >= model.ActualStartDate.Value.Date);
            }
            if (model.ActualEndDate.HasValue)
            {
                activityList = activityList.Where(x => x.ActualEndDate.Value.Date <= model.ActualEndDate.Value.Date);
            }
            if (model.BudgetLineId.Any())
            {
                activityList = activityList.Where(x => model.BudgetLineId.Contains(x.BudgetLineId));
            }
            if (model.AssigneeId.Any())
            {
                activityList = activityList.Where(x => model.AssigneeId.Contains(x.EmployeeID));
            }

            if (model.Planning)
            {
                activityList.Where(x => x.StatusId == (int)ProjectPhaseType.Planning);
            }
            if (model.Implementation)
            {
                activityList.Where(x => x.StatusId == (int)ProjectPhaseType.Implementation);
            }
            if (model.Completed)
            {
                activityList.Where(x => x.StatusId == (int)ProjectPhaseType.Completed);
            }
            if (model.ProgressRange.Any())
            {
            }
            if (model.SleepageRange.Any())
            {
            }
            if (model.DurationRange.Any())
            {
            }

            if (model.LateStart)
            {
                //NOTE: PlannedStartDate < ActualStartDate
                activityList.Where(x => x.PlannedStartDate.Value.Date < x.ProjectSubActivityList.Min(y => y.ActualStartDate.Value.Date));
            }
            if (model.LateEnd)
            {
                //NOTE: PlannedEndDate < ActualEndDate
                activityList.Where(x => x.PlannedEndDate.Value.Date <
                                            (x.ProjectSubActivityList.Min(y => y.ActualEndDate.Value.Date) != null ?
                                            x.ProjectSubActivityList.Min(y => y.ActualEndDate.Value.Date) : DateTime.UtcNow.Date));
            }
            if (model.OnSchedule)
            {
                //NOTE: PlannedStartDate >= ActualStartDate &&  PlannedEndDate >= ActualEndDate
                activityList.Where(x => x.PlannedStartDate.Value.Date >= 
                                            (x.ProjectSubActivityList.Min(y => y.ActualStartDate.Value.Date) != null ? 
                                             x.ProjectSubActivityList.Min(y => y.ActualStartDate.Value.Date) : DateTime.UtcNow.Date) &&
                                        x.PlannedEndDate.Value.Date >= 
                                            (x.ProjectSubActivityList.Max(y => y.ActualEndDate.Value.Date) != null ?
                                             x.ProjectSubActivityList.Max(y => y.ActualEndDate.Value.Date) : DateTime.UtcNow.Date)
                );
            }
            return activityList;
        }

        public async Task<float> FilterProgressRange(long projectId, int minRange, int maxRange)
        {
            float avg = 0;

            Task<long> totalProjectsTask = _uow.GetDbContext().ProjectActivityDetail
                                                    .LongCountAsync(a => a.IsDeleted == false &&
                                                      a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                      a.ParentId == null);
            Task<long> completedProjectsTask = _uow.GetDbContext().ProjectActivityDetail
                                                    .LongCountAsync(a => a.IsDeleted == false &&
                                                      a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                      a.ParentId == null &&
                                                      a.StatusId == (int)ProjectPhaseType.Completed);
            long totalProjects = await totalProjectsTask;
            long completedProjects = await completedProjectsTask;
            if (totalProjects == 0 || completedProjects == 0)
            {
                avg = 0;
            }
            else
            {
                //Note: Here typecasting is important, else it will always return 0 
                avg = (float)completedProjects / totalProjects * 100;
            }
            return avg;
        }

        #endregion


        #region projectMonitoringActivity
        public async Task<APIResponse> AddProjectMonitoringReview(ProjectMonitoringViewModel model, string UserId)
        {
            APIResponse response = new APIResponse();

            try
            {
                ProjectMonitoringReviewDetail projectMonitoringReviewDetail = new ProjectMonitoringReviewDetail();
                projectMonitoringReviewDetail.ActivityId = model.ActivityId;
                projectMonitoringReviewDetail.CreatedById = UserId;
                projectMonitoringReviewDetail.ProjectId = model.ProjectId;
                projectMonitoringReviewDetail.MonitoringDate = model.MonitoringDate;
                projectMonitoringReviewDetail.CreatedDate = DateTime.UtcNow;
                projectMonitoringReviewDetail.IsDeleted = false;
                projectMonitoringReviewDetail.NegativePoints = model.NegativePoints;
                projectMonitoringReviewDetail.PostivePoints = model.PositivePoints;
                projectMonitoringReviewDetail.Recommendations = model.Recommendations;
                projectMonitoringReviewDetail.Remarks = model.Remarks;

                await _uow.GetDbContext().ProjectMonitoringReviewDetail.AddAsync(projectMonitoringReviewDetail);
                await _uow.GetDbContext().SaveChangesAsync();

                foreach (var item in model.MonitoringReviewModel)
                {
                    ProjectMonitoringIndicatorDetail monitoringIndicatorDetail = new ProjectMonitoringIndicatorDetail();
                    monitoringIndicatorDetail.CreatedById = UserId;
                    monitoringIndicatorDetail.CreatedDate = DateTime.UtcNow;
                    monitoringIndicatorDetail.IsDeleted = false;
                    monitoringIndicatorDetail.ProjectMonitoringReviewId = projectMonitoringReviewDetail.ProjectMonitoringReviewId;
                    monitoringIndicatorDetail.ProjectIndicatorId = item.ProjectIndicatorId;

                    await _uow.GetDbContext().ProjectMonitoringIndicatorDetail.AddAsync(monitoringIndicatorDetail);
                    await _uow.GetDbContext().SaveChangesAsync();

                    foreach (var obj in item.IndicatorQuestions)
                    {
                        ProjectMonitoringIndicatorQuestions monitoringQuestions = new ProjectMonitoringIndicatorQuestions();
                        monitoringQuestions.IsDeleted = false;
                        monitoringQuestions.CreatedDate = DateTime.UtcNow;
                        monitoringQuestions.CreatedById = UserId;
                        monitoringQuestions.QuestionId = obj.QuestionId;
                        monitoringQuestions.Verification = obj.Verification;
                        monitoringQuestions.VerificationId = obj.VerificationId;
                        monitoringQuestions.MonitoringIndicatorId = monitoringIndicatorDetail.MonitoringIndicatorId;
                        monitoringQuestions.Score = obj.Score;

                        await _uow.GetDbContext().ProjectMonitoringIndicatorQuestions.AddAsync(monitoringQuestions);
                        await _uow.GetDbContext().SaveChangesAsync();
                    }
                }

                
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

        public async Task<APIResponse> GetProjectMonitoringList(long activityId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var projectMonitoring = await _uow.GetDbContext().ProjectMonitoringReviewDetail
                                                                .Include(y => y.ProjectMonitoringIndicatorDetail)
                                                                .ThenInclude(z => z.ProjectMonitoringIndicatorQuestions)
                                                                .ThenInclude(x=> x.ProjectIndicatorQuestions)
                                                                .Where(x => x.IsDeleted == false && x.ActivityId == activityId)
                                                                .Select(x => new ProjectMonitoringViewModel {
                                                                    ActivityId = x.ActivityId,
                                                                    NegativePoints = x.NegativePoints,
                                                                    PositivePoints = x.PostivePoints,
                                                                    ProjectId = x.ProjectId,
                                                                    MonitoringDate= x.MonitoringDate,
                                                                    Recommendations = x.Recommendations,
                                                                    Remarks = x.Remarks,
                                                                    ProjectMonitoringReviewId = x.ProjectMonitoringReviewId,
                                                                    MonitoringReviewModel = x.ProjectMonitoringIndicatorDetail
                                                                                            .Where(y => y.IsDeleted == false)
                                                                                            .Select(y => new ProjectMonitoringReviewModel {
                                                                                                ProjectIndicatorId = y.ProjectIndicatorId,
                                                                                                MonitoringIndicatorId = y.MonitoringIndicatorId,
                                                                                                IndicatorName= y.ProjectIndicators.IndicatorName,
                                                                                                IndicatorQuestions = y.ProjectMonitoringIndicatorQuestions
                                                                                                                      .Where(z => z.IsDeleted == false)
                                                                                                                      .Select(z => new ProjectMonitoringQuestionModel
                                                                                                                      {
                                                                                                                        MonitoringIndicatorQuestionId= z.Id,
                                                                                                                        QuestionId = z.QuestionId,
                                                                                                                        Score = z.Score,
                                                                                                                        VerificationId = z.VerificationId,
                                                                                                                        Verification= z.Verification,
                                                                                                                        Question= z.ProjectIndicatorQuestions.IndicatorQuestion
                                                                                                                      }).ToList()
                                                                                            }).ToList()
                                                                }).ToListAsync();

                if (projectMonitoring.Any())
                {
                    foreach (var item in projectMonitoring)
                    {
                        if (item.MonitoringReviewModel.Any())
                        {
                            item.MonitoringReviewModel.ForEach(x => x.TotalScore = x.IndicatorQuestions.Sum(y => y.Score));
                            item.Rating = Math.Round(((float)item.MonitoringReviewModel.Sum(y => y.TotalScore.Value) / (float)item.MonitoringReviewModel.Count),2);
                        }
                    }
                }

                response.data.ProjectMonitoring = projectMonitoring;
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

        public async Task<APIResponse> GetProjectMonitoringByMonitoringId(long Id)
        {
            APIResponse response = new APIResponse();

            try
            {

                var monitoring = await _uow.GetDbContext().ProjectMonitoringReviewDetail
                                                .Include(y => y.ProjectMonitoringIndicatorDetail)
                                                .ThenInclude(x => x.ProjectMonitoringIndicatorQuestions)
                                                .ThenInclude(x => x.ProjectIndicatorQuestions)
                                                .Include(x => x.ProjectMonitoringIndicatorDetail)
                                                .ThenInclude(y => y.ProjectIndicators)
                                                .FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectMonitoringReviewId == Id);



                ProjectMonitoringViewModel obj = new ProjectMonitoringViewModel();

                obj.ActivityId = monitoring.ActivityId;
                obj.MonitoringDate = monitoring.MonitoringDate;
                obj.NegativePoints = monitoring.NegativePoints;
                obj.PositivePoints = monitoring.PostivePoints;
                obj.ProjectId = monitoring.ProjectId;
                obj.ProjectMonitoringReviewId = monitoring.ProjectMonitoringReviewId;
                obj.Recommendations = monitoring.Recommendations;
                obj.Remarks = monitoring.Remarks;

                if (monitoring.ProjectMonitoringIndicatorDetail.Any())
                {
                    foreach (var item in monitoring.ProjectMonitoringIndicatorDetail)
                    {
                        ProjectMonitoringReviewModel model = new ProjectMonitoringReviewModel();
                        model.IndicatorName = item.ProjectIndicators.IndicatorName;
                        model.ProjectIndicatorId = item.ProjectIndicatorId;
                        model.MonitoringIndicatorId = item.MonitoringIndicatorId;

                        if (item.ProjectMonitoringIndicatorQuestions.Any())
                        {
                            foreach (var question in item.ProjectMonitoringIndicatorQuestions)
                            {
                                ProjectMonitoringQuestionModel questions = new ProjectMonitoringQuestionModel
                                {
                                    MonitoringIndicatorQuestionId = question.Id,
                                    Question = question.ProjectIndicatorQuestions.IndicatorQuestion,
                                    QuestionId = question.QuestionId,
                                    Score = question.Score,
                                    Verification = question.Verification,
                                    VerificationId = question.VerificationId
                                };
                                model.IndicatorQuestions.Add(questions);
                            }
                        }
                        obj.MonitoringReviewModel.Add(model);
                    }
                }

                response.data.ProjectMonitoringModel = obj;
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

        public async Task<APIResponse> EditProjectMonitoringByMonitoringId(ProjectMonitoringViewModel model, string UserId)
        {
            APIResponse response = new APIResponse();

            try
            {

                var monitoring = await _uow.GetDbContext().ProjectMonitoringReviewDetail
                                                .FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectMonitoringReviewId == model.ProjectMonitoringReviewId);

                // monitoring.ActivityId = model.ActivityId;
                monitoring.ModifiedById = UserId;
                // monitoring.ProjectId = model.ProjectId;
                monitoring.MonitoringDate = model.MonitoringDate;
                monitoring.ModifiedDate = DateTime.UtcNow;
                monitoring.IsDeleted = false;
                monitoring.NegativePoints = model.NegativePoints;
                monitoring.PostivePoints = model.PositivePoints;
                monitoring.Recommendations = model.Recommendations;
                monitoring.Remarks = model.Remarks;

                _uow.GetDbContext().ProjectMonitoringReviewDetail.Update(monitoring);
                await _uow.GetDbContext().SaveChangesAsync();

                List<ProjectMonitoringIndicatorDetail> indicators = await _uow.GetDbContext().ProjectMonitoringIndicatorDetail.Where(x => x.IsDeleted == false && x.ProjectMonitoringReviewId == monitoring.ProjectMonitoringReviewId).ToListAsync();

                indicators.ForEach(x => x.IsDeleted = true);

                _uow.GetDbContext().ProjectMonitoringIndicatorDetail.UpdateRange(indicators);
                await _uow.GetDbContext().SaveChangesAsync();

                foreach (var item in model.MonitoringReviewModel)
                {
                    ProjectMonitoringIndicatorDetail monitoringIndicatorDetail = new ProjectMonitoringIndicatorDetail();

                    if (item.MonitoringIndicatorId == null)
                    {
                        monitoringIndicatorDetail.CreatedById = UserId;
                        monitoringIndicatorDetail.CreatedDate = DateTime.UtcNow;
                        monitoringIndicatorDetail.IsDeleted = false;
                        monitoringIndicatorDetail.ProjectMonitoringReviewId = monitoring.ProjectMonitoringReviewId;
                        monitoringIndicatorDetail.ProjectIndicatorId = item.ProjectIndicatorId;

                        await _uow.GetDbContext().ProjectMonitoringIndicatorDetail.AddAsync(monitoringIndicatorDetail);
                        await _uow.GetDbContext().SaveChangesAsync();
                    }
                    else
                    {
                        monitoringIndicatorDetail = await _uow.GetDbContext().ProjectMonitoringIndicatorDetail
                                                                                    .FirstOrDefaultAsync(x => x.MonitoringIndicatorId == item.MonitoringIndicatorId);

                        monitoringIndicatorDetail.ModifiedById = UserId;
                        monitoringIndicatorDetail.IsDeleted = false;
                        monitoringIndicatorDetail.ModifiedDate = DateTime.UtcNow;
                        monitoringIndicatorDetail.ProjectIndicatorId = item.ProjectIndicatorId;
                        _uow.GetDbContext().ProjectMonitoringIndicatorDetail.Update(monitoringIndicatorDetail);
                        await _uow.GetDbContext().SaveChangesAsync();
                    }

                    if (item.IndicatorQuestions.Any())
                    {
                        foreach (var obj in item.IndicatorQuestions)
                        {
                            ProjectMonitoringIndicatorQuestions monitoringQuestions = new ProjectMonitoringIndicatorQuestions();

                            if (obj.MonitoringIndicatorQuestionId == null)
                            {
                                monitoringQuestions.IsDeleted = false;
                                monitoringQuestions.CreatedDate = DateTime.UtcNow;
                                monitoringQuestions.CreatedById = UserId;
                                monitoringQuestions.QuestionId = obj.QuestionId;
                                monitoringQuestions.Verification = obj.Verification;
                                monitoringQuestions.VerificationId = obj.VerificationId;
                                monitoringQuestions.MonitoringIndicatorId = monitoringIndicatorDetail.MonitoringIndicatorId;
                                monitoringQuestions.Score = obj.Score;
                                await _uow.GetDbContext().ProjectMonitoringIndicatorQuestions.AddAsync(monitoringQuestions);
                                await _uow.GetDbContext().SaveChangesAsync();
                            }
                            else
                            {
                                monitoringQuestions = await _uow.GetDbContext().ProjectMonitoringIndicatorQuestions
                                                                               .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == obj.MonitoringIndicatorQuestionId);


                                monitoringQuestions.ModifiedDate = DateTime.UtcNow;
                                monitoringQuestions.ModifiedById = UserId;
                                monitoringQuestions.QuestionId = obj.QuestionId;
                                monitoringQuestions.Score = obj.Score;
                                monitoringQuestions.Verification = obj.Verification;
                                monitoringQuestions.VerificationId = obj.VerificationId;
                                _uow.GetDbContext().ProjectMonitoringIndicatorQuestions.Update(monitoringQuestions);
                                await _uow.GetDbContext().SaveChangesAsync();
                            }
                        }
                    }
                }

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
       
        #endregion


        #region "Project activity extension"

        public async Task<APIResponse> GetProjectActivityExtension(long activityId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var extensionList = await _uow.GetDbContext().ProjectActivityExtensions
                                                             .Where(x => x.IsDeleted == false &&
                                                                         x.ActivityId == activityId
                                                                   )
                                                              .ToListAsync();

                response.data.ProjectActivityExtensionsList = extensionList;
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


        /// <summary>
        /// AddProjectExtension of project activity detail 03/05/2019 pk
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddProjectActivityExtension(ProjectExtensionModel model,string userId)
         {
            APIResponse response = new APIResponse();
            try
            {
                ProjectActivityExtensions extensionObj = new ProjectActivityExtensions();
                extensionObj.ActivityId = model.ActivityId;
                extensionObj.StartDate = model.StartDate;
                extensionObj.EndDate = model.EndDate;
                extensionObj.Description = model.Description;
                extensionObj.CreatedById = userId;
                extensionObj.IsDeleted = false;
                extensionObj.CreatedDate = DateTime.UtcNow;

                await _uow.ProjectActivityExtensionsRepository.AddAsyn(extensionObj);

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


        public async Task<APIResponse> EditProjectActivityExtension(ProjectExtensionModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ProjectActivityExtensions extensionDetail = await _uow.GetDbContext().ProjectActivityExtensions.FirstOrDefaultAsync(x => x.ActivityId == model.ActivityId && x.IsDeleted == false);

                if (extensionDetail != null)
                {
                    extensionDetail.ActivityId = model.ActivityId;
                    extensionDetail.StartDate = model.StartDate;
                    extensionDetail.EndDate = model.EndDate;
                    extensionDetail.Description = model.Description;
                    extensionDetail.IsDeleted = false;
                    extensionDetail.ModifiedById = userId;
                    extensionDetail.ModifiedDate = DateTime.UtcNow;

                    await _uow.ProjectActivityExtensionsRepository.UpdateAsyn(extensionDetail);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    throw new Exception(StaticResource.ActivityExtensionNotFound);
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }


        public async Task<APIResponse> DeleteProjectActivityExtension(long extensionId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ProjectActivityExtensions extensionDetail = await _uow.GetDbContext().ProjectActivityExtensions.FirstOrDefaultAsync(x => x.ExtensionId == extensionId && x.IsDeleted == false);

                if (extensionDetail != null)
                {
                    extensionDetail.IsDeleted = true;
                    extensionDetail.ModifiedById = userId;
                    extensionDetail.ModifiedDate = DateTime.UtcNow;

                    await _uow.ProjectActivityExtensionsRepository.UpdateAsyn(extensionDetail);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else {
                    throw new Exception(StaticResource.ActivityExtensionNotFound);
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        #endregion


        public async Task<APIResponse> GetProjectActivityDetailList(int parentId)

        {

            APIResponse response = new APIResponse();
            try
            {
                var projectActivityDetails = await _uow.GetDbContext().ProjectActivityDetail
                                          .Include(p => p.ProjectSubActivityList)
                                          .FirstOrDefaultAsync(v => v.IsDeleted == false &&
                                                      v.ActivityId == parentId
                                          );

                List<ProjectSubActivityListModel> activityDetaillist = new List<ProjectSubActivityListModel>();

                activityDetaillist = projectActivityDetails.ProjectSubActivityList.Select(b => new ProjectSubActivityListModel
                {
                    ActivityId = b.ActivityId,
                    BudgetLineId = b.BudgetLineId,
                    EmployeeID = b.EmployeeID,
                    PlannedStartDate = b.PlannedStartDate,
                    PlannedEndDate = b.PlannedEndDate,
                    Recurring = b.Recurring,
                    RecurrinTypeId = b.RecurrinTypeId,
                    IsCompleted = b.IsCompleted
                }).OrderByDescending(x => x.ActivityId)
                  .ToList();
                response.data.ProjectSubActivityListModel = activityDetaillist;
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

        #region "Project SubActivity Details "

        public async Task<APIResponse> GetProjectSubActivityDetails(int parentId)

        {

            APIResponse response = new APIResponse();
            try
            {
                var projectActivityDetails = await _uow.GetDbContext().ProjectActivityDetail
                                          .Include(p => p.ProjectSubActivityList)
                                          .FirstOrDefaultAsync(v => v.IsDeleted == false &&
                                                      v.ActivityId == parentId
                                          );

                List<ProjectSubActivityListModel> activityDetaillist = new List<ProjectSubActivityListModel>();

                activityDetaillist = projectActivityDetails.ProjectSubActivityList.Select(b => new ProjectSubActivityListModel
                {
                    ActivityId = b.ActivityId,
                    BudgetLineId = b.BudgetLineId,
                    EmployeeID = b.EmployeeID,
                    PlannedStartDate = b.PlannedStartDate,
                    PlannedEndDate = b.PlannedEndDate,
                    Recurring = b.Recurring,
                    RecurrinTypeId = b.RecurrinTypeId,
                    IsCompleted = b.IsCompleted,
                    ActivityDescription = b.ActivityDescription,
                    ChallengesAndSolutions = b.ChallengesAndSolutions,
                    Target = b.Target,
                    Achieved = b.Achieved,
                    ActualStartDate = b.ActualStartDate,
                    ActualEndDate = b.ActualEndDate,
                }).OrderByDescending(x => x.ActivityId)
                  .ToList();
                response.data.ProjectSubActivityListModel = activityDetaillist;
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

        public async Task<APIResponse> AddProjectSubActivityDetail(ProjectActivityModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ProjectActivityDetail obj = _mapper.Map<ProjectActivityModel, ProjectActivityDetail>(model);
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                await _uow.ProjectActivityDetailRepository.AddAsyn(obj);
                response.data.ProjectActivityDetail = obj;
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

        public async Task<APIResponse> EditProjectSubActivityDetail(ProjectSubActivityListModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ProjectActivityDetail obj = await _uow.GetDbContext().ProjectActivityDetail.FirstOrDefaultAsync(x => x.ActivityId == model.ActivityId && x.IsDeleted == false);
                if (obj != null)
                {
                    obj.ActivityDescription = model.ActivityDescription;
                    obj.ChallengesAndSolutions = model.ChallengesAndSolutions;
                    obj.EmployeeID = model.EmployeeID;
                    obj.IsCompleted = model.IsCompleted;
                    obj.BudgetLineId = model.BudgetLineId;
                    obj.Achieved = model.Achieved;
                    obj.Target = model.Target;
                    obj.ModifiedDate = DateTime.UtcNow;
                    obj.IsDeleted = false;
                    obj.ModifiedById = UserId;
                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(obj);
                }
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

        public async Task<APIResponse> ProjectSubActivityIscomplete(long activityId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ProjectActivityDetail obj = await _uow.GetDbContext().ProjectActivityDetail.FirstOrDefaultAsync(x => x.ActivityId == activityId && x.IsDeleted == false);
                if (obj != null)
                {

                    obj.IsCompleted = !obj.IsCompleted;
                    obj.ModifiedDate = DateTime.UtcNow;
                    obj.IsDeleted = false;
                    obj.ModifiedById = UserId;
                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(obj);
                }
                response.data.ProjectActivityDetail = obj;
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

        public async Task<APIResponse> StartProjectSubActivity(long activityId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ProjectActivityDetail obj = await _uow.GetDbContext().ProjectActivityDetail.FirstOrDefaultAsync(x => x.ActivityId == activityId && x.IsDeleted == false);
                if (obj != null)
                {
                    obj.StatusId = (int)ProjectPhaseType.Implementation;
                    obj.ActualStartDate = DateTime.UtcNow;
                    obj.ModifiedDate = DateTime.UtcNow;
                    obj.IsDeleted = false;
                    obj.ModifiedById = UserId;
                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(obj);
                }
                response.data.ProjectActivityDetail = obj;
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
        public async Task<APIResponse> EndProjectSubActivity(long activityId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ProjectActivityDetail obj = await _uow.GetDbContext().ProjectActivityDetail.FirstOrDefaultAsync(x => x.ActivityId == activityId && x.IsDeleted == false);
                if (obj != null)
                {

                    obj.ActualEndDate = DateTime.UtcNow;
                    obj.ModifiedDate = DateTime.UtcNow;
                    obj.IsDeleted = false;
                    obj.ModifiedById = UserId;
                    await _uow.ProjectActivityDetailRepository.UpdateAsyn(obj);
                }
                response.data.ProjectActivityDetail = obj;
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


        #endregion

    }
}
