using HumanitarianAssistance.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace HumanitarianAssistance.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        private IMediator mediator;
        private UserManager<AppUser> userManager;
        protected IMediator _mediator => mediator ?? (mediator = HttpContext.RequestServices.GetService<IMediator>());
        protected UserManager<AppUser> _userManager => userManager ?? (userManager = HttpContext.RequestServices.GetService<UserManager<AppUser>>());
    }
}