using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
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
        public HRService(HumanitarianAssistanceDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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

                        await _dbContext.UserDetails.AddAsync(user);
                        await _dbContext.SaveChangesAsync();

                        List<UserDetailOffices> lst = new List<UserDetailOffices>();

                        foreach (var item in request.OfficeId)
                        {
                            UserDetailOffices obj = new UserDetailOffices();
                            obj.OfficeId = item;
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
                        response.Message = StaticResource.UserAlreadyExist;
                    }
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