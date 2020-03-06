using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class SaveTransactionListCommandHandler : IRequestHandler<SaveTransactionListCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public SaveTransactionListCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(SaveTransactionListCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                List<VoucherTransactions> transactions = await _dbContext.VoucherTransactions
                                                                         .Where(x => x.IsDeleted == false &&
                                                                         x.VoucherNo == request.VoucherNo)
                                                                         .ToListAsync();

                VoucherDetail voucher = await _dbContext.VoucherDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.VoucherNo == request.VoucherNo);

                if (transactions.Any())
                {
                    List<long> removedTransactionIds = transactions.Select(x => x.TransactionId).Except(request.TransactionList.Select(x => x.TransactionId)).ToList();
                    List<VoucherTransactions> removedVoucherDetails = transactions.Where(x => removedTransactionIds.Contains(x.TransactionId)).ToList();

                    if (removedVoucherDetails.Any())
                    {
                        removedVoucherDetails.ForEach(x => x.IsDeleted = true);
                        _dbContext.UpdateRange(removedVoucherDetails);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                if (request.TransactionList.Any(x => x.TransactionId == 0))
                {
                    List<TransactionModel> transactionsToAdd = request.TransactionList.Where(x => x.TransactionId == 0).ToList();
                    List<VoucherTransactions> transactionList = new List<VoucherTransactions>();

                    foreach (var item in transactionsToAdd)
                    {
                        VoucherTransactions transaction = new VoucherTransactions
                        {
                            CreatedById = request.CreatedById,
                            VoucherNo = request.VoucherNo,
                            ProjectId = item.ProjectId,
                            BudgetLineId = item.BudgetLineId,
                            JobId = item.JobId,
                            Credit = item.Credit,
                            Debit = item.Debit,
                            CurrencyId = voucher.CurrencyId,
                            ChartOfAccountNewId = item.AccountNo,
                            TransactionDate = DateTime.UtcNow,
                            Description = item.Description,
                            IsDeleted = false
                        };

                        transactionList.Add(transaction);
                    }

                    await _dbContext.VoucherTransactions.AddRangeAsync(transactionList);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    List<TransactionModel> transactionsToAdd = request.TransactionList.Where(x => x.TransactionId != 0).ToList();
                    List<VoucherTransactions> transactionList = await _dbContext.VoucherTransactions.Where(x => x.IsDeleted == false && transactionsToAdd.Select(y => y.TransactionId).Contains(x.TransactionId)).ToListAsync();

                    if (transactionList.Any())
                    {
                        foreach (var item in transactionsToAdd)
                        {
                            int index= transactionList.FindIndex(x=> x.TransactionId == item.TransactionId);

                            if(index != -1)
                            {
                                transactionList[index].ModifiedById = request.CreatedById;
                                transactionList[index].ModifiedDate = DateTime.UtcNow;
                                transactionList[index].VoucherNo = request.VoucherNo;
                                transactionList[index].ProjectId = item.ProjectId;
                                transactionList[index].BudgetLineId = item.BudgetLineId;
                                transactionList[index].JobId = item.JobId;
                                transactionList[index].Credit = item.Credit;
                                transactionList[index].Debit = item.Debit;
                                transactionList[index].CurrencyId = voucher.CurrencyId;
                                transactionList[index].ChartOfAccountNewId = item.AccountNo;
                                transactionList[index].TransactionDate = DateTime.UtcNow;
                                transactionList[index].Description = item.Description;
                            }
                        }

                        _dbContext.VoucherTransactions.UpdateRange(transactionList);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}