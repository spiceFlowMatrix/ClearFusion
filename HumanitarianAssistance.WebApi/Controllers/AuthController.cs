using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HumanitarianAssistance.ViewModels.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using DataAccess.DbEntities;
using HumanitarianAssistance.WebAPI.Auth;
using HumanitarianAssistance.WebAPI.Extensions;
using Microsoft.Extensions.Options;

namespace HumanitarianAssistance.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth/[Action]/")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]LoginViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                //return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password or Account is Inactive", ModelState));
            }
            //     var c1 = identity.Claims.Single(c => c.Type == "CompanyType").Value;
            // Serialize and return the response
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                CompanyTypeId = identity.Claims.Single(c => c.Type == "companyTypeId").Value,
                CompanyType = identity.Claims.Single(c => c.Type == "companytype").Value,
                CompanyId = identity.Claims.Single(c => c.Type == "companyid").Value,
                IsLicensedUser = identity.Claims.Single(c => c.Type == "islicensed").Value,
                IsLicensedExpired = identity.Claims.Single(c => c.Type == "islicensedexpired").Value,
                UserName = identity.Claims.Single(c => c.Type == "username").Value,
                auth_token = await _jwtFactory.GenerateEncodedToken(credentials.UserName, identity),
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
                role = identity.Claims.Single(c => c.Type == "rol").Value,
                //teamRole = identity.Claims.Single(c => c.Type == "TeamRol").Value
            };


            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            bool isAccountActive = true;
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // get the user to verifty
                var userToVerify = await _userManager.FindByNameAsync(userName);

                //get userrole
                var IsSuperAdminrole = _userManager.IsInRoleAsync(userToVerify, "SuperAdmin").Result;
                if (IsSuperAdminrole == false)
                {
                    //check if company account is active
                    //  isAccountActive = _IcompanyContactRepository.GetDetails(userToVerify.Id);
                }
                if (userToVerify != null)
                {
                    //check if user is deleted
                    if (userToVerify.IsDeleted != true && isAccountActive)
                    {
                        // check the credentials  
                        if (await _userManager.CheckPasswordAsync(userToVerify, password))
                        {
                            return await System.Threading.Tasks.Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
                        }
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await System.Threading.Tasks.Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
