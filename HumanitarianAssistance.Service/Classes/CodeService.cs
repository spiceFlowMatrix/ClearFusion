using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class CodeService : ICode
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public CodeService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> GetAllCountry()
        {
            APIResponse response = new APIResponse();
            try
            {
                var countrylist = (from c in await _uow.CountryDetailsRepository.GetAllAsyn()
                                   where c.IsDeleted == false
                                   select new CountryDetailsModel
                                   {
                                       CountryId = c.CountryId,
                                       CountryName = c.CountryName
                                   }).ToList();
                response.data.CountryDetailsList = countrylist;
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

        public async Task<APIResponse> GetAllProvinceDetails(int CountryId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var provincelist = (from p in await _uow.ProvinceDetailsRepository.GetAllAsyn()
                                    where p.CountryId == CountryId && p.IsDeleted == false
                                    select new ProvinceDetailsModel
                                    {
                                        ProvinceId = p.ProvinceId,
                                        ProvinceName = p.ProvinceName
                                    }).ToList();
                response.data.ProvinceDetailsList = provincelist;
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

        public async Task<APIResponse> GetAllNationality()
        {
            APIResponse response = new APIResponse();
            try
            {
                var nationalitylist = (from n in await _uow.NationalityDetailsRepository.GetAllAsyn()
                                       where n.IsDeleted == false
                                       select new NationalityDetailsModel
                                       {
                                           NationalityId = n.NationalityId,
                                           NationalityName = n.NationalityName
                                       }).ToList();
                response.data.NationalityDetailsList = nationalitylist;
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

        public async Task<APIResponse> GetAllQualification()
        {
            APIResponse response = new APIResponse();
            try
            {
                var qualificationlist = (from q in await _uow.QualificationDetailsRepository.GetAllAsyn()
                                         where q.IsDeleted == false
                                         select new QualificationDetailsModel
                                         {
                                             QualificationId = q.QualificationId,
                                             QualificationName = q.QualificationName
                                         }).ToList();
                response.data.QualificationDetailsList = qualificationlist;
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

        public async Task<APIResponse> AddQualificationDetails(QualificationDetailsModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existrecord = await _uow.QualificationDetailsRepository.FindAsync(x => x.QualificationName == model.QualificationName);
                if (existrecord == null)
                {
                    QualificationDetails obj = _mapper.Map<QualificationDetails>(model);
                    await _uow.QualificationDetailsRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                    response.Message = "Qualification Name already exist.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditQualifactionDetails(QualificationDetailsModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existrecord = await _uow.QualificationDetailsRepository.FindAsync(x => x.QualificationId == model.QualificationId);
                if (existrecord != null)
                {
                    var exist = await _uow.QualificationDetailsRepository.FindAsync(x => x.QualificationName == model.QualificationName);
                    if (exist == null)
                    {
                        existrecord.QualificationName = model.QualificationName;
                        existrecord.ModifiedById = model.ModifiedById;
                        existrecord.ModifiedDate = model.ModifiedDate;
                        existrecord.IsDeleted = false;
                        await _uow.QualificationDetailsRepository.UpdateAsyn(existrecord);
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                        response.Message = "Qualification Name already exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllInterviewRoundList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var roundlist = (from r in await _uow.InterviewRoundTypeMasterRepository.GetAllAsyn()
                                 select new InterViewRoundModel
                                 {
                                     RoundId = r.RoundId,
                                     RoundTypeName = r.RoundTypeName
                                 }).ToList();
                response.data.InterviewRoundList = roundlist;
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

        public async Task<APIResponse> AddLeaveReasonDetail(LeaveReasonDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {
                    LeaveReasonDetail obj = _mapper.Map<LeaveReasonDetail>(model);
                    await _uow.LeaveReasonDetailRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
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

        public async Task<APIResponse> EditLeaveReasonDetail(LeaveReasonDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var leavereasoninfo = await _uow.LeaveReasonDetailRepository.FindAsync(x => x.LeaveReasonId == model.LeaveReasonId);
                if (leavereasoninfo != null)
                {
                    leavereasoninfo.ReasonName = model.ReasonName;
                    leavereasoninfo.Unit = model.Unit;
                    leavereasoninfo.ModifiedById = model.ModifiedById;
                    leavereasoninfo.ModifiedDate = model.ModifiedDate;
                    leavereasoninfo.IsDeleted = model.IsDeleted;
                    await _uow.LeaveReasonDetailRepository.UpdateAsyn(leavereasoninfo);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllLeaveReasonList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var leavelist = await Task.Run(() =>
                    _uow.LeaveReasonDetailRepository.FindAllAsync(x => x.IsDeleted == false).Result.ToList()
                );

                var leavereasonlist = leavelist.Select(x => new LeaveReasonDetailModel
                {
                    LeaveReasonId = x.LeaveReasonId,
                    ReasonName = x.ReasonName,
                    Unit = x.Unit
                }).ToList();
                response.data.LeaveReasonList = leavereasonlist;
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

        public async Task<APIResponse> AddFinancialYearDetail(FinancialYearDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var yearlist = await _uow.FinancialYearDetailRepository.GetAllAsyn();
                foreach (var i in yearlist)
                {
                    var existrecord1 = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDeleted == false && x.FinancialYearId == i.FinancialYearId);
                    existrecord1.IsDefault = false;
                    await _uow.FinancialYearDetailRepository.UpdateAsyn(existrecord1);
                }
                FinancialYearDetail obj = _mapper.Map<FinancialYearDetail>(model);
                if (yearlist == null)
                    obj.IsDefault = true;
                await _uow.FinancialYearDetailRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> EditFinancialYearDetail(FinancialYearDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                int? recordexist = _uow.HolidayDetailsRepository.FindAllAsync(x => x.FinancialYearId == model.FinancialYearId).Result.FirstOrDefault().FinancialYearId;
                if (recordexist > 0)
                {

                }

                var existrecord = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDeleted == false && x.FinancialYearId == model.FinancialYearId);
                if (existrecord != null)
                {
                    if (model.IsDefault == true)
                    {
                        var yearlist = await _uow.FinancialYearDetailRepository.GetAllAsyn();
                        foreach (var i in yearlist)
                        {
                            var existrecord1 = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDeleted == false && x.FinancialYearId == i.FinancialYearId);
                            existrecord1.IsDefault = false;
                            await _uow.FinancialYearDetailRepository.UpdateAsyn(existrecord1);
                        }
                    }

                    existrecord.FinancialYearName = model.FinancialYearName;
                    existrecord.StartDate = model.StartDate;
                    existrecord.EndDate = model.EndDate;
                    existrecord.Description = model.Description;
                    existrecord.IsDefault = model.IsDefault;
                    existrecord.ModifiedById = model.ModifiedById;
                    existrecord.ModifiedDate = model.ModifiedDate;
                    existrecord.IsDeleted = model.IsDeleted;
                    await _uow.FinancialYearDetailRepository.UpdateAsyn(existrecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllFinancialYearDetail()
        {
            APIResponse response = new APIResponse();
            try
            {
                var yearlist = await Task.Run(() =>
                    _uow.FinancialYearDetailRepository.FindAllAsync(x => x.IsDeleted == false).Result.OrderBy(x => x.FinancialYearId).ToList()
                );

                var financialyearlist = yearlist.Select(x => new FinancialYearDetailModel
                {
                    FinancialYearId = x.FinancialYearId,
                    FinancialYearName = x.FinancialYearName,
                    StartDate = x.StartDate.Date.ToLocalTime(),
                    EndDate = x.EndDate.Date.ToLocalTime(),
                    Description = x.Description,
                    IsDefault = x.IsDefault
                }).ToList();
                response.data.FinancialYearDetailList = financialyearlist;
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

        public async Task<APIResponse> GetCurrentFinancialYear()
        {
            APIResponse response = new APIResponse();
            try
            {
                var yearlist = await Task.Run(() =>
                    _uow.FinancialYearDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.IsDefault == true).Result.ToList()
                );

                var currentfinanciallist = yearlist.Select(x => new CurrentFinancialYear
                {
                    FinancialYearId = x.FinancialYearId,
                    FinancialYearName = x.FinancialYearName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToList();
                response.data.CurrentFinancialYearList = currentfinanciallist;
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

        public async Task<APIResponse> AddBudgetLineType(BudgetLineTypeModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                BudgetLineType obj = _mapper.Map<BudgetLineType>(model);
                await _uow.GetDbContext().BudgetLineType.AddAsync(obj);
                await _uow.GetDbContext().SaveChangesAsync();
                response.Message = "Add BudgetType Successfully!";
                response.StatusCode = StaticResource.successStatusCode;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = StaticResource.failStatusCode;
            }
            return response;
        }

        public async Task<APIResponse> GetBudgetLineTypes()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await Task.Run(() => _uow.GetDbContext().BudgetLineType.Where(x => x.IsDeleted == false));

                var budgetlinetypelist = list.Select(x => new BudgetLineTypeModel
                {
                    BudgetLineTypeId = x.BudgetLineTypeId,
                    BudgetLineTypeName = x.BudgetLineTypeName
                }).ToList();
                response.data.BudgetLineTypeList = budgetlinetypelist;
                response.Message = "Success";
                response.StatusCode = StaticResource.successStatusCode;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = StaticResource.failStatusCode;
            }
            return response;
        }

        public async Task<APIResponse> GetAllEmployeeType()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await Task.Run(() =>
                    _uow.EmployeeTypeRepository.FindAllAsync(x => x.IsDeleted == false).Result.ToList()
                );
                var employeetypelist = list.Select(x => new EmployeeTypeModel
                {
                    EmployeeTypeId = x.EmployeeTypeId,
                    EmployeeTypeName = x.EmployeeTypeName
                }).ToList();
                response.data.EmployeeTypeList = employeetypelist;
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

        public async Task<APIResponse> GetDepartmentsByOfficeId(int OfficeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.DepartmentRepository.FindAllAsync(x => x.OfficeId == OfficeId && x.IsDeleted == false);
                var departmentlist = list.Where(x => x.IsDeleted == false).
                    Select(x => new DepartmentModel
                    {
                        OfficeCode = x.OfficeCode,
                        DepartmentId = x.DepartmentId,
                        DepartmentName = x.DepartmentName,
                        OfficeId = x.OfficeId
                    }).ToList();
                response.data.Departments = departmentlist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong;
            }
            return response;
        }

        public async Task<APIResponse> AddDepartmentDetails(DepartmentModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existrecord = await _uow.DepartmentRepository.FindAsync(x => x.IsDeleted == false && x.DepartmentName == model.DepartmentName && x.OfficeId == model.OfficeId);
                if (existrecord == null)
                {
                    Department obj = _mapper.Map<Department>(model);
                    await _uow.DepartmentRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                    response.Message = "Department Name already exist for this Office.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditDepartmentDetails(DepartmentModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existrecord = await _uow.DepartmentRepository.FindAsync(x => x.IsDeleted == false && x.DepartmentId == model.DepartmentId);
                if (existrecord != null)
                {
                    var checkdepartment = await _uow.DepartmentRepository.FindAsync(x => x.IsDeleted == false && x.DepartmentName == model.DepartmentName && x.OfficeId == model.OfficeId);
                    if (checkdepartment == null)
                    {
                        existrecord.DepartmentName = model.DepartmentName;
                        existrecord.OfficeId = model.OfficeId;
                        existrecord.ModifiedById = model.ModifiedById;
                        existrecord.ModifiedDate = model.ModifiedDate;
                        existrecord.IsDeleted = model.IsDeleted;
                        await _uow.DepartmentRepository.UpdateAsyn(existrecord);
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                        response.Message = "Department Name already exist for this Office.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = "Success";
            }
            return response;
        }

        public async Task<APIResponse> GetAllDepartment()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await Task.Run(() =>
                    _uow.GetDbContext().Department.Include(o => o.OfficeDetails).Where(x => x.IsDeleted == false).ToListAsync()
                );
                var departmentlist = list.Select(x => new DepartmentModel
                {
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.DepartmentName,
                    OfficeId = x.OfficeId,
                    OfficeName = x.OfficeDetails?.OfficeName ?? null
                }).OrderBy(x => x.DepartmentId).ToList();

                response.data.Departments = departmentlist;
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

        public async Task<APIResponse> AddSalaryHead(SalaryHeadModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                SalaryHeadDetails obj = _mapper.Map<SalaryHeadDetails>(model);
                await _uow.SalaryHeadDetailsRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> EditSalaryHead(SalaryHeadModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existrecord = await _uow.SalaryHeadDetailsRepository.FindAsync(x => x.SalaryHeadId == model.SalaryHeadId);
                if (existrecord != null)
                {
                    existrecord.HeadName = model.HeadName;
                    existrecord.Description = model.Description;
                    existrecord.HeadTypeId = model.HeadTypeId;
                    existrecord.ModifiedById = model.ModifiedById;
                    existrecord.ModifiedDate = model.ModifiedDate;
                    existrecord.IsDeleted = model.IsDeleted;
                    await _uow.SalaryHeadDetailsRepository.UpdateAsyn(existrecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllSalaryHead()
        {
            APIResponse response = new APIResponse();
            try
            {
                var queryResult = EF.CompileAsyncQuery(
                  (ApplicationDbContext ctx) => ctx.SalaryHeadDetails.Where(x => x.IsDeleted == false));
                var list = await Task.Run(() =>
                    queryResult(_uow.GetDbContext()).ToListAsync().Result
                );

                var salaryheadlist = list.Select(x => new SalaryHeadModel
                {
                    SalaryHeadId = x.SalaryHeadId,
                    HeadTypeId = x.HeadTypeId,
                    HeadName = x.HeadName,
                    Description = x.Description
                }).ToList();

                response.data.SalaryHeadList = salaryheadlist;
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


        public async Task<APIResponse> AddPensionRate(EmployeePensionRateModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {
                    var financialYearCheck = await _uow.EmployeePensionRateRepository.FindAllAsync(x => x.FinancialYearId == model.FinancialYearId);
                    if (financialYearCheck.Count == 0)
                    {
                        EmployeePensionRate obj = _mapper.Map<EmployeePensionRate>(model);
                        var lst = await _uow.EmployeePensionRateRepository.FindAsync(x => x.IsDefault == true);
                        if (model.IsDefault == true)
                        {
                            if (lst != null)
                            {
                                lst.IsDefault = false;
                                _uow.GetDbContext().EmployeePensionRate.Update(lst);
                                await _uow.SaveAsync();
                            }
                        }
                        obj.IsDefault = (model.IsDefault == false && lst == null) ? true : model.IsDefault;
                        obj.CreatedById = UserId;
                        obj.CreatedDate = DateTime.Now;
                        await _uow.EmployeePensionRateRepository.AddAsyn(obj);
                        await _uow.SaveAsync();
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = 700;
                        response.Message = "Financial Year Already exists!";
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditPensionRate(EmployeePensionRateModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var lst = await _uow.EmployeePensionRateRepository.FindAsync(x => x.IsDefault == true);
                if (model.IsDefault == true)
                {
                    if (lst != null)
                    {
                        lst.IsDefault = false;
                        _uow.GetDbContext().EmployeePensionRate.Update(lst);
                        await _uow.SaveAsync();
                    }

                }
                if (model.IsDefault == false)
                {
                    if (lst == null)
                        model.IsDefault = true;
                    if (lst.IsDefault == true && lst.FinancialYearId == model.FinancialYearId)
                        model.IsDefault = true;
                    else
                        model.IsDefault = false;
                }

                EmployeePensionRate obj = await _uow.EmployeePensionRateRepository.FindAsync(x => x.FinancialYearId == model.FinancialYearId);
                obj.IsDefault = true;
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.SaveAsync();

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

        public async Task<APIResponse> GetAllPensionRate()
        {
            APIResponse response = new APIResponse();
            try
            {
                response.data.EmployeePensionRateList = await Task.Run(() => _uow.GetDbContext().EmployeePensionRate.Include(x => x.FinancialYearDetail).Where(x => x.IsDeleted == false).Select(x => new EmployeePensionRateModel
                {
                    FinancialYearId = x.FinancialYearId,
                    FinancialYearName = x.FinancialYearDetail.FinancialYearName,
                    PensionRate = x.PensionRate,
                    IsDefault = x.IsDefault
                }).ToListAsync());

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

        public async Task<APIResponse> AddAppraisalQuestion(AppraisalQuestionModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                AppraisalGeneralQuestions obj = _mapper.Map<AppraisalGeneralQuestions>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.AppraisalGeneralQuestionsRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> EditAppraisalQuestion(AppraisalQuestionModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var record = await _uow.AppraisalGeneralQuestionsRepository.FindAsync(x => x.AppraisalGeneralQuestionsId == model.AppraisalGeneralQuestionsId);
                record.SequenceNo = model.SequenceNo;
                record.Question = model.Question;
                record.DariQuestion = model.DariQuestion;
                record.ModifiedById = UserId;
                record.ModifiedDate = DateTime.Now;
                record.OfficeId = model.OfficeId;
                await _uow.AppraisalGeneralQuestionsRepository.UpdateAsyn(record);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> GetAppraisalQuestions(int OfficeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                response.data.AppraisalList = await _uow.GetDbContext().AppraisalGeneralQuestions.Where(x => x.IsDeleted == false && x.OfficeId == OfficeId).ToListAsync();
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

        public async Task<APIResponse> AddEmployeeAppraisalDetails(EmployeeAppraisalDetailsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                EmployeeAppraisalDetails obj = _mapper.Map<EmployeeAppraisalDetails>(model);
                obj.AppraisalStatus = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                obj.TotalScore = model.TotalScore;
                await _uow.EmployeeAppraisalDetailsRepository.AddAsyn(obj);
                List<EmployeeAppraisalQuestions> lst = new List<EmployeeAppraisalQuestions>();
                foreach (var item in model.EmployeeAppraisalQuestionList)
                {
                    EmployeeAppraisalQuestions eaq = new EmployeeAppraisalQuestions();
                    eaq.AppraisalGeneralQuestionsId = item.AppraisalGeneralQuestionsId;
                    eaq.Score = item.Score;
                    eaq.Remarks = item.Remarks;
                    eaq.EmployeeId = model.EmployeeId;
                    eaq.CurrentAppraisalDate = model.CurrentAppraisalDate;
                    eaq.CreatedDate = DateTime.Now;
                    eaq.CreatedById = UserId;
                    lst.Add(eaq);
                }
                await _uow.GetDbContext().EmployeeAppraisalQuestions.AddRangeAsync(lst);
                await _uow.SaveAsync();

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

        public async Task<APIResponse> EditEmployeeAppraisalDetails(EmployeeAppraisalDetailsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var emp = await _uow.EmployeeAppraisalDetailsRepository.FindAsync(x => x.EmployeeId == model.EmployeeId && x.OfficeId == model.OfficeId && x.CurrentAppraisalDate.Date == model.CurrentAppraisalDate.Date);
                emp.Position = model.Position;
                emp.Department = model.Department;
                emp.DutyStation = model.DutyStation;
                emp.AppraisalPeriod = model.AppraisalPeriod;
                emp.TotalScore = model.TotalScore;
                foreach (var item in model.EmployeeAppraisalQuestionList)
                {
                    var question = await _uow.EmployeeAppraisalQuestionsRepository.FindAsync(x => x.EmployeeId == model.EmployeeId && x.CurrentAppraisalDate.Date == model.CurrentAppraisalDate.Date && x.AppraisalGeneralQuestionsId == item.AppraisalGeneralQuestionsId);
                    question.Score = item.Score;
                    question.Remarks = item.Remarks;
                    await _uow.EmployeeAppraisalQuestionsRepository.UpdateAsyn(question);
                }
                await _uow.SaveAsync();
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

        public async Task<APIResponse> GetAllEmployeeAppraisalDetails(int OfficeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();
                var emplst = await _uow.EmployeeAppraisalDetailsRepository.FindAllAsync(x => x.OfficeId == OfficeId && x.AppraisalStatus == false);
                foreach (var item in emplst)
                {
                    EmployeeAppraisalDetailsModel model = new EmployeeAppraisalDetailsModel();
                    var quesLst = await _uow.GetDbContext().EmployeeAppraisalQuestions.Include(x => x.AppraisalGeneralQuestions).Where(x => x.EmployeeId == item.EmployeeId && x.CurrentAppraisalDate == item.CurrentAppraisalDate).ToListAsync();
                    model.EmployeeAppraisalDetailsId = item.EmployeeAppraisalDetailsId;
                    model.EmployeeId = item.EmployeeId;
                    model.EmployeeCode = item.EmployeeCode;
                    model.EmployeeName = item.EmployeeName;
                    model.FatherName = item.FatherName;
                    model.Position = item.Position;
                    model.Department = item.Department;
                    model.Qualification = item.Qualification;
                    model.DutyStation = item.DutyStation;
                    model.RecruitmentDate = item.RecruitmentDate;
                    model.AppraisalPeriod = item.AppraisalPeriod;
                    model.CurrentAppraisalDate = item.CurrentAppraisalDate;
                    model.OfficeId = item.OfficeId;
                    model.TotalScore = item.TotalScore;
                    model.AppraisalStatus = item.AppraisalStatus;
                    foreach (var element in quesLst)
                    {
                        EmployeeAppraisalQuestionModel questions = new EmployeeAppraisalQuestionModel();
                        questions.QuestionEnglish = element.AppraisalGeneralQuestions.Question;
                        questions.QuestionDari = element.AppraisalGeneralQuestions.DariQuestion;
                        questions.SequenceNo = element.AppraisalGeneralQuestions.SequenceNo;
                        questions.AppraisalGeneralQuestionsId = element.AppraisalGeneralQuestionsId;
                        questions.Score = element.Score;
                        questions.Remarks = element.Remarks;
                        model.EmployeeAppraisalQuestionList.Add(questions);
                    }
                    lst.Add(model);
                }
                response.data.EmployeeAppraisalDetailsModelLst = lst;
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

        public async Task<APIResponse> ApproveEmployeeAppraisalRequest(int EmployeeAppraisalDetailsId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();
                var emplst = await _uow.EmployeeAppraisalDetailsRepository.FindAsync(x => x.EmployeeAppraisalDetailsId == EmployeeAppraisalDetailsId && x.AppraisalStatus == false);
                emplst.AppraisalStatus = true;
                emplst.ModifiedById = UserId;
                emplst.ModifiedDate = DateTime.Now;
                await _uow.EmployeeAppraisalDetailsRepository.UpdateAsyn(emplst);

                EmployeeEvaluation evaluationModel = new EmployeeEvaluation();
                evaluationModel.CreatedById = UserId;
                evaluationModel.CreatedDate = DateTime.Now;
                evaluationModel.EmployeeId = emplst.EmployeeId;
                evaluationModel.CurrentAppraisalDate = emplst.CurrentAppraisalDate;
                evaluationModel.IsDeleted = false;
                evaluationModel.EvaluationStatus = null;
                evaluationModel.EmployeeAppraisalDetailsId = emplst.EmployeeAppraisalDetailsId;
                await _uow.EmployeeEvaluationRepository.AddAsyn(evaluationModel);
                await _uow.SaveAsync();

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

        public async Task<APIResponse> AddEmployeeAppraisalMoreDetails(EmployeeAppraisalDetailsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var recordList = await _uow.EmployeeEvaluationRepository.FindAllAsync(x => x.EmployeeId == model.EmployeeId && x.CurrentAppraisalDate.Date == DateTime.Now.Date);
                _uow.GetDbContext().RemoveRange(recordList);

                List<EmployeeEvaluation> lst = new List<EmployeeEvaluation>();
                List<StrongandWeakPoints> StrongList = new List<StrongandWeakPoints>();
                List<StrongandWeakPoints> WeakList = new List<StrongandWeakPoints>();
                foreach (var item in model.EmployeeEvaluationModelList)
                {
                    EmployeeEvaluation obj = new EmployeeEvaluation();
                    //obj.TrainingProgram = item.TrainingProgram;
                    //obj.Program = item.Program;
                    //obj.Participated = item.Participated;
                    //obj.CatchLevel = item.CatchLevel;
                    //obj.RefresherTrm = item.RefresherTrm;
                    //obj.OthRecommendation = item.OthRecommendation;
                    obj.FinalResultQues1 = model.FinalResultQues1;
                    obj.FinalResultQues2 = model.FinalResultQues2;
                    obj.FinalResultQues3 = model.FinalResultQues3;
                    obj.FinalResultQues4 = model.FinalResultQues4;
                    obj.FinalResultQues5 = model.FinalResultQues5;
                    obj.DirectSupervisor = model.DirectSupervisor;
                    //obj.AppraisalTeamMember1 = model.AppraisalTeamMember1;
                    //obj.AppraisalTeamMember2 = model.AppraisalTeamMember2;
                    obj.CommentsByEmployee = model.CommentsByEmployee;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    obj.CurrentAppraisalDate = DateTime.Now;
                    obj.EmployeeId = model.EmployeeId;
                    lst.Add(obj);
                }
                await _uow.GetDbContext().EmployeeEvaluation.AddRangeAsync(lst);

                foreach (var item in model.StrongPoints)
                {
                    StrongandWeakPoints obj = new StrongandWeakPoints();
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    obj.CurrentAppraisalDate = DateTime.Now;
                    obj.EmployeeId = model.EmployeeId;
                    obj.Point = item;
                    obj.Status = 1;         // 1 for strong points
                    StrongList.Add(obj);
                }
                await _uow.GetDbContext().StrongandWeakPoints.AddRangeAsync(StrongList);

                foreach (var item in model.WeakPoints)
                {
                    StrongandWeakPoints obj = new StrongandWeakPoints();
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    obj.CurrentAppraisalDate = DateTime.Now;
                    obj.EmployeeId = model.EmployeeId;
                    obj.Point = item;
                    obj.Status = 2;         // 2 for Weak points
                    WeakList.Add(obj);
                }
                await _uow.GetDbContext().StrongandWeakPoints.AddRangeAsync(WeakList);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> EditEmployeeAppraisalMoreDetails(EmployeeAppraisalDetailsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var recordList = await _uow.EmployeeEvaluationRepository.FindAsync(x => x.EmployeeAppraisalDetailsId == model.EmployeeAppraisalDetailsId);
                //_uow.GetDbContext().RemoveRange(recordList);

                if (recordList != null)
                {
                    List<EmployeeEvaluationTraining> trainingList = new List<EmployeeEvaluationTraining>();
                    List<EmployeeAppraisalTeamMember> appraisalTeamMemberList = new List<EmployeeAppraisalTeamMember>();
                    List<StrongandWeakPoints> StrongList = new List<StrongandWeakPoints>();
                    List<StrongandWeakPoints> WeakList = new List<StrongandWeakPoints>();

                    var trainingData = await _uow.EmployeeEvaluationTrainingRepository.FindAllAsync(x => x.EmployeeAppraisalDetailsId == model.EmployeeAppraisalDetailsId);
                    _uow.GetDbContext().RemoveRange(trainingData);


                    foreach (var item in model.EmployeeEvaluationModelList)
                    {
                        EmployeeEvaluationTraining obj = new EmployeeEvaluationTraining();
                        obj.TrainingProgram = item.TrainingProgram;
                        obj.Program = item.Program;
                        obj.Participated = item.Participated;
                        obj.CatchLevel = item.CatchLevel;
                        obj.RefresherTrm = item.RefresherTrm;
                        obj.OthRecommendation = item.OthRecommendation;

                        obj.CreatedById = UserId;
                        obj.CreatedDate = DateTime.Now;

                        obj.EmployeeAppraisalDetailsId = model.EmployeeAppraisalDetailsId;
                        trainingList.Add(obj);
                    }
                    await _uow.GetDbContext().EmployeeEvaluationTraining.AddRangeAsync(trainingList);

                    //AppraisalTeamMemberList
                    var appraisalTeamMemberData = await _uow.EmployeeAppraisalTeamMemberRepository.FindAllAsync(x => x.EmployeeAppraisalDetailsId == model.EmployeeAppraisalDetailsId);
                    _uow.GetDbContext().RemoveRange(appraisalTeamMemberData);

                    foreach (var item in model.EmployeeAppraisalTeamMemberList)
                    {
                        EmployeeAppraisalTeamMember obj = new EmployeeAppraisalTeamMember();
                        obj.EmployeeAppraisalDetailsId = model.EmployeeAppraisalDetailsId;
                        //obj.EmployeeAppraisalTeamMemberId = item;
                        obj.EmployeeId = item;

                        obj.CreatedById = UserId;
                        obj.CreatedDate = DateTime.Now;

                        appraisalTeamMemberList.Add(obj);
                    }
                    await _uow.GetDbContext().EmployeeAppraisalTeamMember.AddRangeAsync(appraisalTeamMemberList);

                    //EmployeeEvaluation objEval = new EmployeeEvaluation();

                    recordList.FinalResultQues1 = model.FinalResultQues1;
                    recordList.FinalResultQues2 = model.FinalResultQues2;
                    recordList.FinalResultQues3 = model.FinalResultQues3;
                    recordList.FinalResultQues4 = model.FinalResultQues4;
                    recordList.FinalResultQues5 = model.FinalResultQues5;
                    recordList.DirectSupervisor = model.DirectSupervisor;
                    //recordList.AppraisalTeamMember1 = model.AppraisalTeamMember1;
                    //recordList.AppraisalTeamMember2 = model.AppraisalTeamMember2;
                    recordList.CommentsByEmployee = model.CommentsByEmployee;
                    recordList.CurrentAppraisalDate = model.CurrentAppraisalDate;
                    recordList.EmployeeId = model.EmployeeId;

                    await _uow.EmployeeEvaluationRepository.UpdateAsyn(recordList);

                    var empRecords = await _uow.StrongandWeakPointsRepository.FindAllAsync(x => x.IsDeleted == false && x.EmployeeAppraisalDetailsId == model.EmployeeAppraisalDetailsId);
                    _uow.GetDbContext().StrongandWeakPoints.RemoveRange(empRecords);

                    foreach (var item in model.StrongPoints)
                    {
                        StrongandWeakPoints obj = new StrongandWeakPoints();
                        obj.CreatedById = UserId;
                        obj.CreatedDate = DateTime.Now;
                        obj.CurrentAppraisalDate = DateTime.Now;
                        obj.EmployeeId = model.EmployeeId;
                        obj.Point = item;
                        obj.Status = 1;         // 1 for strong points
                        obj.EmployeeAppraisalDetailsId = model.EmployeeAppraisalDetailsId;
                        StrongList.Add(obj);
                    }
                    await _uow.GetDbContext().StrongandWeakPoints.AddRangeAsync(StrongList);

                    foreach (var item in model.WeakPoints)
                    {
                        StrongandWeakPoints obj = new StrongandWeakPoints();
                        obj.CreatedById = UserId;
                        obj.CreatedDate = DateTime.Now;
                        obj.CurrentAppraisalDate = DateTime.Now;
                        obj.EmployeeId = model.EmployeeId;
                        obj.Point = item;
                        obj.Status = 2;         // 2 for Weak points
                        obj.EmployeeAppraisalDetailsId = model.EmployeeAppraisalDetailsId;
                        WeakList.Add(obj);
                    }
                    await _uow.GetDbContext().StrongandWeakPoints.AddRangeAsync(WeakList);
                }
                await _uow.SaveAsync();

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

        public async Task<APIResponse> GetAllEmployeeAppraisalMoreDetails(int OfficeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();

                var emplst = await _uow.EmployeeAppraisalDetailsRepository.FindAllAsync(x => x.OfficeId == OfficeId && x.AppraisalStatus == true && x.IsDeleted == false);
                foreach (var item in emplst)
                {
                    EmployeeAppraisalDetailsModel objAppraisal = new EmployeeAppraisalDetailsModel();

                    var empDetails = await _uow.EmployeeEvaluationRepository.FindAllAsync(x => x.EmployeeAppraisalDetailsId == item.EmployeeAppraisalDetailsId && (x.EvaluationStatus == null || x.EvaluationStatus == "approved"));

                    List<EmployeeEvaluationTrainingModel> trainingList = new List<EmployeeEvaluationTrainingModel>();
                    List<int> appraisalTeamMemberList = new List<int>();

                    List<string> strong = new List<string>();
                    List<string> weak = new List<string>();

                    foreach (var elements in empDetails)
                    {
                        //Training
                        var trainingData = await _uow.EmployeeEvaluationTrainingRepository.FindAllAsync(x => x.EmployeeAppraisalDetailsId == item.EmployeeAppraisalDetailsId && x.IsDeleted == false);

                        foreach (var ele in trainingData)
                        {
                            EmployeeEvaluationTrainingModel objTraining = new EmployeeEvaluationTrainingModel();

                            objTraining.TrainingProgram = ele.TrainingProgram;
                            objTraining.RefresherTrm = ele.RefresherTrm;
                            objTraining.Program = ele.Program;
                            objTraining.Participated = ele.Participated;
                            objTraining.OthRecommendation = ele.OthRecommendation;
                            objTraining.EmployeeEvaluationTrainingId = ele.EmployeeEvaluationTrainingId;
                            objTraining.EmployeeAppraisalDetailsId = ele.EmployeeAppraisalDetailsId;
                            objTraining.CatchLevel = ele.CatchLevel;

                            trainingList.Add(objTraining);
                        }

                        //AppraisalTeamMemberList
                        var appraisalTeamMemberData = await _uow.EmployeeAppraisalTeamMemberRepository.FindAllAsync(x => x.EmployeeAppraisalDetailsId == item.EmployeeAppraisalDetailsId && x.IsDeleted == false);

                        foreach (var teamElement in appraisalTeamMemberData)
                        {
                            //EmployeeAppraisalTeamMemberModel obj = new EmployeeAppraisalTeamMemberModel();
                            //obj.EmployeeAppraisalDetailsId = teamElement.EmployeeAppraisalDetailsId;
                            //obj.EmployeeAppraisalTeamMemberId = teamElement.EmployeeAppraisalTeamMemberId;
                            //obj.EmployeeId = teamElement.EmployeeId;

                            appraisalTeamMemberList.Add(teamElement.EmployeeId);
                        }

                        //Strong n Weak
                        var strongAndWeakPointList = await _uow.StrongandWeakPointsRepository.FindAllAsync(x => x.EmployeeAppraisalDetailsId == item.EmployeeAppraisalDetailsId && x.IsDeleted == false);

                        foreach (var count in strongAndWeakPointList)
                        {
                            if (count.Status == 1)
                            {
                                strong.Add(count.Point);
                            }
                            if (count.Status == 2)
                            {
                                weak.Add(count.Point);
                            }
                        }

                        objAppraisal.EmployeeEvaluationId = elements.EmployeeEvaluationId;
                        objAppraisal.EmployeeAppraisalDetailsId = elements.EmployeeAppraisalDetailsId;
                        objAppraisal.EmployeeId = elements.EmployeeId;
                        objAppraisal.FinalResultQues1 = elements.FinalResultQues1;
                        objAppraisal.FinalResultQues2 = elements.FinalResultQues2;
                        objAppraisal.FinalResultQues3 = elements.FinalResultQues3;
                        objAppraisal.FinalResultQues4 = elements.FinalResultQues4;
                        objAppraisal.FinalResultQues5 = elements.FinalResultQues5;
                        objAppraisal.DirectSupervisor = elements.DirectSupervisor;
                        //objAppraisal.AppraisalTeamMember1 = elements.AppraisalTeamMember1;
                        //objAppraisal.AppraisalTeamMember2 = elements.AppraisalTeamMember2;
                        objAppraisal.CommentsByEmployee = elements.CommentsByEmployee;
                        objAppraisal.CurrentAppraisalDate = elements.CurrentAppraisalDate;
                        objAppraisal.EvaluationStatus = elements.EvaluationStatus;
                        objAppraisal.EmployeeEvaluationModelList = trainingList;
                        objAppraisal.StrongPoints = strong;
                        objAppraisal.WeakPoints = weak;
                        objAppraisal.EmployeeAppraisalTeamMemberList = appraisalTeamMemberList;
                        lst.Add(objAppraisal);

                    }
                }

                response.data.EmployeeAppraisalDetailsModelLst = lst;
                //var finalLst = lst.GroupBy(x => x.EmployeeId).ToList();
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

        public async Task<APIResponse> ApproveEmployeeEvaluationRequest(int EmployeeEvaluationId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();
                var emplst = await _uow.EmployeeEvaluationRepository.FindAsync(x => x.EmployeeEvaluationId == EmployeeEvaluationId);
                emplst.EvaluationStatus = "approved";
                emplst.ModifiedById = UserId;
                emplst.ModifiedDate = DateTime.Now;
                await _uow.EmployeeEvaluationRepository.UpdateAsyn(emplst);
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

        public async Task<APIResponse> RejectEmployeeEvaluationRequest(int EmployeeEvaluationId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();
                var emplst = await _uow.EmployeeEvaluationRepository.FindAsync(x => x.EmployeeEvaluationId == EmployeeEvaluationId);
                emplst.EvaluationStatus = "reject";
                emplst.ModifiedById = UserId;
                emplst.ModifiedDate = DateTime.Now;
                await _uow.EmployeeEvaluationRepository.UpdateAsyn(emplst);
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

        public async Task<APIResponse> GetEmployeeDetailByOfficeId(int OfficeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                response.data.EmployeeDetailListData = await _uow.GetDbContext().EmployeeDetail.Include(x => x.EmployeeProfessionalDetail).Where(x => x.EmployeeProfessionalDetail.OfficeId == OfficeId && x.EmployeeTypeId == 2).Select(x => new EmployeeDetailList
                {
                    EmployeeId = x.EmployeeID,
                    EmployeeName = x.EmployeeName,
                    EmployeeCode = x.EmployeeCode
                }).ToListAsync();
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

        public async Task<APIResponse> GetEmployeeDetailByEmployeeId(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                response.data.EmployeeDetailListData = await _uow.GetDbContext().EmployeeDetail.Include(x => x.EmployeeProfessionalDetail).Include(x => x.EmployeeProfessionalDetail.OfficeDetail).Include(x => x.EmployeeProfessionalDetail.Department).Include(x => x.QualificationDetails).Include(x => x.EmployeeProfessionalDetail.DesignationDetails).Where(x => x.EmployeeID == EmployeeId).Select(x => new EmployeeDetailList
                {
                    EmployeeId = x.EmployeeID,
                    EmployeeName = x.EmployeeName,
                    EmployeeCode = x.EmployeeCode,
                    FathersName = x.FatherName,
                    Position = x.EmployeeProfessionalDetail.DesignationDetails.Designation,
                    Department = x.EmployeeProfessionalDetail.Department.DepartmentName,
                    Qualification = x.QualificationDetails.QualificationName,
                    DutyStation = x.EmployeeProfessionalDetail.OfficeDetail.OfficeName,
                    RecruitmentDate = x.EmployeeProfessionalDetail.HiredOn,
                    TenureWithCHA = (DateTime.Now.Date - x.EmployeeProfessionalDetail.HiredOn.Value.Date).Days.ToString() + " Days",
                    Gender = x.SexId == 1 ? "Male" : x.SexId == 2 ? "Female" : "Other"
                }).ToListAsync();
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

        public async Task<APIResponse> AddInterviewTechnicalQuestions(InterviewTechnicalQuestions model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {
                    model.CreatedById = UserId;
                    model.CreatedDate = DateTime.Now;
                    model.IsDeleted = false;
                    await _uow.InterviewTechnicalQuestionsRepository.AddAsyn(model);
                    await _uow.SaveAsync();
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

        public async Task<APIResponse> EditInterviewTechnicalQuestions(InterviewTechnicalQuestions model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {
                    var obj = await _uow.InterviewTechnicalQuestionsRepository.FindAsync(x => x.OfficeId == model.OfficeId && x.InterviewTechnicalQuestionsId == model.InterviewTechnicalQuestionsId);
                    obj.Question = model.Question;
                    obj.ModifiedById = UserId;
                    obj.ModifiedDate = DateTime.Now;
                    obj.IsDeleted = false;
                    await _uow.InterviewTechnicalQuestionsRepository.UpdateAsyn(obj);
                    await _uow.SaveAsync();
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

        public async Task<APIResponse> GetAllInterviewTechnicalQuestionsByOfficeId(int OfficeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                response.data.InterviewTechnicalQuestionsList = await _uow.GetDbContext().InterviewTechnicalQuestions.Where(x => x.OfficeId == OfficeId).ToListAsync();
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
        public async Task<APIResponse> AddExitInterview(ExitInterviewModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ExistInterviewDetails obj = _mapper.Map<ExistInterviewDetails>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.ExistInterviewDetailsRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> EditExitInterview(ExitInterviewModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ExistInterviewDetails obj = await _uow.ExistInterviewDetailsRepository.FindAsync(x => x.ExistInterviewDetailsId == model.ExistInterviewDetailsId);
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.ExistInterviewDetailsRepository.UpdateAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> GetAllExitInterview()
        {
            APIResponse response = new APIResponse();
            try
            {
                var exitInterviewList = await _uow.GetDbContext().ExistInterviewDetails.Where(x => x.IsDeleted == false).ToListAsync();
                List<ExitInterviewModel> lst = new List<ExitInterviewModel>();
                foreach (var item in exitInterviewList)
                {
                    ExitInterviewModel model = new ExitInterviewModel();
                    _mapper.Map(item, model);
                    var empRecord = await _uow.GetDbContext().EmployeeDetail.Include(x => x.EmployeeProfessionalDetail).Include(x => x.EmployeeProfessionalDetail.DesignationDetails).Include(x => x.EmployeeProfessionalDetail.Department).Where(x => x.EmployeeID == item.EmployeeID).ToListAsync();
                    if (empRecord != null)
                    {
                        model.EmployeeCode = empRecord[0].EmployeeCode;
                        model.EmployeeName = empRecord[0].EmployeeName;
                        model.Position = empRecord[0].EmployeeProfessionalDetail?.DesignationDetails?.Designation ?? null;
                        model.Department = empRecord[0].EmployeeProfessionalDetail?.Department?.DepartmentName ?? null;
                        model.TenureWithCHA = (DateTime.Now.Date - empRecord[0].EmployeeProfessionalDetail.HiredOn.Value.Date).Days.ToString() + " Days";
                        model.Gender = empRecord[0].SexId == 1 ? "Male" : empRecord[0].SexId == 2 ? "Female" : "Other";
                    }
                    lst.Add(model);
                }
                response.data.ExitInterviewList = lst;
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

        public async Task<APIResponse> ApproveEmployeeInterviewRequest(int InterviewDetailsId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();
                var emplst = await _uow.InterviewDetailsRepository.FindAsync(x => x.InterviewDetailsId == InterviewDetailsId);
                emplst.InterviewStatus = "approved";
                emplst.ModifiedById = UserId;
                emplst.ModifiedDate = DateTime.Now;
                await _uow.InterviewDetailsRepository.UpdateAsyn(emplst);
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

        public async Task<APIResponse> RejectEmployeeInterviewRequest(int InterviewDetailsId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();
                var emplst = await _uow.InterviewDetailsRepository.FindAsync(x => x.InterviewDetailsId == InterviewDetailsId);
                emplst.InterviewStatus = "reject";
                emplst.ModifiedById = UserId;
                emplst.ModifiedDate = DateTime.Now;
                await _uow.InterviewDetailsRepository.UpdateAsyn(emplst);
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
