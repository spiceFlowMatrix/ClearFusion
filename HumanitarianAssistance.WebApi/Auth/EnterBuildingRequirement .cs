using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Auth
{
  public class EnterBuildingRequirement : IAuthorizationRequirement
  {


  }
  public class BadgeEntryHandler : AuthorizationHandler<EnterBuildingRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EnterBuildingRequirement requirement)
    {
      if (context.User.HasClaim(c => c.Type == "BadgeId" &&
                                     c.Issuer == "http://microsoftsecurity"))
      {
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }

  }
  public class HasTemporaryStickerHandler : AuthorizationHandler<EnterBuildingRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EnterBuildingRequirement requirement)
    {
      if (context.User.HasClaim(c => c.Type == "TemporaryBadgeId" &&
                                     c.Issuer == "https://microsoftsecurity"))
      {
        // We'd also check the expiration date on the sticker.
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }
  }

}
