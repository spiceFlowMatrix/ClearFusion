using DataAccess.DbEntities;
using HumanitarianAssistance.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

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

        public static async Task Initialize(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            try
            {

                //if (!context.Database.EnsureCreated()) //update-database
                //    context.Database.Migrate();

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

        public static async Task CreateDefaultUserAndRoleForApplication(UserManager<AppUser> um, RoleManager<IdentityRole> rm, ApplicationDbContext context, ILogger<DbInitializer> logger)
        {
            const string administratorRole = "SuperAdmin";
            const string email = "hamza@yopmail.com";

            await CreateDefaultAdministratorRole(rm, administratorRole, logger);
            var user = await CreateDefaultUser(um, context, email, logger);

            await AddDefaultRoleToDefaultUser(um, email, administratorRole, user, logger);
            await AddDefaultRoles(context, rm);
            await AddDefaultPermission(context);
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

        private static async Task<AppUser> CreateDefaultUser(UserManager<AppUser> um, ApplicationDbContext context, string email, ILogger<DbInitializer> logger)
        {
            logger.LogInformation($"Create default user with email `{email}` for application");
            //await CreateDefaultOffice(context);
            //await CreateDefaultDepartment(context);

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


        public static async Task AddDefaultPermission(ApplicationDbContext context)
        {
            try
            {
                List<Permissions> list = new List<Permissions>
                {
                    new Permissions { IsDeleted= false, Name = "CanAdd"},
                    new Permissions { IsDeleted= false, Name = "CanEdit"},
                    new Permissions { IsDeleted= false, Name = "CanView"},
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

    }
}
