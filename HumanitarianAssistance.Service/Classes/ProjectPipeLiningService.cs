using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
	public class ProjectPipeLiningService : IProjectPipeLining
	{
		IUnitOfWork _uow;
		IMapper _mapper;
		UserManager<AppUser> _userManager;
		public ProjectPipeLiningService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
		{
			this._uow = uow;
			this._mapper = mapper;
			this._userManager = userManager;
		}

		public async Task<APIResponse> AddBudgetLine(ProjectBudgetLineModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				ProjectBudgetLine obj = _mapper.Map<ProjectBudgetLine>(model);
				await _uow.ProjectBudgetLineRepository.AddAsyn(obj);
				await _uow.SaveAsync();
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Budget Line Added!";
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> GetProjectLineDetail(long projectId)
		{
			APIResponse response = new APIResponse();
			try
			{
				//string str = string.Format("SELECT *  FROM " + @"""{0}""" + " WHERE " + @"""ProjectId""" + "={2}"+ " And " + @"""IsDeleted""" + " = {1} ", "ProjectBudgetLine", false,projectId);
				//var list = await _uow.GetDbContext().ProjectBudgetLine.FromSql(str).ToListAsync();

				// var list = _uow.ProjectBudgetLineRepository.FindAllAsync(x => x.IsDeleted && x.ProjectId == projectId);

				var list = await _uow.GetDbContext().ProjectBudgetLine.Include(x => x.BudgetLineType).Where(x => x.ProjectId == projectId && x.IsDeleted == false).ToListAsync();
				var list1 = list.Select(x => new ProjectBudgetLineModel
				{

					BudgetLineTypeName = x.BudgetLineType.BudgetLineTypeName,
					BudgetLineTypeId = x.BudgetLineType.BudgetLineTypeId,
					BudgetLineId = x.BudgetLineId,
					StartDate = x.StartDate,
					EndDate = x.EndDate,
					AmountPayable = x.AmountPayable,
					AmountReceivable = x.AmountReceivable,
					Description = x.Description
				}).ToList();


				response.data.ProjectBudgetLineList = list1;
				response.StatusCode = 200;
				response.Message = "Success";
				return response;
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}
		public async Task<APIResponse> EditProjectLineDetail(ProjectBudgetLineModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var exist = await _uow.ProjectBudgetLineRepository.FindAsync(x => x.IsDeleted == false && x.ProjectId == model.ProjectId && x.BudgetLineId == model.BudgetLineId);
				if (exist != null)
				{
					exist.AmountPayable = model.AmountPayable;
					exist.AmountReceivable = model.AmountReceivable;
					exist.Description = model.Description;
					exist.BudgetLineTypeId = model.BudgetLineTypeId;
					exist.StartDate = model.StartDate;
					exist.EndDate = model.EndDate;
					exist.ModifiedById = model.ModifiedById;
					exist.ModifiedDate = DateTime.UtcNow;
					exist.IsDeleted = false;
					await _uow.ProjectBudgetLineRepository.UpdateAsyn(exist);
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Edit Successfully";


				}
				else
				{

					response.StatusCode = StaticResource.successStatusCode;
					response.Message = StaticResource.SomethingWentWrong;

				}

				return response;


			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;

		}

		public async Task<APIResponse> AddBudgetRecivable(BudgetReceivableModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				BudgetReceivable obj = _mapper.Map<BudgetReceivable>(model);

				await _uow.GetDbContext().BudgetReceivable.AddAsync(obj);
				await _uow.SaveAsync();
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Budget Recivable Added!";

				return response;
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> EditBudgetReceivable(BudgetReceivableModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var exist = await _uow.GetDbContext().BudgetReceivable.Where(x => x.IsDeleted == false && x.ProjectId == model.ProjectId && x.BudgetLineId == model.BudgetLineId && x.BudgetReceivalbeId == model.BudgetReceivalbeId).FirstOrDefaultAsync();
				if (exist != null)
				{
					exist.Amount = model.Amount;
					exist.ExpectedDate = model.ExpectedDate;
					exist.BudgetLineId = model.BudgetLineId;
					exist.ProjectId = model.ProjectId;
					exist.ModifiedById = model.ModifiedById;
					exist.ModifiedDate = DateTime.UtcNow;
					exist.IsDeleted = false;
					await _uow.GetDbContext().SaveChangesAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "edit successfully";


				}
				else
				{

					response.StatusCode = StaticResource.successStatusCode;
					response.Message = StaticResource.SomethingWentWrong;

				}

				return response;


			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;

		}

		public async Task<APIResponse> GetBudgetRecivable(long projectId, long budgetlineId)
		{
			APIResponse response = new APIResponse();
			try
			{
				//string str = string.Format("SELECT *  FROM " + @"""{0}""" + " WHERE " + @"""ProjectId""" + "={2}"+ " And " + @"""IsDeleted""" + " = {1} ", "ProjectBudgetLine", false,projectId);
				//var list = await _uow.GetDbContext().ProjectBudgetLine.FromSql(str).ToListAsync();

				// var list = _uow.ProjectBudgetLineRepository.FindAllAsync(x => x.IsDeleted && x.ProjectId == projectId);

				var list = await _uow.GetDbContext().BudgetReceivable.Include(x => x.ProjectBudgetLine).Include(x => x.ProjectDetails).Include(x => x.ProjectBudgetLine.BudgetLineType).Where(x => x.ProjectId == projectId && x.BudgetLineId == budgetlineId).ToListAsync();
				var list1 = list.Select(x => new BudgetReceivableModel
				{
					BudgetReceivalbeId = x.BudgetReceivalbeId,
					ProjectName = x.ProjectDetails.ProjectName,
					BudgetLineTypeId = x.ProjectBudgetLine.BudgetLineType.BudgetLineTypeId,
					BudgetLineTypeName = x.ProjectBudgetLine.BudgetLineType.BudgetLineTypeName,
					Amount = x.Amount,
					ExpectedDate = x.ExpectedDate,
					Description = x.ProjectDetails.Description

				}).ToList();


				response.data.BudgetReceivableList = list1;
				response.StatusCode = 200;
				response.Message = "Success";
				return response;
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}
		public async Task<APIResponse> AddBudgetLineReceived(BudgetReceivedAmountModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				BudgetReceivedAmount obj = _mapper.Map<BudgetReceivedAmount>(model);
				await _uow.GetDbContext().BudgetReceivedAmount.AddAsync(obj);
				await _uow.SaveAsync();
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Success";
				return response;

			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> EditBudgetLineReceived(BudgetReceivedAmountModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var exist = await _uow.GetDbContext().BudgetReceivedAmount.Where(x => x.IsDeleted == false && x.BudgetReceivalbeId == model.BudgetReceivalbeId && x.BudgetReceivedAmountId == model.BudgetReceivedAmountId).FirstOrDefaultAsync();
				if (exist != null)
				{
					exist.Amount = model.Amount;
					exist.Remark = model.Remark;
					exist.ReceivedDate = model.ReceivedDate;
					exist.ModifiedById = model.ModifiedById;
					exist.ModifiedDate = DateTime.UtcNow;
					exist.IsDeleted = false;
					await _uow.GetDbContext().SaveChangesAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "edit successfully";


				}
				else
				{

					response.StatusCode = StaticResource.successStatusCode;
					response.Message = StaticResource.SomethingWentWrong;

				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = ex.Message;
			}
			return response;

		}
		public async Task<APIResponse> GetBudgetReceived(long projectId, long budgetLineId, long recivableId)
		{

			APIResponse response = new APIResponse();
			try
			{
				var list = await _uow.GetDbContext().BudgetReceivedAmount.Include(x => x.BudgetReceivable).Include(x => x.BudgetReceivable.ProjectBudgetLine.BudgetLineType).Include(x => x.BudgetReceivable.ProjectDetails).Where(x => x.BudgetReceivable.ProjectDetails.ProjectId == projectId && x.BudgetReceivable.ProjectBudgetLine.BudgetLineId == budgetLineId && x.BudgetReceivalbeId == recivableId).ToListAsync();
				var list1 = list.Select(x => new BudgetReceivedAmountModel
				{
					BudgetReceivedAmountId = x.BudgetReceivedAmountId,
					ProjectName = x.BudgetReceivable.ProjectDetails.ProjectName,
					BudgetLineName = x.BudgetReceivable.ProjectBudgetLine.BudgetLineType.BudgetLineTypeName,
					BudgetReceivalbeId = x.BudgetReceivalbeId,
					Amount = x.Amount,
					ReceivedDate = x.ReceivedDate,
					Remark = x.Remark

				}).ToList();

				response.data.BudgetReceivedAmountList = list1;
				response.StatusCode = StaticResource.successStatusCode;
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				response.StatusCode = StaticResource.failStatusCode;
			}
			return response;
		}

		public async Task<APIResponse> GetAllProjectBudgetLineByProjectId(long ProjectId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var list = await Task.Run(() =>
					_uow.ProjectBudgetLineRepository.FindAllAsync(x => x.IsDeleted == false && x.ProjectId == ProjectId).Result.ToList()
				);

				var projectbudgetline = list.Select(x => new ProjectBudgetLineModel
				{
					BudgetLineId = x.BudgetLineId,
					Description = x.Description,
					ProjectId = x.ProjectId,
					AmountReceivable = x.AmountReceivable,
					AmountPayable = x.AmountPayable,
					BudgetLineTypeId = x.BudgetLineTypeId
				}).ToList();
				response.data.ProjectBudgetLineList = projectbudgetline;
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

		public async Task<APIResponse> AddBudgetPayable(BudgetPayableModel model)
		{
			APIResponse response = new APIResponse();
			try
			{

				BudgetPayable obj = _mapper.Map<BudgetPayable>(model);
				await _uow.GetDbContext().AddAsync(obj);
				await _uow.SaveAsync();
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Success";
				return response;


			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> GetBudgetPayable(long projectId, long budgetlineId)
		{
			APIResponse response = new APIResponse();
			var list = await _uow.GetDbContext().BudgetPayable.Include(x => x.ProjectBudgetLine).Include(x => x.ProjectDetails).Include(x => x.ProjectBudgetLine.BudgetLineType).Where(x => x.ProjectId == projectId && x.BudgetLineId == budgetlineId).ToListAsync();
			var list1 = list.Select(x => new BudgetPayableModel
			{
				BudgetPayableId = x.BudgetPayableId,
				ProjectName = x.ProjectDetails.ProjectName,
				// BudgetLineTypeId = x.ProjectBudgetLine.BudgetLineType.BudgetLineTypeId,
				BudgetLineTypeName = x.ProjectBudgetLine.BudgetLineType.BudgetLineTypeName,
				Amount = x.Amount,
				ExpectedDate = x.ExpectedDate

			}).ToList();


			response.data.BudgetPayableList = list1;
			response.StatusCode = 200;
			response.Message = "Success";
			return response;

		}

		public async Task<APIResponse> EditBudgetPayable(BudgetPayableModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var exist = await _uow.GetDbContext().BudgetPayable.Where(x => x.IsDeleted == false && x.ProjectId == model.ProjectId && x.BudgetLineId == model.BudgetLineId && x.BudgetPayableId == model.BudgetPayableId).FirstOrDefaultAsync();
				if (exist != null)
				{
					exist.Amount = model.Amount;
					exist.ExpectedDate = model.ExpectedDate;
					exist.BudgetLineId = model.BudgetLineId;
					exist.ProjectId = model.ProjectId;
					exist.ModifiedById = model.ModifiedById;
					exist.ModifiedDate = DateTime.UtcNow;
					exist.IsDeleted = false;
					await _uow.GetDbContext().SaveChangesAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "edit successfully";


				}
				else
				{

					response.StatusCode = StaticResource.failStatusCode;
					response.Message = StaticResource.SomethingWentWrong;

				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}

			return response;

		}

		public async Task<APIResponse> AddBudgetLinePaid(BudgetPayableAmountModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				BudgetPayableAmount obj = _mapper.Map<BudgetPayableAmount>(model);
				await _uow.GetDbContext().BudgetPayableAmount.AddAsync(obj);
				await _uow.SaveAsync();
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Added Successfully";
				return response;
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;

		}

		public async Task<APIResponse> GetBudgetPaidAmount(long projectId, long budgetLineId, long payableId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var list = await _uow.GetDbContext().BudgetPayableAmount.Include(x => x.BudgetPayable).Include(x => x.BudgetPayable.ProjectBudgetLine.BudgetLineType).Include(x => x.BudgetPayable.ProjectDetails).Where(x => x.BudgetPayable.ProjectDetails.ProjectId == projectId && x.BudgetPayable.ProjectBudgetLine.BudgetLineId == budgetLineId && x.BudgetPayableId == payableId).ToListAsync();
				var list1 = list.Select(x => new BudgetPayableAmountModel
				{
					BudgetPayableAmountId = x.BudgetPayableAmountId,
					ProjectName = x.BudgetPayable.ProjectDetails.ProjectName,
					BudgetLineName = x.BudgetPayable.ProjectBudgetLine.BudgetLineType.BudgetLineTypeName,
					BudgetPayableId = x.BudgetPayableId,
					Amount = x.Amount,
					PaymentDate = x.PaymentDate,
					Remark = x.Remark

				}).ToList();

				response.data.BudgetPaidAmountList = list1;
				response.StatusCode = StaticResource.successStatusCode;
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				response.StatusCode = StaticResource.failStatusCode;
			}
			return response;
		}

		public async Task<APIResponse> EditBudgetLinePaid(BudgetPayableAmountModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var exist = await _uow.GetDbContext().BudgetPayableAmount.Where(x => x.IsDeleted == false && x.BudgetPayableId == model.BudgetPayableId && x.BudgetPayableAmountId == model.BudgetPayableAmountId).FirstOrDefaultAsync();
				if (exist != null)
				{
					exist.Amount = model.Amount;
					exist.Remark = model.Remark;
					exist.PaymentDate = model.PaymentDate;
					exist.ModifiedById = model.ModifiedById;
					exist.ModifiedDate = DateTime.UtcNow;
					exist.IsDeleted = false;
					await _uow.GetDbContext().SaveChangesAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "edit successfully";

				}
				else
				{

					response.StatusCode = StaticResource.successStatusCode;
					response.Message = StaticResource.SomethingWentWrong;

				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = ex.Message;
			}
			return response;

		}

		public async Task<APIResponse> GetBudgetSummary(long projectId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var list = await _uow.GetDbContext().BudgetPayableAmount.Include(x => x.BudgetPayable)
					  .Include(x => x.BudgetPayable.ProjectBudgetLine)
					  .Include(x => x.BudgetPayable.ProjectBudgetLine.ProjectDetails)
					  .Include(x => x.BudgetPayable.ProjectBudgetLine.BudgetLineType)
					  .Where(x => x.BudgetPayable.ProjectId == projectId && x.IsDeleted == false).ToListAsync();

				double totalExpenditrue = list.Sum(x => x.Amount);
				var paidAmountList = list.Select(x => new BudgetPayableAmountModel
				{
					Remark = x.Remark,
					Amount = x.Amount,
					BudgetLineName = x.BudgetPayable.ProjectBudgetLine.BudgetLineType.BudgetLineTypeName,
					PaymentDate = x.PaymentDate

				}).ToList();

				response.data.TotalExpenditure = totalExpenditrue;
				response.data.BudgetPaidAmountList = paidAmountList;

				var list2 = await _uow.GetDbContext().BudgetReceivedAmount.Include(x => x.BudgetReceivable)
					.Include(x => x.BudgetReceivable.ProjectBudgetLine)
					  .Include(x => x.BudgetReceivable.ProjectBudgetLine.ProjectDetails)
					  .Include(x => x.BudgetReceivable.ProjectBudgetLine.BudgetLineType)
					  .Where(x => x.BudgetReceivable.ProjectId == projectId && x.IsDeleted == false).ToListAsync();

				double totalIncome = list2.Sum(x => x.Amount);

				var receivedAmountList = list2.Select(x => new BudgetReceivedAmountModel
				{
					Remark = x.Remark,
					Amount = x.Amount,
					BudgetLineName = x.BudgetReceivable.ProjectBudgetLine.BudgetLineType.BudgetLineTypeName,
					ReceivedDate = x.ReceivedDate
				}).ToList();


				response.data.TotalIncome = totalIncome;
				response.data.BudgetReceivedAmountList = receivedAmountList;

				var list3 = await _uow.GetDbContext().BudgetReceivable
				  .Include(x => x.ProjectBudgetLine)
					.Include(x => x.ProjectBudgetLine.ProjectDetails)
					.Include(x => x.ProjectBudgetLine.BudgetLineType)
					.Where(x => x.ProjectId == projectId && x.IsDeleted == false).ToListAsync();
				var totalReceivable = list3.Sum(x => x.Amount);

				var receivableList = list3.Select(x => new BudgetReceivableModel
				{
					BudgetReceivalbeId = x.BudgetReceivalbeId,
					BudgetLineTypeName = x.ProjectBudgetLine.BudgetLineType.BudgetLineTypeName,
					Amount = x.Amount
				}).ToList();

				response.data.TotalRecivable = totalReceivable;
				response.data.BudgetReceivableList = receivableList;


				var list4 = await _uow.GetDbContext().BudgetPayable
				  .Include(x => x.ProjectBudgetLine)
					.Include(x => x.ProjectBudgetLine.ProjectDetails)
					.Include(x => x.ProjectBudgetLine.BudgetLineType)
					.Where(x => x.ProjectId == projectId && x.IsDeleted == false).ToListAsync();
				var totalPayable = list4.Sum(x => x.Amount);

				var payableList = list4.Select(x => new BudgetPayableModel
				{
					BudgetPayableId = x.BudgetPayableId,
					BudgetLineTypeName = x.ProjectBudgetLine.BudgetLineType.BudgetLineTypeName,
					Amount = x.Amount
				}).ToList();

				response.data.TotalPayable = totalPayable;
				response.data.BudgetPayableList = payableList;
				response.data.Balance = totalIncome - totalExpenditrue;
				response.StatusCode = 200;
				response.Message = "Success";
				return response;

			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> AddProjectDocument(ProjectDocumentModel model)
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

				ProjectDocument obj = new ProjectDocument();
				obj.DocumentGUID = guidname;
				obj.Extension = "." + ex;
				obj.FilePath = filepathBase64;
				obj.DocumentName = model.DocumentName;
				obj.DocumentDate = model.DocumentDate;
				obj.ProjectId = model.ProjectId;
				obj.CreatedById = model.CreatedById;
				obj.CreatedDate = DateTime.UtcNow;
				obj.IsDeleted = false;
				await _uow.ProjectDocumentRepository.AddAsyn(obj);
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

		public async Task<APIResponse> DeleteProjectDocument(int projectdocumentid, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.ProjectDocumentRepository.FindAsync(x => x.IsDeleted == false && x.ProjectDocumentId == projectdocumentid);
				if (existrecord != null)
				{
					existrecord.IsDeleted = true;
					existrecord.ModifiedById = userid;
					existrecord.ModifiedDate = DateTime.UtcNow;
					await _uow.ProjectDocumentRepository.UpdateAsyn(existrecord);
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

		public async Task<APIResponse> GetProjectDocumentDetail(int projectid)
		{
			APIResponse response = new APIResponse();
			try
			{
                //var list = await Task.Run(() =>
                //	_uow.ProjectDocumentRepository.FindAllAsync(x => x.IsDeleted == false && x.ProjectId == projectid).Result.ToList()
                //);

                var queryResult = EF.CompileAsyncQuery(
                  (ApplicationDbContext ctx) => ctx.ProjectDocument.Where(x => x.ProjectId == projectid));
                var list = await Task.Run(() =>
                    queryResult(_uow.GetDbContext()).ToListAsync().Result
                );
               
                var projectdocumentlist = list.Select(x => new ProjectDocumentModel
				{
					ProjectDocumentId = x.ProjectDocumentId,
					DocumentName = x.DocumentName,
					DocumentDate = x.DocumentDate,
					ProjectId = x.ProjectId,
					FilePath = Encoding.UTF8.GetString(x.FilePath),
					DocumentGUID = x.DocumentGUID + x.Extension,
					Extension = x.Extension
				}).ToList();
				response.data.ProjectDocumentList = projectdocumentlist;
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

		public async Task<APIResponse> AssignEmployeeToBudgetLine(List<BudgetLineEmployeesModel> model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				List<BudgetLineEmployees> lstemp = new List<BudgetLineEmployees>();
				var emplst = await _uow.BudgetLineEmployeesRepository.FindAllAsync(x => x.OfficeId == model[0].OfficeId && x.ProjectId == model[0].ProjectId && x.BudgetLineId == model[0].BudgetLineId && x.IsActive == true);
				//_uow.GetDbContext().BudgetLineEmployees.RemoveRange(emplst);

				foreach (var element in emplst)
				{
					var lst = model.Find(x => x.EmployeeId == element.EmployeeId && x.OfficeId == element.OfficeId && x.ProjectId == element.ProjectId && x.BudgetLineId == element.BudgetLineId && x.IsActive == true);
					if (lst == null)
					{
						var record = await _uow.BudgetLineEmployeesRepository.FindAsync(x=>x.EmployeeId == element.EmployeeId && x.OfficeId == element.OfficeId && x.ProjectId == element.ProjectId && x.BudgetLineId == element.BudgetLineId && x.IsActive == true);
						record.IsActive = false;
						record.ModifiedDate = DateTime.Now;
						await _uow.BudgetLineEmployeesRepository.UpdateAsyn(record);
					}					
				}

				foreach (var item in model)
				{
					var lst = await _uow.BudgetLineEmployeesRepository.FindAsync(x => x.EmployeeId == item.EmployeeId && x.OfficeId == item.OfficeId && x.ProjectId == item.ProjectId && x.BudgetLineId == item.BudgetLineId);					
					if (lst == null)
					{
						BudgetLineEmployees obj = new BudgetLineEmployees();
						obj.OfficeId = item.OfficeId;
						obj.ProjectId = item.ProjectId;
						obj.BudgetLineId = item.BudgetLineId;
						obj.EmployeeId = item.EmployeeId;
						obj.EmployeeName = item.EmployeeName;
						obj.IsActive = true;
						obj.StartDate = DateTime.Now;
						obj.EndDate = null;
						obj.CreatedById = UserId;
						obj.CreatedDate = DateTime.Now;						
						lstemp.Add(obj);
						//await _uow.GetDbContext().AddAsync(obj);
						//await _uow.SaveAsync();
					}
					else if (lst != null && lst.IsActive == false)
					{
						lst.IsActive = true;
						await _uow.BudgetLineEmployeesRepository.UpdateAsyn(lst);
					}
				}
				await _uow.GetDbContext().AddRangeAsync(lstemp);
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

		public async Task<APIResponse> GetAssignedEmployeesInBudgetLine(int OfficeId, int ProjectId, int BudgetLineId)
		{
			APIResponse response = new APIResponse();
			try
			{
				response.data.GetAllEmployeesInBudgetLine = await _uow.GetDbContext().BudgetLineEmployees.Where(x => x.OfficeId == OfficeId && x.ProjectId == ProjectId && x.BudgetLineId == BudgetLineId && x.IsActive == true).ToListAsync();
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
