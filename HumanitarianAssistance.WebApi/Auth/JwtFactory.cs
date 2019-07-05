using DataAccess.DbEntities;

using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.WebApi.Extensions;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;
        // private readonly IUserDetails _user;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        //IUserDetails user)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            //_user = user;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var claims = new[]
         {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
             };
            identity.Claims.Append(new Claim(JwtRegisteredClaimNames.Sub, userName));
            identity.Claims.Append(new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()));
            identity.Claims.Append(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64));

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: identity.Claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        public class ClaimData
        {
            public Guid claimId { get; set; }
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            List<Claim> claimList = new List<Claim>();
            try
            {
                var data = _context.Users.Where(x => x.Id == id).Join(_context.UserRoles, p => p.Id, q => q.UserId, (p, q) => new { p, q }).Join
                     (_context.RoleClaims, m => m.q.RoleId, n => n.RoleId, (m, n) => new { m, n }).Join(_context.Permissions, k => k.n.ClaimValue, l => l.Id, (k, l) => new { k, l }).Select(c => new { ClaimType = c.k.n.ClaimType, ClaimValue = c.l.Name }).ToList();

                foreach (var i in data)
                {
                    claimList.Add(new Claim(i.ClaimType, i.ClaimValue));
                }
                var user = _userManager.FindByNameAsync(userName).Result;

                var roleList = _userManager.GetRolesAsync(user).Result;
                bool IsSuperAdmin = _userManager.IsInRoleAsync(user, "SuperAdmin").Result;
                if (!IsSuperAdmin)
                {
                    //UserInfo userData = _user.GetUserInfo(id);
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.CompanyType, ""));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.IsLicensed, ""));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.UserName, ""));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.CompanyId, ""));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.IsLicensedExpired, ""));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.CompanyTypeId, ""));
                    //claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.TeamRol, userData.RoleId.ToString()));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, roleList[0]));

                }
                else
                {
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.CompanyType, "SuperAdmin"));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.IsLicensed, ""));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.CompanyId, ""));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.UserName, user.FirstName));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.IsLicensedExpired, ""));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.CompanyTypeId, ""));
                    claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, "SuperAdmin"));
                    //claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.TeamRol,""));
                }

                claimList.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Id, id));
                return new ClaimsIdentity(new GenericIdentity(userName, "Token"), claimList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
