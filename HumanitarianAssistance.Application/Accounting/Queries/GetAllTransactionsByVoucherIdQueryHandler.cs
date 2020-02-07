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
                List<VoucherTransactionsModel> voucherTransactionsList = await _dbContext.VoucherTransactions
                                   .Include(x => x.ProjectJobDetail)
                                   .Include(x=> x.ChartOfAccountDetail)
                                   .Include(x => x.ProjectDetail)
                                   .Include(x => x.ProjectBudgetLineDetail)
                                   .Where(x => x.IsDeleted == false && x.VoucherNo == request.VoucherId)
                                   .Select(x => new VoucherTransactionsModel
                                   {
                                       AccountNo = x.ChartOfAccountNewId,
                                       Debit = (x.Debit != 0 && x.Debit != null) ? x.Debit : 0,
                                       Credit = (x.Credit != 0 && x.Credit != null) ? x.Credit : 0,
                                       Amount = (x.Debit != 0 && x.Debit != null) ? x.Debit : x.Credit,
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
                                   }).ToListAsync();

                response.data.VoucherTransactions = voucherTransactionsList;
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