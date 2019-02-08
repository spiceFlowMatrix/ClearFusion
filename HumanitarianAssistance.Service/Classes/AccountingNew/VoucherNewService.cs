using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Service.Classes.AccountingNew
{
    public class VoucherNewService : IVoucherNewService
    {

        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public VoucherNewService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }


        /// <summary>
        /// Get All Voucher List
        /// </summary>
        /// <param name="voucherNewFilterModel"></param>
        /// <returns>Vouchers List</returns>
        public async Task<APIResponse> GetAllNewVoucherList(VoucherNewFilterModel voucherNewFilterModel)
        {
            APIResponse response = new APIResponse();

            string voucherNoValue = null;
            string referenceNoValue = null;
            string descriptionValue = null;
            string journalNameValue = null;
            string dateValue = null;

            if (!string.IsNullOrEmpty(voucherNewFilterModel.FilterValue))
            {
                voucherNoValue = voucherNewFilterModel.VoucherNoFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                referenceNoValue = voucherNewFilterModel.ReferenceNoFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                descriptionValue = voucherNewFilterModel.DescriptionFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                journalNameValue = voucherNewFilterModel.JournalNameFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                dateValue = voucherNewFilterModel.DateFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
            }

            try
            {

                int totalCount = await _uow.GetDbContext().VoucherDetail
                                       .Where(v => v.IsDeleted == false &&
                                               !string.IsNullOrEmpty(voucherNewFilterModel.FilterValue) ? (
                                               v.VoucherNo.ToString().Trim().Contains(voucherNoValue) ||
                                               v.ReferenceNo.Trim().ToLower().Contains(referenceNoValue) ||
                                               v.Description.Trim().ToLower().Contains(descriptionValue) ||
                                               v.JournalDetails.JournalName.Trim().ToLower().Contains(journalNameValue) ||
                                               v.VoucherDate.ToString().Trim().Contains(dateValue)
                                               ) : true
                                       )
                                      .AsNoTracking()
                                      .CountAsync();

                var voucherList = await _uow.GetDbContext().VoucherDetail
                                      .Where(v => v.IsDeleted == false &&
                                                 !string.IsNullOrEmpty(voucherNewFilterModel.FilterValue) ? (
                                                   v.VoucherNo.ToString().Trim().Contains(voucherNoValue) ||
                                                   v.ReferenceNo.Trim().ToLower().Contains(referenceNoValue) ||
                                                   v.Description.Trim().ToLower().Contains(descriptionValue) ||
                                                   v.JournalDetails.JournalName.Trim().ToLower().Contains(journalNameValue) ||
                                                   v.VoucherDate.ToString().Trim().ToLower().Contains(dateValue)
                                                   ) : true
                                       )
                                      .OrderByDescending(x => x.CreatedDate)
                                      .Select(x => new VoucherDetailModel
                                      {
                                          VoucherNo = x.VoucherNo,
                                          CurrencyCode = x.CurrencyDetail.CurrencyCode,
                                          CurrencyId = x.CurrencyDetail.CurrencyId,
                                          VoucherDate = x.VoucherDate,
                                          ChequeNo = x.ChequeNo,
                                          ReferenceNo = x.ReferenceNo,
                                          Description = x.Description,
                                          JournalName = x.JournalDetails.JournalName,
                                          JournalCode = x.JournalDetails.JournalCode,
                                          VoucherTypeId = x.VoucherTypeId,
                                          OfficeId = x.OfficeId,
                                          ProjectId = x.ProjectId,
                                          BudgetLineId = x.BudgetLineId,
                                          OfficeName = x.OfficeDetails.OfficeName,
                                      })
                                      .Skip(voucherNewFilterModel.pageSize.Value * voucherNewFilterModel.pageIndex.Value)
                                      .Take(voucherNewFilterModel.pageSize.Value)
                                      .AsNoTracking()
                                      .ToListAsync();
                response.data.VoucherDetailList = voucherList;
                response.data.TotalCount = totalCount;
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

        /// <summary>
        /// Get Voucher Detail By VoucherNo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetVoucherDetailByVoucherNo(long id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var voucherDetail = await _uow.GetDbContext().VoucherDetail
                                              .Include(o => o.OfficeDetails).Include(j => j.JournalDetails)
                                              .Include(c => c.CurrencyDetail)
                                              .Include(f => f.FinancialYearDetails)
                                              .FirstOrDefaultAsync(v => v.IsDeleted == false && v.VoucherNo == id);

                if (voucherDetail != null)
                {
                    VoucherDetailModel obj = new VoucherDetailModel();

                    obj.VoucherNo = voucherDetail.VoucherNo;
                    obj.CurrencyCode = voucherDetail.CurrencyDetail?.CurrencyCode ?? null;
                    obj.CurrencyId = voucherDetail.CurrencyDetail?.CurrencyId ?? 0;
                    obj.VoucherDate = voucherDetail.VoucherDate;
                    obj.ChequeNo = voucherDetail.ChequeNo;
                    obj.ReferenceNo = voucherDetail.ReferenceNo;
                    obj.Description = voucherDetail.Description;
                    obj.JournalName = voucherDetail.JournalDetails?.JournalName ?? null;
                    obj.JournalCode = voucherDetail.JournalDetails?.JournalCode ?? null;
                    obj.VoucherTypeId = voucherDetail.VoucherTypeId;
                    obj.OfficeId = voucherDetail.OfficeId;
                    obj.ProjectId = voucherDetail.ProjectId;
                    obj.BudgetLineId = voucherDetail.BudgetLineId;
                    obj.OfficeName = voucherDetail.OfficeDetails?.OfficeName ?? null;
                    obj.FinancialYearId = voucherDetail.FinancialYearId;
                    obj.FinancialYearName = voucherDetail.FinancialYearDetails?.FinancialYearName ?? null;
                    obj.IsVoucherVerified = voucherDetail.IsVoucherVerified;

                    response.data.VoucherDetail = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.VoucherNotPresent;
                }


            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }


        /// <summary>
        /// Add Voucher
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddVoucherNewDetail(VoucherDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {

                var officeCode = _uow.GetDbContext().OfficeDetail.FirstOrDefault(o => o.OfficeId == model.OfficeId)?.OfficeCode; //use OfficeCode

                if (officeCode != null)
                {
                    var fincancialYear = _uow.GetDbContext().FinancialYearDetail.FirstOrDefault(o => o.IsDefault)?.FinancialYearId; //use OfficeCode
                    var currencyCode = _uow.GetDbContext().CurrencyDetails.FirstOrDefault(o => o.CurrencyId == model.CurrencyId)?.CurrencyCode;

                    // NOTE: Dont remove this as we will need journal details in response
                    var journaldetail = _uow.GetDbContext().JournalDetail.FirstOrDefault(o => o.JournalCode == model.JournalCode);

                    if (fincancialYear != null)
                    {
                        if (currencyCode != null)
                        {
                            VoucherDetail obj = _mapper.Map<VoucherDetail>(model);
                            obj.JournalCode = journaldetail != null ? journaldetail.JournalCode : model.JournalCode;
                            obj.FinancialYearId = fincancialYear;
                            obj.CreatedById = model.CreatedById;
                            obj.VoucherDate = DateTime.UtcNow;
                            obj.CreatedDate = DateTime.UtcNow;
                            obj.IsDeleted = false;
                            await _uow.VoucherDetailRepository.AddAsyn(obj);

                            // Pattern: Office Code - Currency Code - Month Number - Voucher Primary Key (sequential) - Year
                            obj.ReferenceNo = officeCode + "-" + currencyCode + "-" + DateTime.Now.Month + "-" + obj.VoucherNo + "-" + DateTime.Now.Year;

                            await _uow.VoucherDetailRepository.UpdateAsyn(obj);

                            //var user = await _uow.UserDetailsRepository.FindAsync(x => x.AspNetUserId == model.CreatedById);

                            //LoggerDetailsModel loggerObj = new LoggerDetailsModel();
                            //loggerObj.NotificationId = (int)Common.Enums.LoggerEnum.VoucherCreated;
                            //loggerObj.IsRead = false;
                            //loggerObj.UserName = user.FirstName + " " + user.LastName;
                            //loggerObj.UserId = model.CreatedById;
                            //loggerObj.LoggedDetail = "Voucher " + obj.ReferenceNo + " Created";
                            //loggerObj.CreatedDate = model.CreatedDate;

                            //response.LoggerDetailsModel = loggerObj;

                            response.data.VoucherDetailEntity = obj;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Success";
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.CurrencyNotFound;
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.defaultFinancialYearIsNotSet;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.officeCodeNotFound;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Edit Voucher
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditVoucherNewDetail(VoucherDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var voucherdetailInfo = await _uow.VoucherDetailRepository.FindAsync(c => c.VoucherNo == model.VoucherNo);
                if (voucherdetailInfo != null)
                {
                    var officekey = _uow.OfficeDetailRepository.FindAsync(o => o.OfficeId == model.OfficeId).Result.OfficeKey;
                    voucherdetailInfo.CurrencyId = model.CurrencyId;
                    voucherdetailInfo.OfficeId = model.OfficeId;
                    voucherdetailInfo.VoucherDate = model.VoucherDate;
                    voucherdetailInfo.ChequeNo = model.ChequeNo;
                    voucherdetailInfo.ReferenceNo = officekey + "-" + voucherdetailInfo.VoucherNo;
                    voucherdetailInfo.JournalCode = model.JournalCode;
                    voucherdetailInfo.FinancialYearId = model.FinancialYearId;
                    voucherdetailInfo.VoucherTypeId = model.VoucherTypeId;
                    voucherdetailInfo.Description = model.Description;
                    voucherdetailInfo.ModifiedById = model.ModifiedById;
                    voucherdetailInfo.ModifiedDate = model.ModifiedDate;
                    await _uow.VoucherDetailRepository.UpdateAsyn(voucherdetailInfo);

                    if (_uow.GetDbContext().VoucherTransactions.Any(x => x.VoucherNo == voucherdetailInfo.VoucherNo))
                    {
                        var voucherTransactions =
                            await _uow.VoucherTransactionsRepository.FindAllAsync(x =>
                                x.VoucherNo == voucherdetailInfo.VoucherNo);
                        foreach (var transaction in voucherTransactions)
                        {
                            transaction.TransactionDate = voucherdetailInfo.VoucherDate;
                        }

                        _uow.GetDbContext().VoucherTransactions.UpdateRange(voucherTransactions);
                        await _uow.SaveAsync();
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Get All Voucher Transaction List 
        /// </summary>
        /// <param name="VoucherNo"></param>
        /// <returns>List of Voucher Transactions</returns>
        public async Task<APIResponse> GetAllTransactionsByVoucherId(long VoucherNo)
        {
            APIResponse response = new APIResponse();

            try
            {

                List<VoucherTransactionsModel> voucherTransactionsList = await _uow.GetDbContext().VoucherTransactions
                                   .Where(x => x.IsDeleted == false && x.VoucherNo == VoucherNo)
                                   .Select(x => new VoucherTransactionsModel
                                   {
                                       AccountNo = x.ChartOfAccountNewId,
                                       Debit = (x.Debit != 0 && x.Debit != null) ? x.Debit : 0,
                                       Credit = (x.Credit != 0 && x.Credit != null) ? x.Credit : 0,
                                       BudgetLineId = x.BudgetLineId,
                                       ProjectId = x.ProjectId,
                                       Description = x.Description,
                                       TransactionId = x.TransactionId,
                                       VoucherNo = x.VoucherNo
                                   }).ToListAsync();

                response.data.VoucherTransactions = voucherTransactionsList;
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

        /// <summary>
        /// Edit Transaction Record on the basis of Transaction Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Success/Failure</returns>
        public async Task<APIResponse> EditTransactionDetail(VoucherTransactionsModel voucherTransactions, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                //get Voucher transaction as per transactionId
                VoucherTransactions transaction = await _uow.VoucherTransactionsRepository.FindAsync(c => c.TransactionId == voucherTransactions.TransactionId);

                if (transaction != null)
                {
                    transaction.ChartOfAccountNewId = voucherTransactions.AccountNo;
                    transaction.Debit = voucherTransactions.Debit;
                    transaction.Credit = voucherTransactions.Credit;
                    transaction.Description = voucherTransactions.Description;
                    transaction.BudgetLineId = voucherTransactions.BudgetLineId;
                    transaction.ProjectId = voucherTransactions.ProjectId;
                    transaction.ModifiedById = userId;
                    transaction.ModifiedDate = DateTime.Now;

                    //update transaction as per new updated values
                    await _uow.VoucherTransactionsRepository.UpdateAsyn(transaction);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Delete Transaction
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns>Success/failure</returns>
        public async Task<APIResponse> DeleteTransactionById(long transactionId)
        {
            APIResponse response = new APIResponse();

            try
            {
                //get Voucher transaction as per transactionId
                VoucherTransactions transaction = await _uow.VoucherTransactionsRepository.FindAsync(c => c.TransactionId == transactionId);

                if (transaction != null)
                {
                    transaction.IsDeleted = true;

                    //update transaction as per new updated values
                    await _uow.VoucherTransactionsRepository.UpdateAsyn(transaction);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Add Voucher Transaction
        /// </summary>
        /// <param name="voucherTransactions"></param>
        /// <param name="userId"></param>
        /// <returns>Success/Failure</returns>
        public async Task<APIResponse> AddTransactionDetail(List<VoucherTransactionsModel> voucherTransactionsList, string userId)
        {
            APIResponse response = new APIResponse();

            List<VoucherTransactions> transactionsList = new List<VoucherTransactions>();

            try
            {
                if (voucherTransactionsList.Any())
                {

                    var voucherDetail = await _uow.VoucherDetailRepository.FindAsync(x => x.IsDeleted == false && x.VoucherNo == voucherTransactionsList.FirstOrDefault().VoucherNo);

                    foreach (VoucherTransactionsModel voucherTransactions in voucherTransactionsList)
                    {
                        //new voucher transaction object
                        VoucherTransactions transaction = new VoucherTransactions();

                        transaction.ChartOfAccountNewId = voucherTransactions.AccountNo;
                        transaction.Debit = voucherTransactions.Debit;
                        transaction.Credit = voucherTransactions.Credit;
                        transaction.Description = voucherTransactions.Description;
                        transaction.BudgetLineId = voucherTransactions.BudgetLineId;
                        transaction.ProjectId = voucherTransactions.ProjectId;
                        transaction.CreatedById = userId;
                        transaction.CreatedDate = DateTime.Now;
                        transaction.IsDeleted = false;
                        transaction.VoucherNo = voucherTransactions.VoucherNo;
                        transaction.CurrencyId = voucherDetail.CurrencyId;
                        transaction.TransactionDate = voucherDetail.VoucherDate;

                        transactionsList.Add(transaction);
                    }

                    //Add transaction
                    await _uow.GetDbContext().VoucherTransactions.AddRangeAsync(transactionsList);
                    await _uow.SaveAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Add/Edit Voucher Transaction
        /// </summary>
        /// <param name="voucherTransactions"></param>
        /// <param name="userId"></param>
        /// <returns>Success/Failure</returns>
        public APIResponse AddEditTransactionList(AddEditTransactionModel voucherTransactions, string userId)
        {
            APIResponse response = new APIResponse();

            List<VoucherTransactions> transactionsListAdd = new List<VoucherTransactions>();
            List<VoucherTransactions> transactionsListEdit = new List<VoucherTransactions>();

            try
            {
                if (voucherTransactions.VoucherTransactions.Any())
                {

                    var editList = voucherTransactions.VoucherTransactions.Where(w => w.TransactionId != 0)
                                                          .Select(s => s.TransactionId);

                    var editTransactionList = _uow.GetDbContext().VoucherTransactions
                                                                 .Where(x => editList
                                                                            .Contains(x.TransactionId))
                                                                 .ToList();

                    var voucherDetail = _uow.VoucherDetailRepository.Find(x => x.IsDeleted == false && x.VoucherNo == voucherTransactions.VoucherNo);

                    if (voucherDetail != null)
                    {
                        foreach (VoucherTransactionsModel item in voucherTransactions.VoucherTransactions)
                        {
                            // Add
                            if (item.TransactionId == 0 && item.IsDeleted == false)
                            {
                                //new voucher transaction object
                                VoucherTransactions transaction = new VoucherTransactions();

                                transaction.ChartOfAccountNewId = item.AccountNo;
                                transaction.Debit = item.Debit;
                                transaction.Credit = item.Credit;
                                transaction.Description = item.Description;
                                transaction.BudgetLineId = item.BudgetLineId;
                                transaction.ProjectId = item.ProjectId;
                                transaction.CreatedById = userId;
                                transaction.CreatedDate = DateTime.Now;
                                transaction.IsDeleted = false;
                                transaction.VoucherNo = item.VoucherNo;
                                transaction.CurrencyId = voucherDetail.CurrencyId;
                                transaction.TransactionDate = voucherDetail.VoucherDate;

                                transactionsListAdd.Add(transaction);
                            }
                            // edit
                            else
                            {
                                VoucherTransactions transaction = editTransactionList.FirstOrDefault(x => x.TransactionId == item.TransactionId);

                                if (transaction != null)
                                {
                                    if (item.IsDeleted == false)
                                    {
                                        transaction.IsDeleted = false;
                                    }
                                    else
                                    {
                                        transaction.IsDeleted = true;
                                    }
                                    transaction.TransactionId = item.TransactionId;
                                    transaction.ChartOfAccountNewId = item.AccountNo;
                                    transaction.Debit = item.Debit;
                                    transaction.Credit = item.Credit;
                                    transaction.Description = item.Description;
                                    transaction.BudgetLineId = item.BudgetLineId;
                                    transaction.ProjectId = item.ProjectId;

                                    transaction.CurrencyId = voucherDetail.CurrencyId;
                                    transaction.TransactionDate = voucherDetail.VoucherDate;

                                    transaction.ModifiedById = userId;
                                    transaction.ModifiedDate = DateTime.Now;
                                    //transaction.VoucherNo = voucherTransactions.VoucherNo;

                                    transactionsListEdit.Add(transaction);
                                }
                            }
                        }

                        using (IDbContextTransaction tran = _uow.GetDbContext().Database.BeginTransaction())
                        {
                            try
                            {
                                _uow.GetDbContext().VoucherTransactions.AddRange(transactionsListAdd);
                                _uow.GetDbContext().VoucherTransactions.UpdateRange(transactionsListEdit);

                                _uow.GetDbContext().SaveChanges();
                                tran.Commit();
                            }

                            catch (Exception ex)
                            {
                                tran.Rollback();
                                response.StatusCode = StaticResource.failStatusCode;
                                response.Message = StaticResource.SomethingWrong + ex.Message;
                                return response;
                            }
                        }

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.VoucherNotPresent;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Verify Voucher and we will consider only verified vouchers for transactions
        /// </summary>
        /// <param name="voucherNo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<APIResponse> VerifyVoucher(long voucherNo, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var voucherDetail = await _uow.VoucherDetailRepository.FindAsync(x => x.VoucherNo == voucherNo);
                if (voucherDetail != null)
                {
                    voucherDetail.IsVoucherVerified = !voucherDetail.IsVoucherVerified;
                    voucherDetail.ModifiedById = userId;
                    voucherDetail.ModifiedDate = DateTime.Now;

                    await _uow.VoucherDetailRepository.UpdateAsyn(voucherDetail);

                    response.data.IsVoucherVerified = voucherDetail.IsVoucherVerified;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = voucherDetail.IsVoucherVerified ? StaticResource.VoucherVerified : StaticResource.VoucherUnVerified;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> CreateGainLossTransaction(ExchangeGainLossVoucherDetails model, string userId)
        {
            APIResponse response = new APIResponse();

            #region "Generate Voucher"
            VoucherDetailModel voucherModel = new VoucherDetailModel
            {
                VoucherNo = model.VoucherNo,
                CurrencyId = model.CurrencyId,
                Description = model.Description,
                JournalCode = model.JournalId,
                VoucherTypeId = model.VoucherType,
                OfficeId = model.OfficeId,
                ProjectId = model.ProjectId,
                BudgetLineId = model.BudgetLineId,
                IsExchangeGainLossVoucher = true
            };

            var responseVoucher = await AddVoucherNewDetail(voucherModel);

            #endregion

            #region "Generate Transaction"

            if (responseVoucher.StatusCode == 200)
            {
                List<VoucherTransactionsModel> transactions = new List<VoucherTransactionsModel>();

                // Credit
                transactions.Add(new VoucherTransactionsModel
                {
                    TransactionId = 0,
                    VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                    AccountNo = model.CreditAccount,
                    Debit = 0,
                    Credit = Math.Abs(model.Amount),
                    Description = "Gain-Loss-Voucher-Credit",
                    IsDeleted = false
                });

                // Debit
                transactions.Add(new VoucherTransactionsModel
                {
                    TransactionId = 0,
                    VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                    AccountNo = model.DebitAccount,
                    Debit = Math.Abs(model.Amount),
                    Credit = 0,
                    Description = "Gain-Loss-Voucher-Debit",
                    IsDeleted = false
                });

                AddEditTransactionModel transactionVoucherDetail = new AddEditTransactionModel
                {
                    VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                    VoucherTransactions = transactions
                };

                var responseTransaction = AddEditTransactionList(transactionVoucherDetail, userId);

                if (responseTransaction.StatusCode == 200)
                {
                    response.data.GainLossVoucherDetail = new GainLossVoucherList
                    {
                        VoucherId = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                        JournalName = responseVoucher.data.VoucherDetailEntity.JournalDetails != null ? responseVoucher.data.VoucherDetailEntity.JournalDetails.JournalName : "",
                        VoucherName = responseVoucher.data.VoucherDetailEntity.ReferenceNo,
                        VoucherDate = responseVoucher.data.VoucherDetailEntity.VoucherDate
                    };
                }
                else
                {
                    throw new Exception(responseTransaction.Message);
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            else
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = responseVoucher.Message;
            }

            #endregion

            return response;
        }
    }
}
