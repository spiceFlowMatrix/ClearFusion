using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Application.Accounting.Commands.Delete
{

    public class DeleteChartOfAccountCommandHandler : IRequestHandler<DeleteChartOfAccountCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public DeleteChartOfAccountCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteChartOfAccountCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ChartOfAccountNew accountDetail = await _dbContext.ChartOfAccountNew.FirstOrDefaultAsync(x => x.ChartOfAccountNewId == request.AccountId);
                if (accountDetail != null)
                {
                    if (accountDetail.AccountLevelId == (int)AccountLevels.InputLevel ? !await CheckTransactionExistOrNot(request.AccountId) : !await CheckChildAccountExistOrNot(request.AccountId))
                    {
                        accountDetail.IsDeleted = true;
                        accountDetail.ModifiedById = request.ModifiedById;
                        accountDetail.ModifiedDate = DateTime.UtcNow;

                        await _dbContext.SaveChangesAsync();

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {

                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = accountDetail.AccountLevelId == (int)AccountLevels.InputLevel ? StaticResource.DeleteAllTransactions : StaticResource.DeleteAllChildAccount;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.AccountNotFound;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<bool> CheckTransactionExistOrNot(long accountId)
        {
            bool transactionExist = await _dbContext.VoucherTransactions.AnyAsync(x => x.ChartOfAccountNewId == accountId && x.IsDeleted == false);
            if (transactionExist)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckChildAccountExistOrNot(long accountId)
        {
            bool childAccountExist = await _dbContext.ChartOfAccountNew.AnyAsync(x => x.ChartOfAccountNewId != accountId && x.ParentID == accountId && x.IsDeleted == false);
            if (childAccountExist)
            {
                return true;
            }
            return false;
        }

    }
}