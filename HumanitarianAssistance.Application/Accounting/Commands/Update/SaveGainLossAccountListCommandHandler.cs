using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Command;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class SaveGainLossAccountListCommandHandler : IRequestHandler<SaveGainLossAccountListCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public SaveGainLossAccountListCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(SaveGainLossAccountListCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request.AccountIds.Any())
                {
                    //Get all Accounts that are already saved
                    GainLossCalculatorConfiguration gainLossSelectedAccounts = await _dbContext.GainLossCalculatorConfiguration.FirstOrDefaultAsync(x => x.IsDeleted == false && x.UserId == request.UserId);

                    //If not saved then add accounts
                    if(gainLossSelectedAccounts == null)
                    {
                        gainLossSelectedAccounts = new GainLossCalculatorConfiguration
                        {
                            UserId= request.UserId,
                            IsDeleted = false,
                            CreatedDate= DateTime.UtcNow,
                            CreatedById= request.UserId,
                            SelectedAccounts = request.AccountIds.ToArray()
                        };

                        await _dbContext.GainLossCalculatorConfiguration.AddAsync(gainLossSelectedAccounts);
                    } // if saved then update accounts
                    else
                    {
                        gainLossSelectedAccounts.ModifiedDate= DateTime.UtcNow;
                        gainLossSelectedAccounts.ModifiedById= request.UserId;
                        gainLossSelectedAccounts.SelectedAccounts= request.AccountIds.ToArray();
                        _dbContext.GainLossCalculatorConfiguration.Update(gainLossSelectedAccounts);
                    }

                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "success";
                }
                else
                {
                    throw new Exception("No Account Selected");
                }
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = exception.Message;
            }

            return response;
        }
    }
}