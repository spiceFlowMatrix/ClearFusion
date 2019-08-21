using MediatR;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using System.Linq;
using System;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class EditChartOfAccountCommandHandler : IRequestHandler<EditChartOfAccountCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public EditChartOfAccountCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditChartOfAccountCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    ChartOfAccountNew accountDetail = await _dbContext.ChartOfAccountNew.FirstOrDefaultAsync(x => x.ChartOfAccountNewId == request.ChartOfAccountNewId);

                    if (accountDetail != null)
                    {
                        accountDetail.AccountName = request.AccountName?.Trim();
                        accountDetail.AccountTypeId = request.AccountTypeId;
                        accountDetail.AccountFilterTypeId = request.AccountFilterTypeId;

                        accountDetail.ModifiedDate = request.ModifiedDate;
                        accountDetail.IsDeleted = false;

                        await _dbContext.SaveChangesAsync();

                        if (accountDetail.AccountLevelId == (int)AccountLevels.SubLevel)
                        {
                            bool inputLevelAccountExists = await _dbContext.ChartOfAccountNew.AnyAsync(x => x.IsDeleted == false && x.ParentID == accountDetail.ChartOfAccountNewId);

                            //Update only if input level account exists on sub level account
                            if (inputLevelAccountExists)
                            {
                                // Updated all input-level accounts' account types and balance types if true
                                await UpdateBalanceMetadataForInputAccounts(accountDetail);
                            }
                        }

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.NoDataFound;
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task UpdateBalanceMetadataForInputAccounts(ChartOfAccountNew subLvlAccount)
        {
            var accounts = await _dbContext.ChartOfAccountNew.Where(x => x.ParentID == subLvlAccount.ChartOfAccountNewId).ToListAsync();
            var accType = await _dbContext.AccountType.FirstOrDefaultAsync(x => x.AccountTypeId == subLvlAccount.AccountTypeId);
            if (accType != null) {
                var accHeadType = await _dbContext.AccountHeadType.FirstOrDefaultAsync(x => x.AccountHeadTypeId == accType.AccountHeadTypeId);

                subLvlAccount.IsCreditBalancetype = accHeadType.IsCreditBalancetype;
            }

            _dbContext.ChartOfAccountNew.Update(subLvlAccount);
            await _dbContext.SaveChangesAsync();

            foreach (var account in accounts)
            {
                account.IsCreditBalancetype = subLvlAccount.IsCreditBalancetype;
                account.AccountTypeId = subLvlAccount.AccountTypeId;
                account.AccountFilterTypeId = subLvlAccount.AccountFilterTypeId;
            }
            _dbContext.ChartOfAccountNew.UpdateRange(accounts);
            await _dbContext.SaveChangesAsync();
        }
    }
}