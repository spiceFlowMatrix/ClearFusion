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
                    if(accountDetail.AccountLevelId == (int)AccountLevels.InputLevel)
                    {
                        if(await CheckTransactionExist(request.AccountId))
                        {
                            throw new Exception(StaticResource.DeleteAllTransactions);
                        }
                        else if(await CheckAccountAssignedToSalaryHeadOrPayrollHead(request.AccountId))
                        {
                            throw new Exception(StaticResource.AccountAssignedToSalaryHead);
                        }
                        else if(await CheckPensionDebitAccountExist(request.AccountId))
                        {
                            throw new Exception(StaticResource.AccountAssignedToPensionDebit);
                        }
                    }
                    else
                    {
                        if(await CheckChildAccountExist(request.AccountId))
                        {
                            throw new Exception(StaticResource.DeleteAllChildAccount);
                        }

                    }

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
                    response.Message = StaticResource.AccountNotFound;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<bool> CheckTransactionExist(long accountId)
        {
            return await _dbContext.VoucherTransactions.AnyAsync(x => x.ChartOfAccountNewId == accountId && x.IsDeleted == false);
        }

        public async Task<bool> CheckChildAccountExist(long accountId)
        {
            return await _dbContext.ChartOfAccountNew.AnyAsync(x => x.ChartOfAccountNewId != accountId && x.ParentID == accountId && x.IsDeleted == false);
        }

        public async Task<bool> CheckAccountAssignedToSalaryHeadOrPayrollHead(long accountId)
        {
            // Check for Parent Salary Head
            bool accountExist = await _dbContext.SalaryHeadDetails.AnyAsync(x => x.AccountNo == accountId && x.IsDeleted == false);

            if (!accountExist)
            {
                //Check for Employee Salary Head
                accountExist = await _dbContext.EmployeePayroll.AnyAsync(x => x.AccountNo == accountId && x.IsDeleted == false);

                if(!accountExist)
                {   
                    //Check for Parent Payroll Head
                    accountExist = await _dbContext.PayrollAccountHead.AnyAsync(x => x.AccountNo == accountId && x.IsDeleted == false);

                    if(!accountExist)
                    {
                        //Check for Employee Payroll Head
                        accountExist = await _dbContext.EmployeePayrollAccountHead.AnyAsync(x => x.AccountNo == accountId && x.IsDeleted == false);
                    }
                }
                
            }
            
            return accountExist;
        }

        public async Task<bool> CheckPensionDebitAccountExist(long accountId)
        {
            return await _dbContext.PensionDebitAccountMaster.AnyAsync(x => x.ChartOfAccountNewId == accountId && x.IsDeleted == false);
        }
    }
}