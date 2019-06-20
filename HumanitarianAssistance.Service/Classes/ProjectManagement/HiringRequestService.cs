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

        public async Task<APIResponse> GetallHiringRequestDetail()
        {
            APIResponse response = new APIResponse();
            try
            {
                var requestDetail = await _uow.GetDbContext().ProjectHiringRequestDetail
                                                              .Include(c => c.CurrencyDetails)
                                                              .Include(b => b.ProjectBudgetLineDetail)
                                                              .Include(o => o.OfficeDetails)
                                                              .Include(c => c.JobGrade)
                                                              .Include(e => e.EmployeeDetail)
                                                              .Include(p => p.ProjectDetail)
                                                              .Include(f => f.ProfessionDetails)
                                                              .Where(x => x.IsDeleted == false)
                                                              .ToListAsync();
                var hiringRequestList = requestDetail.Select(x => new ProjectHiringRequestModel
                {
                    HiringRequestId = x.HiringRequestId,
                    HiringRequestCode = x.HiringRequestCode,
                    CurrencyId = x.CurrencyId,
                    BudgetLineId = x.BudgetLineId,
                    OfficeId = x.OfficeId.Value,
                    GradeId = x.GradeId,
                    BasicPay = x.BasicPay,
                    BudgetName = x.ProjectBudgetLineDetail?.BudgetName ?? null,
                    Description = x.Description,
                    CurrencyName = x.CurrencyDetails?.CurrencyName ?? null,
                    EmployeeID = x.EmployeeDetail?.EmployeeID ?? null,
                    EmployeeName = x.EmployeeDetail?.EmployeeName ?? null,
                    FilledVacancies = x.FilledVacancies,
                    GradeName = x.JobGrade.GradeName,
                    IsCompleted = x.IsCompleted,
                    OfficeName = x.OfficeDetails?.OfficeName ?? null,
                    Position = x.Position,
                    ProfessionId = x.ProfessionId,
                    ProfessionName = x.ProfessionDetails.ProfessionName,
                    TotalVacancies = x.TotalVacancies,
                    RequestedBy = _uow.GetDbContext().UserDetails.Select(z => new
                    {
                        FullName = z.FirstName + " " + z.LastName,
                        z.AspNetUserId,
                        z.IsDeleted
                    }).FirstOrDefault(u => u.AspNetUserId == x.CreatedById && u.IsDeleted == false).FullName

                }).ToList();
                response.data.ProjectHiringRequestModel = hiringRequestList.OrderByDescending(x => x.HiringRequestId).ToList();
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
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditProjectHiringRequest(ProjectHiringRequestModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                string description = string.Empty;

                description = model.Description.ToLower().Trim();
                ProjectHiringRequestDetail ifExist = await _uow.GetDbContext().ProjectHiringRequestDetail.FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                           x.Description.ToLower().Trim() == description);

                if (ifExist == null)
                {

                    ProjectHiringRequestDetail hrDeatil = await _uow.GetDbContext().ProjectHiringRequestDetail
                                                                                              .FirstOrDefaultAsync(x => x.HiringRequestId == model.HiringRequestId &&
                                                                                                                        x.IsDeleted == false);

                    hrDeatil.BasicPay = model.BasicPay;
                    hrDeatil.BudgetLineId = model.BudgetLineId;
                    hrDeatil.ModifiedById = userId;
                    hrDeatil.ModifiedDate = DateTime.UtcNow;
                    hrDeatil.CurrencyId = model.CurrencyId;
                    hrDeatil.Description = model.Description;
                    hrDeatil.EmployeeID = model.EmployeeID;
                    hrDeatil.FilledVacancies = model.FilledVacancies;
                    hrDeatil.GradeId = model.GradeId;
                    hrDeatil.IsCompleted = model.IsCompleted;
                    hrDeatil.OfficeId = model.OfficeId;
                    hrDeatil.Position = model.Position;
                    hrDeatil.ProfessionId = model.ProfessionId;
                    hrDeatil.ProjectId = model.ProjectId;
                    hrDeatil.TotalVacancies = model.TotalVacancies;

                    var objdetail = await _uow.ProjectHiringRequestRepository.UpdateAsyn(hrDeatil);
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
                            jobDetail.HiringRequestId = hrDeatil.HiringRequestId;
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
                var employeeExist = await _uow.GetDbContext().HiringRequestCandidates.FirstOrDefaultAsync(x => x.EmployeeID == model.EmployeeID && x.IsDeleted == false);
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
                    throw new Exception("Candidate already exists");
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
                                                                                                               x.IsDeleted == false && x.HiringRequestId==model.HiringRequestId);
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

        public async Task<APIResponse> EditIntetviewdCandidateDetail(HiringRequestCandidateModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {

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
                                                                  .Where(x => x.HiringRequestId == model.HiringRequestId)
                                                                  .Include(e => e.EmployeeDetail)
                                                                  .OrderByDescending(i => i.EmployeeID)
                                                                  .Select(x => new ProjectHiringCandidateDetailModel
                                                                  {
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
                                                                      IsInterViewed= x.EmployeeDetail.InterviewDetails.Any(y=>y.EmployeeID==x.EmployeeID && y.IsDeleted==false),
                                                                      IsShortListed = x.IsShortListed,
                                                                      IsSelected = x.IsSelected


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
    }
}
