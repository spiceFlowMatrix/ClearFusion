using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.CommonServices
{
    public class HRService : IHRService
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public HRService(HumanitarianAssistanceDbContext dbContext, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> AddEmployeePayrollDetails(int? EmployeeId)
        {
            bool payrollSaved = false;

            try
            {
                bool employeeSalaryHeadAlreadyExists = _dbContext.EmployeePayroll.Any(x => x.IsDeleted == false && x.EmployeeID == EmployeeId);

                // add salary head and payroll heads only if it does not already exists for an employee
                if (!employeeSalaryHeadAlreadyExists)
                {
                    //Get Default payrollaccountheads and save it to the newly created employee
                    List<SalaryHeadDetails> salaryHeadDetails = await _dbContext.SalaryHeadDetails.Where(x => x.IsDeleted == false).ToListAsync();
                    List<EmployeePayroll> EmployeePayrollList = new List<EmployeePayroll>();

                    foreach (SalaryHeadDetails salaryHead in salaryHeadDetails)
                    {
                        EmployeePayroll employeePayroll = new EmployeePayroll();
                        employeePayroll.AccountNo = salaryHead.AccountNo;
                        employeePayroll.HeadTypeId = salaryHead.HeadTypeId;
                        employeePayroll.SalaryHeadId = salaryHead.SalaryHeadId;
                        employeePayroll.IsDeleted = false;
                        employeePayroll.TransactionTypeId = salaryHead.TransactionTypeId;
                        employeePayroll.EmployeeID = EmployeeId.Value;
                        EmployeePayrollList.Add(employeePayroll);
                    }

                    List<EmployeePayrollAccountHead> payrollAccountHeadList = await _dbContext.EmployeePayrollAccountHead
                                                                                          .Where(x => !x.IsDeleted && x.EmployeeId == EmployeeId)
                                                                                          .ToListAsync();

                    List<EmployeePayrollAccountHead> employeePayrollAccountHeads = new List<EmployeePayrollAccountHead>();

                    if (!payrollAccountHeadList.Any())
                    {
                        //Get Default payrollaccountheads and save it to the newly created employee
                        List<PayrollAccountHead> payrollAccountHeads = await _dbContext.PayrollAccountHead.Where(x => x.IsDeleted == false).ToListAsync();

                        foreach (var employeePayrollAccount in payrollAccountHeads)
                        {
                            EmployeePayrollAccountHead employeePayrollAccountHead = new EmployeePayrollAccountHead();

                            employeePayrollAccountHead.IsDeleted = false;
                            employeePayrollAccountHead.AccountNo = employeePayrollAccount.AccountNo;
                            employeePayrollAccountHead.Description = employeePayrollAccount.Description;
                            employeePayrollAccountHead.EmployeeId = EmployeeId.Value;
                            employeePayrollAccountHead.PayrollHeadId = employeePayrollAccount.PayrollHeadId;
                            employeePayrollAccountHead.PayrollHeadName = employeePayrollAccount.PayrollHeadName;
                            employeePayrollAccountHead.PayrollHeadTypeId = employeePayrollAccount.PayrollHeadTypeId;
                            employeePayrollAccountHead.TransactionTypeId = employeePayrollAccount.TransactionTypeId;

                            employeePayrollAccountHeads.Add(employeePayrollAccountHead);
                        }
                    }

                    await _dbContext.EmployeePayrollAccountHead.AddRangeAsync(employeePayrollAccountHeads);
                    await _dbContext.EmployeePayroll.AddRangeAsync(EmployeePayrollList);
                    await _dbContext.SaveChangesAsync();

                    payrollSaved = true;
                }
                else
                {
                    payrollSaved = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return payrollSaved;
            }
            return payrollSaved;
        }

        public async Task<ApiResponse> AddUser(UserModel request)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                AppUser newUser = new AppUser
                {
                    UserName = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.Phone
                };

                AppUser existUser = await _userManager.FindByNameAsync(request.Email);

                if (existUser == null)
                {
                    IdentityResult objNew = await _userManager.CreateAsync(newUser, request.Password);

                    if (!objNew.Succeeded)
                    {
                        throw new Exception("Could Not Create App User");
                    }

                    UserDetails user = new UserDetails();

                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.Password = request.Password;
                    user.Status = request.Status;
                    user.Username = request.Email;
                    user.CreatedById = request.CreatedById;
                    user.CreatedDate = request.CreatedDate;
                    user.UserType = request.UserType;
                    user.AspNetUserId = newUser.Id;
                    user.EmployeeId = request.EmployeeId;

                    await _dbContext.UserDetails.AddAsync(user);
                    await _dbContext.SaveChangesAsync();

                    List<UserDetailOffices> lst = new List<UserDetailOffices>();

                    foreach (var item in request.OfficeId)
                    {
                        UserDetailOffices obj = new UserDetailOffices();
                        obj.OfficeId = item.Value;
                        obj.UserId = user.UserID;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.IsDeleted = false;
                        lst.Add(obj);
                    }

                    await _dbContext.UserDetailOffices.AddRangeAsync(lst);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                    response.Message = StaticResource.EmailAlreadyExist;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ApiResponse> AddNewEmployee(AddNewEmployeeCommand request)
        {
            ApiResponse response = new ApiResponse();

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                EmployeeDetailModel model = new EmployeeDetailModel();

                try
                {
                    EmployeeDetail obj = _mapper.Map<EmployeeDetail>(request);
                    obj.IsDeleted = false;

                    await _dbContext.EmployeeDetail.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    model.EmployeeID = obj.EmployeeID;

                    // OfficeDetail OfficeDetail = await _dbContext.OfficeDetail.FirstOrDefaultAsync(x => x.OfficeId == request.OfficeId && x.IsDeleted == false);
                    obj.EmployeeCode = "E" + obj.EmployeeID;

                    _dbContext.EmployeeDetail.Update(obj);
                    await _dbContext.SaveChangesAsync();

                    // to add Multi currencuy Pension detail 


                    List<MultiCurrencyOpeningPension> pensionDetail = new List<MultiCurrencyOpeningPension>();

                    if (request.PensionDetailModel != null)
                    {
                        if (request.PensionDetailModel.PensionDetail != null && request.PensionDetailModel.PensionDetail.Count > 0)
                        {

                            foreach (var item in request.PensionDetailModel.PensionDetail)
                            {
                                MultiCurrencyOpeningPension detail = new MultiCurrencyOpeningPension()
                                {
                                    EmployeeID = obj.EmployeeID,
                                    PensionStartDate = request.PensionDetailModel.PensionDate,
                                    Amount = item.Amount,
                                    CreatedById = request.CreatedById,
                                    CreatedDate = request.CreatedDate,
                                    IsDeleted = false,
                                    CurrencyId = item.CurrencyId,
                                };

                                pensionDetail.Add(detail);
                            }
                            await _dbContext.MultiCurrencyOpeningPension.AddRangeAsync(pensionDetail);
                            await _dbContext.SaveChangesAsync();

                        }
                    }

                    EmployeeProfessionalDetailModel empprofessional = new EmployeeProfessionalDetailModel
                    {
                        EmployeeId = obj.EmployeeID,
                        EmployeeTypeId = request.EmployeeTypeId,
                        OfficeId = request.OfficeId,
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = request.IsDeleted,
                        ProfessionId = request.ProfessionId,
                        TinNumber = request.TinNumber,
                        HiredOn = request.HiredOn,
                        EmployeeContractTypeId = request.EmployeeContractTypeId,
                        FiredOn = request.FiredOn,
                        FiredReason = request.FiredReason,
                        ResignationOn = request.ResignationOn,
                        ResignationReason = request.ResignationReason,
                        AttendanceGroupId = request.AttendanceGroupId,
                        DutyStation = request.DutyStation,
                        DepartmentId = request.Department,
                        TrainingBenefits = request.TrainingAndBenefits,
                        JobDescription = request.JobDescription,
                        DesignationId = request.Designation
                    };

                    EmployeeProfessionalDetail obj1 = _mapper.Map<EmployeeProfessionalDetail>(empprofessional);
                    await _dbContext.EmployeeProfessionalDetail.AddAsync(obj1);
                    await _dbContext.SaveChangesAsync();

                    if (request.EmployeeTypeId != (int)EmployeeTypeStatus.Prospective)
                    {
                        bool isEmployeeSalaryHeadSaved = await AddEmployeePayrollDetails(obj.EmployeeID);

                        if (!isEmployeeSalaryHeadSaved)
                        {
                            throw new Exception(StaticResource.SalaryHeadNotSaved);
                        }
                    }

                    List<int?> office = new List<int?>();
                    office.Add(request.OfficeId);

                    //Add Employee to UserDetails Table
                    UserModel addUserCommand = new UserModel
                    {
                        Email = request.Email,
                        Phone = request.Phone,
                        FirstName = request.EmployeeName,
                        OfficeId = office,
                        EmployeeId = obj.EmployeeID,
                        Status = (int)UserStatus.Active,
                        Password = request.Password,
                        CreatedById = obj.CreatedById,
                        CreatedDate = obj.CreatedDate
                    };

                    response = await AddUser(addUserCommand);

                    if (response.StatusCode != 200)
                    {
                        throw new Exception(response.Message);
                    }

                    tran.Commit();


                    response.data.EmployeeDetailModel = model;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = ex.Message;
                }
            }
            return response;
        }
        public async Task<ApiResponse> AddBulkUser()
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var employees = _dbContext.EmployeeDetail.Where(r => r.Email != null).Include(x => x.EmployeeProfessionalDetail).Select(r => new UserModel
                {
                    Email = r.Email,
                    Phone = r.Phone,
                    FirstName = r.EmployeeName,
                    OfficeId = new List<int?>() { r.EmployeeProfessionalDetail.OfficeId },
                    EmployeeId = r.EmployeeID,
                    Status = (int)UserStatus.Active,
                    Password = "12345",
                    CreatedById = r.CreatedById,
                    CreatedDate = r.CreatedDate
                }).ToList();
                var existingUsers = await _dbContext.Users.Select(x => x.Email).ToListAsync();
                var users = employees.Where(r => !existingUsers.Contains(r.Email)).ToList();
                foreach (var item in users)
                {
                    AppUser newUser = new AppUser
                    {
                        UserName = item.Email,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        PhoneNumber = item.Phone
                    };
                    IdentityResult objNew = await _userManager.CreateAsync(newUser, item.Password);

                    if (!objNew.Succeeded)
                    {
                        throw new Exception("Could Not Create App User");
                    }

                    UserDetails user = new UserDetails();

                    user.FirstName = item.FirstName;
                    user.LastName = item.LastName;
                    user.Password = item.Password;
                    user.Status = item.Status;
                    user.Username = item.Email;
                    user.CreatedById = item.CreatedById;
                    user.CreatedDate = item.CreatedDate;
                    user.UserType = item.UserType;
                    user.AspNetUserId = newUser.Id;
                    user.EmployeeId = item.EmployeeId;

                    await _dbContext.UserDetails.AddAsync(user);
                    await _dbContext.SaveChangesAsync();

                    List<UserDetailOffices> lst = new List<UserDetailOffices>();

                    foreach (var item1 in item.OfficeId)
                    {
                        UserDetailOffices obj = new UserDetailOffices();
                        obj.OfficeId = item1.Value;
                        obj.UserId = user.UserID;
                        obj.CreatedById = item.CreatedById;
                        obj.CreatedDate = item.CreatedDate;
                        obj.IsDeleted = false;
                        lst.Add(obj);
                    }

                    await _dbContext.UserDetailOffices.AddRangeAsync(lst);
                    await _dbContext.SaveChangesAsync();
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;

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