using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllTransactionsByVoucherIdQueryHandler : IRequestHandler<GetAllTransactionsByVoucherIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllTransactionsByVoucherIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllTransactionsByVoucherIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var query =  _dbContext.VoucherTransactions
                                   .Include(x => x.ProjectJobDetail)
                                   .Include(x=> x.ChartOfAccountDetail)
                                   .Include(x => x.ProjectDetail)
                                   .Include(x => x.ProjectBudgetLineDetail)
                                   .Where(x => x.IsDeleted == false && x.VoucherNo == request.VoucherNo)
                                   .Select(x => new VoucherTransactionsModel
                                   {
                                       AccountNo = x.ChartOfAccountNewId,
                                       Debit = (double)((x.Debit != 0 && x.Debit != null) ? Math.Round((decimal)x.Debit.Value, 2) : 0),
                                       Credit = (double)((x.Credit != 0 && x.Credit != null) ? Math.Round((decimal)x.Credit.Value, 2) : 0),
                                       Amount = (double)((x.Debit != 0 && x.Debit != null) ? Math.Round((decimal)x.Debit.Value,2 ) : Math.Round((decimal)x.Credit.Value, 2)),
                                       BudgetLineId = x.BudgetLineId,
                                       ProjectId = x.ProjectId,
                                       ProjectName= x.ProjectDetail != null ? x.ProjectDetail.ProjectName : "",
                                       BudgetLineName =(x.ProjectBudgetLineDetail != null ? (x.ProjectBudgetLineDetail.BudgetCode + "-"+x.ProjectBudgetLineDetail.BudgetName) :""),
                                       Description = x.Description,
                                       TransactionId = x.TransactionId,
                                       VoucherNo = x.VoucherNo,
                                       JobId = x.JobId,
                                       JobName = x.ProjectJobDetail!= null? x.ProjectJobDetail.ProjectJobName : "",
                                       Type= (x.Debit != 0 && x.Debit != null) ? "Debit" : "Credit",
                                       AccountCode =(x.ChartOfAccountDetail.ChartOfAccountNewCode +"-"+ x.ChartOfAccountDetail.AccountName)
                                   }).AsQueryable();

                int count = await query.CountAsync();
                var result = await query.Skip(request.PageIndex*request.PageSize).Take(request.PageSize).ToListAsync();

                response.data.VoucherTransactions = result;
                response.data.TotalCount = count;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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