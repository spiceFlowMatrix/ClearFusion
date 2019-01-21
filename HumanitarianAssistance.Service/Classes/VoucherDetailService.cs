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
using HumanitarianAssistance.Entities;
using System.Data;
using NpgsqlTypes;
using Npgsql;
using HumanitarianAssistance.ViewModels.SPModels;
using System.Diagnostics;
using DataAccess.DbEntities.AccountingNew;

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
                                      .Include(f => f.FinancialYearDetails)
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

        public async Task<APIResponse> GetAllVouchersByOfficeId(int officeId)
        {
            APIResponse response = new APIResponse();
            try
            {

                var voucherList = await _uow.GetDbContext().VoucherDetail
                                                            .Where(v => v.IsDeleted == false && v.OfficeId == officeId)
                                                            .OrderBy(x => x.ReferenceNo)
                                                            .Select(v => new VoucherDetailModel
                                                            {
                                                                VoucherNo = v.VoucherNo,
                                                                ReferenceNo = v.ReferenceNo,
                                                                VoucherDate = v.VoucherDate
                                                            })
                                                            .ToListAsync();

                response.data.VoucherDetailList = voucherList;
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

        public async Task<APIResponse> GetAllVoucherDetailsByFilter(VoucherFilterModel filterModel)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (filterModel.OfficesList.Count > 0)
                {
                    List<VoucherDetail> voucherList = new List<VoucherDetail>();
                    if (filterModel.Date == null)
                    {
                        //voucherList = await _uow.GetDbContext().VoucherDetail
                        //                                 .Include(o => o.OfficeDetails).Include(j => j.JournalDetails)
                        //                                 .Include(c => c.CurrencyDetail)
                        //                                 .Include(o => o.FinancialYearDetails)
                        //                                 .Where(v => v.IsDeleted == false && filterModel.OfficesList.Contains(v.OfficeId)).OrderBy(x => x.VoucherDate).ToListAsync();


                        voucherList = await _uow.GetDbContext().VoucherDetail
                                                         .Include(o => o.OfficeDetails).Include(j => j.JournalDetails)
                                                         .Include(c => c.CurrencyDetail)
                                                         .Include(o => o.FinancialYearDetails)
                                                         .Where(v => v.IsDeleted == false && filterModel.OfficesList.Contains(v.OfficeId))
                                                         .OrderByDescending(x => x.VoucherDate)
                                                         .Skip(filterModel.Skip)
                                                         .Take(10)
                                                         .ToListAsync();

                    }
                    else
                    {
                        voucherList = await _uow.GetDbContext().VoucherDetail
                                                      .Include(o => o.OfficeDetails).Include(j => j.JournalDetails)
                                                      .Include(c => c.CurrencyDetail)
                                                      .Include(f => f.FinancialYearDetails)
                                                      .Where(v => v.IsDeleted == false && filterModel.OfficesList.Contains(v.OfficeId) && v.VoucherDate.Value.Date == filterModel.Date.Value.Date)
                                                      .OrderByDescending(x => x.VoucherDate)
                                                      .Skip(filterModel.Skip)
                                                      .Take(10)
                                                      .ToListAsync();
                    }

                    List<VoucherDetailModel> voucherFilteredList = new List<VoucherDetailModel>();
                    foreach (var i in voucherList)
                    {
                        VoucherDetailModel obj = new VoucherDetailModel();
                        obj.VoucherNo = i.VoucherNo;
                        obj.CurrencyCode = i.CurrencyDetail?.CurrencyCode ?? null;
                        obj.CurrencyId = i.CurrencyDetail?.CurrencyId ?? 0;
                        obj.VoucherDate = i.VoucherDate;
                        obj.ChequeNo = i.ChequeNo;
                        obj.ReferenceNo = i.ReferenceNo;
                        obj.Description = i.Description;
                        obj.JournalName = i.JournalDetails?.JournalName ?? null;
                        obj.JournalCode = i.JournalDetails?.JournalCode ?? null;
                        obj.VoucherTypeId = i.VoucherTypeId;
                        obj.OfficeId = i.OfficeId;
                        obj.ProjectId = i.ProjectId;
                        obj.BudgetLineId = i.BudgetLineId;
                        obj.OfficeName = i.OfficeDetails?.OfficeName ?? null;
                        obj.FinancialYearId = i.FinancialYearId;
                        obj.FinancialYearName = i.FinancialYearDetails?.FinancialYearName ?? null;
                        voucherFilteredList.Add(obj);
                    }

                    response.data.VoucherDetailList = voucherFilteredList.OrderByDescending(v => v.VoucherDate).ToList();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.NoDataFound;
                }

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
                //var officekey = _uow.OfficeDetailRepository.FindAsync(o => o.OfficeId == model.OfficeId).Result.OfficeKey;

                var officeCode = _uow.OfficeDetailRepository.FindAsync(o => o.OfficeId == model.OfficeId).Result.OfficeCode; //use OfficeCode

                VoucherDetail obj = _mapper.Map<VoucherDetail>(model);
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

                    var user = await _uow.UserDetailsRepository.FindAsync(x => x.AspNetUserId == model.CreatedById);

                    LoggerDetailsModel loggerObj = new LoggerDetailsModel();
                    loggerObj.NotificationId = (int)Common.Enums.LoggerEnum.VoucherUpdate;
                    loggerObj.IsRead = false;
                    loggerObj.UserName = user.FirstName + " " + user.LastName;
                    loggerObj.UserId = model.CreatedById;
                    loggerObj.LoggedDetail = "Voucher " + voucherdetailInfo.ReferenceNo + " Updated";
                    loggerObj.CreatedDate = model.CreatedDate;

                    response.LoggerDetailsModel = loggerObj;

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
                var queryResult = EF.CompileAsyncQuery(
                (ApplicationDbContext ctx) => ctx.VoucherDocumentDetail.Where(x => x.VoucherNo == VoucherNo));
                var list = await Task.Run(() =>
                    queryResult(_uow.GetDbContext()).ToListAsync().Result
                );

                var documentlist = list.Select(x => new VoucherDocumentDetailModel
                {
                    DocumentGUID = x.DocumentGUID + x.Extension,
                    DocumentName = x.DocumentName,
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

                File.WriteAllBytes(@"Documents/" + filename, filepath);

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
                  _uow.GetDbContext().JournalDetail.Include(e => e.VoucherDetails).ThenInclude(e => e.VoucherTransactionDetails).ThenInclude(e => e.ChartOfAccountDetail).ToList()
                    );
                List<JournalVoucherViewModel> listJournalView = new List<JournalVoucherViewModel>();
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

        public async Task<APIResponse> GetJouranlVoucherDetailsByCondition(JournalViewModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                int voucherDetailsCount = 0;
                List<JournalVoucherViewModel> listJournalView = new List<JournalVoucherViewModel>();

                if (model != null)
                {

                    #region Commented code
                    // var allCurrencies = await _uow.CurrencyDetailsRepository.FindAllAsync(x => x.IsDeleted == false);
                    //var baseCurrency = allCurrencies.FirstOrDefault(x => x.Status == true);

                    // if (model.fromdate == null && model.todate == null)
                    // {
                    //     model.fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                    //     model.todate = DateTime.UtcNow;
                    // }

                    // //NOTE: Take only particular currency
                    // if (model.RecordType == 1)
                    // {
                    //     var VoucherDetails = (from v in _uow.GetDbContext().VoucherDetail
                    //                           join t in _uow.GetDbContext().VoucherTransactions on v.VoucherNo equals t.VoucherNo
                    //                           join a in _uow.GetDbContext().ChartOfAccountNew on t.ChartOfAccountNewId equals a.ChartOfAccountNewId
                    //                           where model.JournalCode.Contains(v.JournalCode) &&
                    //                                 model.OfficesList.Contains(v.OfficeId) &&
                    //                                 model.AccountLists.Contains(t.ChartOfAccountNewId) &&
                    //                                 v.IsDeleted == false &&
                    //                                 v.CurrencyId == model.CurrencyId &&       // for particular currency
                    //                                 v.VoucherDate.Value.Date >= model.fromdate.Date &&
                    //                                 v.VoucherDate.Value.Date <= model.todate.Date
                    //                           orderby t.TransactionDate
                    //                           select (new
                    //                           {
                    //                               t.TransactionDate,
                    //                               v.VoucherNo,
                    //                               v.ReferenceNo,
                    //                               t.Description,
                    //                               t.CurrencyId,
                    //                               t.Program,
                    //                               t.Project,
                    //                               t.ChartOfAccountNewId,
                    //                               t.Debit,
                    //                               t.Credit,
                    //                               a.AccountName,
                    //                               a.ChartOfAccountNewCode,
                    //                               v.JournalCode
                    //                           })).ToList();

                    //     foreach (var item in VoucherDetails)
                    //     {
                    //         if (model.CurrencyId == item.CurrencyId)
                    //         {
                    //             // Credit
                    //             JournalVoucherViewModel obj = new JournalVoucherViewModel();
                    //             obj.TransactionDate = item.TransactionDate;
                    //             obj.VoucherNo = item?.VoucherNo ?? 0;
                    //             obj.TransactionDescription = item?.Description ?? null;
                    //             obj.CurrencyId = item.CurrencyId;
                    //             obj.AccountCode = item.ChartOfAccountNewCode;
                    //             obj.AccountName = item.AccountName;
                    //             obj.CreditAmount = item.Credit;
                    //             obj.DebitAmount = item.Debit;
                    //             obj.ReferenceNo = item.ReferenceNo;
                    //             obj.ChartOfAccountNewId = item.ChartOfAccountNewId.Value;
                    //             obj.JournalCode = item.JournalCode;
                    //             listJournalView.Add(obj);
                    //         }
                    //     }
                    // }
                    // else
                    // {

                    //     var VoucherDetails = (from v in _uow.GetDbContext().VoucherDetail
                    //                           join t in _uow.GetDbContext().VoucherTransactions on v.VoucherNo equals t.VoucherNo
                    //                           join a in _uow.GetDbContext().ChartOfAccountNew on t.ChartOfAccountNewId equals a.ChartOfAccountNewId
                    //                           where model.JournalCode.Contains(v.JournalCode) &&
                    //                                 model.OfficesList.Contains(v.OfficeId) &&
                    //                                 model.AccountLists.Contains(t.ChartOfAccountNewId) &&
                    //                                 v.IsDeleted == false &&
                    //                                 v.VoucherDate.Value.Date >= model.fromdate.Date &&
                    //                                 v.VoucherDate.Value.Date <= model.todate.Date
                    //                           select (new
                    //                           {
                    //                               t.TransactionDate,
                    //                               v.VoucherNo,
                    //                               t.Description,
                    //                               t.CurrencyId,
                    //                               t.Program,
                    //                               t.Project,
                    //                               t.ChartOfAccountNewId,
                    //                               t.Debit,
                    //                               t.Credit,
                    //                               a.AccountName,
                    //                               v.ReferenceNo,
                    //                               t.AFGAmount,
                    //                               t.PKRAmount,
                    //                               t.USDAmount,
                    //                               t.EURAmount,
                    //                               a.ChartOfAccountNewCode
                    //                           }))
                    //                             .OrderBy(x => x.TransactionDate).AsNoTracking()
                    //                             .ToList();

                    //     foreach (var item in VoucherDetails)
                    //     {

                    //         ExchangeRateDetail exchangeRateDetail = _uow.GetDbContext().ExchangeRateDetail
                    //                                                                    .OrderByDescending(x => x.Date)
                    //                                                                    .FirstOrDefault(x => x.Date == item.TransactionDate && x.IsDeleted== false 
                    //                                                                     && x.FromCurrency== item.CurrencyId && x.ToCurrency== model.CurrencyId);

                    //         if (exchangeRateDetail == null)
                    //         {
                    //             exchangeRateDetail = _uow.GetDbContext().ExchangeRateDetail
                    //                                                     .OrderByDescending(x => x.Date)
                    //                                                     .FirstOrDefault(x => x.IsDeleted == false
                    //                                                     && x.FromCurrency == item.CurrencyId && x.ToCurrency == model.CurrencyId);
                    //         }

                    //         JournalVoucherViewModel obj = new JournalVoucherViewModel();

                    //         obj.TransactionDate = item.TransactionDate;
                    //         obj.VoucherNo = item.VoucherNo;
                    //         obj.TransactionDescription = item.Description;
                    //         obj.CurrencyId = item.CurrencyId;
                    //         obj.ReferenceNo = item.ReferenceNo;
                    //         obj.AccountCode = item.ChartOfAccountNewCode;
                    //         obj.AccountName = item.AccountName;
                    //         obj.ChartOfAccountNewId = item.ChartOfAccountNewId.Value;

                    //         if (model.CurrencyId == (int)Currency.PKR)
                    //         {
                    //             obj.CreditAmount = item.Credit * (double)exchangeRateDetail.Rate;
                    //             obj.DebitAmount = item.Debit * (double)exchangeRateDetail.Rate;
                    //         }
                    //         else if (model.CurrencyId == (int)Currency.AFG)
                    //         {
                    //             obj.CreditAmount = item.Credit * (double)exchangeRateDetail.Rate;
                    //             obj.DebitAmount = item.Debit * (double)exchangeRateDetail.Rate;

                    //         }
                    //         else if (model.CurrencyId == (int)Currency.EUR)
                    //         {
                    //             obj.CreditAmount = item.Credit * (double)exchangeRateDetail.Rate;
                    //             obj.DebitAmount = item.Debit * (double)exchangeRateDetail.Rate;

                    //         }
                    //         else
                    //         {
                    //             obj.CreditAmount = item.Credit * (double)exchangeRateDetail.Rate;
                    //             obj.DebitAmount = item.Debit * (double)exchangeRateDetail.Rate;

                    //         }

                    //         listJournalView.Add(obj);
                    //     }

                    // }

                    // var journalReport = listJournalView.GroupBy(x => x.ChartOfAccountNewId).ToList();

                    // List<JournalReportViewModel> journalReportList = new List<JournalReportViewModel>();

                    // foreach (var accountItem in journalReport)
                    // {
                    //     journalReportList.Add(new JournalReportViewModel
                    //     {
                    //         ChartOfAccountNewId = accountItem.Key,
                    //         AccountCode = accountItem.FirstOrDefault(x => x.ChartOfAccountNewId == accountItem.Key).AccountCode,
                    //         AccountName = accountItem.FirstOrDefault()?.AccountName,
                    //         DebitAmount = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.DebitAmount)), 4),
                    //         CreditAmount = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.CreditAmount)), 4),
                    //         Balance = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.DebitAmount) - accountItem.Sum(x => x.CreditAmount)), 4)
                    //     });
                    // }


                    // response.data.JournalVoucherViewList = listJournalView;
                    // response.data.JournalReportList = journalReportList; //Report
                    // response.data.TotalCount = voucherDetailsCount;
                    // response.StatusCode = StaticResource.successStatusCode;
                    // response.Message = "Success";
                    #endregion

                    //get Journal Report from sp get_journal_report by passing parameters
                    var spJournalReport = await _uow.GetDbContext().LoadStoredProc("get_journal_report")
                                          .WithSqlParam("currencyid", model.CurrencyId)
                                          .WithSqlParam("recordtype", model.RecordType)
                                          .WithSqlParam("fromdate", model.fromdate.ToString())
                                          .WithSqlParam("todate", model.todate.ToString())
                                          .WithSqlParam("officelist", model.OfficesList)
                                          .WithSqlParam("journalno", model.JournalCode)
                                          .WithSqlParam("accountslist", model.AccountLists)
                                          .ExecuteStoredProc<SPJournalReport>();


                    listJournalView= spJournalReport.Select(x => new JournalVoucherViewModel
                    {
                        AccountCode = x.AccountCode,
                        ChartOfAccountNewId = x.ChartOfAccountNewId,
                        JournalCode = x.JournalCode,
                        CreditAmount = x.CreditAmount,
                        CurrencyId = x.Currency,
                        DebitAmount = x.DebitAmount,
                        ReferenceNo = x.ReferenceNo,
                        TransactionDate = x.TransactionDate,
                        TransactionDescription = x.TransactionDescription,
                        VoucherNo = x.VoucherNo,
                        AccountName= x.AccountName
                    }).ToList();

                    var journalReport= spJournalReport.GroupBy(x => x.ChartOfAccountNewId).ToList();

                    List<JournalReportViewModel> journalReportList = new List<JournalReportViewModel>();

                    foreach (var accountItem in journalReport)
                    {
                        journalReportList.Add(new JournalReportViewModel
                        {
                            ChartOfAccountNewId = accountItem.Key,
                            AccountCode = accountItem.FirstOrDefault(x => x.ChartOfAccountNewId == accountItem.Key).AccountCode,
                            AccountName = accountItem.FirstOrDefault().AccountName,
                            DebitAmount = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.DebitAmount)), 4),
                            CreditAmount = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.CreditAmount)), 4),
                            Balance = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.DebitAmount) - accountItem.Sum(x => x.CreditAmount)), 4)
                        });
                    }


                    response.data.JournalVoucherViewList = listJournalView;
                    response.data.JournalReportList = journalReportList; //Report
                    response.data.TotalCount = voucherDetailsCount;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";


                }
                else
                {
                    response.data.JournalVoucherViewList = null;
                    response.data.TotalCount = voucherDetailsCount;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";

                }
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
                var accountcodelist = (from c in await _uow.ChartOfAccountNewRepository.GetAllAsyn()
                                       where c.IsDeleted == false
                                       select new AccountDetailModel
                                       {
                                           AccountCode = c.ChartOfAccountNewId,
                                           AccountName = c.ChartOfAccountNewCode + " - " + c.AccountName,
                                           ChartOfAccountNewCode = c.ChartOfAccountNewCode,
                                           AccountLevelId = c.AccountLevelId
                                       }).OrderBy(x => x.AccountName).ToList();
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

        public async Task<APIResponse> GetExchangeGainLossVoucherList(int OfficeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var exchangeGainLossVouchers = await _uow.GetDbContext().VoucherDetail.Where(x => x.IsDeleted == false && x.OfficeId == OfficeId
                                                                                      && x.IsExchangeGainLossVoucher == true)
                                                                                      .Select(x => new VoucherDetailModel
                                                                                      {
                                                                                          VoucherNo = x.VoucherNo,
                                                                                          ReferenceNo = x.ReferenceNo
                                                                                      }).ToListAsync();

                response.data.VoucherDetailList = exchangeGainLossVouchers;
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

        public async Task<APIResponse> GetAllControlLevelAccountCode()
        {
            APIResponse response = new APIResponse();
            try
            {
                var accountcodelist = (from c in await _uow.ChartOfAccountNewRepository.GetAllAsyn()
                                       where c.IsDeleted == false
                                       select new AccountDetailModel
                                       {
                                           AccountCode = c.ChartOfAccountNewId,
                                           AccountName = c.ChartOfAccountNewCode + " - " + c.AccountName,
                                           ChartOfAccountNewCode = c.ChartOfAccountNewCode
                                       }).OrderBy(x => x.AccountName).ToList();
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
                //var transactionlist = await _uow.GetDbContext().VoucherTransactionDetails
                //	.Include(x => x.CreditAccountDetails)
                //	//.Include(x => x.DebitAccountDetails)          
                //	.Include(x => x.VoucherDetails)
                //	.Include(x => x.VoucherDetails.ProjectBudgetLine)
                //	.Where(x => x.VoucherDetails.ProjectBudgetLine.ProjectId == projectId && x.VoucherDetails.ProjectBudgetLine.BudgetLineId == budgetLineId && x.IsDeleted == false).ToListAsync();

                //IList<VoucherTransactionModel> tranlist = new List<VoucherTransactionModel>();
                //foreach (var debit in transactionlist)
                //{
                //	VoucherTransactionModel obj = new VoucherTransactionModel();
                //	obj.TransactionId = debit.TransactionId;
                //	obj.TransactionDate = debit.TransactionDate;
                //	obj.VoucherNo = debit.VoucherNo;
                //	obj.Description = debit.Description;
                //	obj.AccountName = debit.DebitAccountDetails.AccountName;
                //	obj.DebitAmount = debit.Amount;
                //	obj.DebitAccount = debit.DebitAccount;
                //	obj.Amount = debit.Amount;
                //	tranlist.Add(obj);
                //	VoucherTransactionModel obj1 = new VoucherTransactionModel();
                //	obj1.TransactionId = debit.TransactionId;
                //	obj1.TransactionDate = debit.TransactionDate;
                //	obj1.VoucherNo = debit.VoucherNo;
                //	obj1.Description = debit.Description;
                //	obj1.AccountName = debit.CreditAccountDetails.AccountName;
                //	obj1.CreditAmount = debit.Amount;
                //	obj1.CreditAccount = debit.CreditAccount;
                //	obj1.Amount = debit.Amount;
                //	tranlist.Add(obj1);
                //}
                //response.data.VoucherTransactionList = tranlist;
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
                //var transactionlist = await _uow.GetDbContext().VoucherTransactionDetails.Include(x => x.CreditAccountDetails).Include(x => x.DebitAccountDetails).Where(x => x.VoucherNo == VoucherNo && x.IsDeleted == false).ToListAsync();
                var transactionlist = await _uow.GetDbContext().VoucherTransactions.Include(x => x.ChartOfAccountDetail).Where(x => x.VoucherNo == VoucherNo && x.IsDeleted == false).ToListAsync();

                IList<VoucherTransactionModel> tranlist = new List<VoucherTransactionModel>();
                foreach (var item in transactionlist)
                {
                    VoucherTransactionModel obj = new VoucherTransactionModel();
                    obj.TransactionId = item.TransactionId;
                    obj.TransactionDate = item.TransactionDate;
                    obj.VoucherNo = item.VoucherNo;
                    obj.Description = item?.Description;
                    obj.DebitAmount = item?.Amount;
                    obj.DebitAccount = item?.DebitAccount;
                    obj.Amount = item.Amount;
                    obj.CreditAmount = item?.Amount;
                    obj.CreditAccount = item?.CreditAccount;

                    //In-Use
                    obj.AccountNo = item.ChartOfAccountNewId;
                    obj.Credit = item?.Credit;
                    obj.Debit = item?.Debit;
                    obj.ProjectId = item?.ProjectId;
                    obj.BudgetLineId = item.BudgetLineId;

                    obj.CreatedDate = item.CreatedDate;
                    obj.ModifiedDate = item.ModifiedDate;
                    tranlist.Add(obj);
                }

                response.data.VoucherTransactionList = tranlist.OrderByDescending(x => x.CreatedDate).ThenBy(x => x.ModifiedDate).ToList();
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

        public async Task<APIResponse> AddVoucherTransactionDetail(List<VoucherTransactionModel> transactions, string UserId)
        {
            APIResponse response = new APIResponse();

            try
            {
                List<VoucherTransactions> xtransactions = await _uow.GetDbContext().VoucherTransactions.Where(x => x.VoucherNo == transactions.FirstOrDefault().VoucherNo).ToListAsync();

                xtransactions.ForEach(x => x.IsDeleted = true);

                _uow.GetDbContext().VoucherTransactions.UpdateRange(xtransactions);
                _uow.GetDbContext().SaveChanges();

                foreach (var model in transactions)
                {
                    var voucherDetail = await _uow.VoucherDetailRepository.FindAsync(x => x.VoucherNo == model.VoucherNo);

                    var exchangeRateAFG = await _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == (int)Currency.AFG);
                    var exchangeRateEUR = await _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == (int)Currency.EUR);
                    var exchangeRatePKR = await _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == (int)Currency.PKR);
                    var exchangeRateUSD = await _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == (int)Currency.USD);

                    if (exchangeRateAFG != null && exchangeRateEUR != null && exchangeRatePKR != null && exchangeRateUSD != null)
                    {
                        //Note : These values are associated with Voucher and Transactions
                        model.TransactionDate = voucherDetail.VoucherDate;
                        model.FinancialYearId = voucherDetail.FinancialYearId;
                        model.CurrencyId = voucherDetail.CurrencyId;

                        VoucherTransactions obj = _mapper.Map<VoucherTransactions>(model);

                        obj.ChartOfAccountNewId = model.AccountNo;
                        obj.CreatedById = UserId;
                        obj.IsDeleted = false;
                        obj.CreatedDate = DateTime.Now;

                        if (obj.CurrencyId == (int)Currency.AFG)
                        {
                            obj.AFGAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit), 4) : Math.Round(Convert.ToDouble(obj.Credit), 4);
                            obj.EURAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * (double)exchangeRateAFG.Rate) / (double)exchangeRateEUR.Rate), 4) : Math.Round(Convert.ToDouble((obj.Credit * (double)exchangeRateAFG.Rate) / (double)exchangeRateEUR.Rate), 4);
                            obj.PKRAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * (double)exchangeRateAFG.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * (double)exchangeRateAFG.Rate)), 4);
                            obj.USDAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * (double)exchangeRateAFG.Rate) / (double)(exchangeRateUSD.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * (double)exchangeRateAFG.Rate) / (double)exchangeRateUSD.Rate), 4);

                        }
                        if (obj.CurrencyId == (int)Currency.EUR)
                        {
                            obj.EURAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit), 4) : Math.Round(Convert.ToDouble(obj.Credit), 4);
                            obj.AFGAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * (double)exchangeRateEUR.Rate) / (double)exchangeRateAFG.Rate), 4) : Math.Round(Convert.ToDouble((obj.Credit * (double)exchangeRateEUR.Rate) / (double)exchangeRateUSD.Rate), 4);
                            obj.PKRAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit * (double)exchangeRateEUR.Rate), 4) : Math.Round(Convert.ToDouble(obj.Credit * (double)exchangeRateEUR.Rate), 4);
                            obj.USDAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * (double)exchangeRateEUR.Rate) / (double)exchangeRateUSD.Rate), 4) : Math.Round(Convert.ToDouble((obj.Credit * (double)exchangeRateEUR.Rate) / (double)exchangeRateUSD.Rate), 4);
                        }
                        if (obj.CurrencyId == (int)Currency.PKR)
                        {

                            obj.PKRAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit), 4) : Math.Round(Convert.ToDouble(obj.Credit), 4);
                            obj.AFGAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit / (double)exchangeRateAFG.Rate), 4) : Math.Round(Convert.ToDouble(obj.Credit / (double)exchangeRateAFG.Rate), 4);
                            obj.EURAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit / (double)exchangeRateEUR.Rate), 4) : Math.Round(Convert.ToDouble(obj.Credit / (double)exchangeRateEUR.Rate), 4);
                            obj.USDAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit / (double)exchangeRateUSD.Rate), 4) : Math.Round(Convert.ToDouble(obj.Credit / (double)exchangeRateUSD.Rate), 4);
                        }
                        if (obj.CurrencyId == (int)Currency.USD)
                        {
                            obj.USDAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit), 4) : Math.Round(Convert.ToDouble(obj.Credit), 4);
                            obj.AFGAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * (double)exchangeRateUSD.Rate) / (double)(exchangeRateAFG.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * (double)exchangeRateUSD.Rate) / (double)(exchangeRateAFG.Rate)), 4);
                            obj.PKRAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * (double)exchangeRateUSD.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * (double)exchangeRateUSD.Rate)), 4);
                            obj.EURAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * (double)exchangeRateUSD.Rate) / (double)(exchangeRateEUR.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * (double)exchangeRateUSD.Rate) / (double)(exchangeRateEUR.Rate)), 4);

                        }

                        obj.ProjectId = model.ProjectId;
                        obj.BudgetLineId = model.BudgetLineId;
                        obj.ChartOfAccountNewId = model.AccountNo;

                        await _uow.GetDbContext().VoucherTransactions.AddAsync(obj);
                        await _uow.SaveAsync();

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";

                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ExchagneRateNotDefined;
                    }

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
                var vouchertransactionInfo = await _uow.VoucherTransactionsRepository.FindAsync(c => c.TransactionId == model.TransactionId);
                if (vouchertransactionInfo != null)
                {
                    var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Date).ToListAsync();

                    vouchertransactionInfo.DebitAccount = model.DebitAccount;
                    vouchertransactionInfo.CreditAccount = model.CreditAccount;
                    vouchertransactionInfo.Amount = model.Amount;
                    vouchertransactionInfo.Description = model.Description;
                    vouchertransactionInfo.CurrencyId = model.CurrencyId;

                    //vouchertransactionInfo.TransactionDate = model.TransactionDate; //should be equals to Voucher Date so don't EDIT 

                    //In-Use
                    vouchertransactionInfo.ChartOfAccountNewId = model.AccountNo;
                    vouchertransactionInfo.Credit = model.Credit;
                    vouchertransactionInfo.Debit = model.Debit;
                    vouchertransactionInfo.ModifiedById = model.ModifiedById;
                    vouchertransactionInfo.ModifiedDate = model.ModifiedDate;

                    if (vouchertransactionInfo.CurrencyId == (int)Currency.AFG)
                    {
                        vouchertransactionInfo.AFGAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble(vouchertransactionInfo.Debit), 4) : Math.Round(Convert.ToDouble(vouchertransactionInfo.Credit), 4);

                        var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            if (exchangeRateToAFG == null)
                            {
                                exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            }
                        }

                        var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            if (exchangeRateToEuro == null)
                            {
                                exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            }
                        }

                        var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            if (exchangeRateToUSD == null)
                            {
                                exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            }
                        }


                        vouchertransactionInfo.EURAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit * exchangeRateToAFG.Rate) / (exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit * exchangeRateToAFG.Rate) / (exchangeRateToEuro.Rate)), 4);
                        vouchertransactionInfo.PKRAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit * exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit * exchangeRateToAFG.Rate)), 4);
                        vouchertransactionInfo.USDAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit * exchangeRateToAFG.Rate) / (exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit * exchangeRateToAFG.Rate) / (exchangeRateToUSD.Rate)), 4);
                    }

                    if (vouchertransactionInfo.CurrencyId == (int)Currency.EUR)
                    {
                        vouchertransactionInfo.EURAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble(vouchertransactionInfo.Debit), 4) : Math.Round(Convert.ToDouble(vouchertransactionInfo.Credit), 4);

                        var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            if (exchangeRateToAFG == null)
                            {
                                exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            }
                        }

                        var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            if (exchangeRateToEuro == null)
                            {
                                exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            }
                        }

                        var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            if (exchangeRateToUSD == null)
                            {
                                exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            }
                        }


                        vouchertransactionInfo.AFGAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit * exchangeRateToEuro.Rate) / (exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit * exchangeRateToEuro.Rate) / (exchangeRateToAFG.Rate)), 4);
                        vouchertransactionInfo.PKRAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit * exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit * exchangeRateToEuro.Rate)), 4);
                        vouchertransactionInfo.USDAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit * exchangeRateToEuro.Rate) / (exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit * exchangeRateToEuro.Rate) / (exchangeRateToUSD.Rate)), 4);
                    }

                    if (vouchertransactionInfo.CurrencyId == (int)Currency.PKR)
                    {

                        vouchertransactionInfo.PKRAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble(vouchertransactionInfo.Debit), 4) : Math.Round(Convert.ToDouble(vouchertransactionInfo.Credit), 4);

                        var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            if (exchangeRateToAFG == null)
                            {
                                exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            }
                        }

                        var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            if (exchangeRateToEuro == null)
                            {
                                exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            }
                        }

                        var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            if (exchangeRateToUSD == null)
                            {
                                exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            }
                        }


                        vouchertransactionInfo.AFGAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit) / (exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit) / (exchangeRateToAFG.Rate)), 4);
                        vouchertransactionInfo.EURAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit) / (exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit) / (exchangeRateToEuro.Rate)), 4);
                        vouchertransactionInfo.USDAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit) / (exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit) / (exchangeRateToUSD.Rate)), 4);
                    }

                    if (vouchertransactionInfo.CurrencyId == (int)Currency.USD)
                    {
                        vouchertransactionInfo.USDAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble(vouchertransactionInfo.Debit), 4) : Math.Round(Convert.ToDouble(vouchertransactionInfo.Credit), 4);

                        var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            if (exchangeRateToAFG == null)
                            {
                                exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            }
                        }

                        var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            if (exchangeRateToEuro == null)
                            {
                                exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            }
                        }

                        var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == vouchertransactionInfo.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            var dateForExchangeRate = vouchertransactionInfo.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            if (exchangeRateToUSD == null)
                            {
                                exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            }
                        }


                        vouchertransactionInfo.AFGAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit * exchangeRateToUSD.Rate) / (exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit * exchangeRateToUSD.Rate) / (exchangeRateToAFG.Rate)), 4);
                        vouchertransactionInfo.PKRAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit * exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit * exchangeRateToUSD.Rate)), 4);
                        vouchertransactionInfo.EURAmount = vouchertransactionInfo.Debit != 0 ? Math.Round(Convert.ToDouble((vouchertransactionInfo.Debit * exchangeRateToUSD.Rate) / (exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((vouchertransactionInfo.Credit * exchangeRateToUSD.Rate) / (exchangeRateToEuro.Rate)), 4);
                    }

                    await _uow.VoucherTransactionsRepository.UpdateAsyn(vouchertransactionInfo);
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


        public async Task<APIResponse> DeleteVoucherTransactionDetail(int transactionId, string modifiedById)
        {
            APIResponse response = new APIResponse();
            try
            {
                var transactionInfo = await _uow.VoucherTransactionsRepository.FindAsync(d => d.TransactionId == transactionId);
                if (transactionInfo != null)
                {
                    transactionInfo.ModifiedById = modifiedById;
                    transactionInfo.ModifiedDate = DateTime.UtcNow;
                    transactionInfo.IsDeleted = true;
                    await _uow.VoucherTransactionsRepository.UpdateAsyn(transactionInfo);
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

        public async Task<APIResponse> DeleteVoucherTransactions(int VoucherId, string modifiedById)
        {
            APIResponse response = new APIResponse();
            try
            {
                var transactionInfo = await _uow.GetDbContext().VoucherTransactions.Where(d => d.VoucherNo == VoucherId && d.IsDeleted == false).ToListAsync();

                if (transactionInfo.Any())
                {

                    foreach (var obj in transactionInfo)
                    {
                        obj.ModifiedById = modifiedById;
                        obj.ModifiedDate = DateTime.UtcNow;
                        obj.IsDeleted = true;
                    }

                    _uow.GetDbContext().VoucherTransactions.RemoveRange(transactionInfo);
                    _uow.GetDbContext().SaveChanges();
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

        public async Task<APIResponse> GetAllLedgerDetailsByCondition(LedgerModels model)
        {
            APIResponse response = new APIResponse();

            #region "new"

            try
            {
                List<LedgerModel> closingLedgerList = new List<LedgerModel>();
                List<LedgerModel> openingLedgerList = new List<LedgerModel>();

                if (model != null)
                {

                    var allCurrencies = await _uow.CurrencyDetailsRepository.FindAllAsync(x => x.IsDeleted == false);
                    var baseCurrency = allCurrencies.FirstOrDefault(x => x.Status == true);

                    Boolean isRecordPresenntForOffice = await _uow.GetDbContext().VoucherDetail
                                                                .AnyAsync(x => x.IsDeleted == false &&
                                                                          model.OfficeIdList.Contains(x.OfficeId.Value) &&
                                                                          x.VoucherDate.Value.Date >= model.fromdate.Date &&
                                                                          x.VoucherDate.Value.Date <= model.todate.Date);

                    if (isRecordPresenntForOffice)
                    {
                        if (model.RecordType == 1)
                        {

                            var spLedgerReportOpening = await _uow.GetDbContext().LoadStoredProc("get_ledger_report")
                                                                  .WithSqlParam("currency", model.CurrencyId)
                                                                  .WithSqlParam("recordtype", model.RecordType)
                                                                  .WithSqlParam("fromdate", model.fromdate.ToString())
                                                                  .WithSqlParam("todate", model.todate.ToString())
                                                                  .WithSqlParam("officelist", model.OfficeIdList)
                                                                  .WithSqlParam("accountslist", model.accountLists)
                                                                  .WithSqlParam("openingbalance", true)
                                                                  .ExecuteStoredProc<SPLedgerReport>();

                            //Opening Calculation
                            openingLedgerList = spLedgerReportOpening.Select(x => new LedgerModel
                            {
                                ChartOfAccountNewId = x.ChartOfAccountNewId,
                                AccountName = x.AccountName,
                                VoucherNo = x.VoucherNo.ToString(),
                                ChartAccountName = x.AccountName,
                                Description = x.Description,
                                VoucherReferenceNo = x.VoucherReferenceNo,
                                CurrencyName = x.CurrencyName,
                                TransactionDate = x.TransactionDate,
                                ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                                CreditAmount = x.CreditAmount,
                                DebitAmount = x.DebitAmount
                            }).ToList();

                            var spLedgerReportClosing = await _uow.GetDbContext().LoadStoredProc("get_ledger_report")
                                                                 .WithSqlParam("currency", model.CurrencyId)
                                                                 .WithSqlParam("recordtype", model.RecordType)
                                                                 .WithSqlParam("fromdate", model.fromdate.ToString())
                                                                 .WithSqlParam("todate", model.todate.ToString())
                                                                 .WithSqlParam("officelist", model.OfficeIdList)
                                                                 .WithSqlParam("accountslist", model.accountLists)
                                                                 .WithSqlParam("openingbalance", false)
                                                                 .ExecuteStoredProc<SPLedgerReport>();

                            closingLedgerList = spLedgerReportClosing.Select(x => new LedgerModel
                            {
                                ChartOfAccountNewId = x.ChartOfAccountNewId,
                                AccountName = x.AccountName,
                                VoucherNo = x.VoucherNo.ToString(),
                                ChartAccountName = x.AccountName,
                                Description = x.Description,
                                VoucherReferenceNo = x.VoucherReferenceNo,
                                CurrencyName = x.CurrencyName,
                                TransactionDate = x.TransactionDate,
                                ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                                CreditAmount = x.CreditAmount,
                                DebitAmount = x.DebitAmount
                            }).ToList();

                            #region old code for single

                            //// ICollection<ChartAccountDetail> accountLevel4 = null;     //level 4
                            // List<long> accountLevel3 = new List<long>();              //level 3
                            // List<long> accountLevel2 = new List<long>();              //level 2
                            //                                                           //ICollection<ChartAccountDetail> accountLevel2 = null;     //level 2

                            //foreach (var accountItem in accountDetail)
                            //{
                            //    if (accountItem.AccountLevelId == 4)
                            //    {
                            //        openingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                    .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                     x.AccountNo == accountItem.AccountCode &&
                            //                                                      model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                     x.CurrencyId == model.CurrencyId &&
                            //                                                     x.TransactionDate.Value.Date < model.fromdate.Date);



                            //        closingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == accountItem.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date >= model.fromdate.Date &&
                            //                                                    x.TransactionDate.Value.Date <= model.todate.Date);
                            //        //Opening calculation
                            //        foreach (var item in openingTransactionDetail)
                            //        {
                            //            if (model.CurrencyId == item.CurrencyId)
                            //            {
                            //                LedgerModel obj = new LedgerModel();

                            //                obj.AccountCode = item.AccountNo.Value;
                            //                obj.AccountName = accountItem.AccountName;
                            //                obj.VoucherNo = item.VoucherNo.ToString();
                            //                obj.ChartAccountName = accountItem.AccountName;
                            //                obj.Description = item.Description;
                            //                obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                obj.CreditAmount = Math.Round(Convert.ToDouble(item.Credit));
                            //                obj.DebitAmount = Math.Round(Convert.ToDouble(item.Debit));
                            //                obj.TransactionDate = item.TransactionDate;

                            //                openingLedgerList.Add(obj);
                            //            }
                            //        }

                            //        //Closing calculation
                            //        foreach (var item in closingTransactionDetail)
                            //        {
                            //            if (model.CurrencyId == item.CurrencyId)
                            //            {
                            //                LedgerModel obj = new LedgerModel();

                            //                obj.AccountCode = item.AccountNo.Value;
                            //                obj.AccountName = accountItem.AccountName;
                            //                obj.VoucherNo = item.VoucherNo.ToString();
                            //                obj.ChartAccountName = accountItem.AccountName;
                            //                obj.Description = item.Description;
                            //                obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                obj.CreditAmount = Math.Round(Convert.ToDouble(item.Credit));
                            //                obj.DebitAmount = Math.Round(Convert.ToDouble(item.Debit));
                            //                obj.TransactionDate = item.TransactionDate;

                            //                closingLedgerList.Add(obj);
                            //            }
                            //        }

                            //    }
                            //    else if (accountItem.AccountLevelId == 3)
                            //    {
                            //        // Gets the fourth level accounts
                            //        accountLevel4 = await _uow.ChartAccountDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.ParentID == accountItem.AccountCode && x.AccountLevelId == 4);

                            //        foreach (var elements in accountLevel4)
                            //        {
                            //            openingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date < model.fromdate.Date);

                            //            closingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date >= model.fromdate.Date &&
                            //                                                    x.TransactionDate.Value.Date <= model.todate.Date);

                            //            //Opening calculation
                            //            foreach (var item in openingTransactionDetail)
                            //            {
                            //                if (model.CurrencyId == item.CurrencyId)
                            //                {
                            //                    LedgerModel obj = new LedgerModel();

                            //                    obj.AccountCode = item.AccountNo.Value;
                            //                    //obj.AccountName = accountItem.AccountName;
                            //                    obj.AccountName = elements.AccountName;
                            //                    obj.VoucherNo = item.VoucherNo.ToString();
                            //                    obj.ChartAccountName = accountItem.AccountName;
                            //                    obj.Description = item.Description;
                            //                    obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                    obj.CreditAmount = Math.Round(Convert.ToDouble(item.Credit));
                            //                    obj.DebitAmount = Math.Round(Convert.ToDouble(item.Debit));
                            //                    obj.TransactionDate = item.TransactionDate;

                            //                    openingLedgerList.Add(obj);
                            //                }
                            //            }

                            //            //Closing calculation
                            //            foreach (var item in closingTransactionDetail)
                            //            {
                            //                if (model.CurrencyId == item.CurrencyId)
                            //                {
                            //                    LedgerModel obj = new LedgerModel();

                            //                    obj.AccountCode = item.AccountNo.Value;
                            //                    //obj.AccountName = accountItem.AccountName;
                            //                    obj.AccountName = elements.AccountName;
                            //                    obj.VoucherNo = item.VoucherNo.ToString();
                            //                    obj.ChartAccountName = accountItem.AccountName;
                            //                    obj.Description = item.Description;
                            //                    obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                    obj.CreditAmount = Math.Round(Convert.ToDouble(item.Credit));
                            //                    obj.DebitAmount = Math.Round(Convert.ToDouble(item.Debit));
                            //                    obj.TransactionDate = item.TransactionDate;

                            //                    closingLedgerList.Add(obj);
                            //                }
                            //            }

                            //        }

                            //    }
                            //    else if (accountItem.AccountLevelId == 2)
                            //    {
                            //        // Gets the third level accounts
                            //        accountLevel3 = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && x.ParentID == accountItem.AccountCode && x.AccountLevelId == 3).Select(x => x.ChartOfAccountCode).ToListAsync();
                            //        // Gets the fourth level accounts
                            //        accountLevel4 = await _uow.ChartAccountDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.AccountLevelId == 4 && accountLevel3.Contains(x.ParentID));

                            //        foreach (var elements in accountLevel4)
                            //        {
                            //            openingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date < model.fromdate.Date);

                            //            closingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date >= model.fromdate.Date &&
                            //                                                    x.TransactionDate.Value.Date <= model.todate.Date);

                            //            //Opening calculation
                            //            foreach (var item in openingTransactionDetail)
                            //            {
                            //                if (model.CurrencyId == item.CurrencyId)
                            //                {
                            //                    LedgerModel obj = new LedgerModel();

                            //                    obj.AccountCode = item.AccountNo.Value;
                            //                    //obj.AccountName = accountItem.AccountName;
                            //                    obj.AccountName = elements.AccountName;
                            //                    obj.VoucherNo = item.VoucherNo.ToString();
                            //                    obj.ChartAccountName = accountItem.AccountName;
                            //                    obj.Description = item.Description;
                            //                    obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                    obj.CreditAmount = Math.Round(Convert.ToDouble(item.Credit));
                            //                    obj.DebitAmount = Math.Round(Convert.ToDouble(item.Debit));
                            //                    obj.TransactionDate = item.TransactionDate;

                            //                    openingLedgerList.Add(obj);
                            //                }
                            //            }

                            //            //Closing calculation
                            //            foreach (var item in closingTransactionDetail)
                            //            {
                            //                if (model.CurrencyId == item.CurrencyId)
                            //                {
                            //                    LedgerModel obj = new LedgerModel();

                            //                    obj.AccountCode = item.AccountNo.Value;
                            //                    //obj.AccountName = accountItem.AccountName;
                            //                    obj.AccountName = elements.AccountName;
                            //                    obj.VoucherNo = item.VoucherNo.ToString();
                            //                    obj.ChartAccountName = accountItem.AccountName;
                            //                    obj.Description = item.Description;
                            //                    obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                    obj.CreditAmount = Math.Round(Convert.ToDouble(item.Credit));
                            //                    obj.DebitAmount = Math.Round(Convert.ToDouble(item.Debit));
                            //                    obj.TransactionDate = item.TransactionDate;

                            //                    closingLedgerList.Add(obj);
                            //                }
                            //            }

                            //        }


                            //    }
                            //    else if (accountItem.AccountLevelId == 1)
                            //    {
                            //        // Gets the second level accounts
                            //        accountLevel2 = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && x.ParentID == accountItem.AccountCode && x.AccountLevelId == 2).Select(x => x.ChartOfAccountCode).ToListAsync();

                            //        // Gets the level 3rd accounts
                            //        accountLevel3 = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && accountLevel2.Contains(x.ParentID) && x.AccountLevelId == 3).Select(x => x.ChartOfAccountCode).ToListAsync();

                            //        // Gets the fourth level accounts
                            //        accountLevel4 = await _uow.ChartAccountDetailRepository.FindAllAsync(x => x.IsDeleted == false && accountLevel3.Contains(x.ParentID) && x.AccountLevelId == 4);

                            //        foreach (var elements in accountLevel4)
                            //        {
                            //            openingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date < model.fromdate.Date);

                            //            closingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date >= model.fromdate.Date &&
                            //                                                    x.TransactionDate.Value.Date <= model.todate.Date);

                            //            //Opening Calculation
                            //            foreach (var item in openingTransactionDetail)
                            //            {
                            //                if (model.CurrencyId == item.CurrencyId)
                            //                {
                            //                    LedgerModel obj = new LedgerModel();

                            //                    obj.AccountCode = item.AccountNo.Value;
                            //                    //obj.AccountName = accountItem.AccountName;
                            //                    obj.AccountName = elements.AccountName;
                            //                    obj.VoucherNo = item.VoucherNo.ToString();
                            //                    obj.ChartAccountName = accountItem.AccountName;
                            //                    obj.Description = item.Description;
                            //                    obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                    obj.CreditAmount = Math.Round(Convert.ToDouble(item.Credit));
                            //                    obj.DebitAmount = Math.Round(Convert.ToDouble(item.Debit));
                            //                    obj.TransactionDate = item.TransactionDate;

                            //                    openingLedgerList.Add(obj);
                            //                }
                            //            }

                            //            //Closing Calculation
                            //            foreach (var item in closingTransactionDetail)
                            //            {
                            //                if (model.CurrencyId == item.CurrencyId)
                            //                {
                            //                    LedgerModel obj = new LedgerModel();

                            //                    obj.AccountCode = item.AccountNo.Value;
                            //                    //obj.AccountName = accountItem.AccountName;
                            //                    obj.AccountName = elements.AccountName;
                            //                    obj.VoucherNo = item.VoucherNo.ToString();
                            //                    obj.ChartAccountName = accountItem.AccountName;
                            //                    obj.Description = item.Description;
                            //                    obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                    obj.CreditAmount = Math.Round(Convert.ToDouble(item.Credit));
                            //                    obj.DebitAmount = Math.Round(Convert.ToDouble(item.Debit));
                            //                    obj.TransactionDate = item.TransactionDate;

                            //                    closingLedgerList.Add(obj);
                            //                }
                            //            }

                            //        }
                            //    }

                            //}

                            #endregion

                            response.data.AccountOpendingAndClosingBL = new AccountOpendingAndClosingBL
                            {
                                OpeningBalance = Math.Round(Convert.ToDouble(openingLedgerList.Sum(x => x.DebitAmount) - openingLedgerList.Sum(x => x.CreditAmount))),
                                //ClosingBalance = opening + closing
                                ClosingBalance = Math.Round(Convert.ToDouble(openingLedgerList.Sum(x => x.DebitAmount) - openingLedgerList.Sum(x => x.CreditAmount) + (closingLedgerList.Sum(x => x.DebitAmount) - closingLedgerList.Sum(x => x.CreditAmount))))

                            };
                        }
                        else
                        {
                            //Consolidate

                            var spLedgerReportOpening = await _uow.GetDbContext().LoadStoredProc("get_ledger_report")
                                                                   .WithSqlParam("currency", model.CurrencyId)
                                                                   .WithSqlParam("recordtype", model.RecordType)
                                                                   .WithSqlParam("fromdate", model.fromdate.ToString())
                                                                   .WithSqlParam("todate", "")
                                                                   .WithSqlParam("officelist", model.OfficeIdList)
                                                                   .WithSqlParam("accountslist", model.accountLists)
                                                                   .WithSqlParam("openingbalance", true)
                                                                   .ExecuteStoredProc<SPLedgerReport>();

                            openingLedgerList = spLedgerReportOpening.Select(x => new LedgerModel
                            {
                                ChartOfAccountNewId = x.ChartOfAccountNewId,
                                AccountName= x.AccountName,
                                VoucherNo= x.VoucherNo.ToString(),
                                ChartAccountName= x.AccountName,
                                Description= x.Description,
                                VoucherReferenceNo= x.VoucherReferenceNo,
                                CurrencyName= x.CurrencyName,
                                TransactionDate= x.TransactionDate,
                                ChartOfAccountNewCode= x.ChartOfAccountNewCode,
                                CreditAmount= x.CreditAmount,
                                DebitAmount= x.DebitAmount
                            }).ToList();


                            var spLedgerReportClosing = await _uow.GetDbContext().LoadStoredProc("get_ledger_report")
                                                                  .WithSqlParam("currency", model.CurrencyId)
                                                                  .WithSqlParam("recordtype", model.RecordType)
                                                                  .WithSqlParam("fromdate", model.fromdate.ToString())
                                                                  .WithSqlParam("todate", model.todate.ToString())
                                                                  .WithSqlParam("officelist", model.OfficeIdList)
                                                                  .WithSqlParam("accountslist", model.accountLists)
                                                                  .WithSqlParam("openingbalance", false)
                                                                  .ExecuteStoredProc<SPLedgerReport>();

                            closingLedgerList= spLedgerReportClosing.Select(x => new LedgerModel
                            {
                                ChartOfAccountNewId = x.ChartOfAccountNewId,
                                AccountName = x.AccountName,
                                VoucherNo = x.VoucherNo.ToString(),
                                ChartAccountName = x.AccountName,
                                Description = x.Description,
                                VoucherReferenceNo = x.VoucherReferenceNo,
                                CurrencyName = x.CurrencyName,
                                TransactionDate = x.TransactionDate,
                                ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                                CreditAmount = x.CreditAmount,
                                DebitAmount = x.DebitAmount
                            }).ToList();

                            #region Old Code for consolidated

                            ////ICollection<ChartAccountDetail> accountLevel4 = null;     //level 4
                            //List<long> accountLevel3 = new List<long>();              //level 3
                            //List<long> accountLevel2 = new List<long>();              //level 2

                            //ICollection<ExchangeRate> exchangeRate = await _uow.ExchangeRateRepository.FindAllAsync(x => x.IsDeleted == false && x.Date.Value.Date <= model.todate.Date);

                            //foreach (var accountItem in accountDetail)
                            //{
                            //    if (accountItem.AccountLevelId == 4)
                            //    {
                            //        openingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                     .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                      x.AccountNo == accountItem.AccountCode &&
                            //                                                      model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                      x.TransactionDate.Value.Date < model.fromdate.Date);

                            //        closingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                     .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                      x.AccountNo == accountItem.AccountCode &&
                            //                                                      model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                      x.TransactionDate.Value.Date >= model.fromdate.Date &&
                            //                                                      x.TransactionDate.Value.Date <= model.todate.Date);

                            //        //Opening Calculation
                            //        foreach (var item in openingTransactionDetail)
                            //        {
                            //            LedgerModel obj = new LedgerModel();

                            //            obj.AccountCode = item.AccountNo.Value;
                            //            obj.AccountName = accountItem.AccountName;
                            //            obj.VoucherNo = item.VoucherNo.ToString();
                            //            obj.ChartAccountName = accountItem.AccountName;
                            //            obj.Description = item.Description;
                            //            obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //            obj.TransactionDate = item.TransactionDate;

                            //            if (model.CurrencyId == (int)Currency.PKR)
                            //            {

                            //                obj.CreditAmount = item.Credit != 0 ? item.PKRAmount : 0;
                            //                obj.DebitAmount = item.Debit != 0 ? item.PKRAmount : 0;
                            //            }
                            //            else if (model.CurrencyId == (int)Currency.AFG)
                            //            {

                            //                obj.CreditAmount = item.Credit != 0 ? item.AFGAmount : 0;
                            //                obj.DebitAmount = item.Debit != 0 ? item.AFGAmount : 0;
                            //            }
                            //            else if (model.CurrencyId == (int)Currency.EUR)
                            //            {

                            //                obj.CreditAmount = item.Credit != 0 ? item.EURAmount : 0;
                            //                obj.DebitAmount = item.Debit != 0 ? item.EURAmount : 0;
                            //            }
                            //            else
                            //            {

                            //                obj.CreditAmount = item.Credit != 0 ? item.USDAmount : 0;
                            //                obj.DebitAmount = item.Debit != 0 ? item.USDAmount : 0;
                            //            }

                            //            openingLedgerList.Add(obj);
                            //        }

                            //        //Closing Calculation
                            //        foreach (var item in closingTransactionDetail)
                            //        {
                            //            LedgerModel obj = new LedgerModel();

                            //            obj.AccountCode = item.AccountNo.Value;
                            //            obj.AccountName = accountItem.AccountName;
                            //            obj.VoucherNo = item.VoucherNo.ToString();
                            //            obj.ChartAccountName = accountItem.AccountName;
                            //            obj.Description = item.Description;
                            //            obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //            obj.TransactionDate = item.TransactionDate;

                            //            if (model.CurrencyId == (int)Currency.PKR)
                            //            {

                            //                obj.CreditAmount = item.Credit != 0 ? item.PKRAmount : 0;
                            //                obj.DebitAmount = item.Debit != 0 ? item.PKRAmount : 0;
                            //            }
                            //            else if (model.CurrencyId == (int)Currency.USD)
                            //            {

                            //                obj.CreditAmount = item.Credit != 0 ? item.USDAmount : 0;
                            //                obj.DebitAmount = item.Debit != 0 ? item.USDAmount : 0;
                            //            }
                            //            else if (model.CurrencyId == (int)Currency.AFG)
                            //            {

                            //                obj.CreditAmount = item.Credit != 0 ? item.AFGAmount : 0;
                            //                obj.DebitAmount = item.Debit != 0 ? item.AFGAmount : 0;
                            //            }
                            //            else
                            //            {

                            //                obj.CreditAmount = item.Credit != 0 ? item.EURAmount : 0;
                            //                obj.DebitAmount = item.Debit != 0 ? item.EURAmount : 0;
                            //            }

                            //            closingLedgerList.Add(obj);
                            //        }

                            //    }
                            //    else if (accountItem.AccountLevelId == 3)
                            //    {
                            //        // Gets the fourth level accounts
                            //        accountLevel4 = await _uow.ChartAccountDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.ParentID == accountItem.AccountCode && x.AccountLevelId == 4);

                            //        foreach (var elements in accountLevel4)
                            //        {
                            //            openingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date < model.fromdate.Date);

                            //            closingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date >= model.fromdate.Date &&
                            //                                                    x.TransactionDate.Value.Date <= model.todate.Date);

                            //            //Opening calculation
                            //            foreach (var item in openingTransactionDetail)
                            //            {
                            //                LedgerModel obj = new LedgerModel();

                            //                obj.AccountCode = item.AccountNo.Value;
                            //                //obj.AccountName = accountItem.AccountName;
                            //                obj.AccountName = elements.AccountName;
                            //                obj.VoucherNo = item.VoucherNo.ToString();
                            //                obj.ChartAccountName = accountItem.AccountName;
                            //                obj.Description = item.Description;
                            //                obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                obj.TransactionDate = item.TransactionDate;

                            //                if (model.CurrencyId == (int)Currency.AFG)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.AFGAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.AFGAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.PKR)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.PKRAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.PKRAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.USD)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.USDAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.USDAmount : 0;
                            //                }
                            //                else
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.EURAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.EURAmount : 0;
                            //                }

                            //                openingLedgerList.Add(obj);
                            //            }

                            //            //Closing calculation
                            //            foreach (var item in closingTransactionDetail)
                            //            {

                            //                LedgerModel obj = new LedgerModel();

                            //                obj.AccountCode = item.AccountNo.Value;
                            //                //obj.AccountName = accountItem.AccountName;
                            //                obj.AccountName = elements.AccountName;
                            //                obj.VoucherNo = item.VoucherNo.ToString();
                            //                obj.ChartAccountName = accountItem.AccountName;
                            //                obj.Description = item.Description;
                            //                obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                obj.TransactionDate = item.TransactionDate;

                            //                if (model.CurrencyId == (int)Currency.PKR)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.PKRAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.PKRAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.USD)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.USDAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.USDAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.AFG)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.AFGAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.AFGAmount : 0;
                            //                }
                            //                else
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.EURAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.EURAmount : 0;

                            //                }

                            //                closingLedgerList.Add(obj);
                            //            }

                            //        }

                            //    }
                            //    else if (accountItem.AccountLevelId == 2)
                            //    {
                            //        // Gets the third level accounts
                            //        accountLevel3 = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && x.ParentID == accountItem.AccountCode && x.AccountLevelId == 3).Select(x => x.ChartOfAccountCode).ToListAsync();
                            //        // Gets the fourth level accounts
                            //        accountLevel4 = await _uow.ChartAccountDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.AccountLevelId == 4 && accountLevel3.Contains(x.ParentID));

                            //        foreach (var elements in accountLevel4)
                            //        {
                            //            openingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date < model.fromdate.Date);

                            //            closingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date >= model.fromdate.Date &&
                            //                                                    x.TransactionDate.Value.Date <= model.todate.Date);

                            //            //Opening calculation
                            //            foreach (var item in openingTransactionDetail)
                            //            {
                            //                LedgerModel obj = new LedgerModel();

                            //                obj.AccountCode = item.AccountNo.Value;
                            //                //obj.AccountName = accountItem.AccountName;
                            //                obj.AccountName = elements.AccountName;
                            //                obj.VoucherNo = item.VoucherNo.ToString();
                            //                obj.ChartAccountName = accountItem.AccountName;
                            //                obj.Description = item.Description;
                            //                obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                obj.TransactionDate = item.TransactionDate;

                            //                if (model.CurrencyId == (int)Currency.AFG)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.AFGAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.AFGAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.PKR)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.PKRAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.PKRAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.USD)
                            //                {
                            //                    obj.CreditAmount = item.Credit != 0 ? item.USDAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.USDAmount : 0;
                            //                }
                            //                else
                            //                {
                            //                    obj.CreditAmount = item.Credit != 0 ? item.EURAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.EURAmount : 0;
                            //                }

                            //                openingLedgerList.Add(obj);
                            //            }

                            //            //Closing calculation
                            //            foreach (var item in closingTransactionDetail)
                            //            {
                            //                LedgerModel obj = new LedgerModel();

                            //                obj.AccountCode = item.AccountNo.Value;
                            //                //obj.AccountName = accountItem.AccountName;
                            //                obj.AccountName = elements.AccountName;
                            //                obj.VoucherNo = item.VoucherNo.ToString();
                            //                obj.ChartAccountName = accountItem.AccountName;
                            //                obj.Description = item.Description;
                            //                obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                obj.TransactionDate = item.TransactionDate;

                            //                if (model.CurrencyId == (int)Currency.PKR)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.PKRAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.PKRAmount : 0;

                            //                }
                            //                else if (model.CurrencyId == (int)Currency.USD)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.USDAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.USDAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.AFG)
                            //                {
                            //                    obj.CreditAmount = item.Credit != 0 ? item.AFGAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.AFGAmount : 0;
                            //                }
                            //                else
                            //                {
                            //                    obj.CreditAmount = item.Credit != 0 ? item.EURAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.EURAmount : 0;
                            //                }
                            //                closingLedgerList.Add(obj);
                            //            }

                            //        }


                            //    }
                            //    else if (accountItem.AccountLevelId == 1)
                            //    {
                            //        // Gets the second level accounts
                            //        accountLevel2 = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && x.ParentID == accountItem.AccountCode && x.AccountLevelId == 2).Select(x => x.ChartOfAccountCode).ToListAsync();

                            //        // Gets the level 3rd accounts
                            //        accountLevel3 = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && accountLevel2.Contains(x.ParentID) && x.AccountLevelId == 3).Select(x => x.ChartOfAccountCode).ToListAsync();

                            //        // Gets the fourth level accounts
                            //        accountLevel4 = await _uow.ChartAccountDetailRepository.FindAllAsync(x => x.IsDeleted == false && accountLevel3.Contains(x.ParentID) && x.AccountLevelId == 4);

                            //        foreach (var elements in accountLevel4)
                            //        {
                            //            openingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date >= model.fromdate.Date &&
                            //                                                    x.TransactionDate.Value.Date <= model.todate.Date);

                            //            closingTransactionDetail = await _uow.VoucherTransactionsRepository
                            //                                   .FindAllAsync(x => x.IsDeleted == false &&
                            //                                                    x.AccountNo == elements.AccountCode &&
                            //                                                    model.OfficeIdList.Contains(x.OfficeId.Value) &&
                            //                                                    x.CurrencyId == model.CurrencyId &&
                            //                                                    x.TransactionDate.Value.Date >= model.fromdate.Date &&
                            //                                                    x.TransactionDate.Value.Date <= model.todate.Date);

                            //            //opening calculation
                            //            foreach (var item in openingTransactionDetail)
                            //            {
                            //                LedgerModel obj = new LedgerModel();

                            //                obj.AccountCode = item.AccountNo.Value;
                            //                //obj.AccountName = accountItem.AccountName;
                            //                obj.AccountName = elements.AccountName;
                            //                obj.VoucherNo = item.VoucherNo.ToString();
                            //                obj.ChartAccountName = accountItem.AccountName;
                            //                obj.Description = item.Description;
                            //                obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                obj.TransactionDate = item.TransactionDate;

                            //                if (model.CurrencyId == (int)Currency.AFG)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.AFGAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.AFGAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.PKR)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.PKRAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.PKRAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.USD)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.USDAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.USDAmount : 0;
                            //                }
                            //                else
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.EURAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.EURAmount : 0;
                            //                }

                            //                openingLedgerList.Add(obj);

                            //            }

                            //            //Closing calculation
                            //            foreach (var item in closingTransactionDetail)
                            //            {

                            //                LedgerModel obj = new LedgerModel();

                            //                obj.AccountCode = item.AccountNo.Value;
                            //                //obj.AccountName = accountItem.AccountName;
                            //                obj.AccountName = elements.AccountName;
                            //                obj.VoucherNo = item.VoucherNo.ToString();
                            //                obj.ChartAccountName = accountItem.AccountName;
                            //                obj.Description = item.Description;
                            //                obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == item.CurrencyId)?.CurrencyName;
                            //                obj.TransactionDate = item.TransactionDate;

                            //                if (model.CurrencyId == (int)Currency.PKR)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.PKRAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.PKRAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.USD)
                            //                {
                            //                    obj.CreditAmount = item.Credit != 0 ? item.USDAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.USDAmount : 0;
                            //                }
                            //                else if (model.CurrencyId == (int)Currency.AFG)
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.AFGAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.AFGAmount : 0;
                            //                }
                            //                else
                            //                {

                            //                    obj.CreditAmount = item.Credit != 0 ? item.EURAmount : 0;
                            //                    obj.DebitAmount = item.Debit != 0 ? item.EURAmount : 0;
                            //                }

                            //                closingLedgerList.Add(obj);
                            //            }
                            //        }
                            //    }
                            //}

                            #endregion

                            response.data.AccountOpendingAndClosingBL = new AccountOpendingAndClosingBL
                            {
                                OpeningBalance = Math.Round(Convert.ToDouble(openingLedgerList.Sum(x => x.DebitAmount) - openingLedgerList.Sum(x => x.CreditAmount))),
                                //ClosingBalance = opening + closing
                                ClosingBalance = Math.Round(Convert.ToDouble(openingLedgerList.Sum(x => x.DebitAmount) - openingLedgerList.Sum(x => x.CreditAmount) + (closingLedgerList.Sum(x => x.DebitAmount) - closingLedgerList.Sum(x => x.CreditAmount))))

                                //ClosingBalance = debitSum - creditSum + lst.Sum(x => x.TotalDebits) - lst.Sum(x => x.TotalCredits)
                            };
                        }
                    }
                }

                #region "report data"

                var ledgerByAccount = closingLedgerList.GroupBy(x => x.ChartOfAccountNewId).ToList();

                List<LedgerReportViewModel> ledgerReportFinal = new List<LedgerReportViewModel>();

                foreach (var accountItem in ledgerByAccount)
                {
                    ledgerReportFinal.Add(new LedgerReportViewModel
                    {
                        AccountName = accountItem.FirstOrDefault().AccountName,
                        LedgerList = accountItem.ToList(),
                        DebitAmount = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.DebitAmount)), 4),
                        CreditAmount = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.CreditAmount)), 4),
                        Balance = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.DebitAmount) - accountItem.Sum(x => x.CreditAmount)), 4)
                    });
                }

                #endregion


                response.data.LedgerList = closingLedgerList;
                response.data.ledgerReportFinal = ledgerReportFinal;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }

            #endregion

            #region "Old code"

            //try
            //{
            //    List<ChartAccountDetail> ledgerList = new List<ChartAccountDetail>();

            //    //ledgerList = await _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).Where(x => x.AccountLevelId == 4 && model.accountLists.Contains(x.AccountCode)).ToListAsync();

            //    //Note : No need of DebitAccountlist
            //    ledgerList = await _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Where(x => x.AccountLevelId == 4 && model.accountLists.Contains(x.AccountCode)).ToListAsync();

            //    var allCurrencies = await _uow.CurrencyDetailsRepository.FindAllAsync(x => x.IsDeleted == false);
            //    var baseCurrency = allCurrencies.FirstOrDefault(x => x.Status == true);


            //    //foreach (var account in model.accountLists)
            //    //{
            //    //	ChartAccountDetail obj = new ChartAccountDetail();
            //    //	obj = await _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).Where(x => x.AccountLevelId == 4 && x.AccountCode == account).FirstOrDefaultAsync();
            //    //	if (obj != null)
            //    //	{
            //    //		if (obj.CreditAccountlist.Count > 0 || obj.DebitAccountlist.Count > 0)
            //    //			ledgerList.Add(obj);
            //    //	}
            //    //}

            //    DateTime defaultdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
            //    if (model.fromdate == null && model.todate == null)
            //    {
            //        model.fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
            //        model.todate = DateTime.UtcNow;
            //    }

            //    double? totalCredit = 0, totalDebit = 0, totalCreditPrevious = 0, totalDebitPrevious = 0;
            //    List<LedgerModel> finalLedgerList = new List<LedgerModel>();
            //    List<AccountTransactionLogger> lst = new List<AccountTransactionLogger>();

            //    foreach (var items in ledgerList)
            //    {
            //        // CHecks for the previous transactions of a particular account before the date range (For OPENING BALANCE)
            //        //Note:  User Credit, Debit as Amount

            //        //var accountData = await _uow.VoucherTransactionDetailsRepository.FindAllAsync(x => x.IsDeleted == false && (x.CreditAccount == items.AccountCode || x.DebitAccount == items.AccountCode) && x.TransactionDate.Value.Date >= new DateTime(model.fromdate.Year, 1, 1).Date && x.TransactionDate.Value.Date < model.fromdate.Date);

            //        //var accountData = await _uow.VoucherTransactionDetailsRepository.FindAllAsync(x => x.IsDeleted == false && (x.AccountNo == items.AccountCode) && x.TransactionDate.Value.Date >= new DateTime(model.fromdate.Year, 1, 1).Date && x.TransactionDate.Value.Date < model.fromdate.Date);

            //        foreach (var elements in items.CreditAccountlist)
            //        {

            //            var voucherDetail = _uow.VoucherDetailRepository.FindAsync(x => x.VoucherNo == elements.VoucherNo)?.Result;

            //            if (elements.AccountNo == items.AccountCode)
            //            {

            //                if (model.RecordType == 1)
            //                {
            //                    if (voucherDetail.CurrencyId == model.CurrencyId)
            //                    {
            //                        totalCreditPrevious += elements.Credit;
            //                        totalDebitPrevious += elements.Debit;
            //                    }
            //                }
            //                else
            //                {
            //                    if (voucherDetail.CurrencyId == model.CurrencyId)
            //                    {
            //                        totalCreditPrevious += elements.Credit;
            //                        totalDebitPrevious += elements.Debit;
            //                    }
            //                    else
            //                    {

            //                        var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == voucherDetail.CurrencyId && x.Date.Value.Date <= voucherDetail.VoucherDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

            //                        if (baseCurrency.CurrencyId == model.CurrencyId)
            //                        {
            //                            totalCreditPrevious += Math.Round(elements.Credit * exchangeRate.Rate ?? 0, 2);
            //                            totalDebitPrevious += Math.Round(elements.Debit * exchangeRate.Rate ?? 0, 2);
            //                        }
            //                        else
            //                        {
            //                            var exchangeRate2 = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == model.CurrencyId && x.Date.Value.Date <= voucherDetail.VoucherDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

            //                            totalCreditPrevious += Math.Round((elements.Credit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
            //                            totalDebitPrevious += Math.Round((elements.Debit * exchangeRate.Rate) / exchangeRate2?.Rate ?? 0, 2);
            //                        }

            //                    }

            //                }
            //            }


            //            AccountTransactionLogger logger = new AccountTransactionLogger();
            //            logger.ClosingBalance = totalDebitPrevious - totalCreditPrevious;
            //            logger.TotalCredits = totalCreditPrevious;
            //            logger.TotalDebits = totalDebitPrevious;
            //            logger.AccountCode = items.AccountCode;
            //            logger.ChartAccountCode = items.ChartOfAccountCode;
            //            lst.Add(logger);

            //        }
            //    }

            //    var newList = lst.GroupBy(x => x.AccountCode).ToList();

            //    lst = new List<AccountTransactionLogger>();

            //    foreach (var final in newList)
            //    {
            //        totalCreditPrevious = final.Sum(x => x.TotalCredits);
            //        totalDebitPrevious = final.Sum(x => x.TotalDebits);
            //        AccountTransactionLogger logger = new AccountTransactionLogger();
            //        logger.ClosingBalance = totalDebitPrevious - totalCreditPrevious;
            //        logger.TotalCredits = totalCreditPrevious;
            //        logger.TotalDebits = totalDebitPrevious;
            //        logger.AccountCode = final.Key;
            //        lst.Add(logger);
            //    }

            //    foreach (var items in ledgerList)
            //    {
            //        //foreach (var creditList in items.CreditAccountlist)
            //        foreach (var list in items.CreditAccountlist)
            //        {

            //            if (list.TransactionDate.Value.Date >= model.fromdate.Date && list.TransactionDate.Value.Date <= model.todate.Date)
            //            {
            //                //var currencyName = await _uow.CurrencyDetailsRepository.FindAsync(x => x.CurrencyId == list.CurrencyId);


            //                //var voucherReferenceNo = await _uow.VoucherDetailRepository.FindAsync(x => x.IsDeleted == false && x.VoucherNo == list.VoucherNo);
            //                var voucherDetail = _uow.GetDbContext().VoucherDetail.Include(x => x.CurrencyDetail).FirstOrDefault(x => x.IsDeleted == false && x.VoucherNo == list.VoucherNo);

            //                //RecordType: Single, Consolidated
            //                if (model.RecordType == 1)
            //                {
            //                    if (voucherDetail.CurrencyId == model.CurrencyId)
            //                    {
            //                        LedgerModel obj = new LedgerModel();
            //                        obj.TransactionDate = list.TransactionDate;
            //                        obj.VoucherNo = voucherDetail?.ReferenceNo ?? null;
            //                        obj.Description = list.Description;
            //                        obj.CurrencyName = voucherDetail?.CurrencyDetail.CurrencyName ?? null;
            //                        obj.AccountCode = items.ChartOfAccountCode;
            //                        obj.AccountName = items.AccountName;
            //                        obj.CreditAmount = list.Credit;
            //                        obj.DebitAmount = list.Debit;

            //                        finalLedgerList.Add(obj);

            //                        totalCredit += list.Credit;
            //                        totalDebit += list.Debit;
            //                    }

            //                }
            //                else
            //                {
            //                    if (voucherDetail.CurrencyId == model.CurrencyId)
            //                    {
            //                        LedgerModel obj = new LedgerModel();
            //                        obj.TransactionDate = list.TransactionDate;
            //                        obj.VoucherNo = voucherDetail?.ReferenceNo ?? null;
            //                        obj.Description = list.Description;
            //                        //obj.CurrencyName = currencyName?.CurrencyName ?? null;
            //                        obj.CurrencyName = voucherDetail?.CurrencyDetail.CurrencyName ?? null;
            //                        obj.AccountCode = items.ChartOfAccountCode;
            //                        obj.AccountName = items.AccountName;
            //                        obj.CreditAmount = list.Credit;
            //                        obj.DebitAmount = list.Debit;

            //                        finalLedgerList.Add(obj);

            //                        totalCredit += list.Credit;
            //                        totalDebit += list.Debit;
            //                    }
            //                    else
            //                    {

            //                        var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == voucherDetail.CurrencyId && x.Date.Value.Date <= voucherDetail.VoucherDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

            //                        if (baseCurrency.CurrencyId == model.CurrencyId)
            //                        {
            //                            LedgerModel obj = new LedgerModel();
            //                            obj.TransactionDate = list.TransactionDate;
            //                            obj.VoucherNo = voucherDetail?.ReferenceNo ?? null;
            //                            obj.Description = list.Description;
            //                            //obj.CurrencyName = currencyName?.CurrencyName ?? null;
            //                            obj.CurrencyName = voucherDetail?.CurrencyDetail.CurrencyName ?? null;
            //                            obj.AccountCode = items.ChartOfAccountCode;
            //                            obj.AccountName = items.AccountName;
            //                            obj.CreditAmount = Math.Round(list.Credit * exchangeRate?.Rate ?? 0, 2);
            //                            obj.DebitAmount = Math.Round(list.Debit * exchangeRate?.Rate ?? 0, 2);

            //                            finalLedgerList.Add(obj);

            //                            totalCredit += Math.Round(list.Credit * exchangeRate?.Rate ?? 0, 2);
            //                            totalDebit += Math.Round(list.Debit * exchangeRate?.Rate ?? 0, 2);

            //                        }
            //                        else
            //                        {
            //                            var exchangeRate2 = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == model.CurrencyId && x.Date.Value.Date <= voucherDetail.VoucherDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

            //                            LedgerModel obj = new LedgerModel();
            //                            obj.TransactionDate = list.TransactionDate;
            //                            obj.VoucherNo = voucherDetail?.ReferenceNo ?? null;
            //                            obj.Description = list.Description;
            //                            //obj.CurrencyName = currencyName?.CurrencyName ?? null;
            //                            obj.CurrencyName = voucherDetail?.CurrencyDetail.CurrencyName ?? null;
            //                            obj.AccountCode = items.ChartOfAccountCode;
            //                            obj.AccountName = items.AccountName;
            //                            obj.CreditAmount = Math.Round((list.Credit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
            //                            obj.DebitAmount = Math.Round((list.Debit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);

            //                            finalLedgerList.Add(obj);

            //                            totalCredit += Math.Round((list.Credit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
            //                            totalDebit += Math.Round((list.Debit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
            //                        }
            //                    }

            //                }
            //            }
            //        }
            //    }

            //    // Till Now Final Ledger List is having all transactions of given date range of all accounts passed in accountList && newlist is having all previous transactions sum  account wise.


            //    // For opening And Closing Amount Calculation
            //    double? creditSum = 0, debitSum = 0;
            //    creditSum = finalLedgerList.Where(x => x.CreditAmount != 0).Sum(x => x.CreditAmount);
            //    debitSum = finalLedgerList.Where(x => x.DebitAmount != 0).Sum(x => x.DebitAmount);

            //    //foreach (var element in finalLedgerList)
            //    //{                    
            //    //    creditSum += finalLedgerList.Where(x => x.AccountCode == element.AccountCode && x.CreditAmount != 0).Sum(x => x.CreditAmount);
            //    //    debitSum = finalLedgerList.Where(x => x.AccountCode == element.AccountCode && x.DebitAmount != 0).Sum(x => x.DebitAmount);
            //    //    balance += creditSum - debitSum + element.ClosingBalance;             // element.ClosingBalance - for previous transaction amount 
            //    //}

            //    response.data.AccountOpendingAndClosingBL = new AccountOpendingAndClosingBL
            //    {
            //        OpeningBalance = lst.Sum(x => x.TotalDebits) - lst.Sum(x => x.TotalCredits),
            //        //ClosingBalance = creditSum - debitSum + lst.Sum(x => x.TotalCredits) - lst.Sum(x => x.TotalDebits)
            //        ClosingBalance = debitSum - creditSum + lst.Sum(x => x.TotalDebits) - lst.Sum(x => x.TotalCredits)
            //    };

            //    response.data.LedgerList = finalLedgerList;
            //    response.StatusCode = StaticResource.successStatusCode;
            //    response.Message = "Success";
            //}
            //catch (Exception ex)
            //{
            //    response.StatusCode = StaticResource.failStatusCode;
            //    response.Message = StaticResource.SomethingWrong + ex.Message;
            //}

            #endregion

            return response;
        }

        public async Task<APIResponse> GetTrailBlanceDetailsByCondition(LedgerModels model)
        {
            APIResponse response = new APIResponse();
            try
            {
                DateTime defaultdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                if (model.fromdate == null && model.todate == null)
                {
                    model.fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                    model.todate = DateTime.UtcNow;
                }


                if (model != null)
                {
                    List<LedgerModel> finalTrialBalanceList = new List<LedgerModel>();

                    ICollection<CurrencyDetails> allCurrencies = await _uow.CurrencyDetailsRepository.FindAllAsync(x => x.IsDeleted == false);
                    //CurrencyDetails baseCurrency = allCurrencies.FirstOrDefault(x => x.Status == true);

                    //ICollection<ChartOfAccountNew> accountDetail = await _uow.ChartOfAccountNewRepository.FindAllAsync(x => model.accountLists.Contains(x.ChartOfAccountNewId));

                    #region Commented code for selecting 4th level accounts from level 1, 2, 3 Accounts when UI dropdown contains All Accounts(Level 1, 2, 3, 4)

                    //List<long> accountLevel4 = new List<long>();     //level 4

                    //foreach (var accountItem in accountDetail)
                    //{
                    //    if (accountItem.AccountLevelId == 4)
                    //    {
                    //        // Gets the fourth level accounts  
                    //        var fourL = await _uow.GetDbContext().ChartAccountDetail.Where(x => accountItem.AccountCode == x.AccountCode && x.AccountLevelId == 4).Select(x => x.ChartOfAccountCode).ToListAsync();

                    //        accountLevel4.AddRange(fourL);
                    //    }
                    //    else if (accountItem.AccountLevelId == 3)
                    //    {
                    //        var threeL = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.ParentID == accountItem.AccountCode && x.AccountLevelId == 4).Select(x => x.ChartOfAccountCode).ToListAsync();

                    //        accountLevel4.AddRange(threeL);
                    //    }
                    //    else if (accountItem.AccountLevelId == 2)
                    //    {
                    //        // Gets the third level accounts
                    //        var thirdL = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.ParentID == accountItem.AccountCode && x.AccountLevelId == 3).Select(x => x.ChartOfAccountCode).ToListAsync();
                    //        // Gets the fourth level accounts
                    //        var fourL = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.AccountLevelId == 4 && thirdL.Contains(x.ParentID)).Select(x => x.ChartOfAccountCode).ToListAsync();

                    //        accountLevel4.AddRange(fourL);
                    //    }
                    //    else if (accountItem.AccountLevelId == 1)
                    //    {
                    //        // Gets the second level accounts
                    //        var secondL = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.ParentID == accountItem.AccountCode && x.AccountLevelId == 2).Select(x => x.ChartOfAccountCode).ToListAsync();

                    //        // Gets the level 3rd accounts
                    //        var thirdL = await _uow.GetDbContext().ChartAccountDetail.Where(x => secondL.Contains(x.ParentID) && x.AccountLevelId == 3).Select(x => x.ChartOfAccountCode).ToListAsync();

                    //        // Gets the fourth level accounts
                    //        var fourthL = await _uow.GetDbContext().ChartAccountDetail.Where(x => thirdL.Contains(x.ParentID) && x.AccountLevelId == 4).Select(x => x.ChartOfAccountCode).ToListAsync();


                    //        accountLevel4.AddRange(fourthL);
                    //    }

                    //}

                    #endregion

                    var accountFourthLevel = model.accountLists;


                    // Single
                    if (model.RecordType == 1)
                    {
                        //get trialbalance report from Stored Procedure get_trialbalance_report
                        var spTrialBalanceReport = await _uow.GetDbContext().LoadStoredProc("get_trialbalance_report")
                                                                    .WithSqlParam("currency", model.CurrencyId)
                                                                    .WithSqlParam("recordtype", model.RecordType)
                                                                    .WithSqlParam("fromdate", model.fromdate.ToString())
                                                                    .WithSqlParam("todate", model.todate.ToString())
                                                                    .WithSqlParam("officelist", model.OfficesList)
                                                                    .WithSqlParam("accountslist", model.accountLists)
                                                                    .ExecuteStoredProc<SP_TrialBalanceModel>();

                        var transactionDetail = spTrialBalanceReport.Select(x => new LedgerModel
                        {
                            AccountName = x.AccountName,
                            ChartOfAccountNewId = x.ChartOfAccountNewId,
                            Description = x.Description,
                            CurrencyName = x.CurrencyName,
                            CreditAmount = x.CreditAmount,
                            DebitAmount = x.DebitAmount,
                            TransactionDate = x.TransactionDate,
                            ChartOfAccountNewCode = x.ChartOfAccountNewCode

                        }).ToList();

                        List<LedgerModel> transactionDetail1 = new List<LedgerModel>();

                        var accountGroup = transactionDetail.GroupBy(x => x.ChartOfAccountNewId);

                        foreach (var item in accountGroup)
                        {
                            LedgerModel obj = new LedgerModel();

                            obj = item.FirstOrDefault();

                            var debit = item.Sum(x => x.DebitAmount);
                            var credit = item.Sum(x => x.CreditAmount);

                            if (debit > credit)
                            {
                                obj.DebitAmount = debit - credit;
                                obj.CreditAmount = 0;
                            }
                            else if (debit < credit)
                            {
                                obj.DebitAmount = 0;
                                obj.CreditAmount = credit - debit;
                            }
                            else if (debit == credit)
                            {
                                obj.DebitAmount = 0;
                                obj.CreditAmount = 0;
                            }

                            finalTrialBalanceList.Add(obj);
                        }

                        var noTransactionAccounts = accountFourthLevel.Except(accountGroup.Select(x => (x.Key)));

                        var allAccountDetails = _uow.ChartOfAccountNewRepository.FindAll(x => x.IsDeleted == false);

                        foreach (var detail in noTransactionAccounts)
                        {
                            LedgerModel obj = new LedgerModel();
                            var noTransactionAccount = allAccountDetails.FirstOrDefault(x => x.ChartOfAccountNewId == detail);

                            obj.ChartOfAccountNewId = noTransactionAccount.ChartOfAccountNewId;
                            obj.AccountName = noTransactionAccount.AccountName;
                            obj.ChartAccountName = noTransactionAccount.AccountName;
                            obj.Description = "";
                            obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == model.CurrencyId)?.CurrencyName;
                            obj.TransactionDate = null;
                            obj.DebitAmount = 0;
                            obj.CreditAmount = 0;
                            obj.ChartOfAccountNewCode = noTransactionAccount.ChartOfAccountNewCode;

                            finalTrialBalanceList.Add(obj);
                        }
                    }
                    else
                    {
                        var accountFourthLevelNotNull = accountFourthLevel.ConvertAll(x => x);


                        List<LedgerModel> trialBalanceList = new List<LedgerModel>();
                        finalTrialBalanceList = new List<LedgerModel>();

                        var spTrialbalanceReport = await _uow.GetDbContext().LoadStoredProc("get_trialbalance_report")
                                                                    .WithSqlParam("currency", model.CurrencyId)
                                                                    .WithSqlParam("recordtype", model.RecordType)
                                                                    .WithSqlParam("fromdate", model.fromdate.ToString())
                                                                    .WithSqlParam("todate", model.todate.ToString())
                                                                    .WithSqlParam("officelist", model.OfficesList)
                                                                    .WithSqlParam("accountslist", model.accountLists)
                                                                    .ExecuteStoredProc<SP_TrialBalanceModel>();

                        trialBalanceList= spTrialbalanceReport.Select(x=> new LedgerModel
                        {
                        ChartOfAccountNewId = x.ChartOfAccountNewId,
                        AccountName = x.AccountName,
                        ChartAccountName = x.AccountName,
                        Description = x.Description,
                        CurrencyName = x.CurrencyName,
                        TransactionDate = x.TransactionDate,
                        ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                        CreditAmount = x.CreditAmount,
                        DebitAmount = x.DebitAmount
                        }).ToList();

                        var accountGroup = trialBalanceList.GroupBy(x => x.ChartOfAccountNewId);

                        var noTransactionAccounts = accountFourthLevelNotNull.Except(accountGroup.Select(x => (x.Key)));

                        foreach (var item in accountGroup)
                        {
                            LedgerModel obj = new LedgerModel();

                            obj = item.FirstOrDefault();

                            var debit = item.Sum(x => x.DebitAmount);
                            var credit = item.Sum(x => x.CreditAmount);

                            if (debit > credit)
                            {
                                obj.DebitAmount = debit - credit;
                                obj.CreditAmount = 0;
                            }
                            else if (debit < credit)
                            {
                                obj.DebitAmount = 0;
                                obj.CreditAmount = credit - debit;
                            }
                            else if (debit == credit)
                            {
                                obj.DebitAmount = 0;
                                obj.CreditAmount = 0;
                            }

                            finalTrialBalanceList.Add(obj);

                        }

                        var allAccountDetails = _uow.ChartOfAccountNewRepository.FindAll(x => x.IsDeleted == false);

                        foreach (var detail in noTransactionAccounts)
                        {
                            LedgerModel obj = new LedgerModel();
                            var noTransactionAccount = allAccountDetails.FirstOrDefault(x => x.ChartOfAccountNewId == detail);

                            obj.ChartOfAccountNewId = noTransactionAccount.ChartOfAccountNewId;
                            obj.AccountName = noTransactionAccount.AccountName;
                            obj.ChartAccountName = noTransactionAccount.AccountName;
                            obj.Description = "";
                            obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == model.CurrencyId)?.CurrencyName;
                            obj.TransactionDate = null;
                            obj.DebitAmount = 0;
                            obj.CreditAmount = 0;
                            obj.ChartOfAccountNewCode = noTransactionAccount.ChartOfAccountNewCode;

                            finalTrialBalanceList.Add(obj);
                        }
                    }

                    response.data.TrailBlanceList = finalTrialBalanceList;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.data.TrailBlanceList = null;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "No data Found";
                }

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
                var list = await _uow.GetDbContext().ProjectBudgetLine.Select(x => new BudgetLineModel
                {
                    BudgetLineId = x.BudgetLineId,
                    ProjectId = x.ProjectId,
                    Description = x.Description
                }).ToListAsync();

                var list1 = await _uow.GetDbContext().ProjectDetails.
                    Select(x => new ProjectBudgetModelNew
                    {
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
                    existrecord.ChartOfAccountNewId = model.AccountCode;
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
                    _uow.GetDbContext().NotesMaster.Include(a => a.AccountType).Include(c => c.ChartOfAccountNew).Where(x => x.IsDeleted == false).ToListAsync()
                );
                var noteslist = list.Select(x => new NotesMasterModel
                {
                    NoteId = x.NoteId,
                    Notes = x.Notes,
                    AccountCode = x.ChartOfAccountNewId,
                    ChartOfAccountCode = x.ChartOfAccountNew.ChartOfAccountNewCode,
                    Narration = x.Narration,
                    BlanceType = x.BlanceType,
                    BlanceTypeName = x.BlanceType == (int)BalanceType.SUM ? "Sum" : x.BlanceType == (int)BalanceType.CR ? "Cr" : x.BlanceType == (int)BalanceType.DR ? "Dr" : "",
                    FinancialReportTypeId = x.FinancialReportTypeId,
                    FinancialReportTypeName = x.FinancialReportTypeId == (int)FinancialReportType.BALANCESHEET ? "Blance Sheet" : x.FinancialReportTypeId == (int)FinancialReportType.INCOMEANDEXPANCE ? "Income and Expance" : "",
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

        /// <summary>
        /// This Api is for: 1.Balance Sheet 
        ///                  2.Financial Report
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetBlanceSheetDetails(FinancialReportModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<CategoryPopulator> cpList = await _uow.GetDbContext().CategoryPopulator.Include(o => o.AccountType).Where(x => x.IsDeleted == false && x.AccountType.AccountCategory == model.financialreporttype).ToListAsync();

                FinancialYearDetail financialYear = null;
                if (model.financialreporttype == 1 && model.SelectType == 2)
                {
                    financialYear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.EndDate.Date >= model.EndDate.Value.Date && x.StartDate.Date <= model.EndDate.Value.Date);
                }
                List<ChartOfAccountNew> allAccounts = new List<ChartOfAccountNew>();
                allAccounts = await _uow.GetDbContext().ChartOfAccountNew/*.Include(x => x.CreditAccountlist).Include(x => x.DebitAccountlist)*/
                        .Select(x => new ChartOfAccountNew
                        {
                            ChartOfAccountNewId = x.ChartOfAccountNewId,
                            AccountLevelId = x.AccountLevelId,
                            AccountLevels = x.AccountLevels,
                            AccountName = x.AccountName,
                            AccountType = x.AccountType,
                            AccountTypeId = x.AccountTypeId,
                            ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                            CreatedById = x.CreatedById,
                            CreatedDate = x.CreatedDate,
                            ParentID = x.ParentID,
                            //CreditAccountDetails = x.CreditAccountDetails,
                            //CreditAccountlist = x.CreditAccountlist.Where(o => o.TransactionDate.Value.Date >= model.StartDate.Value.Date && o.TransactionDate.Value.Date <= model.EndDate.Value.Date).ToList(),
                            //DebitAccountlist = x.DebitAccountlist.Where(o => o.TransactionDate.Value.Date >= model.StartDate.Value.Date && o.TransactionDate.Value.Date <= model.EndDate.Value.Date).ToList()
                        }).ToListAsync();

                ChartOfAccountNew accountDetails = null;

                List<BalanceSheetModel> lstBalanceSheetModel = new List<BalanceSheetModel>();
                foreach (var items in cpList)
                {

                    accountDetails = allAccounts.Find(x => x.ChartOfAccountNewCode == items.ChartOfAccountCodeNew);       // Just used for finding the details of this account

                    double? balanceAmount = 0;

                    List<ChartOfAccountNew> accountsLevelFourth = new List<ChartOfAccountNew>();
                    // IEnumerable<long> accountsLevelOne = null;
                    IEnumerable<long> accountsLevelTwo = null;
                    IEnumerable<long> accountsLevelThird = null;

                    if (accountDetails.AccountLevelId == (int)AccountLevels.MainLevel) //1
                    {
                        // Gets the level 2nd accounts
                        accountsLevelTwo = allAccounts.FindAll(x => x.ParentID == accountDetails.ChartOfAccountNewId && x.AccountLevelId == (int)AccountLevels.ControlLevel).Select(x => x.ChartOfAccountNewId); //3

                        // Gets the level 3rd accounts
                        accountsLevelThird = allAccounts.FindAll(x => accountsLevelTwo.Contains(x.ParentID) && x.AccountLevelId == (int)AccountLevels.SubLevel).Select(x => x.ChartOfAccountNewId); //3

                        // Gets the level 3rd accounts
                        accountsLevelFourth = allAccounts.FindAll(x => x.AccountLevelId == (int)AccountLevels.InputLevel && accountsLevelThird.Contains(x.ParentID)); //4

                    }
                    else if (accountDetails.AccountLevelId == (int)AccountLevels.ControlLevel) //2
                    {
                        // Gets the level 3rd accounts
                        accountsLevelThird = allAccounts.FindAll(x => x.ParentID == accountDetails.ChartOfAccountNewId && x.AccountLevelId == (int)AccountLevels.SubLevel).Select(x => x.ChartOfAccountNewId); //3
                        accountsLevelFourth = allAccounts.FindAll(x => x.AccountLevelId == (int)AccountLevels.InputLevel && accountsLevelThird.Contains(x.ParentID)); //4


                    }
                    else if (accountDetails.AccountLevelId == (int)AccountLevels.SubLevel) //3
                    {
                        // Gets the level 3rd accounts
                        accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == accountDetails.ChartOfAccountNewId && x.AccountLevelId == (int)AccountLevels.InputLevel); //4


                    }
                    else if (accountDetails.AccountLevelId == (int)AccountLevels.InputLevel) //4
                    {
                        // Gets the level 3rd accounts
                        accountsLevelFourth = allAccounts.FindAll(x => x.ChartOfAccountNewId == accountDetails.ChartOfAccountNewId && x.AccountLevelId == (int)AccountLevels.InputLevel); //4


                    }
                    #region "Calculations"

                    ICollection<VoucherTransactions> transactionDetail = null;
                    foreach (var elements in accountsLevelFourth)
                    {
                        // Gets the transactions for level 4th account
                        if (model.financialreporttype == 1)                 // For Balance Sheet
                        {
                            //Single
                            if (model.SelectType == 1)                      // For transactions from starting
                            {
                                if (model.currencyid == (int)Currency.AFG)               // For AFG
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 model.OfficeList.Contains(x.OfficeId) &&
                                                                x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.AFGAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.AFGAmount : 0) ?? 0
                                                                }
                                                                ).ToListAsync();
                                }
                                if (model.currencyid == (int)Currency.EUR)                // For EURO
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 model.OfficeList.Contains(x.OfficeId) &&
                                                                x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.EURAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.EURAmount : 0) ?? 0
                                                                }
                                                                ).ToListAsync();
                                }
                                if (model.currencyid == (int)Currency.PKR)               // For PKR
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 model.OfficeList.Contains(x.OfficeId) &&
                                                                x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.PKRAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.PKRAmount : 0) ?? 0
                                                                }
                                                                ).ToListAsync();
                                }
                                if (model.currencyid == (int)Currency.USD)               // For USD
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 model.OfficeList.Contains(x.OfficeId) &&
                                                                x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.USDAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.USDAmount : 0) ?? 0,
                                                                }
                                                                ).ToListAsync();
                                }
                            }
                            else                                          // For transactions within financial year
                            {
                                //Consolidated
                                if (model.currencyid == (int)Currency.AFG)               // For AFG
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 model.OfficeList.Contains(x.OfficeId) &&
                                                                x.TransactionDate.Value.Date >= financialYear.StartDate.Date &&
                                                                x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.AFGAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.AFGAmount : 0) ?? 0
                                                                }
                                                                ).ToListAsync();
                                }
                                if (model.currencyid == (int)Currency.EUR)               // For EURO
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 model.OfficeList.Contains(x.OfficeId) &&
                                                                 x.TransactionDate.Value.Date >= financialYear.StartDate.Date &&
                                                                x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.EURAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.EURAmount : 0) ?? 0
                                                                }
                                                                ).ToListAsync();
                                }
                                if (model.currencyid == (int)Currency.PKR)              // For PKR
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 model.OfficeList.Contains(x.OfficeId) &&
                                                                 x.TransactionDate.Value.Date >= financialYear.StartDate.Date &&
                                                                x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.PKRAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.PKRAmount : 0) ?? 0
                                                                }
                                                                ).ToListAsync();
                                }
                                if (model.currencyid == (int)Currency.USD)               // For USD
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 model.OfficeList.Contains(x.OfficeId) &&
                                                                 x.TransactionDate.Value.Date >= financialYear.StartDate.Date &&
                                                                x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.USDAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.USDAmount : 0) ?? 0,
                                                                }
                                                                ).ToListAsync();
                                }
                            }
                        }
                        else                                             // For Income and Expense
                        {
                            if (model.currencyid == (int)Currency.AFG)               // For AFG
                            {
                                transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                            .Where(x => x.IsDeleted == false &&
                                                            x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                             model.OfficeList.Contains(x.OfficeId) &&
                                                             x.TransactionDate.Value.Date >= model.StartDate.Value.Date &&
                                                            x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                            {
                                                                Credit = (x.Credit > 0 ? x.AFGAmount : 0) ?? 0,
                                                                Debit = (x.Debit > 0 ? x.AFGAmount : 0) ?? 0
                                                            }
                                                            ).ToListAsync();
                            }
                            if (model.currencyid == (int)Currency.EUR)               // For EURO
                            {
                                transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                            .Where(x => x.IsDeleted == false &&
                                                            x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                             model.OfficeList.Contains(x.OfficeId) &&
                                                             x.TransactionDate.Value.Date >= model.StartDate.Value.Date &&
                                                            x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                            {
                                                                Credit = (x.Credit > 0 ? x.EURAmount : 0) ?? 0,
                                                                Debit = (x.Debit > 0 ? x.EURAmount : 0) ?? 0
                                                            }
                                                            ).ToListAsync();
                            }
                            if (model.currencyid == (int)Currency.PKR)               // For PKR
                            {
                                transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                            .Where(x => x.IsDeleted == false &&
                                                            x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                             model.OfficeList.Contains(x.OfficeId) &&
                                                             x.TransactionDate.Value.Date >= model.StartDate.Value.Date &&
                                                            x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                            {
                                                                Credit = (x.Credit > 0 ? x.PKRAmount : 0) ?? 0,
                                                                Debit = (x.Debit > 0 ? x.PKRAmount : 0) ?? 0
                                                            }
                                                            ).ToListAsync();
                            }
                            if (model.currencyid == (int)Currency.USD)              // For USD
                            {
                                transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                            .Where(x => x.IsDeleted == false &&
                                                            x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                             model.OfficeList.Contains(x.OfficeId) &&
                                                             x.TransactionDate.Value.Date >= model.StartDate.Value.Date &&
                                                            x.TransactionDate.Value.Date <= model.EndDate.Value.Date).Select(x => new VoucherTransactions
                                                            {
                                                                Credit = (x.Credit > 0 ? x.USDAmount : 0) ?? 0,
                                                                Debit = (x.Debit > 0 ? x.USDAmount : 0) ?? 0,
                                                            }
                                                            ).ToListAsync();
                            }
                        }

                        #region "For calculations of Balance TYPE"
                        if (items.ValueSource == (int)BalanceType.SUM)
                        {
                            balanceAmount += transactionDetail.Sum(x => x.Debit) - transactionDetail.Sum(x => x.Credit);
                        }

                        else if (items.ValueSource == (int)BalanceType.DR)
                        {
                            balanceAmount += transactionDetail.Sum(x => x.Credit);
                        }

                        else if (items.ValueSource == (int)BalanceType.CR)
                        {
                            balanceAmount += transactionDetail.Sum(x => x.Debit);
                        }

                        #endregion
                    }

                    #endregion

                    BalanceSheetModel balanceSheetObj = new BalanceSheetModel();
                    balanceSheetObj.Narration = items.SubCategoryLabel;
                    balanceSheetObj.Balance = Math.Round(Convert.ToDouble(balanceAmount), 4);
                    balanceSheetObj.AccountTypeId = items.AccountTypeId;
                    lstBalanceSheetModel.Add(balanceSheetObj);


                }

                BalanceSheet bal = new BalanceSheet();

                if (model.financialreporttype == 1)
                {
                    //1
                    var capData = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.CapitalAssetsWrittenOff);

                    bal.CapitalAssetsWrittenOff = capData
                        .Select(x => new BalanceSheetModel
                        {
                            //Note = x..Notes,
                            Narration = x.Narration,
                            Balance = x.Balance

                        }).ToList();


                    //2
                    var currentAssetsData = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.CurrentAssets);

                    bal.CurrentAssets = currentAssetsData
                        .Select(x => new BalanceSheetModel
                        {
                            Narration = x.Narration,
                            Balance = x.Balance

                        }).ToList();


                    //3
                    var fundsData = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.Funds);

                    bal.Funds = fundsData
                        .Select(x => new BalanceSheetModel
                        {
                            Narration = x.Narration,
                            Balance = x.Balance

                        }).ToList();


                    //4
                    var endownmentFundData = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.EndownmentFund);

                    bal.EndownmentFund = endownmentFundData
                        .Select(x => new BalanceSheetModel
                        {
                            Narration = x.Narration,
                            Balance = x.Balance

                        }).ToList();

                    bal.ReserveAccountAdjustment = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.ReserveAccountAdjustment)
                        .Select(x => new BalanceSheetModel
                        {
                            Narration = x.Narration,
                            Balance = x.Balance

                        }).ToList();

                    bal.LongtermLiability = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.LongtermLiability)
                        .Select(x => new BalanceSheetModel
                        {
                            Narration = x.Narration,
                            Balance = x.Balance

                        }).ToList();
                    bal.CurrentLiability = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.CurrentLiability)
                      .Select(x => new BalanceSheetModel
                      {
                          Narration = x.Narration,
                          Balance = x.Balance

                      }).ToList();
                    bal.ReserveAccount = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.ReserveAccount)
                      .Select(x => new BalanceSheetModel
                      {
                          Narration = x.Narration,
                          Balance = x.Balance

                      }).ToList();

                }

                else if (model.financialreporttype == 2)
                {
                    bal.IncomeFromDonor = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.IncomeFromDonor)
                      .Select(x => new BalanceSheetModel
                      {
                          Narration = x.Narration,
                          Balance = x.Balance

                      }).ToList();


                    bal.IncomeFromProjects = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.IncomeFromProjects)
                      .Select(x => new BalanceSheetModel
                      {
                          Narration = x.Narration,
                          Balance = x.Balance

                      }).ToList();
                    bal.ProfitOnBankDeposits = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.ProfitOnBankDeposits)
                      .Select(x => new BalanceSheetModel
                      {
                          Narration = x.Narration,
                          Balance = x.Balance

                      }).ToList();
                    bal.IncomeExpenditureFund = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.IncomeExpenditureFund)
                      .Select(x => new BalanceSheetModel
                      {
                          Narration = x.Narration,
                          Balance = x.Balance

                      }).ToList();
                }

                response.data.BalanceSheet = bal;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }


            #region "Old Data"
            //try
            //         {
            //             var cpList = await _uow.GetDbContext().CategoryPopulator.Include(o => o.AccountType).Where(x => x.IsDeleted == false && x.AccountType.AccountCategory == model.financialreporttype).ToListAsync();
            //             //var allAccounts = await _uow.GetDbContext().ChartAccountDetail.Include(x=>x.CreditAccountlist).Include(x=>x.DebitAccountlist).ToListAsync();
            //             List<ChartAccountDetail> allAccounts = new List<ChartAccountDetail>();
            //             // For INCOME/EXPENSE REPORT
            //             if (model.StartDate != null)
            //             {
            //                 allAccounts = await _uow.GetDbContext().ChartAccountDetail.Include(x => x.CreditAccountlist).Include(x => x.DebitAccountlist)
            //                     .Select(x => new ChartAccountDetail
            //                     {
            //                         AccountCode = x.AccountCode,
            //                         AccountLevelId = x.AccountLevelId,
            //                         AccountLevels = x.AccountLevels,
            //                         AccountName = x.AccountName,
            //                         AccountType = x.AccountType,
            //                         AccountTypeId = x.AccountTypeId,
            //                         ChartOfAccountCode = x.ChartOfAccountCode,
            //                         CreatedById = x.CreatedById,
            //                         CreatedDate = x.CreatedDate,
            //                         ParentID = x.ParentID,
            //                         //CreditAccountDetails = x.CreditAccountDetails,
            //                         CreditAccountlist = x.CreditAccountlist.Where(o => o.TransactionDate.Value.Date >= model.StartDate.Value.Date && o.TransactionDate.Value.Date <= model.EndDate.Value.Date).ToList(),
            //                         DebitAccountlist = x.DebitAccountlist.Where(o => o.TransactionDate.Value.Date >= model.StartDate.Value.Date && o.TransactionDate.Value.Date <= model.EndDate.Value.Date).ToList()
            //                     }).ToListAsync();
            //             }
            //             // For BALANCE SHEET REPORT
            //             else
            //             {
            //                 var financialYearDetails = await _uow.FinancialYearDetailRepository.FindAsync(x => x.StartDate.Date <= model.EndDate.Value.Date && x.EndDate.Date >= model.EndDate.Value.Date);
            //                 //allAccounts = await _uow.GetDbContext().ChartAccountDetail.Include(x => x.CreditAccountlist).Where(x => x.CreditAccountlist.Any(o => o.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date && o.TransactionDate.Value.Date <= model.EndDate.Value.Date)).ToListAsync();
            //                 allAccounts = await _uow.GetDbContext().ChartAccountDetail.Include(x => x.CreditAccountlist)
            //                 .Select(x => new ChartAccountDetail
            //                 {
            //                     AccountCode = x.AccountCode,
            //                     AccountLevelId = x.AccountLevelId,
            //                     AccountLevels = x.AccountLevels,
            //                     AccountName = x.AccountName,
            //                     AccountType = x.AccountType,
            //                     AccountTypeId = x.AccountTypeId,
            //                     ChartOfAccountCode = x.ChartOfAccountCode,
            //                     CreatedById = x.CreatedById,
            //                     CreatedDate = x.CreatedDate,
            //                     ParentID = x.ParentID,
            //                     //CreditAccountDetails = x.CreditAccountDetails,
            //                     CreditAccountlist = x.CreditAccountlist.Where(o => o.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date && o.TransactionDate.Value.Date <= model.EndDate.Value.Date).ToList()
            //                     //DebitAccountlist = x.DebitAccountlist.Where(o => o.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date && o.TransactionDate.Value.Date <= model.EndDate.Value.Date).ToList()
            //                 }).ToListAsync();
            //             }
            //             List<BalanceSheetModel> lstBalanceSheetModel = new List<BalanceSheetModel>();
            //             foreach (var items in cpList)
            //             {

            //                 var accountDetails = allAccounts.Find(x => x.ChartOfAccountCode == items.ChartOfAccountCode);       // Just used for finding the details of this account

            //                 double? creditAmount = 0, debitAmount = 0, balanceAmount = 0;
            //                 if (accountDetails.AccountLevelId == 4)
            //                 {
            //                     // Gets the transactions for level 4th account
            //                     var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == items.ChartOfAccountCode).ToList();

            //                     #region "For calculations of Balance TYPE"
            //                     if (items.ValueSource == (int)BalanceType.SUM)
            //                     {
            //                         foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                         {

            //                             if (transactionCalcuate.CreditAccountlist.Count > 0)
            //                             {
            //                                 if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                 {
            //                                     creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
            //                                 }
            //                                 else
            //                                 {
            //                                     var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                     creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                 }
            //                             }

            //                             if (transactionCalcuate.DebitAccountlist.Count > 0)
            //                             {
            //                                 if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                 {
            //                                     debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
            //                                 }
            //                                 else
            //                                 {
            //                                     var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                     debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                 }
            //                             }
            //                         }
            //                         balanceAmount = debitAmount - creditAmount;
            //                     }

            //                     else if (items.ValueSource == (int)BalanceType.DR)
            //                     {
            //                         foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                         {

            //                             if (transactionCalcuate.CreditAccountlist.Count > 0)
            //                             {
            //                                 if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                 {
            //                                     creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
            //                                 }
            //                                 else
            //                                 {
            //                                     var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                     creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                 }
            //                             }

            //                         }
            //                         balanceAmount = creditAmount;
            //                     }

            //                     else if (items.ValueSource == (int)BalanceType.CR)
            //                     {
            //                         foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                         {

            //                             if (transactionCalcuate.DebitAccountlist.Count > 0)
            //                             {
            //                                 if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                 {
            //                                     debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
            //                                 }
            //                                 else
            //                                 {
            //                                     var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                     debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                 }
            //                             }
            //                         }
            //                         balanceAmount = debitAmount;
            //                     }

            //                     BalanceSheetModel balanceSheetObj = new BalanceSheetModel();
            //                     balanceSheetObj.Narration = items.SubCategoryLabel;
            //                     balanceSheetObj.Balance = balanceAmount;
            //                     balanceSheetObj.AccountTypeId = items.AccountTypeId;
            //                     lstBalanceSheetModel.Add(balanceSheetObj);

            //                     #endregion
            //                 }
            //                 else if (accountDetails.AccountLevelId == 3)
            //                 {
            //                     // Gets the fourth level accounts
            //                     var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == accountDetails.AccountCode && x.AccountLevelId == 4);

            //                     foreach (var elements in accountsLevelFourth)
            //                     {
            //                         // Gets the transactions for level 4th account
            //                         var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

            //                         #region "For calculations of Balance TYPE"
            //                         if (items.ValueSource == (int)BalanceType.SUM)
            //                         {
            //                             foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                             {

            //                                 if (transactionCalcuate.CreditAccountlist.Count > 0)
            //                                 {
            //                                     if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                     {
            //                                         creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
            //                                     }
            //                                     else
            //                                     {
            //                                         var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                         creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                     }
            //                                 }

            //                                 if (transactionCalcuate.DebitAccountlist.Count > 0)
            //                                 {
            //                                     if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                     {
            //                                         debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
            //                                     }
            //                                     else
            //                                     {
            //                                         var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                         debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                     }
            //                                 }
            //                             }
            //                             balanceAmount = debitAmount - creditAmount;
            //                         }

            //                         else if (items.ValueSource == (int)BalanceType.DR)
            //                         {
            //                             foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                             {

            //                                 if (transactionCalcuate.CreditAccountlist.Count > 0)
            //                                 {
            //                                     if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                     {
            //                                         creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
            //                                     }
            //                                     else
            //                                     {
            //                                         var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                         creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                     }
            //                                 }

            //                             }
            //                             balanceAmount = creditAmount;
            //                         }

            //                         else if (items.ValueSource == (int)BalanceType.CR)
            //                         {
            //                             foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                             {

            //                                 if (transactionCalcuate.DebitAccountlist.Count > 0)
            //                                 {
            //                                     if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                     {
            //                                         debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
            //                                     }
            //                                     else
            //                                     {
            //                                         var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                         debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                     }
            //                                 }
            //                             }
            //                             balanceAmount = debitAmount;
            //                         }


            //                         #endregion
            //                     }

            //                     BalanceSheetModel balanceSheetObj = new BalanceSheetModel();
            //                     balanceSheetObj.Narration = items.SubCategoryLabel;
            //                     balanceSheetObj.Balance = balanceAmount;
            //                     balanceSheetObj.AccountTypeId = items.AccountTypeId;
            //                     lstBalanceSheetModel.Add(balanceSheetObj);
            //                 }
            //                 else if (accountDetails.AccountLevelId == 2)
            //                 {
            //                     // Gets the level 3rd accounts


            //                     var accountsLevelThird = allAccounts.FindAll(x => x.ParentID == accountDetails.ChartOfAccountCode && x.AccountLevelId == 3).Select(x => x.ChartOfAccountCode);
            //                     var accountsLevelFourth = allAccounts.FindAll(x => x.AccountLevelId == 4 && accountsLevelThird.Contains(x.ParentID));

            //                     #region "Calculations"
            //                     //var accountsLevelThird = allAccounts.FindAll(x => x.ParentID == accountDetails.AccountCode && x.AccountLevelId == 3);
            //                     //foreach (var element in accountsLevelThird)
            //                     //{
            //                     //    // Gets the fourth level accounts
            //                     //    var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == element.AccountCode && x.AccountLevelId == 4);

            //                     foreach (var elements in accountsLevelFourth)
            //                     {
            //                         // Gets the transactions for level 4th account
            //                         var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

            //                         #region "For calculations of Balance TYPE"
            //                         if (items.ValueSource == (int)BalanceType.SUM)
            //                         {
            //                             foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                             {
            //                                 if (transactionCalcuate.CreditAccountlist.Count > 0)
            //                                 {
            //                                     if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                     {
            //                                         creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Credit);
            //                                         debitAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Debit);
            //                                     }
            //                                     else
            //                                     {
            //                                         var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.Date.Value.Date <= transactionCalcuate.CreditAccountlist.FirstOrDefault().TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                         //var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                         creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Credit) * exchangeRate.Rate;
            //                                         debitAmount += transactionCalcuate.CreditAccountlist.Sum(x => x.Debit) * exchangeRate.Rate;
            //                                     }
            //                                 }

            //                                 //if (transactionCalcuate.DebitAccountlist.Count > 0)
            //                                 //{
            //                                 //    if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                 //    {
            //                                 //        debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
            //                                 //    }
            //                                 //    else
            //                                 //    {
            //                                 //        var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                 //        debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                 //    }
            //                                 //}
            //                             }
            //                             balanceAmount = debitAmount - creditAmount;
            //                         }

            //                         else if (items.ValueSource == (int)BalanceType.DR)
            //                         {
            //                             foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                             {

            //                                 if (transactionCalcuate.CreditAccountlist.Count > 0)
            //                                 {
            //                                     if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                     {
            //                                         creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Credit);
            //                                     }
            //                                     else
            //                                     {
            //                                         var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.Date.Value.Date <= transactionCalcuate.CreditAccountlist.FirstOrDefault().TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                         //var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                         creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Credit) * exchangeRate.Rate;
            //                                     }
            //                                 }


            //                             }
            //                             balanceAmount = creditAmount;
            //                         }

            //                         else if (items.ValueSource == (int)BalanceType.CR)
            //                         {
            //                             foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                             {


            //                                 if (transactionCalcuate.DebitAccountlist.Count > 0)
            //                                 {
            //                                     if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                     {
            //                                         debitAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Debit);
            //                                     }
            //                                     else
            //                                     {
            //                                         var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.Date.Value.Date <= transactionCalcuate.CreditAccountlist.FirstOrDefault().TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                         //var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                         debitAmount += transactionCalcuate.CreditAccountlist.Sum(x => x.Debit) * exchangeRate.Rate;
            //                                     }
            //                                 }
            //                             }
            //                             balanceAmount = debitAmount;
            //                         }

            //                         #endregion
            //                     }
            //                     //}
            //                     #endregion

            //                     BalanceSheetModel balanceSheetObj = new BalanceSheetModel();
            //                     balanceSheetObj.Narration = items.SubCategoryLabel;
            //                     balanceSheetObj.Balance = Math.Round(Convert.ToDouble(balanceAmount), 2);
            //                     balanceSheetObj.AccountTypeId = items.AccountTypeId;
            //                     lstBalanceSheetModel.Add(balanceSheetObj);
            //                 }
            //                 else if (accountDetails.AccountLevelId == 1)
            //                 {
            //                     // Gets the level 2nd accounts
            //                     var accountsLevelSecond = allAccounts.FindAll(x => x.ParentID == accountDetails.AccountCode && x.AccountLevelId == 2);

            //                     foreach (var item in accountsLevelSecond)
            //                     {
            //                         // Gets the level 3rd accounts
            //                         var accountsLevelThird = allAccounts.FindAll(x => x.ParentID == item.AccountCode && x.AccountLevelId == 3);

            //                         foreach (var element in accountsLevelThird)
            //                         {
            //                             // Gets the fourth level accounts
            //                             var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == element.AccountCode && x.AccountLevelId == 4);

            //                             foreach (var elements in accountsLevelFourth)
            //                             {
            //                                 // Gets the transactions for level 4th account
            //                                 var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

            //                                 #region "For calculations of Balance TYPE"
            //                                 if (items.ValueSource == (int)BalanceType.SUM)
            //                                 {
            //                                     foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                                     {

            //                                         if (transactionCalcuate.CreditAccountlist.Count > 0)
            //                                         {
            //                                             if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                             {
            //                                                 creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
            //                                             }
            //                                             else
            //                                             {
            //                                                 var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                                 creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                             }
            //                                         }

            //                                         if (transactionCalcuate.DebitAccountlist.Count > 0)
            //                                         {
            //                                             if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                             {
            //                                                 debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
            //                                             }
            //                                             else
            //                                             {
            //                                                 var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                                 debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                             }
            //                                         }
            //                                     }
            //                                     balanceAmount = debitAmount - creditAmount;
            //                                 }

            //                                 else if (items.ValueSource == (int)BalanceType.DR)
            //                                 {
            //                                     foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                                     {
            //                                         if (transactionCalcuate.CreditAccountlist.Count > 0)
            //                                         {
            //                                             if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                             {
            //                                                 creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
            //                                             }
            //                                             else
            //                                             {
            //                                                 var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                                 creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                             }
            //                                         }

            //                                     }
            //                                     balanceAmount = creditAmount;
            //                                 }

            //                                 else if (items.ValueSource == (int)BalanceType.CR)
            //                                 {
            //                                     foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
            //                                     {

            //                                         if (transactionCalcuate.DebitAccountlist.Count > 0)
            //                                         {
            //                                             if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
            //                                             {
            //                                                 debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
            //                                             }
            //                                             else
            //                                             {
            //                                                 var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            //                                                 debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
            //                                             }
            //                                         }
            //                                     }
            //                                     balanceAmount = debitAmount;
            //                                 }



            //                                 #endregion
            //                             }
            //                         }
            //                     }

            //                     BalanceSheetModel balanceSheetObj = new BalanceSheetModel();
            //                     balanceSheetObj.Narration = items.SubCategoryLabel;
            //                     balanceSheetObj.Balance = balanceAmount;
            //                     balanceSheetObj.AccountTypeId = items.AccountTypeId;
            //                     lstBalanceSheetModel.Add(balanceSheetObj);
            //                 }
            //             }

            //             BalanceSheet bal = new BalanceSheet();

            //             if (model.financialreporttype == 1)
            //             {
            //                 //1
            //                 var capData = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.CapitalAssetsWrittenOff);

            //                 bal.CapitalAssetsWrittenOff = capData
            //                     .Select(x => new BalanceSheetModel
            //                     {
            //                         //Note = x..Notes,
            //                         Narration = x.Narration,
            //                         Balance = x.Balance

            //                     }).ToList();


            //                 //2
            //                 var currentAssetsData = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.CurrentAssets);

            //                 bal.CurrentAssets = currentAssetsData
            //                     .Select(x => new BalanceSheetModel
            //                     {
            //                         Narration = x.Narration,
            //                         Balance = x.Balance

            //                     }).ToList();


            //                 //3
            //                 var fundsData = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.Funds);

            //                 bal.Funds = fundsData
            //                     .Select(x => new BalanceSheetModel
            //                     {
            //                         Narration = x.Narration,
            //                         Balance = x.Balance

            //                     }).ToList();


            //                 //4
            //                 var endownmentFundData = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.EndownmentFund);

            //                 bal.EndownmentFund = endownmentFundData
            //                     .Select(x => new BalanceSheetModel
            //                     {
            //                         Narration = x.Narration,
            //                         Balance = x.Balance

            //                     }).ToList();

            //                 bal.ReserveAccountAdjustment = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.ReserveAccountAdjustment)
            //                     .Select(x => new BalanceSheetModel
            //                     {
            //                         Narration = x.Narration,
            //                         Balance = x.Balance

            //                     }).ToList();

            //                 bal.LongtermLiability = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.LongtermLiability)
            //                     .Select(x => new BalanceSheetModel
            //                     {
            //                         Narration = x.Narration,
            //                         Balance = x.Balance

            //                     }).ToList();
            //                 bal.CurrentLiability = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.CurrentLiability)
            //                   .Select(x => new BalanceSheetModel
            //                   {
            //                       Narration = x.Narration,
            //                       Balance = x.Balance

            //                   }).ToList();
            //                 bal.ReserveAccount = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.ReserveAccount)
            //                   .Select(x => new BalanceSheetModel
            //                   {
            //                       Narration = x.Narration,
            //                       Balance = x.Balance

            //                   }).ToList();

            //             }

            //             else if (model.financialreporttype == 2)
            //             {
            //                 bal.IncomeFromDonor = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.IncomeFromDonor)
            //                   .Select(x => new BalanceSheetModel
            //                   {
            //                       Narration = x.Narration,
            //                       Balance = x.Balance

            //                   }).ToList();


            //                 bal.IncomeFromProjects = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.IncomeFromProjects)
            //                   .Select(x => new BalanceSheetModel
            //                   {
            //                       Narration = x.Narration,
            //                       Balance = x.Balance

            //                   }).ToList();
            //                 bal.ProfitOnBankDeposits = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.ProfitOnBankDeposits)
            //                   .Select(x => new BalanceSheetModel
            //                   {
            //                       Narration = x.Narration,
            //                       Balance = x.Balance

            //                   }).ToList();
            //                 bal.IncomeExpenditureFund = lstBalanceSheetModel.Where(x => x.AccountTypeId == (int)Common.Enums.AccountType.IncomeExpenditureFund)
            //                   .Select(x => new BalanceSheetModel
            //                   {
            //                       Narration = x.Narration,
            //                       Balance = x.Balance

            //                   }).ToList();
            //             }

            //             response.data.BalanceSheet = bal;
            //             response.StatusCode = StaticResource.successStatusCode;
            //             response.Message = "Success";
            //         }
            //         catch (Exception ex)
            //         {
            //             response.StatusCode = StaticResource.failStatusCode;
            //             response.Message = ex.Message;
            //         }
            #endregion
            return response;
        }

        public async Task<APIResponse> GetDetailsOfNotesReportData(int? financialyearid, int? currencyid)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (financialyearid != null && currencyid != null)
                {
                    var financialYearDetails = await _uow.FinancialYearDetailRepository.FindAsync(x => x.FinancialYearId == financialyearid);

                    //Grouped
                    var allNotes = await _uow.GetDbContext().NotesMaster
                            .Include(c => c.ChartOfAccountNew)
                            .Where(x => x.IsDeleted == false)
                            .OrderBy(o => o.Notes)
                            //.GroupBy(g => g.Notes)
                            .ToListAsync();

                    //var allAccounts = await _uow.GetDbContext().ChartAccountDetail.Include(x => x.CreditAccountlist).Include(x => x.DebitAccountlist).ToListAsync();


                    var allAccounts = await _uow.GetDbContext().ChartOfAccountNew.Include(x => x.VoucherTransactionsList)
                        .Select(x => new
                        {
                            ChartOfAccountNewId = x.ChartOfAccountNewId,
                            AccountLevelId = x.AccountLevelId,
                            AccountLevels = x.AccountLevels,
                            AccountName = x.AccountName,
                            AccountType = x.AccountType,
                            AccountTypeId = x.AccountTypeId,
                            ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                            CreatedById = x.CreatedById,
                            CreatedDate = x.CreatedDate,
                            ParentID = x.ParentID,
                            //CreditAccountlist = x.CreditAccountlist.Where(o => o.FinancialYearId == financialyearid).ToList(),
                        }).ToListAsync();


                    //var allAccounts1 = allAccounts.Where(x => x.CreditAccountlist.Count > 0).ToList();

                    List<DetailsOfNotesModel> lst = new List<DetailsOfNotesModel>();
                    List<VoucherTransactions> transactionDetail = null;

                    foreach (var items in allNotes)
                    {

                        var accountDetails = items.ChartOfAccountNew;       // Just used for finding the details of this account

                        double? creditAmount = 0, debitAmount = 0;
                        #region 
                        if (accountDetails.AccountLevelId == 4)
                        {

                            #region 

                            if (currencyid == 1)               // For AFG
                            {
                                transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                            .Where(x => x.IsDeleted == false &&
                                                            x.ChartOfAccountNewId == items.ChartOfAccountNewId &&
                                                            //model.OfficeList.Contains(x.OfficeId) &&
                                                            x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                            x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                            {
                                                                Credit = (x.Credit > 0 ? x.AFGAmount : 0) ?? 0,
                                                                Debit = (x.Debit > 0 ? x.AFGAmount : 0) ?? 0
                                                            }
                                                            ).ToListAsync();
                            }
                            if (currencyid == 2)               // For EURO
                            {
                                transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                            .Where(x => x.IsDeleted == false &&
                                                            x.ChartOfAccountNewId == items.ChartOfAccountNewId &&
                                                             //model.OfficeList.Contains(x.OfficeId) &&
                                                             x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                            x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                            {
                                                                Credit = (x.Credit > 0 ? x.EURAmount : 0) ?? 0,
                                                                Debit = (x.Debit > 0 ? x.EURAmount : 0) ?? 0
                                                            }
                                                            ).ToListAsync();
                            }
                            if (currencyid == 3)               // For PKR
                            {
                                transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                            .Where(x => x.IsDeleted == false &&
                                                            x.ChartOfAccountNewId == items.ChartOfAccountNewId &&
                                                             //model.OfficeList.Contains(x.OfficeId) &&
                                                             x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                            x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                            {
                                                                Credit = (x.Credit > 0 ? x.PKRAmount : 0) ?? 0,
                                                                Debit = (x.Debit > 0 ? x.PKRAmount : 0) ?? 0
                                                            }
                                                            ).ToListAsync();
                            }
                            if (currencyid == 4)               // For USD
                            {
                                transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                            .Where(x => x.IsDeleted == false &&
                                                            x.ChartOfAccountNewId == items.ChartOfAccountNewId &&
                                                             //model.OfficeList.Contains(x.OfficeId) &&
                                                             x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                            x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                            {
                                                                Credit = (x.Credit > 0 ? x.USDAmount : 0) ?? 0,
                                                                Debit = (x.Debit > 0 ? x.USDAmount : 0) ?? 0,
                                                            }
                                                            ).ToListAsync();
                            }

                            #endregion
                            // Gets the transactions for level 4th account
                            //var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == items.AccountCode).ToList();
                            //var accountsLevelFourthWithTransactions = await _uow.GetDbContext().VoucherTransactions
                            //										.Where(x => x.IsDeleted == false &&
                            //										x.AccountNo == items.AccountCode &&
                            //										x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                            //										x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                            //										{
                            //											Credit = (x.Credit > 0 ? x.AFGAmount : 0) ?? 0,
                            //											Debit = (x.Debit > 0 ? x.AFGAmount : 0) ?? 0
                            //										}
                            //										).ToListAsync();

                            #region "For calculations of Balance TYPE"

                            creditAmount = Math.Round(Convert.ToDouble(transactionDetail.Sum(x => x.Credit)), 3);
                            debitAmount = Math.Round(Convert.ToDouble(transactionDetail.Sum(x => x.Debit)), 3);

                            #region "Not USE"						
                            //foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
                            //{
                            //	foreach (var trans in transactionCalcuate.CreditAccountlist)
                            //	{
                            //		if (trans.CurrencyId == currencyid)
                            //		{
                            //			//creditAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Credit);
                            //			//debitAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Debit);

                            //			creditAmount += trans.Credit;
                            //			debitAmount += trans.Debit;
                            //		}
                            //		else
                            //		{
                            //			//var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

                            //			var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == trans.CurrencyId && x.Date.Value.Date <= trans.TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

                            //			if (trans.CurrencyId == currencyid)
                            //			{
                            //				creditAmount += Math.Round(trans.Credit * exchangeRate?.Rate ?? 0, 2);
                            //				debitAmount += Math.Round(trans.Debit * exchangeRate?.Rate ?? 0, 2);

                            //			}
                            //			else
                            //			{
                            //				var exchangeRate2 = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == currencyid && x.Date.Value.Date <= trans.TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

                            //				creditAmount += Math.Round((trans.Credit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
                            //				debitAmount += Math.Round((trans.Debit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
                            //			}
                            //		}
                            //	}
                            //}
                            //balanceAmount = debitAmount - creditAmount;
                            #endregion

                            DetailsOfNotesModel obj = new DetailsOfNotesModel();
                            obj.ChartOfAccountNewCode = accountDetails.ChartOfAccountNewCode;
                            obj.CreditAmount = creditAmount;
                            obj.DebitAmount = debitAmount;
                            obj.AccountName = accountDetails.AccountName;
                            obj.Notes = items.Notes;
                            lst.Add(obj);

                            #endregion
                        }
                        else if (accountDetails.AccountLevelId == 3)
                        {
                            // Gets the fourth level accounts
                            var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == accountDetails.ChartOfAccountNewId && x.AccountLevelId == 4);

                            foreach (var elements in accountsLevelFourth)
                            {
                                creditAmount = 0; debitAmount = 0;

                                #region 

                                if (currencyid == 1)               // For AFG
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                //model.OfficeList.Contains(x.OfficeId) &&
                                                                x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.AFGAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.AFGAmount : 0) ?? 0
                                                                }
                                                                ).ToListAsync();
                                }
                                if (currencyid == 2)               // For EURO
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 //model.OfficeList.Contains(x.OfficeId) &&
                                                                 x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.EURAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.EURAmount : 0) ?? 0
                                                                }
                                                                ).ToListAsync();
                                }
                                if (currencyid == 3)               // For PKR
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 //model.OfficeList.Contains(x.OfficeId) &&
                                                                 x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.PKRAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.PKRAmount : 0) ?? 0
                                                                }
                                                                ).ToListAsync();
                                }
                                if (currencyid == 4)               // For USD
                                {
                                    transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                .Where(x => x.IsDeleted == false &&
                                                                x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                 //model.OfficeList.Contains(x.OfficeId) &&
                                                                 x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                {
                                                                    Credit = (x.Credit > 0 ? x.USDAmount : 0) ?? 0,
                                                                    Debit = (x.Debit > 0 ? x.USDAmount : 0) ?? 0,
                                                                }
                                                                ).ToListAsync();
                                }

                                #endregion

                                creditAmount += Math.Round(Convert.ToDouble(transactionDetail.Sum(x => x.Credit)), 3);
                                debitAmount += Math.Round(Convert.ToDouble(transactionDetail.Sum(x => x.Debit)), 3);

                                #region "Not Use"
                                //// Gets the transactions for level 4th account
                                //var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

                                //#region "For calculations of Balance TYPE"

                                //foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
                                //{
                                //	foreach (var trans in transactionCalcuate.CreditAccountlist)
                                //	{
                                //		if (trans.CurrencyId == currencyid)
                                //		{
                                //			//creditAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Credit);
                                //			//debitAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Debit);

                                //			creditAmount += trans.Credit;
                                //			debitAmount += trans.Debit;
                                //		}
                                //		else
                                //		{
                                //			//var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();


                                //			var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == trans.CurrencyId && x.Date.Value.Date <= trans.TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

                                //			if (trans.CurrencyId == currencyid)
                                //			{

                                //				creditAmount += Math.Round(trans.Credit * exchangeRate?.Rate ?? 0, 2);
                                //				debitAmount += Math.Round(trans.Debit * exchangeRate?.Rate ?? 0, 2);

                                //			}
                                //			else
                                //			{
                                //				var exchangeRate2 = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == currencyid && x.Date.Value.Date <= trans.TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

                                //				creditAmount += Math.Round((trans.Credit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
                                //				debitAmount += Math.Round((trans.Debit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
                                //			}

                                //		}
                                //	}
                                //}
                                ////balanceAmount = debitAmount - creditAmount;

                                #endregion

                                DetailsOfNotesModel obj = new DetailsOfNotesModel();
                                obj.ChartOfAccountNewCode = elements.ChartOfAccountNewCode;
                                obj.CreditAmount = creditAmount;
                                obj.DebitAmount = debitAmount;
                                obj.AccountName = elements.AccountName;
                                obj.Notes = items.Notes;
                                lst.Add(obj);

                            }



                        }
                        else if (accountDetails.AccountLevelId == 2)
                        {
                            // Gets the level 3rd accounts
                            var accountsLevelThird = allAccounts.FindAll(x => x.ParentID == accountDetails.ChartOfAccountNewId && x.AccountLevelId == 3);

                            foreach (var element in accountsLevelThird)
                            {
                                // Gets the fourth level accounts
                                var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == element.ChartOfAccountNewId && x.AccountLevelId == 4);

                                foreach (var elements in accountsLevelFourth)
                                {
                                    creditAmount = 0; debitAmount = 0;

                                    #region 

                                    if (currencyid == 1)               // For AFG
                                    {
                                        transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                    .Where(x => x.IsDeleted == false &&
                                                                    x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                    //model.OfficeList.Contains(x.OfficeId) &&
                                                                    x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                    x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                    {
                                                                        Credit = (x.Credit > 0 ? x.AFGAmount : 0) ?? 0,
                                                                        Debit = (x.Debit > 0 ? x.AFGAmount : 0) ?? 0
                                                                    }
                                                                    ).ToListAsync();
                                    }
                                    if (currencyid == 2)               // For EURO
                                    {
                                        transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                    .Where(x => x.IsDeleted == false &&
                                                                    x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                     //model.OfficeList.Contains(x.OfficeId) &&
                                                                     x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                    x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                    {
                                                                        Credit = (x.Credit > 0 ? x.EURAmount : 0) ?? 0,
                                                                        Debit = (x.Debit > 0 ? x.EURAmount : 0) ?? 0
                                                                    }
                                                                    ).ToListAsync();
                                    }
                                    if (currencyid == 3)               // For PKR
                                    {
                                        transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                    .Where(x => x.IsDeleted == false &&
                                                                    x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                     //model.OfficeList.Contains(x.OfficeId) &&
                                                                     x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                    x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                    {
                                                                        Credit = (x.Credit > 0 ? x.PKRAmount : 0) ?? 0,
                                                                        Debit = (x.Debit > 0 ? x.PKRAmount : 0) ?? 0
                                                                    }
                                                                    ).ToListAsync();
                                    }
                                    if (currencyid == 4)               // For USD
                                    {
                                        transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                    .Where(x => x.IsDeleted == false &&
                                                                    x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                     //model.OfficeList.Contains(x.OfficeId) &&
                                                                     x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                    x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                    {
                                                                        Credit = (x.Credit > 0 ? x.USDAmount : 0) ?? 0,
                                                                        Debit = (x.Debit > 0 ? x.USDAmount : 0) ?? 0,
                                                                    }
                                                                    ).ToListAsync();
                                    }

                                    #endregion

                                    creditAmount += Math.Round(Convert.ToDouble(transactionDetail.Sum(x => x.Credit)), 3);
                                    debitAmount += Math.Round(Convert.ToDouble(transactionDetail.Sum(x => x.Debit)), 3);

                                    #region "NOT USE"
                                    //// Gets the transactions for level 4th account
                                    //var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

                                    //#region "For calculations of Balance TYPE"

                                    //foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
                                    //{
                                    //	foreach (var trans in transactionCalcuate.CreditAccountlist)
                                    //	{
                                    //		if (trans.CurrencyId == currencyid)
                                    //		{
                                    //			//creditAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Credit);
                                    //			//debitAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Debit);

                                    //			creditAmount += trans.Credit;
                                    //			debitAmount += trans.Debit;
                                    //		}
                                    //		else
                                    //		{
                                    //			//var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();


                                    //			var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == trans.CurrencyId && x.Date.Value.Date <= trans.TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

                                    //			if (trans.CurrencyId == currencyid)
                                    //			{

                                    //				creditAmount += Math.Round(trans.Credit * exchangeRate?.Rate ?? 0, 2);
                                    //				debitAmount += Math.Round(trans.Debit * exchangeRate?.Rate ?? 0, 2);

                                    //			}
                                    //			else
                                    //			{
                                    //				var exchangeRate2 = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == currencyid && x.Date.Value.Date <= trans.TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

                                    //				creditAmount += Math.Round((trans.Credit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
                                    //				debitAmount += Math.Round((trans.Debit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
                                    //			}

                                    //		}
                                    //	}

                                    //}
                                    ////balanceAmount = debitAmount - creditAmount;	
                                    //#endregion

                                    #endregion

                                    DetailsOfNotesModel obj = new DetailsOfNotesModel();
                                    obj.ChartOfAccountNewCode = elements.ChartOfAccountNewCode;
                                    obj.CreditAmount = creditAmount;
                                    obj.DebitAmount = debitAmount;
                                    obj.AccountName = elements.AccountName;
                                    obj.Notes = items.Notes;
                                    lst.Add(obj);

                                }
                            }

                        }
                        else if (accountDetails.AccountLevelId == 1)
                        {
                            // Gets the level 2nd accounts
                            var accountsLevelSecond = allAccounts.FindAll(x => x.ParentID == accountDetails.ChartOfAccountNewId && x.AccountLevelId == 2);

                            foreach (var item in accountsLevelSecond)
                            {
                                // Gets the level 3rd accounts
                                var accountsLevelThird = allAccounts.FindAll(x => x.ParentID == item.ChartOfAccountNewId && x.AccountLevelId == 3);

                                foreach (var element in accountsLevelThird)
                                {
                                    // Gets the fourth level accounts
                                    var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == element.ChartOfAccountNewId && x.AccountLevelId == 4);

                                    foreach (var elements in accountsLevelFourth)
                                    {
                                        creditAmount = 0; debitAmount = 0;

                                        #region 

                                        if (currencyid == 1)               // For AFG
                                        {
                                            transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                        .Where(x => x.IsDeleted == false &&
                                                                        x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                        //model.OfficeList.Contains(x.OfficeId) &&
                                                                        x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                        x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                        {
                                                                            Credit = (x.Credit > 0 ? x.AFGAmount : 0) ?? 0,
                                                                            Debit = (x.Debit > 0 ? x.AFGAmount : 0) ?? 0
                                                                        }
                                                                        ).ToListAsync();
                                        }
                                        if (currencyid == 2)               // For EURO
                                        {
                                            transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                        .Where(x => x.IsDeleted == false &&
                                                                        x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                         //model.OfficeList.Contains(x.OfficeId) &&
                                                                         x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                        x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                        {
                                                                            Credit = (x.Credit > 0 ? x.EURAmount : 0) ?? 0,
                                                                            Debit = (x.Debit > 0 ? x.EURAmount : 0) ?? 0
                                                                        }
                                                                        ).ToListAsync();
                                        }
                                        if (currencyid == 3)               // For PKR
                                        {
                                            transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                        .Where(x => x.IsDeleted == false &&
                                                                        x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                         //model.OfficeList.Contains(x.OfficeId) &&
                                                                         x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                        x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                        {
                                                                            Credit = (x.Credit > 0 ? x.PKRAmount : 0) ?? 0,
                                                                            Debit = (x.Debit > 0 ? x.PKRAmount : 0) ?? 0
                                                                        }
                                                                        ).ToListAsync();
                                        }
                                        if (currencyid == 4)               // For USD
                                        {
                                            transactionDetail = await _uow.GetDbContext().VoucherTransactions
                                                                        .Where(x => x.IsDeleted == false &&
                                                                        x.ChartOfAccountNewId == elements.ChartOfAccountNewId &&
                                                                         //model.OfficeList.Contains(x.OfficeId) &&
                                                                         x.TransactionDate.Value.Date >= financialYearDetails.StartDate.Date &&
                                                                        x.TransactionDate.Value.Date <= financialYearDetails.EndDate.Date).Select(x => new VoucherTransactions
                                                                        {
                                                                            Credit = (x.Credit > 0 ? x.USDAmount : 0) ?? 0,
                                                                            Debit = (x.Debit > 0 ? x.USDAmount : 0) ?? 0,
                                                                        }
                                                                        ).ToListAsync();
                                        }

                                        #endregion

                                        creditAmount += Math.Round(Convert.ToDouble(transactionDetail.Sum(x => x.Credit)), 3);
                                        debitAmount += Math.Round(Convert.ToDouble(transactionDetail.Sum(x => x.Debit)), 3);

                                        #region "NOT USE"
                                        //// Gets the transactions for level 4th account
                                        //// Gets the transactions for level 4th account
                                        //var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

                                        //#region "For calculations of Balance TYPE"

                                        //foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
                                        //{
                                        //	foreach (var trans in transactionCalcuate.CreditAccountlist)
                                        //	{
                                        //		if (trans.CurrencyId == currencyid)
                                        //		{
                                        //			//creditAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Credit);
                                        //			//debitAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Debit);

                                        //			creditAmount += trans.Credit;
                                        //			debitAmount += trans.Debit;
                                        //		}
                                        //		else
                                        //		{
                                        //			//var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();


                                        //			var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == trans.CurrencyId && x.Date.Value.Date <= trans.TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

                                        //			if (trans.CurrencyId == currencyid)
                                        //			{

                                        //				creditAmount += Math.Round(trans.Credit * exchangeRate?.Rate ?? 0, 2);
                                        //				debitAmount += Math.Round(trans.Debit * exchangeRate?.Rate ?? 0, 2);

                                        //			}
                                        //			else
                                        //			{
                                        //				var exchangeRate2 = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == currencyid && x.Date.Value.Date <= trans.TransactionDate.Value.Date).OrderByDescending(x => x.Date).FirstOrDefaultAsync();

                                        //				creditAmount += Math.Round((trans.Credit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
                                        //				debitAmount += Math.Round((trans.Debit * exchangeRate?.Rate) / exchangeRate2?.Rate ?? 0, 2);
                                        //			}

                                        //		}
                                        //	}
                                        //}
                                        ////balanceAmount = debitAmount - creditAmount;
                                        //#endregion
                                        #endregion

                                        DetailsOfNotesModel obj = new DetailsOfNotesModel();
                                        obj.ChartOfAccountNewCode = elements.ChartOfAccountNewCode;
                                        obj.CreditAmount = creditAmount;
                                        obj.DebitAmount = debitAmount;
                                        obj.AccountName = elements.AccountName;
                                        obj.Notes = items.Notes;
                                        lst.Add(obj);
                                    }
                                }
                            }
                        }
                        #endregion
                    }


                    //allNotes = allNotes.Where(x => x.ChartAccountDetails.AccountLevelId == 4).ToList();
                    var Noteslist = lst.GroupBy(g => g.Notes).ToList();

                    if (Noteslist != null)
                    {
                        List<DetailsOfNotesFinalModel> detailsOfNotesFinalList = new List<DetailsOfNotesFinalModel>();

                        foreach (var groupedItem in Noteslist)
                        {
                            DetailsOfNotesFinalModel finalObj = new DetailsOfNotesFinalModel();
                            List<DetailsOfNotesModel> detailsOfNoteList = new List<DetailsOfNotesModel>();

                            foreach (var item in groupedItem)
                            {
                                DetailsOfNotesModel obj = new DetailsOfNotesModel();

                                obj.CreditAmount = item.CreditAmount;
                                obj.DebitAmount = item.DebitAmount;
                                //obj.BalanceAmount = obj.CreditAmount.Value - obj.DebitAmount.Value;

                                obj.ChartOfAccountNewCode = item.ChartOfAccountNewCode;
                                obj.AccountName = item.AccountName;
                                obj.Notes = item.Notes;
                                detailsOfNoteList.Add(obj);
                            }

                            finalObj.DetailsOfNotesList = detailsOfNoteList.ToList();

                            finalObj.CreditSum = Math.Round(Convert.ToDouble(detailsOfNoteList.Sum(x => x.CreditAmount)), 3);
                            finalObj.DebitSum = Math.Round(Convert.ToDouble(detailsOfNoteList.Sum(x => x.DebitAmount)), 3);
                            finalObj.BalanceSum = (finalObj.DebitSum - finalObj.CreditSum).Value;

                            detailsOfNotesFinalList.Add(finalObj);
                        }

                        response.data.DetailsOfNotesFinalList = detailsOfNotesFinalList.ToList();
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record Not Found";

                }

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }



        public async Task<APIResponse> AddCategoryPopulator(CategoryPopulatorModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CategoryPopulator obj = _mapper.Map<CategoryPopulator>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                await _uow.CategoryPopulatorRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditCategoryPopulator(CategoryPopulatorModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var recordExists = await _uow.CategoryPopulatorRepository.FindAsync(x => x.CategoryPopulatorId == model.CategoryPopulatorId);
                if (recordExists != null)
                {
                    recordExists.ModifiedById = UserId;
                    recordExists.ModifiedDate = DateTime.Now;
                    recordExists.IsDeleted = false;
                    _mapper.Map(model, recordExists);
                    await _uow.CategoryPopulatorRepository.UpdateAsyn(recordExists);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> DeleteCategoryPopulator(int categoryPopulatorId, string modifiedById)
        {
            APIResponse response = new APIResponse();
            try
            {
                var recordExists = await _uow.CategoryPopulatorRepository.FindAsync(x => x.CategoryPopulatorId == categoryPopulatorId);
                if (recordExists != null)
                {
                    recordExists.ModifiedById = modifiedById;
                    recordExists.ModifiedDate = DateTime.Now;
                    recordExists.IsDeleted = true;
                    await _uow.CategoryPopulatorRepository.UpdateAsyn(recordExists);
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

        public async Task<APIResponse> GetAllCategoryPopulator()
        {
            APIResponse response = new APIResponse();
            try
            {
                var lst = await _uow.CategoryPopulatorRepository.FindAllAsync(x => x.IsDeleted == false);
                if (lst != null)
                {
                    response.data.CategoryPopulatorLst = lst.ToList();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "No record found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllUserNotifications(string userid)
        {
            APIResponse response = new APIResponse();
            try
            {
                var lst = await _uow.LoggerDetailsRepository.FindAllAsync(x => x.CreatedById == userid);
                List<LoggerModel> loggerList = new List<LoggerModel>();
                foreach (var item in lst)
                {
                    LoggerModel obj = new LoggerModel();
                    obj.userId = item.CreatedById;
                    obj.createdDate = DateTime.Now;
                    obj.notificationId = item.NotificationId;
                    obj.loggerDetailsId = item.LoggerDetailsId;
                    obj.isRead = item.IsRead;
                    obj.userName = item.UserName;
                    obj.loggedDetail = item.LoggedDetail;
                    loggerList.Add(obj);
                }
                response.data.LoggerDetailsModelList = loggerList.OrderByDescending(x => x.createdDate).ToList();
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


        //public async Task<APIResponse> GetExchangeGainOrLossAmount(ExchangeGainOrLossFilterModel model)
        //{
        //    APIResponse response = new APIResponse();


        //    try
        //    {
        //        if (model != null)
        //        {

        //            var baseCurrency = await _uow.CurrencyDetailsRepository.FindAsync(x => x.Status == true);

        //            ExchangeGainOrLossModel responseModel = new ExchangeGainOrLossModel();
        //            List<TransactionsModel> lst = new List<TransactionsModel>();

        //            //var exchangeRateList = await _uow.ExchangeRateRepository.FindAllAsync(x => x.IsDeleted == false && x.OfficeId == model.OfficeId && x.Date.Value.Date <= model.EndDate.Date);

        //            var exchangeRateList = await _uow.GetDbContext().LoadStoredProc("get_ExchangeRates")
        //                                   .WithSqlParam("modelEndDate", model.EndDate.ToString())
        //                                   .WithSqlParam("modelOfficeId", model.OfficeId)
        //                                   .ExecuteStoredProc<SP_ExchangeRate>();



        //            //CASE 1
        //            ////Note: Take all the debits towards an account
        //            //var records = await _uow.GetDbContext().ChartAccountDetail
        //            //                    .Include(x => x.CreditAccountlist)
        //            //                    .Where(x => x.IsDeleted == false && model.AccountCodes.Contains(x.AccountCode))
        //            //                    .ToListAsync();

        //            //CASE 2
        //            //Note: Take all the debits towards an account
        //            //var records = await _uow.GetDbContext().ChartAccountDetail
        //            //                    .Where(x => x.IsDeleted == false && model.AccountCodes.Contains(x.AccountCode))
        //            //                    .ToListAsync();

        //            // CASE 3
        //            ////Note: Take all the debits towards an account
        //            //var records = await _uow.GetDbContext().LoadStoredProc("get_exchangeVoucherTransactionDetails")
        //            //             .WithSqlParam("modelAccountCode", model.AccountCodes.ToArray()) //{511022,1}
        //            //             .WithSqlParam("modelStartDate", model.StartDate.ToString())
        //            //             .WithSqlParam("modelEndDate", model.EndDate.ToString())
        //            //             .WithSqlParam("modelOfficeId", model.OfficeId)
        //            //             .ExecuteStoredProc<ExchangeGainOrLossTransaction>();


        //            var records = await _uow.GetDbContext().VoucherTransactions
        //                              .Where(x => x.IsDeleted == false &&
        //                                    model.AccountCodes.Contains(x.AccountNo) &&
        //                                    x.TransactionDate.Value.Date >= model.StartDate.Date &&
        //                                    x.TransactionDate.Value.Date <= model.EndDate.Date
        //                                    )
        //                              .Skip(model.Skip)
        //                              .Take(model.Take)
        //                              .ToListAsync();

        //            var totalCount = await _uow.GetDbContext().VoucherTransactions
        //                           .Where(x => x.IsDeleted == false &&
        //                                 model.AccountCodes.Contains(x.AccountNo) &&
        //                                 x.TransactionDate.Value.Date >= model.StartDate.Date &&
        //                                 x.TransactionDate.Value.Date <= model.EndDate.Date
        //                                 ).CountAsync();



        //            foreach (var items in records)
        //            {
        //                double? accountTransactionTotal = 0, accountCurrentTotal = 0;

        //                if (items.CurrencyId == baseCurrency.CurrencyId)
        //                {
        //                    accountTransactionTotal += Math.Round(Convert.ToDouble(items.Debit), 2);
        //                    accountCurrentTotal += Math.Round(Convert.ToDouble(items.Debit), 2);
        //                }
        //                else
        //                {
        //                    //var excRateOfTransaction = (from s in _uow.GetDbContext().ExchangeRates
        //                    //                            where s.IsDeleted == false &&
        //                    //                                  s.FromCurrency == items.CurrencyId &&
        //                    //                                  s.Date.Value.Date.Month <= items.TransactionDate.Month &&
        //                    //                                  s.Date.Value.Date.Year <= items.TransactionDate.Year
        //                    //                            orderby s.Date descending
        //                    //                            select s)
        //                    //                            .FirstOrDefault();


        //                    //var excRateForCurrentDate = (from s in _uow.GetDbContext().ExchangeRates
        //                    //                             where s.IsDeleted == false &&
        //                    //                                   s.FromCurrency == items.CurrencyId &&
        //                    //                                   s.Date.Value.Date.Month <= DateTime.Now.Date.Month &&
        //                    //                                   s.Date.Value.Date.Year <= DateTime.Now.Date.Year
        //                    //                             orderby s.Date descending
        //                    //                             select s)
        //                    //                            .FirstOrDefault();



        //                    //var excRateOfTransaction = exchangeRateList.Where(x => x.FromCurrency == items.CurrencyId && x.Date.Month <= items.TransactionDate.Month && x.Date.Year <= items.TransactionDate.Year).OrderByDescending(x => x.Date).FirstOrDefault();
        //                    //var excRateForCurrentDate = exchangeRateList.Where(x => x.FromCurrency == items.CurrencyId && x.Date.Month <= DateTime.Now.Date.Month && x.Date.Year <= DateTime.Now.Date.Year).OrderByDescending(x => x.Date).FirstOrDefault();

        //                    //NOTE: Use AsEnumerable , It will store dast in memory //Much faster
        //                    var excRateOfTransaction = exchangeRateList.AsEnumerable().Where(x => x.FromCurrency == items.CurrencyId && x.Date.Month <= items.TransactionDate.Value.Month && x.Date.Year <= items.TransactionDate.Value.Year).OrderByDescending(x => x.Date).FirstOrDefault();
        //                    var excRateForCurrentDate = exchangeRateList.AsEnumerable().Where(x => x.FromCurrency == items.CurrencyId && x.Date.Month <= DateTime.Now.Date.Month && x.Date.Year <= DateTime.Now.Date.Year).OrderByDescending(x => x.Date).FirstOrDefault();

        //                    accountTransactionTotal += Math.Round(Convert.ToDouble(items.Debit * excRateOfTransaction?.Rate), 2);
        //                    accountCurrentTotal += Math.Round(Convert.ToDouble(items.Debit * excRateForCurrentDate?.Rate), 2);

        //                    //accountTransactionTotal += Math.Round(Convert.ToDouble(items.Debit * excRateOfTransaction?.Rate));
        //                    //accountCurrentTotal += Math.Round(Convert.ToDouble(items.Debit * excRateForCurrentDate?.Rate));
        //                }

        //                TransactionsModel obj = new TransactionsModel();
        //                obj.OriginalAmount = Math.Round(Convert.ToDouble(accountTransactionTotal), 2);
        //                obj.CurrentAmount = Math.Round(Convert.ToDouble(accountCurrentTotal), 2);
        //                obj.Balance = Math.Round(Convert.ToDouble(accountCurrentTotal - accountTransactionTotal), 2);
        //                obj.ChartOfAccountCode = Convert.ToInt64(items.AccountNo);
        //                lst.Add(obj);
        //            }







        //            response.data.TotalCount = totalCount;
        //            responseModel.TransactionsModel = lst;
        //            responseModel.Total = lst.Sum(x => x.Balance);
        //            response.data.ExchangeGainOrLossModel = responseModel;
        //            response.StatusCode = StaticResource.successStatusCode;
        //            response.Message = "Success";
        //        }
        //        else
        //        {
        //            response.data.ExchangeGainOrLossModel = null;
        //            response.data.TotalCount = 0;
        //            response.StatusCode = StaticResource.failStatusCode;
        //            response.Message = "No record Found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = StaticResource.failStatusCode;
        //        response.Message = ex.Message;
        //    }
        //    return response;
        //}


        public async Task<APIResponse> GetAllVoucherByJouranlId(JournalVoucherFilterModel JournalVoucherFilter)
        {
            APIResponse response = new APIResponse();
            try
            {
                ICollection<VoucherDetail> vouchers = await _uow.VoucherDetailRepository
                                                        .FindAllAsync(x =>
                                                                        x.JournalCode == JournalVoucherFilter.JournalNo &&
                                                                        JournalVoucherFilter.OfficeIdList.Contains(x.OfficeId) &&
                                                                        x.VoucherDate.Value.Date >= JournalVoucherFilter.FromDate.Value.Date &&
                                                                        x.VoucherDate.Value.Date <= JournalVoucherFilter.ToDate.Value.Date &&
                                                                        x.IsDeleted == false);

                response.data.VouchersList = vouchers;
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

        public async Task<APIResponse> GetLevelFourAccountCode()
        {
            APIResponse response = new APIResponse();
            try
            {
                var accountcodelist = (from c in await _uow.ChartOfAccountNewRepository.GetAllAsyn()
                                       where c.IsDeleted == false && c.AccountLevelId == 4 //4 = level 4 accounts for transactions
                                       select new AccountDetailModel
                                       {
                                           AccountCode = c.ChartOfAccountNewId,
                                           AccountName = c.ChartOfAccountNewCode + " - " + c.AccountName,
                                           ChartOfAccountNewCode = c.ChartOfAccountNewCode,
                                       }).OrderBy(x => x.AccountName).ToList();
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

        public async Task<APIResponse> AddExchangeGainLossVoucher(ExchangeGainLossVoucher model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var officeCode = _uow.OfficeDetailRepository.FindAsync(o => o.OfficeId == model.OfficeId).Result.OfficeCode; //use OfficeCode
                //var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.OfficeId == 16).OrderByDescending(x => x.Date).ToListAsync();

                VoucherDetail obj = new VoucherDetail();
                obj.CreatedById = model.CreatedById;
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;
                obj.FinancialYearId = model.FinancialYearId;
                obj.VoucherTypeId = model.VoucherTypeId;
                obj.Description = model.Description;
                obj.CurrencyId = model.CurrencyId;
                obj.VoucherDate = model.VoucherDate;
                obj.ChequeNo = model.ChequeNo;
                obj.JournalCode = model.JournalCode;
                obj.OfficeId = model.OfficeId;
                obj.ProjectId = model.ProjectId;
                obj.BudgetLineId = model.BudgetLineId;
                obj.IsExchangeGainLossVoucher = true;

                await _uow.VoucherDetailRepository.AddAsyn(obj);

                obj.ReferenceNo = officeCode + "-" + obj.VoucherNo;
                await _uow.VoucherDetailRepository.UpdateAsyn(obj);

                VoucherTransactions voucherTransactionsCredit = new VoucherTransactions();
                VoucherTransactions voucherTransactionsDebit = new VoucherTransactions();

                List<VoucherTransactions> voucherTransactionList = new List<VoucherTransactions>();

                // for Credit
                voucherTransactionsCredit.IsDeleted = false;
                voucherTransactionsCredit.VoucherNo = obj.VoucherNo;
                voucherTransactionsCredit.FinancialYearId = model.FinancialYearId;
                voucherTransactionsCredit.ChartOfAccountNewId = model.AccountCodeCredit;
                voucherTransactionsCredit.CreditAccount = model.AccountCodeCredit;
                voucherTransactionsCredit.Credit = Math.Abs(model.ExchangeGainLossAmount);
                voucherTransactionsCredit.Debit = 0;
                voucherTransactionsCredit.CurrencyId = model.CurrencyId;
                voucherTransactionsCredit.Description = model.Description;
                voucherTransactionsCredit.OfficeId = model.OfficeId;
                voucherTransactionsCredit.TransactionDate = obj.VoucherDate;

                voucherTransactionList.Add(voucherTransactionsCredit);

                //Transacting Credit
                //APIResponse xAPIResponse = await AddVoucherTransactionConvertedToExchangeRate(xVoucherTransactionModel, exchangeRate);

                ////When Transaction for Credit is Successful the proceed with Debit
                //if (xAPIResponse.StatusCode == 200)
                //{
                // for Debit
                voucherTransactionsDebit.IsDeleted = false;
                voucherTransactionsDebit.VoucherNo = obj.VoucherNo;
                voucherTransactionsDebit.FinancialYearId = model.FinancialYearId;
                voucherTransactionsDebit.ChartOfAccountNewId = model.AccountCodeCredit;
                voucherTransactionsDebit.Debit = model.ExchangeGainLossAmount;
                voucherTransactionsDebit.Credit = 0;
                voucherTransactionsDebit.DebitAccount = model.ChartOfAccountNewIdDebit;
                voucherTransactionsDebit.CreditAccount = 0;
                voucherTransactionsDebit.CurrencyId = model.CurrencyId;
                voucherTransactionsDebit.Description = model.Description;
                voucherTransactionsDebit.OfficeId = model.OfficeId;
                voucherTransactionsDebit.TransactionDate = obj.VoucherDate;

                voucherTransactionList.Add(voucherTransactionsDebit);

                await _uow.GetDbContext().VoucherTransactions.AddRangeAsync(voucherTransactionList);
                await _uow.SaveAsync();

                //Transacting Debit
                //xAPIResponse = await AddVoucherTransactionConvertedToExchangeRate(xVoucherTransactionModel, exchangeRate);
                //}

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
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Delete Voucher and Its Transactions as per Voucher No
        /// </summary>
        /// <param name="VoucherNo"></param>
        /// <returns>boolean</returns>
        public async Task<APIResponse> DeleteExchangeGainLossVoucher(long VoucherNo, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                //Get Transactions Associated with Voucher No.
                List<VoucherTransactions> voucherTransactions = await _uow.GetDbContext().VoucherTransactions.Where(x => x.VoucherNo == VoucherNo).ToListAsync();

                foreach (VoucherTransactions transactions in voucherTransactions)
                {
                    //Delete each transactions
                    transactions.IsDeleted = true;
                    await _uow.VoucherTransactionsRepository.UpdateAsyn(transactions);
                }

                //Get Voucher as per the Voucher No.
                VoucherDetail VoucherDetails = await _uow.VoucherDetailRepository.FindAsync(x => x.VoucherNo == VoucherNo);

                //Delete Voucher
                VoucherDetails.IsDeleted = true;
                await _uow.VoucherDetailRepository.UpdateAsyn(VoucherDetails);

                var user = await _uow.UserDetailsRepository.FindAsync(x => x.AspNetUserId == UserId);

                LoggerDetailsModel loggerObj = new LoggerDetailsModel();
                loggerObj.NotificationId = (int)Common.Enums.LoggerEnum.VoucherDeleted;
                loggerObj.IsRead = false;
                loggerObj.UserName = user.FirstName + " " + user.LastName;
                loggerObj.UserId = UserId;
                loggerObj.LoggedDetail = "Voucher " + VoucherNo + " Deleted";
                loggerObj.CreatedDate = DateTime.Now;

                response.LoggerDetailsModel = loggerObj;
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
        /// Returns Accounts List based on Voucher Nos
        /// </summary>
        /// <returns></returns>
        //public async Task<APIResponse> GetAllAccountCodeByVoucherNo(ExchangeGainOrLossFilterModel ExchangeGainOrLossFilter)
        //{
        //    APIResponse response = new APIResponse();
        //    try
        //    {
        //        var accountcodelist = (from c in await _uow.VoucherTransactionsRepository.GetAllAsyn()
        //                               join a in await _uow.ChartAccountDetailRepository.GetAllAsyn() on c.AccountNo equals a.AccountCode
        //                               where c.IsDeleted == false && c.TransactionDate>= ExchangeGainOrLossFilter.FromDate 
        //                               && c.TransactionDate >= ExchangeGainOrLossFilter.ToDate && ExchangeGainOrLossFilter.VoucherList.Contains(c.VoucherNo)
        //                               select new AccountDetailModel
        //                               {
        //                                   AccountCode = a.AccountCode,
        //                                   AccountName = a.ChartOfAccountCode + " - " + a.AccountName,
        //                                   ChartOfAccountCode = a.ChartOfAccountCode
        //                               }).OrderBy(x => x.ChartOfAccountCode).ToList();
        //        response.data.AccountDetailList = accountcodelist;
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

        public async Task<APIResponse> GetAllInputLevelAccountCode()
        {
            APIResponse response = new APIResponse();
            try
            {
                var accountcodelist = (from c in await _uow.ChartOfAccountNewRepository.GetAllAsyn()
                                       where c.IsDeleted == false && c.AccountLevelId == (int)AccountLevels.InputLevel
                                       select new AccountDetailModel
                                       {
                                           AccountCode = c.ChartOfAccountNewId,
                                           AccountName = c.ChartOfAccountNewCode + " - " + c.AccountName,
                                           ChartOfAccountNewCode = c.ChartOfAccountNewCode
                                       }).OrderBy(x => x.AccountName).ToList();
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

        /// <summary>
        /// Addition of Employee Pension Payment
        /// </summary>
        /// <param name="OfficeId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddEmployeePensionPayment(EmployeePensionPaymentModel EmployeePensionPayment)
        {
            APIResponse response = new APIResponse();

            try
            {
                var officeCode = _uow.OfficeDetailRepository.FindAsync(o => o.OfficeId == EmployeePensionPayment.OfficeId).Result.OfficeCode; //use OfficeCode
                var financialYear = _uow.GetDbContext().FinancialYearDetail.FirstOrDefault(x => x.IsDefault == true && x.IsDeleted == false);
                var EmployeeDetails = _uow.GetDbContext().EmployeeDetail.FirstOrDefault(x => x.EmployeeID == EmployeePensionPayment.EmployeeId && x.IsDeleted == false);
                //var exchangeRate = await _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).Where(x => x.OfficeId == 16).ToListAsync();

                //Creating Voucher for Voucher transaction
                VoucherDetail obj = new VoucherDetail();
                obj.CreatedById = EmployeePensionPayment.CreatedById;
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;
                obj.FinancialYearId = financialYear.FinancialYearId;
                obj.VoucherTypeId = EmployeePensionPayment.VoucherTypeId;
                obj.Description = string.Format(StaticResource.PensionPaymentCreated, DateTime.Now.Date, EmployeeDetails.EmployeeName);
                obj.CurrencyId = EmployeePensionPayment.CurrencyId;
                obj.VoucherDate = DateTime.Now;
                obj.JournalCode = EmployeePensionPayment.JournalId;
                obj.OfficeId = EmployeePensionPayment.OfficeId;

                await _uow.VoucherDetailRepository.AddAsyn(obj);

                obj.ReferenceNo = officeCode + "-" + obj.VoucherNo;
                await _uow.VoucherDetailRepository.UpdateAsyn(obj);

                List<VoucherTransactions> VoucherTransactionsList = new List<VoucherTransactions>();

                VoucherTransactions xVoucherTransactionCredit = new VoucherTransactions();
                VoucherTransactions xVoucherTransactionDebit = new VoucherTransactions();

                //Creating Voucher Transaction for Credit
                xVoucherTransactionCredit.IsDeleted = false;
                xVoucherTransactionCredit.VoucherNo = obj.VoucherNo;
                xVoucherTransactionCredit.FinancialYearId = financialYear.FinancialYearId;
                xVoucherTransactionCredit.ChartOfAccountNewId = EmployeePensionPayment.CreditAccount;
                xVoucherTransactionCredit.CreditAccount = EmployeePensionPayment.CreditAccount;
                xVoucherTransactionCredit.Credit = Convert.ToDouble(EmployeePensionPayment.PensionAmount);
                xVoucherTransactionCredit.Debit = 0;
                xVoucherTransactionCredit.CurrencyId = EmployeePensionPayment.CurrencyId;
                xVoucherTransactionCredit.Description = string.Format(StaticResource.PensionPaymentCreated, DateTime.Now.Date, EmployeeDetails.EmployeeName); ;
                xVoucherTransactionCredit.OfficeId = EmployeePensionPayment.OfficeId;

                VoucherTransactionsList.Add(xVoucherTransactionCredit);

                //Creating Voucher Transaction for Debit
                xVoucherTransactionDebit.IsDeleted = false;
                xVoucherTransactionDebit.VoucherNo = obj.VoucherNo;
                xVoucherTransactionDebit.FinancialYearId = financialYear.FinancialYearId;
                xVoucherTransactionDebit.Debit = Convert.ToDouble(EmployeePensionPayment.PensionAmount);
                xVoucherTransactionDebit.Credit = 0;
                xVoucherTransactionDebit.ChartOfAccountNewId = EmployeePensionPayment.DebitAccount;
                xVoucherTransactionDebit.DebitAccount = EmployeePensionPayment.DebitAccount;
                xVoucherTransactionDebit.CreditAccount = 0;
                xVoucherTransactionDebit.CurrencyId = EmployeePensionPayment.CurrencyId;
                xVoucherTransactionDebit.Description = string.Format(StaticResource.PensionPaymentCreated, DateTime.Now.Date, EmployeeDetails.EmployeeName); ;
                xVoucherTransactionDebit.OfficeId = EmployeePensionPayment.OfficeId;

                VoucherTransactionsList.Add(xVoucherTransactionDebit);

                //save voucher transactions to db
                await _uow.GetDbContext().AddRangeAsync(VoucherTransactionsList);
                await _uow.GetDbContext().SaveChangesAsync();

                //Adding details to Pension Payment History Table
                PensionPaymentHistory pensionPayments = new PensionPaymentHistory();
                pensionPayments.PaymentDate = DateTime.Now;
                pensionPayments.PaymentAmount = EmployeePensionPayment.PensionAmount;
                pensionPayments.IsDeleted = false;
                pensionPayments.CreatedById = EmployeePensionPayment.CreatedById;
                pensionPayments.EmployeeId = EmployeePensionPayment.EmployeeId.Value;
                pensionPayments.VoucherNo = obj.VoucherNo;
                pensionPayments.VoucherReferenceNo = obj.ReferenceNo;

                _uow.PensionPaymentHistoryRepository.Add(pensionPayments);

                var user = await _uow.UserDetailsRepository.FindAsync(x => x.AspNetUserId == EmployeePensionPayment.CreatedById);

                LoggerDetailsModel loggerObj = new LoggerDetailsModel();
                loggerObj.NotificationId = (int)Common.Enums.LoggerEnum.VoucherCreated;
                loggerObj.IsRead = false;
                loggerObj.UserName = user.FirstName + " " + user.LastName;
                loggerObj.UserId = EmployeePensionPayment.CreatedById;
                loggerObj.LoggedDetail = "Voucher " + obj.ReferenceNo + " Created";
                loggerObj.CreatedDate = DateTime.Now;

                response.LoggerDetailsModel = loggerObj;
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
        /// Generating Salary Voucher for an employee
        /// </summary>
        /// <param name="EmployeePensionPayment"></param>
        /// <returns>Boolean</returns>
        public async Task<APIResponse> GenerateSalaryVoucher(EmployeeSalaryVoucherModel EmployeeSalaryVoucher)
        {
            APIResponse response = new APIResponse();

            try
            {
                List<VoucherTransactions> voucherTransactionsList = new List<VoucherTransactions>();

                //for gross salary= basicpay * totalworkhours
                decimal? grossSalary = EmployeeSalaryVoucher.EmployeePayrollLists.Where(x => x.HeadTypeId == (int)SalaryHeadType.GENERAL).Sum(x => x.MonthlyAmount) * EmployeeSalaryVoucher.PresentHours;

                //total Allowances of an employee over a month
                decimal? totalAllowance = EmployeeSalaryVoucher.EmployeePayrollLists.Where(x => x.HeadTypeId == (int)SalaryHeadType.ALLOWANCE).Sum(x => x.MonthlyAmount);

                //total deductions of an employee over a month
                decimal? totalDeductions = EmployeeSalaryVoucher.EmployeePayrollLists.Where(x => x.HeadTypeId == (int)SalaryHeadType.DEDUCTION).Sum(x => x.MonthlyAmount);

                //total salary payable to employee in a month
                decimal? totalSalaryOfEmployee = (grossSalary + totalAllowance) - totalDeductions;

                var officeCode = _uow.OfficeDetailRepository.FindAsync(o => o.OfficeId == EmployeeSalaryVoucher.OfficeId).Result.OfficeCode; //use OfficeCode
                var financialYear = _uow.GetDbContext().FinancialYearDetail.FirstOrDefault(x => x.IsDefault == true && x.IsDeleted == false);
                var EmployeeDetails = _uow.GetDbContext().EmployeeDetail.FirstOrDefault(x => x.EmployeeID == EmployeeSalaryVoucher.EmployeeId && x.IsDeleted == false);

                //Creating Voucher for Voucher transaction
                VoucherDetail obj = new VoucherDetail();
                obj.CreatedById = EmployeeSalaryVoucher.CreatedById;
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;
                obj.FinancialYearId = financialYear.FinancialYearId;
                obj.VoucherTypeId = (int)VoucherTypes.Journal;
                obj.Description = string.Format(StaticResource.SalaryPaymentDone, DateTime.Now.Date, EmployeeDetails.EmployeeName);
                obj.CurrencyId = EmployeeSalaryVoucher.CurrencyId;
                obj.VoucherDate = DateTime.Now;
                obj.JournalCode = EmployeeSalaryVoucher.JournalCode;//null for now as per client
                obj.OfficeId = EmployeeSalaryVoucher.OfficeId;

                await _uow.VoucherDetailRepository.AddAsyn(obj);

                obj.ReferenceNo = officeCode + "-" + obj.VoucherNo;
                await _uow.VoucherDetailRepository.UpdateAsyn(obj);

                foreach (SalaryHeadModel salaryhead in EmployeeSalaryVoucher.EmployeePayrollLists)
                {

                    VoucherTransactions xVoucherTransactions = new VoucherTransactions();
                    //Creating Voucher Transaction for Credit
                    xVoucherTransactions.IsDeleted = false;
                    xVoucherTransactions.VoucherNo = obj.VoucherNo;
                    xVoucherTransactions.FinancialYearId = financialYear.FinancialYearId;
                    xVoucherTransactions.CurrencyId = EmployeeSalaryVoucher.CurrencyId;
                    xVoucherTransactions.OfficeId = EmployeeSalaryVoucher.OfficeId;

                    try
                    {
                        //Include only salary heads in voucher that contain transaction type ""
                        if (salaryhead.TransactionTypeId != null && salaryhead.TransactionTypeId != 0)
                        {
                            //Include only salary heads in voucher that has transaction type as credit and salary head type is not general
                            if (salaryhead.TransactionTypeId == (int)TransactionType.Debit && (salaryhead.MonthlyAmount != null && salaryhead.MonthlyAmount != 0) && salaryhead.HeadTypeId != (int)SalaryHeadType.GENERAL)
                            {
                                xVoucherTransactions.ChartOfAccountNewId = salaryhead.AccountNo;
                                xVoucherTransactions.DebitAccount = salaryhead.AccountNo;
                                xVoucherTransactions.Description = string.Format(StaticResource.SalaryHeadAllowances, salaryhead.HeadName);
                                xVoucherTransactions.Debit = Convert.ToDouble(salaryhead.MonthlyAmount);
                                xVoucherTransactions.Credit = 0;

                                //Note : These values are associated with Voucher and Transactions
                                xVoucherTransactions.TransactionDate = obj.VoucherDate;
                                xVoucherTransactions.FinancialYearId = obj.FinancialYearId;
                                xVoucherTransactions.CurrencyId = obj.CurrencyId;

                                voucherTransactionsList.Add(xVoucherTransactions);

                                //await _uow.GetDbContext().VoucherTransactions.AddAsync(xVoucherTransactions);
                                //await _uow.SaveAsync();

                            }//Include only salary heads in voucher that has transaction type as debit and salary head type is not general
                            else if (salaryhead.TransactionTypeId == (int)TransactionType.Credit && (salaryhead.MonthlyAmount != null && salaryhead.MonthlyAmount != 0) && salaryhead.HeadTypeId != (int)SalaryHeadType.GENERAL)
                            {
                                xVoucherTransactions.ChartOfAccountNewId = salaryhead.AccountNo;
                                xVoucherTransactions.CreditAccount = salaryhead.AccountNo;
                                xVoucherTransactions.Description = string.Format(StaticResource.SalaryHeadDeductions, salaryhead.HeadName);
                                xVoucherTransactions.Credit = Convert.ToDouble(salaryhead.MonthlyAmount);
                                xVoucherTransactions.Debit = 0;

                                voucherTransactionsList.Add(xVoucherTransactions);

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                foreach (PayrollHeadModel payrollHead in EmployeeSalaryVoucher.EmployeePayrollListPrimary)
                {
                    VoucherTransactions xVoucherTransactions = new VoucherTransactions();
                    //Creating Voucher Transaction for Credit
                    xVoucherTransactions.IsDeleted = false;
                    xVoucherTransactions.VoucherNo = obj.VoucherNo;
                    xVoucherTransactions.FinancialYearId = financialYear.FinancialYearId;
                    xVoucherTransactions.CurrencyId = EmployeeSalaryVoucher.CurrencyId;
                    xVoucherTransactions.OfficeId = EmployeeSalaryVoucher.OfficeId;

                    try
                    {
                        //Include only salary heads in voucher that contain transaction type ""
                        if (payrollHead.TransactionTypeId != null && payrollHead.TransactionTypeId != 0)
                        {
                            //Include only salary heads in voucher that has transaction type as credit
                            if (payrollHead.TransactionTypeId == (int)TransactionType.Debit && (payrollHead.Amount != null && payrollHead.Amount != 0))
                            {
                                xVoucherTransactions.ChartOfAccountNewId = payrollHead.AccountNo;
                                xVoucherTransactions.DebitAccount = payrollHead.AccountNo;

                                if (payrollHead.PayrollHeadTypeId != (int)SalaryHeadType.GENERAL)
                                {
                                    xVoucherTransactions.Description = string.Format(StaticResource.SalaryHeadAllowances, payrollHead.PayrollHeadName);
                                }
                                else
                                {
                                    xVoucherTransactions.Description = payrollHead.PayrollHeadName + "Debited";
                                }

                                xVoucherTransactions.Debit = Convert.ToDouble(payrollHead.Amount);
                                xVoucherTransactions.Credit = 0;
                                xVoucherTransactions.TransactionDate = obj.VoucherDate;
                                xVoucherTransactions.FinancialYearId = obj.FinancialYearId;
                                xVoucherTransactions.CurrencyId = obj.CurrencyId;

                                voucherTransactionsList.Add(xVoucherTransactions);

                                //await _uow.GetDbContext().VoucherTransactions.AddAsync(xVoucherTransactions);
                                //await _uow.SaveAsync();

                            }//Include only salary heads in voucher that has transaction type as debit
                            else if (payrollHead.TransactionTypeId == (int)TransactionType.Credit && (payrollHead.Amount != null && payrollHead.Amount != 0))
                            {
                                xVoucherTransactions.ChartOfAccountNewId = Convert.ToInt32(payrollHead.AccountNo);
                                xVoucherTransactions.CreditAccount = Convert.ToInt32(payrollHead.AccountNo);

                                if (payrollHead.PayrollHeadTypeId != (int)SalaryHeadType.GENERAL)
                                {
                                    xVoucherTransactions.Description = string.Format(StaticResource.SalaryHeadDeductions, payrollHead.PayrollHeadName);
                                }
                                else
                                {
                                    xVoucherTransactions.Description = payrollHead.PayrollHeadName + "Credited";
                                }

                                xVoucherTransactions.Credit = Convert.ToDouble(payrollHead.Amount);
                                xVoucherTransactions.Debit = 0;
                                xVoucherTransactions.TransactionDate = obj.VoucherDate;
                                xVoucherTransactions.FinancialYearId = obj.FinancialYearId;
                                xVoucherTransactions.CurrencyId = obj.CurrencyId;

                                voucherTransactionsList.Add(xVoucherTransactions);

                                //await _uow.GetDbContext().VoucherTransactions.AddAsync(xVoucherTransactions);
                                //await _uow.SaveAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                //Creating Voucher transactions for Gross Salary it is being calculated in this method so we need to insert record for it separately
                if (grossSalary != null && grossSalary != 0)
                {
                    VoucherTransactions xVoucherTransactions = new VoucherTransactions();
                    //Creating Voucher Transaction for Credit
                    xVoucherTransactions.IsDeleted = false;
                    xVoucherTransactions.VoucherNo = obj.VoucherNo;
                    xVoucherTransactions.FinancialYearId = financialYear.FinancialYearId;
                    xVoucherTransactions.CurrencyId = EmployeeSalaryVoucher.CurrencyId;
                    xVoucherTransactions.OfficeId = EmployeeSalaryVoucher.OfficeId;

                    xVoucherTransactions.ChartOfAccountNewId = Convert.ToInt32(EmployeeSalaryVoucher.EmployeePayrollLists.FirstOrDefault(x => x.HeadTypeId == (int)SalaryHeadType.GENERAL).AccountNo);
                    xVoucherTransactions.CreditAccount = xVoucherTransactions.ChartOfAccountNewId;
                    xVoucherTransactions.Description = "Basic Pay Debited";
                    xVoucherTransactions.Debit = Convert.ToDouble(grossSalary);
                    xVoucherTransactions.Credit = 0;
                    xVoucherTransactions.TransactionDate = obj.VoucherDate;
                    xVoucherTransactions.FinancialYearId = obj.FinancialYearId;
                    xVoucherTransactions.CurrencyId = obj.CurrencyId;

                    voucherTransactionsList.Add(xVoucherTransactions);

                    await _uow.GetDbContext().VoucherTransactions.AddRangeAsync(voucherTransactionsList);
                    await _uow.SaveAsync();

                }

                //Creating an entry in EmployeeSalaryPaymentHistory Table
                EmployeeSalaryPaymentHistory employeeSalaryPaymentHistory = new EmployeeSalaryPaymentHistory();
                employeeSalaryPaymentHistory.CreatedById = EmployeeSalaryVoucher.CreatedById;
                employeeSalaryPaymentHistory.CreatedDate = DateTime.Now;
                employeeSalaryPaymentHistory.IsDeleted = false;
                employeeSalaryPaymentHistory.EmployeeId = EmployeeSalaryVoucher.EmployeeId;
                employeeSalaryPaymentHistory.VoucherNo = obj.VoucherNo;
                employeeSalaryPaymentHistory.IsSalaryReverse = false;
                employeeSalaryPaymentHistory.Year = DateTime.Now.Year;
                employeeSalaryPaymentHistory.Month = DateTime.Now.Month;

                await _uow.EmployeeSalaryPaymentHistoryRepository.AddAsyn(employeeSalaryPaymentHistory);

                response.data.VoucherReferenceNo = obj.ReferenceNo;
                response.data.VoucherNo = obj.VoucherNo;

                var user = await _uow.UserDetailsRepository.FindAsync(x => x.AspNetUserId == EmployeeSalaryVoucher.CreatedById);

                LoggerDetailsModel loggerObj = new LoggerDetailsModel();
                loggerObj.NotificationId = (int)Common.Enums.LoggerEnum.VoucherCreated;
                loggerObj.IsRead = false;
                loggerObj.UserName = user.FirstName + " " + user.LastName;
                loggerObj.UserId = EmployeeSalaryVoucher.CreatedById;
                loggerObj.LoggedDetail = "Voucher " + obj.ReferenceNo + " Created";
                loggerObj.CreatedDate = DateTime.Now;

                response.LoggerDetailsModel = loggerObj;
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
        /// Get Employee Salary Voucher Details by Employee Id, Year and Month
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns>Voucher Reference No</returns>
        public async Task<APIResponse> GetEmployeeSalaryVoucher(int EmployeeId, int Month, int Year)
        {
            APIResponse response = new APIResponse();

            VoucherDetail voucherDetail = new VoucherDetail();

            try
            {
                EmployeeSalaryPaymentHistory employeeSalaryPaymentHistory = await _uow.GetDbContext().EmployeeSalaryPaymentHistory.FirstOrDefaultAsync(x => x.IsDeleted == false
                                                                                 && x.IsSalaryReverse == false
                                                                                 && x.EmployeeId == EmployeeId && x.Year == Year
                                                                                 && x.Month == Month);

                if (employeeSalaryPaymentHistory != null)
                {
                    voucherDetail = _uow.VoucherDetailRepository.Find(x => x.VoucherNo == employeeSalaryPaymentHistory.VoucherNo);
                }

                response.data.VoucherNo = voucherDetail.VoucherNo;
                response.data.VoucherReferenceNo = voucherDetail.ReferenceNo;
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
        /// Reverse Employee Salary Voucher
        /// </summary>
        /// <param name="VoucherNo"></param>
        /// <param name="UserId"></param>
        /// <returns>Void</returns>
        public async Task<APIResponse> ReverseEmployeeSalaryVoucher(long VoucherNo, string UserId)
        {
            APIResponse response = new APIResponse();

            VoucherDetail voucherDetail = new VoucherDetail();

            try
            {
                //Retrieving the list of transactions based on voucher no
                List<VoucherTransactions> voucherTransactionDetails = await _uow.GetDbContext().VoucherTransactions.Where(x => x.IsDeleted == false
                                                                                 && x.VoucherNo == VoucherNo).ToListAsync();

                //var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.OfficeId == 16).OrderByDescending(x => x.Date).ToListAsync();

                if (voucherTransactionDetails.Any())
                {
                    //looping each transaction and reversing it.
                    foreach (VoucherTransactions transaction in voucherTransactionDetails)
                    {
                        VoucherTransactions reverseTransactions = new VoucherTransactions();

                        reverseTransactions.Debit = transaction.Credit;
                        reverseTransactions.CreditAccount = transaction.DebitAccount;
                        reverseTransactions.DebitAccount = transaction.CreditAccount;
                        reverseTransactions.Credit = transaction.Debit;
                        reverseTransactions.CurrencyId = transaction.CurrencyId;
                        reverseTransactions.FinancialYearId = transaction.FinancialYearId;
                        reverseTransactions.ChartOfAccountNewId = transaction.ChartOfAccountNewId;
                        reverseTransactions.OfficeId = transaction.OfficeId;
                        reverseTransactions.VoucherNo = transaction.VoucherNo;
                        reverseTransactions.IsDeleted = false;
                        reverseTransactions.TransactionDate = voucherDetail.VoucherDate;

                        await _uow.GetDbContext().VoucherTransactions.AddAsync(reverseTransactions);
                        await _uow.SaveAsync();

                        //APIResponse apiResponse = await AddVoucherTransactionConvertedToExchangeRate(reverseTransactions, exchangeRate);
                    }
                }

                //Getting the Salary Payment history record and updating the flag isSalaryReversed to true
                EmployeeSalaryPaymentHistory employeeSalaryPaymentHistory = _uow.EmployeeSalaryPaymentHistoryRepository.Find(x => x.IsDeleted == false && x.IsSalaryReverse == false
                                                                                                                             && x.VoucherNo == VoucherNo);
                employeeSalaryPaymentHistory.IsSalaryReverse = true;

                await _uow.EmployeeSalaryPaymentHistoryRepository.UpdateAsyn(employeeSalaryPaymentHistory);

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
        /// Disapprove Employee Approved Salary
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> DisapproveEmployeeApprovedSalary(DisapprovePayrollModel model, string UserId)
        {
            APIResponse response = new APIResponse();

            try
            {

                foreach (Employees Employee in model.EmployeeList)
                {
                    //Check if salary paymet is done for an approved payroll
                    EmployeeSalaryPaymentHistory employeeSalaryPaymentHistory = _uow.EmployeeSalaryPaymentHistoryRepository.Find(x => x.EmployeeId == Employee.EmployeeId
                                                                                                                                && x.Year == model.Year && x.Month == model.Month && x.IsSalaryReverse == false
                                                                                                                                && x.IsDeleted == false);
                    //if salary payment is already made
                    if (employeeSalaryPaymentHistory != null)
                    {
                        //Getting the details for salary payment voucher
                        VoucherDetail voucherDetail = _uow.VoucherDetailRepository.Find(x => x.IsDeleted == false && x.VoucherNo == employeeSalaryPaymentHistory.VoucherNo);

                        if (voucherDetail != null)
                        {
                            //Reverse the salary payment for the approved payroll of an employee
                            APIResponse apiResponse = await ReverseEmployeeSalaryVoucher(voucherDetail.VoucherNo, UserId);

                            if (apiResponse.StatusCode == 200)
                            {
                                //Update salary reversed flag in table employeesalarypaymenthistory 
                                employeeSalaryPaymentHistory.IsSalaryReverse = true;
                                await _uow.EmployeeSalaryPaymentHistoryRepository.UpdateAsyn(employeeSalaryPaymentHistory);

                                //Retrieving Approved payroll for the month
                                EmployeePaymentTypes employeePaymentTypes = await _uow.EmployeePaymentTypeRepository.FindAsync(x => x.IsDeleted == false && x.PayrollYear == model.Year
                                                                                                                    && x.PayrollMonth == model.Month &&
                                                                                                                    x.EmployeeID == Employee.EmployeeId);

                                employeePaymentTypes.IsDeleted = true;

                                //update approved employee payroll table
                                await _uow.EmployeePaymentTypeRepository.UpdateAsyn(employeePaymentTypes);

                                //get employee monthly salary heads 
                                List<EmployeePayrollMonth> EmployeePayrollMonthList = await _uow.GetDbContext().EmployeePayrollMonth.Where(x => x.IsDeleted == false && x.Date.Month == model.Month && x.Date.Year == model.Year && x.EmployeeID == Employee.EmployeeId).ToListAsync();

                                //set each monthly salary head to isdeleted false
                                EmployeePayrollMonthList.ForEach(x => x.IsDeleted = true);

                                //update employee monthly salary heads 
                                _uow.GetDbContext().EmployeePayrollMonth.UpdateRange(EmployeePayrollMonthList);
                                _uow.Save();

                                //Retrieving employee monthly attendance record
                                EmployeeMonthlyAttendance employeeMonthlyAttendance = _uow.EmployeeMonthlyAttendanceRepository.Find(x => x.IsDeleted == false && x.EmployeeId == Employee.EmployeeId
                                                                                                                                    && x.Month == model.Month && x.Year == model.Year);

                                //Setting monthly attendance approved to false
                                employeeMonthlyAttendance.IsApproved = false;
                                employeeMonthlyAttendance.AdvanceAmount = 0;
                                employeeMonthlyAttendance.AdvanceRecoveryAmount = 0;
                                employeeMonthlyAttendance.GrossSalary = 0;
                                employeeMonthlyAttendance.NetSalary = 0;
                                employeeMonthlyAttendance.PensionAmount = 0;
                                employeeMonthlyAttendance.SalaryTax = 0;
                                employeeMonthlyAttendance.TotalAllowance = 0;
                                employeeMonthlyAttendance.TotalDeduction = 0;

                                await _uow.EmployeeMonthlyAttendanceRepository.UpdateAsyn(employeeMonthlyAttendance);
                            }
                        }
                    }
                    else
                    {
                        //Retrieving Approved payroll for the month
                        EmployeePaymentTypes employeePaymentTypes = await _uow.EmployeePaymentTypeRepository.FindAsync(x => x.IsDeleted == false && x.PayrollYear == model.Year
                                                                                                            && x.PayrollMonth == model.Month &&
                                                                                                            x.EmployeeID == Employee.EmployeeId);

                        employeePaymentTypes.IsDeleted = true;

                        //update approved employee payroll table
                        await _uow.EmployeePaymentTypeRepository.UpdateAsyn(employeePaymentTypes);

                        //get employee monthly salary heads 
                        List<EmployeePayrollMonth> EmployeePayrollMonthList = await _uow.GetDbContext().EmployeePayrollMonth.Where(x => x.IsDeleted == false && x.Date.Month == model.Month && x.Date.Year == model.Year && x.EmployeeID == Employee.EmployeeId).ToListAsync();

                        //set each monthly salary head to isdeleted false
                        EmployeePayrollMonthList.ForEach(x => x.IsDeleted = true);

                        //update employee monthly salary heads 
                        _uow.GetDbContext().EmployeePayrollMonth.UpdateRange(EmployeePayrollMonthList);
                        _uow.Save();

                        //Retrieving employee monthly attendance record
                        EmployeeMonthlyAttendance employeeMonthlyAttendance = _uow.EmployeeMonthlyAttendanceRepository.Find(x => x.IsDeleted == false && x.EmployeeId == Employee.EmployeeId
                                                                                                                            && x.Month == model.Month && x.Year == model.Year);
                        employeeMonthlyAttendance.AdvanceAmount = 0;
                        employeeMonthlyAttendance.AdvanceRecoveryAmount = 0;
                        employeeMonthlyAttendance.GrossSalary = 0;
                        employeeMonthlyAttendance.NetSalary = 0;
                        employeeMonthlyAttendance.PensionAmount = 0;
                        employeeMonthlyAttendance.SalaryTax = 0;
                        employeeMonthlyAttendance.TotalAllowance = 0;
                        employeeMonthlyAttendance.TotalDeduction = 0;

                        //Setting monthly attendance approved to false
                        employeeMonthlyAttendance.IsApproved = false;

                        await _uow.EmployeeMonthlyAttendanceRepository.UpdateAsyn(employeeMonthlyAttendance);
                    }
                }

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


        /// <summary>
        /// This is a common function used for transactions made for employees
        /// </summary>
        /// <param name="model"></param>
        /// /// <param name="exchangeRate"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddVoucherTransactionConvertedToExchangeRate(VoucherTransactionModel model, List<ExchangeRate> exchangeRate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var voucherDetail = await _uow.VoucherDetailRepository.FindAsync(x => x.VoucherNo == model.VoucherNo);


                //Note : These values are associated with Voucher and Transactions
                model.TransactionDate = voucherDetail.VoucherDate;
                model.FinancialYearId = voucherDetail.FinancialYearId;
                model.CurrencyId = voucherDetail.CurrencyId;

                VoucherTransactions obj = _mapper.Map<VoucherTransactions>(model);

                if (obj.CurrencyId == (int)Currency.AFG)
                {
                    obj.AFGAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit), 4) : Math.Round(Convert.ToDouble(obj.Credit), 4);

                    var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                    if (exchangeRateToAFG == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                        }
                    }

                    var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                    if (exchangeRateToEuro == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                        }
                    }

                    var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                    if (exchangeRateToUSD == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                        }
                    }


                    obj.EURAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * exchangeRateToAFG.Rate) / (exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * exchangeRateToAFG.Rate) / (exchangeRateToEuro.Rate)), 4);
                    obj.PKRAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * exchangeRateToAFG.Rate)), 4);
                    obj.USDAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * exchangeRateToAFG.Rate) / (exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * exchangeRateToAFG.Rate) / (exchangeRateToUSD.Rate)), 4);
                }

                if (obj.CurrencyId == (int)Currency.EUR)
                {
                    obj.EURAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit), 4) : Math.Round(Convert.ToDouble(obj.Credit), 4);

                    var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                    if (exchangeRateToAFG == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                        }
                    }

                    var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                    if (exchangeRateToEuro == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                        }
                    }

                    var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                    if (exchangeRateToUSD == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                        }
                    }


                    obj.AFGAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * exchangeRateToEuro.Rate) / (exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * exchangeRateToEuro.Rate) / (exchangeRateToAFG.Rate)), 4);
                    obj.PKRAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * exchangeRateToEuro.Rate)), 4);
                    obj.USDAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * exchangeRateToEuro.Rate) / (exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * exchangeRateToEuro.Rate) / (exchangeRateToUSD.Rate)), 4);
                }

                if (obj.CurrencyId == (int)Currency.PKR)
                {

                    obj.PKRAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit), 4) : Math.Round(Convert.ToDouble(obj.Credit), 4);

                    var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                    if (exchangeRateToAFG == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                        }
                    }

                    var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                    if (exchangeRateToEuro == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                        }
                    }

                    var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                    if (exchangeRateToUSD == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                        }
                    }


                    obj.AFGAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit) / (exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit) / (exchangeRateToAFG.Rate)), 4);
                    obj.EURAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit) / (exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit) / (exchangeRateToEuro.Rate)), 4);
                    obj.USDAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit) / (exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit) / (exchangeRateToUSD.Rate)), 4);
                }

                if (obj.CurrencyId == (int)Currency.USD)
                {
                    obj.USDAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble(obj.Debit), 4) : Math.Round(Convert.ToDouble(obj.Credit), 4);

                    var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                    if (exchangeRateToAFG == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                        }
                    }

                    var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                    if (exchangeRateToEuro == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                        }
                    }

                    var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == obj.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                    if (exchangeRateToUSD == null)
                    {
                        var dateForExchangeRate = obj.TransactionDate.Value.Date.AddDays(-1);
                        exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                        }
                    }


                    obj.AFGAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * exchangeRateToUSD.Rate) / (exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * exchangeRateToUSD.Rate) / (exchangeRateToAFG.Rate)), 4);
                    obj.PKRAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * exchangeRateToUSD.Rate)), 4);
                    obj.EURAmount = obj.Debit != 0 ? Math.Round(Convert.ToDouble((obj.Debit * exchangeRateToUSD.Rate) / (exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((obj.Credit * exchangeRateToUSD.Rate) / (exchangeRateToEuro.Rate)), 4);
                }

                await _uow.GetDbContext().VoucherTransactions.AddAsync(obj);
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

        public async Task<APIResponse> GetVoucherDetailByVoucherNo(long VoucherNo)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<VoucherDetail> voucherList = new List<VoucherDetail>();

                voucherList = await _uow.GetDbContext().VoucherDetail
                                              .Include(o => o.OfficeDetails).Include(j => j.JournalDetails)
                                              .Include(c => c.CurrencyDetail)
                                              .Include(f => f.FinancialYearDetails)
                                              .Where(v => v.IsDeleted == false && v.VoucherNo == VoucherNo)
                                              .OrderByDescending(x => x.VoucherDate)
                                              .ToListAsync();

                List<VoucherDetailModel> voucherFilteredList = new List<VoucherDetailModel>();
                foreach (var i in voucherList)
                {
                    VoucherDetailModel obj = new VoucherDetailModel();
                    obj.VoucherNo = i.VoucherNo;
                    obj.CurrencyCode = i.CurrencyDetail?.CurrencyCode ?? null;
                    obj.CurrencyId = i.CurrencyDetail?.CurrencyId ?? 0;
                    obj.VoucherDate = i.VoucherDate;
                    obj.ChequeNo = i.ChequeNo;
                    obj.ReferenceNo = i.ReferenceNo;
                    obj.Description = i.Description;
                    obj.JournalName = i.JournalDetails?.JournalName ?? null;
                    obj.JournalCode = i.JournalDetails?.JournalCode ?? null;
                    obj.VoucherTypeId = i.VoucherTypeId;
                    obj.OfficeId = i.OfficeId;
                    obj.ProjectId = i.ProjectId;
                    obj.BudgetLineId = i.BudgetLineId;
                    obj.OfficeName = i.OfficeDetails?.OfficeName ?? null;
                    obj.FinancialYearId = i.FinancialYearId;
                    obj.FinancialYearName = i.FinancialYearDetails?.FinancialYearName ?? null;
                    voucherFilteredList.Add(obj);
                }

                response.data.VoucherDetailList = voucherFilteredList.OrderByDescending(v => v.VoucherDate).ToList();
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