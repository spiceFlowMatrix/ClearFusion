using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Service.Classes
{
    public class VoucherDetailService : IVoucherDetail
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public VoucherDetailService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> GetAllVoucherDetails()
        {
            APIResponse response = new APIResponse();
            try
            {

                var voucherList = await Task.Run(() =>
                    _uow.GetDbContext().VoucherDetail
                                      .Include(o => o.OfficeDetails).Include(j => j.JournalDetails)
                                      .Include(c => c.CurrencyDetail)
                                      .Include(f=> f.FinancialYearDetails)
                                      .Where(v => v.IsDeleted == false).OrderBy(x => x.VoucherDate).ToList()
                                      );
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
                    FinancialYearId = v.FinancialYearId,
                    FinancialYearName = v.FinancialYearDetails?.FinancialYearName ?? null
                }).ToList();
                response.data.VoucherDetailList = voucherdetaillist;
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

        public async Task<APIResponse> GetAllVoucherType()
        {
            APIResponse response = new APIResponse();
            try
            {
                var vouchertypelist = (from v in await _uow.VoucherTypeRepository.GetAllAsyn()
                                       select new VoucherTypeModel
                                       {
                                           VoucherTypeId = v.VoucherTypeId,
                                           VoucherTypeName = v.VoucherTypeName
                                       }).ToList();
                response.data.VoucherTypeList = vouchertypelist;
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

        public async Task<APIResponse> AddVoucherDetail(VoucherDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var officekey = _uow.OfficeDetailRepository.FindAsync(o => o.OfficeId == model.OfficeId).Result.OfficeKey;
                VoucherDetail obj = _mapper.Map<VoucherDetail>(model);
                obj.CreatedById = model.CreatedById;
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;
                await _uow.VoucherDetailRepository.AddAsyn(obj);
                await _uow.SaveAsync();

                obj.ReferenceNo = officekey + "-" + obj.VoucherNo;
                await _uow.VoucherDetailRepository.UpdateAsyn(obj);

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

        public async Task<APIResponse> EditVoucherDetail(VoucherDetailModel model)
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

        public async Task<APIResponse> DeleteVoucherDetail(int VoucherNo, string ModifiedById)
        {
            APIResponse response = new APIResponse();
            try
            {
                var voucherdetailInfo = await Task.Run(() =>
                    _uow.GetDbContext().VoucherDetail
                                      .Include(x => x.VoucherTransactionDetails).Where(v => v.VoucherNo == VoucherNo).FirstOrDefault()
                                      );
                if (voucherdetailInfo != null)
                {
                    if (voucherdetailInfo.VoucherTransactionDetails.Count == 0)
                    {
                        voucherdetailInfo.IsDeleted = true;
                        voucherdetailInfo.ModifiedById = ModifiedById;
                        voucherdetailInfo.ModifiedDate = DateTime.UtcNow;
                        await _uow.VoucherDetailRepository.UpdateAsyn(voucherdetailInfo);
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.IdAlreadyUsedInOtherTable;
                        response.Message = "This voucher has been already use in transaction details.";
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

        public async Task<APIResponse> GetAllVoucherDocumentDetailByVoucherNo(int VoucherNo)
        {
            APIResponse response = new APIResponse();
            try
            {
                var documentlist = (from v in await _uow.VoucherDocumentDetailRepository.FindAllAsync(v => v.VoucherNo == VoucherNo)
                                    where v.IsDeleted == false
                                    select new VoucherDocumentDetailModel
                                    {
                                        DocumentGUID = v.DocumentGUID + v.Extension,
                                        DocumentName = v.DocumentName,
                                        //FilePath = Encoding.UTF8.GetString(v.FilePath)
                                    }).ToList();
                response.data.VoucherDocumentDetailList = documentlist;
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

        public async Task<APIResponse> AddVoucherDocumentDetail(VoucherDocumentDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                byte[] filepathBase64 = Encoding.UTF8.GetBytes(model.FilePath);
                string[] str = model.FilePath.Split(",");
                byte[] filepath = Convert.FromBase64String(str[1]);

                string ex = str[0].Split("/")[1].Split(";")[0];

                string guidname = Guid.NewGuid().ToString();
                //byte[] filepath = Encoding.UTF8.GetBytes(str[1]);
                string filename = guidname + "." + ex;

                File.WriteAllBytes(@"Documents\" + filename, filepath);

                //VoucherDocumentDetail obj = _mapper.Map<VoucherDocumentDetail>(model);
                VoucherDocumentDetail obj = new VoucherDocumentDetail();
                obj.DocumentGUID = guidname;
                //Doctype 1 for voucher document
                obj.DocumentType = 1;
                obj.Extension = "." + ex;
                obj.FilePath = null;
                obj.DocumentName = model.DocumentName;
                obj.DocumentDate = model.DocumentDate;
                obj.VoucherNo = model.VoucherNo;
                obj.CreatedById = model.CreatedById;
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;
                await _uow.VoucherDocumentDetailRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> DeleteVoucherDocumentDetail(int DocumentId, string ModifiedById)
        {
            APIResponse response = new APIResponse();
            try
            {
                var documentInfo = await _uow.VoucherDocumentDetailRepository.FindAsync(d => d.DocumentID == DocumentId);
                if (documentInfo != null)
                {
                    documentInfo.ModifiedById = ModifiedById;
                    documentInfo.ModifiedDate = DateTime.UtcNow;
                    documentInfo.IsDeleted = true;
                    await _uow.VoucherDocumentDetailRepository.UpdateAsyn(documentInfo);
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

        public async Task<APIResponse> GetJouranlVoucherDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var journalcodeList = await Task.Run(() =>
                  _uow.GetDbContext().JournalDetail.Include(e => e.VoucherDetails).ThenInclude(e => e.VoucherTransactionDetails).ThenInclude(e => e.CreditAccountDetails).ToList()
                    );
                //var journalcodeList1 = await Task.Run(() =>
                //  _uow.GetDbContext().JournalDetail.Include(e => e.VoucherDetails).ThenInclude(e => e.VoucherTransactionDetails).ThenInclude(e => e.DebitAccountDetails).ToList()
                //    );
                //var journalcodeList = _uow.GetDbContext().JournalDetail.Include(e => e.VoucherDetails).ThenInclude(e => e.VoucherTransactionDetails).ToList();

                List<JournalVoucherViewModel> listJournalView = new List<JournalVoucherViewModel>();
                foreach (var j in journalcodeList)
                {
                    foreach (var v in j.VoucherDetails)
                    {
                        foreach (var t in v.VoucherTransactionDetails)
                        {
                            JournalVoucherViewModel vModel = new JournalVoucherViewModel();
                            JournalVoucherViewModel vModel1 = new JournalVoucherViewModel();
                            vModel.JournalCode = j.JournalName;
                            vModel1.JournalCode = j.JournalName;

                            vModel.VoucherNo = v.ReferenceNo;
                            vModel1.VoucherNo = v.ReferenceNo;
                            //var tran=journalcodeList1.Where(x => x.JournalCode == j.JournalCode).
                            //    FirstOrDefault().
                            //    VoucherDetails.Where(x => x.VoucherNo == v.VoucherNo).
                            //    FirstOrDefault().VoucherTransactionDetails.Where(x => x.TransactionId == t.TransactionId).FirstOrDefault();
                            vModel.TransactionNo = v.ReferenceNo + "-" + t.TransactionId;
                            vModel.TransactionDate = t.TransactionDate.ToShortDateString();
                            vModel.Amount = t.Amount;
                            vModel.TransactionType = "Debit";
                            vModel.AccountCode = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == t.DebitAccount).Result.AccountName;
                            vModel1.TransactionNo = v.ReferenceNo + "-" + t.TransactionId;
                            vModel1.TransactionDate = t.TransactionDate.ToShortDateString();
                            vModel1.Amount = t.Amount;
                            vModel1.TransactionType = "Credit";
                            vModel1.AccountCode = t.CreditAccountDetails.AccountName;
                            listJournalView.Add(vModel);
                            listJournalView.Add(vModel1);

                        }



                    }


                }

                response.data.JournalVoucherViewList = listJournalView;
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

        public async Task<APIResponse> GetJouranlVoucherDetailsByCondition(int? CurrencyId = null, DateTime? fromdate = null, DateTime? todate = null, int? officeid = null, int? RecordType = null)
        {
            APIResponse response = new APIResponse();
            try
            {
                var journalcodeList = await Task.Run(() =>
                  _uow.GetDbContext().JournalDetail.Include(e => e.VoucherDetails).ThenInclude(e => e.VoucherTransactionDetails).ThenInclude(e => e.CreditAccountDetails).ToList()
                    );

                if (fromdate == null && todate == null)
                {
                    fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                    todate = DateTime.UtcNow;
                }
                var exchangeratelist = await _uow.ExchangeRateRepository.GetAllAsyn();
                double exchangerate = 0, amount = 0;
                List<JournalVoucherViewModel> listJournalView = new List<JournalVoucherViewModel>();
                foreach (var j in journalcodeList)
                {
                    foreach (var v in j.VoucherDetails)
                    {
                        if (RecordType == 1)
                        {
                            v.VoucherTransactionDetails = v.VoucherTransactionDetails.Where(x => x.TransactionDate >= fromdate && x.TransactionDate <= todate && x.CurrencyId == CurrencyId).ToList();
                        }
                        else
                        {
                            v.VoucherTransactionDetails = v.VoucherTransactionDetails.Where(x => x.TransactionDate >= fromdate && x.TransactionDate <= todate).ToList();
                        }
                        if (officeid != null)
                        {
                            v.VoucherTransactionDetails = v.VoucherTransactionDetails.Where(x => x.OfficeId == officeid).ToList();
                        }

                        if (RecordType == 1)
                        {
                            foreach (var t in v.VoucherTransactionDetails)
                            {
                                JournalVoucherViewModel vModel = new JournalVoucherViewModel();
                                JournalVoucherViewModel vModel1 = new JournalVoucherViewModel();
                                vModel.JournalCode = j.JournalName;
                                vModel1.JournalCode = j.JournalName;

                                vModel.VoucherNo = v.ReferenceNo;
                                vModel1.VoucherNo = v.ReferenceNo;
                                vModel.TransactionNo = v.ReferenceNo + "-" + t.TransactionId;
                                vModel.TransactionDate = t.TransactionDate.ToShortDateString();
                                vModel.Amount = t.Amount;
                                vModel.TransactionType = "Debit";
                                vModel.AccountCode = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == t.DebitAccount).Result.AccountName;
                                vModel1.TransactionNo = v.ReferenceNo + "-" + t.TransactionId;
                                vModel1.TransactionDate = t.TransactionDate.ToShortDateString();
                                vModel1.Amount = t.Amount;
                                vModel1.TransactionType = "Credit";
                                vModel1.AccountCode = t.CreditAccountDetails.AccountName;
                                listJournalView.Add(vModel);
                                listJournalView.Add(vModel1);
                            }
                        }
                        else
                        {
                            foreach (var t in v.VoucherTransactionDetails)
                            {
                                exchangerate = 0; amount = 0;
                                if (t.CurrencyId != CurrencyId)
                                {
                                    var list = exchangeratelist.Where(x => x.FromCurrency == t.CurrencyId && x.ToCurrency == CurrencyId && x.Date.Date == t.TransactionDate.Date).OrderByDescending(o => o.ExchangeRateId).FirstOrDefault();
                                    exchangerate = list?.Rate ?? 0;
                                    amount = t.Amount * exchangerate;
                                }
                                else
                                {
                                    amount = t.Amount;
                                }

                                JournalVoucherViewModel vModel = new JournalVoucherViewModel();
                                JournalVoucherViewModel vModel1 = new JournalVoucherViewModel();
                                vModel.JournalCode = j.JournalName;
                                vModel1.JournalCode = j.JournalName;

                                vModel.VoucherNo = v.ReferenceNo;
                                vModel1.VoucherNo = v.ReferenceNo;
                                vModel.TransactionNo = v.ReferenceNo + "-" + t.TransactionId;
                                vModel.TransactionDate = t.TransactionDate.ToShortDateString();
                                vModel.Amount = amount;
                                vModel.TransactionType = "Debit";
                                vModel.AccountCode = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == t.DebitAccount).Result.AccountName;
                                vModel1.TransactionNo = v.ReferenceNo + "-" + t.TransactionId;
                                vModel1.TransactionDate = t.TransactionDate.ToShortDateString();
                                vModel1.Amount = amount;
                                vModel1.TransactionType = "Credit";
                                vModel1.AccountCode = t.CreditAccountDetails.AccountName;
                                listJournalView.Add(vModel);
                                listJournalView.Add(vModel1);
                            }
                        }
                    }
                }

                response.data.JournalVoucherViewList = listJournalView;
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

        public async Task<APIResponse> GetAllAccountCode()
        {
            APIResponse response = new APIResponse();
            try
            {
                var accountcodelist = (from c in await _uow.ChartAccountDetailRepository.FindAllAsync(x => x.AccountLevelId == 4)
                                       where c.IsDeleted == false
                                       select new AccountDetailModel
                                       {
                                           AccountCode = c.AccountCode,
                                           AccountName = c.AccountName,
										   ChartOfAccountCode = c.ChartOfAccountCode
                                       }).ToList();
                response.data.AccountDetailList = accountcodelist;
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

        public async Task<APIResponse> GetAllVoucherTransactionDetailByBudgetLine(long projectId, long budgetLineId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var transactionlist = await _uow.GetDbContext().VoucherTransactionDetails
                    .Include(x => x.CreditAccountDetails)
                    .Include(x => x.DebitAccountDetails)
                    .Include(x => x.VoucherDetails)
                    .Include(x => x.VoucherDetails.ProjectBudgetLine)
                    .Where(x => x.VoucherDetails.ProjectBudgetLine.ProjectId == projectId && x.VoucherDetails.ProjectBudgetLine.BudgetLineId == budgetLineId && x.IsDeleted == false).ToListAsync();

                IList<VoucherTransactionModel> tranlist = new List<VoucherTransactionModel>();
                foreach (var debit in transactionlist)
                {
                    VoucherTransactionModel obj = new VoucherTransactionModel();
                    obj.TransactionId = debit.TransactionId;
                    obj.TransactionDate = debit.TransactionDate;
                    obj.VoucherNo = debit.VoucherNo;
                    obj.Description = debit.Description;
                    obj.AccountName = debit.DebitAccountDetails.AccountName;
                    obj.DebitAmount = debit.Amount;
                    obj.DebitAccount = debit.DebitAccount;
                    obj.Amount = debit.Amount;
                    tranlist.Add(obj);
                    VoucherTransactionModel obj1 = new VoucherTransactionModel();
                    obj1.TransactionId = debit.TransactionId;
                    obj1.TransactionDate = debit.TransactionDate;
                    obj1.VoucherNo = debit.VoucherNo;
                    obj1.Description = debit.Description;
                    obj1.AccountName = debit.CreditAccountDetails.AccountName;
                    obj1.CreditAmount = debit.Amount;
                    obj1.CreditAccount = debit.CreditAccount;
                    obj1.Amount = debit.Amount;
                    tranlist.Add(obj1);
                }




                //var vouchertransactionlist = (from t in await _uow.VoucherTransactionDetailsRepository.FindAllAsync(v => v.VoucherNo == VoucherNo)
                //							  where t.IsDeleted == false orderby t.TransactionDate ascending
                //							  select new VoucherTransactionModel
                //							  {
                //								  TransactionId = t.TransactionId,
                //								  DebitAccount = t.DebitAccount,
                //								  CreditAccount = t.CreditAccount,
                //								  Amount = t.Amount,
                //								  Description = t.Description,
                //								  TransactionDate = t.TransactionDate,
                //								  VoucherNo = t.VoucherNo
                //							  }).ToList();

                //var list= await PaginatedList<VoucherTransactionModel>.CreateAsync(tranlist.AsQueryable<VoucherTransactionModel>(), 1, 10);

                response.data.VoucherTransactionList = tranlist;
                //response.data.VoucherTransactionList = list;

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

        public async Task<APIResponse> GetAllVoucherTransactionDetailByVoucherNo(int VoucherNo)
        {
            APIResponse response = new APIResponse();
            try
            {
                var transactionlist = await _uow.GetDbContext().VoucherTransactionDetails.Include(x => x.CreditAccountDetails).Include(x => x.DebitAccountDetails).Where(x => x.VoucherNo == VoucherNo).ToListAsync();
                IList<VoucherTransactionModel> tranlist = new List<VoucherTransactionModel>();
                foreach (var debit in transactionlist)
                {
                    VoucherTransactionModel obj = new VoucherTransactionModel();
                    obj.TransactionId = debit.TransactionId;
                    obj.TransactionDate = debit.TransactionDate.ToLocalTime().Date;
                    obj.VoucherNo = debit.VoucherNo;
                    obj.Description = debit?.Description;
                    obj.AccountName = debit.DebitAccountDetails?.AccountName ?? null;
                    obj.DebitAmount = debit?.Amount;
                    obj.DebitAccount = debit?.DebitAccount;
                    obj.Amount = debit.Amount;
                    tranlist.Add(obj);
                    VoucherTransactionModel obj1 = new VoucherTransactionModel();
                    obj1.TransactionId = debit.TransactionId;
                    obj1.TransactionDate = debit.TransactionDate.ToLocalTime().Date;
                    obj1.VoucherNo = debit.VoucherNo;
                    obj1.Description = debit?.Description;
                    obj1.AccountName = debit.CreditAccountDetails?.AccountName ?? null;
                    obj1.CreditAmount = debit?.Amount;
                    obj1.CreditAccount = debit?.CreditAccount;
                    obj1.Amount = debit.Amount;
                    tranlist.Add(obj1);
                }




                //var vouchertransactionlist = (from t in await _uow.VoucherTransactionDetailsRepository.FindAllAsync(v => v.VoucherNo == VoucherNo)
                //							  where t.IsDeleted == false orderby t.TransactionDate ascending
                //							  select new VoucherTransactionModel
                //							  {
                //								  TransactionId = t.TransactionId,
                //								  DebitAccount = t.DebitAccount,
                //								  CreditAccount = t.CreditAccount,
                //								  Amount = t.Amount,
                //								  Description = t.Description,
                //								  TransactionDate = t.TransactionDate,
                //								  VoucherNo = t.VoucherNo
                //							  }).ToList();

                //var list= await PaginatedList<VoucherTransactionModel>.CreateAsync(tranlist.AsQueryable<VoucherTransactionModel>(), 1, 10);

                response.data.VoucherTransactionList = tranlist;
                //response.data.VoucherTransactionList = list;

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

        public async Task<APIResponse> AddVoucherTransactionDetail(VoucherTransactionModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var transactionDate = model.TransactionDate.ToLocalTime().Date;
                //var isexistExchangeRate = await _uow.ExchangeRateRepository.FindAsync(x => x.Date.Date == transactionDate);
                var isexistExchangeRate = await _uow.GetDbContext().ExchangeRates.FirstOrDefaultAsync(x => x.Date.Date == transactionDate.Date);
                if (isexistExchangeRate != null)
                {
                    VoucherTransactionDetails obj = _mapper.Map<VoucherTransactionDetails>(model);
                    obj.CreatedById = model.CreatedById;
                    obj.CreatedDate = DateTime.UtcNow;
                    obj.IsDeleted = false;
                    await _uow.VoucherTransactionDetailsRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Exchange Rate is not diffined for this date.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditVoucherTransactionDetail(VoucherTransactionModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var vouchertransactionInfo = await _uow.VoucherTransactionDetailsRepository.FindAsync(c => c.TransactionId == model.TransactionId);
                if (vouchertransactionInfo != null)
                {
                    vouchertransactionInfo.DebitAccount = model.DebitAccount;
                    vouchertransactionInfo.CreditAccount = model.CreditAccount;
                    vouchertransactionInfo.Amount = model.Amount;
                    vouchertransactionInfo.Description = model.Description;
                    vouchertransactionInfo.TransactionDate = model.TransactionDate;
                    vouchertransactionInfo.ModifiedById = model.ModifiedById;
                    vouchertransactionInfo.ModifiedDate = model.ModifiedDate;
                    await _uow.VoucherTransactionDetailsRepository.UpdateAsyn(vouchertransactionInfo);
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

        //public async Task<APIResponse> GetAllLedgerDetails()
        //{
        //    APIResponse response = new APIResponse();
        //    try
        //    {
        //        var ledgerList = await Task.Run(() =>
        //          _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).ToList()
        //            );

        //        List<LedgerModel> list = new List<LedgerModel>();


        //        foreach (var i in ledgerList)
        //        {
        //            LedgerModel ledgerModel = new LedgerModel();
        //            ledgerModel.Transactionlist = new List<Transaction>();
        //            ledgerModel.AccountCode = i.AccountCode;
        //            ledgerModel.ChartAccountName = i.AccountName;
        //            if (i.DebitAccountlist.Count > 0)
        //            {
        //                var currency = await Task.Run(() =>
        //                    _uow.GetDbContext().VoucherDetail.Include(e => e.CurrencyDetail).Where(v => v.VoucherNo == i.DebitAccountlist[0].VoucherNo).SingleOrDefault()
        //                );
        //                ledgerModel.CurrencyName = currency.CurrencyDetail.CurrencyName;
        //            }
        //            foreach (var debit in i.DebitAccountlist)
        //            {
        //                Transaction transaction = new Transaction();
        //                transaction.TransactionNo = debit.TransactionId;
        //                transaction.AccountName = debit.CreditAccountDetails.AccountName;
        //                transaction.TransactionDate = debit.TransactionDate;
        //                transaction.DebitAmount = debit.Amount;
        //                transaction.VoucherNo = debit.VoucherNo;
        //                transaction.Description = debit.Description;
        //                ledgerModel.Transactionlist.Add(transaction);
        //            }
        //            foreach (var credit in i.CreditAccountlist)
        //            {
        //                Transaction transaction = new Transaction();
        //                transaction.TransactionNo = credit.TransactionId;
        //                transaction.AccountName = credit.DebitAccountDetails.AccountName;
        //                transaction.TransactionDate = credit.TransactionDate;
        //                transaction.CreditAmount = credit.Amount;
        //                transaction.VoucherNo = credit.VoucherNo;
        //                transaction.Description = credit.Description;
        //                ledgerModel.Transactionlist.Add(transaction);
        //            }
        //            if (i.DebitAccountlist.Count > 0 || i.CreditAccountlist.Count > 0)
        //            {
        //                list.Add(ledgerModel);
        //            }
        //        }
        //        response.data.LedgerList = list;
        //        response.StatusCode = StaticResource.successStatusCode;
        //        response.Message = "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = StaticResource.failStatusCode;
        //        response.Message = StaticResource.SomethingWrong + ex.Message;
        //    }
        //    return response;
        //}

        public async Task<APIResponse> GetAllLedgerDetails()
        {

            //APIResponse response = new APIResponse();
            //try
            //{
            //    var ledgerList = await Task.Run(() =>
            //      _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).ToList()
            //        );

            //    List<LedgerModel> list = new List<LedgerModel>();
            //    foreach (var i in ledgerList)
            //    {
            //        LedgerModel ledgerModel = new LedgerModel();
            //        ledgerModel.Transactionlist = new List<Transaction>();
            //        ledgerModel.AccountCode = i.AccountCode;
            //        ledgerModel.ChartAccountName = i.AccountName;
            //        if (i.DebitAccountlist.Count > 0)
            //        {
            //            var currency = await Task.Run(() =>
            //                _uow.GetDbContext().VoucherDetail.Include(e => e.CurrencyDetail).Where(v => v.VoucherNo == i.DebitAccountlist[0].VoucherNo).SingleOrDefault()
            //            );
            //            ledgerModel.CurrencyName = currency.CurrencyDetail.CurrencyName;
            //        }

            //        foreach (var debit in i.DebitAccountlist)
            //        {
            //            Transaction transaction = new Transaction();
            //            transaction.TransactionNo = debit.TransactionId;
            //            transaction.AccountName = debit.CreditAccountDetails.AccountName;
            //            transaction.TransactionDate = debit.TransactionDate;
            //            transaction.DebitAmount = debit.Amount;
            //            transaction.VoucherNo = debit.VoucherNo;
            //            transaction.AccountType = "Debit";
            //            transaction.Description = debit.Description;
            //            ledgerModel.Transactionlist.Add(transaction);
            //        }
            //        foreach (var credit in i.CreditAccountlist)
            //        {
            //            Transaction transaction = new Transaction();
            //            transaction.TransactionNo = credit.TransactionId;
            //            transaction.AccountName = credit.DebitAccountDetails.AccountName;
            //            transaction.TransactionDate = credit.TransactionDate;
            //            transaction.CreditAmount = credit.Amount;
            //            transaction.VoucherNo = credit.VoucherNo;
            //            transaction.Description = credit.Description;
            //            transaction.AccountType = "Credit";
            //            ledgerModel.Transactionlist.Add(transaction);
            //        }
            //        if (i.DebitAccountlist.Count > 0 || i.CreditAccountlist.Count > 0)
            //        {
            //            list.Add(ledgerModel);
            //        }
            //    }

            //    List<LedgerModel> list1 = new List<LedgerModel>();
            //    foreach(var acc in list)
            //    {
            //        var s = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == acc.AccountCode).Result.ParentID;
            //        var sublevel = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == s).Result;
            //        var controllevel = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == sublevel.ParentID).Result;
            //        var mainLevel = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == controllevel.ParentID).Result;
            //        foreach (var tr in acc.Transactionlist)
            //        {
            //            LedgerModel leg = new LedgerModel();   
            //            leg.AccountCode = acc.AccountCode;
            //            leg.ChartAccountName = acc.ChartAccountName;
            //            leg.CurrencyName = acc.CurrencyName;
            //            leg.MainLevel = mainLevel.AccountName;
            //            leg.ControlLevel = controllevel.AccountName;
            //            leg.SubLevel = sublevel.AccountName;
            //            if (tr.AccountType == "Credit")
            //            {
            //                leg.Amount = tr.CreditAmount;
            //                leg.TransactionType = "Credit";
            //            }
            //            else
            //            {
            //                leg.Amount = tr.DebitAmount;
            //                leg.TransactionType = "Debit";
            //            }
            //            leg.AccountName = tr.AccountName;
            //            leg.TransactionDate = tr.TransactionDate;
            //            leg.VoucherNo = tr.VoucherNo;
            //            leg.Description = tr.Description;
            //            list1.Add(leg);
            //        }    
            //    }
            //    response.data.LedgerList = list1;

            //    response.StatusCode = StaticResource.successStatusCode;
            //    response.Message = "Success";
            //}
            //catch (Exception ex)
            //{
            //    response.StatusCode = StaticResource.failStatusCode;
            //    response.Message = StaticResource.SomethingWrong + ex.Message;
            //}
            //return response;

            APIResponse response = new APIResponse();
            try
            {
                var ledgerList = await Task.Run(() =>
                  _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).Where(x => x.AccountLevelId == 4).ToList()
                    );

                List<LedgerModel> list1 = new List<LedgerModel>();
                //long VoucherNo = 0;
                string mainlevel1 = null, controllevel1 = null, sublevel1 = null, currency = null;
                foreach (var i in ledgerList)
                {
                    if (i.DebitAccountlist.Count > 0 || i.CreditAccountlist.Count > 0)
                    {
                        var s = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == i.AccountCode).Result.ParentID;
                        var sublevel = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == s).Result;
                        var controllevel = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == sublevel.ParentID).Result;
                        var mainLevel = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == controllevel.ParentID).Result;
                        mainlevel1 = mainLevel.AccountName;
                        controllevel1 = controllevel.AccountName;
                        sublevel1 = sublevel.AccountName;

                        // if (i.DebitAccountlist.Count > 0)
                        //     VoucherNo = i.DebitAccountlist[0].VoucherNo;
                        // else
                        //     VoucherNo = i.CreditAccountlist[0].VoucherNo;
                        // var currencylist = await Task.Run(() =>
                        //    _uow.GetDbContext().VoucherDetail.Include(e => e.CurrencyDetail).Where(v => v.VoucherNo == VoucherNo).SingleOrDefault()
                        //);
                        // currency = currencylist.CurrencyDetail.CurrencyName;
                    }

                    foreach (var debit in i.DebitAccountlist)
                    {
                        var currencylist = await Task.Run(() =>
                           _uow.GetDbContext().VoucherDetail.Include(e => e.CurrencyDetail).Where(v => v.VoucherNo == debit.VoucherNo).FirstOrDefault()
                       );
                        currency = currencylist.CurrencyDetail.CurrencyName;
                        LedgerModel leg = new LedgerModel();
                        leg.AccountCode = i.AccountCode;
                        leg.ChartAccountName = i.AccountName;
                        leg.CurrencyName = currency;
                        leg.MainLevel = mainlevel1;
                        leg.ControlLevel = controllevel1;
                        leg.SubLevel = sublevel1;
                        leg.Amount = debit.Amount;
                        leg.TransactionType = "Debit";
                        leg.AccountName = debit.CreditAccountDetails.AccountName;
                        leg.TransactionDate = debit.TransactionDate;
                        leg.VoucherNo = debit.VoucherNo;
                        leg.Description = debit.Description;
                        list1.Add(leg);
                    }
                    foreach (var credit in i.CreditAccountlist)
                    {
                        var currencylist = await Task.Run(() =>
                           _uow.GetDbContext().VoucherDetail.Include(e => e.CurrencyDetail).Where(v => v.VoucherNo == credit.VoucherNo).FirstOrDefault()
                       );
                        currency = currencylist.CurrencyDetail.CurrencyName;
                        LedgerModel leg = new LedgerModel();
                        leg.AccountCode = i.AccountCode;
                        leg.ChartAccountName = i.AccountName;
                        leg.CurrencyName = currency;
                        leg.MainLevel = mainlevel1;
                        leg.ControlLevel = controllevel1;
                        leg.SubLevel = sublevel1;
                        leg.Amount = credit.Amount;
                        leg.TransactionType = "Credit";
                        leg.AccountName = credit.DebitAccountDetails.AccountName;
                        leg.TransactionDate = credit.TransactionDate;
                        leg.VoucherNo = credit.VoucherNo;
                        leg.Description = credit.Description;
                        list1.Add(leg);
                    }
                }
                response.data.LedgerList = list1;


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

        public async Task<APIResponse> GetAllLedgerDetailsByCondition(int? CurrencyId = 2, DateTime? fromdate = null, DateTime? todate = null, int? offceId = null, int? AccountId = null, int? RecordType = 1)
        {
            APIResponse response = new APIResponse();
            try
            {
                var chartofAccountlist = await _uow.ChartAccountDetailRepository.GetAllAsyn();
                IList<ChartAccountDetail> ledgerList = null;

                if (AccountId == null)
                {
                    ledgerList = await Task.Run(() =>
                      _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).
                      Include(d => d.DebitAccountlist).Where(x => x.AccountLevelId == 4).ToList()
                        );
                }
                else
                {
                    ledgerList = await Task.Run(() =>
                    _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).
                    Include(d => d.DebitAccountlist).Where(x => x.AccountLevelId == 4 && x.AccountCode == AccountId).ToList()
                      );
                }
                DateTime defaultdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                if (fromdate == null && todate == null)
                {
                    fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                    todate = DateTime.UtcNow;
                }

                List<LedgerModel> list1 = new List<LedgerModel>();
                string mainlevel1 = null, controllevel1 = null, sublevel1 = null, currency = null;
                double totalDebit = 0, totalCredit = 0;
                List<VoucherTransactionDetails> openingdebitlist = null;
                List<VoucherTransactionDetails> openingcreditlist = null;
                var exchangeratelist = await _uow.ExchangeRateRepository.GetAllAsyn();
                var currencylist = await _uow.CurrencyDetailsRepository.GetAllAsyn();
                foreach (var i in ledgerList)
                {

                    if (offceId != null)
                    {
                        if (RecordType != 1)
                        {

                            if (AccountId != null)
                            {
                                openingdebitlist = i.DebitAccountlist.Where(x => x.TransactionDate >= defaultdate && x.TransactionDate <= fromdate && x.OfficeId == offceId).ToList();
                                openingcreditlist = i.CreditAccountlist.Where(x => x.TransactionDate >= defaultdate && x.TransactionDate <= fromdate && x.OfficeId == offceId).ToList();
                            }

                            i.DebitAccountlist = i.DebitAccountlist.Where(x => x.TransactionDate >= fromdate && x.TransactionDate <= todate && x.OfficeId == offceId).ToList();
                            i.CreditAccountlist = i.CreditAccountlist.Where(x => x.TransactionDate >= fromdate && x.TransactionDate <= todate && x.OfficeId == offceId).ToList();
                        }
                        else
                        {
                            if (AccountId != null)
                            {
                                openingdebitlist = i.DebitAccountlist.Where(x => x.TransactionDate >= defaultdate && x.TransactionDate <= fromdate && x.OfficeId == offceId && x.CurrencyId == CurrencyId).ToList();
                                openingcreditlist = i.CreditAccountlist.Where(x => x.TransactionDate >= defaultdate && x.TransactionDate <= fromdate && x.OfficeId == offceId && x.CurrencyId == CurrencyId).ToList();
                            }
                            i.DebitAccountlist = i.DebitAccountlist.Where(x => x.TransactionDate >= fromdate && x.TransactionDate <= todate && x.OfficeId == offceId && x.CurrencyId == CurrencyId).ToList();
                            i.CreditAccountlist = i.CreditAccountlist.Where(x => x.TransactionDate >= fromdate && x.TransactionDate <= todate && x.OfficeId == offceId && x.CurrencyId == CurrencyId).ToList();
                        }
                    }
                    else
                    {
                        if (RecordType != 1)
                        {
                            if (AccountId != null)
                            {
                                openingdebitlist = i.DebitAccountlist.Where(x => x.TransactionDate >= defaultdate && x.TransactionDate <= fromdate).ToList();
                                openingcreditlist = i.CreditAccountlist.Where(x => x.TransactionDate >= defaultdate && x.TransactionDate <= fromdate).ToList();
                            }
                            i.DebitAccountlist = i.DebitAccountlist.Where(x => x.TransactionDate >= fromdate && x.TransactionDate <= todate).ToList();
                            i.CreditAccountlist = i.CreditAccountlist.Where(x => x.TransactionDate >= fromdate && x.TransactionDate <= todate).ToList();
                        }
                        else
                        {
                            if (AccountId != null)
                            {
                                openingdebitlist = i.DebitAccountlist.Where(x => x.TransactionDate >= defaultdate && x.TransactionDate <= fromdate && x.CurrencyId == CurrencyId).ToList();
                                openingcreditlist = i.CreditAccountlist.Where(x => x.TransactionDate >= defaultdate && x.TransactionDate <= fromdate && x.CurrencyId == CurrencyId).ToList();
                            }
                            i.DebitAccountlist = i.DebitAccountlist.Where(x => x.TransactionDate >= fromdate && x.TransactionDate <= todate && x.CurrencyId == CurrencyId).ToList();
                            i.CreditAccountlist = i.CreditAccountlist.Where(x => x.TransactionDate >= fromdate && x.TransactionDate <= todate && x.CurrencyId == CurrencyId).ToList();
                        }
                    }

                    if (i.DebitAccountlist.Count > 0 || i.CreditAccountlist.Count > 0)
                    {
                        var s = chartofAccountlist.FirstOrDefault(x => x.AccountCode == i.AccountCode).ParentID;
                        var sublevel = chartofAccountlist.Where(x => x.AccountCode == s).FirstOrDefault();
                        var controllevel = chartofAccountlist.Where(x => x.AccountCode == sublevel.ParentID).FirstOrDefault();
                        var mainLevel = chartofAccountlist.Where(x => x.AccountCode == controllevel.ParentID).FirstOrDefault();

                        //var s = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == i.AccountCode).Result.ParentID;
                        //var sublevel = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == s).Result;
                        //var controllevel = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == sublevel.ParentID).Result;
                        //var mainLevel = _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == controllevel.ParentID).Result;
                        mainlevel1 = mainLevel.AccountName;
                        controllevel1 = controllevel.AccountName;
                        sublevel1 = sublevel.AccountName;
                    }
                    double debitamount = 0, creditamount = 0, exchangerate = 0;
                    foreach (var debit in i.DebitAccountlist)
                    {
                        currency = currencylist.Where(x => x.CurrencyId == debit.CurrencyId).FirstOrDefault().CurrencyCode;
                        if (debit.CurrencyId != CurrencyId)
                        {
                            var list = exchangeratelist.Where(x => x.FromCurrency == debit.CurrencyId && x.ToCurrency == CurrencyId && x.Date.Date == debit.TransactionDate.Date).OrderByDescending(o => o.ExchangeRateId).FirstOrDefault();
                            exchangerate = list?.Rate ?? 0;
                            debitamount = debit.Amount * exchangerate;
                        }
                        else
                        {
                            debitamount = debit.Amount;
                        }
                        totalDebit += debitamount;

                        var accountname = chartofAccountlist.FirstOrDefault(x => x.AccountCode == debit.CreditAccount).AccountName;

                        LedgerModel leg = new LedgerModel();
                        leg.AccountCode = i.AccountCode;
                        leg.ChartAccountName = i.AccountName;
                        leg.CurrencyName = currency;
                        leg.MainLevel = mainlevel1;
                        leg.ControlLevel = controllevel1;
                        leg.SubLevel = sublevel1;
                        leg.Amount = debitamount;
                        leg.TransactionType = "Debit";
                        leg.AccountName = accountname;
                        leg.TransactionDate = debit.TransactionDate;
                        leg.VoucherNo = debit.VoucherNo;
                        leg.Description = debit.Description;
                        list1.Add(leg);
                    }
                    foreach (var credit in i.CreditAccountlist)
                    {
                        currency = currencylist.Where(x => x.CurrencyId == credit.CurrencyId).FirstOrDefault().CurrencyCode;
                        if (credit.CurrencyId != CurrencyId)
                        {
                            var list = exchangeratelist.Where(x => x.FromCurrency == credit.CurrencyId && x.ToCurrency == CurrencyId && x.Date.Date == credit.TransactionDate.Date).OrderByDescending(o => o.ExchangeRateId).FirstOrDefault();
                            exchangerate = list?.Rate ?? 0;
                            creditamount = credit.Amount * exchangerate;
                        }
                        else
                        {
                            creditamount = credit.Amount;
                        }
                        totalCredit += creditamount;
                        var accountname = chartofAccountlist.FirstOrDefault(x => x.AccountCode == credit.DebitAccount).AccountName;

                        LedgerModel leg = new LedgerModel();
                        leg.AccountCode = i.AccountCode;
                        leg.ChartAccountName = i.AccountName;
                        leg.CurrencyName = currency;
                        leg.MainLevel = mainlevel1;
                        leg.ControlLevel = controllevel1;
                        leg.SubLevel = sublevel1;
                        leg.Amount = creditamount;
                        leg.TransactionType = "Credit";
                        leg.AccountName = accountname;
                        leg.TransactionDate = credit.TransactionDate;
                        leg.VoucherNo = credit.VoucherNo;
                        leg.Description = credit.Description;
                        list1.Add(leg);
                    }
                }

                if (AccountId != null)
                {
                    double totaldebitopeing = 0, totalcreditopeing = 0, opendingbalance = 0, closingbalance = 0;
                    if (openingdebitlist != null)
                    {
                        totaldebitopeing = openingdebitlist.Sum(x => x.Amount);
                    }
                    if (openingcreditlist != null)
                    {
                        totalcreditopeing = openingcreditlist.Sum(x => x.Amount);
                    }

                    string balanceTypeClosing = string.Empty;
                    string balanceTypeOpening = string.Empty;

                    if (totalcreditopeing > totaldebitopeing)
                    {
                        opendingbalance = totalcreditopeing - totaldebitopeing;
                        balanceTypeOpening = "Credit";
                    }
                    else
                    {
                        opendingbalance = totaldebitopeing - totalcreditopeing;
                        balanceTypeOpening = "Debit";
                    }


                    if (totalCredit > totalDebit)
                    {
                        closingbalance = totalCredit - totalDebit;
                        balanceTypeClosing = "Credit";
                    }
                    else
                    {
                        closingbalance = totalDebit - totalCredit;
                        balanceTypeClosing = "Debit";
                    }

                    response.data.AccountOpendingAndClosingBL = new AccountOpendingAndClosingBL
                    {
                        OpenningBalance = opendingbalance,
                        ClosingBalance = closingbalance,
                        OpenningBalanceType = balanceTypeOpening,
                        ClosingBalanceType = balanceTypeClosing
                    };
                }
                response.data.LedgerList = list1;
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

        public async Task<APIResponse> GetTrailBlanceDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var ledgerList = await Task.Run(() =>
                  _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).ToList()
                    );

                List<TrailBlance> list = new List<TrailBlance>();
                double debitamount = 0, creditamount = 0;
                foreach (var i in ledgerList)
                {
                    TrailBlance trailBlance = new TrailBlance();
                    if (i.DebitAccountlist.Count > 0 || i.CreditAccountlist.Count > 0)
                    {
                        long voucherno = 0;
                        if (i.DebitAccountlist.Count > 0)
                            voucherno = i.DebitAccountlist[0].VoucherNo;
                        else
                            voucherno = i.CreditAccountlist[0].VoucherNo;
                        var currency = await Task.Run(() =>
                            _uow.GetDbContext().VoucherDetail.Include(e => e.CurrencyDetail).Include(x => x.OfficeDetails).Where(v => v.VoucherNo == voucherno).FirstOrDefault()
                        );
                        //trailBlance.OfficeName = currency.OfficeDetails.OfficeName;
                        trailBlance.CurrencyName = currency.CurrencyDetail.CurrencyName;
                        trailBlance.AccountName = i.AccountName;
                        foreach (var debit in i.DebitAccountlist)
                        {
                            debitamount = debitamount + debit.Amount;
                        }
                        foreach (var credit in i.CreditAccountlist)
                        {
                            creditamount = creditamount + credit.Amount;
                        }
                        trailBlance.DebitAmount = debitamount;
                        trailBlance.CreditAmount = creditamount;
                        list.Add(trailBlance);
                    }
                }
                response.data.TrailBlanceList = list;
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


        public async Task<APIResponse> GetTrailBlanceDetailsByCondition(int? OfficeId = null, DateTime? Fromdate = null, DateTime? Todate = null, int? CurrencyId = null, int? RecordType = null)
        {
            APIResponse response = new APIResponse();
            try
            {
                var exchangeratelist = await _uow.ExchangeRateRepository.GetAllAsyn();
                //var currencylist = await _uow.CurrencyDetailsRepository.GetAllAsyn();

                var ledgerList = await Task.Run(() =>
                  _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).Where(x => x.AccountLevelId == 4).ToList()
                    );

                if (Fromdate == null && Todate == null)
                {
                    Fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                    Todate = DateTime.UtcNow;
                }
                List<TrailBlance> list = new List<TrailBlance>();
                double debitamount = 0, creditamount = 0, exchangerate = 0, totaldebitamount = 0, totalcreditamount = 0;
                foreach (var i in ledgerList)
                {
                    TrailBlance trailBlance = new TrailBlance();

                    if (RecordType == 1)
                    {
                        i.CreditAccountlist = i.CreditAccountlist.Where(x => x.TransactionDate >= Fromdate && x.TransactionDate <= Todate && x.CurrencyId == CurrencyId).ToList();
                        i.DebitAccountlist = i.DebitAccountlist.Where(x => x.TransactionDate >= Fromdate && x.TransactionDate <= Todate && x.CurrencyId == CurrencyId).ToList();
                    }
                    else
                    {
                        i.CreditAccountlist = i.CreditAccountlist.Where(x => x.TransactionDate >= Fromdate && x.TransactionDate <= Todate).ToList();
                        i.DebitAccountlist = i.DebitAccountlist.Where(x => x.TransactionDate >= Fromdate && x.TransactionDate <= Todate).ToList();
                    }

                    if (i.DebitAccountlist.Count > 0 || i.CreditAccountlist.Count > 0)
                    {
                        if (OfficeId != null)
                        {
                            i.CreditAccountlist = i.CreditAccountlist.Where(x => x.OfficeId == OfficeId).ToList();
                            i.DebitAccountlist = i.DebitAccountlist.Where(x => x.OfficeId == OfficeId).ToList();
                        }

                        if (i.DebitAccountlist.Count > 0 || i.CreditAccountlist.Count > 0)
                        {
                            //trailBlance.CurrencyName = currencylist.Where(x => x.CurrencyId == CurrencyId).FirstOrDefault().CurrencyCode;
                            trailBlance.AccountName = i.AccountName;
                            debitamount = 0; creditamount = 0; totaldebitamount = 0; totalcreditamount = 0; exchangerate = 0;
                            if (RecordType != 1)
                            {
                                foreach (var debit in i.DebitAccountlist)
                                {
                                    if (debit.CurrencyId != CurrencyId)
                                    {
                                        var ratelist = exchangeratelist.Where(x => x.FromCurrency == debit.CurrencyId && x.ToCurrency == CurrencyId && x.Date.Date == debit.TransactionDate.Date).OrderByDescending(o => o.ExchangeRateId).FirstOrDefault();
                                        exchangerate = ratelist?.Rate ?? 0;
                                        debitamount = debit.Amount * exchangerate;
                                    }
                                    else
                                    {
                                        debitamount = debit.Amount;
                                    }

                                    totaldebitamount += debitamount;
                                }
                                foreach (var credit in i.CreditAccountlist)
                                {
                                    if (credit.CurrencyId != CurrencyId)
                                    {
                                        var ratelist = exchangeratelist.Where(x => x.FromCurrency == credit.CurrencyId && x.ToCurrency == CurrencyId && x.Date.Date == credit.TransactionDate.Date).FirstOrDefault();
                                        exchangerate = ratelist?.Rate ?? 0;
                                        creditamount = credit.Amount * exchangerate;
                                    }
                                    else
                                    {
                                        creditamount = credit.Amount;
                                    }

                                    totalcreditamount += creditamount;
                                }
                            }
                            else
                            {
                                if (i.DebitAccountlist != null)
                                {
                                    totaldebitamount = i.DebitAccountlist.Sum(x => x.Amount);
                                }
                                if (i.CreditAccountlist != null)
                                {
                                    totalcreditamount = i.CreditAccountlist.Sum(x => x.Amount);
                                }
                            }
                            trailBlance.DebitAmount = totaldebitamount;
                            trailBlance.CreditAmount = totalcreditamount;
                            list.Add(trailBlance);
                        }
                    }
                }
                response.data.TrailBlanceList = list;
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

        public async Task<APIResponse> GetProjectAndBudgetLine()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().ProjectBudgetLine.Select(x => new BudgetLineModel {
                    BudgetLineId = x.BudgetLineId,
                    ProjectId = x.ProjectId,
                    Description = x.Description
                }).ToListAsync();

                var list1 = await _uow.GetDbContext().ProjectDetails.
                    Select(x => new ProjectBudgetModelNew {
                        ProjectId = x.ProjectId,
                        ProjectName = x.ProjectName
                    }).ToListAsync();

                ProjectBudgetLinesModel model = new ProjectBudgetLinesModel();
                model.BudgetLines = list;
                model.ProjectList = list1;

                response.data.ProjectBudgetLinesModel = model;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Project BudgetLine List ";
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> AddNotesDetails(NotesMasterModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                //var existrecord = await _uow.NotesMasterRepository.FindAsync(x => x.IsDeleted == false && x.Notes == model.Notes && x.BlanceType == model.BlanceType);
                NotesMaster obj = _mapper.Map<NotesMaster>(model);
                await _uow.NotesMasterRepository.AddAsyn(obj);
                await _uow.SaveAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditNotesDetails(NotesMasterModel model)
        {
            APIResponse respone = new APIResponse();
            try
            {
                var existrecord = await _uow.NotesMasterRepository.FindAsync(x => x.NoteId == model.NoteId);
                if (existrecord != null)
                {
                    existrecord.AccountCode = model.AccountCode;
                    existrecord.Narration = model.Narration;
                    existrecord.Notes = model.Notes;
                    existrecord.BlanceType = model.BlanceType;
                    existrecord.FinancialReportTypeId = model.FinancialReportTypeId;
                    existrecord.AccountTypeId = model.AccountTypeId;
                    existrecord.ModifiedById = model.ModifiedById;
                    existrecord.ModifiedDate = model.ModifiedDate;
                    existrecord.IsDeleted = model.IsDeleted;
                    await _uow.NotesMasterRepository.UpdateAsyn(existrecord);
                    respone.StatusCode = StaticResource.successStatusCode;
                    respone.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                respone.StatusCode = StaticResource.failStatusCode;
                respone.Message = ex.Message;
            }
            return respone;
        }

        public async Task<APIResponse> GetAllNotesDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await Task.Run(() =>
                    //_uow.NotesMasterRepository.FindAllAsync(x=> x.IsDeleted== false).Result.ToList()
                    _uow.GetDbContext().NotesMaster.Include(a=> a.AccountType).Include(c=> c.ChartAccountDetails).Where(x=> x.IsDeleted == false).ToListAsync()
                );
                var noteslist = list.Select(x => new NotesMasterModel
                {
                    NoteId = x.NoteId,
                    Notes = x.Notes,
                    AccountCode = x.AccountCode,
					ChartOfAccountCode = x.ChartAccountDetails?.ChartOfAccountCode ?? 0,
                    Narration = x.Narration,
                    BlanceType = x.BlanceType,
                    BlanceTypeName = x.BlanceType == (int)BalanceType.SUM ? "Sum" : x.BlanceType == (int)BalanceType.CR ? "Cr" : x.BlanceType == (int)BalanceType.DR ? "Dr" : "",
                    FinancialReportTypeId = x.FinancialReportTypeId,
                    FinancialReportTypeName = x.FinancialReportTypeId == (int)FinancialReportType.BALANCESHEET ? "Blance Sheet" : x.FinancialReportTypeId == (int)FinancialReportType.INCOMEANDEXPANCE ?  "Income and Expance" : "",
                    AccountTypeId = x.AccountTypeId,
                    AccountTypeName = x.AccountType?.AccountTypeName ?? null
                }).ToList();
                response.data.NotesDetailsList = noteslist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetBlanceSheetDetails(int? financialyearid, int? currencyid, int? financialreporttype)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await Task.Run(() =>
                    _uow.GetDbContext().NotesMaster.Include(c => c.ChartAccountDetails)
                    .Include(c=> c.ChartAccountDetails.CreditAccountlist)
                    .Include(c=> c.ChartAccountDetails.DebitAccountlist)
                    .Where(x => x.IsDeleted == false && x.FinancialReportTypeId== financialreporttype).ToListAsync()
                );

                var exchangeratelist = await _uow.ExchangeRateRepository.GetAllAsyn();

                List<NotesMasterModel> noteList = new List<NotesMasterModel>();
                double creditAmount = 0, debitAmount = 0, balanceAmount=0, exchangerate = 0;
                int accountcurrencyid = 0;
                foreach (var l in list)
                {
                    if (l.BlanceType== (int)BalanceType.SUM)
                    {
                        creditAmount =l.ChartAccountDetails.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid && f.CurrencyId == currencyid).Sum(x => x.Amount);
                        debitAmount=l.ChartAccountDetails.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid && f.CurrencyId == currencyid).Sum(x => x.Amount);
                        balanceAmount = creditAmount - debitAmount;

                        if (l.ChartAccountDetails.CreditAccountlist.Count > 0)
                        {
                            var accountlist = l.ChartAccountDetails.CreditAccountlist.OrderByDescending(x => x.TransactionId).ToList();
                            accountcurrencyid = Convert.ToInt32(accountlist[0].CurrencyId);
                            if (accountcurrencyid != currencyid)
                            {
                                var ratelist = exchangeratelist.Where(x => x.FromCurrency == accountcurrencyid && x.ToCurrency == currencyid && x.Date.Date == accountlist[0].TransactionDate.Date).FirstOrDefault();
                                exchangerate = ratelist?.Rate ?? 0;
                                balanceAmount = balanceAmount * exchangerate;
                            }
                        }
                    }
                    if(l.BlanceType == (int)BalanceType.DR)
                    {
                        debitAmount = l.ChartAccountDetails.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid && f.CurrencyId == currencyid).Sum(x => x.Amount);
                        balanceAmount = debitAmount;

                        if (l.ChartAccountDetails.CreditAccountlist.Count > 0)
                        {
                            var accountlist = l.ChartAccountDetails.CreditAccountlist.OrderByDescending(x => x.TransactionId).ToList();
                            accountcurrencyid = Convert.ToInt32(accountlist[0].CurrencyId);
                            if (accountcurrencyid != currencyid)
                            {
                                var ratelist = exchangeratelist.Where(x => x.FromCurrency == accountcurrencyid && x.ToCurrency == currencyid && x.Date.Date == accountlist[0].TransactionDate.Date).FirstOrDefault();
                                exchangerate = ratelist?.Rate ?? 0;
                                balanceAmount = balanceAmount * exchangerate;
                            }
                        }
                    }
                    if (l.BlanceType == (int)BalanceType.CR)
                    {
                        creditAmount = l.ChartAccountDetails.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid && f.CurrencyId == currencyid).Sum(x => x.Amount);
                        balanceAmount = creditAmount;

                        if (l.ChartAccountDetails.CreditAccountlist.Count > 0)
                        {
                            var accountlist = l.ChartAccountDetails.CreditAccountlist.OrderByDescending(x => x.TransactionId).ToList();
                            accountcurrencyid = Convert.ToInt32(accountlist[0].CurrencyId);
                            if (accountcurrencyid != currencyid)
                            {
                                var ratelist = exchangeratelist.Where(x => x.FromCurrency == accountcurrencyid && x.ToCurrency == currencyid && x.Date.Date == accountlist[0].TransactionDate.Date).FirstOrDefault();
                                exchangerate = ratelist?.Rate ?? 0;
                                balanceAmount = balanceAmount * exchangerate;
                            }
                        }
                    }

                    NotesMasterModel obj = new NotesMasterModel();
                    obj.AccountCode = l.AccountCode;
                    obj.Notes = l.Notes;
                    obj.AccountTypeId = l.AccountTypeId;
                    obj.Narration = l.Narration;
                    obj.BalanceAmount = balanceAmount;
                    noteList.Add(obj);
                }


                BalanceSheet bal = new BalanceSheet();

                bal.CurrentAssest = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.ASSET).GroupBy(x => x.Notes)
                    .Select(x => new BalanceSheetModel
                    {
                        Note=x.First().Notes,
                        Narration=x.First().Narration,
                        Balance=x.Sum(y=>y.BalanceAmount)
                        
                    }).ToList();

                bal.Libility = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.LIBILITY).GroupBy(x => x.Notes)
                    .Select(x => new BalanceSheetModel
                    {
                        Note = x.First().Notes,
                        Narration = x.First().Narration,
                        Balance = x.Sum(y => y.BalanceAmount)

                    }).ToList();

                bal.Equity = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.EQUITY).GroupBy(x => x.Notes)
                    .Select(x => new BalanceSheetModel
                    {
                        Note = x.First().Notes,
                        Narration = x.First().Narration,
                        Balance = x.Sum(y => y.BalanceAmount)

                    }).ToList();

                bal.Revenue = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.REVENUE).GroupBy(x => x.Notes)
                    .Select(x => new BalanceSheetModel
                    {
                        Note = x.First().Notes,
                        Narration = x.First().Narration,
                        Balance = x.Sum(y => y.BalanceAmount)

                    }).ToList();

                bal.Expense = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.EXPENSE).GroupBy(x => x.Notes)
                    .Select(x => new BalanceSheetModel
                    {
                        Note = x.First().Notes,
                        Narration = x.First().Narration,
                        Balance = x.Sum(y => y.BalanceAmount)

                    }).ToList();

                bal.Income = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.INCOME).GroupBy(x => x.Notes)
                    .Select(x => new BalanceSheetModel
                    {
                        Note = x.First().Notes,
                        Narration = x.First().Narration,
                        Balance = x.Sum(y => y.BalanceAmount)

                    }).ToList();
                bal.Funds = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.FUNDS).GroupBy(x => x.Notes)
                  .Select(x => new BalanceSheetModel
                  {
                      Note = x.First().Notes,
                      Narration = x.First().Narration,
                      Balance = x.Sum(y => y.BalanceAmount)

                  }).ToList();
                bal.Reserve_Account_Adjustment = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.RESERVE_ACCOUNT_ADJUSTMENT).GroupBy(x => x.Notes)
                  .Select(x => new BalanceSheetModel
                  {
                      Note = x.First().Notes,
                      Narration = x.First().Narration,
                      Balance = x.Sum(y => y.BalanceAmount)

                  }).ToList();
                bal.Current_Libility = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.CURRENT_LIBILITY).GroupBy(x => x.Notes)
                  .Select(x => new BalanceSheetModel
                  {
                      Note = x.First().Notes,
                      Narration = x.First().Narration,
                      Balance = x.Sum(y => y.BalanceAmount)

                  }).ToList();


                bal.Long_Term_Libility = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.LONG_TERM_LIBILITY).GroupBy(x => x.Notes)
                  .Select(x => new BalanceSheetModel
                  {
                      Note = x.First().Notes,
                      Narration = x.First().Narration,
                      Balance = x.Sum(y => y.BalanceAmount)

                  }).ToList();
                bal.Reserve_Account = noteList.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.RESERVE_ACCOUNT).GroupBy(x => x.Notes)
                  .Select(x => new BalanceSheetModel
                  {
                      Note = x.First().Notes,
                      Narration = x.First().Narration,
                      Balance = x.Sum(y => y.BalanceAmount)

                  }).ToList();

                response.data.BalanceSheet = bal;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetDetailsOfNotes(int? financialyearid, int? currencyid)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await Task.Run(() =>
                    _uow.GetDbContext().NotesMaster.Include(c => c.ChartAccountDetails)
                    .Include(c => c.ChartAccountDetails.CreditAccountlist)
                    .Include(c => c.ChartAccountDetails.DebitAccountlist)
                    .Where(x => x.IsDeleted == false && x.FinancialReportTypeId == 1).ToListAsync()
                );

                List<DetailsOfNotesModel> detailsofnoteList = new List<DetailsOfNotesModel>();
                double creditAmount = 0, debitAmount = 0, balanceAmount = 0;
                foreach (var l in list)
                {
                    if (l.BlanceType == (int)BalanceType.SUM)
                    {
                        creditAmount = l.ChartAccountDetails.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid && f.CurrencyId == currencyid).Sum(x => x.Amount);
                        debitAmount = l.ChartAccountDetails.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid && f.CurrencyId == currencyid).Sum(x => x.Amount);
                        balanceAmount = creditAmount - debitAmount;
                    }
                    if (l.BlanceType == (int)BalanceType.DR)
                    {
                        debitAmount = l.ChartAccountDetails.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid && f.CurrencyId == currencyid).Sum(x => x.Amount);
                        balanceAmount = debitAmount;
                    }
                    if (l.BlanceType == (int)BalanceType.CR)
                    {
                        creditAmount = l.ChartAccountDetails.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid && f.CurrencyId == currencyid).Sum(x => x.Amount);
                        balanceAmount = creditAmount;
                    }

                    DetailsOfNotesModel obj = new DetailsOfNotesModel();
                    obj.ChartOfAccountCode = l.ChartAccountDetails?.ChartOfAccountCode ?? null;
                    obj.AccountName = l.ChartAccountDetails?.AccountName ?? null;
                    obj.Notes = l.Notes;
                    obj.BalanceAmount = balanceAmount;
                    detailsofnoteList.Add(obj);
                }

                response.data.DetailsOfNotesList = detailsofnoteList.OrderBy(x=> x.Notes).ToList();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
