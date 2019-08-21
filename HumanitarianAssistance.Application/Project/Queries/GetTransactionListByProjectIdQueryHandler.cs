using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetTransactionListByProjectIdQueryHandler : IRequestHandler<GetTransactionListByProjectIdQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetTransactionListByProjectIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetTransactionListByProjectIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var TransList = await _dbContext.VoucherTransactions
                                          .Include(c => c.CurrencyDetails)
                                          .Where(x => x.IsDeleted == false && x.ProjectId == request.ProjectId && x.ProjectId != null)
                                          .OrderByDescending(x => x.Debit)
                                          .ToListAsync();

                var budgetDetaillist = TransList.Select(b => new TransactionBudgetModel
                {

                    CurrencyId = b.CurrencyDetails?.CurrencyId ?? null,
                    CurrencyName = b.CurrencyDetails?.CurrencyName ?? null,
                    Credit = b.Credit,
                    Debit = b.Debit,
                    TransactionDate = b.TransactionDate,
                    UserName = request.UserName
                }).ToList();

                response.data.TransactionBudgetModelList = budgetDetaillist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
