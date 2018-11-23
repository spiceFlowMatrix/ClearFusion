//using AutoMapper;
//using DataAccess;
//using DataAccess.DbEntities;
//using HumanitarianAssistance.Common.Helpers;
//using HumanitarianAssistance.Service.APIResponses;
//using HumanitarianAssistance.Service.interfaces;
//using HumanitarianAssistance.ViewModels.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HumanitarianAssistance.Service.Classes
//{
//	public class ProjectBudgetService : IProjectBudget
//	{
//		IUnitOfWork _uow;
//		IMapper _mapper;
//		UserManager<AppUser> _userManager;

//		public ProjectBudgetService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
//		{
//			this._uow = uow;
//			this._mapper = mapper;
//			this._userManager = userManager;
//		}


//		public async Task<APIResponse> AddProjectBudget(ProjectBudgetModel model)
//		{
//			APIResponse response = new APIResponse();

//			try
//			{
//				ProjectBudget obj = _mapper.Map<ProjectBudget>(model);
//				obj.CreatedById = model.CreatedById;
//				obj.CreatedDate = DateTime.UtcNow;
//				obj.IsDeleted = false;
//				await _uow.ProjectBudgetRepository.AddAsyn(obj);
//				response.StatusCode = StaticResource.successStatusCode;
//				response.Message = "Success";
//			}
//			catch (Exception ex)
//			{
//				response.StatusCode = StaticResource.failStatusCode;
//				response.Message = StaticResource.SomethingWrong + ex.Message;

//			}
//			return response;
//		}

//		public async Task<APIResponse> EditProjectBudget(ProjectBudgetModel model)
//		{
//			APIResponse response = new APIResponse();
//			try
//			{
//				var obj = await _uow.ProjectBudgetRepository.FindAsync(x => x.BudgetId == model.BudgetId);
//				if (obj != null)
//				{
//					obj.ReceivableAmount = model.ReceivableAmount;
//					obj.PayableAmount = model.PayableAmount;
//					obj.StartDate = model.StartDate;
//					obj.EndDate = model.EndDate;
//					obj.ModifiedById = model.ModifiedById;
//					obj.ModifiedDate = model.ModifiedDate;
//					await _uow.ProjectBudgetRepository.UpdateAsyn(obj);
//					response.StatusCode = StaticResource.successStatusCode;
//					response.Message = "Success";
//				}

//			}
//			catch (Exception ex)
//			{
//				response.StatusCode = StaticResource.failStatusCode;
//				response.Message = StaticResource.SomethingWrong + ex.Message;
//			}
//			return response;
//		}

//		public async Task<APIResponse> GetProjectBudget()
//		{
//			APIResponse response = new APIResponse();
//			try
//			{

//				string str = string.Format("SELECT *  FROM"+ @"""{0}"""+" WHERE "+@"""IsDeleted"""+" = {1} ","ProjectBudget", false);
//				var list = await _uow.GetDbContext().ProjectBudget.FromSql(str).ToListAsync();
//				response.data.ProjectBudgetList = list;
//				response.StatusCode = StaticResource.successStatusCode;
//				response.Message = "Success";
//			}
//			catch (Exception ex)
//			{
//				response.StatusCode = StaticResource.failStatusCode;
//				response.Message = StaticResource.SomethingWrong + ex.Message;

//			}
//			return response;
//		}        
//	}
//}
