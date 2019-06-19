using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Project;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.ProjectManagement;
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
                                                              .Where(x => x.IsDeleted == false)
                                                              .ToListAsync();
                var hiringRequestList = requestDetail.Select(x => new ProjectHiringRequestModel
                {
                    HiringRequestId = x.HiringRequestId,
                    HiringRequestCode = x.HiringRequestCode,
                    CurrencyId = x.CurrencyId,
                    BudgetLineId = x.BudgetLineId,
                    OfficeId = x.OfficeId,
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
                    Profession = x.Profession,
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
                    Profession = model.Profession,
                    ProjectId = model.ProjectId,
                    TotalVacancies = model.TotalVacancies
                };
                var objdetail = await _uow.ProjectHiringRequestRepository.AddAsyn(hiringRequestDeatil);
                await _uow.GetDbContext().SaveChangesAsync();
                //if (objdetail.HiringReuestId != 0)
                //{
                //    objdetail.HiringRequestCode = await GetProjectBudgetLineCode(objdetail);
                //    //Note : update using repository not working thats why update using entity. 
                //    _uow.GetDbContext().ProjectBudgetLineDetail.Update(objdetail);
                //    await _uow.GetDbContext().SaveChangesAsync();


                //}

                //else
                //{
                //    throw new Exception("Budget line can not be created");
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

        public async Task<APIResponse> EditProjectHiringRequest(ProjectHiringRequestModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
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
                    hrDeatil.Profession = model.Profession;
                    hrDeatil.ProjectId = model.ProjectId;
                    hrDeatil.TotalVacancies = model.TotalVacancies;
                
                var objdetail = await _uow.ProjectHiringRequestRepository.UpdateAsyn(hrDeatil);
                await _uow.GetDbContext().SaveChangesAsync();
                //if (objdetail.HiringReuestId != 0)
                //{
                //    objdetail.HiringRequestCode = await GetProjectBudgetLineCode(objdetail);
                //    //Note : update using repository not working thats why update using entity. 
                //    _uow.GetDbContext().ProjectBudgetLineDetail.Update(objdetail);
                //    await _uow.GetDbContext().SaveChangesAsync();


                //}

                //else
                //{
                //    throw new Exception("Budget line can not be created");
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
}
