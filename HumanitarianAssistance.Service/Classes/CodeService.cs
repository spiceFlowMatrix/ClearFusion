using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
	public class CodeService : ICode
	{
		IUnitOfWork _uow;
		IMapper _mapper;
		UserManager<AppUser> _userManager;
		public CodeService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
		{
			this._uow = uow;
			this._mapper = mapper;
			this._userManager = userManager;
		}

		public async Task<APIResponse> GetAllCountry()
		{
			APIResponse response = new APIResponse();
			try
			{
				var countrylist = (from c in await _uow.CountryDetailsRepository.GetAllAsyn()
								   where c.IsDeleted == false
								   select new CountryDetailsModel
								   {
									   CountryId = c.CountryId,
									   CountryName = c.CountryName
								   }).ToList();
				response.data.CountryDetailsList = countrylist;
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

		public async Task<APIResponse> GetAllProvinceDetails(int CountryId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var provincelist = (from p in await _uow.ProvinceDetailsRepository.GetAllAsyn()
									where p.CountryId == CountryId && p.IsDeleted == false
									select new ProvinceDetailsModel
									{
										ProvinceId = p.ProvinceId,
										ProvinceName = p.ProvinceName
									}).ToList();
				response.data.ProvinceDetailsList = provincelist;
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

		public async Task<APIResponse> GetAllNationality()
		{
			APIResponse response = new APIResponse();
			try
			{
				var nationalitylist = (from n in await _uow.NationalityDetailsRepository.GetAllAsyn()
									   where n.IsDeleted == false
									   select new NationalityDetailsModel
									   {
										   NationalityId = n.NationalityId,
										   NationalityName = n.NationalityName
									   }).ToList();
				response.data.NationalityDetailsList = nationalitylist;
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

		public async Task<APIResponse> GetAllQualification()
		{
			APIResponse response = new APIResponse();
			try
			{
				var qualificationlist = (from q in await _uow.QualificationDetailsRepository.GetAllAsyn()
										 where q.IsDeleted == false
										 select new QualificationDetailsModel
										 {
											 QualificationId = q.QualificationId,
											 QualificationName = q.QualificationName
										 }).ToList();
				response.data.QualificationDetailsList = qualificationlist;
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

		public async Task<APIResponse> AddQualificationDetails(QualificationDetailsModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.QualificationDetailsRepository.FindAsync(x => x.QualificationName == model.QualificationName);
				if (existrecord == null)
				{
					QualificationDetails obj = _mapper.Map<QualificationDetails>(model);
					await _uow.QualificationDetailsRepository.AddAsyn(obj);
					await _uow.SaveAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
					response.Message = "Qualification Name already exist.";
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> EditQualifactionDetails(QualificationDetailsModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.QualificationDetailsRepository.FindAsync(x => x.QualificationId == model.QualificationId);
				if (existrecord != null)
				{
					var exist = await _uow.QualificationDetailsRepository.FindAsync(x => x.QualificationName == model.QualificationName);
					if (exist == null)
					{
						existrecord.QualificationName = model.QualificationName;
						existrecord.ModifiedById = model.ModifiedById;
						existrecord.ModifiedDate = model.ModifiedDate;
						existrecord.IsDeleted = false;
						await _uow.QualificationDetailsRepository.UpdateAsyn(existrecord);
						response.StatusCode = StaticResource.successStatusCode;
						response.Message = "Success";
					}
					else
					{
						response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
						response.Message = "Qualification Name already exist.";
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> GetAllInterviewRoundList()
		{
			APIResponse response = new APIResponse();
			try
			{
				var roundlist = (from r in await _uow.InterviewRoundTypeMasterRepository.GetAllAsyn()
								 select new InterViewRoundModel
								 {
									 RoundId = r.RoundId,
									 RoundTypeName = r.RoundTypeName
								 }).ToList();
				response.data.InterviewRoundList = roundlist;
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

		public async Task<APIResponse> AddLeaveReasonDetail(LeaveReasonDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				if (model != null)
				{
					LeaveReasonDetail obj = _mapper.Map<LeaveReasonDetail>(model);
					await _uow.LeaveReasonDetailRepository.AddAsyn(obj);
					await _uow.SaveAsync();
				}
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

		public async Task<APIResponse> EditLeaveReasonDetail(LeaveReasonDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var leavereasoninfo = await _uow.LeaveReasonDetailRepository.FindAsync(x => x.LeaveReasonId == model.LeaveReasonId);
				if (leavereasoninfo != null)
				{
					leavereasoninfo.ReasonName = model.ReasonName;
					leavereasoninfo.Unit = model.Unit;
					leavereasoninfo.ModifiedById = model.ModifiedById;
					leavereasoninfo.ModifiedDate = model.ModifiedDate;
					leavereasoninfo.IsDeleted = model.IsDeleted;
					await _uow.LeaveReasonDetailRepository.UpdateAsyn(leavereasoninfo);
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

		public async Task<APIResponse> GetAllLeaveReasonList()
		{
			APIResponse response = new APIResponse();
			try
			{
				var leavelist = await Task.Run(() =>
					_uow.LeaveReasonDetailRepository.FindAllAsync(x => x.IsDeleted == false).Result.ToList()
				);

				var leavereasonlist = leavelist.Select(x => new LeaveReasonDetailModel
				{
					LeaveReasonId = x.LeaveReasonId,
					ReasonName = x.ReasonName,
					Unit = x.Unit
				}).ToList();
				response.data.LeaveReasonList = leavereasonlist;
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

		public async Task<APIResponse> AddFinancialYearDetail(FinancialYearDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var yearlist = await _uow.FinancialYearDetailRepository.GetAllAsyn();
				foreach (var i in yearlist)
				{
					var existrecord1 = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDeleted == false && x.FinancialYearId == i.FinancialYearId);
					existrecord1.IsDefault = false;
					await _uow.FinancialYearDetailRepository.UpdateAsyn(existrecord1);
				}
				FinancialYearDetail obj = _mapper.Map<FinancialYearDetail>(model);
				if (yearlist == null)
					obj.IsDefault = true;
				await _uow.FinancialYearDetailRepository.AddAsyn(obj);
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

		public async Task<APIResponse> EditFinancialYearDetail(FinancialYearDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				int? recordexist = _uow.HolidayDetailsRepository.FindAllAsync(x => x.FinancialYearId == model.FinancialYearId).Result.FirstOrDefault().FinancialYearId;
				if (recordexist > 0)
				{

				}

				var existrecord = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDeleted == false && x.FinancialYearId == model.FinancialYearId);
				if (existrecord != null)
				{
					if (model.IsDefault == true)
					{
						var yearlist = await _uow.FinancialYearDetailRepository.GetAllAsyn();
						foreach (var i in yearlist)
						{
							var existrecord1 = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDeleted == false && x.FinancialYearId == i.FinancialYearId);
							existrecord1.IsDefault = false;
							await _uow.FinancialYearDetailRepository.UpdateAsyn(existrecord1);
						}
					}

					existrecord.FinancialYearName = model.FinancialYearName;
					existrecord.StartDate = model.StartDate;
					existrecord.EndDate = model.EndDate;
					existrecord.Description = model.Description;
					existrecord.IsDefault = model.IsDefault;
					existrecord.ModifiedById = model.ModifiedById;
					existrecord.ModifiedDate = model.ModifiedDate;
					existrecord.IsDeleted = model.IsDeleted;
					await _uow.FinancialYearDetailRepository.UpdateAsyn(existrecord);
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

		public async Task<APIResponse> GetAllFinancialYearDetail()
		{
			APIResponse response = new APIResponse();
			try
			{
				var yearlist = await Task.Run(() =>
					_uow.FinancialYearDetailRepository.FindAllAsync(x => x.IsDeleted == false).Result.OrderBy(x => x.FinancialYearId).ToList()
				);

				var financialyearlist = yearlist.Select(x => new FinancialYearDetailModel
				{
					FinancialYearId = x.FinancialYearId,
					FinancialYearName = x.FinancialYearName,
					StartDate = x.StartDate.Date.ToLocalTime(),
					EndDate = x.EndDate.Date.ToLocalTime(),
					Description = x.Description,
					IsDefault = x.IsDefault
				}).ToList();
				response.data.FinancialYearDetailList = financialyearlist;
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

		public async Task<APIResponse> GetCurrentFinancialYear()
		{
			APIResponse response = new APIResponse();
			try
			{
				var yearlist = await Task.Run(() =>
					_uow.FinancialYearDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.IsDefault == true).Result.ToList()
				);

				var currentfinanciallist = yearlist.Select(x => new CurrentFinancialYear
				{
					FinancialYearId = x.FinancialYearId,
					FinancialYearName = x.FinancialYearName,
					StartDate = x.StartDate,
					EndDate = x.EndDate
				}).ToList();
				response.data.CurrentFinancialYearList = currentfinanciallist;
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

		public async Task<APIResponse> AddBudgetLineType(BudgetLineTypeModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				BudgetLineType obj = _mapper.Map<BudgetLineType>(model);
				await _uow.GetDbContext().BudgetLineType.AddAsync(obj);
				await _uow.GetDbContext().SaveChangesAsync();
				response.Message = "Add BudgetType Successfully!";
				response.StatusCode = StaticResource.successStatusCode;
				return response;
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				response.StatusCode = StaticResource.failStatusCode;
			}
			return response;
		}

		public async Task<APIResponse> GetBudgetLineTypes()
		{
			APIResponse response = new APIResponse();
			try
			{
				var list = await Task.Run(() => _uow.GetDbContext().BudgetLineType.Where(x => x.IsDeleted == false));

				var budgetlinetypelist = list.Select(x => new BudgetLineTypeModel
				{
					BudgetLineTypeId = x.BudgetLineTypeId,
					BudgetLineTypeName = x.BudgetLineTypeName
				}).ToList();
				response.data.BudgetLineTypeList = budgetlinetypelist;
				response.Message = "Success";
				response.StatusCode = StaticResource.successStatusCode;
				return response;
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				response.StatusCode = StaticResource.failStatusCode;
			}
			return response;
		}

		public async Task<APIResponse> GetAllEmployeeType()
		{
			APIResponse response = new APIResponse();
			try
			{
				var list = await Task.Run(() =>
					_uow.EmployeeTypeRepository.FindAllAsync(x => x.IsDeleted == false).Result.ToList()
				);
				var employeetypelist = list.Select(x => new EmployeeTypeModel
				{
					EmployeeTypeId = x.EmployeeTypeId,
					EmployeeTypeName = x.EmployeeTypeName
				}).ToList();
				response.data.EmployeeTypeList = employeetypelist;
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

		public async Task<APIResponse> GetDepartmentsByOfficeId(int OfficeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var list = await _uow.DepartmentRepository.FindAllAsync(x => x.OfficeId == OfficeId && x.IsDeleted == false);
				var departmentlist = list.Where(x => x.IsDeleted == false).
					Select(x => new DepartmentModel
					{
						OfficeCode = x.OfficeCode,
						DepartmentId = x.DepartmentId,
						DepartmentName = x.DepartmentName,
						OfficeId = x.OfficeId
					}).ToList();
				response.data.Departments = departmentlist;
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Success";
			}
			catch (Exception)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong;
			}
			return response;
		}

		public async Task<APIResponse> AddDepartmentDetails(DepartmentModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.DepartmentRepository.FindAsync(x => x.IsDeleted == false && x.DepartmentName == model.DepartmentName && x.OfficeId == model.OfficeId);
				if (existrecord == null)
				{
					Department obj = _mapper.Map<Department>(model);
					await _uow.DepartmentRepository.AddAsyn(obj);
					await _uow.SaveAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
					response.Message = "Department Name already exist for this Office.";
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> EditDepartmentDetails(DepartmentModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.DepartmentRepository.FindAsync(x => x.IsDeleted == false && x.DepartmentId == model.DepartmentId);
				if (existrecord != null)
				{
					var checkdepartment = await _uow.DepartmentRepository.FindAsync(x => x.IsDeleted == false && x.DepartmentName == model.DepartmentName && x.OfficeId == model.OfficeId);
					if (checkdepartment == null)
					{
						existrecord.DepartmentName = model.DepartmentName;
						existrecord.OfficeId = model.OfficeId;
						existrecord.ModifiedById = model.ModifiedById;
						existrecord.ModifiedDate = model.ModifiedDate;
						existrecord.IsDeleted = model.IsDeleted;
						await _uow.DepartmentRepository.UpdateAsyn(existrecord);
						response.StatusCode = StaticResource.successStatusCode;
						response.Message = "Success";
					}
					else
					{
						response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
						response.Message = "Department Name already exist for this Office.";
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = "Success";
			}
			return response;
		}

		public async Task<APIResponse> GetAllDepartment()
		{
			APIResponse response = new APIResponse();
			try
			{
				var list = await Task.Run(() =>
					_uow.GetDbContext().Department.Include(o => o.OfficeDetails).Where(x => x.IsDeleted == false).ToListAsync()
				);
				var departmentlist = list.Select(x => new DepartmentModel
				{
					DepartmentId = x.DepartmentId,
					DepartmentName = x.DepartmentName,
					OfficeId = x.OfficeId,
					OfficeName = x.OfficeDetails?.OfficeName ?? null
				}).OrderBy(x => x.DepartmentId).ToList();

				response.data.Departments = departmentlist;
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

		public async Task<APIResponse> AddSalaryHead(SalaryHeadModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				SalaryHeadDetails obj = _mapper.Map<SalaryHeadDetails>(model);
				await _uow.SalaryHeadDetailsRepository.AddAsyn(obj);
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

		public async Task<APIResponse> EditSalaryHead(SalaryHeadModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.SalaryHeadDetailsRepository.FindAsync(x => x.SalaryHeadId == model.SalaryHeadId);
				if (existrecord != null)
				{
					existrecord.HeadName = model.HeadName;
					existrecord.Description = model.Description;
					existrecord.HeadTypeId = model.HeadTypeId;
					existrecord.ModifiedById = model.ModifiedById;
					existrecord.ModifiedDate = model.ModifiedDate;
					existrecord.IsDeleted = model.IsDeleted;
					await _uow.SalaryHeadDetailsRepository.UpdateAsyn(existrecord);
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

		public async Task<APIResponse> GetAllSalaryHead()
		{
			APIResponse response = new APIResponse();
			try
			{
				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.SalaryHeadDetails.Where(x => x.IsDeleted == false));
				var list = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var salaryheadlist = list.Select(x => new SalaryHeadModel
				{
					SalaryHeadId = x.SalaryHeadId,
					HeadTypeId = x.HeadTypeId,
					HeadName = x.HeadName,
					Description = x.Description
				}).ToList();

				response.data.SalaryHeadList = salaryheadlist;
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


		public async Task<APIResponse> AddPensionRate(EmployeePensionRateModel model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				if (model != null)
				{
					var financialYearCheck = await _uow.EmployeePensionRateRepository.FindAllAsync(x => x.FinancialYearId == model.FinancialYearId);
					if (financialYearCheck.Count == 0)
					{
						EmployeePensionRate obj = _mapper.Map<EmployeePensionRate>(model);
						var lst = await _uow.EmployeePensionRateRepository.FindAsync(x => x.IsDefault == true);
						if (model.IsDefault == true)
						{
							if (lst != null)
							{
								lst.IsDefault = false;
								_uow.GetDbContext().EmployeePensionRate.Update(lst);
								await _uow.SaveAsync();
							}
						}
						obj.IsDefault = (model.IsDefault == false && lst == null) ? true : model.IsDefault;
						obj.CreatedById = UserId;
						obj.CreatedDate = DateTime.Now;
						await _uow.EmployeePensionRateRepository.AddAsyn(obj);
						await _uow.SaveAsync();
						response.StatusCode = StaticResource.successStatusCode;
						response.Message = "Success";
					}
					else
					{
						response.StatusCode = 700;
						response.Message = "Financial Year Already exists!";
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> EditPensionRate(EmployeePensionRateModel model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var lst = await _uow.EmployeePensionRateRepository.FindAsync(x => x.IsDefault == true);
				if (model.IsDefault == true)
				{
					if (lst != null)
					{
						lst.IsDefault = false;
						_uow.GetDbContext().EmployeePensionRate.Update(lst);
						await _uow.SaveAsync();
					}

				}
				if (model.IsDefault == false)
				{
					if (lst == null)
						model.IsDefault = true;
					if (lst.IsDefault == true && lst.FinancialYearId == model.FinancialYearId)
						model.IsDefault = true;
					else
						model.IsDefault = false;
				}
				
				EmployeePensionRate obj = await _uow.EmployeePensionRateRepository.FindAsync(x => x.FinancialYearId == model.FinancialYearId);
				obj.IsDefault = true;
				obj.ModifiedById = UserId;
				obj.ModifiedDate = DateTime.Now;
				_mapper.Map(model, obj);
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

		public async Task<APIResponse> GetAllPensionRate()
		{
			APIResponse response = new APIResponse();
			try
			{
				response.data.EmployeePensionRateList = await Task.Run(() => _uow.GetDbContext().EmployeePensionRate.Include(x => x.FinancialYearDetail).Where(x => x.IsDeleted == false).Select(x => new EmployeePensionRateModel
				{
					FinancialYearId = x.FinancialYearId,
					FinancialYearName = x.FinancialYearDetail.FinancialYearName,
					PensionRate = x.PensionRate,
					IsDefault = x.IsDefault
				}).ToListAsync());

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

		public async Task<APIResponse> AddAppraisalQuestion(AppraisalQuestionModel model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				AppraisalGeneralQuestions obj = _mapper.Map<AppraisalGeneralQuestions>(model);
				obj.CreatedById = UserId;
				obj.CreatedDate = DateTime.Now;
				await _uow.AppraisalGeneralQuestionsRepository.AddAsyn(obj);
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

		public async Task<APIResponse> EditAppraisalQuestion(AppraisalQuestionModel model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var record = await _uow.AppraisalGeneralQuestionsRepository.FindAsync(x => x.AppraisalGeneralQuestionsId == model.AppraisalGeneralQuestionsId);
				record.SequenceNo = model.SequenceNo;
				record.Question = model.Question;
				record.DariQuestion = model.DariQuestion;
				record.ModifiedById = UserId;
				record.ModifiedDate = DateTime.Now;
				record.OfficeId = model.OfficeId;
				await _uow.AppraisalGeneralQuestionsRepository.UpdateAsyn(record);
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

		public async Task<APIResponse> GetAppraisalQuestions(int OfficeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				response.data.AppraisalList = await _uow.GetDbContext().AppraisalGeneralQuestions.Where(x => x.IsDeleted == false && x.OfficeId == OfficeId).ToListAsync();
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

		public async Task<APIResponse> AddEmployeeAppraisalDetails(EmployeeAppraisalDetailsModel model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				EmployeeAppraisalDetails obj = _mapper.Map<EmployeeAppraisalDetails>(model);
				obj.AppraisalStatus = false;
				obj.CreatedById = UserId;
				obj.CreatedDate = DateTime.Now;
				await _uow.EmployeeAppraisalDetailsRepository.AddAsyn(obj);				
				List<EmployeeAppraisalQuestions> lst = new List<EmployeeAppraisalQuestions>();
				foreach (var item in model.EmployeeAppraisalQuestionList)
				{
					EmployeeAppraisalQuestions eaq = new EmployeeAppraisalQuestions();
					eaq.AppraisalGeneralQuestionsId = item.AppraisalGeneralQuestionsId;
					eaq.Score = item.Score;
					eaq.Remarks = item.Remarks;
					eaq.EmployeeId = model.EmployeeId;
					eaq.CurrentAppraisalDate = model.CurrentAppraisalDate;
					eaq.CreatedDate = DateTime.Now;
					eaq.CreatedById = UserId;
					lst.Add(eaq);
				}
				await _uow.GetDbContext().EmployeeAppraisalQuestions.AddRangeAsync(lst);
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

		public async Task<APIResponse> EditEmployeeAppraisalDetails(EmployeeAppraisalDetailsModel model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var emp = await _uow.EmployeeAppraisalDetailsRepository.FindAsync(x => x.EmployeeId == model.EmployeeId && x.OfficeId == model.OfficeId && x.CurrentAppraisalDate == model.CurrentAppraisalDate);
				emp.Position = model.Position;
				emp.Department = model.Department;
				emp.DutyStation = model.DutyStation;
				emp.AppraisalPeriod = model.AppraisalPeriod;
				foreach (var item in model.EmployeeAppraisalQuestionList)
				{
					var question = await _uow.EmployeeAppraisalQuestionsRepository.FindAsync(x => x.EmployeeId == model.EmployeeId && x.CurrentAppraisalDate == model.CurrentAppraisalDate && x.AppraisalGeneralQuestionsId == item.AppraisalGeneralQuestionsId);
					question.Score = item.Score;
					question.Remarks = item.Remarks;
					await _uow.EmployeeAppraisalQuestionsRepository.UpdateAsyn(question);
				}
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

		public async Task<APIResponse> GetAllEmployeeAppraisalDetails(int OfficeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();				
				var emplst = await _uow.EmployeeAppraisalDetailsRepository.FindAllAsync(x=>x.OfficeId == OfficeId && x.AppraisalStatus == false);				
				foreach (var item in emplst)
				{
					EmployeeAppraisalDetailsModel model = new EmployeeAppraisalDetailsModel();
					var quesLst = await _uow.GetDbContext().EmployeeAppraisalQuestions.Include(x=>x.AppraisalGeneralQuestions).Where(x => x.EmployeeId == item.EmployeeId && x.CurrentAppraisalDate == item.CurrentAppraisalDate).ToListAsync();
					model.EmployeeId = item.EmployeeId;
					model.EmployeeCode = item.EmployeeCode;
					model.EmployeeName = item.EmployeeName;
					model.FatherName = item.FatherName;
					model.Position = item.Position;
					model.Department = item.Department;
					model.Qualification = item.Qualification;
					model.DutyStation = item.DutyStation;
					model.RecruitmentDate = item.RecruitmentDate;
					model.AppraisalPeriod = item.AppraisalPeriod;
					model.CurrentAppraisalDate = item.CurrentAppraisalDate;					
					foreach (var element in quesLst)
					{
						EmployeeAppraisalQuestionModel questions = new EmployeeAppraisalQuestionModel();
						questions.QuestionEnglish = element.AppraisalGeneralQuestions.Question;
						questions.QuestionDari = element.AppraisalGeneralQuestions.DariQuestion;
						questions.SequenceNo = element.AppraisalGeneralQuestions.SequenceNo;
						questions.AppraisalGeneralQuestionsId = element.AppraisalGeneralQuestionsId;
						questions.Score = element.Score;
						questions.Remarks = element.Remarks;
						model.EmployeeAppraisalQuestionList.Add(questions);						
					}
					lst.Add(model);
				}				
				response.data.EmployeeAppraisalDetailsModelLst = lst;
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

		public async Task<APIResponse> AddEmployeeAppraisalMoreDetails(EmployeeAppraisalDetailsModel model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				List<EmployeeEvaluation> lst = new List<EmployeeEvaluation>();
				foreach (var item in model.EmployeeEvaluationModelList)
				{
					EmployeeEvaluation obj = new EmployeeEvaluation();
					obj.TrainingProgram = item.TrainingProgram;
					obj.Program = item.Program;
					obj.Participated = item.Participated;
					obj.CatchLevel = item.CatchLevel;
					obj.RefresherTrm = item.RefresherTrm;
					obj.OthRecommendation = item.OthRecommendation;
					obj.FinalResultQues1 = model.FinalResultQues1;
					obj.FinalResultQues2 = model.FinalResultQues2;
					obj.FinalResultQues3 = model.FinalResultQues3;
					obj.FinalResultQues4 = model.FinalResultQues4;
					obj.FinalResultQues5 = model.FinalResultQues5;
					obj.DirectSupervisor = model.DirectSupervisor;
					obj.AppraisalTeamMember1 = model.AppraisalTeamMember1;
					obj.AppraisalTeamMember2 = model.AppraisalTeamMember2;
					obj.CommentsByEmployee = model.CommentsByEmployee;
					obj.CreatedById = UserId;
					obj.CreatedDate = DateTime.Now;
					obj.CurrentAppraisalDate = model.CurrentAppraisalDate;
					obj.EmployeeId = model.EmployeeId;
					lst.Add(obj);
				}
				foreach (var item in model.StrongPoints)
				{
					EmployeeEvaluation obj = new EmployeeEvaluation();
					obj.StrongPoints = item;
					obj.CreatedById = UserId;
					obj.CreatedDate = DateTime.Now;
					obj.CurrentAppraisalDate = model.CurrentAppraisalDate;
					obj.EmployeeId = model.EmployeeId;
					lst.Add(obj);
				}
				foreach (var item in model.WeakPoints)
				{
					EmployeeEvaluation obj = new EmployeeEvaluation();
					obj.WeakPoints = item;
					obj.CreatedById = UserId;
					obj.CreatedDate = DateTime.Now;
					obj.CurrentAppraisalDate = model.CurrentAppraisalDate;
					obj.EmployeeId = model.EmployeeId;
					lst.Add(obj);
				}
				await _uow.GetDbContext().EmployeeEvaluation.AddRangeAsync(lst);
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

		public async Task<APIResponse> EditEmployeeAppraisalMoreDetails(EmployeeAppraisalDetailsModel model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var recordList = await _uow.EmployeeEvaluationRepository.FindAllAsync(x => x.EmployeeId == model.EmployeeId && x.CurrentAppraisalDate == model.CurrentAppraisalDate);
				_uow.GetDbContext().RemoveRange(recordList);

				if (recordList != null)
				{
					List<EmployeeEvaluation> lst = new List<EmployeeEvaluation>();
					foreach (var item in model.EmployeeEvaluationModelList)
					{
						EmployeeEvaluation obj = new EmployeeEvaluation();
						obj.TrainingProgram = item.TrainingProgram;
						obj.Program = item.Program;
						obj.Participated = item.Participated;
						obj.CatchLevel = item.CatchLevel;
						obj.RefresherTrm = item.RefresherTrm;
						obj.OthRecommendation = item.OthRecommendation;
						obj.FinalResultQues1 = model.FinalResultQues1;
						obj.FinalResultQues2 = model.FinalResultQues2;
						obj.FinalResultQues3 = model.FinalResultQues3;
						obj.FinalResultQues4 = model.FinalResultQues4;
						obj.FinalResultQues5 = model.FinalResultQues5;
						obj.DirectSupervisor = model.DirectSupervisor;
						obj.AppraisalTeamMember1 = model.AppraisalTeamMember1;
						obj.AppraisalTeamMember2 = model.AppraisalTeamMember2;
						obj.CommentsByEmployee = model.CommentsByEmployee;
						obj.CreatedById = UserId;
						obj.CreatedDate = DateTime.Now;
						obj.CurrentAppraisalDate = model.CurrentAppraisalDate;
						obj.EmployeeId = model.EmployeeId;
						lst.Add(obj);
					}
					foreach (var item in model.StrongPoints)
					{
						EmployeeEvaluation obj = new EmployeeEvaluation();
						obj.StrongPoints = item;
						obj.CreatedById = UserId;
						obj.CreatedDate = DateTime.Now;
						obj.CurrentAppraisalDate = model.CurrentAppraisalDate;
						obj.EmployeeId = model.EmployeeId;
						lst.Add(obj);
					}
					foreach (var item in model.WeakPoints)
					{
						EmployeeEvaluation obj = new EmployeeEvaluation();
						obj.WeakPoints = item;
						obj.CreatedById = UserId;
						obj.CreatedDate = DateTime.Now;
						obj.CurrentAppraisalDate = model.CurrentAppraisalDate;
						obj.EmployeeId = model.EmployeeId;
						lst.Add(obj);
					}
					await _uow.GetDbContext().EmployeeEvaluation.AddRangeAsync(lst);
				}
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

		public async Task<APIResponse> GetAllEmployeeAppraisalMoreDetails(int OfficeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();			
				var emplst = await _uow.EmployeeAppraisalDetailsRepository.FindAllAsync(x => x.OfficeId == OfficeId && x.AppraisalStatus == false);
				foreach (var item in emplst)
				{
					var detail = emplst.Where(x => x.EmployeeId == item.EmployeeId && x.CurrentAppraisalDate == item.CurrentAppraisalDate).ToList();
					//	EmployeeAppraisalDetailsModel model = new EmployeeAppraisalDetailsModel();
					//	var quesLst = await _uow.GetDbContext().EmployeeAppraisalQuestions.Include(x => x.AppraisalGeneralQuestions).Where(x => x.EmployeeId == item.EmployeeId && x.CurrentAppraisalDate == item.CurrentAppraisalDate).ToListAsync();
					//	model.EmployeeId = item.EmployeeId;
					//	model.EmployeeCode = item.EmployeeCode;
					//	model.EmployeeName = item.EmployeeName;
					//	model.FatherName = item.FatherName;
					//	model.Position = item.Position;
					//	model.Department = item.Department;
					//	model.Qualification = item.Qualification;
					//	model.DutyStation = item.DutyStation;
					//	model.RecruitmentDate = item.RecruitmentDate;
					//	model.AppraisalPeriod = item.AppraisalPeriod;
					//	model.CurrentAppraisalDate = item.CurrentAppraisalDate;
					//	foreach (var element in quesLst)
					//	{
					//		EmployeeAppraisalQuestionModel questions = new EmployeeAppraisalQuestionModel();
					//		questions.QuestionEnglish = element.AppraisalGeneralQuestions.Question;
					//		questions.QuestionDari = element.AppraisalGeneralQuestions.DariQuestion;
					//		questions.SequenceNo = element.AppraisalGeneralQuestions.SequenceNo;
					//		questions.AppraisalGeneralQuestionsId = element.AppraisalGeneralQuestionsId;
					//		questions.Score = element.Score;
					//		questions.Remarks = element.Remarks;
					//		model.EmployeeAppraisalQuestionList.Add(questions);
					//	}
					//	lst.Add(model);
				}
				//response.data.EmployeeAppraisalDetailsModelLst = lst;
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

		public async Task<APIResponse> GetEmployeeDetailByOfficeId(int OfficeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				response.data.EmployeeDetailListData = await _uow.GetDbContext().EmployeeDetail.Include(x => x.EmployeeProfessionalDetail).Where(x=>x.EmployeeProfessionalDetail.OfficeId == OfficeId).Select(x=> new EmployeeDetailList
				{
					EmployeeId = x.EmployeeID,
					EmployeeName = x.EmployeeName,
					EmployeeCode = x.EmployeeCode
				}).ToListAsync();
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

		public async Task<APIResponse> GetEmployeeDetailByEmployeeId(int EmployeeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				response.data.EmployeeDetailListData = await _uow.GetDbContext().EmployeeDetail.Include(x => x.EmployeeProfessionalDetail).Include(x=>x.EmployeeProfessionalDetail.OfficeDetail).Include(x=>x.EmployeeProfessionalDetail.Department).Include(x=>x.ProfessionDetails).Include(x=>x.QualificationDetails).Where(x => x.EmployeeID == EmployeeId).Select(x => new EmployeeDetailList
				{
					EmployeeId = x.EmployeeID,
					EmployeeName = x.EmployeeName,
					EmployeeCode = x.EmployeeCode,
					FathersName = x.FatherName,
					Position = x.ProfessionDetails.ProfessionName,
					Department = x.EmployeeProfessionalDetail.Department.DepartmentName,
					Qualification = x.QualificationDetails.QualificationName,
					DutyStation = x.EmployeeProfessionalDetail.OfficeDetail.OfficeName,
					RecruitmentDate = x.EmployeeProfessionalDetail.HiredOn
				}).ToListAsync();
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
