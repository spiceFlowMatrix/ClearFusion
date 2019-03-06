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
                    projectactivityDetail.StatusId = (int)ProjectPhaseType.Implementation;

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

        public async Task<int> GetActivityOnSchedule()
        {

            int totalCount = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                   a.StartDate == (a.ActualStartDate.Value.Date != null ?
                                                                                   a.ActualStartDate.Value.Date : DateTime.UtcNow.Date));
            return totalCount;
        }

        public async Task<int> GetLateStart()
        {

            int totalCount = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                  a.StartDate.Value.Date < (a.ActualStartDate != null ?
                                                                                                           a.ActualStartDate.Value.Date :
                                                                                                           DateTime.UtcNow.Date)
                                                                              );
            return totalCount;

        }

        public async Task<int> GetLateEnd()
        {

            int totalCount = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                   (a.EndDate.Value.Date != null ? a.EndDate.Value.Date : DateTime.UtcNow.Date) < (
                                                                                   a.ActualEndDate.Value.Date != null ?
                                                                                   a.ActualEndDate.Value.Date : DateTime.UtcNow.Date));
            return totalCount;
        }

        public async Task<int> GetSlippage()
        {

            int slippage = await _uow.GetDbContext().ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                      (a.ActualEndDate.Value.Date != null ?
                                                                                      a.ActualEndDate.Value.Date : DateTime.UtcNow.Date) >
                                                                                      (a.EndDate.Value.Date != null ? a.EndDate.Value.Date : DateTime.UtcNow.Date));
            return slippage;
        }

        public async Task<float> GetProgress()
        {

            float avg = 0;
            float totalImplementationProgress = 0;
            float totalMonitoringProgrss = 0;
            var slippage = await _uow.GetDbContext().ProjectActivityDetail
                                                    .Where(a => a.IsDeleted == false)
                                                    .Select(x => new
                                                    {
                                                        x.ImplementationProgress,
                                                        x.MonitoringProgress
                                                    })
                                                    .ToListAsync();

            if (slippage != null)
            {
                totalImplementationProgress = slippage.Sum(x => x.ImplementationProgress ?? 0);
                totalMonitoringProgrss = slippage.Sum(x => x.MonitoringProgress ?? 0);
                avg = (totalImplementationProgress + totalMonitoringProgrss) / 2;
            }

            return avg;
        }

        public async Task<APIResponse> AllProjectActivityStatus()
        {
            APIResponse response = new APIResponse();
            try
            {
                Task<int> ActivityOnSchedule = GetActivityOnSchedule();
                Task<int> LateStart = GetLateStart();
                Task<int> LateEnd = GetLateEnd();
                Task<float> Progress = GetProgress();
                Task<int> Slippage = GetSlippage();

                ProjectActivityStatusModel obj = new ProjectActivityStatusModel
                {
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

        public APIResponse UploadDocumentFile(IFormFile file, string UserId, long activityId, string fileName, string logginUserEmailId, string ext)
        {
            APIResponse response = new APIResponse();
            try
            {

                ProjectActivityModel model = new ProjectActivityModel();
                // ProjectActivityDetail list = _uow.GetDbContext().ProjectActivityDetail.FirstOrDefault(x => x.ActivityId == activityId && x.IsDeleted == false);
                var folderName = _uow.GetDbContext().ProjectActivityDetail.Include(x => x.ProjectBudgetLineDetail)
                                                                      .FirstOrDefault(x => x.ActivityId == activityId && x.IsDeleted == false)
                                                                      .ProjectBudgetLineDetail.ProjectId
                                                                     ;

                Console.WriteLine("------Before Credential path Upload----------");


                //read credientials
                // string googleCredentialPathFile = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                //string googleCredentialPathFile1 = Path.Combine(Directory.GetCurrentDirectory(), StaticResource.googleCredential + StaticResource.credentialsJsonFile);
                //Console.WriteLine(googleCredentialPathFile1);

                Console.WriteLine("------------After Credential path Upload-------------");

                //GoogleCredential result = new GoogleCredential();
                //using (StreamReader files = File.OpenText(googleCredentialPathFile1))
                //using (JsonTextReader reader = new JsonTextReader(files))
                //{
                //    JObject o2 = (JObject)JToken.ReadFrom(reader);

                //    result = o2["GoogleCredential"].ToObject<GoogleCredential>();
                //}

                var ifExistdata = _uow.GetDbContext().ProjectActivityDetail.FirstOrDefault(x => x.ActivityId == activityId && x.IsDeleted == false);
                if (ifExistdata != null)
                {
                    //model = GCBucket.UploadDocument(folderName, file, fileName, result, EmailID, logginUserEmailId, ext, googleCredentialPathFile1, ProposalType).Result;
                   
                }
                else
                {
                    //model = GCBucket.UploadDocument(folderName, file, fileName, result, EmailID, logginUserEmailId, ext, googleCredentialPathFile1, ProposalType).Result;
                }
                //ProjectProposalDetail proposaldetails = _uow.GetDbContext().ProjectProposalDetail.FirstOrDefault(x => x.ProjectId == ProjectId && x.IsDeleted == false);

                //if (proposaldetails == null)
                //{
                //    proposaldetails = new ProjectProposalDetail();

                //}

                //if (ProposalType == "Proposal")
                //{
                //    proposaldetails.FolderName = model.FolderName;
                //    proposaldetails.ProposalFileName = model.ProposalFileName;
                //    proposaldetails.ProposalWebLink = model.ProposalWebLink;
                //    proposaldetails.ProjectId = Convert.ToInt64(Projectid);
                //    proposaldetails.CreatedDate = DateTime.UtcNow;
                //    proposaldetails.IsDeleted = false;
                //    proposaldetails.CreatedById = UserId;

                //    // response folder path
                //    response.data.ProposalWebLink = model.ProposalWebLink;
                //    response.data.ProposalWebLinkExtType = model.ProposalExtType;
                //}
                //else
                //{
                //    if (ProposalType == "EOI")
                //    {
                //        proposaldetails.FolderName = model.FolderName;

                //        proposaldetails.EdiFileId = model.EdiFileId;
                //        proposaldetails.EDIFileName = model.EDIFileName;
                //        proposaldetails.EDIFileWebLink = model.EDIFileWebLink;
                //        proposaldetails.EDIFileExtType = model.EDIFileExtType;

                //        // response folder path
                //        response.data.EDIWebLink = model.EDIFileWebLink;
                //        response.data.EDIWebLinkExtType = model.EDIFileExtType;
                //    }
                //    else if (ProposalType == "BUDGET")
                //    {
                //        proposaldetails.FolderName = model.FolderName;

                //        proposaldetails.BudgetFileId = model.BudgetFileId;
                //        proposaldetails.BudgetFileName = model.BudgetFileName;
                //        proposaldetails.BudgetFileWebLink = model.BudgetFileWebLink;
                //        proposaldetails.BudgetFileExtType = model.BudgetFileExtType;

                //        // response folder path
                //        response.data.BudgetWebLink = model.BudgetFileWebLink;
                //        response.data.BudgetWebLinkExtType = model.BudgetFileExtType;
                //    }
                //    else if (ProposalType == "CONCEPT")
                //    {
                //        proposaldetails.FolderName = model.FolderName;

                //        proposaldetails.ConceptFileId = model.ConceptFileId;
                //        proposaldetails.ConceptFileName = model.ConceptFileName;
                //        proposaldetails.ConceptFileWebLink = model.ConceptFileWebLink;
                //        proposaldetails.ConceptFileExtType = model.ConceptFileExtType;

                //        // response folder path
                //        response.data.ConceptWebLink = model.ConceptFileWebLink;
                //        response.data.ConceptWebLinkExtType = model.ConceptFileExtType;
                //    }
                //    else if (ProposalType == "PRESENTATION")
                //    {
                //        proposaldetails.FolderName = model.FolderName;

                //        proposaldetails.PresentationFileId = model.PresentationFileId;
                //        proposaldetails.PresentationFileName = model.PresentationFileName;
                //        proposaldetails.PresentationFileWebLink = model.PresentationFileWebLink;
                //        proposaldetails.PresentationExtType = model.PresentationExtType;

                //        // response folder path
                //        response.data.PresentationWebLink = model.PresentationFileWebLink;
                //        response.data.PresentationWebLinkExtType = model.PresentationExtType;
                //    }
                //    proposaldata.ProjectId = Convert.ToInt64(Projectid);
                //    proposaldata.ModifiedDate = DateTime.Now;

                //}

                //if (proposaldetails.ProjectProposaldetailId == 0)
                //{
                //    _uow.ProjectProposalDetailRepository.Add(proposaldetails);
                //    _uow.GetDbContext().SaveChanges();
                //}
                //else
                //{
                //    // _uow.ProjectProposalDetailRepository.Update(proposaldetails, proposaldetails.ProjectProposaldetailId);
                //    _uow.GetDbContext().ProjectProposalDetail.Update(proposaldetails);
                //    _uow.GetDbContext().SaveChanges();
                //}

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




    #endregion

}
