using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Auth
{
  public class MinimumAgeRequirement : IAuthorizationRequirement
  {
    public int MinimumAge { get; private set; }

    public MinimumAgeRequirement(int minimumAge)
    {
      MinimumAge = minimumAge;
    }
  }

  public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
      if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth &&
                                 c.Issuer == "http://abc.com"))
      {
        // .NET 4.x -> return Task.FromResult(0);
        return Task.CompletedTask;
      }

      var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(
          c => c.Type == ClaimTypes.DateOfBirth && c.Issuer == "http://abc.com").Value);

      int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
      if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
      {
        calculatedAge--;
      }

      if (calculatedAge >= requirement.MinimumAge)
      {
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }
  }
}
