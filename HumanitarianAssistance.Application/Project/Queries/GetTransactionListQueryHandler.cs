using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetTransactionListQueryHandler : IRequestHandler<GetTransactionListQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetTransactionListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //var TransList = await _dbContext.VoucherTransactions
                //                          .Include(c => c.CurrencyDetails)
                //                          .Where(x => x.IsDeleted == false && x.VoucherDetails.CurrencyId == request.CurrencyId && x.BudgetLineId == request.BudgetLineId)
                //                          .OrderByDescending(x => x.Debit)
                //                          .ToListAsync();
                var TransList = await _dbContext.LoadStoredProc("get_transaction_list")
                                     .WithSqlParam("budgetline_id", request.BudgetLineId)
                                     .WithSqlParam("currency_id", request.CurrencyId)
                                     .ExecuteStoredProc<spTransactionBudgetModel>();  

                var budgetDetaillist = TransList.Select(b => new TransactionBudgetModel
                {

                    CurrencyId = b.CurrencyId ?? null,
                    CurrencyName = b.CurrencyName ?? null,
                    Credit = b.Credit,
                    Debit = b.Debit,
                    TransactionDate = b.TransactionDate,
                    UserName = request.UserName,
                    CreatedDate = b.CreatedDate
                }).ToList();

                response.data.TransactionBudgetModelList = budgetDetaillist;
                if (response.data.TransactionBudgetModelList.Count == 0)
                {
                    //response.Message = StaticResource.NoDataFound;
                    response.StatusCode = StaticResource.notFoundCode;

                    throw new Exception(StaticResource.NoDataFound);
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = response.StatusCode == StaticResource.notFoundCode ? response.StatusCode : StaticResource.failStatusCode;
                response.Message = response.StatusCode == StaticResource.notFoundCode ? StaticResource.NoDataFound : StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
