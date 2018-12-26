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

            //string codeValue = null;
            //string journalValue = null;
            //string dateValue = null;
            //string nameValue = null;

            //if (!string.IsNullOrEmpty(voucherNewFilterModel.FilterValue))
            //{
            //    codeValue = voucherNewFilterModel.CodeFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
            //    nameValue = voucherNewFilterModel.NameFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
            //    dateValue = voucherNewFilterModel.DateFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
            //    journalValue = voucherNewFilterModel.JournalFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
            //}

            try
            {
                var voucherList = await Task.Run(() =>
                    _uow.GetDbContext().VoucherDetail
                                      .Include(o => o.OfficeDetails)
                                      .Include(j => j.JournalDetails)
                                      .Include(c => c.CurrencyDetail)
                                      .Include(f => f.FinancialYearDetails)
                                      .Where(v => v.IsDeleted == false)
                                      .OrderBy(x => x.VoucherDate).ToList()
                                      );

                //var voucherList = await Task.Run(() =>
                //    _uow.GetDbContext().VoucherDetail
                //                      .Include(o => o.OfficeDetails)
                //                      .Include(j => j.JournalDetails)
                //                      .Include(c => c.CurrencyDetail)
                //                      .Include(f => f.FinancialYearDetails)
                //                      .Where(v => v.IsDeleted == false &&
                //                               (
                //                               v.VoucherNo.ToString().Trim() == codeValue ||
                //                               v.ReferenceNo.ToString().Trim() == nameValue ||
                //                               v.VoucherDate.ToString().Trim() == dateValue ||
                //                               v.JournalDetails.JournalName.ToLower().Trim() == journalValue
                //                               )
                //                      )
                //                      .OrderBy(x => x.VoucherDate).ToList()
                //                      );
                var voucherdetaillist = voucherList.Select(v => new VoucherDetailModel
                {
                    VoucherNo = v.VoucherNo,
                    CurrencyCode = v.CurrencyDetail?.CurrencyCode ?? null,
                    CurrencyId = v.CurrencyDetail?.CurrencyId ?? 0,
                    VoucherDate = v.VoucherDate,
                    ChequeNo = v.ChequeNo,
                    ReferenceNo = v.ReferenceNo,
                    Description = v.Description,
                    JournalName = v.JournalDetails?.JournalName ?? null,
                    JournalCode = v.JournalDetails?.JournalCode ?? null,
                    VoucherTypeId = v.VoucherTypeId,
                    OfficeId = v.OfficeId,
                    ProjectId = v.ProjectId,
                    BudgetLineId = v.BudgetLineId,
                    OfficeName = v.OfficeDetails?.OfficeName ?? null,
                }).ToList();
                response.data.VoucherDetailList = voucherdetaillist.OrderByDescending(x => x.VoucherDate).ToList();
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

                    if (fincancialYear != null)
                    {
                        VoucherDetail obj = _mapper.Map<VoucherDetail>(model);
                        obj.FinancialYearId = fincancialYear;
                        obj.CreatedById = model.CreatedById;
                        obj.CreatedDate = DateTime.UtcNow;
                        obj.IsDeleted = false;
                        await _uow.VoucherDetailRepository.AddAsyn(obj);
                        await _uow.SaveAsync();

                        obj.ReferenceNo = officeCode + "-" + obj.VoucherNo;
                        await _uow.VoucherDetailRepository.UpdateAsyn(obj);

                        var user = await _uow.UserDetailsRepository.FindAsync(x => x.AspNetUserId == model.CreatedById);

                        LoggerDetailsModel loggerObj = new LoggerDetailsModel();
                        loggerObj.NotificationId = (int)Common.Enums.LoggerEnum.VoucherCreated;
                        loggerObj.IsRead = false;
                        loggerObj.UserName = user.FirstName + " " + user.LastName;
                        loggerObj.UserId = model.CreatedById;
                        loggerObj.LoggedDetail = "Voucher " + obj.ReferenceNo + " Created";
                        loggerObj.CreatedDate = model.CreatedDate;

                        response.LoggerDetailsModel = loggerObj;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Default Financial year is not set";
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Office Code Not Found";
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

                List<VoucherTransactionsModel> voucherTransactionsList= await _uow.GetDbContext().VoucherTransactions
                                   .Where(x => x.IsDeleted == false && x.VoucherNo == VoucherNo)
                                   .Select(x => new VoucherTransactionsModel
                                   {
                                       AccountNo= x.ChartOfAccountNewId,
                                       Debit= (x.Debit !=0 && x.Debit !=null)? x.Debit : 0,
                                       Credit = (x.Credit != 0 && x.Credit != null) ? x.Credit : 0,
                                       BudgetLineId= x.BudgetLineId,
                                       ProjectId= x.ProjectId,
                                       Description= x.Description,
                                       TransactionId= x.TransactionId
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
        /// <returns></returns>
        public async Task<APIResponse> EditTransactionDetail(VoucherTransactionsModel voucherTransactions)
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
        /// <returns></returns>
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

    }
}
