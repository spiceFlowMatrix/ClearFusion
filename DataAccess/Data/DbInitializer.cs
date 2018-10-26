using DataAccess.DbEntities;
using HumanitarianAssistance.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Data.SqlClient;
using DataAccess.DbEntities.Store;

namespace DataAccess.Data
{
    public class DbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public static async System.Threading.Tasks.Task Initialize(ApplicationDbContext context, UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            try
            {
                // Look for any users.
                if (context.Users.Any())
                {
                    return; // DB has been seeded
                }

                await CreateDefaultUserAndRoleForApplication(userManager, roleManager, context, logger);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private static async Task CreateDefaultUserAndRoleForApplication(UserManager<AppUser> um, RoleManager<IdentityRole> rm, ApplicationDbContext context, ILogger<DbInitializer> logger)
        {
            const string administratorRole = "SuperAdmin";
            const string email = "hamza@yopmail.com";

            await CreateDefaultAdministratorRole(rm, administratorRole, logger);
            var user = await CreateDefaultUser(um, context, email, logger);
            //await SetPasswordForDefaultUser(um, user);
            await AddDefaultRoleToDefaultUser(um, email, administratorRole, user, logger);

            var s = AddEmployeeType(context);
            var s2 = AddVoucherType(context);
            var s3 = AddCountry(context);
            var s4 = AddEmailType(context);
            var s5 = AddDefaultPermission(context);
            var s6 = AddEmployeeContractType(context);
            var s7 = AddAccountType(context);
            var s8 = AddStatusAtTimeOfIssue(context);
            var s9 = AddReceiptType(context);
            var s10 = AddCurrency(context);
            var s11 = AddDefaultRoles(context, rm);
            var s12 = CreateDefaultEmployeePayrollHead(context);
            var s13 = CreateDefaultAccountLevel(context);


            await s;
            await s2;
            await s3;
            await s4;
            await s5;
            await s6;
            await s7;
            await s8;
            await s9;
            await s10;
            await s11;
            await s12;
            await s13;
            await AddProvinceDetails(context);
        }

        private static async Task CreateDefaultAdministratorRole(RoleManager<IdentityRole> rm, string administratorRole, ILogger<DbInitializer> logger)
        {
            logger.LogInformation($"Create the role `{administratorRole}` for application");
            var ir = await rm.CreateAsync(new IdentityRole(administratorRole));
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created the role `{administratorRole}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default role `{administratorRole}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task CreateDefaultOffice(ApplicationDbContext context)
        {
            try
            {
                OfficeDetail office = new OfficeDetail();
                office.OfficeCode = "A0001";
                office.OfficeKey = "AF";
                office.OfficeName = "Afghanistan";
                await context.OfficeDetail.AddAsync(office);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormesage = ex.Message;
            }
        }

        private static async Task CreateDefaultDepartment(ApplicationDbContext context)
        {
            try
            {
                Department department = new Department();
                department.DepartmentName = "Administration";
                department.OfficeId = 1;
                await context.Department.AddAsync(department);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private static async Task<AppUser> CreateDefaultUser(UserManager<AppUser> um, ApplicationDbContext context, string email, ILogger<DbInitializer> logger)
        {
            logger.LogInformation($"Create default user with email `{email}` for application");
            await CreateDefaultOffice(context);
            await CreateDefaultDepartment(context);

            var newuser = new AppUser { UserName = email, FirstName = "Hamza", LastName = "Rahimy", Email = email, PhoneNumber = "5365425698" };
            var ir = await um.CreateAsync(newuser, "aA123456!");
            await um.AddClaimAsync(newuser, new Claim("Roles", "SuperAdmin"));
            await um.AddClaimAsync(newuser, new Claim("Roles", "Admin"));
            await um.AddClaimAsync(newuser, new Claim("Permission", "dashboardhome"));
            await um.AddClaimAsync(newuser, new Claim("Permission", "home"));
            await um.AddClaimAsync(newuser, new Claim("Permission", "registercompany"));
            await um.AddClaimAsync(newuser, new Claim("Permission", "registercompanycontact"));
            await um.AddClaimAsync(newuser, new Claim("OfficeId", "1"));
            await um.AddClaimAsync(newuser, new Claim("DepartmentId", "1"));

            UserDetails userDetails = new UserDetails();
            userDetails.AspNetUserId = newuser.Id;
            userDetails.FirstName = "Hamza";
            userDetails.LastName = "Rahimy";
            userDetails.Username = email;
            userDetails.Password = "aA123456!";
            userDetails.OfficeId = 1;
            userDetails.DepartmentId = 1;
            userDetails.Status = 1;
            userDetails.UserType = 1;
            await context.UserDetails.AddAsync(userDetails);
            context.SaveChanges();
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default user `{email}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }

            var createdUser = await um.FindByEmailAsync(email);
            return createdUser;
        }

        private static async Task SetPasswordForDefaultUser(UserManager<AppUser> um, AppUser user, string email, ILogger<DbInitializer> logger)
        {
            logger.LogInformation($"Set password for default user `{email}`");
            const string password = "aA123456!!";
            var ir = await um.AddPasswordAsync(user, password);
            if (ir.Succeeded)
            {
                logger.LogTrace($"Set password `{password}` for default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Password for the user cannot be set");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task AddDefaultRoleToDefaultUser(UserManager<AppUser> um, string email, string administratorRole, AppUser user, ILogger<DbInitializer> logger)
        {
            logger.LogInformation($"Add default user `{email}` to role '{administratorRole}'");
            var ir = await um.AddToRoleAsync(user, administratorRole);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Added the role '{administratorRole}' to default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"The role `{administratorRole}` cannot be set for the user `{email}`");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task AddCountry(ApplicationDbContext context)
        {
            try
            {
                List<CountryDetails> countrylist = new List<CountryDetails>
                {
                    new CountryDetails { IsDeleted= false, CountryId = 1, CountryName = "Afghanistan"},
                    new CountryDetails { IsDeleted= false, CountryId = 2, CountryName = "United States"}
                };

                await context.CountryDetails.AddRangeAsync(countrylist);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddProvinceDetails(ApplicationDbContext context)
        {
            try
            {
                List<ProvinceDetails> list = new List<ProvinceDetails>
                {
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Badghis"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Baghlan"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Balkh"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Bamyan"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Daykundi"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Farah"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Faryab"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Ghazni"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Ghor"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Helmand"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Herat"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Jowzjan"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Kabul"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Kandahar"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Kapisa"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Khost"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Kunar"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Kunduz"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Laghman"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Logar"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Maidan Wardak"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Nangarhar"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Nimruz"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Nuristan"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Paktia"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Paktika"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Panjshir"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Parwan"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Samangan"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Sar-e Pol"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Takhar"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Urozgan"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Zabul"},
                    new ProvinceDetails { IsDeleted= false, CountryId=1,ProvinceName="Alabama"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Alaska"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Arizona"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Arkansas"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="California"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Colorado"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Connecticut"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Delaware"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Florida"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Georgia"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Hawaii"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Idaho"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Illinois"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Indiana"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Iowa"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Kansas"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Kentucky"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Louisiana"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Maine"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Maryland"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Massachusetts"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Michigan"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Minnesota"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Mississippi"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Missouri"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Montana"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Nebraska"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Nevada"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="New Hampshire"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="New Jersey"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="New Mexico"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="New York"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="North Carolina"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="North Dakota"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Ohio"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Oklahoma"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Oregon"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Pennsylvania"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Rhode Island"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="South Carolina"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="South Dakota"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Tennessee"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Texas"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Utah"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Vermont"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Virginia"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Washington"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="West Virginia"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Wisconsin"},
                    new ProvinceDetails { IsDeleted= false, CountryId=2,ProvinceName="Wyoming"}
                };
                await context.ProvinceDetails.AddRangeAsync(list);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddEmployeeType(ApplicationDbContext context)
        {
            try
            {
                List<EmployeeType> employeetypelist = new List<EmployeeType>
                {
                    new EmployeeType { EmployeeTypeId = 1, EmployeeTypeName= "Prospective", IsDeleted = false},
                    new EmployeeType { EmployeeTypeId = 2, EmployeeTypeName= "Active", IsDeleted = false},
                    new EmployeeType { EmployeeTypeId = 3, EmployeeTypeName= "Terminated", IsDeleted = false}
                };
                await context.EmployeeType.AddRangeAsync(employeetypelist);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddEmployeeContractType(ApplicationDbContext context)
        {
            try
            {
                List<EmployeeContractType> employeetypelist = new List<EmployeeContractType>
                {
                    new EmployeeContractType {  EmployeeContractTypeId = 1, EmployeeContractTypeName= "Probationary"},
                    new EmployeeContractType { EmployeeContractTypeId = 2, EmployeeContractTypeName= "PartTime"},
                    new EmployeeContractType { EmployeeContractTypeId = 3, EmployeeContractTypeName= "Permanent"}
                };
                await context.EmployeeContractType.AddRangeAsync(employeetypelist);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddAccountType(ApplicationDbContext context)
        {
            try
            {
                List<AccountType> employeetypelist = new List<AccountType>
                {
                    new AccountType { AccountTypeId = 1, AccountTypeName= "Capital Assets Written Off", AccountCategory = 1},
                    new AccountType { AccountTypeId = 2, AccountTypeName= "Current Assets", AccountCategory = 1},
                    new AccountType { AccountTypeId = 3, AccountTypeName= "Funds", AccountCategory = 1},
                    new AccountType { AccountTypeId = 4, AccountTypeName= "Endownment Fund", AccountCategory = 1},
                    new AccountType { AccountTypeId = 5, AccountTypeName= "Reserve Account Adjustment", AccountCategory = 1},
                    new AccountType { AccountTypeId = 6, AccountTypeName= "Long term Liability", AccountCategory = 1},
                    new AccountType { AccountTypeId = 7, AccountTypeName= "Current Liability", AccountCategory = 1},
                    new AccountType { AccountTypeId = 8, AccountTypeName= "Reserve Account", AccountCategory = 1},
                    new AccountType { AccountTypeId = 9, AccountTypeName= "Income From Donor", AccountCategory = 2},
                    new AccountType { AccountTypeId = 10, AccountTypeName= "Income From Projects", AccountCategory = 2},
                    new AccountType { AccountTypeId = 11, AccountTypeName= "Profit On Bank Deposits", AccountCategory = 2},
                    new AccountType { AccountTypeId = 12, AccountTypeName= "Income Expenditure Fund", AccountCategory = 2}
                };
                await context.AccountType.AddRangeAsync(employeetypelist);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddVoucherType(ApplicationDbContext context)
        {
            try
            {
                List<VoucherType> vouchertypelist = new List<VoucherType>
                {
                    new VoucherType { VoucherTypeId = 1, VoucherTypeName ="Adjustment"},
                    new VoucherType { VoucherTypeId = 2, VoucherTypeName ="Journal"}
                };
                await context.VoucherType.AddRangeAsync(vouchertypelist);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddEmailType(ApplicationDbContext context)
        {
            try
            {
                List<EmailType> emailtypelist = new List<EmailType>
                {
                    new EmailType { IsDeleted= false,  EmailTypeName = "General"},
                    new EmailType { IsDeleted= false, EmailTypeName = "Bidding Panel"}
                };

                await context.EmailType.AddRangeAsync(emailtypelist);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddDefaultPermission(ApplicationDbContext context)
        {
            try
            {
                List<Permissions> list = new List<Permissions>
                {
                    new Permissions { IsDeleted= false, Name = "CanAdd"},
                    new Permissions { IsDeleted= false, Name = "CanEdit"},
                    new Permissions { IsDeleted= false, Name = "CanView"},
                    new Permissions { IsDeleted= false, Name = "CanRead"},
                    new Permissions { IsDeleted= false, Name = "CanDelete"}
                };
                await context.Permissions.AddRangeAsync(list);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddDefaultRoles(ApplicationDbContext context, RoleManager<IdentityRole> rm)
        {
            try
            {
                List<string> rolesList = new List<string>() { "Administrator", "Accounting Manager", "HR Manager", "Project Manager" };
                foreach (var items in rolesList)
                {
                    var roleExists = await rm.FindByNameAsync(items);
                    if (roleExists == null)
                    {
                        var role = new IdentityRole();
                        role.Name = items;
                        await rm.CreateAsync(role);
                    }
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }



        private static async Task AddStatusAtTimeOfIssue(ApplicationDbContext context)
        {
            try
            {
                List<StatusAtTimeOfIssue> statusAtTimeOfIssuelist = new List<StatusAtTimeOfIssue>
                {
                    new StatusAtTimeOfIssue { IsDeleted= false, StatusAtTimeOfIssueId = 1, StatusName = "New"},
                    new StatusAtTimeOfIssue { IsDeleted= false, StatusAtTimeOfIssueId = 2, StatusName = "Useable"},
                    new StatusAtTimeOfIssue { IsDeleted= false, StatusAtTimeOfIssueId = 3, StatusName = "To Repair"},
                    new StatusAtTimeOfIssue { IsDeleted= false, StatusAtTimeOfIssueId = 4, StatusName = "Damage"},
                    new StatusAtTimeOfIssue { IsDeleted= false, StatusAtTimeOfIssueId = 5, StatusName = "Sold"},
                    new StatusAtTimeOfIssue { IsDeleted= false, StatusAtTimeOfIssueId = 6, StatusName = "Stolen"},
                    new StatusAtTimeOfIssue { IsDeleted= false, StatusAtTimeOfIssueId = 7, StatusName = "Handover"},
                    new StatusAtTimeOfIssue { IsDeleted= false, StatusAtTimeOfIssueId = 8, StatusName = "Demolished"},
                    new StatusAtTimeOfIssue { IsDeleted= false, StatusAtTimeOfIssueId = 9, StatusName = "Broken"},
                };

                await context.StatusAtTimeOfIssue.AddRangeAsync(statusAtTimeOfIssuelist);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        private static async Task AddReceiptType(ApplicationDbContext context)
        {
            try
            {
                List<ReceiptType> receiptTypeList = new List<ReceiptType>
                {
                    new ReceiptType { IsDeleted= false, ReceiptTypeId = 1, ReceiptTypeName = "Purchased"},
                    new ReceiptType { IsDeleted= false, ReceiptTypeId = 2, ReceiptTypeName = "Transfers"},
                    new ReceiptType { IsDeleted= false, ReceiptTypeId = 3, ReceiptTypeName = "Donation"},
                    new ReceiptType { IsDeleted= false, ReceiptTypeId = 4, ReceiptTypeName = "Take Over"},
                    new ReceiptType { IsDeleted= false, ReceiptTypeId = 5, ReceiptTypeName = "Loan"},
                    new ReceiptType { IsDeleted= false, ReceiptTypeId = 6, ReceiptTypeName = "Return"},
                    new ReceiptType { IsDeleted= false, ReceiptTypeId = 7, ReceiptTypeName = "Other"}
                };

                await context.ReceiptType.AddRangeAsync(receiptTypeList);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        private static async Task AddCurrency(ApplicationDbContext context)
        {
            try
            {
                List<CurrencyDetails> currencyDetailList = new List<CurrencyDetails>
                {
                    new CurrencyDetails { CurrencyId = 1, CurrencyName = "Afghanistan", CurrencyCode ="AFG", IsDeleted= false , Status =  false, SalaryTaxFlag= true},
                    new CurrencyDetails { CurrencyId = 2, CurrencyName = "European Curency", CurrencyCode ="EUR", IsDeleted= false , Status =  false, SalaryTaxFlag= false},
                    new CurrencyDetails { CurrencyId = 3, CurrencyName = "Pakistani Rupees", CurrencyCode ="PKR", IsDeleted= false , Status =  true, SalaryTaxFlag= false}, //base currency :  Status = true
                    new CurrencyDetails { CurrencyId = 4, CurrencyName = "US Dollars", CurrencyCode ="USD", IsDeleted= false , Status =  false, SalaryTaxFlag= false}
                };

                await context.CurrencyDetails.AddRangeAsync(currencyDetailList);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        private static async Task CreateDefaultEmployeePayrollHead(ApplicationDbContext context)
        {
            try
            {
                List<PayrollAccountHead> payrollAccountHeadList = new List<PayrollAccountHead>
                {
                    new PayrollAccountHead{AccountNo=null, IsDeleted= false, Description=null, PayrollHeadName="Net Salary", PayrollHeadTypeId=3, TransactionTypeId= 1},
                    new PayrollAccountHead{AccountNo=null, IsDeleted= false, Description=null, PayrollHeadName="Advance Deduction", PayrollHeadTypeId=3, TransactionTypeId= 2},
                    new PayrollAccountHead{AccountNo=null, IsDeleted= false, Description=null, PayrollHeadName="Salary Tax", PayrollHeadTypeId=3, TransactionTypeId= 2},
                    new PayrollAccountHead{AccountNo=null, IsDeleted= false, Description=null, PayrollHeadName="Gross Salary", PayrollHeadTypeId=3, TransactionTypeId= 2},
                    new PayrollAccountHead{AccountNo=null, IsDeleted= false, Description=null, PayrollHeadName="Pension", PayrollHeadTypeId=3, TransactionTypeId= 2}
                };
                await context.PayrollAccountHead.AddRangeAsync(payrollAccountHeadList);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormesage = ex.Message;
            }
        }

        private static async Task CreateDefaultAccountLevel(ApplicationDbContext context)
        {
            try
            {
                List<AccountLevel> AccountLevelList = new List<AccountLevel>
                {
                    new AccountLevel{AccountLevelName="Main Level Accounts"},
                    new AccountLevel{AccountLevelName="Control Level Accounts"},
                    new AccountLevel{AccountLevelName="Sub Level Accounts"},
                    new AccountLevel{AccountLevelName="Input Level Accounts"}

                };

                await context.AccountLevel.AddRangeAsync(AccountLevelList);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormesage = ex.Message;
            }
        }


        //private static async Task AddAspNetRoles(ApplicationDbContext context)
        //{
        //    try
        //    {
        //        List<AspNetRoles> aspNetRolesList = new List<AspNetRoles>
        //        {
        //            new AspNetRoles { CurrencyId = 1, CurrencyName = "Afghanistan", CurrencyCode ="AFG", IsDeleted= false , Status =  false, SalaryTaxFlag= true},
        //            new CurrencyDetails { CurrencyId = 2, CurrencyName = "European Curency", CurrencyCode ="EUR", IsDeleted= false , Status =  false, SalaryTaxFlag= false},
        //            new CurrencyDetails { CurrencyId = 3, CurrencyName = "Pakistani Rupees", CurrencyCode ="PKR", IsDeleted= false , Status =  true, SalaryTaxFlag= false}, //base currency :  Status = true
        //            new CurrencyDetails { CurrencyId = 4, CurrencyName = "US Dollars", CurrencyCode ="USD", IsDeleted= false , Status =  false, SalaryTaxFlag= false}
        //        };

        //        await context.CurrencyDetails.AddRangeAsync(currencyDetailList);
        //        context.SaveChanges();

        //    }
        //    catch (Exception ex)
        //    {
        //        var errormessage = ex.Message;
        //    }
        //}



        private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        {
            string errors = null;
            foreach (var identityError in ir.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }


    }
}
