using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Domain.Entities.Store;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Persistence
{
    public class HumanitarianAssistanceInitializer
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HumanitarianAssistanceInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public static async Task Initialize(HumanitarianAssistanceDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HumanitarianAssistanceInitializer> logger)
        {
            await SeedEverything(context, userManager, roleManager, logger);
        }

        public static async Task SeedEverything(
                        HumanitarianAssistanceDbContext context,
                        UserManager<AppUser> userManager,
                        RoleManager<IdentityRole> roleManager,
                        ILogger<HumanitarianAssistanceInitializer> logger
                        )
        {

            context.Database.EnsureCreated();

            // check if default user is present or not
            if (!context.Users.Any())
            {
                await CreateDefaultUserAndRoleForApplication(context, userManager, roleManager, logger);
            }

            if (!context.EmployeeContractType.Any())
            {
                await AddEmployeeContractType(context);
            }

            // Caution : Make sure EmployeeContractType details are filled before EmployeeContractType.
            if (!context.ContractTypeContent.Any())
            {
                await AddContractClauses(context);
            }

            // check if JobGrade present or not
            if (!context.JobGrade.Any())
            {
                await AddJobGrades(context);
            }

            // check if Categories present or not
            if (!context.Categories.Any())
            {
                await AddMarketingCategory(context);
            }

            if (!context.ActivityStatusDetail.Any())
            {
                await AddActivityStatus(context);
            }

            if (!context.CurrencyDetails.Any())
            {
                await AddCurrencyDetails(context);
            }

            if (!context.AccountLevel.Any())
            {
                await AddAccountLevel(context);
            }

            if (!context.PayrollAccountHead.Any())
            {
                await AddPayrollAccountHead(context);
            }

            if (!context.OfficeDetail.Any())
            {
                await AddOfficeDetail(context);
            }

            if (!context.Department.Any())
            {
                await AddDepartment(context);
            }

            if (!context.SalaryHeadDetails.Any())
            {
                await AddSalaryHeadDetails(context);
            }

            if (!context.LanguageDetail.Any())
            {
                await AddLanguageDetail(context);
            }

            if (!context.LeaveReasonDetail.Any())
            {
                await AddLeaveReasonDetail(context);
            }

            if (!context.ActivityTypes.Any())
            {
                await AddActivityType(context);
            }

            if (!context.FinancialYearDetail.Any())
            {
                await AddFinancialYearDetail(context);
            }

            // NOTE: Country detail should be add after Province detail is filled 
            if (!context.CountryDetails.Any())
            {
                await AddCountryDetails(context);
            }

            // NOTE: Province detail should be add before District detail is filled 
            if (!context.ProvinceDetails.Any())
            {
                await AddProvinceDetails(context);
            }

            // NOTE: District detail should be add before Province detail is filled 
            if (!context.DistrictDetail.Any())
            {
                await AddDistrictDetail(context);
            }

            if (!context.ReceiptType.Any())
            {
                await AddReceiptType(context);
            }

            if (!context.StatusAtTimeOfIssue.Any())
            {
                await AddStatusAtTimeOfIssue(context);
            }

            if (!context.AccountHeadType.Any())
            {
                await AddAccountHeadType(context);
            }

            if (!context.EmailType.Any())
            {
                await AddEmailType(context);
            }

            if (!context.VoucherType.Any())
            {
                await AddVoucherType(context);
            }


            if (!context.EmployeeType.Any())
            {
                await AddEmployeeType(context);
            }


            if (!context.StrengthConsiderationDetail.Any())
            {
                await AddStrengthConsiderationDetail(context);
            }

            if (!context.GenderConsiderationDetail.Any())
            {
                await AddGenderConsiderationDetail(context);
            }

            if (!context.SecurityDetail.Any())
            {
                await AddSecurityDetail(context);
            }

            if (!context.SecurityConsiderationDetail.Any())
            {
                await AddSecurityConsiderationDetail(context);
            }

            if (!context.CodeType.Any())
            {
                await AddCodeType(context);
            }

            if (!context.AccountFilterType.Any())
            {
                await AddAccountFilterType(context);
            }

            if (!context.ProjectPhaseDetails.Any())
            {
                await AddProjectPhaseDetails(context);
            }

            if (!context.StoreInventories.Any())
            {
                await AddStoreInventorySeedData(context);
            }

            if (!context.StoreItemGroups.Any())
            {
                await AddStoreItemGroupSeedData(context);
            }

             if (!context.InventoryItems.Any())
            {
                await AddStoreItemSeedData(context);
            }


        }
        private static async Task CreateDefaultUserAndRoleForApplication(
             HumanitarianAssistanceDbContext context,
             UserManager<AppUser> userManager,
             RoleManager<IdentityRole> roleManager,
             ILogger<HumanitarianAssistanceInitializer> logger
            )
        {
            const string administratorRole = "SuperAdmin";
            const string email = "hamza@yopmail.com";

            await CreateDefaultAdministratorRole(roleManager, administratorRole, logger);
            AppUser user = await CreateDefaultUser(userManager, context, email, logger);
            await AddDefaultRoleToDefaultUser(userManager, email, administratorRole, user, logger);
            await AddDefaultRoles(context, roleManager);
            await AddDefaultPermission(context);
        }

        private static async Task CreateDefaultAdministratorRole(RoleManager<IdentityRole> rm, string administratorRole, ILogger<HumanitarianAssistanceInitializer> logger)
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

        private static async Task<AppUser> CreateDefaultUser(UserManager<AppUser> um, HumanitarianAssistanceDbContext context, string email, ILogger<HumanitarianAssistanceInitializer> logger)
        {
            logger.LogInformation($"Create default user with email `{email}` for application");

            var newuser = new AppUser
            {
                UserName = email,
                FirstName = "Hamza",
                LastName = "Rahimy",
                Email = email,
                PhoneNumber = "5365425698"
            };

            var ir = await um.CreateAsync(newuser, "aA123456!");

            await um.AddClaimAsync(newuser, new Claim("Roles", "SuperAdmin"));
            await um.AddClaimAsync(newuser, new Claim("Roles", "Admin"));
            await um.AddClaimAsync(newuser, new Claim("Permission", "dashboardhome"));
            await um.AddClaimAsync(newuser, new Claim("Permission", "home"));
            await um.AddClaimAsync(newuser, new Claim("Permission", "registercompany"));
            await um.AddClaimAsync(newuser, new Claim("Permission", "registercompanycontact"));
            await um.AddClaimAsync(newuser, new Claim("OfficeId", "1"));
            await um.AddClaimAsync(newuser, new Claim("DepartmentId", "1"));

            UserDetails userDetails = new UserDetails
            {
                AspNetUserId = newuser.Id,
                FirstName = "Hamza",
                LastName = "Rahimy",
                Username = email,
                Password = "aA123456!",
                OfficeId = 1,
                DepartmentId = 1,
                Status = 1,
                UserType = 1,
                CreatedDate = DateTime.UtcNow
            };

            await context.UserDetails.AddAsync(userDetails);
            await context.SaveChangesAsync();

            UserDetailOffices userDetailOffices = new UserDetailOffices
            {
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                OfficeId = 1,
                UserId = userDetails.UserID
            };

            await context.UserDetailOffices.AddAsync(userDetailOffices);
            await context.SaveChangesAsync();

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

        private static async Task SetPasswordForDefaultUser(UserManager<AppUser> um, AppUser user, string email, ILogger<HumanitarianAssistanceInitializer> logger)
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

        private static async Task AddDefaultRoleToDefaultUser(UserManager<AppUser> um, string email, string administratorRole, AppUser user, ILogger<HumanitarianAssistanceInitializer> logger)
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

        public static async Task AddDefaultRoles(HumanitarianAssistanceDbContext context, RoleManager<IdentityRole> rm)
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
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        public static async Task AddDefaultPermission(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<Permissions> list = new List<Permissions>
                {
                    new Permissions { IsDeleted = false, CreatedDate = DateTime.UtcNow, Name = "CanAdd"},
                    new Permissions { IsDeleted = false, CreatedDate = DateTime.UtcNow, Name = "CanEdit"},
                    new Permissions { IsDeleted = false, CreatedDate = DateTime.UtcNow, Name = "CanView"},
                    new Permissions { IsDeleted = false, CreatedDate = DateTime.UtcNow, Name = "CanDelete"}
                };
                await context.Permissions.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task AddContractClauses(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<ContractTypeContent> list = new List<ContractTypeContent>
                {
                    new ContractTypeContent { IsDeleted= false, ContentEnglish="<p style='text-align: left;'><strong><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>By signing of this contract the employee hereby agrees to:</span></strong></p><ul style='list-style-type: disc;'><li><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>Fulfill the requirement detailed in the rule description.</span></li><li><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>Adhere to CHA&rsquo;s procedure and policies</span></li><li><span style='font-size: 12pt; font-family: \"Times New Roman\", serif'>Conduct him/her self in ethical and honest way </span></li></ul><p style='text-align: justify;'><strong><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>Termination/ Extension of contract:</span></strong></p><p style = 'text-align: justify;'><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> This agreement can be terminated by either party with or without reason by submitting 30 days written notice, in special cases like termination of contact by donors, organization can terminate employees contact without any notice.</span></p><p style = 'text-align: justify;'><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> The contract may be extended in the case of project continuation and or the further availability of fund.</span></p><ul style = 'list-style-type: disc;'><li><span style = 'font-size: 12pt; font-family: Calibri;'> This contract can be terminated by the organization any time with a one month prior notice, in cases like contract termination by donors, the organization can terminate this contract any time without prior notice.</span></li><li><span style = 'font-size: 12pt; font-family: Calibri;'> In case project extends, contract with project staff will be extended, but this will be done if there is need for doing so, otherwise contract extension is not must.</span></li><li><span style = 'font-size: 12pt; font-family: Calibri;'> At the end of contract the organization is obliged to pay one month salary to the employee </span></li><li><span style = 'font-size: 12pt; font-family: Calibri;'> Employees cannot take any type of loans under the name of CHA from any agency.</span></li><li><span style = 'font-size: 12pt; font-family: Calibri;'> On time payment of staff salaries depend on receipt of money from donors, and in case of delays, CHA cannot be taken responsible </span></li><li><span style = 'font-size: 12pt; font-family: Calibri;'> Besides the assigned tasks the organization can assign the employee to other official tasks or transfer him / her to other locations.</span></li><li><span style = 'font-size: 12pt; font-family: Calibri;'> The employee has to be present to duty location for the defined time period.</span></li></ul><p style = 'text-align: justify;'><strong><span style = 'font-family: Calibri;'> The employee gives commitment toward the policy and procedure to:</span></strong></p><ul style = 'list-style-type: disc;'><li><span style = 'font-family: Calibri;'> To pay 4.5 % from monthly salary to the pension account according to the rules and procedure of the organization </span></li><li><span style = 'font-family: Calibri;'> If leaves job in a wrong way, can take their money from this account by paying one month salary to the organization(CHA) </span></li><li><span style = 'font-family: Calibri;'> Does not try to withdraw this amount while working in the organization.</span></li><li><span style = 'font-family: Calibri;'> Responsibility for safety measures against unexpected incidents such as traffic accidents, murder, theft, abduction, etc.during the performance of their duties, are related to the employee, and the CHA has no responsibility for it </span></li></ul> <p style = 'text-align: justify;'><strong><span style = 'font-size: 12pt; font-family: Calibri;'> Considering religious and moral principles, the employee promises to:</span></strong></p><ul style = 'list-style-type: disc;'><li><span style = 'font-size: 12pt; font-family: Calibri;'> Perform tasks honestly, with integrity, neutrality, without any party, tribe, religion, gender, language and region related discriminations, protect organization & rsquo; s properties and belongings and does not prefer self - interests over organization & rsquo; s interests.</span></li><li><span style = 'font-size: 12pt; font-family: Calibri;'> Make efforts for attaining defined goals and objectives with all his / her abilities and strengths, use his / her strength and energy for the organization and in this way for sound development of Afghanistan.</span></li><li><span style = 'font-size: 12pt; font-family: Calibri;'> Accept all rules and regulations and considers them as a guidance.</span></li></ul><p style = 'text-align: justify;'><span style = 'font-size: 12pt; font-family: Calibri;'> These terms are acceptable and will be followed.</span></p><p> <strong> Contract singed by:</strong></p><p><strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> Name:</span></strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> </span></p><p><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> &nbsp;</span></p><p><strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> Signature:</span></strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> (&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;)</span></p> ", ContentDari="<p dir='RTL' style='text-align: justify;'><strong><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>با امضای این قرارداد کارمند موافقه مینماید که: </span></strong></p><ul style='list-style-type: disc;'><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>شرایط که در اصول وظایفش تشریح گردیده را اجرا نماید</span></li><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>به پالیسی و قوانین موسسه سی اچ ای پایبند باشد </span></li><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>در رفتار و اخلاقش صادق باشد </span></li></ul><p dir='RTL' style='text-align: justify;'><strong><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>انفکاک یا </span></strong><strong><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>تمدید قرارداد:&nbsp; </span></strong></p><p dir='RTL' style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>یکی از طرفین این قرارداد با ارایه اطلاع کتبی یک ماه قبل میتوانند اینقرارداد را فسخنمایند. در شرایط خاص مانند ختم قرارداد از طرف نهاد های تمویل کننده موسسهمیتواند کارمند را بدون اطلاع قبلی منفک نماید.</span></p><p dir='RTL' style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>در موجودیت امکانات مالی یا تمدید قرارداد با نهاد تمویل کننده قرارداد کارمند نیز تمدید میگردد. </span></p><ul style='list-style-type: disc;'><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>اين قرار داد از طرف سازمان با تذکري که يک ماه قبل داده ميشود ، ميتواند در هر زماني با کارمند فسخ گردد</span><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>، در حالات خاص مانند فسخ قرار داد توسط دونرها، اداره ميتواند قرار داد کارمند را بدون آگهي قبلي فسخ نمايد</span><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>.</span></li><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>صورت ادامه پروژه اگر قرارداد کارمند پروژوي باشد قرارداد دوبارهتمديد ميگردد ، آنهم نظربه ضرورت اداره وامر حتمى نيست كه قرارداد بايد تمديدشود. </span></li><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>درصورت ختم قرارداد اداره مكلف به پرداخت يك ماه معاش اضافى به كارمند نميباشد.</span></li><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>کارمندان صلاحيت گرفتن هيچ نوع قرضه را به نام </span><span dir='LTR' style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>CHA</span><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'> از هيچ مرجعي ندارد.</span></li><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>اجراي بروقت معاش کارمندان مربوط به رسيدن پول پروژه از جانب دونر ميباشد و در قسمت تاخير آن </span><span dir='LTR' style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>CHA</span><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'> هيچ نوع مسوليت ندارد.</span></li><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>وظايف ذكر شده اداره ميتواند شخص استخدام شونده را درصورت ضرورت به اجراء وظايف ديگردرحيطه فعاليت هاي رسمي سازمان توظيف يا به محل ديگرى تبديل نمايد.</span></li><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>شخص استخدام شونده مكلف به حضور در محل كار وايفاى وظيفه درطول وقت معينه ميباشد.</span></li></ul><p dir='RTL' style='text-align: justify;'><strong><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>کارمند در قبال پاليسي و پروسيجر تقاعد وسکیورتی تعهد مينمايد که:</span></strong></p><p dir = 'RTL' style = 'text-align: justify;'><strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> &nbsp;</span></strong></p><ul style = 'list-style-type: disc;'><li style = 'text-align: justify;'><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> کارمند مطابق پاليسي سازمان، متعهد به پرداخت ماهانه</span><span dir = 'LTR' style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> 4.5 %</span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> از معاش خويش، بحساب بانکيتقاعد ميباشد. </span></li><li style = 'text-align: justify;'><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'>صورتيکه کارمند وظيفه خويشرا مغاير اصول سازمان ترک نمايد، در آنصورت زماني مستحق حقوق تقاعد خويش ميگرددکه يکماه معاش خود را به دفتر</span><span dir = 'LTR' style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> CHA </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> باز پرداخت نمايد .</span></li><li style = 'text-align: justify;'><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> کارمند در مدت زمان برحالي در وظيفه ادعاي اخذ پول تقاعد خويش را از دفتر کرده نمي تواند </span><span dir = 'LTR' style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'></span></li><li style = 'text-align: justify;'><span dir = 'LTR' style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;' dir = 'LTR'>تدابیر ایمنی درمقابل حوادث غیر مترقبه از قبیل حوادث ترافیکی، قتل ،سرقت، اختطاف ، وغیره & nbsp; درحین اجرای وظیفه وبیرون از آن مربوط بخود کارمند بوده در قبال آن موسسه سی ایچ ای حیج نوع مسوولیت ندارد </span>.</span></li></ul><p dir = 'RTL' style = 'text-align: justify;'><strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> شخص استخدام شونده با درنظرداشت موازين دينى و اخلاقى تعهد مينمايد كه:</span></strong></p><p dir = 'RTL' style = 'text-align: justify;'><strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> &nbsp;</span></strong></p><ul style = 'list-style-type: disc;'><li style = 'text-align: justify;'><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> وظايف خودرا با كمال صداقت، امانت دارى ، بيطرفى كامل ومبرا از هر گونه تبعيض وتمايز گروه </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> ي </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'>، تنظيم </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> ي </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'>، قوم </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> ي </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> ، مذهب </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> ي </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'>، جنس </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> ي </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'>، لسان </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> ي </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> ومنطق </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> وي </span><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> انجام دهد ،اموال ودارايى مؤسسه را به نحواحسن مراقبت وحفاظت كند و منافع شخصى خود را بر منافع مؤسسه رجحان ندهد.</span></li><li style = 'text-align: justify;'><span style = 'font-size: 12pt; font-weight: normal;'> استخدام شونده تعهد مينمايد كه</span> <span style = 'font-size: 12pt; font-weight: normal;'> نيرو وتوان در راه حصول اهداف اساسى وبرنامه هاى پلان شدهء مؤسسه تلاش كند ، قدرت وانرژى فكرى خود را در راه خدمت به مؤسسه وازين طريقدرراستاى ترقى وانكشاف سالم و پاياى افغانستان عزيز مصرف نمايد.</span></li><li style = 'text-align: justify;'><span style = 'font-size: 12pt; font-weight: normal;'> تمام اصول و ضوابط اداره را قبول نموده وآنرا رهنماي کاري خويش قرار دهد </span></li></ul><p dir = 'RTL' style = 'text-align: justify;'><span style = 'font-size: 12pt; font-weight: normal;'> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span></p><p dir = 'RTL' style = 'text-align: justify;'><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> موارد فوق قابل قبول و توافق بوده كاملاً رعايت ميگردند.</span></p><p style = 'text-align: justify;'><strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> تایید شده توسط:</span></strong></p><p style = 'text-align: justify;'><strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> نام:</span></strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> </span></p><p style = 'text-align: justify;'><strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;'> امضا:</span></strong><span style = 'font-size: 12pt; font-family: \"Times New Roman\",serif;'(&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; )</span></p> ", EmployeeContractTypeId=3, OfficeId=1 },
                    new ContractTypeContent { IsDeleted= false, ContentEnglish="<p dir='ltr'><strong>Conditions: </strong></p><p dir='ltr'>This contract is for ( -&nbsp; ) hours (&nbsp;&nbsp; -&nbsp;&nbsp; &nbsp;) days (&nbsp;&nbsp;&nbsp; -&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ) weeks (&nbsp;&nbsp;&nbsp; 3&nbsp;&nbsp; &nbsp;&nbsp;) months, if the duration is more than 4 months, two months will be counted as probationary period, if the employee is unable to continue job, he/she will be notified one month before contract termination, but in probationary period the organization can terminate contract after giving a 3 days&rsquo; notice. If the employee does not complete one year of employment, the organization will not pay any other allowances except the salary for the days of work. After completing 45 days of employment your performance will be assessed. If you were proved to be a professional, good and follower of rules and regulations employee and if there was need for continuation of your employment, your contract will be extended.</p><p dir='ltr'><strong>Working Hours:</strong></p><p dir='ltr'>Your working hours in week, from Sunday to Thursday are 40 hours; your overtime working has value. During a day there is a one hour break for you.</p><p dir='ltr'>Work start time: 08:00 AM</p><p dir='ltr'>Work end time:&nbsp; (depends on yearly seasons)</p><p dir='ltr'>Service employees&rsquo; working hours are from Saturday to Friday&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p dir='ltr'><!--[endif]----></p><p dir='ltr'><strong>Leaves:</strong></p><p dir='ltr'>Employees are entitled to following leaves in a year:</p><p dir='ltr'>Annual leave: are 20 days, request for using this leave should be given one month prior to the related department, if the department agrees, then the leave will be applicable and will be counted according to rules and regulations.</p><p dir='ltr'>Emergency leave: it is 10 days in a year</p><p dir='ltr'>Sick leave: it is 20 days in a year. If sick leave is more than 3 days it needs approval from health center.</p><p dir='ltr'>Maternity leave: it 3 months in a year, one month before delivery and 2 month after delivery and up to two years there is one hour break for mothers to feed their children. An employee is eligible for maternity leave once in three years, and the employee is not eligible for other leaves on the year she takes maternity leave.</p><p dir='ltr'><strong>Part time employees are not eligible for these leaves.</strong></p><p dir='ltr'>All leave request will be given to admin department after filling the leave form, department approval and supervisor&rsquo;s signature. If an employee does not fill leave form he/she will be counted absent for off-office days and his/her salary will be deducted. If an employee does not use his/her leaves, the leaves will not be transferred to next year and the employee will not be paid for unused leave.</p><p dir='ltr'><strong>Note:</strong></p><p dir='ltr'>&lt; &gt;Employees cannot take any type of loans under the name of CHA from any agency.<!-- -->On time payment of staff salaries depend on receipt of money from donors, and in case of delays, CHA cannot be taken responsible<!-- -->Responsibility for safety measures against unexpected incidents such as traffic accidents, murder, theft, abduction, etc. during the performance of their duties, are related to the employee, and the CHA has no responsibility for it<!-- -->To enhance the competitive differentiation of the organization and the renewal of the working systems and the development of staff capacity in the organization, 10 $ is deducted from the salaries of the Project and management staff (the service staff is exemplary), the amount indicated is used for renewal and retention Systems and employee capacity development.<!-- -->In order to co-operate with and to address the contributing colleagues who suffer, from the salary level of each employee (whether permanent, project or service) of the network of offices, they will be charged a monthly amount of 5 $ for this purpose.<!-- dir='RTL'-->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span dir='LTR'>End:</span></p><p dir='ltr'>I accept all the terms in this contract and promise to perform all duties honestly, with integrity, and according to rules and regulations of CHA</p><p dir='ltr'>Selected candidate for hiring:&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Signature: (&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;)</p><p dir='ltr'>Approval by Field Office Manager:&nbsp; Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Signature: (&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;)</p><p dir='ltr'>Approval by authority: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Signature: (&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;)</p><p><!--![endif]----></p>", ContentDari="'<p dir='rtl'>شرايط قرارداد:&nbsp; &nbsp;</p><p dir='rtl'>اين قرارداد بمدت (&nbsp;&nbsp; 2&nbsp; ) ساعت<span dir='LTR'>)&nbsp; </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span dir='LTR'>(</span> روز<span dir='LTR'>)</span> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span dir='LTR'>(</span>&nbsp; &nbsp;هفته<span dir='LTR'>)</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span dir='LTR'>(</span> &nbsp;ماه <span dir='LTR'>)</span> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<span dir='LTR'>(</span>&nbsp; بوده درصورتيكه ميعاد قرارداد اضافه از چهار ماه باشد دو ماه آن شامل دوره امتحاني ميباشد، در صورتيكه كارمند به ادامه كار موافق نباشند بايد يك ماه قبل بخاطر فسخ قرار داد اطلاع بدهند و درصورت عدم كارآئي كارمند در مدت دوره امتحاني اداره با اطلاع قبلي سه روزه قرارداد را فسخ كرده ميتواند. درصورت عدم تكميل يكسال كار كارمند اداره هيچ نوع معاوضه اضافي به كارمند پرداخته نميتواند كارمند صرف مستحق معاش ايام كاركرده گي خويش ميباشد. بعد از تكميل 45 روز از كار تان ارزيابي صورت ميگيرد در صورتيكه شخص كاركن، وظيفه شناس و رعايت كننده لايحه اصول و ضوابط سازمان در ابعاد كاري تان&nbsp; به مؤسسه ثابت شويد و اداره به ادامهء كار شما نياز داشته باشد. قرارداد تان تمديد خواهد شد &nbsp;.</p><p dir='rtl'>ساعات كاري:</p><p dir='rtl'>ساعات كاري شما در يك هفته از روز یکشنبه &nbsp;الي پنجشنبه<span dir='LTR'>)&nbsp; </span>40 <span dir='LTR'>&nbsp;&nbsp;(</span> ساعت بوده به كار نمودن اضافه از ساعات تعين شده در صورت كه ضرورت باشد ارج گذاشته ميشود. در جريان روز مدت يك ساعت وقفه نيم روزي تخصيص داده شده است.</p><p dir='rtl'>شروع كار:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;</p><p dir='rtl'>ختم كار:&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (اين وقت نظر به فصول سال متغير است )&nbsp;</p><p dir='rtl'>براي كارمندان خدماتي ايام كاري از شنبه الي جمعه بوده ساعات كاري قابل تغير ميباشد.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p dir='rtl'>رخصتي:</p><p dir='rtl'>كارمندان سازمان در جريان يكسال مستحق رخصتي هاي ذيل ميباشند:</p><p dir='rtl'><strong>رخصت تفريحي:</strong> 20 روز در يكسال بوده كه رخصتي تفريحي قبلاً پلان شده و درخواست جهت اجراي آن حد اقل يكماه قبل به بخش مربوطه سپرده ميشود بعد از موافقه بخش مربوطه قابل اجرا ميباشد و مطابق لايحه محاسبه ميگردد.</p><p dir='rtl'><strong>رخصت ضرورت عاجل:</strong>&nbsp; 10 روز در يكسال ميباشد.</p><p dir='rtl'><strong>رخصت مريضي:</strong>&nbsp; 20 روز دريكسال ميباشد، درصورتيكه رخصتي مريضي اضافه از سه روز ميباشد تصديق مرجع معتبر صحي لازمي است.</p><p dir='rtl'><strong>رخصت ولادي: </strong>سه ماه در يكسال بوده كه يكماه آن قبل از ولادت و دو ماه آن بعد از ولادت قابل اجرا است و نيز الي مدت دوسال براي مادران شيرده روزانه يكساعت وقفه براي شيردهي طفل در نظر گرفته شده است. يك كارمند در طول سه سال يكبار مستحق رخصتي ولادي ميباشد و درصورت استفاده از رخصتي ولادي درهمان سال مستحق رخصت تفريحي نميباشد<strong><span dir='RTL'>كارمندان نيمه وقت امتياز استفاده از رخصتي هاي فوق را ندارند.</span></strong></p><p dir='rtl'>تمام درخواست رخصتي ها بعد از خانه پري فورم رخصت، تصديق بخش مربوطه و امضاء آمر دفتر يا مسئول اداري به بخش اداري&nbsp; تسليم داده ميشود درصورت عدم خانه پري فورم رخصت ايام غيابت غير حاضر معامله گرديده و معاش كارمند وضع ميگردد، درصورتيكه كارمند از رخصتي هاي خويش استفاده نمي نمايد در سال متعاقب يا معاوضه آن قابل مجرا نيست.</p><p dir='rtl'>کارمندان مستحق گرفتن این&nbsp; رخصتی ها بعد از تکمیل دوره امتحانی می باشند.</p><p dir='rtl'><strong>نوت:</strong></p><p dir='rtl'>1- کارمندان صلاحیت گرفتن هیچ نوع قرضه را به نام <span dir='LTR'>CHA</span> از هيچ مرجعي ندارد.</p><p dir='rtl'>2- اجراي بروقت معاش کارمندان مربوط به رسيدن پول پروژه از جانب دونر ميباشد و در قسمت تاخیر آن <span dir='LTR'>CHA</span> هیچ نوع مسولیت ندارد.</p><p dir='rtl'>3 - مسؤلیت&nbsp; تدابیر ایمنی درمقابل حوادث غیر مترقبه از قبیل حوادث ترافیکی، قتل ، سرقت، اختطاف ، وغیره&nbsp; درحین اجرای وظیفه وبیرون از آن مربوط بخود کارمند بوده موسسه <span dir='LTR'>CHA</span>&nbsp; در قبال آن هیچ نوع مسؤلیت ندارد</p><p dir='rtl'>4- جهت بلند بردن تمایز رقابتی سازمان و تجدید سیستم های کاری و انکشاف ظرفیت های کاری کارمندان در سازمان10 دالر امریکائی از معاشات کارمندان منجمنت و پروژوی سازمان وضع میگردد ( کارمندان خدماتی از این امر مثتثنی میباشد )، و مبلغ ذکر شده جهت تجدید و تازه نگهداشتن سیستم ها و انکشاف ظرفیت کاری کارمندان بمصرف میرسد.</p><p dir='rtl'>5-جهت همکاری و رسیدگی به همکاران آسیب رسیده و متضرر ازپيرول معاش هر کارمند (چه دايمی وچه پروژوی وچه خدماتی) دفاتر شبکه، از بودجهء مربوطه شان ماهانه مبلغ 5 دالر بدينمنظور وضع ميگردد.</p><h5 dir='rtl'>&nbsp;&nbsp; و من الله توفيق</h5><p dir='rtl'>اينجانب كه در پايان اين يادداشت امضاء نموده ام تمام مواد مندرجه (يادداشت استخدام/قرار داد مؤقت) را قبول داشته و وعده ميدهم تا كارهايم را با كمال صداقت، امانت داري، به وجه احسن و مطابق لايحه اصول و ضوابط سازمان هم آهنگي كمكهاي انساني (همكاري) <span dir='LTR'>CHA</span> انجام دهم.</p><p dir='rtl'><strong>&nbsp;كارمند انتخاب شده براي استخدام:</strong>&nbsp;&nbsp; اسم:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;امضاء: &nbsp;&nbsp;(&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)</p><p dir='rtl'>&nbsp;</p><p dir='rtl'><strong><span dir='RTL'>منظوري مقام منظور كننده:</span></strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span dir='RTL'>اسم:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>&nbsp;<span dir='RTL'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;امضاء: &nbsp;(&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)</span></p>'", EmployeeContractTypeId=1, OfficeId= 1 },
                    new ContractTypeContent { IsDeleted= false, ContentEnglish="<p><strong>By signing of this contract, the employee hereby agrees to:</strong></p><ul><li>Fulfill the requirement detailed in the rule description.</li><li>Adhere to CHA&rsquo;s procedure and policies</li><li>Conduct him/her self in ethical and honest way</li></ul><p>&nbsp;</p><p><strong>Termination/ Extension of contract:</strong></p><p>This agreement can be terminated by either party with or without reason by submitting 30 days written notice, in special cases like termination of contact by donors, organization can terminate employees contact without any notice.</p><p>The contract may be extended in the case of project continuation and or the further availability of fund.</p><ul><li>This contract can be terminated by the organization any time with a one month prior notice, in cases like contract termination by donors; the organization can terminate this contract any time without prior notice.</li></ul><ul><li>In case project extends, contract with project staff will be extended, but this will be done if there is need for doing so, otherwise contract extension is not must.</li><li>At the end of contract the organization is obliged to pay one month salary to the employee</li><li>Employees cannot take any type of loans under the name of CHA from any agency.</li><li>On time payment of staff salaries depend on receipt of money from donors, and in case of delays, CHA cannot be taken responsible</li><li>Besides the assigned tasks, the organization can assign the employee to other official tasks or transfer him/her to other locations.</li><li>The employee has to be present to duty location for the defined time period.</li><li>Responsibility for safety measures against unexpected incidents such as traffic accidents, murder, theft, abduction, etc. during the performance of their duties, are related to the employee, and the CHA has no responsibility for it</li><li>To enhance the competitive differentiation of the organization and the renewal of the working systems and the development of staff capacity in the organization, 10 $ is deducted from the salaries of the Project and management staff (the service staff is exemplary), the amount indicated is used for renewal and retention Systems and employee capacity development.</li><li>- In order to co-operate with and to address the contributing colleagues who suffer, from the salary level of each employee (whether permanent, project or service) of the network of offices, they will be charged a monthly amount of 5 $ for this purpose.</li></ul><p><strong>The employee gives commitment toward the policy and procedure to:</strong></p><ul><li>To pay 4.5% from monthly salary to the pension account according to the rules and procedure of the organization</li><li>If leaves job in a wrong way, can take their money from this account by paying one month salary to the organization (CHA)</li><li>Does not try to withdraw this amount while working in the organization.</li></ul><p dir='RTL' style='margin-right:.5in'>&nbsp;</p><p style='margin-right:7.1pt'><strong>Considering religious and moral principles, the employee promises to:</strong></p><ul><li>Perform tasks honestly, with integrity, neutrality, without any party, tribe, religion, gender, language and region related discriminations, protect organization&rsquo;s properties and belongings and does not prefer self-interests over organization&rsquo;s interests.</li><li>Make efforts for attaining defined goals and objectives with all his/her abilities and strengths, use his/her strength and energy for the organization and in this way for sound development of Afghanistan.</li><li>Accept all rules and regulations and considers them as guidance.</li></ul><p style='margin-right:7.1pt'>These terms are acceptable and will be followed.</p><p dir='RTL'>&nbsp;</p><p style='margin-right:7.1pt'><strong>Employee name and signature&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Approved by<span dir='RTL'>:</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span dir='RTL'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>Name: </strong>Ghulam Yahya Abbasy&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<strong>Name:</strong> Zarjan&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Managing Director &nbsp;&nbsp;</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span dir='RTL'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><strong>Signature:&nbsp;</strong>&nbsp;(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; )<strong>&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Signature</strong>: (&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)</p>", ContentDari="<p dir='RTL' style='text-align: justify;'><strong><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>با امضای این قرارداد کارمند موافقه مینماید که: </span></strong></p><ul style='list-style-type: disc;'><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>شرایط که در اصول وظایفش تشریح گردیده را اجرا نماید</span></li><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>به پالیسی و قوانین موسسه سی اچ ای پایبند باشد </span></li><li style='text-align: justify;'><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>در رفتار و اخلاقش صادق باشد </span></li></ul><p dir='RTL' style='text-align: justify;'><strong><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>انفکاک یا </span></strong><strong><span style='font-size: 12pt; font-family: \"Times New Roman\", serif;'>تمدید قرارداد:&nbsp; </ span ></ strong ></ p >< p dir = 'RTL' style = 'text-align: justify;' >< span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;' > یکی از طرفین این قرارداد با ارایه اطلاع کتبی یک ماه قبل میتوانند اینقرارداد را فسخنمایند.در شرایط خاص مانند ختم قرارداد از طرف نهاد های تمویل کننده موسسهمیتواند کارمند را بدون اطلاع قبلی منفک نماید.</ span ></ p >< p dir = 'RTL' style = 'text-align: justify;' >< span style = 'font-size: 12pt; font-family: \"Times New Roman\", serif;' > در موجودیت امکانات مالی یا تمدید قرارداد با نهاد تمویل کننده قرارداد کارمند نیز تمدید میگردد. </ span ></ p ></p><ul dir='rtl'><li>اين قرار داد از طرف سازمان با تذکري که يک ماه قبل داده ميشود ، ميتواند در هر زماني با کارمند فسخ گردد، در حالات خاص مانند فسخ قرار داد توسط دونرها، اداره ميتواند قرار داد کارمند را بدون آگهي قبلي فسخ نمايد.&nbsp;&nbsp;</li><li>در صورت ادامه پروژه اگر قرارداد کارمند پروژوي باشد قرارداد دوبارهتمديد ميگردد ، آنهم نظربه ضرورت اداره وامر حتمى نيست كه قرارداد بايد تمديد شود.</li><li>درصورت ختم قرارداد اداره مكلف به پرداخت يك ماه معاش اضافى به كارمند نميباشد.</li><li>کارمندان صلاحيت گرفتن هيچ نوع قرضه را به نام <span dir='LTR'>CHA</span> از هيچ مرجعي ندارد.</li><li>اجراي بروقت معاش کارمندان مربوط به رسيدن پول پروژه از جانب دونر ميباشد و در قسمت تاخير آن <span dir='LTR'>CHA</span> هيچ نوع مسوليت ندارد.</li><li>برعلاوه وظايف ذكر شده اداره ميتواند شخص استخدام شونده را درصورت ضرورت به اجراء وظايف ديگردرحيطه فعاليت هاي رسمي سازمان توظيف يا به محل ديگرى تبديل نمايد.</li><li>شخص استخدام شونده مكلف به حضور در محل كار وايفاى وظيفه درطول وقت معينه ميباشد.</li><li><span dir='LTR'>CHA</span>&nbsp; در قبال آن هیچ نوع مسؤلیت ندارد</li><li>جهت بلند بردن تمایز رقابتی سازمان و تجدید سیستم های کاری و انکشاف ظرفیت های کاری کارمندان در سازمان10 دالر امریکائی از معاشات کارمندان منجمنت و پروژوی سازمان وضع میگردد ( کارمندان خدماتی از این امر مثتثنی میباشد )، و مبلغ ذکر شده جهت تجدید و تازه نگهداشتن سیستم ها و انکشاف ظرفیت کاری کارمندان بمصرف میرسد.</li><li>جهت همکاری و رسیدگی به همکاران آسیب رسیده و متضرر ازپيرول معاش هر کارمند (چه دايمی وچه پروژوی وچه خدماتی) دفاتر شبکه، از بودجهء مربوطه شان ماهانه مبلغ 5 دالر بدينمنظور وضع ميگردد.</li></ul><p dir='RTL' style='margin-left:7.1pt'>&nbsp;</p><p dir='RTL' style='margin-left:7.1pt'><strong>کارمند در قبال پاليسي و پروسيجر تقاعد تعهد مينمايد که:</strong></p><p dir='RTL' style='margin-left:7.1pt'>&nbsp;</p><ul dir='rtl'><li>کارمند مطابق پاليسي سازمان، متعهد به پرداخت ماهانه <span dir='LTR'>4.5%</span> از معاش خويش، بحساب بانکي&nbsp; تقاعد ميباشد.</li><li>در صورتيکه کارمند وظيفه خويشرا مغاير اصول سازمان ترک نمايد، در آنصورت زماني مستحق حقوق تقاعد خويش ميگرددکه يکماه معاش خود را به دفتر <span dir='LTR'>CHA</span> باز پرداخت نمايد .</li><li>کارمند در مدت زمان برحالي در وظيفه ادعاي اخذ پول تقاعد خويش را از دفتر کرده نمي تواند<span dir='LTR'>.</span>&nbsp;</li></ul><p dir='RTL' style='margin-left:7.1pt'>&nbsp;</p><p dir='RTL' style='margin-left:7.1pt'><strong>شخص استخدام شونده با درنظرداشت موازين دينى و اخلاقى تعهد مينمايد كه:</strong></p><p dir='RTL' style='margin-left:7.1pt'>&nbsp;</p><ul dir='rtl'><li>وظايف خودرا با كمال صداقت، امانت دارى ، بيطرفى كامل ومبرا از هر گونه تبعيض وتمايز گروهي، تنظيمي، قومي ، مذهبي، جنسي، لساني ومنطقوي انجام دهد ،اموال ودارايى مؤسسه را به نحواحسن مراقبت وحفاظت كند و منافع شخصى خود را بر منافع مؤسسه رجحان ندهد.</li><li>استخدام شونده تعهد مينمايد كه باتمام نيرو وتوان در راه حصول اهداف اساسى وبرنامه هاى پلان شدهء مؤسسه تلاش كند ، قدرت وانرژى فكرى خود را در راه خدمت به مؤسسه وازين طريق&nbsp; درراستاى ترقى وانكشاف سالم و پاياى افغانستان عزيز مصرف نمايد.</li><li>تمام اصول و ضوابط اداره را قبول نموده وآنرا رهنماي کاري خويش قرار دهد.</li></ul><p dir='RTL' style='margin-left:7.1pt'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p dir='RTL' style='margin-left:7.1pt'>موارد فوق قابل قبول و توافق بوده كاملاً رعايت ميگردند.</p><p dir='rtl'><strong>Approved by:&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Employee name and signature&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Name</strong>:&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p style='text-align: right;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Name: &nbsp;</strong>&nbsp;&nbsp;<strong>&nbsp;&nbsp;&nbsp;</strong>&nbsp;</p><p dir='rtl'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Signature:&nbsp; </strong>&nbsp;(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Signature:</strong> (&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>", EmployeeContractTypeId=2, OfficeId= 1 }
                };
                await context.ContractTypeContent.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static async Task AddAccountLevel(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<AccountLevel> list = new List<AccountLevel>
                {
                    new AccountLevel { AccountLevelId = 1, AccountLevelName = "Main Level Accounts", CreatedDate = DateTime.UtcNow},
                    new AccountLevel { AccountLevelId = 2, AccountLevelName = "Control Level Accounts", CreatedDate = DateTime.UtcNow },
                    new AccountLevel { AccountLevelId = 3, AccountLevelName = "Sub Level Accounts", CreatedDate = DateTime.UtcNow },
                    new AccountLevel { AccountLevelId = 4, AccountLevelName = "Input Level Accounts", CreatedDate = DateTime.UtcNow }
                };
                await context.AccountLevel.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static async Task AddJobGrades(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<JobGrade> list = new List<JobGrade>
                {
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-1"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-2"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-3"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-4"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-5"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-6"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-7"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-8"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-9"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-10"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-11"},
                    new JobGrade {IsDeleted= false, CreatedDate = DateTime.UtcNow, GradeName="Grade-12"}
                };

                await context.JobGrade.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task AddMarketingCategory(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<Category> list = new List<Category>
                {
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryName="Bank" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryName = "NGO" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryName = "Telecommunicaton" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryName = "Government" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryName = "Hospital" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryName = "Travel Agency" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryName = "University" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryName = "Media Groups" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryName = "Shops" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow,  CategoryName = "Energy" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow,  CategoryName = "School" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow,  CategoryName = "Construction" }
                };
                await context.Categories.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task AddActivityStatus(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<ActivityStatusDetail> list = new List<ActivityStatusDetail>
                {
                    new ActivityStatusDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName="Planning" },
                    new ActivityStatusDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "Implementation" },
                    new ActivityStatusDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "Completed" }
                };
                await context.ActivityStatusDetail.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddCurrencyDetails(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<CurrencyDetails> list = new List<CurrencyDetails>
                {
                    new CurrencyDetails { CurrencyName = "Afghani", CurrencyCode = "AFN", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new CurrencyDetails { CurrencyName = "European Curency", CurrencyCode = "EUR", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new CurrencyDetails { CurrencyName = "Pakistani Rupees", CurrencyCode = "PKR", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new CurrencyDetails { CurrencyName = "US Dollars", CurrencyCode = "USD", IsDeleted = false, CreatedDate = DateTime.UtcNow }
                };
                await context.CurrencyDetails.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddPayrollAccountHead(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<PayrollAccountHead> list = new List<PayrollAccountHead>
                {
                   new PayrollAccountHead { AccountNo = null, IsDeleted = false, Description = null, PayrollHeadName = "Net Salary", PayrollHeadTypeId = 3, TransactionTypeId = 1 , CreatedDate = DateTime.UtcNow},
                    new PayrollAccountHead { AccountNo = null, IsDeleted = false, Description = null, PayrollHeadName = "Advance Deduction", PayrollHeadTypeId = 2, TransactionTypeId = 1, CreatedDate = DateTime.UtcNow },
                    new PayrollAccountHead { AccountNo = null, IsDeleted = false, Description = null, PayrollHeadName = "Salary Tax", PayrollHeadTypeId = 2, TransactionTypeId = 1, CreatedDate = DateTime.UtcNow},
                    new PayrollAccountHead { AccountNo = null, IsDeleted = true, Description = null, PayrollHeadName = "Gross Salary", PayrollHeadTypeId = 3, TransactionTypeId = 2, CreatedDate = DateTime.UtcNow},
                    new PayrollAccountHead { AccountNo = null, IsDeleted = false, Description = null, PayrollHeadName = "Pension", PayrollHeadTypeId = 2, TransactionTypeId = 1 , CreatedDate = DateTime.UtcNow}
                };
                await context.PayrollAccountHead.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddOfficeDetail(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<OfficeDetail> list = new List<OfficeDetail>
                {
                    new OfficeDetail { OfficeCode = "A0001", OfficeKey = "AF", OfficeName = "Afghanistan", IsDeleted = false, CreatedDate = DateTime.UtcNow }
                };
                await context.OfficeDetail.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddDepartment(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<Department> list = new List<Department>
                {
                    new Department (){ DepartmentName = "Administration", OfficeId = 1, IsDeleted = false }
                };
                await context.Department.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddSalaryHeadDetails(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<SalaryHeadDetails> list = new List<SalaryHeadDetails>
                {
                    new SalaryHeadDetails {  HeadName = "Tr Allowance", Description = "Tr Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails {  HeadName = "Food Allowance", Description = "Food Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails {  HeadName = "Fine Deduction", Description = "Fine Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                    new SalaryHeadDetails {  HeadName = "Capacity Building Deduction", Description = "Capacity Building Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                    new SalaryHeadDetails {  HeadName = "Security Deduction", Description = "Security Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                    new SalaryHeadDetails {  HeadName = "Other Allowance", Description = "Other Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails {  HeadName = "Other Deduction", Description = "Other Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                    new SalaryHeadDetails {  HeadName = "Medical Allowance", Description = "Medical Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails {  HeadName = "Other1Allowance", Description = "Other1Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails {  HeadName = "Other2Allowance", Description = "Other2Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails {  HeadName = "Basic Pay (In hours)", Description = "Basic Pay (In hours)", HeadTypeId = 3, TransactionTypeId = 2, IsDeleted = false }
                };
                await context.SalaryHeadDetails.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddLanguageDetail(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<LanguageDetail> list = new List<LanguageDetail>
                {
                    new LanguageDetail { IsDeleted = false, LanguageName = "Arabic" },
                    new LanguageDetail { IsDeleted = false, LanguageName = "Dari" },
                    new LanguageDetail { IsDeleted = false, LanguageName = "English" },
                    new LanguageDetail { IsDeleted = false, LanguageName = "French" },
                    new LanguageDetail { IsDeleted = false, LanguageName = "German" },
                    new LanguageDetail { IsDeleted = false, LanguageName = "Pashto" },
                    new LanguageDetail { IsDeleted = false, LanguageName = "Russian" },
                    new LanguageDetail { IsDeleted = false, LanguageName = "Turkish" },
                    new LanguageDetail { IsDeleted = false, LanguageName = "Turkmani" },
                    new LanguageDetail { IsDeleted = false, LanguageName = "Urdu" },
                    new LanguageDetail { IsDeleted = false, LanguageName = "Uzbek" }
                };
                await context.LanguageDetail.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddLeaveReasonDetail(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<LeaveReasonDetail> list = new List<LeaveReasonDetail>
                {
                    new LeaveReasonDetail { IsDeleted = false, ReasonName = "Casual Leave", Unit = 12 },
                    new LeaveReasonDetail { IsDeleted = false, ReasonName = "Emergency Leave", Unit = 6 },
                    new LeaveReasonDetail { IsDeleted = false, ReasonName = "Maternity Leave", Unit = 90 }
                };
                await context.LeaveReasonDetail.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddActivityType(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<ActivityType> list = new List<ActivityType>
                {
                    new ActivityType { IsDeleted = false, ActivityName = "Broadcasting" },
                    new ActivityType { IsDeleted = false, ActivityName = "Production" }
                };
                await context.ActivityTypes.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddFinancialYearDetail(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<FinancialYearDetail> list = new List<FinancialYearDetail>
                {
                    new FinancialYearDetail { IsDeleted = false, StartDate = new DateTime(DateTime.Now.Year, 1, 1), EndDate = new DateTime(DateTime.Now.Year, 12, 31), FinancialYearName = DateTime.Now.Year + " Financial Year", IsDefault = true }
                };
                await context.FinancialYearDetail.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddCountryDetails(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<CountryDetails> list = new List<CountryDetails>
                {
                    new CountryDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryName = "Afghanistan" },
                    new CountryDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryName = "United States" }
                 };
                await context.CountryDetails.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddProvinceDetails(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<ProvinceDetails> list = new List<ProvinceDetails>
                {
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Badghis" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Baghlan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Balkh" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Bamyan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Daykundi" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Farah" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Faryab" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Ghazni" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Ghor" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Helmand" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Herat" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Jowzjan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Kabul" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Kandahar" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Kapisa" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Khost" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Kunar" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Kunduz" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Laghman" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Logar" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Wardak" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Nangarhar" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Nimruz" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Nuristan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Paktia" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Paktika" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Panjshir" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Parwan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Samangan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Sar-e Pol" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Takhar" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Urozgan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Zabul" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Alabama" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Alaska" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Arizona" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Arkansas" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "California" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Colorado" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Connecticut" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Delaware" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Florida" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Georgia" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Hawaii" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Idaho" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Illinois" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Indiana" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Iowa" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Kansas" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Kentucky" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Louisiana" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Maine" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Maryland" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Massachusetts" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Michigan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Minnesota" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Mississippi" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Missouri" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Montana" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Nebraska" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Nevada" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "New Hampshire" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "New Jersey" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "New Mexico" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "New York" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "North Carolina" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "North Dakota" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Ohio" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Oklahoma" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Oregon" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Pennsylvania" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Rhode Island" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "South Carolina" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "South Dakota" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Tennessee" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Texas" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Utah" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Vermont" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Virginia" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Washington" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "West Virginia" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Wisconsin" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceName = "Wyoming" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceName = "Badakhshan" }
               };
                await context.ProvinceDetails.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddDistrictDetail(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<DistrictDetail> list = new List<DistrictDetail>
                {
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Ab Kamari", ProvinceID = 1 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Ghormach", ProvinceID = 1 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Jawand", ProvinceID = 1 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Andarab", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Baghlan", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Balkh", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Chahar Bolak", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Bamyan", ProvinceID = 4 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Kahmard", ProvinceID = 4 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Panjab", ProvinceID = 4 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Gizab", ProvinceID = 5 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Anar Dara", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Bakwa", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Andkhoy", ProvinceID = 7 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Almar", ProvinceID = 7 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Bilchiragh", ProvinceID = 7 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Ab Band", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Ajristan", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Chaghcharan", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Charsada", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Baghran", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Dishu", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Adraskan", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Aqcha", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Darzab", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Fayzabad", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Bagrami", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Chahar Asyab", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Deh Sabz", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Arghandab", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Dand(Kandahar)", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Alasay", ProvinceID = 15 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Bak", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Gurbuz", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Asadabad", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Bar Kunar", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Ali Abad", ProvinceID = 18 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Archi", ProvinceID = 18 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Alingar", ProvinceID = 19 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Alishing", ProvinceID = 19 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Azra", ProvinceID = 20 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Baraki Barak", ProvinceID = 20 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Chaki", ProvinceID = 21 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Achin", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Bati Kot", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Chahar Burjak", ProvinceID = 23 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Chakhansur", ProvinceID = 23 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Bargi Matal", ProvinceID = 24 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Du Ab", ProvinceID = 24 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Ahmadabad", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Chamkani", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Dand Wa Patan", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Gardez", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Barmal", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Dila", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Gayan", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Gomal", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Anaba", ProvinceID = 27 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Bagram", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Chaharikar", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Ghorband", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Jabal Saraj", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Kohi Safi", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Aybak", ProvinceID = 29 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Balkhab", ProvinceID = 30 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Baharak", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Chora", ProvinceID = 32 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Argahandab", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Atghar", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Alabama", ProvinceID = 34 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Arizona", ProvinceID = 35 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Jurors", ProvinceID = 35 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Arona", ProvinceID = 35 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Arkansas", ProvinceID = 36 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "California", ProvinceID = 37 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Califor", ProvinceID = 37 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Colorado", ProvinceID = 38 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Connecticut", ProvinceID = 39 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Aelaware", ProvinceID = 40 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Florida", ProvinceID = 41 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Georia", ProvinceID = 42 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Hawaii", ProvinceID = 43 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Idaho", ProvinceID = 44 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Illinois", ProvinceID = 45 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Indiana", ProvinceID = 46 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Undia", ProvinceID = 46 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Iowa", ProvinceID = 47 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Lansa", ProvinceID = 48 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Kentucky", ProvinceID = 49 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Louisiana", ProvinceID = 50 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Maine", ProvinceID = 51 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Maryland", ProvinceID = 52 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Massachusetts", ProvinceID = 53 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Michigan", ProvinceID = 54 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Minnesota", ProvinceID = 55 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Mississippi", ProvinceID = 56 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Missouri", ProvinceID = 57 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Montana", ProvinceID = 58 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, District = "Nebraska", ProvinceID = 59 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Yevada", ProvinceID = 60 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "New Hampshire", ProvinceID = 61 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "New Jersey", ProvinceID = 62 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "New Mexico", ProvinceID = 63 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "New York", ProvinceID = 64 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "North Carolina", ProvinceID = 65 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "North Dakota", ProvinceID = 66 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ohio", ProvinceID = 67 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Oklahoma", ProvinceID = 68 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tregon", ProvinceID = 69 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Pennsylvania", ProvinceID = 70 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Rhode Island", ProvinceID = 71 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "South Carolina", ProvinceID = 72 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "South Dakota", ProvinceID = 73 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tennessee", ProvinceID = 74 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Texas", ProvinceID = 75 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Wtaha", ProvinceID = 76 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Oermont", ProvinceID = 77 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Virginia", ProvinceID = 78 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Washinn", ProvinceID = 79 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "West Virginia", ProvinceID = 80 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nouit Vinia", ProvinceID = 80 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Wisconsin", ProvinceID = 81 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Wyoming", ProvinceID = 82 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Arghanj Khwa", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Argo", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Baharak", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Darayim", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Fayzabad", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ishkashim", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Jurm", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khash", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khwahan", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kishim", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kohistan", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kuf Ab", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kuran Wa Munjan", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Maimay", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nusay", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ragh", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shahri Buzurg", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shekay", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shighnan", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shuhada", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tagab", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shuhada", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tishkan", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Wakhan", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Wurduj", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Yaftali Sufla", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Yamgan", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Yawan", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Zebak", ProvinceID = 84 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Baghlani Jadid", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Burka", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dahana i Ghuri", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dih Salah", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dushi", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Farang wa Gharu", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Guzargahi Nur", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khinjan", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khost wa Fereng", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khwaja Hijran", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nahrin", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Puli Hisar", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Puli Khumri", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tala Wa Barfak", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Chardara", ProvinceID = 18 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Imam Sahib", ProvinceID = 18 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khan Abad", ProvinceID = 18 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kunduz", ProvinceID = 18 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qalay-I-Zal", ProvinceID = 18 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Bangi", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Chah Ab", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Chal", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Darqad", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dashti Qala", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Farkhar", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Hazar Sumuch", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ishkamish", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kalafgan", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khwaja Baha Wuddin", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khwaja Ghar", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Namak Ab", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Rustaq", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Taluqan", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Warsaj", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Yangi Qala", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Chahar Kint", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Chimtal", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dawlatabad", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dihdadi", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kaldar", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khulmi", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kishindih", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Marmul", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mazar-e Sharif", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nahri Shahi", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sholgara", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shortepa", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Zari", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khamyab", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khaniqa", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khwaja Du Koh", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mardyan", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mingajik", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qarqin", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qush Tepa", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shibirghan", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dara-I-Sufi Balla", ProvinceID = 29 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dara-I-Sufi Payan", ProvinceID = 29 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Feroz Nakhchir", ProvinceID = 29 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Hazrati Sultan", ProvinceID = 29 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khuram Wa Sarbagh", ProvinceID = 29 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ruyi Du Ab", ProvinceID = 29 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Gosfandi", ProvinceID = 30 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kohistanat", ProvinceID = 30 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sangcharak", ProvinceID = 30 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sar-e Pul", ProvinceID = 30 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sayyad", ProvinceID = 30 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sozma Qala", ProvinceID = 30 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Farza", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Guldara", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Istalif", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kabul", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kalakan", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khaki Jabbar", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mir Bacha Kot", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mussahi", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Paghman", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qarabagh", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shakardara", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Surobi", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Hesa Awal Kohistan", ProvinceID = 15 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Hesa Duwum Kohistan", ProvinceID = 15 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Koh Band", ProvinceID = 15 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mahmud Raqi", ProvinceID = 15 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nijrab", ProvinceID = 15 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tagab", ProvinceID = 15 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Charkh", ProvinceID = 20 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kharwar", ProvinceID = 20 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khoshi", ProvinceID = 20 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mohammad Agha", ProvinceID = 20 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Puli Alam", ProvinceID = 20 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Bazarak", ProvinceID = 27 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Darah", ProvinceID = 27 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khenj", ProvinceID = 27 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Paryan", ProvinceID = 27 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Rokha", ProvinceID = 27 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shotul", ProvinceID = 27 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Salang", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sayed Khel", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shekh Ali", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shinwari", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Surkhi Parsa", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Day Mirdad", ProvinceID = 21 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Hisa-I-Awali Bihsud", ProvinceID = 21 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Jaghatu", ProvinceID = 21 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Jalrez", ProvinceID = 21 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Markazi Bihsud", ProvinceID = 21 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Maydan Shahr", ProvinceID = 21 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nirkh", ProvinceID = 21 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Saydabad", ProvinceID = 21 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Chapa Dara", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Chawkay", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dangam", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dara-I-Pech", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ghaziabad", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khas Kunar", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Marawara", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Narang Wa Badil", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nari", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nurgal", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shaygal Wa Shiltan", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sirkanay", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Wata Pur", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shultan", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Baad Pakh", ProvinceID = 19 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dawlat Shah", ProvinceID = 19 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mihtarlam", ProvinceID = 19 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qarghayi", ProvinceID = 19 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Behsud", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Chaparhar", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dara-I-Nur", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dih Bala", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dur Baba", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Goshta", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Hisarak", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Jalalabad", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kama", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khogyani", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kot", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kuz Kunar", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Lal Pur", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Muhmand Dara", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nazyan", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Pachir Wa Agam", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Rodat", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sherzad", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shinwar", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Surkh Rod", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Haska Meyna", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kamdesh", ProvinceID = 24 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mandol", ProvinceID = 24 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nurgaram", ProvinceID = 24 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Parun", ProvinceID = 24 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Wama", ProvinceID = 24 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Waygal", ProvinceID = 24 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Muqur", ProvinceID = 1 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Murghab", ProvinceID = 1 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qadis", ProvinceID = 1 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qala-I-Naw", ProvinceID = 1 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sayghan", ProvinceID = 4 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shibar", ProvinceID = 4 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Waras", ProvinceID = 4 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Yakawlang", ProvinceID = 4 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Bala Buluk", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Farah", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Gulistan", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khaki Safed", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Lash wa Juwayn", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Pur Chaman", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Pusht Rod", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qala i Kah", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shib Koh", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dawlat Yar", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Du Layna", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Lal Wa Sarjangal", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Pasaband", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Saghar", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shahrak", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Taywara", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tulak", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ferozkoh", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Chishti Sharif", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Farsi", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ghoryan", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Gulran", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Guzara", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Hirat", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Injil", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Karukh", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kohsan", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kushk", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kushki Kuhna", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Obe", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Pashtun Zarghun", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shindand", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Zinda Jan", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Turghandi", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Islam Qala", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Andar", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Dih Yak", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Gelan", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ghazni City", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Giro", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Jaghatū District", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Jaghuri", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khugiani", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khwaja Umari", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Malistan", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Muqur", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nawa", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nawur", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qarabagh", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Rashidan", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Waghaz", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Zana Khan", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Jaji Maydan", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khost(Matun)", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mandozai", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Musa Khel", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nadir Shah Kot", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qalandar", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sabari", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shamal", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Spera", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tani", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tere Zayi", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Jaji", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Jani Khail", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Lazha Ahmad Khel", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sayed Karam", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shwak", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Wuza Zadran", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Zurmat", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Janikhel", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mata Khan", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nika", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Omna", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sar Hawza", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sarobi", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sharan", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Terwa", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Urgun", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Wazakhwa", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Wor Mamay", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Yahya Khel", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Yusufkhel", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Zarghun Shahr", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ziruk", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ishtarlay", ProvinceID = 5 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kajran", ProvinceID = 5 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khadir", ProvinceID = 5 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kiti", ProvinceID = 5 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Miramor", ProvinceID = 5 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nili", ProvinceID = 5 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sangtakht", ProvinceID = 5 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shahristan", ProvinceID = 5 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Garmsir", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Gerishk", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kajaki", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khanashin", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Lashkargah", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Majrah", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Musa Qala", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nad Ali", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nawa-I-Barakzayi", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Nawzad", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Sangin", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Washir", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Arghistan", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Daman", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Ghorak", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kandahar", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khakrez", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Maruf", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Maywand", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Miyan Nasheen", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Naish", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Panjwaye", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Reg", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shah Wali Kot", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shorabak", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Spin Boldak", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Zhari", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Takhta pool", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kang", ProvinceID = 23 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khash Rod", ProvinceID = 23 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Zaranj", ProvinceID = 23 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Deh Rahwod", ProvinceID = 32 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Khas Uruzgan", ProvinceID = 32 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shahindi Hassas", ProvinceID = 32 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tarin Kowt", ProvinceID = 32 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Daychopan", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Kakar", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Mizan", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Naw Bahar", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Qalat", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shah Joy", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shamulzayi", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Shinkay", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow,  District = "Tarnak Wa Jaldak", ProvinceID = 33 }
                };
                await context.DistrictDetail.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddReceiptType(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<ReceiptType> list = new List<ReceiptType>
                {
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeName = "Purchased" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeName = "Transfers" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeName = "Donation" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeName = "Take Over" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeName = "Loan" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeName = "Return" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeName = "Other" }
                };
                await context.ReceiptType.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddStatusAtTimeOfIssue(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<StatusAtTimeOfIssue> list = new List<StatusAtTimeOfIssue>
                {
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "New" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "Useable" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "To Repair" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "Damage" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "Sold" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "Stolen" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "Handover" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "Demolished" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusName = "Broken" }
                };
                await context.StatusAtTimeOfIssue.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddAccountHeadType(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<AccountHeadType> list = new List<AccountHeadType>
                {
                    new AccountHeadType { CreatedDate = DateTime.UtcNow, AccountHeadTypeName = "Assets", IsDeleted = false, IsCreditBalancetype = false },
                    new AccountHeadType { CreatedDate = DateTime.UtcNow, AccountHeadTypeName = "Liabilities", IsDeleted = false, IsCreditBalancetype = true },
                    new AccountHeadType { CreatedDate = DateTime.UtcNow, AccountHeadTypeName = "Donors Equity", IsDeleted = false, IsCreditBalancetype = true },
                    new AccountHeadType { CreatedDate = DateTime.UtcNow, AccountHeadTypeName = "Income", IsDeleted = false, IsCreditBalancetype = true },
                    new AccountHeadType { CreatedDate = DateTime.UtcNow, AccountHeadTypeName = "Expense", IsDeleted = false, IsCreditBalancetype = false }
                };
                await context.AccountHeadType.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddEmployeeContractType(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<EmployeeContractType> list = new List<EmployeeContractType>
                {
                    new EmployeeContractType { EmployeeContractTypeName = "Probationary" },
                    new EmployeeContractType { EmployeeContractTypeName = "PartTime" },
                    new EmployeeContractType { EmployeeContractTypeName = "Permanent" }
                };
                await context.EmployeeContractType.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddEmailType(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<EmailType> list = new List<EmailType>
                {
                    new EmailType { IsDeleted = false, EmailTypeName = "General", CreatedDate = DateTime.UtcNow },
                    new EmailType { IsDeleted = false, EmailTypeName = "Bidding Panel", CreatedDate = DateTime.UtcNow }
                };
                await context.EmailType.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddVoucherType(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<VoucherType> list = new List<VoucherType>
                {
                    new VoucherType { VoucherTypeName = "Adjustment", CreatedDate = DateTime.UtcNow },
                    new VoucherType { VoucherTypeName = "Journal", CreatedDate = DateTime.UtcNow }
                };
                await context.VoucherType.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddEmployeeType(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<EmployeeType> list = new List<EmployeeType>
                {
                    new EmployeeType { EmployeeTypeName = "Prospective", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new EmployeeType { EmployeeTypeName = "Active", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new EmployeeType { EmployeeTypeName = "Terminated", IsDeleted = false, CreatedDate = DateTime.UtcNow }
                };
                await context.EmployeeType.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddStrengthConsiderationDetail(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<StrengthConsiderationDetail> list = new List<StrengthConsiderationDetail>
                {
                    new StrengthConsiderationDetail { StrengthConsiderationName = "Gender Friendly", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new StrengthConsiderationDetail { StrengthConsiderationName = "Not Gender Friendly", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new StrengthConsiderationDetail { StrengthConsiderationName = "Not Applicable", IsDeleted = false, CreatedDate = DateTime.UtcNow }
             };
                await context.StrengthConsiderationDetail.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddGenderConsiderationDetail(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<GenderConsiderationDetail> list = new List<GenderConsiderationDetail>
                {
                    new GenderConsiderationDetail { GenderConsiderationName = "50 % F - 50 % M Excellent", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationName = "40 % F - 60 % M Very Good", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationName = "30 % F - 70 % M Good", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationName = "25 % F - 75 % M Poor", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationName = "20 % F - 80 % M Poor", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationName = "10 % F - 90 % M Poor", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationName = "5 % F - 95 % M Poor", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationName = "0 % F - 100 % M Poor", IsDeleted = false, CreatedDate = DateTime.UtcNow }
                };
                await context.GenderConsiderationDetail.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddSecurityDetail(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<SecurityDetail> list = new List<SecurityDetail>
                {
                    new SecurityDetail { CreatedDate = DateTime.UtcNow, SecurityName = "Insecure", IsDeleted = false },
                    new SecurityDetail { CreatedDate = DateTime.UtcNow, SecurityName = "Partially Insecure", IsDeleted = false },
                    new SecurityDetail { CreatedDate = DateTime.UtcNow, SecurityName = "Secure (Green Area)", IsDeleted = false }
                };
                await context.SecurityDetail.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddSecurityConsiderationDetail(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<SecurityConsiderationDetail> list = new List<SecurityConsiderationDetail>
                {
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationName = "Project Staff Cannot Visit Project Site", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationName = "Beneficiaries cannot be reached", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationName = "Resources cannot be deployed", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationName = "Threat exit for future (Highly)", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationName = "Project staff access the are partially", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationName = "Bonfires can be reached partially", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationName = "Resources can be deployed partially", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationName = "Future Threats exits", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationName = "No barrier for staff to access the area", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow,  SecurityConsiderationName = "No obstacle for deploying Resources & office", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow,  SecurityConsiderationName = "Future Threats expected", IsDeleted = false }
                };
                await context.SecurityConsiderationDetail.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddCodeType(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<CodeType> list = new List<CodeType>
                {
                    new CodeType { IsDeleted = false, CreatedDate = DateTime.UtcNow, CodeTypeName = "Organizations" },
                    new CodeType { IsDeleted = false, CreatedDate = DateTime.UtcNow, CodeTypeName = "Suppliers" },
                    new CodeType { IsDeleted = false, CreatedDate = DateTime.UtcNow, CodeTypeName = "Repair Shops" },
                    new CodeType { IsDeleted = false, CreatedDate = DateTime.UtcNow, CodeTypeName = "Individual/Others" },
                    new CodeType { IsDeleted = false, CreatedDate = DateTime.UtcNow, CodeTypeName = "Locations/Stores" }
                };
                await context.CodeType.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddAccountFilterType(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<AccountFilterType> list = new List<AccountFilterType>
                {
                    new AccountFilterType { IsDeleted = false, CreatedDate = DateTime.UtcNow, AccountFilterTypeName = "Inventory Account" },
                    new AccountFilterType { IsDeleted = false, CreatedDate = DateTime.UtcNow, AccountFilterTypeName = "Salary Account" }
                };
                await context.AccountFilterType.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddProjectPhaseDetails(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<ProjectPhaseDetails> list = new List<ProjectPhaseDetails>
                {
                    new ProjectPhaseDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, ProjectPhase = "Data Entry" }

                };
                await context.ProjectPhaseDetails.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddStoreInventorySeedData(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<StoreInventory> list = new List<StoreInventory>
                {
                    new StoreInventory { AssetType= (int)InventoryMasterType.Consumables, InventoryCode= "C01", InventoryName="Transport", InventoryDescription ="Transport", IsDeleted= false, CreatedDate= DateTime.UtcNow },
                    new StoreInventory { AssetType= (int)InventoryMasterType.Expendables, InventoryCode= "E01", InventoryName="Transport", InventoryDescription ="Transport", IsDeleted= false, CreatedDate= DateTime.UtcNow },
                    new StoreInventory { AssetType= (int)InventoryMasterType.NonExpendables, InventoryCode= "N01", InventoryName="Transport", InventoryDescription ="Transport", IsDeleted= false, CreatedDate= DateTime.UtcNow }
                };
               
                await context.StoreInventories.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddStoreItemGroupSeedData(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<StoreItemGroup> list = new List<StoreItemGroup>
                {
                    new StoreItemGroup { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemGroupCode ="C01-01", ItemGroupName= "Vehicle", InventoryId =1},
                    new StoreItemGroup { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemGroupCode ="C01-02", ItemGroupName= "Generator", InventoryId =1},
                    new StoreItemGroup { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemGroupCode ="E01-01", ItemGroupName= "Vehicle", InventoryId =2},
                    new StoreItemGroup { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemGroupCode ="E01-02", ItemGroupName= "Generator", InventoryId =2},
                    new StoreItemGroup { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemGroupCode ="N01-01", ItemGroupName= "Vehicle", InventoryId =3},
                    new StoreItemGroup { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemGroupCode ="N01-02", ItemGroupName= "Generator", InventoryId =3},
                };
                await context.StoreItemGroups.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static async Task AddStoreItemSeedData(HumanitarianAssistanceDbContext context)
        {
            try
            {
                List<StoreInventoryItem> list = new List<StoreInventoryItem>
                {
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 1, ItemGroupId=1, ItemCode ="C01-01-01", ItemName= "Vehicle Fuel"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 1, ItemGroupId=2, ItemCode ="C01-02-01", ItemName= "Generator Fuel"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 1, ItemGroupId=1, ItemCode ="C01-01-02", ItemName= "Vehicle Mobil Oil"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 1, ItemGroupId=2, ItemCode ="C01-02-02", ItemName= "Generator Mobil Oil"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 1, ItemGroupId=1, ItemCode ="C01-01-03", ItemName= "Vehicle Maintenance Service"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 1, ItemGroupId=2, ItemCode ="C01-02-03", ItemName= "Generator Maintenance Service"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 2, ItemGroupId=3, ItemCode ="E01-01-01", ItemName= "Vehicle"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 2, ItemGroupId=4, ItemCode ="E01-02-01", ItemName= "Generator"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 2, ItemGroupId=3, ItemCode ="E01-01-02", ItemName= "Vehicle Spare Parts"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 2, ItemGroupId=4, ItemCode ="E01-02-02", ItemName= "Generator Spare Parts"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 3, ItemGroupId=5, ItemCode ="N01-01-01", ItemName= "Vehicle"},
                    new StoreInventoryItem { IsDeleted = false, CreatedDate = DateTime.UtcNow, ItemInventory= 3, ItemGroupId=6, ItemCode ="N01-02-01", ItemName= "Generator"},
                };

                await context.InventoryItems.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
