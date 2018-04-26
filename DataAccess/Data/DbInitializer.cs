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

                await CreateDefaultUserAndRoleForApplication(userManager, roleManager, context,logger);                
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        private static async Task CreateDefaultUserAndRoleForApplication(UserManager<AppUser> um, RoleManager<IdentityRole> rm, ApplicationDbContext context, ILogger<DbInitializer> logger)
        {
            const string administratorRole = "Administrator";
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
            await s;
            await s2;
            await s3;
            await s4;
            await s5;
			await s6;
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
                    new CountryDetails { CountryId = 1, CountryName = "Afghanistan"},
                    new CountryDetails { CountryId = 2, CountryName = "United States"}
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
                    new ProvinceDetails { CountryId=1,ProvinceName="Badghis"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Baghlan"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Balkh"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Bamyan"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Daykundi"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Farah"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Faryab"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Ghazni"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Ghor"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Helmand"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Herat"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Jowzjan"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Kabul"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Kandahar"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Kapisa"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Khost"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Kunar"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Kunduz"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Laghman"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Logar"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Maidan Wardak"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Nangarhar"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Nimruz"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Nuristan"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Paktia"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Paktika"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Panjshir"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Parwan"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Samangan"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Sar-e Pol"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Takhar"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Urozgan"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Zabul"},
                    new ProvinceDetails { CountryId=1,ProvinceName="Alabama"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Alaska"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Arizona"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Arkansas"},
                    new ProvinceDetails { CountryId=2,ProvinceName="California"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Colorado"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Connecticut"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Delaware"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Florida"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Georgia"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Hawaii"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Idaho"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Illinois"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Indiana"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Iowa"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Kansas"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Kentucky"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Louisiana"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Maine"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Maryland"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Massachusetts"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Michigan"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Minnesota"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Mississippi"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Missouri"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Montana"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Nebraska"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Nevada"},
                    new ProvinceDetails { CountryId=2,ProvinceName="New Hampshire"},
                    new ProvinceDetails { CountryId=2,ProvinceName="New Jersey"},
                    new ProvinceDetails { CountryId=2,ProvinceName="New Mexico"},
                    new ProvinceDetails { CountryId=2,ProvinceName="New York"},
                    new ProvinceDetails { CountryId=2,ProvinceName="North Carolina"},
                    new ProvinceDetails { CountryId=2,ProvinceName="North Dakota"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Ohio"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Oklahoma"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Oregon"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Pennsylvania"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Rhode Island"},
                    new ProvinceDetails { CountryId=2,ProvinceName="South Carolina"},
                    new ProvinceDetails { CountryId=2,ProvinceName="South Dakota"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Tennessee"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Texas"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Utah"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Vermont"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Virginia"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Washington"},
                    new ProvinceDetails { CountryId=2,ProvinceName="West Virginia"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Wisconsin"},
                    new ProvinceDetails { CountryId=2,ProvinceName="Wyoming"}
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
                    new EmployeeType { EmployeeTypeId = 1, EmployeeTypeName= "Prospective"},
                    new EmployeeType { EmployeeTypeId = 2, EmployeeTypeName= "Active"},
                    new EmployeeType { EmployeeTypeId = 3, EmployeeTypeName= "Terminated"}
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
					new EmployeeContractType { EmployeeContractTypeId = 1, EmployeeContractTypeName= "Probationary"},
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
                    new EmailType { EmailTypeName = "General"},
                    new EmailType { EmailTypeName = "Bidding Panel"}
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
                    new Permissions { Name = "CanAdd"},
                    new Permissions { Name = "CanEdit"},
                    new Permissions { Name = "CanView"},
                    new Permissions { Name = "CanRead"},
                    new Permissions { Name = "CanDelete"}
                };
                await context.Permissions.AddRangeAsync(list);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

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
