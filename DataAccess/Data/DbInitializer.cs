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
using DataAccess.DbEntities.Marketing;
using DataAccess.DbEntities.Project;

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

            //modelBuilder.Entity<OfficeDetail>().HasData(
            //     new OfficeDetail { OfficeId = 1, OfficeCode = "A0001", OfficeKey = "AF", OfficeName = "Afghanistan", IsDeleted = false }
            //);

            //OfficeDetail officeDetail = new OfficeDetail
            //{
            //    IsDeleted= false,
            //    CreatedDate= DateTime.Now,
            //    OfficeId = 1,
            //    OfficeCode = "A0001",
            //    OfficeKey = "AF",
            //    OfficeName = "Afghanistan",
            //};

            UserDetailOffices userDetailOffices = new UserDetailOffices();
            userDetailOffices.IsDeleted = false;
            userDetailOffices.CreatedDate = DateTime.Now;
            userDetailOffices.OfficeId = 1;
            userDetailOffices.UserId = userDetails.UserID;

            await context.UserDetailOffices.AddAsync(userDetailOffices);
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

        public static async Task AddContractClauses(ApplicationDbContext context)
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
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddJobGrades(ApplicationDbContext context)
        {
            try
            {
                List<JobGrade> list = new List<JobGrade>
                {
                    new JobGrade {IsDeleted= false, GradeName="Grade-1"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-2"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-3"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-4"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-5"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-6"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-7"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-8"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-9"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-10"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-11"},
                    new JobGrade {IsDeleted= false, GradeName="Grade-12"}
                };

                await context.JobGrade.AddRangeAsync(list);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddMarketingCategory(ApplicationDbContext context)
        {
            try
            {
                List<Category> list = new List<Category>
                {
                    new Category { IsDeleted = false, CategoryId = 1, CategoryName="Bank" },
                    new Category { IsDeleted = false, CategoryId = 2, CategoryName = "NGO" },
                    new Category { IsDeleted = false, CategoryId = 3, CategoryName = "Telecommunicaton" },
                    new Category { IsDeleted = false, CategoryId = 4, CategoryName = "Government" },
                    new Category { IsDeleted = false, CategoryId = 5, CategoryName = "Hospital" },
                    new Category { IsDeleted = false, CategoryId = 6, CategoryName = "Travel Agency" },
                    new Category { IsDeleted = false, CategoryId = 7, CategoryName = "University" },
                    new Category { IsDeleted = false, CategoryId = 8, CategoryName = "Media Groups" },
                    new Category { IsDeleted = false, CategoryId = 9, CategoryName = "Shops" },
                    new Category { IsDeleted = false, CategoryId = 10, CategoryName = "Energy" },
                    new Category { IsDeleted = false, CategoryId = 11, CategoryName = "School" },
                    new Category { IsDeleted = false, CategoryId = 12, CategoryName = "Construction" }
                };
                await context.Categories.AddRangeAsync(list);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }

        public static async Task AddActivityStatus(ApplicationDbContext context)
        {
            try
            {
                List<ActivityStatusDetail> list = new List<ActivityStatusDetail>
                {
                    new ActivityStatusDetail { IsDeleted = false, StatusId = 1, StatusName="Planning" },
                    new ActivityStatusDetail { IsDeleted = false, StatusId = 2, StatusName = "Implementation" },
                    new ActivityStatusDetail { IsDeleted = false, StatusId = 3, StatusName = "Completed" }
                };
                await context.ActivityStatusDetail.AddRangeAsync(list);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
            }
        }
    }
}
