using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Project;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.ProjectManagement;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes.ProjectManagement
{
    public class HiringRequestService : IHiringRequestService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public HiringRequestService(IUnitOfWork uow,
            IMapper mapper,
            UserManager<AppUser> userManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<APIResponse> GetallHiringRequestDetail(ProjectHiringRequestModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                int totalCount = await _uow.GetDbContext().ProjectHiringRequestDetail.CountAsync(x => x.IsDeleted == false && x.ProjectId == model.ProjectId);


                var requestDetail = (from hr in _uow.GetDbContext().ProjectHiringRequestDetail.Where(x => x.IsDeleted == false &&
                                                                                                         x.ProjectId == model.ProjectId)
                                     join c in _uow.GetDbContext().CurrencyDetails on hr.CurrencyId equals c.CurrencyId into h
                                     from c in h.DefaultIfEmpty()
                                     join b in _uow.GetDbContext().ProjectBudgetLineDetail on hr.BudgetLineId equals b.BudgetLineId into bl
                                     from b in bl.DefaultIfEmpty()
                                     join o in _uow.GetDbContext().OfficeDetail on hr.OfficeId equals o.OfficeId into od
                                     from o in od.DefaultIfEmpty()
                                     join g in _uow.GetDbContext().JobGrade on hr.GradeId equals g.GradeId into gd
                                     from g in gd.DefaultIfEmpty()
                                     join e in _uow.GetDbContext().EmployeeDetail on hr.EmployeeID equals e.EmployeeID into ed
                                     from e in ed.DefaultIfEmpty()
                                     join p in _uow.GetDbContext().ProfessionDetails on hr.ProfessionId equals p.ProfessionId into pd
                                     from p in pd.DefaultIfEmpty()
                                     join u in _uow.GetDbContext().UserDetails on hr.CreatedById equals u.AspNetUserId into ud
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
                                         BudgetName = b.BudgetName ,
                                         Description = hr.Description,
                                         CurrencyName = c.CurrencyName ,
                                         EmployeeID = e.EmployeeID,
                                         EmployeeName = e.EmployeeName ,
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
                                     .Skip(model.pageSize.Value * model.pageIndex.Value)
                                     .Take(model.pageSize.Value)
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
        public async Task<APIResponse> AddProjectHiringRequest(ProjectHiringRequestModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ProjectHiringRequestDetail hiringRequestDeatil = new ProjectHiringRequestDetail()
                {
                    BasicPay = model.BasicPay,
                    BudgetLineId = model.BudgetLineId,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow,
                    CurrencyId = model.CurrencyId,
                    Description = model.Description,
                    EmployeeID = model.EmployeeID,
                    FilledVacancies = model.FilledVacancies,
                    GradeId = model.GradeId,
                    IsCompleted = model.IsCompleted,
                    IsDeleted = false,
                    OfficeId = model.OfficeId,
                    Position = model.Position,
                    ProfessionId = model.ProfessionId,
                    ProjectId = model.ProjectId,
                    TotalVacancies = model.TotalVacancies
                };
                var objdetail = await _uow.ProjectHiringRequestRepository.AddAsyn(hiringRequestDeatil);
                await _uow.GetDbContext().SaveChangesAsync();

                if (objdetail.HiringRequestId != 0)
                {
                    string description = string.Empty;

                    JobHiringDetails jobDetail = new JobHiringDetails();
                    if (!string.IsNullOrEmpty(model.Description))
                    {

                        description = model.Description.ToLower().Trim();
                        jobDetail = await _uow.GetDbContext().JobHiringDetails.Where(x => x.IsDeleted == false &&
                                                                                          x.JobDescription.ToLower().Trim() == description)
                                                                                                                .FirstOrDefaultAsync();
                    }

                    if (jobDetail == null)
                    {
                        jobDetail = new JobHiringDetails()
                        {
                            JobDescription = model.Description,
                            ProfessionId = model.ProfessionId,
                            OfficeId = model.OfficeId,
                            IsActive = true,
                            GradeId = model.GradeId,
                            HiringRequestId = hiringRequestDeatil.HiringRequestId,
                            IsDeleted = false,
                            CreatedById = userId,
                            CreatedDate = DateTime.UtcNow,
                            Unit = model.TotalVacancies.Value

                        };
                        await _uow.JobHiringDetailsRepository.AddAsyn(jobDetail);
                        if (jobDetail.JobId != 0)
                        {
                            jobDetail.JobCode = "JC" + String.Format("{0:D4}", jobDetail.JobId);
                            await _uow.JobHiringDetailsRepository.UpdateAsyn(jobDetail);
                        }

                    }

                    else
                    {
                        throw new Exception("Job is already exist");
                    }


                }


                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message =   ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditProjectHiringRequest(ProjectHiringRequestModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                string description = model.Description.ToLower().Trim();

                bool recordExists = await _uow.GetDbContext().ProjectHiringRequestDetail.AnyAsync(x => x.IsDeleted == false &&
                                                                                           x.Description.ToLower().Trim() == description && x.HiringRequestId != model.HiringRequestId);

                if (!recordExists)
                {

                    ProjectHiringRequestDetail projectHiringRequest = await _uow.GetDbContext().ProjectHiringRequestDetail
                                                                                              .FirstOrDefaultAsync(x => x.HiringRequestId == model.HiringRequestId &&
                                                                                                                        x.IsDeleted == false);

                    projectHiringRequest.BasicPay = model.BasicPay;
                    projectHiringRequest.BudgetLineId = model.BudgetLineId;
                    projectHiringRequest.ModifiedById = userId;
                    projectHiringRequest.ModifiedDate = DateTime.UtcNow;
                    projectHiringRequest.CurrencyId = model.CurrencyId;
                    projectHiringRequest.Description = model.Description;
                    projectHiringRequest.EmployeeID = model.EmployeeID;
                    projectHiringRequest.FilledVacancies = model.FilledVacancies;
                    projectHiringRequest.GradeId = model.GradeId;
                    projectHiringRequest.IsCompleted = model.IsCompleted;
                    projectHiringRequest.OfficeId = model.OfficeId;
                    projectHiringRequest.Position = model.Position;
                    projectHiringRequest.ProfessionId = model.ProfessionId;
                    projectHiringRequest.ProjectId = model.ProjectId;
                    projectHiringRequest.TotalVacancies = model.TotalVacancies;

                    var objdetail = await _uow.ProjectHiringRequestRepository.UpdateAsyn(projectHiringRequest);
                    await _uow.GetDbContext().SaveChangesAsync();
                    // Note : edit ProjectJob in old Ui
                    if (objdetail.HiringRequestId != 0)
                    {
                        JobHiringDetails jobDetail = await _uow.GetDbContext().JobHiringDetails.FirstOrDefaultAsync(x => x.HiringRequestId == model.HiringRequestId &&
                                                                                                            x.IsDeleted == false);
                        if (jobDetail != null)
                        {
                            jobDetail.JobDescription = model.Description;
                            jobDetail.ProfessionId = model.ProfessionId;
                            jobDetail.OfficeId = model.OfficeId;
                            jobDetail.IsActive = true;
                            jobDetail.GradeId = model.GradeId;
                            jobDetail.HiringRequestId = projectHiringRequest.HiringRequestId;
                            jobDetail.IsDeleted = false;
                            jobDetail.ModifiedById = userId;
                            jobDetail.ModifiedDate = DateTime.UtcNow;
                            jobDetail.Unit = model.TotalVacancies.Value;

                            await _uow.JobHiringDetailsRepository.UpdateAsyn(jobDetail);
                            await _uow.GetDbContext().SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    throw new Exception("Hiring Request is already exist");
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllEmployeeList()
        {
            APIResponse response = new APIResponse();
            try
            {
                response.data.EmployeeDetailListData = await _uow.GetDbContext().EmployeeDetail.Where(x => x.EmployeeTypeId != (int)EmployeeTypeStatus.Terminated &&
                                                                                                           x.IsDeleted == false).Select(x => new EmployeeDetailList
                                                                                                           {
                                                                                                               EmployeeId = x.EmployeeID,
                                                                                                               EmployeeName = x.EmployeeName
                                                                                                           }
                                                                                                      ).OrderByDescending(x => x.EmployeeId).ToListAsync();

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


        public async Task<APIResponse> AddHiringRequestCandidate(HiringRequestCandidateModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeExist = await _uow.GetDbContext().HiringRequestCandidates
                                              .FirstOrDefaultAsync(x => x.EmployeeID == model.EmployeeID
                                              && x.HiringRequestId == model.HiringRequestId
                                              && x.IsDeleted == false);

                if (employeeExist == null)
                {
                    HiringRequestCandidates candidateDeatil = new HiringRequestCandidates()
                    {
                        HiringRequestId = model.HiringRequestId,
                        EmployeeID = model.EmployeeID,
                        CreatedById = userId,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,

                    };
                    await _uow.HiringRequestCandidatesRepository.AddAsyn(candidateDeatil);
                    await _uow.GetDbContext().SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Candidate already exists with a job in project");
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditHiringRequestCandidate(ProjectHiringCandidateDetailModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeExist = await _uow.GetDbContext().HiringRequestCandidates.FirstOrDefaultAsync(x => x.EmployeeID == model.EmployeeID &&
                                                                                                               x.IsDeleted == false && x.HiringRequestId == model.HiringRequestId);
                if (employeeExist != null)
                {

                    employeeExist.HiringRequestId = model.HiringRequestId;
                    employeeExist.EmployeeID = model.EmployeeID;
                    employeeExist.ModifiedById = userId;
                    employeeExist.ModifiedDate = DateTime.UtcNow;
                    employeeExist.IsDeleted = false;
                    employeeExist.IsShortListed = model.IsShortListed;



                    await _uow.HiringRequestCandidatesRepository.UpdateAsyn(employeeExist);
                    await _uow.GetDbContext().SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Candidate Not exists");
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }


        public async Task<APIResponse> GetAllCandidateList(ProjectHiringCandidateDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var EmployeeDetailListData = await _uow.GetDbContext()
                                                                  .HiringRequestCandidates
                                                                  .Where(x => x.HiringRequestId == model.HiringRequestId && x.IsDeleted ==false)
                                                                  //.Include(e => e.EmployeeDetail)
                                                                  .OrderByDescending(i => i.EmployeeID)
                                                                  .Select(x => new ProjectHiringCandidateDetailModel
                                                                  {
                                                                      CandidateId = x.CandidateId,
                                                                      EmployeeID = x.EmployeeID,
                                                                      EmployeeName = x.EmployeeDetail.EmployeeName,
                                                                      EmployeeCode = x.EmployeeDetail.EmployeeCode,
                                                                      EmployeeTypeId = x.EmployeeDetail.EmployeeTypeId,
                                                                      EmployeeTypeName = x.EmployeeDetail.EmployeeType.EmployeeTypeName == "Prospective" ?
                                                                                                                                           "Candidate" :
                                                                                                                                            x.EmployeeDetail.EmployeeType.EmployeeTypeName,
                                                                      Gender = x.EmployeeDetail.SexId == (int)Sex.Male ? "Male" :
                                                                                x.EmployeeDetail.SexId == (int)Sex.Female ? "Female" :
                                                                                x.EmployeeDetail.SexId == (int)Gender.OTHER ? "Other" : null,
                                                                      IsInterViewed = x.EmployeeDetail.InterviewDetails.Any(y => y.EmployeeID == x.EmployeeID && y.IsDeleted == false),
                                                                      IsShortListed = x.IsShortListed,
                                                                      IsSelected = x.IsSelected,



                                                                  }
                                                                  ).ToListAsync();
                response.data.ProjectHiringCandidateDetailModel = EmployeeDetailListData;
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

        public async Task<APIResponse> AddCandidateInterviewDetail(CandidateInterViewModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (!string.IsNullOrEmpty(model.JobDescription))
                {
                    string descrtiopn = string.Empty;
                    descrtiopn = model.JobDescription.ToLower().Trim();

                    // note cjeck the jobname and update in interview table
                    JobHiringDetails Jobdetail = await _uow.GetDbContext().JobHiringDetails.FirstOrDefaultAsync(x => x.JobDescription.ToLower().Trim() == descrtiopn && x.IsDeleted == false);
                    InterviewDetails obj = new InterviewDetails();
                    if (Jobdetail != null)
                    {
                        obj.InterviewStatus = null; //Approve - Reject Flag
                        obj.CreatedById = userId;
                        obj.CreatedDate = DateTime.Now;
                        obj.IsDeleted = false;
                        obj.JobId = Jobdetail.JobId;
                        obj.EmployeeID = model.EmployeeID;
                        await _uow.InterviewDetailsRepository.AddAsyn(obj);
                    }
                    else
                    {
                        throw new Exception("Job does not exists");
                    }


                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;


        }

        #region HiringRequestSelectCandidate
        public async Task<APIResponse> HiringRequestSelectCandidate(HiringSelectCandidateModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (model != null)
                {

                    HiringRequestCandidates hiringRequestCandidates = await _uow.GetDbContext().HiringRequestCandidates
                                                                               .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                         x.EmployeeID == model.EmployeeId &&
                                                                                                         x.HiringRequestId == model.HiringRequestId);

                    if (hiringRequestCandidates != null)
                    {
                        hiringRequestCandidates.IsSelected = true;
                        hiringRequestCandidates.ModifiedById = userId;
                        hiringRequestCandidates.ModifiedDate = DateTime.UtcNow;
                        hiringRequestCandidates.IsDeleted = false;
                        _uow.GetDbContext().HiringRequestCandidates.Update(hiringRequestCandidates);
                        await _uow.GetDbContext().SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Candidate not found");
                    }

                    //update the hiring request table when candidate is selected

                    ProjectHiringRequestDetail hrDetail = await _uow.GetDbContext().ProjectHiringRequestDetail
                                                                                 .FirstOrDefaultAsync(x => x.HiringRequestId == model.HiringRequestId &&
                                                                                                          x.IsDeleted == false);
                    if (hrDetail != null)
                    {
                        int count = await _uow.GetDbContext().HiringRequestCandidates.Where(x => x.HiringRequestId == model.HiringRequestId &&
                                                                                                 x.IsDeleted == false).CountAsync(x => x.IsSelected);
                        hrDetail.FilledVacancies = count;
                        await _uow.ProjectHiringRequestRepository.UpdateAsyn(hrDetail);
                        await _uow.SaveAsync();
                    }
                    else
                    {
                        throw new Exception("Hiring Job not found");
                    }
                    EmployeeSalaryAnalyticalInfo analyticalInfo = new EmployeeSalaryAnalyticalInfo();

                    analyticalInfo.IsDeleted = false;
                    analyticalInfo.CreatedById = userId;
                    analyticalInfo.CreatedDate = DateTime.UtcNow;
                    analyticalInfo.EmployeeID = model.EmployeeId;
                    analyticalInfo.BudgetlineId = model.BudgetLineId;
                    analyticalInfo.ProjectId = model.ProjectId;
                    analyticalInfo.HiringRequestId = model.HiringRequestId;
                    await _uow.GetDbContext().EmployeeSalaryAnalyticalInfo.AddAsync(analyticalInfo);
                    await _uow.GetDbContext().SaveChangesAsync();
                    response.ResponseData = hrDetail;
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        #endregion

        #region CompleteHiringRequest
        public async Task<APIResponse> CompleteHiringRequest(long hiringRequestId, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                ProjectHiringRequestDetail projectHiringRequestDetail = new ProjectHiringRequestDetail();
                if (hiringRequestId != 0)
                {
                    projectHiringRequestDetail = await _uow.GetDbContext().ProjectHiringRequestDetail
                                                                          .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                    x.HiringRequestId == hiringRequestId);

                    projectHiringRequestDetail.IsCompleted = true;

                    await _uow.ProjectHiringRequestRepository.UpdateAsyn(projectHiringRequestDetail);
                    await _uow.GetDbContext().SaveChangesAsync();
                }
                response.ResponseData = projectHiringRequestDetail;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        #endregion

        #region "DeleteCandidateDetail"
        public async Task<APIResponse> DeleteCandidateDetail(ProjectHiringCandidateDetailModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                HiringRequestCandidates projectCandidateDetail = new HiringRequestCandidates();
                ProjectHiringRequestDetail hrDetail = new ProjectHiringRequestDetail();
                if (model.HiringRequestId != 0)
                {
                    projectCandidateDetail = await _uow.GetDbContext().HiringRequestCandidates
                                                                      .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                x.HiringRequestId == model.HiringRequestId &&
                                                                                                x.CandidateId == model.CandidateId);

                    if (projectCandidateDetail != null)
                    {
                        projectCandidateDetail.IsDeleted = true;
                        await _uow.HiringRequestCandidatesRepository.UpdateAsyn(projectCandidateDetail);
                        await _uow.GetDbContext().SaveChangesAsync();

                        // note: to update filled vacancire in hiring request detail page

                        hrDetail = await _uow.GetDbContext().ProjectHiringRequestDetail
                                                            .FirstOrDefaultAsync(x => x.HiringRequestId == model.HiringRequestId &&
                                                                                      x.IsDeleted == false);
                        if (hrDetail == null)
                        {
                            throw new Exception("Hiring Job not found");
                        }
                        int count = await _uow.GetDbContext().HiringRequestCandidates
                                                             .CountAsync(x => x.HiringRequestId == model.HiringRequestId &&
                                                                              x.IsDeleted == false &&
                                                                              x.IsSelected);
                        hrDetail.FilledVacancies = count;
                        
                        await _uow.ProjectHiringRequestRepository.UpdateAsyn(hrDetail);
                        await _uow.SaveAsync();

                    }
                    else
                    {
                        throw new Exception("No Candidate found");

                    }
                }
                response.ResponseData = hrDetail;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion
    }
}
