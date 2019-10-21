using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Command;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
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
                    List<GainLossSelectedAccounts> gainLossSelectedAccountsList = await _dbContext.GainLossSelectedAccounts.Where(x => x.IsDeleted == false).ToListAsync();

                    if (gainLossSelectedAccountsList.Any())
                    {

                        //Get List of Removed Accounts
                        List<GainLossSelectedAccounts> removedGainLossSelectedAccounts = gainLossSelectedAccountsList.Where(x => !request.AccountIds.Contains(x.ChartOfAccountNewId)).ToList();

                        if (removedGainLossSelectedAccounts.Any())
                        {
                            removedGainLossSelectedAccounts.ForEach(x => x.IsDeleted = true);

                            //Delete and update the table with the accounts already deleted
                            _dbContext.UpdateRange(removedGainLossSelectedAccounts);
                            await _dbContext.SaveChangesAsync();
                        }

                        //Get List of Accounts that are to be added
                        List<long> addGainLossSelectedAccounts = request.AccountIds.Where(x => !gainLossSelectedAccountsList.Select(y => y.ChartOfAccountNewId).Contains(x)).ToList();

                        gainLossSelectedAccountsList = new List<GainLossSelectedAccounts>();

                        foreach (long accountId in addGainLossSelectedAccounts)
                        {
                            GainLossSelectedAccounts gainLossSelectedAccounts = new GainLossSelectedAccounts
                            {
                                IsDeleted = false,
                                CreatedDate = DateTime.Now,
                                ChartOfAccountNewId = accountId,
                            };

                            gainLossSelectedAccountsList.Add(gainLossSelectedAccounts);

                        }
                    }
                    else //table is empty so it is safe to save all the accounts
                    {
                        gainLossSelectedAccountsList = new List<GainLossSelectedAccounts>();

                        foreach (long accountId in request.AccountIds)
                        {
                            GainLossSelectedAccounts gainLossSelectedAccounts = new GainLossSelectedAccounts
                            {
                                IsDeleted = false,
                                CreatedDate = DateTime.Now,
                                ChartOfAccountNewId = accountId,
                            };

                            gainLossSelectedAccountsList.Add(gainLossSelectedAccounts);
                        }
                    }

                    //Save Accounts to the DB
                    if (gainLossSelectedAccountsList.Any())
                    {
                       await _dbContext.GainLossSelectedAccounts.AddRangeAsync(gainLossSelectedAccountsList);
                       await _dbContext.SaveChangesAsync();
                    }

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