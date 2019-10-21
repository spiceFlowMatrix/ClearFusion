using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{

    public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public ResetUserPasswordCommandHandler(HumanitarianAssistanceDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ApiResponse> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                AppUser existUser = await _userManager.FindByNameAsync(request.Username);
                string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

                IdentityResult passchangeResult = await _userManager.ResetPasswordAsync(existUser, token, request.NewPassword);

                if (passchangeResult.Succeeded == true)
                {
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}