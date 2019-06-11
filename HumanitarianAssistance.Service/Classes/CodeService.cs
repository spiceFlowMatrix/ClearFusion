using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Marketing;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using HumanitarianAssistance.ViewModels.Models.Project;
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
                                   }).OrderBy(x => x.CountryName).ToList();
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
                                    }).OrderBy(x => x.ProvinceName).ToList();
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
                                       }).OrderBy(x => x.NationalityName).ToList();
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
                                         }).OrderBy(x => x.QualificationName).ToList();
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
                    obj.IsDeleted = false;
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
                                 }).OrderBy(x => x.RoundTypeName).ToList();
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
                    obj.IsDeleted = false;
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
                }).OrderBy(x => x.ReasonName).ToList();
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
                if (model.IsDefault == true)
                {
                    foreach (var i in yearlist)
                    {
                        var existrecord1 = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDeleted == false && x.FinancialYearId == i.FinancialYearId);
                        existrecord1.IsDefault = false;
                        await _uow.FinancialYearDetailRepository.UpdateAsyn(existrecord1);
                    }
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
                //int? recordexist = _uow.HolidayDetailsRepository.FindAllAsync(x => x.FinancialYearId == model.FinancialYearId).Result.FirstOrDefault().FinancialYearId;

                //if (recordexist > 0)
                //{

                //}

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
                obj.IsDeleted = false;
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
                }).OrderBy(x => x.BudgetLineTypeName).ToList();
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
                var list = await _uow.EmployeeTypeRepository.FindAllAsync(x => x.IsDeleted == false);

                var employeetypelist = list.Select(x => new EmployeeTypeModel
                {
                    EmployeeTypeId = x.EmployeeTypeId,
                    EmployeeTypeName = x.EmployeeTypeName
                }).OrderBy(x => x.EmployeeTypeName).ToList();

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
                    }).OrderBy(x => x.DepartmentName).ToList();
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
                    obj.IsDeleted = false;
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
                obj.IsDeleted = false;
                await _uow.SalaryHeadDetailsRepository.AddAsyn(obj);
                await _uow.SaveAsync();

                List<int> employeeIds = await _uow.GetDbContext().EmployeeDetail.Where(x => x.IsDeleted == false && x.EmployeeTypeId == (int)EmployeeTypeStatus.Active).Select(x => x.EmployeeID).ToListAsync();

                if (employeeIds.Any())
                {
                    List<EmployeePayroll> employeePayrollList = new List<EmployeePayroll>();

                    foreach (int employeeid in employeeIds)
                    {
                        EmployeePayroll employeePayroll = new EmployeePayroll();
                        employeePayroll.IsDeleted = false;
                        employeePayroll.AccountNo = model.AccountNo;
                        employeePayroll.SalaryHeadId = obj.SalaryHeadId;
                        employeePayroll.HeadTypeId = model.HeadTypeId;
                        employeePayroll.AccountNo = model.AccountNo;
                        employeePayroll.TransactionTypeId = model.TransactionTypeId;
                        employeePayroll.MonthlyAmount = (double)model.MonthlyAmount;
                        employeePayrollList.Add(employeePayroll);
                    }

                    await _uow.GetDbContext().EmployeePayroll.AddRangeAsync(employeePayrollList);
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

        public async Task<APIResponse> EditSalaryHead(SalaryHeadModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existrecord = await _uow.SalaryHeadDetailsRepository.FindAsync(x => x.SalaryHeadId == model.SalaryHeadId);

                if (existrecord != null)
                {
                    existrecord.HeadName = model.HeadName;
                    existrecord.AccountNo = model.AccountNo;
                    existrecord.TransactionTypeId = model.TransactionTypeId;
                    existrecord.Description = model.Description;
                    existrecord.HeadTypeId = model.HeadTypeId;
                    existrecord.ModifiedById = model.ModifiedById;
                    existrecord.ModifiedDate = model.ModifiedDate;
                    existrecord.IsDeleted = false;
                    await _uow.SalaryHeadDetailsRepository.UpdateAsyn(existrecord);
                }

                if (model.SaveForAll)
                {
                    List<EmployeePayroll> employeePayrollList = await _uow.GetDbContext().EmployeePayroll.Where(x => x.IsDeleted == false && x.SalaryHeadId == model.SalaryHeadId).ToListAsync();

                    if (employeePayrollList.Any())
                    {
                        employeePayrollList.ForEach(x =>
                        {
                            x.AccountNo = model.AccountNo; x.TransactionTypeId = model.TransactionTypeId;
                            x.HeadTypeId = model.HeadTypeId;
                        });

                        _uow.GetDbContext().EmployeePayroll.UpdateRange(employeePayrollList);
                        _uow.Save();
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


        public async Task<APIResponse> DeleteSalaryHead(SalaryHeadModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existrecord = await _uow.SalaryHeadDetailsRepository.FindAsync(x => x.SalaryHeadId == model.SalaryHeadId);
                if (existrecord != null)
                {
                    existrecord.IsDeleted = true;

                    await _uow.SalaryHeadDetailsRepository.UpdateAsyn(existrecord);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "No Record Found";
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
                    Description = x.Description,
                    AccountNo = x?.AccountNo ?? 0,
                    TransactionTypeId = x?.TransactionTypeId ?? 0
                }).OrderBy(x => x.HeadName).ToList();

                response.data.SalaryHeadList = salaryheadlist.OrderBy(x => x.TransactionTypeId).ThenBy(x => x.HeadTypeId).ToList();
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
                        obj.IsDeleted = false;

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
                obj.IsDeleted = false;
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

        public async Task<APIResponse> GetAppraisalQuestions()
        {
            APIResponse response = new APIResponse();
            try
            {
                response.data.AppraisalList = await _uow.GetDbContext().AppraisalGeneralQuestions.Where(x => x.IsDeleted == false).ToListAsync();
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
                obj.IsDeleted = false;
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
                var emp = await _uow.EmployeeAppraisalDetailsRepository.FindAsync(x => x.EmployeeAppraisalDetailsId== model.EmployeeAppraisalDetailsId);
                emp.Position = model.Position;
                emp.Department = model.Department;
                emp.DutyStation = model.DutyStation;
                emp.AppraisalPeriod = model.AppraisalPeriod;
                emp.TotalScore = model.TotalScore;
                foreach (var item in model.EmployeeAppraisalQuestionList)
                {
                    var question = await _uow.EmployeeAppraisalQuestionsRepository.FindAsync(x => x.EmployeeAppraisalQuestionsId== item.EmployeeAppraisalQuestionsId);
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
                var emplst = await _uow.EmployeeAppraisalDetailsRepository.FindAllAsync(x => x.OfficeId == OfficeId && x.AppraisalStatus == false && x.IsDeleted == false);
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
                        questions.SequenceNo = element.AppraisalGeneralQuestions.SequenceNo.Value;
                        questions.AppraisalGeneralQuestionsId = element.AppraisalGeneralQuestionsId;
                        questions.Score = element.Score;
                        questions.Remarks = element.Remarks;
                        questions.EmployeeAppraisalQuestionsId = element.EmployeeAppraisalQuestionsId;
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

        public async Task<APIResponse> GetAllEmployeeAppraisalDetailsByEmployeeId(int EmployeeId, DateTime CurrentAppraisalDate)
        {
            APIResponse response = new APIResponse();
            try
            {
                var empAppraisalDetails = await _uow.GetDbContext().EmployeeAppraisalDetails.Where(x => x.EmployeeId == EmployeeId && x.CurrentAppraisalDate.Date.Day == CurrentAppraisalDate.Date.Day && x.CurrentAppraisalDate.Date.Month == CurrentAppraisalDate.Date.Month && x.CurrentAppraisalDate.Date.Year == CurrentAppraisalDate.Date.Year && x.IsDeleted == false && x.AppraisalStatus == true).OrderByDescending(x => x.CurrentAppraisalDate).FirstOrDefaultAsync();
                if (empAppraisalDetails != null)
                {
                    EmployeeAppraisalDetailsModel model = new EmployeeAppraisalDetailsModel();
                    var quesLst = await _uow.GetDbContext().EmployeeAppraisalQuestions.Include(x => x.AppraisalGeneralQuestions).Where(x => x.EmployeeId == empAppraisalDetails.EmployeeId && x.CurrentAppraisalDate.Date.Day == CurrentAppraisalDate.Date.Day && x.CurrentAppraisalDate.Date.Month == CurrentAppraisalDate.Date.Month && x.CurrentAppraisalDate.Date.Year == CurrentAppraisalDate.Date.Year).ToListAsync();
                    model.EmployeeAppraisalDetailsId = empAppraisalDetails.EmployeeAppraisalDetailsId;
                    model.EmployeeId = empAppraisalDetails.EmployeeId;
                    model.EmployeeCode = empAppraisalDetails.EmployeeCode;
                    model.EmployeeName = empAppraisalDetails.EmployeeName;
                    model.FatherName = empAppraisalDetails.FatherName;
                    model.Position = empAppraisalDetails.Position;
                    model.Department = empAppraisalDetails.Department;
                    model.Qualification = empAppraisalDetails.Qualification;
                    model.DutyStation = empAppraisalDetails.DutyStation;
                    model.RecruitmentDate = empAppraisalDetails.RecruitmentDate;
                    model.AppraisalPeriod = empAppraisalDetails.AppraisalPeriod;
                    model.CurrentAppraisalDate = empAppraisalDetails.CurrentAppraisalDate;
                    model.OfficeId = empAppraisalDetails.OfficeId;
                    model.TotalScore = empAppraisalDetails.TotalScore;
                    model.AppraisalStatus = empAppraisalDetails.AppraisalStatus;
                    foreach (var element in quesLst)
                    {
                        EmployeeAppraisalQuestionModel questions = new EmployeeAppraisalQuestionModel();
                        questions.QuestionEnglish = element.AppraisalGeneralQuestions.Question;
                        questions.QuestionDari = element.AppraisalGeneralQuestions.DariQuestion;
                        questions.SequenceNo = element.AppraisalGeneralQuestions.SequenceNo.Value;
                        questions.AppraisalGeneralQuestionsId = element.AppraisalGeneralQuestionsId;
                        questions.Score = element.Score;
                        questions.Remarks = element.Remarks;
                        model.EmployeeAppraisalQuestionList.Add(questions);
                    }
                    response.data.EmployeeAppraisalDetailsModel = model;
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


        public async Task<APIResponse> RejectEmployeeAppraisalRequest(int EmployeeAppraisalDetailsId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();
                var emplst = await _uow.EmployeeAppraisalDetailsRepository.FindAsync(x => x.EmployeeAppraisalDetailsId == EmployeeAppraisalDetailsId && x.AppraisalStatus == false);
                emplst.AppraisalStatus = false;
                emplst.ModifiedById = UserId;
                emplst.ModifiedDate = DateTime.Now;
                emplst.IsDeleted = true;
                await _uow.EmployeeAppraisalDetailsRepository.UpdateAsyn(emplst);

                EmployeeEvaluation evaluationModel = new EmployeeEvaluation();
                evaluationModel.CreatedById = UserId;
                evaluationModel.CreatedDate = DateTime.Now;
                evaluationModel.EmployeeId = emplst.EmployeeId;
                evaluationModel.CurrentAppraisalDate = emplst.CurrentAppraisalDate;
                evaluationModel.IsDeleted = true;
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

        public async Task<APIResponse> GetAllEmployeeList()
        {
            APIResponse response = new APIResponse();
            try
            {

                response.data.EmployeeDetailListData = await _uow.GetDbContext().EmployeeDetail
                                                            .Where(x => x.EmployeeTypeId == (int)EmployeeTypeStatus.Active)
                                                            .Select(x => new EmployeeDetailList
                                                            {
                                                                EmployeeId = x.EmployeeID,
                                                                EmployeeName = x.EmployeeName,
                                                                EmployeeCode = x.EmployeeCode ?? x.EmployeeProfessionalDetail.OfficeDetail.OfficeCode + x.EmployeeID,
                                                                CodeEmployeeName = x.EmployeeCode != null ? x.EmployeeCode + " - " + x.EmployeeName : x.EmployeeProfessionalDetail.OfficeDetail.OfficeCode + x.EmployeeID + " - " + x.EmployeeName
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

        public async Task<APIResponse> GetEmployeeDetailByOfficeId(int OfficeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var OfficeDetail = await _uow.OfficeDetailRepository.FindAsync(x => x.OfficeId == OfficeId);

                response.data.EmployeeDetailListData = await _uow.GetDbContext().EmployeeDetail.Include(x => x.EmployeeProfessionalDetail).Where(x => x.EmployeeProfessionalDetail.OfficeId == OfficeId && x.EmployeeTypeId != 1 && x.IsDeleted== false).Select(x => new EmployeeDetailList
                {
                    EmployeeId = x.EmployeeID,
                    EmployeeName = x.EmployeeName,
                    EmployeeCode = x.EmployeeCode != null ? x.EmployeeCode : OfficeDetail.OfficeCode + x.EmployeeID,
                    CodeEmployeeName = x.EmployeeCode != null ? x.EmployeeCode + " - " + x.EmployeeName : OfficeDetail.OfficeCode + x.EmployeeID + " - " + x.EmployeeName
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


                //response.data.EmployeeDetailListData = await _uow.GetDbContext().EmployeeContract.Where(x => x.EmployeeId == EmployeeId).Select(x => new EmployeeDetailList /*Include(x=> x.Employee).Include(x => x.Employee.EmployeeProfessionalDetail).Include(x => x.Employee.EmployeeProfessionalDetail.OfficeDetail).Include(x => x.Employee.EmployeeProfessionalDetail.Department).Include(x => x.Employee.QualificationDetails).Include(x => x.Employee.EmployeeProfessionalDetail.DesignationDetails)*/
                //{
                //    EmployeeId = x.Employee.EmployeeID,
                //    EmployeeName = x.Employee.EmployeeName,
                //    EmployeeCode = x.Employee.EmployeeCode,
                //    FathersName = x.Employee.FatherName,
                //    Position = x.Employee.EmployeeProfessionalDetail.DesignationDetails.Designation,
                //    Department = x.Employee.EmployeeProfessionalDetail.Department.DepartmentName,
                //    Qualification = x.Employee.QualificationDetails.QualificationName,
                //    DutyStation = x.Employee.EmployeeProfessionalDetail.OfficeDetail.OfficeName,
                //    OfficeId= x.Employee.EmployeeProfessionalDetail.OfficeDetail.OfficeId,
                //    RecruitmentDate = x.Employee.EmployeeProfessionalDetail.HiredOn,
                //    ContractStartDate= x.ContractStartDate,
                //    ContractEndDate= x.ContractEndDate,
                //    TenureWithCHA = (DateTime.Now.Date - x.Employee.EmployeeProfessionalDetail.HiredOn.Value.Date).Days.ToString() + " Days",
                //    Gender = x.Employee.SexId == 1 ? "Male" : x.Employee.SexId == 2 ? "Female" : "Other"
                //}).ToListAsync();
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
                obj.IsDeleted = false;
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

        public async Task<APIResponse> DeleteExitInterview(int existInterviewDetailsId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var exitInterviewRecord = await _uow.ExistInterviewDetailsRepository.FindAsync(x => x.IsDeleted == false && x.ExistInterviewDetailsId == existInterviewDetailsId);
                if (exitInterviewRecord != null)
                {
                    exitInterviewRecord.ModifiedById = UserId;
                    exitInterviewRecord.ModifiedDate = DateTime.UtcNow;
                    exitInterviewRecord.IsDeleted = true;
                    await _uow.ExistInterviewDetailsRepository.UpdateAsyn(exitInterviewRecord);

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


        public async Task<APIResponse> GetSalaryTaxReportContentDetails(int officeId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var salaryTaxContentdata = await _uow.SalaryTaxReportContentRepository.FindAsync(x => x.OfficeId == officeId && x.IsDeleted == false);

                if (salaryTaxContentdata != null)
                {
                    response.data.SalaryTaxReportContentDetails = salaryTaxContentdata;
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

        public async Task<APIResponse> AddSalaryTaxReportContentDetails(SalaryTaxReportContent model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {

                    model.CreatedDate = DateTime.Now;
                    model.CreatedById = UserId;
                    model.IsDeleted = false;

                    await _uow.SalaryTaxReportContentRepository.AddAsyn(model);

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

        public async Task<APIResponse> EditSalaryTaxReportContentDetails(SalaryTaxReportContent model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existrecord = await _uow.SalaryTaxReportContentRepository.FindAsync(x => x.SalaryTaxReportContentId == model.SalaryTaxReportContentId);
                if (existrecord != null)
                {
                    existrecord.EmployerAuthorizedOfficerName = model.EmployerAuthorizedOfficerName;
                    existrecord.PositionAuthorizedOfficer = model.PositionAuthorizedOfficer;
                    existrecord.OfficeId = model.OfficeId;
                    existrecord.ModifiedById = model.ModifiedById;
                    existrecord.ModifiedDate = model.ModifiedDate;
                    existrecord.IsDeleted = model.IsDeleted;

                    await _uow.SalaryTaxReportContentRepository.UpdateAsyn(existrecord);

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

        /// <summary>
        /// Get Employee Advance History Detail
        /// </summary>
        /// <param name="AdvanceID"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetEmployeeAdvanceHistoryDetail(long AdvanceID)
        {
            APIResponse response = new APIResponse();

            try
            {
                var advances = await _uow.GetDbContext().EmployeePaymentTypes.Where(x => x.IsDeleted == false && x.AdvanceId == AdvanceID).ToListAsync();

                if (advances.Any())
                {

                    List<AdvancesHistoryModel> advancesHistory = advances.Select(x => new AdvancesHistoryModel
                    {
                        AdvanceId = x.AdvanceId,
                        BalanceAmount = x.AdvanceAmount.Value,
                        InstallmentPaid = x.AdvanceRecoveryAmount.Value,
                        PaymentDate = x.PaymentDate.Value.Date
                    }).ToList();

                    response.data.AdvanceHistory = advancesHistory;
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

        /// <summary>
        /// Get All Payroll Head Details
        /// </summary>
        /// <returns>List of Payroll Heads</returns>
        public async Task<APIResponse> GetAllPayrollHead()
        {

            APIResponse response = new APIResponse();

            try
            {
                ICollection<PayrollAccountHead> PayrollAccountHeadList = await _uow.PayrollAccountHeadRepository.FindAllAsync(x => x.IsDeleted == false);

                response.data.PayrollAccountHead = PayrollAccountHeadList;
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

        /// <summary>
        /// Adding Payroll Heads to PayrollAccountHead Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddPayrollAccountHead(PayrollHeadModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                PayrollAccountHead obj = _mapper.Map<PayrollAccountHead>(model);
                obj.IsDeleted = false;
                await _uow.PayrollAccountHeadRepository.AddAsyn(obj);
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

        /// <summary>
        /// Update Payroll Account Head
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> UpdatePayrollAccountHead(PayrollHeadModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                PayrollAccountHead xPayrollAccountHead = _uow.GetDbContext().PayrollAccountHead.FirstOrDefault(x => x.PayrollHeadId == model.PayrollHeadId);

                xPayrollAccountHead.AccountNo = model.AccountNo;
                xPayrollAccountHead.Description = model.Description;
                xPayrollAccountHead.PayrollHeadId = model.PayrollHeadId;
                xPayrollAccountHead.PayrollHeadName = model.PayrollHeadName;
                xPayrollAccountHead.PayrollHeadTypeId = model.PayrollHeadTypeId;
                xPayrollAccountHead.TransactionTypeId = model.TransactionTypeId;
                xPayrollAccountHead.IsDeleted = false;

                await _uow.PayrollAccountHeadRepository.UpdateAsyn(xPayrollAccountHead);
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

        /// <summary>
        /// Delete Payroll Account Head
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Boolean</returns>
        public async Task<APIResponse> DeletePayrollAccountHead(PayrollHeadModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                PayrollAccountHead xPayrollAccountHead = _uow.GetDbContext().PayrollAccountHead.FirstOrDefault(x => x.PayrollHeadId == model.PayrollHeadId);
                xPayrollAccountHead.IsDeleted = true;

                await _uow.PayrollAccountHeadRepository.UpdateAsyn(xPayrollAccountHead);
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

        /// <summary>
        /// Update the changes of payroll heads for all employees
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> UpdatePayrollAccountHeadAllEmployees(List<PayrollHeadModel> model, string UserId)
        {

            APIResponse response = new APIResponse();

            try
            {
                //NOTE: Do not remove code for saving payroll heads for employees that do not exist in EmployeePayrollAccountHead table
                //List<EmployeeProfessionalDetail> employeesNotInPayrollAccountHeadTable = new List<EmployeeProfessionalDetail>();
                //List<EmployeeProfessionalDetail> employeeList = await _uow.GetDbContext().EmployeeProfessionalDetail.Where(x => x.IsDeleted == false).ToListAsync();

                IEnumerable<int> employeeIds = _uow.GetDbContext().EmployeeDetail
                                              .Where(x => x.IsDeleted == false).Select(x => x.EmployeeID).ToList();

                IEnumerable<int> employeeWithNoPayrollHead;
                IEnumerable<int> employeeWithPayrollHead;

                List<EmployeePayrollAccountHead> xEmployeePayrollAccountHead = await _uow.GetDbContext().EmployeePayrollAccountHead.Where(x => x.IsDeleted == false).ToListAsync();

                employeeWithPayrollHead = xEmployeePayrollAccountHead.Select(x => x.EmployeeId).Distinct().ToList();

                employeeWithNoPayrollHead = employeeIds.Except(employeeWithPayrollHead);

                foreach (PayrollHeadModel payrollHead in model)
                {
                    PayrollAccountHead xPayrollAccountHead = _uow.GetDbContext().PayrollAccountHead.FirstOrDefault(x => x.PayrollHeadId == payrollHead.PayrollHeadId);

                    xPayrollAccountHead.AccountNo = payrollHead.AccountNo;
                    xPayrollAccountHead.Description = payrollHead.Description;
                    xPayrollAccountHead.PayrollHeadId = payrollHead.PayrollHeadId;
                    xPayrollAccountHead.PayrollHeadName = payrollHead.PayrollHeadName;
                    xPayrollAccountHead.PayrollHeadTypeId = payrollHead.PayrollHeadTypeId;
                    xPayrollAccountHead.TransactionTypeId = payrollHead.TransactionTypeId;
                    xPayrollAccountHead.IsDeleted = false;

                    await _uow.PayrollAccountHeadRepository.UpdateAsyn(xPayrollAccountHead);
                    await _uow.SaveAsync();

                    List<EmployeePayrollAccountHead> xEmployeePayrollAccount = xEmployeePayrollAccountHead.Where(x => x.IsDeleted == false && x.PayrollHeadId == payrollHead.PayrollHeadId).ToList();

                    if (xEmployeePayrollAccount.Any())
                    {
                        xEmployeePayrollAccount.ForEach(x =>
                        {
                            x.AccountNo = payrollHead.AccountNo; x.Description = payrollHead.Description;
                            x.PayrollHeadTypeId = payrollHead.PayrollHeadTypeId; x.TransactionTypeId = payrollHead.TransactionTypeId;
                        });

                        _uow.GetDbContext().EmployeePayrollAccountHead.UpdateRange(xEmployeePayrollAccountHead);
                        await _uow.GetDbContext().SaveChangesAsync();
                    }

                    List<EmployeePayrollAccountHead> employeePayrollHeads = new List<EmployeePayrollAccountHead>();

                    //Adding New Payroll Heads for Employees not having Payroll Head already saved
                    foreach (int employeeId in employeeWithNoPayrollHead)
                    {
                        EmployeePayrollAccountHead employeePayrollAccountHead = new EmployeePayrollAccountHead();
                        employeePayrollAccountHead.AccountNo = payrollHead.AccountNo;
                        employeePayrollAccountHead.CreatedById = UserId;
                        employeePayrollAccountHead.Description = payrollHead.Description;
                        employeePayrollAccountHead.EmployeeId = employeeId;
                        employeePayrollAccountHead.IsDeleted = false;
                        employeePayrollAccountHead.PayrollHeadId = payrollHead.PayrollHeadId;
                        employeePayrollAccountHead.PayrollHeadName = payrollHead.PayrollHeadName;
                        employeePayrollAccountHead.PayrollHeadTypeId = payrollHead.PayrollHeadTypeId;
                        employeePayrollAccountHead.TransactionTypeId = payrollHead.TransactionTypeId;
                        employeePayrollHeads.Add(employeePayrollAccountHead);
                    }

                    _uow.GetDbContext().EmployeePayrollAccountHead.AddRange(employeePayrollHeads);
                    await _uow.GetDbContext().SaveChangesAsync();

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

        public async Task<APIResponse> GetAllDistrictDetailByProvinceId(List<int?> ProvinceId)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<DistrictDetail> districtlist = _uow.GetDbContext().DistrictDetail.Where(x => x.IsDeleted == false && ProvinceId.Contains(x.ProvinceID)).ToList();
                response.data.Districtlist = districtlist;
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

        #region Language
        /// <summary>
        /// get Languages List
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllLanguage()
        {
            APIResponse response = new APIResponse();
            try
            {
                ICollection<LanguageDetail> Languages = await _uow.LanguageRepository.FindAllAsync(x => x.IsDeleted == false);
                response.data.Languages = Languages;
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

        /// <summary>
        /// Delete Selected Language
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteLanguage(LanguageModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var languageDetails = await _uow.LanguageRepository.FindAsync(x => x.IsDeleted == false && x.LanguageId == model.LanguageId);
                if (languageDetails != null)
                {
                    languageDetails.ModifiedById = UserId;
                    languageDetails.ModifiedDate = DateTime.UtcNow;
                    languageDetails.IsDeleted = true;
                    await _uow.LanguageRepository.UpdateAsyn(languageDetails);

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

        /// <summary>
        /// Edit Selected Language
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditLanguage(LanguageModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                LanguageDetail obj = await _uow.LanguageRepository.FindAsync(x => x.LanguageId == model.LanguageId);
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.LanguageRepository.UpdateAsyn(obj);
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

        /// <summary>
        /// Add New Language
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddLanguage(LanguageModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                LanguageDetail obj = _mapper.Map<LanguageModel, LanguageDetail>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                obj.LanguageName = model.LanguageName;
                await _uow.LanguageRepository.AddAsyn(obj);
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
        #endregion

        /// <summary>
        /// Get Application Pages List
        /// </summary>
        /// <returns>Pages List</returns>
        public async Task<APIResponse> GetApplicationPages()
        {
            APIResponse response = new APIResponse();

            try
            {
                List<ApplicationPages> xApplicationPages = await _uow.GetDbContext().ApplicationPages.Where(x => x.IsDeleted == false).ToListAsync();

                response.data.ApplicationPagesList = xApplicationPages;
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

        /// <summary>
        /// Add/Edit Pension Debit Account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="userId"></param>
        /// <returns>Success/ Failure</returns>
        public async Task<APIResponse> AddEditPensionDebitAccount(long accountId, string userId)
        {
            APIResponse response = new APIResponse();
            PensionDebitAccountMaster pensionDebitAccount;

            try
            {
                pensionDebitAccount = _uow.GetDbContext().PensionDebitAccountMaster.FirstOrDefault(x => x.IsDeleted == false);

                if (pensionDebitAccount == null)
                {
                    pensionDebitAccount = new PensionDebitAccountMaster();
                    pensionDebitAccount.ChartOfAccountNewId = accountId;
                    pensionDebitAccount.CreatedDate = DateTime.UtcNow;
                    pensionDebitAccount.CreatedById = userId;
                    pensionDebitAccount.IsDeleted = false;

                    await _uow.GetDbContext().PensionDebitAccountMaster.AddAsync(pensionDebitAccount);
                    await _uow.GetDbContext().SaveChangesAsync();
                }
                else
                {
                    pensionDebitAccount.ModifiedById = userId;
                    pensionDebitAccount.ModifiedDate = DateTime.UtcNow;
                    pensionDebitAccount.ChartOfAccountNewId = accountId;

                     _uow.GetDbContext().PensionDebitAccountMaster.Update(pensionDebitAccount);
                    await _uow.GetDbContext().SaveChangesAsync();
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

        public async Task<APIResponse> GetPensionDebitAccount()
        {
            APIResponse response = new APIResponse();

            try
            {
                PensionDebitAccountMaster pensionDebitAccount = await _uow.GetDbContext().PensionDebitAccountMaster.FirstOrDefaultAsync(x => x.IsDeleted == false);

                if (pensionDebitAccount != null)
                {
                    response.data.PensionDebitAccountId = pensionDebitAccount.ChartOfAccountNewId;
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

        #region Attendance Groups

        /// <summary>
        /// Get Attendance Group List
        /// </summary>
        /// <returns>APIResponse</returns>
        public async Task<APIResponse> GetAttendanceGroups()
        {
            APIResponse response = new APIResponse();

            try
            {
                response.data.AttendanceGroupMasterList = await _uow.GetDbContext().AttendanceGroupMaster
                                                                    .Where(x => x.IsDeleted == false)
                                                                    .OrderByDescending(x=> x.CreatedDate)
                                                                    .Select(x=> new AttendanceGroupMasterModel
                                                                    {
                                                                       Description = x.Description,
                                                                       Id= x.AttendanceGroupId,
                                                                       Name= x.Name.ToLower().Trim()
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

        /// <summary>
        /// Add Attendance Group Item
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns>APIResponse</returns>
        public async Task<APIResponse> AddAttendanceGroups(AttendanceGroupMasterModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (model != null)
                {

                    AttendanceGroupMaster recordExists = await _uow.GetDbContext().AttendanceGroupMaster.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Name == model.Name.ToLower().Trim());

                    if (recordExists != null)
                    {
                        throw new Exception($"Attendance Group with Name '{model.Name}' already exists ");
                    }

                    AttendanceGroupMaster attendanceGroupMaster = new AttendanceGroupMaster
                    {
                        CreatedDate = DateTime.UtcNow,
                        CreatedById= userId,
                        Description= model.Description,
                        IsDeleted= false,
                        Name= model.Name
                    };

                    await _uow.GetDbContext().AttendanceGroupMaster.AddAsync(attendanceGroupMaster);
                    await _uow.GetDbContext().SaveChangesAsync();
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

        /// <summary>
        /// Edit Attendance Group Item
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns>APIResponse</returns>
        public async Task<APIResponse> EditAttendanceGroups(AttendanceGroupMasterModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (model != null)
                {
                    List<AttendanceGroupMaster> attendanceGroupMasterList = await _uow.GetDbContext().AttendanceGroupMaster.Where(x => x.IsDeleted == false && x.Name == model.Name.ToLower().Trim()).ToListAsync();

                    bool isRecordWithSameNameExists = attendanceGroupMasterList.Any(x => x.AttendanceGroupId != model.Id);

                    if (isRecordWithSameNameExists)
                    {
                        throw new Exception($"Attendance Group with Name '{model.Name}' already exists");
                    }

                    AttendanceGroupMaster attendanceGroupMaster = attendanceGroupMasterList.FirstOrDefault(x => x.IsDeleted == false && x.AttendanceGroupId == model.Id);

                    if (attendanceGroupMaster != null)
                    {
                        attendanceGroupMaster.Description = model.Description;
                        attendanceGroupMaster.Name = model.Name;
                        attendanceGroupMaster.ModifiedById = userId;
                        attendanceGroupMaster.ModifiedDate = DateTime.UtcNow;

                        _uow.GetDbContext().AttendanceGroupMaster.Update(attendanceGroupMaster);
                        await _uow.GetDbContext().SaveChangesAsync();
                    }

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

        #endregion
    }
}
