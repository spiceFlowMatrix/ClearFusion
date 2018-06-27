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



		public async Task<APIResponse> GetAllVoucherDetailsByFilter(VoucherFilterModel filterModel)
		{
			APIResponse response = new APIResponse();
			try
			{
				if (filterModel != null)
				{

					if (filterModel.Date == null && filterModel.OfficesList != null)
					{
						var voucherList = await _uow.GetDbContext().VoucherDetail
													  .Include(o => o.OfficeDetails).Include(j => j.JournalDetails)
													  .Include(c => c.CurrencyDetail)
													  .Include(f => f.FinancialYearDetails)
													  .Where(v => v.IsDeleted == false).OrderBy(x => x.VoucherDate).ToListAsync();

						List<VoucherDetailModel> voucherFilteredList = new List<VoucherDetailModel>();


						foreach (var item in filterModel.OfficesList)
						{


							var voucherData = voucherList.FindAll(v => v.OfficeId == item);
							if (voucherData != null)
							{
								//List<VoucherDetailModel> voucherFilteredList = new List<VoucherDetailModel>();

								foreach (var i in voucherData)
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


							}
						}

						response.data.VoucherDetailList = voucherFilteredList.OrderBy(v => v.VoucherDate).ToList();

					}



					else if (filterModel.Date != null && filterModel.OfficesList != null)
					{
						var voucherList = await _uow.GetDbContext().VoucherDetail
													  .Include(o => o.OfficeDetails).Include(j => j.JournalDetails)
													  .Include(c => c.CurrencyDetail)
													  .Include(f => f.FinancialYearDetails)
													  .Where(v => v.IsDeleted == false && v.VoucherDate.Value.Date == filterModel.Date.Value.Date).OrderBy(x => x.VoucherDate).ToListAsync();

						List<VoucherDetailModel> voucherFilteredList = new List<VoucherDetailModel>();


						foreach (var item in filterModel.OfficesList)
						{


							var voucherData = voucherList.FindAll(v => v.OfficeId == item);
							if (voucherData != null)
							{
								//List<VoucherDetailModel> objList = new List<VoucherDetailModel>();

								foreach (var i in voucherData)
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


							}
						}

						response.data.VoucherDetailList = voucherFilteredList.OrderBy(v => v.VoucherDate).ToList();

					}
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
				  _uow.GetDbContext().JournalDetail.Include(e => e.VoucherDetails).ThenInclude(e => e.VoucherTransactionDetails).ThenInclude(e => e.CreditAccountDetails).ToList()
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
				List<JournalDetail> list = new List<JournalDetail>();
				var journalcodeList = await Task.Run(() =>
				  _uow.GetDbContext().JournalDetail.Include(e => e.VoucherDetails).ThenInclude(p => p.VoucherTransactionDetails).ThenInclude(p => p.CreditAccountDetails).Include(e => e.VoucherDetails).ThenInclude(c => c.ProjectBudgetLine).ThenInclude(c => c.ProjectDetails).Where(x => x.IsDeleted == false && model.JournalCode.Contains(x.JournalCode)).ToList()
					);
				List<ExchangeRate> exchangeratelist = new List<ExchangeRate>();
				if (model.fromdate == null && model.todate == null)
				{
					model.fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
					model.todate = DateTime.UtcNow;
				}
				exchangeratelist = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Date >= model.fromdate.Date && x.Date.Date <= model.todate.Date).ToListAsync();

				List<JournalVoucherViewModel> listJournalView = new List<JournalVoucherViewModel>();
				foreach (var j in journalcodeList)
				{
					foreach (var v in j.VoucherDetails)
					{
						foreach (var officeId in model.OfficesList)
						{
							if (officeId == v.OfficeId)
							{
								foreach (var transactions in v.VoucherTransactionDetails)
								{
									if (transactions.TransactionDate.Date >= model.fromdate.Date && transactions.TransactionDate.Date <= model.todate.Date)
									{
										var creditAccount = await _uow.ChartAccountDetailRepository.FindAsync(x => x.IsDeleted == false && x.AccountCode == transactions.CreditAccount);
										var debitAccount = await _uow.ChartAccountDetailRepository.FindAsync(x => x.IsDeleted == false && x.AccountCode == transactions.DebitAccount);
										if (model.RecordType == 1)
										{
											if (model.CurrencyId == transactions.CurrencyId)
											{
												// Credit
												JournalVoucherViewModel obj = new JournalVoucherViewModel();
												obj.TransactionDate = transactions.TransactionDate;
												obj.VoucherNo = transactions?.VoucherNo ?? 0;
												obj.TransactionDescription = transactions?.Description ?? null;
												obj.CurrencyId = transactions.CurrencyId;
												obj.Project = v.ProjectDetails?.Description ?? null;
												obj.BudgetLineDescription = v.ProjectBudgetLine?.Description ?? null;
												obj.AccountCode = creditAccount?.ChartOfAccountCode ?? 0;
												obj.AccountName = creditAccount?.AccountName ?? null;
												obj.CreditAmount = transactions?.Amount ?? 0;
												obj.DebitAmount = 0;
												listJournalView.Add(obj);

												// Debit
												JournalVoucherViewModel obj1 = new JournalVoucherViewModel();
												obj1.TransactionDate = transactions.TransactionDate;
												obj1.VoucherNo = transactions?.VoucherNo ?? 0;
												obj1.TransactionDescription = transactions?.Description ?? null;
												obj1.CurrencyId = transactions.CurrencyId;
												obj1.Project = v.ProjectDetails?.Description ?? null;
												obj1.BudgetLineDescription = v.ProjectBudgetLine?.Description ?? null;
												obj1.AccountCode = debitAccount?.ChartOfAccountCode ?? 0;
												obj1.AccountName = debitAccount?.AccountName ?? null;
												obj1.CreditAmount = 0;
												obj1.DebitAmount = transactions?.Amount ?? 0;
												listJournalView.Add(obj1);
											}
										}
										else
										{
											if (model.CurrencyId == transactions.CurrencyId)
											{
												// Credit
												JournalVoucherViewModel obj = new JournalVoucherViewModel();
												obj.TransactionDate = transactions.TransactionDate;
												obj.VoucherNo = transactions?.VoucherNo ?? 0;
												obj.TransactionDescription = transactions?.Description ?? null;
												obj.CurrencyId = transactions.CurrencyId;
												obj.Project = v.ProjectDetails?.Description ?? null;
												obj.BudgetLineDescription = v.ProjectBudgetLine?.Description ?? null;
												obj.AccountCode = creditAccount?.ChartOfAccountCode ?? 0;
												obj.AccountName = creditAccount?.AccountName ?? null;
												obj.CreditAmount = transactions?.Amount ?? 0;
												obj.DebitAmount = 0;
												listJournalView.Add(obj);

												// Debit
												JournalVoucherViewModel obj1 = new JournalVoucherViewModel();
												obj1.TransactionDate = transactions.TransactionDate;
												obj1.VoucherNo = transactions?.VoucherNo ?? 0;
												obj1.TransactionDescription = transactions?.Description ?? null;
												obj1.CurrencyId = transactions.CurrencyId;
												obj1.Project = v.ProjectDetails?.Description ?? null;
												obj1.BudgetLineDescription = v.ProjectBudgetLine?.Description ?? null;
												obj1.AccountCode = debitAccount?.ChartOfAccountCode ?? 0;
												obj1.AccountName = debitAccount?.AccountName ?? null;
												obj1.CreditAmount = 0;
												obj1.DebitAmount = transactions?.Amount ?? 0;
												listJournalView.Add(obj1);
											}
											else
											{
												var exchangeRate = exchangeratelist.Where(x => x.IsDeleted == false && x.FromCurrency == transactions.CurrencyId && x.ToCurrency == model.CurrencyId).OrderByDescending(x => x.Date).FirstOrDefault().Rate;
												JournalVoucherViewModel obj = new JournalVoucherViewModel();
												obj.TransactionDate = transactions.TransactionDate;
												obj.VoucherNo = transactions?.VoucherNo ?? 0;
												obj.TransactionDescription = transactions?.Description ?? null;
												obj.CurrencyId = transactions.CurrencyId;
												obj.Project = v.ProjectDetails?.Description ?? null;
												obj.BudgetLineDescription = v.ProjectBudgetLine?.Description ?? null;
												obj.AccountCode = creditAccount?.ChartOfAccountCode ?? 0;
												obj.AccountName = creditAccount?.AccountName ?? null;
												obj.CreditAmount = exchangeRate * transactions?.Amount ?? 0;
												obj.DebitAmount = 0;
												listJournalView.Add(obj);

												// Debit
												JournalVoucherViewModel obj1 = new JournalVoucherViewModel();
												obj1.TransactionDate = transactions.TransactionDate;
												obj1.VoucherNo = transactions?.VoucherNo ?? 0;
												obj1.TransactionDescription = transactions?.Description ?? null;
												obj1.CurrencyId = transactions.CurrencyId;
												obj.Project = v.ProjectDetails?.Description ?? null;
												obj1.BudgetLineDescription = v.ProjectBudgetLine?.Description ?? null;
												obj1.AccountCode = debitAccount?.ChartOfAccountCode ?? 0;
												obj1.AccountName = debitAccount?.AccountName ?? null;
												obj1.CreditAmount = 0;
												obj1.DebitAmount = exchangeRate * transactions?.Amount ?? 0;
												listJournalView.Add(obj1);
											}
										}
									}
								}
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
				var accountcodelist = (from c in await _uow.ChartAccountDetailRepository.GetAllAsyn()
									   where c.IsDeleted == false
									   select new AccountDetailModel
									   {
										   AccountCode = c.AccountCode,
										   AccountName = c.ChartOfAccountCode + " - " + c.AccountName,
										   ChartOfAccountCode = c.ChartOfAccountCode
									   }).OrderBy(x => x.ChartOfAccountCode).ToList();
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

		public async Task<APIResponse> GetAllControlLevelAccountCode()
		{
			APIResponse response = new APIResponse();
			try
			{
				var accountcodelist = (from c in await _uow.ChartAccountDetailRepository.FindAllAsync(x => x.AccountLevelId == 2)
									   where c.IsDeleted == false
									   select new AccountDetailModel
									   {
										   AccountCode = c.AccountCode,
										   AccountName = c.ChartOfAccountCode + " - " + c.AccountName,
										   ChartOfAccountCode = c.ChartOfAccountCode
									   }).OrderBy(x => x.ChartOfAccountCode).ToList();
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
				response.data.VoucherTransactionList = tranlist;
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
				var transactionlist = await _uow.GetDbContext().VoucherTransactionDetails.Include(x => x.CreditAccountDetails).Include(x => x.DebitAccountDetails).Where(x => x.VoucherNo == VoucherNo && x.IsDeleted == false).ToListAsync();
				IList<VoucherTransactionModel> tranlist = new List<VoucherTransactionModel>();
				foreach (var item in transactionlist)
				{
					VoucherTransactionModel obj = new VoucherTransactionModel();
					obj.TransactionId = item.TransactionId;
					obj.TransactionDate = item.TransactionDate.ToLocalTime().Date;
					obj.VoucherNo = item.VoucherNo;
					obj.Description = item?.Description;
					obj.DebitAmount = item?.Amount;
					obj.DebitAccount = item?.DebitAccount;
					obj.Amount = item.Amount;
					obj.CreditAmount = item?.Amount;
					obj.CreditAccount = item?.CreditAccount;
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

		public async Task<APIResponse> AddVoucherTransactionDetail(VoucherTransactionModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				VoucherTransactionDetails obj = _mapper.Map<VoucherTransactionDetails>(model);

				await _uow.GetDbContext().VoucherTransactionDetails.AddAsync(obj);
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


		public async Task<APIResponse> DeleteVoucherTransactionDetail(int transactionId, string modifiedById)
		{
			APIResponse response = new APIResponse();
			try
			{
				var transactionInfo = await _uow.VoucherTransactionDetailsRepository.FindAsync(d => d.TransactionId == transactionId);
				if (transactionInfo != null)
				{
					transactionInfo.ModifiedById = modifiedById;
					transactionInfo.ModifiedDate = DateTime.UtcNow;
					transactionInfo.IsDeleted = true;
					await _uow.VoucherTransactionDetailsRepository.UpdateAsyn(transactionInfo);
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

		public async Task<APIResponse> GetAllLedgerDetails()
		{

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

		public async Task<APIResponse> GetAllLedgerDetailsByCondition(LedgerModels model)
		{
			APIResponse response = new APIResponse();
			try
			{
				List<ChartAccountDetail> ledgerList = new List<ChartAccountDetail>();

				ledgerList = await _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).Where(x => x.AccountLevelId == 4 && model.accountLists.Contains(x.AccountCode)).ToListAsync();

				//foreach (var account in model.accountLists)
				//{
				//	ChartAccountDetail obj = new ChartAccountDetail();
				//	obj = await _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).Where(x => x.AccountLevelId == 4 && x.AccountCode == account).FirstOrDefaultAsync();
				//	if (obj != null)
				//	{
				//		if (obj.CreditAccountlist.Count > 0 || obj.DebitAccountlist.Count > 0)
				//			ledgerList.Add(obj);
				//	}
				//}

				DateTime defaultdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
				if (model.fromdate == null && model.todate == null)
				{
					model.fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
					model.todate = DateTime.UtcNow;
				}

				double totalCredit = 0, totalDebit = 0, totalCreditPrevious = 0, totalDebitPrevious = 0;
				List<LedgerModel> finalLedgerList = new List<LedgerModel>();
				List<AccountTransactionLogger> lst = new List<AccountTransactionLogger>();
				foreach (var items in ledgerList)
				{
					// CHecks for the previous transactions of a particular account before the date range (For OPENING BALANCE)

					var accountData = await _uow.VoucherTransactionDetailsRepository.FindAllAsync(x => x.IsDeleted == false && (x.CreditAccount == items.AccountCode || x.DebitAccount == items.AccountCode) && x.TransactionDate.Date >= new DateTime(model.fromdate.Year, 1, 1).Date && x.TransactionDate.Date < model.fromdate.Date);
					foreach (var elements in accountData)
					{

						if (elements.CreditAccount == items.AccountCode)
						{
							if (model.RecordType == 1)
							{
								if (elements.CurrencyId == model.CurrencyId)
								{
									totalCreditPrevious += elements.Amount;
								}
							}
							else
							{
								if (elements.CurrencyId == model.CurrencyId)
								{
									totalCreditPrevious += elements.Amount;
								}
								else
								{
									var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == elements.CurrencyId && x.ToCurrency == model.CurrencyId).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
									totalCreditPrevious += elements.Amount * exchangeRate.Rate;
								}
							}
						}

						if (elements.DebitAccount == items.AccountCode)
						{
							if (model.RecordType == 1)
							{
								if (elements.CurrencyId == model.CurrencyId)
								{
									totalDebitPrevious += elements.Amount;
								}
							}
							else
							{
								if (elements.CurrencyId == model.CurrencyId)
								{
									totalDebitPrevious += elements.Amount;
								}
								else
								{
									var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == elements.CurrencyId && x.ToCurrency == model.CurrencyId).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
									totalDebitPrevious += elements.Amount * exchangeRate.Rate;
								}
							}
						}

						AccountTransactionLogger logger = new AccountTransactionLogger();
						logger.ClosingBalance = totalCreditPrevious - totalDebitPrevious;
						logger.TotalCredits = totalCreditPrevious;
						logger.TotalDebits = totalDebitPrevious;
						logger.AccountCode = items.AccountCode;
						logger.ChartAccountCode = items.ChartOfAccountCode;
						lst.Add(logger);

					}
				}

				var newList = lst.GroupBy(x => x.AccountCode).ToList();
				lst = new List<AccountTransactionLogger>();
				foreach (var final in newList)
				{
					totalCreditPrevious = final.Sum(x => x.TotalCredits);
					totalDebitPrevious = final.Sum(x => x.TotalDebits);
					AccountTransactionLogger logger = new AccountTransactionLogger();
					logger.ClosingBalance = totalCreditPrevious - totalDebitPrevious;
					logger.TotalCredits = totalCreditPrevious;
					logger.TotalDebits = totalDebitPrevious;
					logger.AccountCode = final.Key;
					lst.Add(logger);
				}

				foreach (var items in ledgerList)
				{
					foreach (var creditList in items.CreditAccountlist)
					{

						if (creditList.TransactionDate.Date >= model.fromdate.Date && creditList.TransactionDate.Date <= model.todate.Date)
						{
							var currencyName = await _uow.CurrencyDetailsRepository.FindAsync(x => x.CurrencyId == creditList.CurrencyId);
							var voucherReferenceNo = await _uow.VoucherDetailRepository.FindAsync(x => x.IsDeleted == false && x.VoucherNo == creditList.VoucherNo);
							if (model.RecordType == 1)
							{
								if (creditList.CurrencyId == model.CurrencyId)
								{
									LedgerModel obj = new LedgerModel();
									obj.TransactionDate = creditList.TransactionDate;
									obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
									obj.Description = creditList.Description;
									obj.CurrencyName = currencyName?.CurrencyName ?? null;
									obj.AccountCode = items.ChartOfAccountCode;
									obj.AccountName = items.AccountName;
									obj.CreditAmount = creditList.Amount;
									obj.DebitAmount = 0;
									finalLedgerList.Add(obj);
									totalCredit += creditList.Amount;
								}
							}
							else
							{
								if (creditList.CurrencyId == model.CurrencyId)
								{
									LedgerModel obj = new LedgerModel();
									obj.TransactionDate = creditList.TransactionDate;
									obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
									obj.Description = creditList.Description;
									obj.CurrencyName = currencyName?.CurrencyName ?? null;
									obj.AccountCode = items.ChartOfAccountCode;
									obj.AccountName = items.AccountName;
									obj.CreditAmount = creditList.Amount;
									obj.DebitAmount = 0;
									finalLedgerList.Add(obj);

									totalCredit += creditList.Amount;
								}
								else
								{
									var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == creditList.CurrencyId && x.ToCurrency == model.CurrencyId).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
									LedgerModel obj = new LedgerModel();
									obj.TransactionDate = creditList.TransactionDate;
									obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
									obj.Description = creditList.Description;
									obj.CurrencyName = currencyName?.CurrencyName ?? null;
									obj.AccountCode = items.ChartOfAccountCode;
									obj.AccountName = items.AccountName;
									obj.CreditAmount = creditList.Amount * exchangeRate?.Rate ?? 0;
									obj.DebitAmount = 0;
									finalLedgerList.Add(obj);
									totalCredit += creditList.Amount * exchangeRate?.Rate ?? 0;
								}
							}
						}

					}

					foreach (var debitList in items.DebitAccountlist)
					{

						if (debitList.TransactionDate.Date >= model.fromdate.Date && debitList.TransactionDate.Date <= model.todate.Date)
						{
							var currencyName = await _uow.CurrencyDetailsRepository.FindAsync(x => x.CurrencyId == debitList.CurrencyId);
							var voucherReferenceNo = await _uow.VoucherDetailRepository.FindAsync(x => x.IsDeleted == false && x.VoucherNo == debitList.VoucherNo);
							if (model.RecordType == 1)
							{
								if (debitList.CurrencyId == model.CurrencyId)
								{
									LedgerModel obj = new LedgerModel();
									obj.TransactionDate = debitList.TransactionDate;
									obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
									obj.Description = debitList.Description;
									obj.CurrencyName = currencyName?.CurrencyName ?? null;
									obj.AccountCode = items.ChartOfAccountCode;
									obj.AccountName = items.AccountName;
									obj.CreditAmount = 0;
									obj.DebitAmount = debitList.Amount;
									finalLedgerList.Add(obj);
									totalDebit += debitList.Amount;
								}
							}
							else
							{
								if (debitList.CurrencyId == model.CurrencyId)
								{
									LedgerModel obj = new LedgerModel();
									obj.TransactionDate = debitList.TransactionDate;
									obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
									obj.Description = debitList.Description;
									obj.CurrencyName = currencyName?.CurrencyName ?? null;
									obj.AccountCode = items.ChartOfAccountCode;
									obj.AccountName = items.AccountName;
									obj.CreditAmount = 0;
									obj.DebitAmount = debitList.Amount;
									finalLedgerList.Add(obj);
									totalDebit += debitList.Amount;
								}
								else
								{
									var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == debitList.CurrencyId && x.ToCurrency == model.CurrencyId).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
									LedgerModel obj = new LedgerModel();
									obj.TransactionDate = debitList.TransactionDate;
									obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
									obj.Description = debitList.Description;
									obj.CurrencyName = currencyName?.CurrencyName ?? null;
									obj.AccountCode = items.ChartOfAccountCode;
									obj.AccountName = items.AccountName;
									obj.CreditAmount = 0;
									obj.DebitAmount = debitList.Amount * exchangeRate?.Rate ?? 0;
									finalLedgerList.Add(obj);
									totalDebit += debitList.Amount * exchangeRate?.Rate ?? 0;
								}
							}
						}

					}
				}

				// Till Now Final Ledger List is having all transactions of given date range of all accounts passed in accountList && newlist is having all previous transactions sum  account wise.


				// For opening And Closing Amount Calculation
				double? creditSum = 0, debitSum = 0;
				creditSum = finalLedgerList.Where(x => x.CreditAmount != 0).Sum(x => x.CreditAmount);
				debitSum = finalLedgerList.Where(x => x.DebitAmount != 0).Sum(x => x.DebitAmount);

				//foreach (var element in finalLedgerList)
				//{                    
				//    creditSum += finalLedgerList.Where(x => x.AccountCode == element.AccountCode && x.CreditAmount != 0).Sum(x => x.CreditAmount);
				//    debitSum = finalLedgerList.Where(x => x.AccountCode == element.AccountCode && x.DebitAmount != 0).Sum(x => x.DebitAmount);
				//    balance += creditSum - debitSum + element.ClosingBalance;             // element.ClosingBalance - for previous transaction amount 
				//}

				response.data.AccountOpendingAndClosingBL = new AccountOpendingAndClosingBL
				{
					OpeningBalance = lst.Sum(x => x.TotalCredits) - lst.Sum(x => x.TotalDebits),
					ClosingBalance = creditSum - debitSum + lst.Sum(x => x.TotalCredits) - lst.Sum(x => x.TotalDebits)
				};

				response.data.LedgerList = finalLedgerList;
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
				//response.data.TrailBlanceList = list;
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


		public async Task<APIResponse> GetTrailBlanceDetailsByCondition(LedgerModels model)
		{
			APIResponse response = new APIResponse();
			try
			{
				List<ChartAccountDetail> ledgerList = new List<ChartAccountDetail>();
				ledgerList = await _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).Where(x => x.AccountLevelId == 4 && model.accountLists.Contains(x.AccountCode)).ToListAsync();
				//foreach (var account in model.accountLists)
				//{
				//	ChartAccountDetail obj = new ChartAccountDetail();
				//	obj = await _uow.GetDbContext().ChartAccountDetail.Include(e => e.CreditAccountlist).Include(d => d.DebitAccountlist).Where(x => x.AccountLevelId == 4 && x.AccountCode == account).FirstOrDefaultAsync();
				//	if (obj != null)
				//	{
				//		if (obj.CreditAccountlist.Count > 0 || obj.DebitAccountlist.Count > 0)
				//			ledgerList.Add(obj);
				//	}
				//}

				DateTime defaultdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
				if (model.fromdate == null && model.todate == null)
				{
					model.fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
					model.todate = DateTime.UtcNow;
				}
				List<LedgerModel> finalLedgerList = new List<LedgerModel>();
				foreach (var items in ledgerList)
				{
					foreach (var creditList in items.CreditAccountlist)
					{
						var voucherOfficeId = await _uow.VoucherDetailRepository.FindAsync(x => x.IsDeleted == false && x.VoucherNo == creditList.VoucherNo);
						foreach (var offices in model.OfficesList)
						{
							if (offices == voucherOfficeId.OfficeId)
							{
								if (creditList.TransactionDate.Date >= model.fromdate.Date && creditList.TransactionDate.Date <= model.todate.Date)
								{
									var currencyName = await _uow.CurrencyDetailsRepository.FindAsync(x => x.CurrencyId == creditList.CurrencyId);
									var voucherReferenceNo = await _uow.VoucherDetailRepository.FindAsync(x => x.IsDeleted == false && x.VoucherNo == creditList.VoucherNo);
									if (model.RecordType == 1)
									{
										if (creditList.CurrencyId == model.CurrencyId)
										{
											LedgerModel obj = new LedgerModel();
											obj.TransactionDate = creditList.TransactionDate;
											obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
											obj.Description = creditList.Description;
											obj.CurrencyName = currencyName?.CurrencyName ?? null;
											obj.AccountCode = items.ChartOfAccountCode;
											obj.ChartAccountName = items.AccountName;
											obj.CreditAmount = creditList.Amount;
											obj.DebitAmount = 0;
											finalLedgerList.Add(obj);
											//totalCredit += creditList.Amount;
										}
									}
									else
									{
										if (creditList.CurrencyId == model.CurrencyId)
										{
											LedgerModel obj = new LedgerModel();
											obj.TransactionDate = creditList.TransactionDate;
											obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
											obj.Description = creditList.Description;
											obj.CurrencyName = currencyName?.CurrencyName ?? null;
											obj.AccountCode = items.ChartOfAccountCode;
											obj.ChartAccountName = items.AccountName;
											obj.CreditAmount = creditList.Amount;
											obj.DebitAmount = 0;
											finalLedgerList.Add(obj);

											//totalCredit += creditList.Amount;
										}
										else
										{
											var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == creditList.CurrencyId && x.ToCurrency == model.CurrencyId).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
											LedgerModel obj = new LedgerModel();
											obj.TransactionDate = creditList.TransactionDate;
											obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
											obj.Description = creditList.Description;
											obj.CurrencyName = currencyName?.CurrencyName ?? null;
											obj.AccountCode = items.ChartOfAccountCode;
											obj.ChartAccountName = items.AccountName;
											obj.CreditAmount = creditList.Amount * exchangeRate?.Rate ?? 0;
											obj.DebitAmount = 0;
											finalLedgerList.Add(obj);
											//totalCredit += creditList.Amount * exchangeRate?.Rate ?? 0;
										}
									}
								}
							}
						}
					}

					foreach (var debitList in items.DebitAccountlist)
					{
						var voucherOfficeId = await _uow.VoucherDetailRepository.FindAsync(x => x.IsDeleted == false && x.VoucherNo == debitList.VoucherNo);
						foreach (var offices in model.OfficesList)
						{
							if (offices == voucherOfficeId.OfficeId)
							{
								if (debitList.TransactionDate.Date >= model.fromdate.Date && debitList.TransactionDate.Date <= model.todate.Date)
								{
									var currencyName = await _uow.CurrencyDetailsRepository.FindAsync(x => x.CurrencyId == debitList.CurrencyId);
									var voucherReferenceNo = await _uow.VoucherDetailRepository.FindAsync(x => x.IsDeleted == false && x.VoucherNo == debitList.VoucherNo);
									if (model.RecordType == 1)
									{
										if (debitList.CurrencyId == model.CurrencyId)
										{
											LedgerModel obj = new LedgerModel();
											obj.TransactionDate = debitList.TransactionDate;
											obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
											obj.Description = debitList.Description;
											obj.CurrencyName = currencyName?.CurrencyName ?? null;
											obj.AccountCode = items.ChartOfAccountCode;
											obj.ChartAccountName = items.AccountName;
											obj.CreditAmount = 0;
											obj.DebitAmount = debitList.Amount;
											finalLedgerList.Add(obj);
											//totalDebit += debitList.Amount;
										}
									}
									else
									{
										if (debitList.CurrencyId == model.CurrencyId)
										{
											LedgerModel obj = new LedgerModel();
											obj.TransactionDate = debitList.TransactionDate;
											obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
											obj.Description = debitList.Description;
											obj.CurrencyName = currencyName?.CurrencyName ?? null;
											obj.AccountCode = items.ChartOfAccountCode;
											obj.ChartAccountName = items.AccountName;
											obj.CreditAmount = 0;
											obj.DebitAmount = debitList.Amount;
											finalLedgerList.Add(obj);
											//totalDebit += debitList.Amount;
										}
										else
										{
											var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.FromCurrency == debitList.CurrencyId && x.ToCurrency == model.CurrencyId).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
											LedgerModel obj = new LedgerModel();
											obj.TransactionDate = debitList.TransactionDate;
											obj.VoucherNo = voucherReferenceNo?.ReferenceNo ?? null;
											obj.Description = debitList.Description;
											obj.CurrencyName = currencyName?.CurrencyName ?? null;
											obj.AccountCode = items.ChartOfAccountCode;
											obj.ChartAccountName = items.AccountName;
											obj.CreditAmount = 0;
											obj.DebitAmount = debitList.Amount * exchangeRate?.Rate ?? 0;
											finalLedgerList.Add(obj);
											//totalDebit += debitList.Amount * exchangeRate?.Rate ?? 0;
										}
									}
								}
							}
						}
					}

				}
				response.data.TrailBlanceList = finalLedgerList;
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
					_uow.GetDbContext().NotesMaster.Include(a => a.AccountType).Include(c => c.ChartAccountDetails).Where(x => x.IsDeleted == false).ToListAsync()
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

		public async Task<APIResponse> GetBlanceSheetDetails(FinancialReportModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var cpList = await _uow.GetDbContext().CategoryPopulator.Include(o => o.AccountType).Where(x => x.IsDeleted == false && x.AccountType.AccountCategory == model.financialreporttype).ToListAsync();
				//var allAccounts = await _uow.GetDbContext().ChartAccountDetail.Include(x=>x.CreditAccountlist).Include(x=>x.DebitAccountlist).ToListAsync();
				List<ChartAccountDetail> allAccounts = new List<ChartAccountDetail>();
				// For INCOME/EXPENSE REPORT
				if (model.StartDate != null)
				{
					allAccounts = await _uow.GetDbContext().ChartAccountDetail.Include(x => x.CreditAccountlist).Include(x => x.DebitAccountlist)
						.Select(x => new ChartAccountDetail
						{
							AccountCode = x.AccountCode,
							AccountLevelId = x.AccountLevelId,
							AccountLevels = x.AccountLevels,
							AccountName = x.AccountName,
							AccountType = x.AccountType,
							AccountTypeId = x.AccountTypeId,
							ChartOfAccountCode = x.ChartOfAccountCode,
							CreatedById = x.CreatedById,
							CreatedDate = x.CreatedDate,
							ParentID = x.ParentID,
							CreditAccountDetails = x.CreditAccountDetails,
							CreditAccountlist = x.CreditAccountlist.Where(o => o.TransactionDate.Date >= model.StartDate.Value.Date && o.TransactionDate.Date <= model.EndDate.Value.Date).ToList(),
							DebitAccountlist = x.DebitAccountlist.Where(o => o.TransactionDate.Date >= model.StartDate.Value.Date && o.TransactionDate.Date <= model.EndDate.Value.Date).ToList()
						}).ToListAsync();
				}
				// For BALANCE SHEET REPORT
				else
				{
					var financialYearDetails = await _uow.FinancialYearDetailRepository.FindAsync(x => x.StartDate.Date <= model.EndDate.Value.Date && x.EndDate.Date >= model.EndDate.Value.Date);
					allAccounts = await _uow.GetDbContext().ChartAccountDetail.Include(x => x.CreditAccountlist).Include(x => x.DebitAccountlist)
						.Select(x => new ChartAccountDetail
						{
							AccountCode = x.AccountCode,
							AccountLevelId = x.AccountLevelId,
							AccountLevels = x.AccountLevels,
							AccountName = x.AccountName,
							AccountType = x.AccountType,
							AccountTypeId = x.AccountTypeId,
							ChartOfAccountCode = x.ChartOfAccountCode,
							CreatedById = x.CreatedById,
							CreatedDate = x.CreatedDate,
							ParentID = x.ParentID,
							CreditAccountDetails = x.CreditAccountDetails,
							CreditAccountlist = x.CreditAccountlist.Where(o => o.TransactionDate.Date >= financialYearDetails.StartDate.Date && o.TransactionDate.Date <= model.EndDate.Value.Date).ToList(),
							DebitAccountlist = x.DebitAccountlist.Where(o => o.TransactionDate.Date >= financialYearDetails.StartDate.Date && o.TransactionDate.Date <= model.EndDate.Value.Date).ToList()
						}).ToListAsync();
				}
				List<BalanceSheetModel> lstBalanceSheetModel = new List<BalanceSheetModel>();
				foreach (var items in cpList)
				{

					var accountDetails = allAccounts.Find(x => x.AccountCode == items.ChartOfAccountCode);       // Just used for finding the details of this account

					double? creditAmount = 0, debitAmount = 0, balanceAmount = 0;
					if (accountDetails.AccountLevelId == 4)
					{
						// Gets the transactions for level 4th account
						var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == items.ChartOfAccountCode).ToList();

						#region "For calculations of Balance TYPE"
						if (items.ValueSource == (int)BalanceType.SUM)
						{
							foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
							{

								if (transactionCalcuate.CreditAccountlist.Count > 0)
								{
									if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
									{
										creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
									}
									else
									{
										var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
										creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
									}
								}

								if (transactionCalcuate.DebitAccountlist.Count > 0)
								{
									if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
									{
										debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
									}
									else
									{
										var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
										debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
									}
								}
							}
							balanceAmount = debitAmount - creditAmount;
						}

						else if (items.ValueSource == (int)BalanceType.DR)
						{
							foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
							{

								if (transactionCalcuate.CreditAccountlist.Count > 0)
								{
									if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
									{
										creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
									}
									else
									{
										var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
										creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
									}
								}

							}
							balanceAmount = creditAmount;
						}

						else if (items.ValueSource == (int)BalanceType.CR)
						{
							foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
							{

								if (transactionCalcuate.DebitAccountlist.Count > 0)
								{
									if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
									{
										debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
									}
									else
									{
										var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
										debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
									}
								}
							}
							balanceAmount = debitAmount;
						}

						BalanceSheetModel balanceSheetObj = new BalanceSheetModel();
						balanceSheetObj.Narration = items.SubCategoryLabel;
						balanceSheetObj.Balance = balanceAmount;
						balanceSheetObj.AccountTypeId = items.AccountTypeId;
						lstBalanceSheetModel.Add(balanceSheetObj);

						#endregion
					}
					else if (accountDetails.AccountLevelId == 3)
					{
						// Gets the fourth level accounts
						var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == accountDetails.AccountCode && x.AccountLevelId == 4);

						foreach (var elements in accountsLevelFourth)
						{
							// Gets the transactions for level 4th account
							var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

							#region "For calculations of Balance TYPE"
							if (items.ValueSource == (int)BalanceType.SUM)
							{
								foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
								{

									if (transactionCalcuate.CreditAccountlist.Count > 0)
									{
										if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
										{
											creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
										}
										else
										{
											var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
											creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
										}
									}

									if (transactionCalcuate.DebitAccountlist.Count > 0)
									{
										if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
										{
											debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
										}
										else
										{
											var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
											debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
										}
									}
								}
								balanceAmount = debitAmount - creditAmount;
							}

							else if (items.ValueSource == (int)BalanceType.DR)
							{
								foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
								{

									if (transactionCalcuate.CreditAccountlist.Count > 0)
									{
										if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
										{
											creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
										}
										else
										{
											var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
											creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
										}
									}

								}
								balanceAmount = creditAmount;
							}

							else if (items.ValueSource == (int)BalanceType.CR)
							{
								foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
								{

									if (transactionCalcuate.DebitAccountlist.Count > 0)
									{
										if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
										{
											debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
										}
										else
										{
											var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
											debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
										}
									}
								}
								balanceAmount = debitAmount;
							}


							#endregion
						}

						BalanceSheetModel balanceSheetObj = new BalanceSheetModel();
						balanceSheetObj.Narration = items.SubCategoryLabel;
						balanceSheetObj.Balance = balanceAmount;
						balanceSheetObj.AccountTypeId = items.AccountTypeId;
						lstBalanceSheetModel.Add(balanceSheetObj);
					}
					else if (accountDetails.AccountLevelId == 2)
					{
						// Gets the level 3rd accounts
						var accountsLevelThird = allAccounts.FindAll(x => x.ParentID == accountDetails.AccountCode && x.AccountLevelId == 3);

						foreach (var element in accountsLevelThird)
						{
							// Gets the fourth level accounts
							var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == element.AccountCode && x.AccountLevelId == 4);

							foreach (var elements in accountsLevelFourth)
							{
								// Gets the transactions for level 4th account
								var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

								#region "For calculations of Balance TYPE"
								if (items.ValueSource == (int)BalanceType.SUM)
								{
									foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
									{

										if (transactionCalcuate.CreditAccountlist.Count > 0)
										{
											if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
											{
												creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
											}
											else
											{
												var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
												creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
											}
										}

										if (transactionCalcuate.DebitAccountlist.Count > 0)
										{
											if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
											{
												debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
											}
											else
											{
												var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
												debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
											}
										}
									}
									balanceAmount = debitAmount - creditAmount;
								}

								else if (items.ValueSource == (int)BalanceType.DR)
								{
									foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
									{

										if (transactionCalcuate.CreditAccountlist.Count > 0)
										{
											if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
											{
												creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
											}
											else
											{
												var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
												creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
											}
										}


									}
									balanceAmount = creditAmount;
								}

								else if (items.ValueSource == (int)BalanceType.CR)
								{
									foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
									{


										if (transactionCalcuate.DebitAccountlist.Count > 0)
										{
											if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
											{
												debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
											}
											else
											{
												var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
												debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
											}
										}
									}
									balanceAmount = debitAmount;
								}

								#endregion
							}
						}

						BalanceSheetModel balanceSheetObj = new BalanceSheetModel();
						balanceSheetObj.Narration = items.SubCategoryLabel;
						balanceSheetObj.Balance = balanceAmount;
						balanceSheetObj.AccountTypeId = items.AccountTypeId;
						lstBalanceSheetModel.Add(balanceSheetObj);
					}
					else if (accountDetails.AccountLevelId == 1)
					{
						// Gets the level 2nd accounts
						var accountsLevelSecond = allAccounts.FindAll(x => x.ParentID == accountDetails.AccountCode && x.AccountLevelId == 2);

						foreach (var item in accountsLevelSecond)
						{
							// Gets the level 3rd accounts
							var accountsLevelThird = allAccounts.FindAll(x => x.ParentID == item.AccountCode && x.AccountLevelId == 3);

							foreach (var element in accountsLevelThird)
							{
								// Gets the fourth level accounts
								var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == element.AccountCode && x.AccountLevelId == 4);

								foreach (var elements in accountsLevelFourth)
								{
									// Gets the transactions for level 4th account
									var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

									#region "For calculations of Balance TYPE"
									if (items.ValueSource == (int)BalanceType.SUM)
									{
										foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
										{

											if (transactionCalcuate.CreditAccountlist.Count > 0)
											{
												if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
												{
													creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
												}
												else
												{
													var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
													creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
												}
											}

											if (transactionCalcuate.DebitAccountlist.Count > 0)
											{
												if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
												{
													debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
												}
												else
												{
													var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
													debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
												}
											}
										}
										balanceAmount = debitAmount - creditAmount;
									}

									else if (items.ValueSource == (int)BalanceType.DR)
									{
										foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
										{
											if (transactionCalcuate.CreditAccountlist.Count > 0)
											{
												if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
												{
													creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount);
												}
												else
												{
													var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
													creditAmount += transactionCalcuate.CreditAccountlist?.Sum(x => x.Amount) * exchangeRate.Rate;
												}
											}

										}
										balanceAmount = creditAmount;
									}

									else if (items.ValueSource == (int)BalanceType.CR)
									{
										foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
										{

											if (transactionCalcuate.DebitAccountlist.Count > 0)
											{
												if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == model.currencyid)
												{
													debitAmount += transactionCalcuate.DebitAccountlist?.Sum(x => x.Amount);
												}
												else
												{
													var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == model.currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
													debitAmount += transactionCalcuate.DebitAccountlist.Sum(x => x.Amount) * exchangeRate.Rate;
												}
											}
										}
										balanceAmount = debitAmount;
									}



									#endregion
								}
							}
						}

						BalanceSheetModel balanceSheetObj = new BalanceSheetModel();
						balanceSheetObj.Narration = items.SubCategoryLabel;
						balanceSheetObj.Balance = balanceAmount;
						balanceSheetObj.AccountTypeId = items.AccountTypeId;
						lstBalanceSheetModel.Add(balanceSheetObj);
					}
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
			return response;
		}

		public async Task<APIResponse> GetDetailsOfNotes(int? financialyearid, int? currencyid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var Noteslist = await _uow.GetDbContext().NotesMaster
						.Include(c => c.ChartAccountDetails)
						.Include(c => c.ChartAccountDetails.CreditAccountlist)
						.Include(c => c.ChartAccountDetails.DebitAccountlist)
						.Where(x => x.IsDeleted == false && x.FinancialReportTypeId == 1)
						.GroupBy(g => g.Notes)
						.ToListAsync();

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
						balanceAmount = debitAmount - creditAmount;
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

				response.data.DetailsOfNotesList = detailsofnoteList.OrderBy(x => x.Notes).ToList();
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
							.Include(c => c.ChartAccountDetails)
							.Where(x => x.IsDeleted == false)
							.OrderBy(o => o.Notes)
							//.GroupBy(g => g.Notes)
							.ToListAsync();

					//var allAccounts = await _uow.GetDbContext().ChartAccountDetail.Include(x => x.CreditAccountlist).Include(x => x.DebitAccountlist).ToListAsync();


					var allAccounts = await _uow.GetDbContext().ChartAccountDetail.Include(x => x.CreditAccountlist).Include(x => x.DebitAccountlist)
						.Select(x => new
						{
							AccountCode = x.AccountCode,
							AccountLevelId = x.AccountLevelId,
							AccountLevels = x.AccountLevels,
							AccountName = x.AccountName,
							AccountType = x.AccountType,
							AccountTypeId = x.AccountTypeId,
							ChartOfAccountCode = x.ChartOfAccountCode,
							CreatedById = x.CreatedById,
							CreatedDate = x.CreatedDate,
							ParentID = x.ParentID,
							CreditAccountDetails = x.CreditAccountDetails,
							CreditAccountlist = x.CreditAccountlist.Where(o => o.FinancialYearId == financialyearid).ToList(),
							DebitAccountlist = x.DebitAccountlist.Where(o => o.FinancialYearId == financialyearid).ToList()
						}).ToListAsync();

					List<DetailsOfNotesModel> lst = new List<DetailsOfNotesModel>();
					foreach (var items in allNotes)
					{

						var accountDetails = items.ChartAccountDetails;       // Just used for finding the details of this account

						double? creditAmount = 0, debitAmount = 0, balanceAmount = 0, exchangerate = 0;
						if (accountDetails.AccountLevelId == 4)
						{
							// Gets the transactions for level 4th account
							var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == items.AccountCode).ToList();

							#region "For calculations of Balance TYPE"

							foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
							{
								if (transactionCalcuate.CreditAccountlist.Count > 0)
								{
									if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == currencyid)
									{
										creditAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount);
									}
									else
									{
										var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
										creditAmount += transactionCalcuate.CreditAccountlist?.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount) * exchangeRate.Rate;
									}
								}

								if (transactionCalcuate.DebitAccountlist.Count > 0)
								{
									if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == currencyid)
									{
										debitAmount += transactionCalcuate.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount);
									}
									else
									{
										var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
										debitAmount += transactionCalcuate.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount) * exchangeRate.Rate;
									}
								}

							}
							//balanceAmount = debitAmount - creditAmount;

							DetailsOfNotesModel obj = new DetailsOfNotesModel();
							obj.ChartOfAccountCode = accountDetails.ChartOfAccountCode;
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
							var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == accountDetails.AccountCode && x.AccountLevelId == 4);

							foreach (var elements in accountsLevelFourth)
							{
								creditAmount = 0; debitAmount = 0;
								// Gets the transactions for level 4th account
								var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

								#region "For calculations of Balance TYPE"

								foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
								{
									if (transactionCalcuate.CreditAccountlist.Count > 0)
									{
										if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == currencyid)
										{
											creditAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount);
										}
										else
										{
											var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
											creditAmount += transactionCalcuate.CreditAccountlist?.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount) * exchangeRate.Rate;
										}
									}

									if (transactionCalcuate.DebitAccountlist.Count > 0)
									{
										if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == currencyid)
										{
											debitAmount += transactionCalcuate.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount);
										}
										else
										{
											var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
											debitAmount += transactionCalcuate.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount) * exchangeRate.Rate;
										}
									}

								}
								//balanceAmount = debitAmount - creditAmount;

								DetailsOfNotesModel obj = new DetailsOfNotesModel();
								obj.ChartOfAccountCode = elements.ChartOfAccountCode;
								obj.CreditAmount = creditAmount;
								obj.DebitAmount = debitAmount;
								obj.AccountName = elements.AccountName;
								obj.Notes = items.Notes;
								lst.Add(obj);

							}


							#endregion
						}
						else if (accountDetails.AccountLevelId == 2)
						{
							// Gets the level 3rd accounts
							var accountsLevelThird = allAccounts.FindAll(x => x.ParentID == accountDetails.AccountCode && x.AccountLevelId == 3);

							foreach (var element in accountsLevelThird)
							{
								// Gets the fourth level accounts
								var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == element.AccountCode && x.AccountLevelId == 4);

								foreach (var elements in accountsLevelFourth)
								{
									creditAmount = 0; debitAmount = 0;
									// Gets the transactions for level 4th account
									var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

									#region "For calculations of Balance TYPE"

									foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
									{
										if (transactionCalcuate.CreditAccountlist.Count > 0)
										{
											if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == currencyid)
											{
												creditAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount);
											}
											else
											{
												var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
												creditAmount += transactionCalcuate.CreditAccountlist?.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount) * exchangeRate.Rate;
											}
										}

										if (transactionCalcuate.DebitAccountlist.Count > 0)
										{
											if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == currencyid)
											{
												debitAmount += transactionCalcuate.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount);
											}
											else
											{
												var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
												debitAmount += transactionCalcuate.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount) * exchangeRate.Rate;
											}
										}

									}
									//balanceAmount = debitAmount - creditAmount;									

									#endregion
									DetailsOfNotesModel obj = new DetailsOfNotesModel();
									obj.ChartOfAccountCode = elements.ChartOfAccountCode;
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
							var accountsLevelSecond = allAccounts.FindAll(x => x.ParentID == accountDetails.AccountCode && x.AccountLevelId == 2);

							foreach (var item in accountsLevelSecond)
							{
								// Gets the level 3rd accounts
								var accountsLevelThird = allAccounts.FindAll(x => x.ParentID == item.AccountCode && x.AccountLevelId == 3);

								foreach (var element in accountsLevelThird)
								{
									// Gets the fourth level accounts
									var accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == element.AccountCode && x.AccountLevelId == 4);

									foreach (var elements in accountsLevelFourth)
									{
										creditAmount = 0; debitAmount = 0;
										// Gets the transactions for level 4th account
										// Gets the transactions for level 4th account
										var accountsLevelFourthWithTransactions = allAccounts.Where(x => x.AccountCode == elements.AccountCode).ToList();

										#region "For calculations of Balance TYPE"

										foreach (var transactionCalcuate in accountsLevelFourthWithTransactions)
										{
											if (transactionCalcuate.CreditAccountlist.Count > 0)
											{
												if (transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId == currencyid)
												{
													creditAmount += transactionCalcuate.CreditAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount);
												}
												else
												{
													var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.CreditAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
													creditAmount += transactionCalcuate.CreditAccountlist?.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount) * exchangeRate.Rate;
												}
											}

											if (transactionCalcuate.DebitAccountlist.Count > 0)
											{
												if (transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId == currencyid)
												{
													debitAmount += transactionCalcuate.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount);
												}
												else
												{
													var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == transactionCalcuate.DebitAccountlist.FirstOrDefault().CurrencyId && x.ToCurrency == currencyid).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
													debitAmount += transactionCalcuate.DebitAccountlist.Where(f => f.FinancialYearId == financialyearid).Sum(x => x.Amount) * exchangeRate.Rate;
												}
											}

										}
										//balanceAmount = debitAmount - creditAmount;
										#endregion

										DetailsOfNotesModel obj = new DetailsOfNotesModel();
										obj.ChartOfAccountCode = elements.ChartOfAccountCode;
										obj.CreditAmount = creditAmount;
										obj.DebitAmount = debitAmount;
										obj.AccountName = elements.AccountName;
										obj.Notes = items.Notes;
										lst.Add(obj);
									}
								}
							}
						}
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

								obj.ChartOfAccountCode = item.ChartOfAccountCode;
								obj.AccountName = item.AccountName;
								obj.Notes = item.Notes;
								detailsOfNoteList.Add(obj);
							}

							finalObj.DetailsOfNotesList = detailsOfNoteList.ToList();

							finalObj.CreditSum = detailsOfNoteList.Sum(x => x.CreditAmount);
							finalObj.DebitSum = detailsOfNoteList.Sum(x => x.DebitAmount);
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
					_mapper.Map(model, recordExists);
					await _uow.CategoryPopulatorRepository.UpdateAsyn(recordExists);
					await _uow.SaveAsync();
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


		public async Task<APIResponse> GetExchangeGainOrLossAmount(ExchangeGainOrLossFilterModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var baseCurrency = await _uow.CurrencyDetailsRepository.FindAsync(x=>x.Status == true);
				var exchangeRateList = await _uow.ExchangeRateRepository.GetAllAsyn();

				ExchangeGainOrLossModel responseModel = new ExchangeGainOrLossModel();
				List<TransactionsModel> lst = new List<TransactionsModel>();
				var records = await _uow.GetDbContext().ChartAccountDetail.Include(x => x.DebitAccountlist).Where(x => x.IsDeleted == false && model.AccountCodes.Contains(x.AccountCode)).ToListAsync();
				foreach (var items in records)
				{
					//var DebitTransactionForEachAccount = items.DebitAccountlist.Where(x => x.TransactionDate.Date.Month >= model.StartDate.Date.Month && x.TransactionDate.Date.Year >= model.StartDate.Date.Year && x.TransactionDate.Date.Month <= model.EndDate.Date.Month && x.TransactionDate.Date.Year <= model.EndDate.Date.Year).ToList();
					var DebitTransactionForEachAccount = items.DebitAccountlist.Where(x => x.TransactionDate.Date >= model.StartDate.Date && x.TransactionDate.Date <= model.EndDate.Date).ToList();
					double accountTransactionTotal = 0, accountCurrentTotal = 0;					
					foreach (var elements in DebitTransactionForEachAccount)
					{						
						if (elements.CurrencyId == baseCurrency.CurrencyId)
						{
							accountTransactionTotal += elements.Amount;
							accountCurrentTotal += elements.Amount;
						}
						else
						{
							var exchangeRateOfTransaction = exchangeRateList.Where(x => x.IsDeleted == false && x.FromCurrency == elements.CurrencyId && x.ToCurrency == baseCurrency.CurrencyId && x.Date.Date.Month <= elements.TransactionDate.Date.Month && x.Date.Date.Year <= elements.TransactionDate.Date.Year).OrderByDescending(x=>x.Date).FirstOrDefault();
							var exchangeRateForCurrentDate = exchangeRateList.Where(x => x.IsDeleted == false && x.FromCurrency == elements.CurrencyId && x.ToCurrency == baseCurrency.CurrencyId && x.Date.Date.Month <= DateTime.Now.Date.Month && x.Date.Date.Year == DateTime.Now.Date.Year).OrderByDescending(x => x.Date).FirstOrDefault();

							accountTransactionTotal += elements.Amount * (exchangeRateOfTransaction?.Rate ?? 0);
							accountCurrentTotal += elements.Amount * (exchangeRateForCurrentDate?.Rate ?? 0);
						}
					}

					TransactionsModel obj = new TransactionsModel();
					obj.OriginalAmount = accountTransactionTotal;
					obj.CurrentAmount = accountCurrentTotal;
					obj.Balance = accountCurrentTotal - accountTransactionTotal;
					obj.ChartOfAccountCode = items.AccountCode;
					lst.Add(obj);
				}
				responseModel.TransactionsModel = lst;
				responseModel.Total = lst.Sum(x=>x.Balance);
				response.data.ExchangeGainOrLossModel = responseModel;
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
