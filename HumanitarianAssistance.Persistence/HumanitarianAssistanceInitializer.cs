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
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 1, CategoryName="Bank" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 2, CategoryName = "NGO" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 3, CategoryName = "Telecommunicaton" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 4, CategoryName = "Government" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 5, CategoryName = "Hospital" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 6, CategoryName = "Travel Agency" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 7, CategoryName = "University" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 8, CategoryName = "Media Groups" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 9, CategoryName = "Shops" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 10, CategoryName = "Energy" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 11, CategoryName = "School" },
                    new Category { IsDeleted = false, CreatedDate = DateTime.UtcNow, CategoryId = 12, CategoryName = "Construction" }
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
                    new ActivityStatusDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusId = 1, StatusName="Planning" },
                    new ActivityStatusDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusId = 2, StatusName = "Implementation" },
                    new ActivityStatusDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusId = 3, StatusName = "Completed" }
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
                    new CurrencyDetails { CurrencyId = 1, CurrencyName = "Afghani", CurrencyCode = "AFN", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new CurrencyDetails { CurrencyId = 2, CurrencyName = "European Curency", CurrencyCode = "EUR", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new CurrencyDetails { CurrencyId = 3, CurrencyName = "Pakistani Rupees", CurrencyCode = "PKR", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new CurrencyDetails { CurrencyId = 4, CurrencyName = "US Dollars", CurrencyCode = "USD", IsDeleted = false, CreatedDate = DateTime.UtcNow }
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
                   new PayrollAccountHead { AccountNo = null, IsDeleted = false, PayrollHeadId = 1, Description = null, PayrollHeadName = "Net Salary", PayrollHeadTypeId = 3, TransactionTypeId = 1 , CreatedDate = DateTime.UtcNow},
                    new PayrollAccountHead { AccountNo = null, IsDeleted = false, PayrollHeadId = 2, Description = null, PayrollHeadName = "Advance Deduction", PayrollHeadTypeId = 2, TransactionTypeId = 1, CreatedDate = DateTime.UtcNow },
                    new PayrollAccountHead { AccountNo = null, IsDeleted = false, PayrollHeadId = 3, Description = null, PayrollHeadName = "Salary Tax", PayrollHeadTypeId = 2, TransactionTypeId = 1, CreatedDate = DateTime.UtcNow},
                    new PayrollAccountHead { AccountNo = null, IsDeleted = true, PayrollHeadId = 4, Description = null, PayrollHeadName = "Gross Salary", PayrollHeadTypeId = 3, TransactionTypeId = 2, CreatedDate = DateTime.UtcNow},
                    new PayrollAccountHead { AccountNo = null, IsDeleted = false, PayrollHeadId = 5, Description = null, PayrollHeadName = "Pension", PayrollHeadTypeId = 2, TransactionTypeId = 1 , CreatedDate = DateTime.UtcNow}
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
                    new OfficeDetail { OfficeId = 1, OfficeCode = "A0001", OfficeKey = "AF", OfficeName = "Afghanistan", IsDeleted = false, CreatedDate = DateTime.UtcNow }
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
                    new Department (){ DepartmentId = 1, DepartmentName = "Administration", OfficeId = 1, IsDeleted = false }
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
                    new SalaryHeadDetails { SalaryHeadId = 1, HeadName = "Tr Allowance", Description = "Tr Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails { SalaryHeadId = 2, HeadName = "Food Allowance", Description = "Food Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails { SalaryHeadId = 3, HeadName = "Fine Deduction", Description = "Fine Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                    new SalaryHeadDetails { SalaryHeadId = 4, HeadName = "Capacity Building Deduction", Description = "Capacity Building Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                    new SalaryHeadDetails { SalaryHeadId = 5, HeadName = "Security Deduction", Description = "Security Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                    new SalaryHeadDetails { SalaryHeadId = 6, HeadName = "Other Allowance", Description = "Other Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails { SalaryHeadId = 7, HeadName = "Other Deduction", Description = "Other Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                    new SalaryHeadDetails { SalaryHeadId = 8, HeadName = "Medical Allowance", Description = "Medical Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails { SalaryHeadId = 9, HeadName = "Other1Allowance", Description = "Other1Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails { SalaryHeadId = 10, HeadName = "Other2Allowance", Description = "Other2Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                    new SalaryHeadDetails { SalaryHeadId = 11, HeadName = "Basic Pay (In hours)", Description = "Basic Pay (In hours)", HeadTypeId = 3, TransactionTypeId = 2, IsDeleted = false }
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
                    new LanguageDetail { IsDeleted = false, LanguageId = 1, LanguageName = "Arabic" },
                    new LanguageDetail { IsDeleted = false, LanguageId = 2, LanguageName = "Dari" },
                    new LanguageDetail { IsDeleted = false, LanguageId = 3, LanguageName = "English" },
                    new LanguageDetail { IsDeleted = false, LanguageId = 4, LanguageName = "French" },
                    new LanguageDetail { IsDeleted = false, LanguageId = 5, LanguageName = "German" },
                    new LanguageDetail { IsDeleted = false, LanguageId = 6, LanguageName = "Pashto" },
                    new LanguageDetail { IsDeleted = false, LanguageId = 7, LanguageName = "Russian" },
                    new LanguageDetail { IsDeleted = false, LanguageId = 8, LanguageName = "Turkish" },
                    new LanguageDetail { IsDeleted = false, LanguageId = 9, LanguageName = "Turkmani" },
                    new LanguageDetail { IsDeleted = false, LanguageId = 10, LanguageName = "Urdu" },
                    new LanguageDetail { IsDeleted = false, LanguageId = 11, LanguageName = "Uzbek" }
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
                    new LeaveReasonDetail { IsDeleted = false, LeaveReasonId = 1, ReasonName = "Casual Leave", Unit = 12 },
                    new LeaveReasonDetail { IsDeleted = false, LeaveReasonId = 2, ReasonName = "Emergency Leave", Unit = 6 },
                    new LeaveReasonDetail { IsDeleted = false, LeaveReasonId = 3, ReasonName = "Maternity Leave", Unit = 90 }
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
                    new ActivityType { IsDeleted = false, ActivityTypeId = 1, ActivityName = "Broadcasting" },
                    new ActivityType { IsDeleted = false, ActivityTypeId = 2, ActivityName = "Production" }
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
                    new FinancialYearDetail { IsDeleted = false, FinancialYearId = 1, StartDate = new DateTime(DateTime.Now.Year, 1, 1), EndDate = new DateTime(DateTime.Now.Year, 12, 31), FinancialYearName = DateTime.Now.Year + " Financial Year", IsDefault = true }
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
                    new CountryDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, CountryName = "Afghanistan" },
                    new CountryDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, CountryName = "United States" }
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
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 1, ProvinceName = "Badghis" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 2, ProvinceName = "Baghlan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 3, ProvinceName = "Balkh" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 4, ProvinceName = "Bamyan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 5, ProvinceName = "Daykundi" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 6, ProvinceName = "Farah" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 7, ProvinceName = "Faryab" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 8, ProvinceName = "Ghazni" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 9, ProvinceName = "Ghor" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 10, ProvinceName = "Helmand" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 11, ProvinceName = "Herat" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 12, ProvinceName = "Jowzjan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 13, ProvinceName = "Kabul" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 14, ProvinceName = "Kandahar" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 15, ProvinceName = "Kapisa" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 16, ProvinceName = "Khost" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 17, ProvinceName = "Kunar" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 18, ProvinceName = "Kunduz" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 19, ProvinceName = "Laghman" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 20, ProvinceName = "Logar" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 21, ProvinceName = "Maidan Wardak" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 22, ProvinceName = "Nangarhar" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 23, ProvinceName = "Nimruz" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 24, ProvinceName = "Nuristan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 25, ProvinceName = "Paktia" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 26, ProvinceName = "Paktika" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 27, ProvinceName = "Panjshir" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 28, ProvinceName = "Parwan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 29, ProvinceName = "Samangan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 30, ProvinceName = "Sar-e Pol" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 31, ProvinceName = "Takhar" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 32, ProvinceName = "Urozgan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 33, ProvinceName = "Zabul" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 1, ProvinceId = 34, ProvinceName = "Alabama" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 35, ProvinceName = "Alaska" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 36, ProvinceName = "Arizona" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 37, ProvinceName = "Arkansas" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 38, ProvinceName = "California" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 39, ProvinceName = "Colorado" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 40, ProvinceName = "Connecticut" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 41, ProvinceName = "Delaware" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 42, ProvinceName = "Florida" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 43, ProvinceName = "Georgia" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 44, ProvinceName = "Hawaii" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 45, ProvinceName = "Idaho" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 46, ProvinceName = "Illinois" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 47, ProvinceName = "Indiana" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 48, ProvinceName = "Iowa" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 49, ProvinceName = "Kansas" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 50, ProvinceName = "Kentucky" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 51, ProvinceName = "Louisiana" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 52, ProvinceName = "Maine" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 53, ProvinceName = "Maryland" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 54, ProvinceName = "Massachusetts" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 55, ProvinceName = "Michigan" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 56, ProvinceName = "Minnesota" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 57, ProvinceName = "Mississippi" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 58, ProvinceName = "Missouri" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 59, ProvinceName = "Montana" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 60, ProvinceName = "Nebraska" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 61, ProvinceName = "Nevada" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 62, ProvinceName = "New Hampshire" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 63, ProvinceName = "New Jersey" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 64, ProvinceName = "New Mexico" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 65, ProvinceName = "New York" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 66, ProvinceName = "North Carolina" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 67, ProvinceName = "North Dakota" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 68, ProvinceName = "Ohio" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 69, ProvinceName = "Oklahoma" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 70, ProvinceName = "Oregon" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 71, ProvinceName = "Pennsylvania" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 72, ProvinceName = "Rhode Island" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 73, ProvinceName = "South Carolina" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 74, ProvinceName = "South Dakota" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 75, ProvinceName = "Tennessee" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 76, ProvinceName = "Texas" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 77, ProvinceName = "Utah" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 78, ProvinceName = "Vermont" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 79, ProvinceName = "Virginia" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 80, ProvinceName = "Washington" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 81, ProvinceName = "West Virginia" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 82, ProvinceName = "Wisconsin" },
                    new ProvinceDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, CountryId = 2, ProvinceId = 83, ProvinceName = "Wyoming" }
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
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 1, District = "Jawand", ProvinceID = 1 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 2, District = "Muqur", ProvinceID = 1 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 3, District = "Qadis", ProvinceID = 1 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 4, District = "Baghlani Jadid", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 5, District = "Dahana i Ghuri", ProvinceID = 2 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 6, District = "Chahar Bolak", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 7, District = "Chahar Kint", ProvinceID = 3 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 8, District = "Panjab", ProvinceID = 4 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 9, District = "Shibar", ProvinceID = 4 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 10, District = "Bamyan", ProvinceID = 4 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 11, District = "Gizab", ProvinceID = 5 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 12, District = "Bala Buluk", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 13, District = "Bakwa", ProvinceID = 6 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 14, District = "Andkhoy", ProvinceID = 7 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 15, District = "Almar", ProvinceID = 7 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 16, District = "Bilchiragh", ProvinceID = 7 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 17, District = "Ajristan", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 18, District = "Andar", ProvinceID = 8 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 19, District = "Shahrak", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 20, District = "Tulak", ProvinceID = 9 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 21, District = "Baghran", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 22, District = "Garmsir", ProvinceID = 10 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 23, District = "Chishti Sharif", ProvinceID = 11 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 24, District = "Aqcha", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 25, District = "Fayzabad", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 26, District = "GuzDarzabara", ProvinceID = 12 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 27, District = "Chahar Asyab", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 28, District = "Deh Sabz", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 29, District = "Bagrami", ProvinceID = 13 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 30, District = "Daman", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 31, District = "Ghorak", ProvinceID = 14 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 32, District = "Alasay", ProvinceID = 15 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 33, District = "Bak", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 34, District = "Gurbuz", ProvinceID = 16 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 35, District = "Asadabad", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 36, District = "Bar Kunar", ProvinceID = 17 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 37, District = "Ali Abad", ProvinceID = 18 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 38, District = "Archi", ProvinceID = 18 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 39, District = "Alingar", ProvinceID = 19 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 40, District = "Alishing", ProvinceID = 19 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 41, District = "Baraki Barak", ProvinceID = 20 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 42, District = "Charkh", ProvinceID = 20 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 43, District = "Maidan Wardak", ProvinceID = 21 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 44, District = "Achin", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 45, District = "Bati Kot", ProvinceID = 22 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 46, District = "Kang", ProvinceID = 23 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 47, District = "Chakhansur", ProvinceID = 23 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 48, District = "Kamdesh", ProvinceID = 24 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 49, District = "Mandol", ProvinceID = 24 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 50, District = "Gardez", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 51, District = "Jaji", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 52, District = "Zurmat", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 53, District = "Wuza Zadran", ProvinceID = 25 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 54, District = "Dila", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 55, District = "Barmal", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 56, District = "Kal", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 57, District = "Chang", ProvinceID = 26 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 58, District = "Anaba", ProvinceID = 27 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 59, District = "Bagram", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 60, District = "Chaharikar", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 61, District = "Jabal Saraj", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 62, District = "Kohi Safi", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 63, District = "Salang", ProvinceID = 28 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 64, District = "Aybak", ProvinceID = 29 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 65, District = "Balkhab", ProvinceID = 30 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 66, District = "Bangi", ProvinceID = 31 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 67, District = "Uakhar", ProvinceID = 32 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 68, District = "Argahandab", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 69, District = "Atghar", ProvinceID = 33 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 70, District = "Alabama", ProvinceID = 34 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 71, District = "Arizona", ProvinceID = 35 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 72, District = "Jurors", ProvinceID = 35 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 73, District = "Arona", ProvinceID = 35 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 74, District = "Arkansas", ProvinceID = 36 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 75, District = "California", ProvinceID = 37 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 76, District = "Califor", ProvinceID = 37 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 77, District = "Colorado", ProvinceID = 38 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 78, District = "Connecticut", ProvinceID = 39 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 79, District = "Aelaware", ProvinceID = 40 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 80, District = "Florida", ProvinceID = 41 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 81, District = "Georia", ProvinceID = 42 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 82, District = "Hawaii", ProvinceID = 43 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 83, District = "Idaho", ProvinceID = 44 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 84, District = "Illinois", ProvinceID = 45 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 85, District = "Indiana", ProvinceID = 46 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 86, District = "Undia", ProvinceID = 46 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 87, District = "Iowa", ProvinceID = 47 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 88, District = "Lansa", ProvinceID = 48 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 89, District = "Kentucky", ProvinceID = 49 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 90, District = "Louisiana", ProvinceID = 50 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 91, District = "Maine", ProvinceID = 51 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 92, District = "Maryland", ProvinceID = 52 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 93, District = "Massachusetts", ProvinceID = 53 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 94, District = "Michigan", ProvinceID = 54 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 95, District = "Minnesota", ProvinceID = 55 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 96, District = "Mississippi", ProvinceID = 56 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 97, District = "Missouri", ProvinceID = 57 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 98, District = "Montana", ProvinceID = 58 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 99, District = "Nebraska", ProvinceID = 59 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 100, District = "Yevada", ProvinceID = 60 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 101, District = "New Hampshire", ProvinceID = 61 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 102, District = "New Jersey", ProvinceID = 62 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 103, District = "New Mexico", ProvinceID = 63 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 104, District = "New York", ProvinceID = 64 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 105, District = "North Carolina", ProvinceID = 65 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 106, District = "North Dakota", ProvinceID = 66 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 107, District = "Ohio", ProvinceID = 67 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 108, District = "Oklahoma", ProvinceID = 68 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 109, District = "Tregon", ProvinceID = 69 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 110, District = "Pennsylvania", ProvinceID = 70 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 111, District = "Rhode Island", ProvinceID = 71 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 112, District = "South Carolina", ProvinceID = 72 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 113, District = "South Dakota", ProvinceID = 73 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 114, District = "Tennessee", ProvinceID = 74 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 115, District = "Texas", ProvinceID = 75 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 116, District = "Wtaha", ProvinceID = 76 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 117, District = "Oermont", ProvinceID = 77 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 118, District = "Virginia", ProvinceID = 78 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 119, District = "Washinn", ProvinceID = 79 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 120, District = "West Virginia", ProvinceID = 80 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 121, District = "Nouit Vinia", ProvinceID = 80 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 122, District = "Wisconsin", ProvinceID = 81 },
                    new DistrictDetail { IsDeleted = false, CreatedDate = DateTime.UtcNow, DistrictID = 123, District = "Wyoming", ProvinceID = 82 }
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
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeId = 1, ReceiptTypeName = "Purchased" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeId = 2, ReceiptTypeName = "Transfers" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeId = 3, ReceiptTypeName = "Donation" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeId = 4, ReceiptTypeName = "Take Over" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeId = 5, ReceiptTypeName = "Loan" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeId = 6, ReceiptTypeName = "Return" },
                    new ReceiptType { IsDeleted = false, CreatedDate = DateTime.UtcNow, ReceiptTypeId = 7, ReceiptTypeName = "Other" }
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
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusAtTimeOfIssueId = 1, StatusName = "New" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusAtTimeOfIssueId = 2, StatusName = "Useable" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusAtTimeOfIssueId = 3, StatusName = "To Repair" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusAtTimeOfIssueId = 4, StatusName = "Damage" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusAtTimeOfIssueId = 5, StatusName = "Sold" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusAtTimeOfIssueId = 6, StatusName = "Stolen" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusAtTimeOfIssueId = 7, StatusName = "Handover" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusAtTimeOfIssueId = 8, StatusName = "Demolished" },
                    new StatusAtTimeOfIssue { IsDeleted = false, CreatedDate = DateTime.UtcNow, StatusAtTimeOfIssueId = 9, StatusName = "Broken" }
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
                    new AccountHeadType { CreatedDate = DateTime.UtcNow, AccountHeadTypeId = 1, AccountHeadTypeName = "Assets", IsDeleted = false, IsCreditBalancetype = false },
                    new AccountHeadType { CreatedDate = DateTime.UtcNow, AccountHeadTypeId = 2, AccountHeadTypeName = "Liabilities", IsDeleted = false, IsCreditBalancetype = true },
                    new AccountHeadType { CreatedDate = DateTime.UtcNow, AccountHeadTypeId = 3, AccountHeadTypeName = "Donors Equity", IsDeleted = false, IsCreditBalancetype = true },
                    new AccountHeadType { CreatedDate = DateTime.UtcNow, AccountHeadTypeId = 4, AccountHeadTypeName = "Income", IsDeleted = false, IsCreditBalancetype = true },
                    new AccountHeadType { CreatedDate = DateTime.UtcNow, AccountHeadTypeId = 5, AccountHeadTypeName = "Expense", IsDeleted = false, IsCreditBalancetype = false }
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
                    new EmployeeContractType { EmployeeContractTypeId = 1, EmployeeContractTypeName = "Probationary" },
                    new EmployeeContractType { EmployeeContractTypeId = 2, EmployeeContractTypeName = "PartTime" },
                    new EmployeeContractType { EmployeeContractTypeId = 3, EmployeeContractTypeName = "Permanent" }
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
                    new EmailType { IsDeleted = false, EmailTypeId = 1, EmailTypeName = "General", CreatedDate = DateTime.UtcNow },
                    new EmailType { IsDeleted = false, EmailTypeId = 2, EmailTypeName = "Bidding Panel", CreatedDate = DateTime.UtcNow }
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
                    new VoucherType { VoucherTypeId = 1, VoucherTypeName = "Adjustment", CreatedDate = DateTime.UtcNow },
                    new VoucherType { VoucherTypeId = 2, VoucherTypeName = "Journal", CreatedDate = DateTime.UtcNow }
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
                    new EmployeeType { EmployeeTypeId = 1, EmployeeTypeName = "Prospective", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new EmployeeType { EmployeeTypeId = 2, EmployeeTypeName = "Active", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new EmployeeType { EmployeeTypeId = 3, EmployeeTypeName = "Terminated", IsDeleted = false, CreatedDate = DateTime.UtcNow }
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
                    new StrengthConsiderationDetail { StrengthConsiderationId = 1, StrengthConsiderationName = "Gender Friendly", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new StrengthConsiderationDetail { StrengthConsiderationId = 2, StrengthConsiderationName = "Not Gender Friendly", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new StrengthConsiderationDetail { StrengthConsiderationId = 3, StrengthConsiderationName = "Not Applicable", IsDeleted = false, CreatedDate = DateTime.UtcNow }
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
                    new GenderConsiderationDetail { GenderConsiderationId = 1, GenderConsiderationName = "50 % F - 50 % M Excellent", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationId = 2, GenderConsiderationName = "40 % F - 60 % M Very Good", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationId = 3, GenderConsiderationName = "30 % F - 70 % M Good", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationId = 4, GenderConsiderationName = "25 % F - 75 % M Poor", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationId = 5, GenderConsiderationName = "20 % F - 80 % M Poor", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationId = 6, GenderConsiderationName = "10 % F - 90 % M Poor", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationId = 7, GenderConsiderationName = "5 % F - 95 % M Poor", IsDeleted = false, CreatedDate = DateTime.UtcNow },
                    new GenderConsiderationDetail { GenderConsiderationId = 8, GenderConsiderationName = "0 % F - 100 % M Poor", IsDeleted = false, CreatedDate = DateTime.UtcNow }
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
                    new SecurityDetail { CreatedDate = DateTime.UtcNow, SecurityId = 1, SecurityName = "Insecure", IsDeleted = false },
                    new SecurityDetail { CreatedDate = DateTime.UtcNow, SecurityId = 2, SecurityName = "Partially Insecure", IsDeleted = false },
                    new SecurityDetail { CreatedDate = DateTime.UtcNow, SecurityId = 3, SecurityName = "Secure (Green Area)", IsDeleted = false }
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
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 1, SecurityConsiderationName = "Project Staff Cannot Visit Project Site", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 2, SecurityConsiderationName = "Beneficiaries cannot be reached", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 3, SecurityConsiderationName = "Resources cannot be deployed", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 4, SecurityConsiderationName = "Threat exit for future (Highly)", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 5, SecurityConsiderationName = "Project staff access the are partially", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 6, SecurityConsiderationName = "Bonfires can be reached partially", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 7, SecurityConsiderationName = "Resources can be deployed partially", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 8, SecurityConsiderationName = "Future Threats exits", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 9, SecurityConsiderationName = "No barrier for staff to access the area", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 10, SecurityConsiderationName = "No obstacle for deploying Resources & office", IsDeleted = false },
                    new SecurityConsiderationDetail { CreatedDate = DateTime.UtcNow, SecurityConsiderationId = 11, SecurityConsiderationName = "Future Threats expected", IsDeleted = false }
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
                    new CodeType { IsDeleted = false, CreatedDate = DateTime.UtcNow, CodeTypeId = 1, CodeTypeName = "Organizations" },
                    new CodeType { IsDeleted = false, CreatedDate = DateTime.UtcNow, CodeTypeId = 2, CodeTypeName = "Suppliers" },
                    new CodeType { IsDeleted = false, CreatedDate = DateTime.UtcNow, CodeTypeId = 3, CodeTypeName = "Repair Shops" },
                    new CodeType { IsDeleted = false, CreatedDate = DateTime.UtcNow, CodeTypeId = 4, CodeTypeName = "Individual/Others" },
                    new CodeType { IsDeleted = false, CreatedDate = DateTime.UtcNow, CodeTypeId = 5, CodeTypeName = "Locations/Stores" }
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
                    new AccountFilterType { IsDeleted = false, CreatedDate = DateTime.UtcNow, AccountFilterTypeId = 1, AccountFilterTypeName = "Inventory Account" },
                    new AccountFilterType { IsDeleted = false, CreatedDate = DateTime.UtcNow, AccountFilterTypeId = 2, AccountFilterTypeName = "Salary Account" }
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
                    new ProjectPhaseDetails { IsDeleted = false, CreatedDate = DateTime.UtcNow, ProjectPhaseDetailsId = 1, ProjectPhase = "Data Entry" }

                };
                await context.ProjectPhaseDetails.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
