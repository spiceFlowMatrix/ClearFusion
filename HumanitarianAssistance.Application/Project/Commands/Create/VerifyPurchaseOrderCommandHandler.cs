using System;
using HumanitarianAssistance.Common.Enums;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.Accounting.Models;
using Microsoft.EntityFrameworkCore.Storage;
using HumanitarianAssistance.Application.Accounting.Commands.Common;
using HumanitarianAssistance.Application.CommonModels;
using System.Collections.Generic;
using System.Linq;
using HumanitarianAssistance.Domain.Entities.Store;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class VerifyPurchaseOrderCommandHandler : IRequestHandler<VerifyPurchaseOrderCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProjectServices _iProjectServices;
        private readonly IAccountingServices _iAccountingServices;

        public VerifyPurchaseOrderCommandHandler(HumanitarianAssistanceDbContext dbContext, IAccountingServices iAccountingServices, IProjectServices iProjectServices)
        {
            _dbContext = dbContext;
            _iAccountingServices = iAccountingServices;
        }

        public async Task<ApiResponse> Handle(VerifyPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            List<VoucherTransactionsModel> transactions = new List<VoucherTransactionsModel>();
            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var _logisticReq = await _dbContext.ProjectLogisticRequests
                    .Include(x=>x.ProjectBudgetLineDetail)
                    .FirstOrDefaultAsync(x=>x.IsDeleted== false && x.LogisticRequestsId == request.RequestId);
                    if(_logisticReq == null) {
                        throw new Exception("Request not found!");
                    }
                    AddVoucherDetailCommand model = new AddVoucherDetailCommand(){
                        ChequeNo = "",
                        CurrencyId= _logisticReq.CurrencyId,
                        Description= request.VoucherDescription,
                        JournalCode= request.Journal,
                        OfficeId= _logisticReq.OfficeId,
                        TimezoneOffset= -330,
                        VoucherDate= _logisticReq.PurchaseDate ?? throw new Exception("Purchase not submitted!"),
                        VoucherTypeId= 2
                    };
                    VoucherDetailEntityModel voucherDetail = await _iAccountingServices.AddVoucherDetail(model);
                    VoucherTransactionsModel creditTrans = new VoucherTransactionsModel{
                        AccountNo = request.CreditAccount,
                        BudgetLineId = _logisticReq.BudgetLineId,
                        Credit = request.TotalCost,
                        Debit = 0,
                        Description = request.CreditDescription,
                        IsDeleted = false,
                        JobId = _logisticReq.ProjectBudgetLineDetail.ProjectJobId,
                        ProjectId = _logisticReq.ProjectId,
                        TransactionId = 0,
                        VoucherNo = voucherDetail.VoucherNo
                    };
                    transactions.Add(creditTrans);
                    VoucherTransactionsModel debitTrans = new VoucherTransactionsModel{
                        AccountNo = request.DebitAccount,
                        BudgetLineId = _logisticReq.BudgetLineId,
                        Credit = 0,
                        Debit = request.TotalCost,
                        Description = request.DebitDescription,
                        IsDeleted = false,
                        JobId = _logisticReq.ProjectBudgetLineDetail.ProjectJobId,
                        ProjectId = _logisticReq.ProjectId,
                        TransactionId = 0,
                        VoucherNo = voucherDetail.VoucherNo
                    };
                    transactions.Add(debitTrans);
                    AddEditTransactionListCommand transList = new AddEditTransactionListCommand{
                        VoucherNo = voucherDetail.VoucherNo,
                        VoucherTransactions = transactions
                    };

                    await Task.Run(() =>
                    {
                        // Common Function to Add/Update Transaction
                        bool transactionAddedFlag = _iAccountingServices.AddEditTransactionList(transList);

                        if (!transactionAddedFlag)
                        {
                            throw new Exception(StaticResource.SomethingWentWrong);
                        }
                    });

                    var _logisticItems = await _dbContext.ProjectLogisticItems
                    .Include(x=>x.StoreInventoryItem)
                    .Where(x=>x.IsDeleted== false && x.LogisticRequestsId == request.RequestId).ToListAsync();
                    foreach (var item in _logisticItems)
                    {
                        StoreItemPurchase obj = new StoreItemPurchase{
                            CreatedDate = request.CreatedDate,
                            CreatedById = request.CreatedById,
                            IsDeleted = false,
                            InventoryItem = item.ItemId,
                            PurchaseDate = _logisticReq.PurchaseDate ?? throw new Exception("Purchase not submitted!"),
                            Currency = _logisticReq.CurrencyId,
                            UnitCost = Convert.ToDouble(item.FinalCost),
                            Quantity = (Convert.ToInt32(item.Quantity)),
                            VoucherId = voucherDetail.VoucherNo,
                            VoucherDate = voucherDetail.VoucherDate,
                            ProjectId = _logisticReq.ProjectId,
                            BudgetLineId = _logisticReq.BudgetLineId,
                            OfficeId = _logisticReq.OfficeId,
                            PurchaseName = "Purchased for Purchase Order",
                            PurchasedById = await _dbContext.UserDetails.Where(x=>x.IsDeleted == false && x.AspNetUserId == _logisticReq.ModifiedById).Select(x=>x.EmployeeId).FirstOrDefaultAsync() ?? 0,
                            ReceiptTypeId = 1,
                            UnitType = await _dbContext.PurchaseUnitType.Where(x=>x.IsDeleted == false && x.IsDefault == true).Select(x=>x.UnitTypeId).FirstOrDefaultAsync()
                        };
                        await _dbContext.StoreItemPurchases.AddAsync(obj);
                    }

                    _logisticReq.Status = (int)LogisticRequestStatus.PurchaseCompleted;
                    await _dbContext.SaveChangesAsync();
                    tran.Commit();     
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                catch(Exception ex) 
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = ex.Message;
                }
            }
            
            return response;
        }

    }
}