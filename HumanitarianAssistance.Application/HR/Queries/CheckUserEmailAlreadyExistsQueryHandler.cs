using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class CheckUserEmailAlreadyExistsQueryHandler : IRequestHandler<CheckUserEmailAlreadyExistsQuery, bool>
    {
        HumanitarianAssistanceDbContext _dbContext;
        UserManager<AppUser> _userManager;

         public CheckUserEmailAlreadyExistsQueryHandler(HumanitarianAssistanceDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<bool> Handle(CheckUserEmailAlreadyExistsQuery request, CancellationToken cancellationToken)
        {
            bool emailAlreadyExists = false;

            
                AppUser existUser = await _userManager.FindByNameAsync(request.Email);

                if(existUser != null)
                {
                    emailAlreadyExists= true;
                }

            return emailAlreadyExists;
        }
    }
}