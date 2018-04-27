using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;

namespace HumanitarianAssistance.Service.Classes
{
	public class HREmployeeService : IHREmployee
	{
		IUnitOfWork _uow;
		IMapper _mapper;
		UserManager<AppUser> _userManager;
		IHostingEnvironment _env;
		public HREmployeeService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager, IHostingEnvironment env)
		{
			this._uow = uow;
			this._mapper = mapper;
			this._userManager = userManager;
			this._env = env;
		}

		public async Task<APIResponse> AddNewEmployee(EmployeeDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				EmployeeDetail obj = _mapper.Map<EmployeeDetail>(model);
				await _uow.EmployeeDetailRepository.AddAsyn(obj);
				await _uow.SaveAsync();

				EmployeeProfessionalDetailModel empprofessional = new EmployeeProfessionalDetailModel();
				empprofessional.EmployeeId = obj.EmployeeID;
				empprofessional.EmployeeTypeId = model.EmployeeTypeId;
				empprofessional.OfficeId = model.OfficeId;
				empprofessional.CreatedById = model.CreatedById;
				empprofessional.CreatedDate = model.CreatedDate;
				empprofessional.IsDeleted = model.IsDeleted;
				EmployeeProfessionalDetail obj1 = _mapper.Map<EmployeeProfessionalDetail>(empprofessional);
				await _uow.EmployeeProfessionalDetailRepository.AddAsyn(obj1);
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

		public async Task<APIResponse> AddJobHiringDetail(JobHiringDetailsModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var jobhiringinfo = await _uow.JobHiringDetailsRepository.FindAsync(x => x.JobCode == model.JobCode);
				if (jobhiringinfo == null)
				{
					JobHiringDetails obj = _mapper.Map<JobHiringDetails>(model);
					await _uow.JobHiringDetailsRepository.AddAsyn(obj);
					await _uow.SaveAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
					response.Message = StaticResource.MandateNameAlreadyExist;
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> EditEmployeeDetail(EmployeeDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var employeeinfo = await _uow.EmployeeDetailRepository.FindAsync(x => x.EmployeeID == model.EmployeeID);
				if (employeeinfo != null)
				{
					employeeinfo.EmployeeTypeId = model.EmployeeTypeId;
					employeeinfo.EmployeeName = model.EmployeeName;
					employeeinfo.IDCard = model.IDCard;
					employeeinfo.FatherName = model.FatherName;
					employeeinfo.GradeId = model.GradeId;
					employeeinfo.PermanentAddress = model.PermanentAddress;
					employeeinfo.City = model.City;
					employeeinfo.District = model.District;
					employeeinfo.ProvinceId = model.ProvinceId;
					employeeinfo.CountryId = model.CountryId;
					employeeinfo.Phone = model.Phone;
					employeeinfo.Fax = model.Fax;
					employeeinfo.Email = model.Email;
					employeeinfo.ReferBy = model.ReferBy;
					employeeinfo.Passport = model.Passport;
					employeeinfo.NationalityId = model.NationalityId;
					employeeinfo.Language = model.Language;
					employeeinfo.SexId = model.SexId;
					employeeinfo.Age = model.Age;
					employeeinfo.DateOfBirth = model.DateOfBirth;
					employeeinfo.PlaceOfBirth = model.PlaceOfBirth;
					employeeinfo.HigherQualificationId = model.HigherQualificationId;
					employeeinfo.CurrentAddress = model.CurrentAddress;
					employeeinfo.ProfessionId = model.ProfessionId;
					employeeinfo.PreviousWork = model.PreviousWork;
					employeeinfo.Remarks = model.Remarks;
					employeeinfo.ExperienceYear = model.ExperienceYear;
					employeeinfo.ExperienceMonth = model.ExperienceMonth;
					employeeinfo.EmployeePhoto = model.EmployeePhoto;
					await _uow.EmployeeDetailRepository.UpdateAsyn(employeeinfo);
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

		public async Task<APIResponse> EditJobHiringDetail(JobHiringDetailsModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var jobhiringinfo = await _uow.JobHiringDetailsRepository.FindAsync(x => x.JobId == model.JobId);
				if (jobhiringinfo != null)
				{
					jobhiringinfo.JobCode = model.JobCode;
					jobhiringinfo.JobDescription = model.JobDescription;
					jobhiringinfo.ProfessionId = model.ProfessionId;
					jobhiringinfo.Unit = model.Unit;
					jobhiringinfo.OfficeId = model.OfficeId;
					jobhiringinfo.GradeId = model.GradeId;
					jobhiringinfo.IsActive = model.IsActive;
					await _uow.JobHiringDetailsRepository.UpdateAsyn(jobhiringinfo);
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

		public async Task<APIResponse> GetAllEmployeeDetail()
		{
			APIResponse response = new APIResponse();
			try
			{
				var employeelist = await Task.Run(() =>
					_uow.GetDbContext().EmployeeDetail.ToListAsync()
				);

				//string str = string.Format("SELECT *  FROM" + @"""{0}""" + " WHERE " + @"""IsDeleted""" + " = {1} " + " ORDER BY " + @"""EmployeeID""" + "ASC", "EmployeeDetail", false);
				//var employeedetaillist = await _uow.GetDbContext().EmployeeDetail.FromSql(str).ToListAsync();

				var employeedetaillist = employeelist.Select(x => new EmployeeDetailsAllModel
				{
					EmployeeTypeId = x.EmployeeTypeId,
					EmployeeID = x.EmployeeID,
					EmployeeCode = x.EmployeeCode,
					EmployeeName = x.EmployeeName,
					EmployeePhoto = x.EmployeePhoto,
					DocumentGUID = x.DocumentGUID + x.Extension,
					SexName = x.SexId == (int)Gender.MALE ? "Male" : x.SexId == (int)Gender.FEMALE ? "Female" : x.SexId == (int)Gender.OTHER ? "Other" : null,
					Age = x.Age
				}).ToList();

				response.data.ProspectiveEmployeeDetailsList = employeedetaillist.Where(x => x.EmployeeTypeId == 1).OrderByDescending(x => x.EmployeeID).ToList();
				response.data.ActiveEmployeeDetailsList = employeedetaillist.Where(x => x.EmployeeTypeId == 2).OrderByDescending(x => x.EmployeeID).ToList();
				response.data.TerminatedEmployeeDetailsList = employeedetaillist.Where(x => x.EmployeeTypeId == 3).OrderByDescending(x => x.EmployeeID).ToList();
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

		public async Task<APIResponse> GetAllEmployeeDetail(int EmployeeType, int officeid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var queryResult = EF.CompileAsyncQuery(
					(ApplicationDbContext ctx) => ctx.EmployeeDetail
					.Include(e => e.EmployeeProfessionalDetail).Where(x => x.EmployeeProfessionalDetail.OfficeId == officeid)
					.Where(x => x.IsDeleted == false && x.EmployeeTypeId == EmployeeType));
				var employeelist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result.OrderByDescending(x => x.EmployeeID)
				);


				//var employeelist = await Task.Run(() =>
				//    _uow.GetDbContext().EmployeeDetail.Include(p=> p.EmployeeProfessionalDetail)
				//     .Where(x => x.IsDeleted == false && x.EmployeeTypeId == EmployeeType).OrderByDescending(x => x.EmployeeID).ToListAsync()
				//);

				var employeedetaillist = employeelist.Select(x => new EmployeeDetailsAllModel
				{
					EmployeeTypeId = x.EmployeeTypeId,
					EmployeeID = x.EmployeeID,
					EmployeeCode = x.EmployeeCode,
					EmployeeName = x.EmployeeName,
					EmployeePhoto = x.EmployeePhoto,
					DocumentGUID = x.DocumentGUID + x.Extension,
					EmployeeDOB = x.DateOfBirth,
					HiredOn = x.EmployeeProfessionalDetail?.HiredOn ?? null,
					SexName = x.SexId == (int)Gender.MALE ? "Male" : x.SexId == (int)Gender.FEMALE ? "Female" : x.SexId == (int)Gender.OTHER ? "Other" : null,
					Age = x.Age,
					Email = x.Email,
					Profession = x.EmployeeProfessionalDetail.JobDescription,
					DesignationId = x.EmployeeProfessionalDetail.DesignationId,
					ExperienceYear = x.ExperienceYear,
					ExperienceMonth = x.ExperienceMonth
				}).ToList();

				response.data.EmployeeDetailsList = employeedetaillist;
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

		public async Task<APIResponse> GetAllJobHiringDetails()
		{
			APIResponse response = new APIResponse();
			try
			{
				//var jobhiringlist = await Task.Run(() =>
				//    _uow.GetDbContext().JobHiringDetails
				//        .Include(p => p.JobGrade)
				//        .Include(p => p.ProfessionDetails).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.JobHiringDetails.Include(j => j.JobGrade).Include(p => p.ProfessionDetails).Where(x => x.IsDeleted == false));
				var jobhiringlist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var jobhiringdetailslist = jobhiringlist.Select(x => new JobHiringDetailsModel
				{
					JobId = x.JobId,
					JobCode = x.JobCode,
					//ProjectId = x.ProjectId,
					//ProjectName = x.ProjectDetails.ProjectName,
					JobDescription = x.JobDescription,
					ProfessionId = x.ProfessionId,
					ProfessionName = x.ProfessionDetails?.ProfessionName ?? null,
					//SkillSet = x.SkillSet,
					Unit = x.Unit,
					OfficeId = x.OfficeId,
					GradeId = x.GradeId,
					GradeName = x.JobGrade?.GradeName ?? null,
					//Budget = x.Budget,
					//SalaryType = x.SalaryType,
					IsActive = x.IsActive,

					//StartDate = x.StartDate,
					//EndDate = x.EndDate
				}).ToList();
				response.data.JobHiringDetailsList = jobhiringdetailslist;
				response.Message = "Success";
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> GetEmployeeDetailsByEmployeeId(int EmployeeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				//Get Employee General Information
				//var employeelist = await Task.Run(() =>
				//    _uow.GetDbContext().EmployeeDetail.Include(e=> e.EmployeeType)
				//        //.Include(o=> o.OfficeDetails)
				//        //.Include(d=> d.DepartmentDetails)
				//        //.Include(d=> d.DesignationDetails)
				//        .Include(p=> p.ProvinceDetails)
				//        .Include(c=> c.CountryDetails)
				//        .Include(n=> n.NationalityDetails)
				//        .Include(h=> h.QualificationDetails)
				//        .Include(p=> p.ProfessionDetails)
				//    .Where(x => x.EmployeeID == EmployeeId).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
				 (ApplicationDbContext ctx) => ctx.EmployeeDetail.Include(e => e.EmployeeType)
						.Include(p => p.ProvinceDetails)
						.Include(c => c.CountryDetails)
						//.Include(n => n.NationalityDetails)
						.Include(h => h.QualificationDetails)
						.Include(p => p.ProfessionDetails)
						.Where(x => x.EmployeeID == EmployeeId));
				var employeelist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var employeedetaillist = employeelist.Select(x => new EmployeeDetailModel
				{
					EmployeeID = x.EmployeeID,
					EmployeeCode = x.EmployeeCode,
					EmployeeTypeId = x.EmployeeTypeId,
					EmployeeTypeName = x.EmployeeType?.EmployeeTypeName ?? null,
					EmployeeName = x.EmployeeName,
					//IDCard = x.IDCard,
					FatherName = x.FatherName,
					//OfficeId = x.OfficeId,
					//OfficeName = x.OfficeDetails?.OfficeName?? null,
					//DepartmentId = x.DepartmentId,
					//DepartmentName = x.DepartmentDetails?.DepartmentName ?? null,
					GradeId = x.GradeId,
					//DesignationId = x.DesignationId,
					//DesignationName = x.DesignationDetails?.Designation ?? null,
					PermanentAddress = x.PermanentAddress,
					CurrentAddress = x.CurrentAddress,
					City = x.City,
					//District = x.District,
					ProvinceId = x.ProvinceId,
					ProvinceName = x.ProvinceDetails?.ProvinceName ?? null,
					CountryId = x.CountryId,
					CountryName = x.CountryDetails?.CountryName ?? null,
					Phone = x.Phone,
					//Fax = x.Fax,
					Email = x.Email,
					ReferBy = x.ReferBy,
					//Passport = x.Passport,
					//NationalityId = x.NationalityId,
					//NationalityName = x.NationalityDetails?.NationalityName ?? null,
					//Language = x.Language,
					SexId = x.SexId,
					SexName = x.SexId == (int)Gender.MALE ? "Male" : x.SexId == (int)Gender.FEMALE ? "Female" : x.SexId == (int)Gender.OTHER ? "Other" : null,
					DateOfBirth = x.DateOfBirth,
					Age = x.Age ?? 0,
					//PlaceOfBirth = x.PlaceOfBirth,
					HigherQualificationId = x.HigherQualificationId,
					HigherQualificationName = x.QualificationDetails?.QualificationName ?? null,
					ProfessionId = x.ProfessionId,
					ProfessionName = x.ProfessionDetails?.ProfessionName ?? null,
					PreviousWork = x.PreviousWork,
					//Remarks = x.Remarks,
					ExperienceYear = x.ExperienceYear,
					ExperienceMonth = x.ExperienceMonth,
					Resume = x.Resume,
					EmployeePhoto = x.EmployeePhoto,
					DocumentGUID = x.DocumentGUID + x.Extension,
				}).ToList();


				//Get EmployeeProfessional Details
				//var employeeprofessional = await Task.Run(() =>
				//    _uow.GetDbContext().EmployeeProfessionalDetail.Include(e => e.EmployeeType)
				//    .Include(o => o.OfficeDetail)
				//    .Include(d => d.DesignationDetails)
				//    .Include(d => d.Department).Where(x => x.EmployeeId == EmployeeId).ToListAsync()
				//);
				//var employeeprofessionallist = employeeprofessional.Select(x => new EmployeeProfessionalDetailModel
				//{
				//    EmployeeProfessionalId = x.EmployeeProfessionalId,
				//    EmployeeTypeId = x.EmployeeTypeId,
				//    EmployeeTypeName = x.EmployeeType?.EmployeeTypeName ?? null,
				//    Status = x.Status,
				//    OfficeId = x.OfficeId,
				//    OfficeName = x.OfficeDetail?.OfficeName ?? null,
				//    DesignationId = x.DesignationId,
				//    DesignationName = x.DesignationDetails?.Designation ?? null,
				//    DepartmentId = x.DepartmentId,
				//    DepartmentName = x.Department?.DepartmentName ?? null,
				//    HiredOn = x.HiredOn,
				//    FiredOn = x.FiredOn,
				//    FiredReason = x.FiredReason,
				//    ResignationOn = x.ResignationOn,
				//    ResignationReason = x.ResignationReason,
				//    JobDescription = x.JobDescription,
				//    TrainingBenefits = x.TrainingBenefits
				//}).ToList();
				//employeedetaillist[0].EmployeeProfessionalList = employeeprofessionallist;


				//Get Employee Document Details
				//var list = await Task.Run(() =>
				//    _uow.EmployeeDocumentDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).Result.ToList()
				//);
				//var documentlist = list.Select(x => new EmployeeDocumentDetailModel
				//{
				//    //DocumentID = x.DocumentID,
				//    //DocumentName = x.DocumentName,
				//    //FilePath = x.FilePath,
				//    //DocumentDate = x.DocumentDate,
				//    //DocumentGUID=x.DocumentGUID
				//    DocumentGUID = x.DocumentGUID + x.Extension,
				//    DocumentName = x.DocumentName,
				//    FilePath = Encoding.UTF8.GetString(x.FilePath)

				//}).ToList();
				//employeedetaillist[0].EmployeeDocumentDetailList = documentlist;

				//Get Employee History Details
				//var list1 = await Task.Run(() =>
				//    _uow.EmployeeHistoryDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).Result.ToList()
				//);
				//var employeehistorylist = list1.Select(x => new EmployeeHistoryDetailModel
				//{
				//    HistoryID = x.HistoryID,
				//    HistoryDate = x.HistoryDate,
				//    Description = x.Description
				//}).ToList();
				//employeedetaillist[0].EmployeeHistoryDetailList = employeehistorylist;

				//Get Assign Employee Leave Details
				//var financialyear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDeleted == false && x.IsDefault == true);
				//if (financialyear != null)
				//{
				//    int financialyearid= financialyear.FinancialYearId;
				//    var list2 = await Task.Run(() =>
				//        _uow.GetDbContext().AssignLeaveToEmployee.Include(x => x.LeaveReasonDetails).Where(x => x.IsDeleted == false && x.FinancialYearId == financialyearid && x.EmployeeId == EmployeeId).ToListAsync()
				//    );
				//    var assignleavelist = list2.Select(x => new AssignLeaveToEmployeeModel
				//    {
				//        LeaveId = x.LeaveId,
				//        LeaveReasonId = x.LeaveReasonId,
				//        LeaveReasonName = x.LeaveReasonDetails.ReasonName,
				//        Unit = x.LeaveReasonDetails.Unit,
				//        AssignUnit = x.AssignUnit,
				//        BlanceLeave = (x.AssignUnit - (x.UsedLeaveUnit ?? 0)),
				//        FinancialYearId = x.FinancialYearId,
				//        Description = x.Description
				//    }).ToList();
				//    employeedetaillist[0].AssignLeaveToEmployeeList = assignleavelist;
				//}

				//Get Employee Health Information
				//var healthlist = await Task.Run(() =>
				//    _uow.EmployeeHealthDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.EmployeeId == EmployeeId).Result.ToList()
				//);
				//var emphealthlist = healthlist.Select(x => new EmployeeHealthInformationModel
				//{
				//    HealthInfoId = x.HealthInfoId,
				//    EmployeeId = x.EmployeeId,
				//    BloodGroup = x.BloodGroup,
				//    MedicalHistory = x.MedicalHistory,
				//    SmokeAndDrink = x.SmokeAndDrink,
				//    Insurance = x.Insurance,
				//    MedicalInsurance = x.MedicalInsurance,
				//    MeasureDieases = x.MeasureDiseases,
				//    AllergicSubstance = x.AllergicSubstance,
				//    FamilyHistory = x.FamilyHistory
				//}).ToList();
				//employeedetaillist[0].EmployeeHealthInfoList = emphealthlist;

				response.data.EmployeeDetailList = employeedetaillist;
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

		public async Task<APIResponse> GetEmployeeGeneralInformationById(int employeeid)
		{
			APIResponse response = new APIResponse();
			try
			{
				//var employeelist = await Task.Run(() =>
				//    _uow.GetDbContext().EmployeeDetail.Include(e => e.EmployeeType)
				//        .Include(p => p.ProvinceDetails)
				//        .Include(c => c.CountryDetails)
				//        .Include(n => n.NationalityDetails)
				//        .Include(h => h.QualificationDetails)
				//        .Include(p => p.ProfessionDetails)
				//    .Where(x => x.EmployeeID == employeeid).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
				(ApplicationDbContext ctx) => ctx.EmployeeDetail.Include(e => e.EmployeeType)
					   .Include(p => p.ProvinceDetails)
					   .Include(c => c.CountryDetails)
					   //.Include(n => n.NationalityDetails)
					   .Include(h => h.QualificationDetails)
					   .Include(p => p.ProfessionDetails)
					   .Where(x => x.EmployeeID == employeeid));
				var employeelist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var employeedetaillist = employeelist.Select(x => new EmployeeDetailModel
				{
					EmployeeID = x.EmployeeID,
					EmployeeCode = x.EmployeeCode,
					EmployeeTypeId = x.EmployeeTypeId,
					EmployeeTypeName = x.EmployeeType?.EmployeeTypeName ?? null,
					EmployeeName = x.EmployeeName,
					//IDCard = x.IDCard,
					FatherName = x.FatherName,
					GradeId = x.GradeId,
					PermanentAddress = x.PermanentAddress,
					CurrentAddress = x.CurrentAddress,
					City = x.City,
					//District = x.District,
					ProvinceId = x.ProvinceId,
					ProvinceName = x.ProvinceDetails?.ProvinceName ?? null,
					CountryId = x.CountryId,
					CountryName = x.CountryDetails?.CountryName ?? null,
					Phone = x.Phone,
					//Fax = x.Fax,
					Email = x.Email,
					ReferBy = x.ReferBy,
					//Passport = x.Passport,
					//NationalityId = x.NationalityId,
					//NationalityName = x.NationalityDetails?.NationalityName ?? null,
					//Language = x.Language,
					SexId = x.SexId,
					SexName = x.SexId == (int)Gender.MALE ? "Male" : x.SexId == (int)Gender.FEMALE ? "Female" : x.SexId == (int)Gender.OTHER ? "Other" : null,
					DateOfBirth = x.DateOfBirth,
					Age = x.Age ?? 0,
					//PlaceOfBirth = x.PlaceOfBirth,
					HigherQualificationId = x.HigherQualificationId,
					HigherQualificationName = x.QualificationDetails?.QualificationName ?? null,
					ProfessionId = x.ProfessionId,
					ProfessionName = x.ProfessionDetails?.ProfessionName ?? null,
					PreviousWork = x.PreviousWork,
					//Remarks = x.Remarks,
					ExperienceYear = x.ExperienceYear,
					ExperienceMonth = x.ExperienceMonth,
					//Resume = x.Resume,
					EmployeePhoto = x.EmployeePhoto
				}).ToList();
				response.data.EmployeeDetailList = employeedetaillist;
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

		//public async Task<APIResponse> GetAllProspectiveEmployee()
		//{
		//    APIResponse response = new APIResponse();
		//    try
		//    {
		//        // var emplist = await Task.Run(() =>
		//        //    _uow.GetDbContext().EmployeeDetail.Include(p=> p.ProfessionDetails).Where(x=> x.EmployeeTypeId ==1 && x.IsDeleted == false).ToListAsync()
		//        //);

		//        var queryResult = EF.CompileAsyncQuery(
		//          (ApplicationDbContext ctx) => ctx.EmployeeDetail.Include(p => p.ProfessionDetails).Where(x => x.EmployeeTypeId == 1));
		//        var emplist = await Task.Run(() =>
		//            queryResult(_uow.GetDbContext()).ToListAsync().Result
		//        );

		//        var employeelist = emplist.Select(x => new InterviewScheduleForProspectiveEmployeeModel
		//        {
		//            EmployeeId = x.EmployeeID,
		//            EmployeeCode = x.EmployeeCode,
		//            EmployeeName = x.EmployeeName,
		//            PhoneNo = x.Phone,
		//            //Resume = x.Resume,
		//            //BOC Alpit
		//            ProfessionId = x.ProfessionId,
		//            ProfessionName = x.ProfessionDetails?.ProfessionName ?? "",
		//            //EOC Alpit
		//        }).ToList();
		//        response.data.ISFPEmployeeList = employeelist;
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

		public async Task<APIResponse> AddInterviewScheduleDetails(List<InterviewScheduleModel> model, string CreatedById)
		{
			APIResponse response = new APIResponse();
			try
			{
				foreach (var i in model)
				{
					i.CreatedById = CreatedById;
					i.CreatedDate = DateTime.UtcNow;
					i.IsDeleted = false;
					i.InterviewStatus = 1;   //Alpit
					InterviewScheduleDetails obj = _mapper.Map<InterviewScheduleDetails>(i);
					await _uow.InterviewScheduleDetailsRepository.AddAsyn(obj);
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

		//public async Task<APIResponse> AddInterviewFeedbackDetails(InterviewFeedbackDetailsModel model)
		//{
		//    APIResponse response = new APIResponse();
		//    try
		//    {
		//        InterviewFeedbackDetails obj = _mapper.Map<InterviewFeedbackDetails>(model);
		//        await _uow.InterviewFeedbackDetailsRepository.AddAsyn(obj);
		//        await _uow.SaveAsync();
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

		//public async Task<APIResponse> GetAllInterviewFeedbackByScheduleId(int? ScheduleId)
		//{
		//    APIResponse response = new APIResponse();
		//    try
		//    {
		//        var feedbacklist = await Task.Run(() =>
		//                _uow.GetDbContext().InterviewFeedbackDetails.Include(e => e.EmployeeDetails).Include(r => r.InterviewRoundTypeMasters).Where(x => x.ScheduleId == ScheduleId).ToListAsync()
		//            );

		//        var interviewfeedbacklist = feedbacklist.Select(x => new InterviewFeedbackDetailsModel
		//        {
		//            FeedbackId = x.FeedbackId,
		//            ScheduleId = x.ScheduleId,
		//            InterviewerId = x.InterviewerId,
		//            InterviewerName = x.EmployeeDetails.EmployeeName,
		//            RoundId = x.RoundId,
		//            RoundName = x.InterviewRoundTypeMasters.RoundTypeName,
		//            Status = x.Status,
		//            Date = x.Date,
		//            Description = x.Description
		//        }).ToList();
		//        response.data.InterviewFeedbackDetailsList = interviewfeedbacklist;
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

		public async Task<APIResponse> AddEmployeeSalaryDetail(List<EmployeePayrollModel> model, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{

				List<EmployeePayroll> employeepayrolllist = new List<EmployeePayroll>();
				foreach (var list in model)
				{
					EmployeePayroll obj = new EmployeePayroll();
					obj.EmployeeID = list.EmployeeId;
					obj.CurrencyId = list.CurrencyId;
					obj.SalaryHeadId = list.SalaryHeadId;
					obj.HeadTypeId = list.HeadTypeId;
					obj.MonthlyAmount = list.MonthlyAmount;
					obj.PaymentType = list.PaymentType;
					obj.PensionRate = list.PensionRate;
					employeepayrolllist.Add(obj);
				}
				await _uow.GetDbContext().EmployeePayroll.AddRangeAsync(employeepayrolllist);
				await _uow.SaveAsync();

				double totalgeneraamount = model.Where(x => x.HeadTypeId == (int)SalaryHeadType.GENERAL).Sum(x => x.MonthlyAmount);
				double totalallowance = model.Where(x => x.HeadTypeId == (int)SalaryHeadType.ALLOWANCE).Sum(x => x.MonthlyAmount);
				double totaldeduction = model.Where(x => x.HeadTypeId == (int)SalaryHeadType.DEDUCTION).Sum(x => x.MonthlyAmount);

				EmployeeSalaryDetails obj1 = new EmployeeSalaryDetails();
				obj1.EmployeeId = model[0].EmployeeId;
				obj1.CurrencyId = model[0].CurrencyId;
				obj1.TotalGeneralAmount = totalgeneraamount;
				obj1.TotalAllowance = totalallowance;
				obj1.Totalduduction = totaldeduction;
				obj1.PaymentType = model[0].PaymentType;
				obj1.PensionRate = model[0].PensionRate;
				obj1.CreatedById = userid;
				obj1.CreatedDate = DateTime.UtcNow;
				obj1.IsDeleted = false;
				await _uow.EmployeeSalaryDetailsRepository.AddAsyn(obj1);
				await _uow.SaveAsync();


				//EmployeeSalaryDetails obj = _mapper.Map<EmployeeSalaryDetails>(model);
				//await _uow.EmployeeSalaryDetailsRepository.AddAsyn(obj);
				//await _uow.SaveAsync();
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

		public async Task<APIResponse> EditEmployeeSalaryDetail(List<EmployeePayrollModel> model, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.EmployeePayrollRepository.FindAllAsync(x => x.EmployeeID == model[0].EmployeeId);
				_uow.GetDbContext().EmployeePayroll.RemoveRange(existrecord);
				await _uow.SaveAsync();

				List<EmployeePayroll> employeepayrolllist = new List<EmployeePayroll>();
				foreach (var list in model)
				{
					EmployeePayroll obj = new EmployeePayroll();
					obj.EmployeeID = list.EmployeeId;
					obj.CurrencyId = list.CurrencyId;
					obj.HeadTypeId = list.HeadTypeId;
					obj.SalaryHeadId = list.SalaryHeadId;
					obj.MonthlyAmount = list.MonthlyAmount;
					obj.PaymentType = list.PaymentType;
					obj.PensionRate = list.PensionRate;
					employeepayrolllist.Add(obj);
				}
				await _uow.GetDbContext().EmployeePayroll.AddRangeAsync(employeepayrolllist);
				await _uow.SaveAsync();

				var recordexist = await _uow.EmployeeSalaryDetailsRepository.FindAsync(x => x.EmployeeId == model[0].EmployeeId);

				double totalgeneraamount = model.Where(x => x.HeadTypeId == (int)SalaryHeadType.GENERAL).Sum(x => x.MonthlyAmount);
				double totalallowance = model.Where(x => x.HeadTypeId == (int)SalaryHeadType.ALLOWANCE).Sum(x => x.MonthlyAmount);
				double totaldeduction = model.Where(x => x.HeadTypeId == (int)SalaryHeadType.DEDUCTION).Sum(x => x.MonthlyAmount);

				if (recordexist != null)
				{
					recordexist.TotalGeneralAmount = totalgeneraamount;
					recordexist.TotalAllowance = totalallowance;
					recordexist.Totalduduction = totaldeduction;
					recordexist.CurrencyId = model[0].CurrencyId;
					recordexist.PaymentType = model[0].PaymentType;
					recordexist.PensionRate = model[0].PensionRate;
					recordexist.ModifiedById = userid;
					recordexist.ModifiedDate = DateTime.UtcNow;
					recordexist.IsDeleted = false;
					await _uow.EmployeeSalaryDetailsRepository.UpdateAsyn(recordexist);
				}

				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Success";

				//var existrecord = await _uow.EmployeeSalaryDetailsRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeId == model.EmployeeId);
				//if (existrecord != null)
				//{
				//	existrecord.CurrencyId = model.CurrencyId;
				//	//existrecord.MonthlySalary = model.MonthlySalary;
				//	//existrecord.FoodAllowance = model.FoodAllowance;
				//	//existrecord.TRAllowance = model.TRAllowance;
				//	//existrecord.MedicalAllowance = model.MedicalAllowance;
				//	//existrecord.OtherAllowance = model.OtherAllowance;
				//	existrecord.Description = model.Description;
				//	//existrecord.TotalAllowance = model.TotalAllowance;
				//	//existrecord.TotalEarning = model.TotalEarning;
				//	//existrecord.ProvidentFund = model.ProvidentFund;
				//	//existrecord.Otherdeduction = model.Otherdeduction;
				//	existrecord.Totalduduction = model.Totalduduction;
				//	//existrecord.NetAmount = model.NetAmount;
				//	existrecord.PaymentType = model.PaymentType;
				//	existrecord.ModifiedById = model.ModifiedById;
				//	existrecord.ModifiedDate = model.ModifiedDate;
				//	existrecord.IsDeleted = model.IsDeleted;
				//	await _uow.EmployeeSalaryDetailsRepository.UpdateAsyn(existrecord);

				//}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> GetEmployeeSalaryDetailsByEmployeeId(int EmployeeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				//var salarylist = await Task.Run(() =>
				//    _uow.EmployeeSalaryDetailsRepository.FindAllAsync(x=> x.EmployeeId == EmployeeId).Result.ToList()
				//);


				var employeepayrolllist = (from salaryhead in await _uow.SalaryHeadDetailsRepository.GetAllAsyn()
										   join emppayroll in await _uow.EmployeePayrollRepository.FindAllAsync(x => x.EmployeeID == EmployeeId) on salaryhead.SalaryHeadId equals emppayroll.SalaryHeadId
										   into employeepayrollinfo
										   from employeepayrolls in employeepayrollinfo.DefaultIfEmpty()
										   select new EmployeePayrollModel
										   {
											   PayrollId = employeepayrolls?.PayrollId ?? 0,
											   SalaryHeadType = salaryhead.HeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : salaryhead.HeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : salaryhead.HeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : "",
											   HeadTypeId = salaryhead.HeadTypeId,
											   SalaryHeadId = salaryhead.SalaryHeadId,
											   SalaryHead = salaryhead.HeadName,
											   MonthlyAmount = employeepayrolls?.MonthlyAmount ?? 0,
											   CurrencyId = employeepayrolls?.CurrencyId ?? 0,
											   PaymentType = employeepayrolls?.PaymentType ?? 0,
											   PensionRate = employeepayrolls?.PensionRate ?? 0
										   }).ToList();


				//            var queryResult = EF.CompileAsyncQuery(
				//  (ApplicationDbContext ctx) => ctx.EmployeeSalaryDetails.Where(x => x.EmployeeId == EmployeeId));
				//var salarylist = await Task.Run(() =>
				//	queryResult(_uow.GetDbContext()).ToListAsync().Result
				//);

				//var employeesalarylist = salarylist.Select(x => new EmployeeSalaryDetailsModel
				//{
				//	SalaryId = x.SalaryId,
				//	EmployeeId = x.EmployeeId,
				//	CurrencyId = x.CurrencyId,
				//	MonthlySalary = x.MonthlySalary,
				//	//FoodAllowance = x.FoodAllowance,
				//	//TRAllowance = x.TRAllowance,
				//	//MedicalAllowance = x.MedicalAllowance,
				//	//OtherAllowance = x.OtherAllowance,
				//	Description = x.Description,
				//	TotalAllowance = x.TotalAllowance,
				//	TotalEarning = x.TotalEarning,
				//	//ProvidentFund = x.ProvidentFund,
				//	//Otherdeduction = x.Otherdeduction,
				//	Totalduduction = x.Totalduduction,
				//	NetAmount = x.NetAmount,
				//	PaymentType = x.PaymentType
				//}).ToList();



				response.data.EmployeePayrollList = employeepayrolllist;
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

		public async Task<APIResponse> AssignLeaveToEmployeeDetail(AssignLeaveToEmployeeModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var assignleavelist = await _uow.AssignLeaveToEmployeeRepository.FindAsync(x => x.IsDeleted == false && x.FinancialYearId == model.FinancialYearId && x.LeaveReasonId == model.LeaveReasonId && x.EmployeeId == model.EmployeeId);
				if (assignleavelist == null)
				{
					AssignLeaveToEmployee obj = _mapper.Map<AssignLeaveToEmployee>(model);
					await _uow.AssignLeaveToEmployeeRepository.AddAsyn(obj);
					await _uow.SaveAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
					response.Message = "Leave Reason Type already exist for this financial year.";
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> EditAssignLeaveToEmployee(AssignLeaveToEmployeeModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var leaveinfo = await _uow.AssignLeaveToEmployeeRepository.FindAsync(x => x.IsDeleted == false && x.LeaveId == model.LeaveId);
				if (leaveinfo != null)
				{
					leaveinfo.AssignUnit = model.AssignUnit;
					leaveinfo.Description = model.Description;
					leaveinfo.ModifiedById = model.ModifiedById;
					leaveinfo.ModifiedDate = model.ModifiedDate;
					leaveinfo.IsDeleted = model.IsDeleted;
					await _uow.AssignLeaveToEmployeeRepository.UpdateAsyn(leaveinfo);
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

		public async Task<APIResponse> AddDocumentDetail(EmployeeDocumentDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				//EmployeeDocumentDetail obj = _mapper.Map<EmployeeDocumentDetail>(model);
				//await _uow.EmployeeDocumentDetailRepository.AddAsyn(obj);
				//await _uow.SaveAsync();
				//response.StatusCode = StaticResource.successStatusCode;
				//response.Message = "Success";


				byte[] filepathBase64 = Encoding.UTF8.GetBytes(model.FilePath);
				string[] str = model.FilePath.Split(",");
				byte[] filepath = Convert.FromBase64String(str[1]);

				string ex = str[0].Split("/")[1].Split(";")[0];

				string guidname = Guid.NewGuid().ToString();
				//byte[] filepath = Encoding.UTF8.GetBytes(str[1]);
				string filename = guidname + "." + ex;

				File.WriteAllBytes(@"Documents/" + filename, filepath);    // Document path for server

				//VoucherDocumentDetail obj = _mapper.Map<VoucherDocumentDetail>(model);
				EmployeeDocumentDetail obj = new EmployeeDocumentDetail();
				obj.DocumentGUID = guidname;
				//Doctype 1 for voucher document
				//obj.DocumentType = 1;
				obj.Extension = "." + ex;
				//obj.FilePath = filepathBase64;
				obj.DocumentName = model.DocumentName;
				obj.DocumentDate = model.DocumentDate;
				obj.EmployeeID = model.EmployeeID;
				obj.CreatedById = model.CreatedById;
				obj.CreatedDate = DateTime.UtcNow;
				obj.IsDeleted = false;
				await _uow.EmployeeDocumentDetailRepository.AddAsyn(obj);
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

		public async Task<APIResponse> DeleteDocumentDetail(int documentid, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var documentinfo = await _uow.EmployeeDocumentDetailRepository.FindAsync(x => x.DocumentID == documentid);
				if (documentinfo != null)
				{
					documentinfo.IsDeleted = true;
					documentinfo.ModifiedById = UserId;
					documentinfo.ModifiedDate = DateTime.UtcNow;
					await _uow.EmployeeDocumentDetailRepository.UpdateAsyn(documentinfo);
					response.StatusCode = StaticResource.failStatusCode;
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

		public async Task<APIResponse> GetAllDocumentDetailByEmployeeId(int EmployeeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				//var list = await Task.Run(() =>
				//    _uow.EmployeeDocumentDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).Result.ToList()
				//);

				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.EmployeeDocumentDetail.Where(x => x.EmployeeID == EmployeeId));
				var list = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var documentlist = list.Select(x => new EmployeeDocumentDetailModel
				{
					DocumentGUID = x.DocumentGUID + x.Extension,
					DocumentName = x.DocumentName,
					//FilePath = Encoding.UTF8.GetString(x.FilePath)
				}).ToList();

				response.data.EmployeeDocumentList = documentlist;
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

		public async Task<APIResponse> AddEmployeeHistoryDetail(EmployeeHistoryDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				EmployeeHistoryDetail obj = _mapper.Map<EmployeeHistoryDetail>(model);
				await _uow.EmployeeHistoryDetailRepository.AddAsyn(obj);
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

		public async Task<APIResponse> EditEmployeeHistoryDetail(EmployeeHistoryDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var historyinfo = await _uow.EmployeeHistoryDetailRepository.FindAsync(x => x.HistoryID == model.HistoryID);
				if (historyinfo != null)
				{
					historyinfo.HistoryDate = model.HistoryDate;
					historyinfo.Description = model.Description;
					historyinfo.ModifiedById = model.ModifiedById;
					historyinfo.ModifiedDate = model.ModifiedDate;
					historyinfo.IsDeleted = model.IsDeleted;
					await _uow.EmployeeHistoryDetailRepository.UpdateAsyn(historyinfo);
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

		public async Task<APIResponse> DeleteEmployeeHistoryDetail(int HistoryId, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var historyinfo = await _uow.EmployeeHistoryDetailRepository.FindAsync(x => x.HistoryID == HistoryId);
				if (historyinfo != null)
				{
					historyinfo.IsDeleted = true;
					historyinfo.ModifiedById = UserId;
					historyinfo.ModifiedDate = DateTime.UtcNow;
					await _uow.EmployeeHistoryDetailRepository.UpdateAsyn(historyinfo);
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

		public async Task<APIResponse> GetAllEmployeeHistoryByEmployeeId(int EmployeeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				//var list = await Task.Run(() =>
				//    _uow.EmployeeHistoryDetailRepository.FindAllAsync(x=> x.IsDeleted == false && x.EmployeeID == EmployeeId).Result.ToList()
				//);

				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.EmployeeHistoryDetail.Where(x => x.EmployeeID == EmployeeId));
				var list = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var employeehistorylist = list.Select(x => new EmployeeHistoryDetailModel
				{
					HistoryID = x.HistoryID,
					HistoryDate = x.HistoryDate != null ? Convert.ToDateTime(x.HistoryDate) : x.HistoryDate,
					Description = x.Description
				}).ToList();
				response.data.EmployeeHistoryDetailList = employeehistorylist;
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

		public async Task<APIResponse> AddEmployeeProfessionalDetail(EmployeeProfessionalDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				EmployeeProfessionalDetail obj = _mapper.Map<EmployeeProfessionalDetail>(model);
				await _uow.EmployeeProfessionalDetailRepository.AddAsyn(obj);
				await _uow.SaveAsync();

				var employeeinfo = await _uow.EmployeeDetailRepository.FindAsync(x => x.EmployeeID == model.EmployeeId);
				if (employeeinfo != null)
				{
					employeeinfo.EmployeeTypeId = model.EmployeeTypeId;
					await _uow.EmployeeDetailRepository.UpdateAsyn(employeeinfo);

					//if (model.EmployeeTypeId == (int)EmployeeTypeStatus.Active)
					//{

					//    var financiallist = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);

					//    var queryResult = EF.CompileAsyncQuery(
					//        (ApplicationDbContext ctx) => ctx.HolidayDetails
					//        .Where(x => x.IsDeleted == false && x.FinancialYearId == financiallist.FinancialYearId));
					//    var holidaylist = await Task.Run(() =>
					//        queryResult(_uow.GetDbContext()).ToListAsync().Result
					//    );

					//    List<EmployeeAttendance> attendancelist = new List<EmployeeAttendance>();
					//    foreach (var list in holidaylist)
					//    {
					//        EmployeeAttendance attendance = new EmployeeAttendance();
					//        attendance.EmployeeId = (int)model.EmployeeId;
					//        attendance.InTime = list.Date.Date;
					//        attendance.OutTime = list.Date.Date;
					//        attendance.TotalWorkTime = "0";
					//        attendance.HoverTimeHours = 0;
					//        attendance.AttendanceTypeId = (int)AttendanceType.H;
					//        attendance.HolidayId = list.HolidayId;
					//        attendance.Date = list.Date;
					//        attendance.CreatedDate = model.CreatedDate;
					//        attendance.CreatedById = model.CreatedById;
					//        attendance.IsDeleted = false;
					//        attendance.FinancialYearId = financiallist.FinancialYearId;
					//        attendancelist.Add(attendance);
					//    }
					//    await _uow.GetDbContext().EmployeeAttendance.AddRangeAsync(attendancelist);
					//    await _uow.SaveAsync();

					//}
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

		public async Task<APIResponse> EditEmployeeProfessionalDetail(EmployeeProfessionalDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.EmployeeProfessionalDetailRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeProfessionalId == model.EmployeeProfessionalId);
				if (existrecord != null)
				{
					existrecord.EmployeeTypeId = model.EmployeeTypeId;
					existrecord.OfficeId = model.OfficeId;
					existrecord.DepartmentId = model.DepartmentId;
					existrecord.DesignationId = model.DesignationId;
					existrecord.EmployeeContractTypeId = model.EmployeeContractTypeId;
					existrecord.HiredOn = model.HiredOn;
					existrecord.FiredOn = model.FiredOn;
					existrecord.FiredReason = model.FiredReason;
					existrecord.ResignationOn = model.ResignationOn;
					existrecord.TrainingBenefits = model.TrainingBenefits;
					existrecord.JobDescription = model.JobDescription;
					existrecord.ResignationReason = model.ResignationReason;
					existrecord.ModifiedById = model.ModifiedById;
					existrecord.ModifiedDate = model.ModifiedDate;
					await _uow.EmployeeProfessionalDetailRepository.UpdateAsyn(existrecord);

					var employeeinfo = await _uow.EmployeeDetailRepository.FindAsync(x => x.EmployeeID == model.EmployeeId);
					if (employeeinfo != null)
					{
						employeeinfo.EmployeeTypeId = model.EmployeeTypeId;
						await _uow.EmployeeDetailRepository.UpdateAsyn(employeeinfo);
					}


					//if (model.EmployeeTypeId == (int)EmployeeTypeStatus.Active)
					//{

					//    var financiallist = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);

					//    var queryResult = EF.CompileAsyncQuery(
					//        (ApplicationDbContext ctx) => ctx.HolidayDetails
					//        .Where(x => x.IsDeleted == false && x.FinancialYearId == financiallist.FinancialYearId));
					//    var holidaylist = await Task.Run(() =>
					//        queryResult(_uow.GetDbContext()).ToListAsync().Result
					//    );

					//    //                 var empattendanceResult = EF.CompileAsyncQuery(
					//    //                     (ApplicationDbContext ctx) => ctx.EmployeeAttendance
					//    //                     .Where(x => x.IsDeleted == false && x.FinancialYearId == financiallist.FinancialYearId && x.EmployeeId == model.EmployeeId));
					//    //                 var empattendancelist = await Task.Run(() =>
					//    //empattendanceResult(_uow.GetDbContext()).ToListAsync().Result
					//    //                 );

					//    var empattendancelist = await _uow.EmployeeAttendanceRepository.FindAllAsync(x => x.FinancialYearId == financiallist.FinancialYearId && x.EmployeeId == model.EmployeeId);


					//    List<EmployeeAttendance> attendancelist = new List<EmployeeAttendance>();
					//    foreach (var list in holidaylist)
					//    {
					//        var existlist = empattendancelist.Where(x => x.Date.Date == list.Date.Date).ToList();
					//        if (existlist.Count == 0)
					//        {
					//            EmployeeAttendance attendance = new EmployeeAttendance();
					//            attendance.EmployeeId = (int)model.EmployeeId;
					//            attendance.InTime = list.Date.Date;
					//            attendance.OutTime = list.Date.Date;
					//            attendance.TotalWorkTime = "0";
					//            attendance.HoverTimeHours = 0;
					//            attendance.AttendanceTypeId = (int)AttendanceType.H;
					//            attendance.HolidayId = list.HolidayId;
					//            attendance.Date = model.CreatedDate;
					//            attendance.CreatedDate = model.CreatedDate;
					//            attendance.CreatedById = model.CreatedById;
					//            attendance.IsDeleted = false;
					//            attendance.FinancialYearId = financiallist.FinancialYearId;
					//            attendancelist.Add(attendance);
					//        }
					//    }
					//    await _uow.GetDbContext().EmployeeAttendance.AddRangeAsync(attendancelist);
					//    await _uow.SaveAsync();


					//}
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

		public async Task<APIResponse> GetEmployeeProfessionalDetail(int EmployeeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				//var employeeprofessional = await Task.Run(() =>
				//    _uow.GetDbContext().EmployeeProfessionalDetail.Include(e => e.EmployeeType)
				//    .Include(o => o.OfficeDetail)
				//    .Include(d => d.DesignationDetails)
				//    .Include(d => d.Department).Where(x => x.EmployeeId == EmployeeId).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.EmployeeProfessionalDetail.Include(e => e.EmployeeType)
					.Include(o => o.OfficeDetail)
					.Include(d => d.DesignationDetails)
					.Include(d => d.Department)
					.Where(x => x.EmployeeId == EmployeeId));
				var employeeprofessional = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var employeeprofessionallist = employeeprofessional.Select(x => new EmployeeProfessionalDetailModel
				{
					EmployeeProfessionalId = x.EmployeeProfessionalId,
					EmployeeTypeId = x.EmployeeTypeId,
					EmployeeTypeName = x.EmployeeType?.EmployeeTypeName ?? null,
					Status = x.Status,
					OfficeId = x.OfficeId,
					OfficeName = x.OfficeDetail?.OfficeName ?? null,
					DesignationId = x.DesignationId,
					DesignationName = x.DesignationDetails?.Designation ?? null,
					DepartmentId = x.DepartmentId,
					DepartmentName = x.Department?.DepartmentName ?? null,
					EmployeeContractTypeId = x.EmployeeContractTypeId,
					HiredOn = x.HiredOn != null ? Convert.ToDateTime(x.HiredOn).Date : x.HiredOn,
					FiredOn = x.FiredOn != null ? Convert.ToDateTime(x.FiredOn).Date : x.FiredOn,
					FiredReason = x.FiredReason,
					ResignationOn = x.ResignationOn != null ? Convert.ToDateTime(x.ResignationOn).Date : x.ResignationOn,
					ResignationReason = x.ResignationReason,
					JobDescription = x.JobDescription,
					TrainingBenefits = x.TrainingBenefits
				}).ToList();
				response.data.EmployeeProfessionalList = employeeprofessionallist;
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

		public async Task<APIResponse> AddPayrollMonthlyHourDetail(PayrollMonthlyHourDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var payrollinfo = await _uow.PayrollMonthlyHourDetailRepository.FindAsync(x => x.IsDeleted == false && x.OfficeId == model.OfficeId);
				if (payrollinfo == null)
				{
					TimeSpan hours;
					hours = Convert.ToDateTime(model.OutTime) - Convert.ToDateTime(model.InTime);
					model.Hours = Convert.ToInt32(hours.ToString().Substring(0, 2));
					PayrollMonthlyHourDetail obj = _mapper.Map<PayrollMonthlyHourDetail>(model);
					await _uow.PayrollMonthlyHourDetailRepository.AddAsyn(obj);
					await _uow.SaveAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
					response.Message = "Daily Hours already set for this Office.";
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> EditPayrollMonthlyHourDetail(PayrollMonthlyHourDetailModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var payrollmonthlyinfo = await _uow.PayrollMonthlyHourDetailRepository.FindAsync(x => x.PayrollMonthlyHourID == model.PayrollMonthlyHourID);
				if (payrollmonthlyinfo != null)
				{
					TimeSpan hours;
					hours = Convert.ToDateTime(model.OutTime) - Convert.ToDateTime(model.InTime);

					payrollmonthlyinfo.OfficeId = model.OfficeId;
					payrollmonthlyinfo.PayrollMonth = model.PayrollMonth;
					payrollmonthlyinfo.PayrollYear = model.PayrollYear;
					payrollmonthlyinfo.Hours = Convert.ToInt32(hours.ToString().Substring(0, 2));
					payrollmonthlyinfo.InTime = model.InTime;
					payrollmonthlyinfo.OutTime = model.OutTime;
					payrollmonthlyinfo.ModifiedById = model.ModifiedById;
					payrollmonthlyinfo.ModifiedDate = model.ModifiedDate;
					payrollmonthlyinfo.IsDeleted = model.IsDeleted;
					await _uow.PayrollMonthlyHourDetailRepository.UpdateAsyn(payrollmonthlyinfo);
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

		public async Task<APIResponse> GetAllPayrollMonthlyHourDetail()
		{
			APIResponse response = new APIResponse();
			try
			{
				//var list = await Task.Run(() =>
				//    _uow.GetDbContext().PayrollMonthlyHourDetail.Include(x => x.OfficeDetails).Where(p => p.IsDeleted == false).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.PayrollMonthlyHourDetail.Include(x => x.OfficeDetails)
					.Where(p => p.IsDeleted == false));
				var list = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var payrollmonthlylist = list.Select(x => new PayrollMonthlyHourDetailModel
				{
					PayrollMonthlyHourID = x.PayrollMonthlyHourID,
					OfficeId = x.OfficeId,
					OfficeName = x.OfficeDetails.OfficeName,
					PayrollMonth = x.PayrollMonth,
					PayrollYear = x.PayrollYear,
					Hours = x.Hours,
					InTime = x.InTime,
					OutTime = x.OutTime
				}).ToList();
				response.data.PayrollMonthlyHourList = payrollmonthlylist;
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

		public async Task<APIResponse> GetAllEmployeeAssignLeave(int EmployeeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var financialyearid = _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDeleted == false && x.IsDefault == true).Result.FinancialYearId;
				//var list = await Task.Run(() =>
				//    _uow.GetDbContext().AssignLeaveToEmployee.Include(x => x.LeaveReasonDetails).Where(x => x.IsDeleted == false && x.FinancialYearId == financialyearid && x.EmployeeId == EmployeeId).OrderByDescending(a=> a.LeaveId).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.AssignLeaveToEmployee.Include(x => x.LeaveReasonDetails)
					.Where(x => x.IsDeleted == false && x.FinancialYearId == financialyearid && x.EmployeeId == EmployeeId));
				var list = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result.OrderByDescending(a => a.LeaveId)
				);

				var assignleavelist = list.Select(x => new AssignLeaveToEmployeeModel
				{
					LeaveId = x.LeaveId,
					LeaveReasonId = x.LeaveReasonId,
					LeaveReasonName = x.LeaveReasonDetails.ReasonName,
					Unit = x.LeaveReasonDetails.Unit,
					AssignUnit = x.AssignUnit,
					BlanceLeave = (x.AssignUnit - (x.UsedLeaveUnit ?? 0)),
					FinancialYearId = x.FinancialYearId,
					Description = x.Description
				}).ToList();
				response.data.AssignLeaveToEmployeeList = assignleavelist;
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


		public async Task<APIResponse> EditEmployeeAssignLeave(EditAssignLeaveToEmployeeModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.AssignLeaveToEmployeeRepository.FindAsync(x => x.IsDeleted == false && x.LeaveId == model.LeaveId);
				if (existrecord != null)
				{
					existrecord.AssignUnit = model.AssignUnit;
					existrecord.ModifiedById = model.ModifiedById;
					existrecord.ModifiedDate = model.ModifiedDate;
					existrecord.IsDeleted = model.IsDeleted;
					await _uow.AssignLeaveToEmployeeRepository.UpdateAsyn(existrecord);
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


		//public async Task<APIResponse> GetAllActiveEmployeeForAttendance()
		//{
		//    APIResponse response = new APIResponse();
		//    try
		//    {


		//        //var attendancestatus = _uow.EmployeeAttendanceRepository.FindAsync(x => x.Date.Date == DateTime.Now.Date && x.LeaveReasonId != 3);
		//        //if (attendancestatus != null)
		//        //    response.data.AttendanceStatus = true;
		//        //else
		//        //    response.data.AttendanceStatus = false;

		//        ////var stopwatch = new Stopwatch();
		//        ////stopwatch.Start();
		//        ////var activelist = await Task.Run(() =>
		//        ////    //_uow.EmployeeDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.EmployeeTypeId == 2).Result.OrderBy(e=> e.EmployeeID).ToList()
		//        ////    _uow.GetDbContext().EmployeeDetail.Include(a => a.EmployeeAttendance)
		//        ////    .Where(x => x.IsDeleted == false && x.EmployeeTypeId == 2).OrderBy(e => e.EmployeeID).ToListAsync()
		//        ////);
		//        ////stopwatch.Stop();

		//        ////var stopwatch1 = new Stopwatch();
		//        ////stopwatch1.Start();
		//        //var queryResult = EF.CompileAsyncQuery(
		//        //  (ApplicationDbContext ctx) => ctx.EmployeeDetail.Include(a => a.EmployeeAttendance)
		//        //  .Where(e => e.IsDeleted == false && e.EmployeeTypeId == 2));
		//        //var activelist = await Task.Run(() =>
		//        //    queryResult(_uow.GetDbContext()).ToListAsync().Result.OrderBy(x => x.EmployeeID)
		//        //);
		//        ////stopwatch1.Stop();



		//        //var employeeactivelist = activelist.Select(x => new EmployeeAttendanceModel
		//        //{
		//        //    EmployeeId = x.EmployeeID,
		//        //    EmployeeName = x.EmployeeName,
		//        //    AttendanceTypeId = (int)AttendanceType.P,
		//        //    Date = DateTime.UtcNow,
		//        //    LeaveStatus = x.EmployeeAttendance?.Where(a => a.Date.Date == DateTime.UtcNow.Date).ToList().Count == 0 ? false : x.EmployeeAttendance.Where(e => e.AttendanceTypeId == (int)AttendanceType.L).ToList().Count > 0 ? true : false
		//        //    //InTime = DateTime.UtcNow,
		//        //    //OutTime = DateTime.UtcNow
		//        //}).ToList();

		//        //response.data.EmployeeAttendanceList = employeeactivelist;
		//        //response.StatusCode = StaticResource.successStatusCode;
		//        //response.Message = "Success";
		//    }
		//    catch (Exception ex)
		//    {
		//        response.StatusCode = StaticResource.failStatusCode;
		//        response.Message = ex.Message;
		//    }
		//    return response;
		//}

		public async Task<APIResponse> GetAllEmployeesAttendanceByDate(string SelectedDate, int OfficeId, bool attendancestatus)
		{
			APIResponse response = new APIResponse();
			try
			{
				//var attendancestatus = _uow.EmployeeAttendanceRepository.FindAllAsync(x => x.Date.Date == DateTime.Parse(SelectedDate).Date && x.AttendanceTypeId != 3).Result.FirstOrDefault();  //marked or not
				//if (attendancestatus != null)
				//	response.data.AttendanceStatus = true;  //attendance already marked
				//else
				//	response.data.AttendanceStatus = false; //not marked

				var empattendancelist = _uow.EmployeeAttendanceRepository.FindAllAsync(x => x.Date.Date == DateTime.Parse(SelectedDate).Date).Result.ToList();  //marked or not

				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.EmployeeDetail
				  .Include(x => x.EmployeeProfessionalDetail).Where(x => x.EmployeeProfessionalDetail.OfficeId == OfficeId && Convert.ToDateTime(x.EmployeeProfessionalDetail.HiredOn).Date <= DateTime.Parse(SelectedDate).Date)
				  .Include(a => a.EmployeeAttendance)
				  .Where(e => e.IsDeleted == false && e.EmployeeTypeId == 2));
				var activelist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result.OrderBy(x => x.EmployeeID)
				);

				IList<EmployeeAttendanceModel> empAttModel = new List<EmployeeAttendanceModel>();
				if (attendancestatus == true)
				{
					//response.data.AttendanceStatus = attendancestatus;
					empAttModel = activelist.SelectMany(x => x.EmployeeAttendance).Where(x => x.Date.Date == DateTime.Parse(SelectedDate).Date).Select(x => new EmployeeAttendanceModel
					{
						AttendanceId = x.AttendanceId,
						EmployeeId = x.EmployeeId,
						Date = x.Date,
						InTime = x.InTime,
						OutTime = x.OutTime,
						AttendanceTypeId = x.AttendanceTypeId,
						EmployeeName = x.EmployeeDetails.EmployeeName,
						LeaveStatus = x.AttendanceId == (int)AttendanceType.L ? true : false,
						OfficeId = x.EmployeeDetails.EmployeeProfessionalDetail.OfficeId
					}).ToList();


					if (empAttModel.Count == 0 || empAttModel == null)
						response.data.AttendanceStatus = false;  //attendance already marked
					else
						response.data.AttendanceStatus = true; //not marked

				}
				else
				{

					//               empAttModel = activelist.SelectMany(x => x.EmployeeAttendance.Except(empattendancelist)).Select(x =>  new EmployeeAttendanceModel
					//               {
					//                   AttendanceId = x.AttendanceId,
					//                   EmployeeId = x.EmployeeId,
					//                   Date = DateTime.Parse(SelectedDate),
					//                   //InTime = x.InTime,
					//                   //OutTime = x.OutTime,
					//                   AttendanceTypeId = (int)AttendanceType.P,
					//                   EmployeeName = x.EmployeeDetails.EmployeeName,
					//                   LeaveStatus = x.AttendanceId == (int)AttendanceType.L ? true : false,
					//                   OfficeId = x.EmployeeDetails.EmployeeProfessionalDetail.OfficeId
					//               }).ToList();

					//empAttModel = empAttModel.Except(list).ToList();
					int count = 0;
					//response.data.AttendanceStatus = attendancestatus;
					var existrecord = _uow.HolidayDetailsRepository.FindAllAsync(x => x.Date.Date == DateTime.Parse(SelectedDate).Date && x.OfficeId == OfficeId).Result.FirstOrDefault();
					if (existrecord == null)
					{
						foreach (var item in activelist)
						{
							EmployeeAttendanceModel obj = new EmployeeAttendanceModel();
							count = empattendancelist.Where(x => x.EmployeeId == item.EmployeeID).ToList().Count();
							if (count == 0)
							{
								obj.EmployeeId = item.EmployeeID;
								obj.EmployeeName = item.EmployeeName;
								obj.AttendanceTypeId = (int)AttendanceType.P;
								obj.Date = DateTime.UtcNow;
								obj.LeaveStatus = false;
								obj.OfficeId = OfficeId;
								empAttModel.Add(obj);
							}
						}
					}

					//empAttModel = activelist.Select(x => new EmployeeAttendanceModel
					//{
					//	EmployeeId = x.EmployeeID,
					//	EmployeeName = x.EmployeeName,
					//	AttendanceTypeId = (int)AttendanceType.P,
					//	Date = DateTime.UtcNow,
					//	LeaveStatus = false,
					//	OfficeId = x.EmployeeProfessionalDetail.OfficeId
					//	//InTime = DateTime.UtcNow,
					//	//OutTime = DateTime.UtcNow
					//}).ToList();

					if (empAttModel.Count == 0 || empAttModel == null)
						response.data.AttendanceStatus = false;  //attendance already marked
					else
						response.data.AttendanceStatus = true; //not marked

				}

				response.data.EmployeeAttendanceList = empAttModel;
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

		public async Task<APIResponse> EditEmployeeAttendanceByDate(EmployeeAttendanceModel model, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				TimeSpan? totalworkhour;
				var existrecord = await _uow.EmployeeAttendanceRepository.FindAsync(x => x.AttendanceId == model.AttendanceId);
				if (existrecord != null)
				{
					//var userdetails = await _uow.UserDetailsRepository.FindAsync(x => x.IsDeleted == false && x.AspNetUserId == userid);
					int? defaulthours = 0;
					var DefaultHourDetail = await _uow.PayrollMonthlyHourDetailRepository.FindAsync(x => x.IsDeleted == false && x.OfficeId == model.OfficeId);
					if (DefaultHourDetail != null)
					{
						defaulthours = DefaultHourDetail.Hours;
					}

					totalworkhour = model.OutTime - model.InTime;
					if (totalworkhour.ToString() == "00:00:00" || existrecord.AttendanceTypeId == (int)AttendanceType.A)
					{
						existrecord.AttendanceTypeId = 2;
						existrecord.InTime = model.Date;
						existrecord.OutTime = model.Date;
						totalworkhour = model.Date.Date - model.Date.Date;
					}
					if (Convert.ToInt32(totalworkhour.ToString().Substring(0, 2)) > defaulthours)
					{
						existrecord.TotalWorkTime = defaulthours.ToString();
						existrecord.HoverTimeHours = Convert.ToInt32(totalworkhour.ToString().Substring(0, 2)) - defaulthours;
					}
					else
					{
						existrecord.TotalWorkTime = totalworkhour.ToString().Substring(0, 2);
						existrecord.HoverTimeHours = 0;
					}


					existrecord.InTime = model.InTime;
					existrecord.OutTime = model.OutTime;
					existrecord.AttendanceTypeId = model.AttendanceTypeId;
					existrecord.ModifiedById = model.ModifiedById;
					existrecord.ModifiedDate = model.ModifiedDate;
					existrecord.IsDeleted = model.IsDeleted;
					await _uow.EmployeeAttendanceRepository.UpdateAsyn(existrecord);
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


		public async Task<APIResponse> AddEmployeeAttendanceDetails(List<EmployeeAttendanceModel> modellist, string UserId)
		{
			TimeSpan? totalworkhour;
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await Task.Run(() =>
					_uow.EmployeeAttendanceRepository.FindAllAsync(x => x.Date.Date == modellist[0].Date.Date).Result.ToList()
				);
				//if (existrecord.Count == 0)
				//{

				//var userdetails = await _uow.UserDetailsRepository.FindAsync(x => x.IsDeleted == false && x.AspNetUserId == UserId);
				int? defaulthours = 0;
				var DefaultHourDetail = await _uow.PayrollMonthlyHourDetailRepository.FindAsync(x => x.IsDeleted == false && x.OfficeId == modellist[0].OfficeId);
				if (DefaultHourDetail != null)
				{
					defaulthours = DefaultHourDetail.Hours;


					var financiallist = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);

					foreach (var list in modellist)
					{
						var isemprecord = existrecord.Where(x => x.EmployeeId == list.EmployeeId && x.Date.Date == list.Date.Date).ToList();
						if (isemprecord.Count == 0)
						{
							//intime = Convert.ToDateTime(list.InTime);
							//outtime = Convert.ToDateTime(list.OutTime);
							totalworkhour = list.OutTime - list.InTime;
							if (totalworkhour.ToString() == "00:00:00" || list.AttendanceTypeId == (int)AttendanceType.A)
							{
								list.AttendanceTypeId = 2;
								list.InTime = list.Date;
								list.OutTime = list.Date;
								//list.InTime = list.Date.Date;
								//list.OutTime = list.Date.Date;
								totalworkhour = list.Date.Date - list.Date.Date;
							}
							if (Convert.ToInt32(totalworkhour.ToString().Substring(0, 2)) > defaulthours)
							{
								list.TotalWorkTime = defaulthours.ToString();
								list.HoverTimeHours = Convert.ToInt32(totalworkhour.ToString().Substring(0, 2)) - defaulthours;
							}
							else
							{
								list.TotalWorkTime = totalworkhour.ToString().Substring(0, 2);
								list.HoverTimeHours = 0;
							}
							list.FinancialYearId = financiallist.FinancialYearId;
							list.CreatedById = UserId;
							list.CreatedDate = DateTime.UtcNow;
							list.IsDeleted = false;
							EmployeeAttendance obj = _mapper.Map<EmployeeAttendance>(list);
							await _uow.EmployeeAttendanceRepository.AddAsyn(obj);
							await _uow.SaveAsync();
						}
					}
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.failStatusCode;
					response.Message = "Daily payroll hours has been not defined";
				}
				//}
				//else
				//{
				// response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
				// response.Message = "Employee Attendance already done for this date.";
				//}

			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> GetEmployeeAttendanceDetails(int employeeid)
		{
			APIResponse response = new APIResponse();
			try
			{
				//var list = await Task.Run(() =>
				//    _uow.EmployeeAttendanceRepository.FindAllAsync(x=> x.IsDeleted== false && x.EmployeeId == employeeid).Result.ToList()
				//);

				var officedetails = await _uow.EmployeeProfessionalDetailRepository.FindAsync(x => x.EmployeeId == employeeid);

				int officeid = (int)officedetails.OfficeId;
				var financialyear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);


				var queryResult1 = EF.CompileAsyncQuery(
					(ApplicationDbContext ctx) => ctx.HolidayDetails
					.Where(x => x.IsDeleted == false && x.OfficeId == officeid && x.FinancialYearId == financialyear.FinancialYearId));
				var holidaylist = await Task.Run(() =>
					queryResult1(_uow.GetDbContext()).ToListAsync().Result.OrderBy(x => x.Date)
				);

				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.EmployeeAttendance.Where(x => x.EmployeeId == employeeid && x.FinancialYearId == financialyear.FinancialYearId));
				var list = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				//var queryResult1 = EF.CompileAsyncQuery(
				//	(ApplicationDbContext ctx) => ctx.HolidayDetails.Where(x => x.IsDeleted == false && x.OfficeId == officedetails.OfficeId && x.FinancialYearId == financialyear.FinancialYearId));
				//            var holidaylist = await Task.Run(() =>
				//                queryResult1(_uow.GetDbContext()).ToListAsync().Result
				//);






				var attendancelist = list.Select(x => new DisplayEmployeeAttendanceModel
				{
					attendanceId = x.AttendanceId,

					employeeID = x.EmployeeId,
					//InTime = x.InTime,
					//OutTime = x.OutTime,
					//TotalWorkTime = x.TotalWorkTime,
					//AttendanceTypeId = x.AttendanceTypeId,
					OverTimeHours = x.HoverTimeHours,
					text = x.AttendanceTypeId == (int)AttendanceType.P ? "P" : x.AttendanceTypeId == (int)AttendanceType.A ? "A" : x.AttendanceTypeId == (int)AttendanceType.L ? "L" : "",
					startDate = x.AttendanceTypeId == 1 ? x.InTime?.ToString() : x.InTime.Value.ToShortDateString(),
					endDate = x.AttendanceTypeId == 1 ? x.OutTime.ToString() : x.OutTime?.ToShortDateString()
				}).ToList();
				response.data.DisEmployeeAttendanceList = attendancelist;


				foreach (var hlist in holidaylist)
				{
					DisplayEmployeeAttendanceModel obj = new DisplayEmployeeAttendanceModel();
					obj.attendanceId = 0;
					obj.employeeID = employeeid;
					obj.OverTimeHours = 0;
					obj.text = "H";
					obj.startDate = hlist.Date.ToString();
					obj.endDate = hlist.Date.ToString();
					response.data.DisEmployeeAttendanceList.Add(obj);
				}

				//response.data.TotalAbsentDays = attendancelist.Count(x => x.AttendanceTypeId == 2);
				//response.data.TotalPresentDays = GetMonthDays(month, year);
				//response.data.TotalPresentDays = (response.data.TotalPresentDays - response.data.TotalAbsentDays);
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

		public int GetMonthDays(int month, int year)
		{
			int monthdays = 0;
			switch (month)
			{
				case 1:
					monthdays = 31;
					break;
				case 2:
					if (year % 4 == 0)
						monthdays = 29;
					else
						monthdays = 28;
					break;
				case 3:
					monthdays = 31;
					break;
				case 4:
					monthdays = 30;
					break;
				case 5:
					monthdays = 31;
					break;
				case 6:
					monthdays = 30;
					break;
				case 7:
					monthdays = 31;
					break;
				case 8:
					monthdays = 31;
					break;
				case 9:
					monthdays = 30;
					break;
				case 10:
					monthdays = 31;
					break;
				case 11:
					monthdays = 30;
					break;
				case 12:
					monthdays = 31;
					break;
			}
			return monthdays;
		}

		public async Task<APIResponse> AddEmployeeHealthDetail(EmployeeHealthInformationModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				EmployeeHealthDetail obj = _mapper.Map<EmployeeHealthDetail>(model);
				await _uow.EmployeeHealthDetailRepository.AddAsyn(obj);
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

		public async Task<APIResponse> EditEmployeeHealthDetail(EmployeeHealthInformationModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.EmployeeHealthDetailRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeId == model.EmployeeId);
				if (existrecord != null)
				{
					existrecord.BloodGroup = model.BloodGroup;
					existrecord.MedicalHistory = model.MedicalHistory;
					existrecord.SmokeAndDrink = model.SmokeAndDrink;
					existrecord.Insurance = model.Insurance;
					existrecord.MedicalInsurance = model.MedicalInsurance;
					existrecord.AllergicSubstance = model.AllergicSubstance;
					existrecord.MeasureDiseases = model.MeasureDieases;
					existrecord.FamilyHistory = model.FamilyHistory;
					existrecord.ModifiedById = model.ModifiedById;
					existrecord.ModifiedDate = model.ModifiedDate;
					existrecord.IsDeleted = model.IsDeleted;
					await _uow.EmployeeHealthDetailRepository.UpdateAsyn(existrecord);
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

		public async Task<APIResponse> GetAllEmployeeHealthDetailByEmployeeId(int employeeid)
		{
			APIResponse response = new APIResponse();
			try
			{
				//var list = await Task.Run(() =>
				//    _uow.EmployeeHealthDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.EmployeeId == employeeid).Result.ToList()
				//);

				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.EmployeeHealthDetail.Where(x => x.EmployeeId == employeeid));
				var list = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var emphealthlist = list.Select(x => new EmployeeHealthInformationModel
				{
					HealthInfoId = x.HealthInfoId,
					EmployeeId = x.EmployeeId,
					BloodGroup = x.BloodGroup,
					MedicalHistory = x.MedicalHistory,
					SmokeAndDrink = x.SmokeAndDrink,
					Insurance = x.Insurance,
					MedicalInsurance = x.MedicalInsurance,
					MeasureDieases = x.MeasureDiseases,
					AllergicSubstance = x.AllergicSubstance,
					FamilyHistory = x.FamilyHistory
				}).ToList();
				response.data.EmployeeHealthInfoList = emphealthlist;
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

		public async Task<APIResponse> ChangeEmployeeImage(ChangeEmployeeImage model)
		{
			APIResponse response = new APIResponse();
			try
			{
				//byte[] filepathBase64 = Encoding.UTF8.GetBytes(model.EmployeeImage);
				string[] str = model.EmployeeImage.Split(",");
				byte[] filepath = Convert.FromBase64String(str[1]);

				string ex = str[0].Split("/")[1].Split(";")[0];

				string guidname = Guid.NewGuid().ToString();
				//byte[] filepath = Encoding.UTF8.GetBytes(str[1]);
				string filename = guidname + "." + ex;
				var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;


				File.WriteAllBytes(@"Documents/" + filename, filepath);

				var employeeinfo = await _uow.EmployeeDetailRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeID == model.EmployeeId);
				if (employeeinfo != null)
				{
					employeeinfo.DocumentGUID = guidname;
					// For Employee Image
					employeeinfo.DocumentType = 2;
					employeeinfo.Extension = "." + ex;
					employeeinfo.EmployeePhoto = null;
					employeeinfo.ModifiedById = model.ModifiedById;
					employeeinfo.ModifiedDate = model.ModifiedDate;
					employeeinfo.IsDeleted = false;
					await _uow.EmployeeDetailRepository.UpdateAsyn(employeeinfo);
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

		//Apply Employee Leave Service
		public async Task<APIResponse> AddEmployeeApplyLeaveDetail(List<EmployeeApplyLeaveModel> model, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				//int isexist = 0;
				//DateTime fromdate = model.FromDate.Date;
				//DateTime todate = model.FromDate.Date.AddDays(1);
				//while (fromdate < model.ToDate)
				//{
				//    var isexistrecord = await _uow.EmployeeApplyLeaveRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeId == model.EmployeeId && x.FromDate.Date == fromdate.Date && x.ToDate.Date == todate.Date);
				//    if (isexistrecord != null)
				//    {
				//        response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
				//        response.Message = "Leave has been already applied during for date.";
				//        isexist = 1;
				//        break;
				//    }
				//    fromdate = fromdate.AddDays(1);
				//    todate = todate.AddDays(1);
				//}

				//if (isexist != 1)
				//{
				//    EmployeeApplyLeave obj = _mapper.Map<EmployeeApplyLeave>(model);
				//    await _uow.EmployeeApplyLeaveRepository.AddAsyn(obj);
				//    await _uow.SaveAsync();

				//    var existrecord = await _uow.AssignLeaveToEmployeeRepository.FindAsync(x => x.EmployeeId == model.EmployeeId && x.LeaveReasonId == model.LeaveReasonId);
				//    if (existrecord != null)
				//    {
				//        model.ToDate = model.ToDate.Date;
				//        model.FromDate = model.FromDate.Date;
				//        TimeSpan difference = model.ToDate.Date - model.FromDate.Date;
				//        int days = (int)difference.TotalDays;
				//        int? usedleaveunit = existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit;
				//        existrecord.UsedLeaveUnit = usedleaveunit + days;
				//        existrecord.ModifiedById = model.ModifiedById;
				//        existrecord.ModifiedDate = model.ModifiedDate;
				//        existrecord.IsDeleted = false;
				//        await _uow.AssignLeaveToEmployeeRepository.UpdateAsyn(existrecord);
				//    }                
				//}


				var existrecord = await _uow.AssignLeaveToEmployeeRepository.FindAsync(x => x.EmployeeId == model[0].EmployeeId && x.LeaveReasonId == model[0].LeaveReasonId);

				int balanceleave = (int)existrecord.AssignUnit - (int)(existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit);
				if (balanceleave >= model.Count)
				{
					List<EmployeeApplyLeave> applyleavelist = new List<EmployeeApplyLeave>();
					foreach (var list in model)
					{
						EmployeeApplyLeave obj = new EmployeeApplyLeave();
						obj.EmployeeId = list.EmployeeId;
						obj.FromDate = list.FromDate;
						obj.ToDate = list.ToDate;
						obj.LeaveReasonId = list.LeaveReasonId;
						obj.Remarks = list.Remarks;
						obj.FinancialYearId = existrecord.FinancialYearId;
						obj.CreatedById = userid;
						obj.CreatedDate = DateTime.UtcNow;
						obj.IsDeleted = false;
						applyleavelist.Add(obj);
					}
					await _uow.GetDbContext().EmployeeApplyLeave.AddRangeAsync(applyleavelist);
					await _uow.SaveAsync();

					if (existrecord != null)
					{
						int? usedleaveunit = existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit;
						existrecord.UsedLeaveUnit = usedleaveunit + model.Count;
						existrecord.ModifiedById = userid;
						existrecord.ModifiedDate = DateTime.UtcNow;
						existrecord.IsDeleted = false;
						await _uow.AssignLeaveToEmployeeRepository.UpdateAsyn(existrecord);
					}
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.failStatusCode;
					response.Message = "More than leave cannot apply from balance leave.";
				}

			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> EditEmployeeApplyLeaveDetail(EmployeeApplyLeaveModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				int isexist = 0;
				DateTime fromdate = model.FromDate.Date;
				DateTime todate = model.FromDate.Date.AddDays(1);
				while (fromdate < model.ToDate)
				{
					var isexistrecord = await _uow.EmployeeApplyLeaveRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeId == model.EmployeeId && x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Reject && x.FromDate.Date == fromdate.Date && x.ToDate.Date == todate.Date);
					if (isexistrecord != null)
					{
						response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
						response.Message = "Leave has been already applied during for date.";
						isexist = 1;
						break;
					}
					fromdate = fromdate.AddDays(1);
					todate = todate.AddDays(1);
				}

				if (isexist != 1)
				{
					var existrecord = await _uow.EmployeeApplyLeaveRepository.FindAsync(x => x.IsDeleted == false && x.ApplyLeaveId == model.ApplyLeaveId);
					if (existrecord != null)
					{
						existrecord.FromDate = model.FromDate;
						existrecord.ToDate = model.ToDate;
						//existrecord.LeaveTypeId = model.LeaveTypeId;
						existrecord.LeaveReasonId = model.LeaveReasonId;
						existrecord.Remarks = model.Remarks;
						existrecord.ModifiedById = model.ModifiedById;
						existrecord.ModifiedDate = model.ModifiedDate;
						existrecord.IsDeleted = model.IsDeleted;
						await _uow.EmployeeApplyLeaveRepository.UpdateAsyn(existrecord);
						response.StatusCode = StaticResource.successStatusCode;
						response.Message = "Success";
					}

					var isexistrecord = await _uow.AssignLeaveToEmployeeRepository.FindAsync(x => x.EmployeeId == model.EmployeeId && x.LeaveReasonId == model.LeaveReasonId);
					if (isexistrecord != null)
					{
						model.ToDate = model.ToDate;
						model.FromDate = model.FromDate;
						TimeSpan difference = model.ToDate.Date - model.FromDate.Date;
						int days = (int)difference.TotalDays;
						int? usedleaveunit = isexistrecord.UsedLeaveUnit == null ? 0 : isexistrecord.UsedLeaveUnit;
						isexistrecord.UsedLeaveUnit = usedleaveunit + days;
						isexistrecord.ModifiedById = model.ModifiedById;
						isexistrecord.ModifiedDate = model.ModifiedDate;
						isexistrecord.IsDeleted = false;
						await _uow.AssignLeaveToEmployeeRepository.UpdateAsyn(isexistrecord);
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

		public async Task<APIResponse> GetEmployeeApplyLeaveDetailById(int employeeid)
		{
			APIResponse response = new APIResponse();
			try
			{
				//var list = await Task.Run(() =>
				//    _uow.GetDbContext().EmployeeApplyLeave.Include(x=> x.LeaveReasonDetails).Where(a=> a.IsDeleted == false && a.EmployeeId == employeeid).OrderByDescending(o=> o.ApplyLeaveId).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
				  (ApplicationDbContext ctx) => ctx.EmployeeApplyLeave.Include(x => x.LeaveReasonDetails).Where(x => x.EmployeeId == employeeid));
				var list = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result.OrderByDescending(o => o.ApplyLeaveId)
				);

				var empapplyleavelist = list.Select(x => new EmployeeApplyLeaveModel
				{
					ApplyLeaveId = x.ApplyLeaveId,
					EmployeeId = x.EmployeeId,
					FromDate = x.FromDate,
					ToDate = x.ToDate,
					LeaveReasonId = x.LeaveReasonId,
					LeaveReasonName = x.LeaveReasonDetails?.ReasonName ?? null,
					ApplyLeaveStatusId = x.ApplyLeaveStatusId,
					ApplyLeaveStatus = x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Approve ? "Approve" : x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Reject ? "Reject" : "",
					Remarks = x.Remarks
				}).ToList();
				response.data.EmployeeApplyLeaveList = empapplyleavelist;
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

		public async Task<APIResponse> GetAllEmployeeApplyLeaveList(int officeid)
		{
			APIResponse response = new APIResponse();
			try
			{
				//var list = await Task.Run(() =>
				//    _uow.GetDbContext().EmployeeApplyLeave.Include(x => x.LeaveReasonDetails)
				//    .Include(e=> e.EmployeeDetails)
				//    .Where(a => a.IsDeleted == false && a.ApplyLeaveStatusId == null).OrderByDescending(o => o.EmployeeId).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
				 (ApplicationDbContext ctx) => ctx.EmployeeApplyLeave.Include(x => x.LeaveReasonDetails)
					.Include(e => e.EmployeeDetails)
					.Include(p => p.EmployeeDetails.EmployeeProfessionalDetail).Where(p => p.EmployeeDetails.EmployeeProfessionalDetail.OfficeId == officeid)
					.Where(a => a.ApplyLeaveStatusId == null));
				var list = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result.OrderByDescending(o => o.EmployeeId)
				);

				var empapplyleavelist = list.Select(x => new EmployeeApplyLeaveModel
				{
					ApplyLeaveId = x.ApplyLeaveId,
					EmployeeId = x.EmployeeId,
					EmployeeName = x.EmployeeDetails?.EmployeeName ?? "",
					FromDate = x.FromDate,
					ToDate = x.ToDate,
					LeaveReasonId = x.LeaveReasonId,
					LeaveReasonName = x.LeaveReasonDetails?.ReasonName ?? null,
					ApplyLeaveStatusId = x.ApplyLeaveStatusId,
					//ApplyLeaveStatus = x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Approve ? "Approve" : x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Reject ? "Reject" : "",
					Remarks = x.Remarks
				}).ToList();
				response.data.EmployeeApplyLeaveList = empapplyleavelist;
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

		public async Task<APIResponse> ApproveEmployeeLeave(List<ApproveLeaveModel> model, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var financialyear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);
				foreach (var item in model)
				{
					var existleaverecord = await _uow.EmployeeApplyLeaveRepository.FindAsync(x => x.IsDeleted == false && x.ApplyLeaveId == item.ApplyLeaveId);
					if (existleaverecord != null)
					{
						existleaverecord.ApplyLeaveStatusId = (int)ApplyLeaveStatus.Approve;
						existleaverecord.ModifiedById = userid;
						existleaverecord.ModifiedDate = DateTime.UtcNow;
						existleaverecord.IsDeleted = false;
						await _uow.EmployeeApplyLeaveRepository.UpdateAsyn(existleaverecord);

						for (DateTime i = existleaverecord.FromDate.Date; i <= existleaverecord.ToDate.Date; i = i.AddDays(1))
						{
							var existrecord = await _uow.EmployeeAttendanceRepository.FindAsync(x => x.Date.Date == i.Date.Date && x.EmployeeId == existleaverecord.EmployeeId);
							if (existrecord == null)
							{
								EmployeeAttendance attendance = new EmployeeAttendance();
								attendance.EmployeeId = existleaverecord.EmployeeId;
								attendance.AttendanceTypeId = 3;
								attendance.LeaveReasonId = existleaverecord.LeaveReasonId;
								attendance.Date = i.Date;
								attendance.InTime = i.Date.Date;
								attendance.OutTime = i.Date.Date;
								attendance.TotalWorkTime = "00";
								attendance.HoverTimeHours = 0;
								attendance.FinancialYearId = financialyear.FinancialYearId;
								attendance.CreatedById = userid;
								attendance.CreatedDate = DateTime.UtcNow;
								attendance.IsDeleted = false;
								await _uow.EmployeeAttendanceRepository.AddAsyn(attendance);
								await _uow.SaveAsync();
							}
							else
							{
								existrecord.AttendanceTypeId = (int)AttendanceType.L;
								existrecord.LeaveReasonId = existleaverecord.LeaveReasonId;
								existrecord.TotalWorkTime = "00";
								existrecord.HoverTimeHours = 0;
								existrecord.ModifiedById = userid;
								existrecord.ModifiedDate = DateTime.UtcNow;
								await _uow.EmployeeAttendanceRepository.UpdateAsyn(existrecord);
							}
						}

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

		public async Task<APIResponse> RejectEmployeeLeave(List<ApproveLeaveModel> model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				foreach (var item in model)
				{
					var existleaverecord = await _uow.EmployeeApplyLeaveRepository.FindAsync(x => x.IsDeleted == false && x.ApplyLeaveId == item.ApplyLeaveId);
					if (existleaverecord != null)
					{
						existleaverecord.ApplyLeaveStatusId = (int)ApplyLeaveStatus.Reject;
						existleaverecord.ModifiedById = UserId;
						existleaverecord.ModifiedDate = DateTime.UtcNow;
						existleaverecord.IsDeleted = false;
						await _uow.EmployeeApplyLeaveRepository.UpdateAsyn(existleaverecord);

						var existassignempleave = await _uow.AssignLeaveToEmployeeRepository.FindAsync(x => x.IsDeleted == false && x.LeaveReasonId == existleaverecord.LeaveReasonId && x.EmployeeId == existleaverecord.EmployeeId);
						if (existassignempleave != null)
						{
							//TimeSpan daysdifference = existleaverecord.ToDate.Date - existleaverecord.FromDate.Date;
							//int days = (int)daysdifference.TotalDays;

							existassignempleave.UsedLeaveUnit = existassignempleave.UsedLeaveUnit - 1;
							existassignempleave.ModifiedById = UserId;
							existassignempleave.ModifiedDate = DateTime.UtcNow;
							existassignempleave.IsDeleted = false;
							await _uow.AssignLeaveToEmployeeRepository.UpdateAsyn(existassignempleave);
						}
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

		public async Task<APIResponse> DeleteApplyEmployeeLeave(int applyleaveid, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existleaverecord = await _uow.EmployeeApplyLeaveRepository.FindAsync(x => x.IsDeleted == false && x.ApplyLeaveId == applyleaveid);
				if (existleaverecord != null)
				{
					existleaverecord.ModifiedById = userid;
					existleaverecord.ModifiedDate = DateTime.UtcNow;
					existleaverecord.IsDeleted = true;
					await _uow.EmployeeApplyLeaveRepository.UpdateAsyn(existleaverecord);

					var existassignempleave = await _uow.AssignLeaveToEmployeeRepository.FindAsync(x => x.IsDeleted == false && x.LeaveReasonId == existleaverecord.LeaveReasonId && x.EmployeeId == existleaverecord.EmployeeId);
					if (existassignempleave != null)
					{
						//TimeSpan daysdifference = existleaverecord.ToDate.Date - existleaverecord.FromDate.Date;
						//int days = (int)daysdifference.TotalDays;

						existassignempleave.UsedLeaveUnit = existassignempleave.UsedLeaveUnit - 1;
						existassignempleave.ModifiedById = userid;
						existassignempleave.ModifiedDate = DateTime.UtcNow;
						existassignempleave.IsDeleted = false;
						await _uow.AssignLeaveToEmployeeRepository.UpdateAsyn(existassignempleave);
					}
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
		//EOF: Apply Employee Leave Service


		//Alpit
		#region Get All Job Grade
		public async Task<APIResponse> GetAllJobGrade()
		{
			APIResponse response = new APIResponse();
			try
			{
				//var jobGradelist = await Task.Run(() =>
				//    _uow.JobGradeRepository.FindAllAsync(x=> x.IsDeleted == false).Result.ToList()
				//);

				var queryResult = EF.CompileAsyncQuery(
				(ApplicationDbContext ctx) => ctx.JobGrade.Where(x => x.IsDeleted == false));
				var jobGradelist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var jobGradeDetailsList = jobGradelist.Select(x => new JobGradeModel
				{
					GradeId = x.GradeId,
					GradeName = x.GradeName,

				}).ToList();
				response.StatusCode = StaticResource.successStatusCode;
				response.data.JobGradeList = jobGradeDetailsList;
				response.Message = "Success";
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
			}
			return response;
		}
		#endregion

		//Alpit
		#region Add Job Grade Detail
		public async Task<APIResponse> AddJobGradeDetail(JobGradeModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				JobGrade obj = _mapper.Map<JobGrade>(model);
				await _uow.JobGradeRepository.AddAsyn(obj);
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
		#endregion

		//Alpit
		#region Edit Job Grade Detail
		public async Task<APIResponse> EditJobGradeDetail(JobGradeModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.JobGradeRepository.FindAsync(x => x.GradeId == model.GradeId);
				if (existrecord != null)
				{
					existrecord.GradeName = model.GradeName;
					existrecord.ModifiedById = model.ModifiedById;
					existrecord.ModifiedDate = model.ModifiedDate;
					existrecord.IsDeleted = false;
					await _uow.JobGradeRepository.UpdateAsyn(existrecord);
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
		#endregion


		//Alpit
		#region Get All Prospective Employee by ProfessionId
		public async Task<APIResponse> GetProspectiveEmployeesByProfessionId(int ProfessionId)
		{
			APIResponse response = new APIResponse();
			try
			{
				// var emplist = await Task.Run(() =>
				//    _uow.GetDbContext().EmployeeDetail.Include(p => p.ProfessionDetails)
				//    .Include(i=> i.InterviewScheduleDetails)
				//    .Where(x => x.EmployeeTypeId == 1 && x.ProfessionId == ProfessionId && x.IsDeleted == false).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
			   (ApplicationDbContext ctx) => ctx.EmployeeDetail.Include(p => p.ProfessionDetails)
				   .Include(i => i.InterviewScheduleDetails).Where(x => x.EmployeeTypeId == 1 && x.ProfessionId == ProfessionId));
				var emplist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				emplist = emplist.Where(x => x.InterviewScheduleDetails.Count == 0).ToList();

				var employeelist = emplist.Select(x => new InterviewScheduleForProspectiveEmployeeModel
				{
					EmployeeId = x.EmployeeID,
					EmployeeCode = x.EmployeeCode,
					EmployeeName = x.EmployeeName,
					PhoneNo = x.Phone,
					Resume = x.Resume,
					ProfessionId = x.ProfessionId,
					ProfessionName = x.ProfessionDetails?.ProfessionName ?? "",
				}).ToList();
				response.data.ISFPEmployeeList = employeelist;
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
		#endregion

		//Alpit
		#region Get All Scheduled Prospective Employee 
		public async Task<APIResponse> GetAllScheduledProspectiveEmployee()
		{
			APIResponse response = new APIResponse();
			try
			{
				// var emplist = await Task.Run(() =>
				//    _uow.GetDbContext().InterviewScheduleDetails
				//    .Include(i => i.EmployeeDetails)
				//    .Include(i => i.JobHiringDetails).Where(x=> x.IsDeleted == false).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
					(ApplicationDbContext ctx) => ctx.InterviewScheduleDetails
				   .Include(i => i.EmployeeDetails)
				   .Include(i => i.JobHiringDetails).Where(x => x.IsDeleted == false));
				var emplist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);


				var employeelist = emplist.Select(x => new ScheduleCandidateModel
				{
					EmployeeId = x.EmployeeId,
					EmployeeName = x.EmployeeDetails.EmployeeName,
					PhoneNo = x.EmployeeDetails.Phone,
					JobId = x.JobHiringDetails.JobId,
					JobCode = x.JobHiringDetails.JobCode

				}).ToList();
				response.data.ScheduledProspectiveEmployee = employeelist;
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
		#endregion

		//Alpit
		#region Get all Scheduled Employee
		public async Task<APIResponse> GetAllScheduledEmployeeList()
		{
			APIResponse response = new APIResponse();
			try
			{
				// var emplist = await Task.Run(() =>
				//    _uow.GetDbContext().InterviewScheduleDetails
				//    .Include(i => i.JobGrade)
				//    .Include(i => i.EmployeeDetails)               
				//    .Include(i => i.JobHiringDetails).Where(x => x.IsDeleted == false && (x.Approval1 == null || x.Approval2 == null)).ToListAsync()
				//);

				var queryResult = EF.CompileAsyncQuery(
					(ApplicationDbContext ctx) => ctx.InterviewScheduleDetails
				   .Include(i => i.JobGrade)
				   .Include(i => i.EmployeeDetails)
				   .Include(i => i.JobHiringDetails).Where(x => (x.Approval1 == null || x.Approval2 == null)));
				var emplist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var approvalForGeneralAssemblylist = emplist.Select(x => new InterviewScheduleModel
				{
					EmployeeId = x.EmployeeId,
					EmployeeName = x.EmployeeDetails.EmployeeName,
					PhoneNo = x.EmployeeDetails.Phone,
					JobId = x.JobHiringDetails.JobId,
					JobCode = x.JobHiringDetails.JobCode,
					GradeId = x.GradeId,
					GradeName = x.JobGrade.GradeName,
					Approval1 = x.Approval1,    //For General Assembly (Grade == 1)
					Approval2 = x.Approval2,    //For Director (Grade > 1)
					Approval3 = x.Approval3,    //For General Admin (Approval1 == true || Approval2 == true)
					Approval4 = x.Approval4     //For Field Office (Approval1 == true || Approval2 == true)
				}).ToList();
				response.data.InterviewScheduleGeneralAssemblylist = approvalForGeneralAssemblylist.Where(x => x.GradeId == 1 && x.Approval1 == null).ToList();
				response.data.InterviewScheduleDirectorlist = approvalForGeneralAssemblylist.Where(x => x.GradeId > 1 && x.Approval2 == null).ToList();
				response.data.InterviewScheduleGeneralAdminlist = approvalForGeneralAssemblylist.Where(x => x.IsDeleted == false && ((x.Approval1 == true || x.Approval2 == true) && x.Approval3 == null)).ToList();
				response.data.InterviewScheduleFieldOfficelist = approvalForGeneralAssemblylist.Where(x => x.IsDeleted == false && ((x.Approval1 == true || x.Approval2 == true) && x.Approval4 == null)).ToList();
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
		#endregion

		//Alpit
		#region Get All Scheduled Prospective Employee 
		public async Task<APIResponse> GetAllApprovedEmployeeList()
		{
			APIResponse response = new APIResponse();
			try
			{

				//var emplist = await Task.Run(() =>
				//_uow.GetDbContext().InterviewScheduleDetails
				//.Include(i => i.JobGrade)
				//.Include(i => i.EmployeeDetails)
				//.Include(i => i.JobHiringDetails).Where(x => x.IsDeleted == false && (x.Approval3 == true && x.Approval4 ==true)).ToListAsync());

				var queryResult = EF.CompileAsyncQuery(
					(ApplicationDbContext ctx) => ctx.InterviewScheduleDetails
					.Include(i => i.JobGrade)
					.Include(i => i.EmployeeDetails)
					.Include(i => i.JobHiringDetails).Where(x => (x.Approval3 == true && x.Approval4 == true)));
				var emplist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var approvedEmployeeList = emplist.Select(x => new InterviewScheduleModel
				{
					EmployeeId = x.EmployeeId,
					EmployeeName = x.EmployeeDetails.EmployeeName,
					PhoneNo = x.EmployeeDetails.Phone,
					JobId = x.JobHiringDetails.JobId,
					JobCode = x.JobHiringDetails.JobCode,
					GradeId = x.GradeId,
					GradeName = x.JobGrade.GradeName,
					Approval1 = x.Approval1,    //For General Assembly (Grade == 1)
					Approval2 = x.Approval2,    //For Director (Grade > 1)
					Approval3 = x.Approval3,    //For General Admin (Approval1 == true || Approval2 == true)
					Approval4 = x.Approval4     //For Field Office (Approval1 == true || Approval2 == true)

				}).ToList();
				response.data.InterviewApprovedEmployeeList = approvedEmployeeList;
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
		#endregion

		#region Interview Approvals
		public async Task<APIResponse> InterviewApprovals(List<InterviewScheduleModel> model, int approvalId, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				foreach (var list in model)
				{
					var approvalRecord = await _uow.InterviewScheduleDetailsRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeId == list.EmployeeId);
					if (approvalRecord != null)
					{
						if (approvalId == 1)
							approvalRecord.Approval1 = list.Approval1;
						else if (approvalId == 2)
							approvalRecord.Approval2 = list.Approval2;
						else if (approvalId == 3)
							approvalRecord.Approval3 = list.Approval3;
						else if (approvalId == 4)
							approvalRecord.Approval4 = list.Approval4;

						approvalRecord.ModifiedById = userid;
						approvalRecord.ModifiedDate = DateTime.UtcNow;
						approvalRecord.IsDeleted = false;
						await _uow.InterviewScheduleDetailsRepository.UpdateAsyn(approvalRecord);
						response.StatusCode = StaticResource.successStatusCode;
						response.Message = "Success";
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
		#endregion

		public async Task<APIResponse> GetAllEmployeeMonthlyPayrollList(int officeid, int currencyid, int month, int year, int paymentType)
		{
			APIResponse response = new APIResponse();
			try
			{
				var userdetailslist = (from ept in await _uow.EmployeePaymentTypeRepository.GetAllAsyn()
									   join emp in await _uow.EmployeeMonthlyPayrollRepository.GetAllAsyn() on ept.EmployeeID equals emp.EmployeeID
									   join es in await _uow.SalaryHeadDetailsRepository.GetAllAsyn() on emp.SalaryHeadId equals es.SalaryHeadId
									   where ept.FinancialYearDate.Date.Month == month && ept.FinancialYearDate.Date.Year == year && emp.Date.Date.Month == month && emp.Date.Date.Year == year && ept.OfficeId == officeid && ept.PaymentType == paymentType
									   select new EmployeeMonthlyPayrollModel
									   {
										   EmployeeId = ept.EmployeeID,
										   EmployeeName = ept.EmployeeName,
										   PaymentType = ept.PaymentType,
										   WorkingDays = ept.WorkingDays,
										   PresentDays = ept.PresentDays,
										   AbsentDays = ept.AbsentDays,
										   LeaveDays = ept.LeaveDays,
										   TotalWorkHours = ept.TotalWorkHours,
										   HourlyRate = ept.HourlyRate,
										   TotalGeneralAmount = ept.TotalGeneralAmount,
										   TotalAllowance = ept.TotalAllowance,
										   TotalDeduction = ept.TotalDeduction,
										   GrossSalary = ept.GrossSalary,
										   OverTimeHours = ept.OverTimeHours,
										   SalaryHeadId = emp.SalaryHeadId,
										   MonthlyAmount = emp.MonthlyAmount,
										   CurrencyId = emp.CurrencyId,
										   SalaryHead = es.HeadName,
										   HeadTypeId = es.HeadTypeId,
										   SalaryHeadType = es.HeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : es.HeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : es.HeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : "",
										   PayrollId = emp.MonthlyPayrollId,
										   IsApproved = ept.IsApproved,
										   PensionRate = ept.PensionRate,
										   SalaryTax = ept.SalaryTax,
										   PensionAmount = Math.Round(Convert.ToDouble(ept.PensionAmount), 2),
										   NetSalary = ept.GrossSalary - ept.TotalDeduction
									   }).ToList();



				response.data.EmployeeMonthlyPayrolllist = userdetailslist.GroupBy(u => u.EmployeeId).Select(x => new EmployeeMonthlyPayrollModel
				{
					employeepayrolllist = userdetailslist.Where(p => p.EmployeeId == x.FirstOrDefault().EmployeeId).Select(e => new EmployeePayrollModel
					{
						PayrollId = e.PayrollId,
						SalaryHeadId = e.SalaryHeadId,
						EmployeeId = e.EmployeeId,
						CurrencyId = e.CurrencyId,
						PaymentType = e.PaymentType,
						MonthlyAmount = e.MonthlyAmount,
						PensionRate = e.PensionRate,
						SalaryHead = e.SalaryHead,
						HeadTypeId = e.HeadTypeId,
						SalaryHeadType = e.SalaryHeadType
					}).ToList(),
					CurrencyId = x.FirstOrDefault().CurrencyId,

					EmployeeId = x.FirstOrDefault().EmployeeId,
					EmployeeName = x.FirstOrDefault().EmployeeName,
					PaymentType = x.FirstOrDefault().PaymentType,
					WorkingDays = x.FirstOrDefault().WorkingDays,
					PresentDays = x.FirstOrDefault().PresentDays,
					AbsentDays = x.FirstOrDefault().AbsentDays,
					LeaveDays = x.FirstOrDefault().LeaveDays,
					TotalWorkHours = x.FirstOrDefault().TotalWorkHours,
					OverTimeHours = x.FirstOrDefault().OverTimeHours,
					TotalGeneralAmount = x.FirstOrDefault().TotalGeneralAmount,
					TotalAllowance = x.FirstOrDefault().TotalAllowance,
					TotalDeduction = x.FirstOrDefault().TotalDeduction,
					GrossSalary = x.FirstOrDefault().GrossSalary,
					NetSalary = x.FirstOrDefault().GrossSalary - x.FirstOrDefault().TotalDeduction,
					PensionAmount = x.FirstOrDefault().PensionAmount,
					SalaryTax = x.FirstOrDefault().SalaryTax,
					IsApproved = x.FirstOrDefault().IsApproved
				}).ToList();


				if (userdetailslist.Count == 0)
				{
					var queryResult = EF.CompileAsyncQuery(
					(ApplicationDbContext ctx) => ctx.EmployeeAttendance
					.Include(e => e.EmployeeDetails).Include(e => e.EmployeeDetails.EmployeeProfessionalDetail).Where(x => x.EmployeeDetails.EmployeeProfessionalDetail.OfficeId == officeid)
					.Include(e => e.EmployeeDetails.EmployeeSalaryDetails).Where(x => x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == paymentType)
					.Where(x => x.Date.Month == month && x.Date.Year == year).GroupBy(x => x.EmployeeId));
					var payrolllist = await Task.Run(() =>
						queryResult(_uow.GetDbContext()).ToListAsync().Result
					);
					//var pensionLst = _uow.EmployeePensionRateRepository.FindAsync(x => x.FinancialYearId == payrolllist[0].FirstOrDefault().FinancialYearId);					
					var pensionLst = _uow.EmployeePensionRateRepository.Find(x => x.IsDefault == true);
					int monthdays = GetMonthDays(month, year);
					int totalhours = 0, presentdays = 0, absentdays = 0, leavedays = 0, overtimehours = 0;
					List<EmployeeMonthlyPayrollModel> monthlypayrolllist = new List<EmployeeMonthlyPayrollModel>();
					for (int i = 0; i < payrolllist.Count; i++)
					{
						EmployeeMonthlyPayrollModel payrollmodel = new EmployeeMonthlyPayrollModel();
						totalhours = payrolllist[i].Sum(x => Convert.ToInt32(x.TotalWorkTime));
						overtimehours = payrolllist[i].Sum(x => Convert.ToInt32(x.HoverTimeHours));
						presentdays = payrolllist[i].Count(x => (x.AttendanceTypeId == (int)AttendanceType.P));
						absentdays = payrolllist[i].Count(x => (x.AttendanceTypeId == (int)AttendanceType.A));
						//leavedays = payrolllist[i].Count(x => (x.AttendanceTypeId == (int)AttendanceType.L));

						// WHEN CURRENCY ID IS SAME (WITHOUT CONVERSION)
						if (currencyid == payrolllist[i].FirstOrDefault().EmployeeDetails.EmployeeSalaryDetails.CurrencyId)
						{
							payrollmodel = payrolllist[i].Select(x => new EmployeeMonthlyPayrollModel
							{
								employeepayrolllist = _uow.GetDbContext().EmployeePayroll.Include(o => o.SalaryHeadDetails).Where(c => c.EmployeeID == x.EmployeeId).ToList().Select(e => new EmployeePayrollModel
								{
									PayrollId = e.PayrollId,
									SalaryHeadId = e.SalaryHeadId,
									EmployeeId = e.EmployeeID,
									CurrencyId = e.CurrencyId,
									PaymentType = e.PaymentType,
									MonthlyAmount = e.MonthlyAmount,
									PensionRate = pensionLst.PensionRate,
									SalaryHead = e.SalaryHeadDetails.HeadName,
									HeadTypeId = e.SalaryHeadDetails.HeadTypeId,
									SalaryHeadType = e.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : e.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : e.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : ""
								}).ToList(),
								EmployeeId = x.EmployeeId,
								EmployeeName = x.EmployeeDetails.EmployeeName,
								PaymentType = x.EmployeeDetails.EmployeeSalaryDetails.PaymentType,
								WorkingDays = (presentdays + absentdays + leavedays),
								PresentDays = presentdays,
								AbsentDays = absentdays,
								LeaveDays = leavedays,
								TotalWorkHours = totalhours,
								OverTimeHours = overtimehours,
								TotalGeneralAmount = x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount,
								TotalAllowance = x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance,
								GrossSalary = x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == 1 ? Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)), 2) : Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)), 2),

								SalaryTax = SalaryCalculate(x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == 1 ? Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)), 2) : Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)), 2), 1),

								PensionAmount = x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == 1 ?
								Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)
								* pensionLst.PensionRate), 2)
								:
								Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)
								* pensionLst.PensionRate), 2),

								// Deduction Starts
								TotalDeduction = x.EmployeeDetails.EmployeeSalaryDetails.Totalduduction + (x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == 1 ?
								Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)
								* pensionLst.PensionRate) + SalaryCalculate(Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)), 2), 1), 2)
								:
								Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)
								* pensionLst.PensionRate) + SalaryCalculate(Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)), 2), 1), 2)),
								// Deduction Ends

								// Net Salary Starts
								NetSalary = x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == 1 ? Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount
								+ x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance) - (x.EmployeeDetails.EmployeeSalaryDetails.Totalduduction + Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)
								* pensionLst.PensionRate) + SalaryCalculate(Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)), 2), 1), 2))), 2)
								:
								Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance) - (x.EmployeeDetails.EmployeeSalaryDetails.Totalduduction + Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)
								* pensionLst.PensionRate) + SalaryCalculate(Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)), 2), 1), 2))), 2),
								// Net Salary End
								IsApproved = false
							}).FirstOrDefault();
						}
						// TO CONVERT AMOUNT USING CONVERSION RATE
						else
						{
							var conversionRate = _uow.ExchangeRateRepository.FindAll(x => x.ToCurrency == currencyid && x.FromCurrency == payrolllist[i].FirstOrDefault().EmployeeDetails.EmployeeSalaryDetails.CurrencyId).OrderByDescending(x => x.Date).FirstOrDefault();
							payrollmodel = payrolllist[i].Select(x => new EmployeeMonthlyPayrollModel
							{
								employeepayrolllist = _uow.GetDbContext().EmployeePayroll.Include(o => o.SalaryHeadDetails).Where(c => c.EmployeeID == x.EmployeeId).ToList().Select(e => new EmployeePayrollModel
								{
									PayrollId = e.PayrollId,
									SalaryHeadId = e.SalaryHeadId,
									EmployeeId = e.EmployeeID,
									CurrencyId = e.CurrencyId,
									PaymentType = e.PaymentType,
									MonthlyAmount = e.MonthlyAmount * conversionRate.Rate,
									PensionRate = pensionLst.PensionRate,
									SalaryHead = e.SalaryHeadDetails.HeadName,
									HeadTypeId = e.SalaryHeadDetails.HeadTypeId,
									SalaryHeadType = e.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : e.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : e.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : ""
								}).ToList(),
								EmployeeId = x.EmployeeId,
								EmployeeName = x.EmployeeDetails.EmployeeName,
								PaymentType = x.EmployeeDetails.EmployeeSalaryDetails.PaymentType,
								WorkingDays = (presentdays + absentdays + leavedays),
								PresentDays = presentdays,
								AbsentDays = absentdays,
								LeaveDays = leavedays,
								TotalWorkHours = totalhours,
								OverTimeHours = overtimehours,
								TotalGeneralAmount = x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * conversionRate.Rate,
								TotalAllowance = x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance * conversionRate.Rate,
								GrossSalary = x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == 1 ? Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance) * conversionRate.Rate), 2)
								:
								Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours * conversionRate.Rate) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance * conversionRate.Rate)), 2),

								SalaryTax = SalaryCalculate(x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == 1 ? Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance) * conversionRate.Rate), 2) :
								Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours * conversionRate.Rate) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance * conversionRate.Rate)), 2), conversionRate.Rate),

								PensionAmount = x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == 1 ?
								Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance) * conversionRate.Rate) * pensionLst.PensionRate), 2)
								:
								Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours * conversionRate.Rate) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance * conversionRate.Rate) * pensionLst.PensionRate), 2),

								// Deduction Starts
								TotalDeduction = (x.EmployeeDetails.EmployeeSalaryDetails.Totalduduction * conversionRate.Rate) + (x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == 1 ?
								Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * conversionRate.Rate + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance * conversionRate.Rate)
								* pensionLst.PensionRate), 2)
								:
								Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours * conversionRate.Rate) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance * conversionRate.Rate)
								* pensionLst.PensionRate), 2)),
								// Deduction Ends

								// Net Salary Starts
								NetSalary = x.EmployeeDetails.EmployeeSalaryDetails.PaymentType == 1 ? Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * conversionRate.Rate
								+ x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance * conversionRate.Rate) - (x.EmployeeDetails.EmployeeSalaryDetails.Totalduduction * conversionRate.Rate + Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * conversionRate.Rate + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance * conversionRate.Rate)
								* pensionLst.PensionRate) + SalaryCalculate(Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)), 2), conversionRate.Rate), 2))), 2)
								:
								Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours * conversionRate.Rate) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance * conversionRate.Rate) - (x.EmployeeDetails.EmployeeSalaryDetails.Totalduduction * conversionRate.Rate + Math.Round(Convert.ToDouble(((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount * totalhours * conversionRate.Rate) + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance * conversionRate.Rate)
								* pensionLst.PensionRate) + SalaryCalculate(Math.Round(Convert.ToDouble((x.EmployeeDetails.EmployeeSalaryDetails.TotalGeneralAmount + x.EmployeeDetails.EmployeeSalaryDetails.TotalAllowance)), 2), conversionRate.Rate), 2))), 2),
								// Net Salary End
								IsApproved = false
							}).FirstOrDefault();
						}
						monthlypayrolllist.Add(payrollmodel);
					}
					response.data.EmployeeMonthlyPayrolllist = monthlypayrolllist.OrderBy(x => x.EmployeeId).ToList();
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

		public double SalaryCalculate(double grossSalary, double exchangeRate)
		{
			double salaryTax = 0;
			if (grossSalary < 5000)
			{
				salaryTax = 0;
			}
			else if (grossSalary >= 5000 && grossSalary < 12500)
			{
				salaryTax = (grossSalary * exchangeRate - 5000) * 2 / 100;
			}
			else if (grossSalary >= 12500 && grossSalary < 100000)
			{
				salaryTax = (((grossSalary * exchangeRate - 12500) * 10 / 100) + 150) / exchangeRate;
			}
			else
			{
				salaryTax = ((((grossSalary * exchangeRate) - 100000) * 20 / 100) + 8900) / exchangeRate;
			}
			return salaryTax;
		}

		public async Task<APIResponse> AddHolidayDetails(HolidayDetailsModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var financialyear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);
				if (model.HolidayType == (int)HolidayType.REPEATWEEKLYDAY)
				{
					List<HolidayWeeklyDetails> holidayweeklylist = new List<HolidayWeeklyDetails>();
					foreach (var hlist in model.RepeatWeeklyDay)
					{
						HolidayWeeklyDetails list = new HolidayWeeklyDetails();
						list.Day = hlist.Day;
						list.OfficeId = (int)model.OfficeId;
						list.FinancialYearId = financialyear.FinancialYearId;
						list.CreatedById = model.CreatedById;
						list.CreatedDate = model.CreatedDate;
						list.IsDeleted = false;
						holidayweeklylist.Add(list);
					}
					await _uow.GetDbContext().HolidayWeeklyDetails.AddRangeAsync(holidayweeklylist);
					await _uow.SaveAsync();

					List<HolidayDetails> holidaylist = new List<HolidayDetails>();
					for (DateTime todaydate = financialyear.StartDate; todaydate <= financialyear.EndDate;)
					{
						HolidayDetails holiday = new HolidayDetails();
						string day = todaydate.DayOfWeek.ToString();
						foreach (var list in model.RepeatWeeklyDay)
						{
							if (list.Day == day)
							{
								holiday.HolidayName = "Weekly Off";
								holiday.Date = todaydate;
								holiday.FinancialYearId = financialyear.FinancialYearId;
								holiday.OfficeId = model.OfficeId;
								holiday.HolidayType = model.HolidayType;
								holiday.CreatedById = model.CreatedById;
								holiday.CreatedDate = model.CreatedDate;
								holidaylist.Add(holiday);
							}
						}
						todaydate = todaydate.AddDays(1);
					}
					await _uow.GetDbContext().HolidayDetails.AddRangeAsync(holidaylist);
					await _uow.SaveAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					var existrecord = await _uow.HolidayDetailsRepository.FindAsync(x => x.Date.Date == model.Date.Date);
					if (existrecord == null)
					{
						model.FinancialYearId = financialyear.FinancialYearId;
						model.HolidayType = model.HolidayType;
						HolidayDetails obj = _mapper.Map<HolidayDetails>(model);
						await _uow.HolidayDetailsRepository.AddAsyn(obj);
						await _uow.SaveAsync();

						//List<EmployeeAttendance> employeeAttendancelist = new List<EmployeeAttendance>();
						//var activeemployeelist = await _uow.EmployeeDetailRepository.FindAllAsync(x => x.EmployeeTypeId == (int)EmployeeTypeStatus.Active);
						//foreach (var list in activeemployeelist)
						//{
						//    EmployeeAttendance employee = new EmployeeAttendance();
						//    employee.AttendanceTypeId = (int)AttendanceType.H;
						//    employee.EmployeeId = list.EmployeeID;
						//    employee.InTime = model.Date;
						//    employee.OutTime = model.Date;
						//    employee.TotalWorkTime = "00";
						//    employee.HoverTimeHours = 0;
						//    employee.CreatedById = model.CreatedById;
						//    employee.CreatedDate = model.CreatedDate;
						//    employee.IsDeleted = false;
						//    employee.HolidayId = obj.HolidayId;
						//    employee.FinancialYearId = financiallist.FinancialYearId;
						//    employeeAttendancelist.Add(employee);
						//}
						//await _uow.GetDbContext().EmployeeAttendance.AddRangeAsync(employeeAttendancelist);
						//await _uow.SaveAsync();
						response.StatusCode = StaticResource.successStatusCode;
						response.Message = "Success";
					}
					else
					{
						response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
						response.Message = "Holiday Details already exist for this date.";
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

		public async Task<APIResponse> EditHolidayDetails(HolidayDetailsModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var financialyear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);
				if (model.HolidayType == (int)HolidayType.REPEATWEEKLYDAY)
				{
					var existrecord = await _uow.HolidayWeeklyDetailRepository.FindAllAsync(x => x.IsDeleted == false && x.OfficeId == model.OfficeId && x.FinancialYearId == financialyear.FinancialYearId);
					_uow.GetDbContext().RemoveRange(existrecord);
					await _uow.SaveAsync();

					List<HolidayWeeklyDetails> holidayweeklylist = new List<HolidayWeeklyDetails>();
					foreach (var hweeklylist in model.RepeatWeeklyDay)
					{
						HolidayWeeklyDetails list = new HolidayWeeklyDetails();
						list.Day = hweeklylist.Day;
						list.OfficeId = (int)model.OfficeId;
						list.FinancialYearId = financialyear.FinancialYearId;
						list.ModifiedById = model.ModifiedById;
						list.ModifiedDate = model.ModifiedDate;
						list.IsDeleted = false;
						holidayweeklylist.Add(list);
					}
					await _uow.GetDbContext().HolidayWeeklyDetails.AddRangeAsync(holidayweeklylist);
					await _uow.SaveAsync();

					List<HolidayDetails> holidaylist = new List<HolidayDetails>();
					var hlist = await _uow.HolidayDetailsRepository.FindAllAsync(x => x.IsDeleted == false && x.FinancialYearId == financialyear.FinancialYearId && x.OfficeId == model.OfficeId && x.HolidayType == (int)HolidayType.REPEATWEEKLYDAY);
					foreach (var h in hlist)
					{
						h.IsDeleted = true;
						h.ModifiedById = model.ModifiedById;
						h.ModifiedDate = model.ModifiedDate;
						holidaylist.Add(h);
					}
					//_uow.GetDbContext().HolidayDetails.UpdateRange(holidaylist);
					_uow.GetDbContext().RemoveRange(holidaylist);
					await _uow.SaveAsync();

					List<HolidayDetails> holidaylist1 = new List<HolidayDetails>();
					for (DateTime todaydate = financialyear.StartDate; todaydate <= financialyear.EndDate;)
					{
						HolidayDetails holiday = new HolidayDetails();
						string day = todaydate.DayOfWeek.ToString();
						foreach (var list in model.RepeatWeeklyDay)
						{
							if (list.Day == day)
							{
								holiday.HolidayName = "Weekly Off";
								holiday.Date = todaydate;
								holiday.FinancialYearId = financialyear.FinancialYearId;
								holiday.OfficeId = model.OfficeId;
								holiday.HolidayType = model.HolidayType;
								holiday.CreatedById = model.CreatedById;
								holiday.CreatedDate = model.CreatedDate;
								holidaylist1.Add(holiday);
							}
						}
						todaydate = todaydate.AddDays(1);
					}
					await _uow.GetDbContext().HolidayDetails.AddRangeAsync(holidaylist1);
					await _uow.SaveAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					var existrecord = await _uow.HolidayDetailsRepository.FindAsync(x => x.HolidayId == model.HolidayId);
					if (existrecord != null)
					{
						existrecord.HolidayName = model.HolidayName;
						existrecord.Remarks = model.Remarks;
						existrecord.ModifiedById = model.ModifiedById;
						existrecord.ModifiedDate = model.ModifiedDate;
						existrecord.IsDeleted = false;
						await _uow.HolidayDetailsRepository.UpdateAsyn(existrecord);
						response.StatusCode = StaticResource.successStatusCode;
						response.Message = "Success";
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

		public async Task<APIResponse> GetAllHolidayDetails(int officeid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var financialyear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);

				var queryResult = EF.CompileAsyncQuery(
					(ApplicationDbContext ctx) => ctx.HolidayDetails
					.Where(x => x.IsDeleted == false && x.OfficeId == officeid && x.FinancialYearId == financialyear.FinancialYearId));
				var holidaylist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				response.data.HolidayDetailsList = holidaylist;
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

		public async Task<APIResponse> GetAllHolidayWeeklyDetails(int officeid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var financialyear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);

				var queryResult = EF.CompileAsyncQuery(
					(ApplicationDbContext ctx) => ctx.HolidayWeeklyDetails
					.Where(x => x.IsDeleted == false && x.OfficeId == officeid && x.FinancialYearId == financialyear.FinancialYearId));
				var holidayweeklylist = await Task.Run(() =>
					queryResult(_uow.GetDbContext()).ToListAsync().Result
				);

				var list = holidayweeklylist.Select(x => new RepeatWeeklyDay
				{
					Day = x.Day
				}).ToList();

				response.data.HolidayWeeklyDetailsList = list;
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

		public async Task<APIResponse> DeleteHolidayDetails(long holidayid, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existrecord = await _uow.HolidayDetailsRepository.FindAsync(x => x.HolidayId == holidayid);
				if (existrecord != null)
				{
					existrecord.IsDeleted = true;
					existrecord.ModifiedById = userid;
					existrecord.ModifiedDate = DateTime.UtcNow;
					await _uow.HolidayDetailsRepository.UpdateAsyn(existrecord);

					var existempattendance = await _uow.EmployeeAttendanceRepository.FindAllAsync(x => x.HolidayId == holidayid);
					if (existempattendance.Count > 0)
					{
						List<EmployeeAttendance> empattendancelist = new List<EmployeeAttendance>();
						foreach (var list in existempattendance)
						{
							EmployeeAttendance empattendance = new EmployeeAttendance();
							empattendance.EmployeeId = list.EmployeeId;
							empattendance.AttendanceId = list.AttendanceId;
							empattendance.IsDeleted = false;
							empattendance.ModifiedById = userid;
							empattendance.ModifiedDate = DateTime.UtcNow;
							empattendancelist.Add(empattendance);
						}
						_uow.GetDbContext().EmployeeAttendance.UpdateRange(empattendancelist);
					}
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

		public async Task<APIResponse> GetAllDisableCalanderDate(int employeeid, int OfficeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var financialyear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);
				var list = await _uow.EmployeeApplyLeaveRepository.FindAllAsync(x => x.IsDeleted == false && x.EmployeeId == employeeid && x.ApplyLeaveStatusId != (int)ApplyLeaveStatus.Reject && x.FinancialYearId == financialyear.FinancialYearId);
				var applyleavelist = list.Select(x => new ApplyLeaveModel
				{
					Date = x.FromDate
				}).ToList();
				response.data.ApplyLeaveList = applyleavelist;
				var holidaylist = await _uow.HolidayDetailsRepository.FindAllAsync(x => x.OfficeId == OfficeId && x.FinancialYearId == financialyear.FinancialYearId);
				foreach (var list1 in holidaylist)
				{
					ApplyLeaveModel obj = new ApplyLeaveModel();
					obj.Date = list1.Date;
					response.data.ApplyLeaveList.Add(obj);
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

		public async Task<APIResponse> GetAllDateforDisableCalenderDate(int OfficeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var financialyear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);
				var list = await _uow.HolidayDetailsRepository.FindAllAsync(x => x.OfficeId == OfficeId && x.FinancialYearId == financialyear.FinancialYearId);
				var applyleavelist = list.Select(x => new ApplyLeaveModel
				{
					Date = x.Date
				}).ToList();
				response.data.ApplyLeaveList = applyleavelist;
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


		public async Task<APIResponse> MonthlyEmployeeAttendanceReport(MonthlyEmployeeAttendanceReportModel obj)
		{
			APIResponse response = new APIResponse();
			try
			{
				TimeSpan officeintime, officeouttime;
				TimeSpan intime, outtime;
				List<MonthlyEmployeeAttendanceModel> empmonthlyattendancelist = new List<MonthlyEmployeeAttendanceModel>();
				//var officeid = await _uow.EmployeeProfessionalDetailRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeId == employeeid);

				var payrolltimelist = await _uow.PayrollMonthlyHourDetailRepository.FindAsync(x => x.IsDeleted == false && x.OfficeId == obj.OfficeId);
				string time = Convert.ToDateTime(payrolltimelist.InTime).ToString("hh:mm");
				officeintime = Convert.ToDateTime(time).TimeOfDay;

				time = Convert.ToDateTime(payrolltimelist.OutTime).ToString("hh:mm");
				officeouttime = Convert.ToDateTime(time).TimeOfDay;

				var emplist = await _uow.EmployeeAttendanceRepository.FindAllAsync(x => x.EmployeeId == obj.employeeid && x.Date.Year == obj.year && x.Date.Month == obj.month);
				List<EmployeeAttendance> empattencancelist = null;
				int monthdays = GetMonthDays(obj.month, obj.year);

				var holidaylist = await _uow.HolidayDetailsRepository.FindAllAsync(x => x.Date.Year == obj.year && x.Date.Month == obj.month && x.OfficeId == obj.OfficeId);
				List<HolidayDetails> holiday = null;

				for (int i = 1; i <= monthdays; i++)
				{
					empattencancelist = emplist.Where(x => x.Date.Day == i).ToList();
					MonthlyEmployeeAttendanceModel model = new MonthlyEmployeeAttendanceModel();
					if (empattencancelist.Count > 0)
					{
						model.Date = i + "/" + obj.month + "/" + obj.year;
						model.InTime = empattencancelist[0].AttendanceTypeId == (int)AttendanceType.P ? Convert.ToDateTime(empattencancelist[0].InTime).ToString("HH:mm") : "NA";
						model.OutTime = model.InTime != "NA" ? Convert.ToDateTime(empattencancelist[0].OutTime).ToString("HH:mm") : "NA";
						model.AttendanceType = empattencancelist[0].AttendanceTypeId == (int)AttendanceType.P ? "P" : empattencancelist[0].AttendanceTypeId == (int)AttendanceType.A ? "A" : empattencancelist[0].AttendanceTypeId == (int)AttendanceType.L ? "L" : empattencancelist[0].AttendanceTypeId == (int)AttendanceType.H ? "H" : "NA";
						model.Hours = empattencancelist[0].TotalWorkTime + "" + "h";
						model.OverTimeHours = empattencancelist[0].HoverTimeHours.ToString() + "" + "h";
						if (empattencancelist[0].AttendanceTypeId == 1)
						{
							time = Convert.ToDateTime(empattencancelist[0].InTime).ToString("HH:mm");
							intime = Convert.ToDateTime(time).TimeOfDay;
							if (officeintime >= intime)
							{
								model.LateArrival = "NA";
							}
							else
							{
								model.LateArrival = (intime - officeintime).ToString();
							}

							time = Convert.ToDateTime(empattencancelist[0].OutTime).ToString("HH:mm");
							outtime = Convert.ToDateTime(time).TimeOfDay;
							if (officeouttime >= outtime)
								model.EarlyOut = (officeouttime - outtime).ToString();
							else
								model.EarlyOut = "NA";
						}
						else
						{
							model.LateArrival = "NA";
							model.EarlyOut = "NA";
						}
					}
					else
					{
						holiday = holidaylist.Where(x => x.Date.Day == i).ToList();
						if (holiday.Count > 0)
						{
							model.Date = i + "/" + obj.month + "/" + obj.year;
							model.InTime = "NA";
							model.OutTime = "NA";
							model.AttendanceType = holiday[0].HolidayName;
							model.Hours = "NA";
							model.OverTimeHours = "NA";
							model.LateArrival = "NA";
							model.EarlyOut = "NA";
						}
						else
						{
							model.Date = i + "/" + obj.month + "/" + obj.year;
							model.InTime = "NA";
							model.OutTime = "NA";
							model.AttendanceType = "NA";
							model.Hours = "NA";
							model.OverTimeHours = "NA";
							model.LateArrival = "NA";
							model.EarlyOut = "NA";
						}
					}
					empmonthlyattendancelist.Add(model);
				}
				response.data.MonthlyEmployeeAttendanceList = empmonthlyattendancelist;
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

		public async Task<APIResponse> OnPostExport()
		{
			string sWebRootFolder = _env.WebRootPath;
			string sFileName = @"PensionReport.xlsx";
			//string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
			FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
			var memory = new MemoryStream();
			IWorkbook workbook;
			using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open, FileAccess.Read))
			{

				workbook = new XSSFWorkbook(fs);
				ISheet excelSheet = workbook.GetSheet("Pensions");

				IFont boldFont = workbook.CreateFont();
				boldFont.Boldweight = (short)FontBoldWeight.Bold;
				boldFont.Color = (short)FontColor.Red;
				ICellStyle boldStyle = workbook.CreateCellStyle();
				boldStyle.SetFont(boldFont);
				//boldStyle.BorderBottom = CellBorderType.MEDIUM;
				//boldStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index;
				//boldStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;

				//ICellStyle style = workbook.CreateCellStyle();
				//style.FillForegroundColor = (short)FontColor.Red;
				////style.setFillPattern(CellStyle.SOLID_FOREGROUND);
				//Font font = workbook.createFont();
				//font.setColor(IndexedColors.RED.getIndex());
				//style.setFont(font);

				IRow row = excelSheet.CreateRow(13);
				ICell cell = row.CreateCell(9);
				row.CreateCell(9).SetCellValue(2);
				cell.CellStyle = boldStyle;
				row.CreateCell(10).SetCellValue("ABC");
				row.CreateCell(11).SetCellValue(33);

				//row.CreateCell(0).SetCellValue("ID");
				//row.CreateCell(0).CellStyle = boldStyle;
				//row.CreateCell(1).CellStyle = boldStyle;
				//row.CreateCell(2).CellStyle = boldStyle;
				//row.CreateCell(1).SetCellValue("Name");
				//row.CreateCell(2).SetCellValue("Age");

				//row = excelSheet.CreateRow(6);
				//row.CreateCell(0).SetCellValue(1);
				//            row.CreateCell(1).SetCellValue("Kane Williamson");
				//            row.CreateCell(2).SetCellValue(29);

				//            row = excelSheet.CreateRow(7);
				//row.GetCell(0).SetCellValue(2);
				//row.GetCell(1).SetCellValue("ABC");
				//row.GetCell(2).SetCellValue(33);

				//row = excelSheet.CreateRow(8);
				//row.CreateCell(0).SetCellValue(3);
				//row.CreateCell(1).SetCellValue("Colin Munro");
				//row.CreateCell(2).SetCellValue(23);

				//workbook.Write(fs);
				//fs.Close();
			}
			using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
			{
				workbook.Write(stream);
			}
			//memory.Position = 0;
			//return  File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
			return null;
		}

		//	public async Task<APIResponse> EmployeesSalarySummary(EmployeeSummaryModel model)
		//	{
		//		APIResponse response = new APIResponse();
		//		try
		//		{
		//			double? TotalGrossSalary = 0.0;
		//			double? TotalAllowances = 0.0;
		//			double? TotalDeductions = 0.0;


		//			var empList = await _uow.GetDbContext().EmployeeDetail
		//					.Include(o => o.EmployeeSalaryDetails)
		//					.Include(p => p.EmployeeProfessionalDetail)
		//					.Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId)
		//					.ToListAsync();


		//			if (model.EmployeeTypeId == 2 && model.CurrencyId != null)
		//			{
		//				if (model.AllowanceId == null && model.DeductionId == null)
		//				{
		//					//var empList = await _uow.GetDbContext().EmployeeDetail
		//					//                         .Include(o => o.EmployeeSalaryDetails)
		//					//                         .Include(p => p.EmployeeProfessionalDetail)
		//					//                         .Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId)
		//					//                         .ToListAsync();


		//					var grossSalary = await _uow.GetDbContext().EmployeeDetail.Include(o => o.EmployeeSalaryDetails).Include(p => p.EmployeeProfessionalDetail).Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId && x.EmployeeSalaryDetails.CurrencyId == model.CurrencyId).ToListAsync();
		//					foreach (var item in grossSalary)
		//					{
		//						if (item.EmployeeSalaryDetails != null)
		//						{
		//							TotalGrossSalary += item.EmployeeSalaryDetails?.TotalGeneralAmount + item.EmployeeSalaryDetails?.TotalAllowance;
		//						}
		//					}
		//					if (empList.Count > 0)
		//					{
		//						var allowanceSalaryTypes = await _uow.SalaryHeadDetailsRepository.FindAllAsync(x => x.HeadTypeId == 1);
		//						foreach (var item in allowanceSalaryTypes)
		//						{
		//							var allowanceList = await _uow.GetDbContext().EmployeeDetail.Include(o => o.EmployeePayrollList).Include(p => p.EmployeeProfessionalDetail).Where(x => x.EmployeeTypeId == model.EmployeeTypeId && x.EmployeePayroll.CurrencyId == model.CurrencyId && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.IsDeleted == false && x.EmployeePayroll.SalaryHeadId == item.SalaryHeadId).ToListAsync();
		//							TotalAllowances += allowanceList.Sum(x => x.EmployeePayroll.MonthlyAmount);
		//						}
		//						var deductionSalaryTypes = await _uow.SalaryHeadDetailsRepository.FindAllAsync(x => x.HeadTypeId == 2);
		//						foreach (var item in deductionSalaryTypes)
		//						{
		//							var deductionList = await _uow.GetDbContext().EmployeeDetail.Include(o => o.EmployeePayrollList).Include(p => p.EmployeeProfessionalDetail).Where(x => x.EmployeeTypeId == model.EmployeeTypeId && x.EmployeePayroll.CurrencyId == model.CurrencyId && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.IsDeleted == false && x.EmployeePayroll.SalaryHeadId == item.SalaryHeadId).ToListAsync();
		//							TotalDeductions += deductionList.Sum(x => x.EmployeePayroll.MonthlyAmount);
		//						}
		//					}
		//					response.data.TotalEmployees = empList.Count();
		//					response.data.TotalAllowances = TotalAllowances;
		//					response.data.TotalDeductions = TotalDeductions;
		//					response.data.TotalGrossSalary = TotalGrossSalary;
		//				}
		//				else if (model.AllowanceId != null && model.DeductionId == null)
		//				{
		//					//var empList = await _uow.GetDbContext().EmployeeDetail
		//					//                         .Include(o => o.EmployeeSalaryDetails)
		//					//                         .Include(p => p.EmployeeProfessionalDetail)
		//					//                         .Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId)
		//					//                         .ToListAsync();

		//					var grossSalary = await _uow.GetDbContext().EmployeeDetail.Include(o => o.EmployeeSalaryDetails).Include(p => p.EmployeeProfessionalDetail).Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId && x.EmployeeSalaryDetails.CurrencyId == model.CurrencyId).ToListAsync();
		//					foreach (var item in grossSalary)
		//					{
		//						if (item.EmployeeSalaryDetails != null)
		//						{
		//							TotalGrossSalary += item.EmployeeSalaryDetails?.TotalGeneralAmount + item.EmployeeSalaryDetails?.TotalAllowance;
		//							TotalDeductions += item.EmployeeSalaryDetails?.Totalduduction;
		//						}
		//					}

		//					if (empList.Count > 0)
		//					{
		//						var allowanceList = await _uow.GetDbContext().EmployeeDetail.Include(o => o.EmployeePayrollList).Include(p => p.EmployeeProfessionalDetail).Where(x => x.EmployeeTypeId == model.EmployeeTypeId && x.EmployeePayroll.CurrencyId == model.CurrencyId && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.IsDeleted == false && x.EmployeePayroll.SalaryHeadId == model.AllowanceId).ToListAsync();
		//						TotalAllowances += allowanceList.Sum(x => x.EmployeePayroll.MonthlyAmount);
		//					}

		//					response.data.TotalEmployees = empList.Count();
		//					response.data.TotalAllowances = TotalAllowances;
		//					response.data.TotalDeductions = TotalDeductions;
		//					response.data.TotalGrossSalary = TotalGrossSalary;
		//				}
		//				else if (model.AllowanceId == null && model.DeductionId != null)
		//				{
		//					//var empList = await _uow.GetDbContext().EmployeeDetail
		//					//                         .Include(o => o.EmployeeSalaryDetails)
		//					//                         .Include(p => p.EmployeeProfessionalDetail)
		//					//                         .Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId)
		//					//                         .ToListAsync();

		//					var grossSalary = await _uow.GetDbContext().EmployeeDetail.Include(o => o.EmployeeSalaryDetails).Include(p => p.EmployeeProfessionalDetail).Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId && x.EmployeeSalaryDetails.CurrencyId == model.CurrencyId).ToListAsync();
		//					foreach (var item in grossSalary)
		//					{
		//						if (item.EmployeeSalaryDetails != null)
		//						{
		//							TotalGrossSalary += item.EmployeeSalaryDetails?.TotalGeneralAmount + item.EmployeeSalaryDetails?.TotalAllowance;
		//							TotalAllowances += item.EmployeeSalaryDetails?.TotalAllowance;
		//						}
		//					}

		//					if (empList.Count > 0)
		//					{
		//						var deductionList = await _uow.GetDbContext().EmployeeDetail.Include(o => o.EmployeePayrollList).Include(p => p.EmployeeProfessionalDetail).Where(x => x.EmployeeTypeId == model.EmployeeTypeId && x.EmployeePayroll.CurrencyId == model.CurrencyId && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.IsDeleted == false && x.EmployeePayroll.SalaryHeadId == model.DeductionId).ToListAsync();
		//						TotalDeductions += deductionList.Sum(x => x.EmployeePayroll.MonthlyAmount);
		//					}
		//					response.data.TotalEmployees = empList.Count();
		//					response.data.TotalAllowances = TotalAllowances;
		//					response.data.TotalDeductions = TotalDeductions;
		//					response.data.TotalGrossSalary = TotalGrossSalary;
		//				}
		//				else
		//				{
		//					//var empList = await _uow.GetDbContext().EmployeeDetail
		//					//                         .Include(o => o.EmployeeSalaryDetails)
		//					//                         .Include(p => p.EmployeeProfessionalDetail)
		//					//                         .Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId)
		//					//                         .ToListAsync();

		//					var grossSalary = await _uow.GetDbContext().EmployeeDetail.Include(o => o.EmployeeSalaryDetails).Include(p => p.EmployeeProfessionalDetail).Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId && x.EmployeeSalaryDetails.CurrencyId == model.CurrencyId).ToListAsync();
		//					foreach (var item in grossSalary)
		//					{
		//						if (item.EmployeeSalaryDetails != null)
		//						{
		//							TotalGrossSalary += item.EmployeeSalaryDetails?.TotalGeneralAmount + item.EmployeeSalaryDetails?.TotalAllowance;
		//						}
		//					}
		//					if (empList.Count > 0)
		//					{
		//						var allowanceList = await _uow.GetDbContext().EmployeeDetail.Include(o => o.EmployeePayrollList).Include(p => p.EmployeeProfessionalDetail).Where(x => x.EmployeeTypeId == model.EmployeeTypeId && x.EmployeePayrollList.CurrencyId == model.CurrencyId && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.IsDeleted == false && x.EmployeePayrollList.SalaryHeadId == model.AllowanceId).ToListAsync();
		//						TotalAllowances += allowanceList.Sum(x => x.EmployeePayroll.MonthlyAmount);

		//						var deductionList = await _uow.GetDbContext().EmployeeDetail.Include(o => o.EmployeePayrollList).Include(p => p.EmployeeProfessionalDetail).Where(x => x.EmployeeTypeId == model.EmployeeTypeId && x.EmployeePayrollList.CurrencyId == model.CurrencyId && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.IsDeleted == false && x.EmployeePayrollList..SalaryHeadId == model.DeductionId).ToListAsync();
		//						TotalDeductions += deductionList.Sum(x => x.EmployeePayroll.MonthlyAmount);
		//					}
		//					response.data.TotalEmployees = empList.Count();
		//					response.data.TotalAllowances = TotalAllowances;
		//					response.data.TotalDeductions = TotalDeductions;
		//					response.data.TotalGrossSalary = TotalGrossSalary;
		//				}
		//			}
		//			else
		//			{
		//				//var empList = await _uow.GetDbContext().EmployeeDetail
		//				//    .Include(o => o.EmployeeSalaryDetails)
		//				//    .Include(p => p.EmployeeProfessionalDetail)
		//				//    .Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId)
		//				//    .ToListAsync();

		//				response.data.TotalEmployees = empList.Count();
		//				response.data.TotalAllowances = TotalAllowances;
		//				response.data.TotalDeductions = TotalDeductions;
		//				response.data.TotalGrossSalary = TotalGrossSalary;
		//			}
		//			//}
		//			//           else
		//			//           {
		//			//               //var empFinalList = await _uow.GetDbContext().EmployeeDetail
		//			//               //    .Include(o => o.EmployeeSalaryDetails)
		//			//               //    .Include(p => p.EmployeeProfessionalDetail)
		//			//               //    .Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId)
		//			//               //    .ToListAsync();  

		//			//response.data.TotalEmployees = empList.Count();     
		//			//response.data.TotalAllowances = TotalAllowances;
		//			//response.data.TotalDeductions = TotalDeductions;
		//			//response.data.TotalGrossSalary = TotalGrossSalary;
		//			//           }
		//			response.StatusCode = StaticResource.successStatusCode;
		//			response.Message = "Success";


		//		}

		//		catch (Exception ex)
		//		{
		//			response.StatusCode = StaticResource.failStatusCode;
		//			response.Message = ex.Message;
		//		}
		//		return response;
		//	}
		//}

		public async Task<APIResponse> EmployeesSalarySummary(EmployeeSummaryModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				double? TotalGrossSalary = 0.0;
				double? TotalAllowances = 0.0;
				double? TotalDeductions = 0.0;
				//string CurrencyName;
				var empList = await _uow.GetDbContext().EmployeeDetail
						.Include(o => o.EmployeeSalaryDetails)
						.Include(c => c.EmployeeSalaryDetails.CurrencyDetails)
						.Include(x => x.EmployeePayrollList)
						.Include(p => p.EmployeeProfessionalDetail)
						.Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == model.OfficeId && x.EmployeeTypeId == model.EmployeeTypeId)
						.ToListAsync();

				//var empCountList = await _uow.GetDbContext().EmployeePaymentType.Include(o => o.EmployeeDetails).Where(x => x.EmployeeDetails.EmployeeTypeId == model.EmployeeTypeId && x.IsApproved == true).ToListAsync();
				var empCountList = await _uow.EmployeeProfessionalDetailRepository.FindAllAsync(x => x.EmployeeTypeId == model.EmployeeTypeId && x.OfficeId == model.OfficeId);
				response.data.TotalEmployees = empCountList.Count;
				//response.data.TotalEmployees = 
				var userdetailslist = (from ept in await _uow.EmployeePaymentTypeRepository.GetAllAsyn()
									   join emp in await _uow.EmployeeMonthlyPayrollRepository.GetAllAsyn() on ept.EmployeeID equals emp.EmployeeID
									   join es in await _uow.SalaryHeadDetailsRepository.GetAllAsyn() on emp.SalaryHeadId equals es.SalaryHeadId
									   join emd in await _uow.EmployeeDetailRepository.GetAllAsyn() on ept.EmployeeID equals emd.EmployeeID
									   where ept.OfficeId == model.OfficeId && ept.FinancialYearDate.Date.Year == model.Year && emd.EmployeeTypeId == model.EmployeeTypeId && ept.IsApproved == true
									   select new EmployeeMonthlyPayrollModel
									   {
										   EmployeeId = ept.EmployeeID,
										   EmployeeName = ept.EmployeeName,
										   PaymentType = ept.PaymentType,
										   WorkingDays = ept.WorkingDays,
										   PresentDays = ept.PresentDays,
										   AbsentDays = ept.AbsentDays,
										   LeaveDays = ept.LeaveDays,
										   TotalWorkHours = ept.TotalWorkHours,
										   HourlyRate = ept.HourlyRate,
										   TotalGeneralAmount = ept.TotalGeneralAmount,
										   TotalAllowance = ept.TotalAllowance,
										   TotalDeduction = ept.TotalDeduction,
										   GrossSalary = ept.GrossSalary,
										   OverTimeHours = ept.OverTimeHours,
										   SalaryHeadId = emp.SalaryHeadId,
										   MonthlyAmount = emp.MonthlyAmount,
										   CurrencyId = emp.CurrencyId,
										   SalaryHead = es.HeadName,
										   HeadTypeId = es.HeadTypeId,
										   SalaryHeadType = es.HeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : es.HeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : es.HeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : "",
										   IsApproved = ept.IsApproved,
										   Date = ept.FinancialYearDate
										   //PensionRate = emp.EmployeeDetails.EmployeeSalaryDetails.PensionRate									   
									   }).ToList();
				var allowanceList = userdetailslist;
				var deductionList = userdetailslist;
				//var empList = userdetailslist;
				if (model.Month != null)
				{
					userdetailslist = userdetailslist.Where(x => x.Date.Month == model.Month).ToList();
				}
				if (model.AllowanceId != null)
				{
					allowanceList = userdetailslist.Where(x => x.SalaryHeadId == model.AllowanceId && x.HeadTypeId == (int)SalaryHeadType.ALLOWANCE).ToList();
				}
				if (model.DeductionId != null)
				{
					deductionList = userdetailslist.Where(x => x.SalaryHeadId == model.DeductionId && x.HeadTypeId == (int)SalaryHeadType.DEDUCTION).ToList();
				}

				// Record Type - Single Currency

				if (model.RecordType == (int)RECORDTYPE.SINGLE)
				{
					var empList1 = userdetailslist.GroupBy(x => x.CurrencyId).Select(group => new EmployeeSummaryDetails
					{
						Currency = group.FirstOrDefault().CurrencyId,
						TotalGrossSalary = group.FirstOrDefault().GrossSalary,
						TotalAllowance = model.AllowanceId == null ? group.FirstOrDefault().TotalAllowance : allowanceList.Where(x => x.SalaryHeadId == model.AllowanceId).Sum(x => x.MonthlyAmount),
						TotalDeduction = model.DeductionId == null ? group.FirstOrDefault().TotalDeduction : deductionList.Where(x => x.SalaryHeadId == model.DeductionId).Sum(x => x.MonthlyAmount)
					}).ToList();
					response.data.EmployeeSummaryDetailsList = empList1;
				}

				// Record Type - Consolidate Currency

				if (model.RecordType == (int)RECORDTYPE.CONSOLIDATE)
				{
					var empList1 = userdetailslist.GroupBy(x => x.CurrencyId).Select(group => new
					{
						Currency = group.FirstOrDefault().CurrencyId,
						TotalGrossSalary = group.FirstOrDefault().GrossSalary,
						TotalAllowances = group.FirstOrDefault().TotalAllowance,
						TotalDeductions = group.FirstOrDefault().TotalDeduction
					}).ToList();

					foreach (var item in empList1)
					{
						var exchangeRateList = _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == item.Currency && x.ToCurrency == model.CurrencyId).OrderByDescending(x => x.Date).ToListAsync().Result.FirstOrDefault();
						TotalGrossSalary += item.TotalGrossSalary * exchangeRateList.Rate;
						TotalAllowances += item.TotalAllowances * exchangeRateList.Rate;
						if (model.AllowanceId != null)
						{
							var allowanceTotal = allowanceList.Where(x => x.SalaryHeadId == model.AllowanceId).Sum(x => x.MonthlyAmount);
							TotalAllowances = allowanceTotal * exchangeRateList.Rate;
						}
						TotalDeductions += item.TotalDeductions * exchangeRateList.Rate;
						if (model.DeductionId != null)
						{
							var deductionTotal = deductionList.Where(x => x.SalaryHeadId == model.DeductionId).Sum(x => x.MonthlyAmount);
							TotalDeductions = deductionTotal * exchangeRateList.Rate;
						}
					}
					response.data.TotalGrossSalary = TotalGrossSalary;
					response.data.TotalAllowances = TotalAllowances;
					response.data.TotalDeductions = TotalDeductions;
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

		public async Task<APIResponse> EmployeePaymentTypeReport(List<EmployeePaymentTypeModel> model, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var isexistrecord = _uow.EmployeePaymentTypeRepository.FindAllAsync(x => x.OfficeId == model[0].OfficeId && x.FinancialYearDate.Date.Month == model[0].FinancialYearDate.Date.Month && x.FinancialYearDate.Date.Year == model[0].FinancialYearDate.Date.Year && x.PaymentType == model[0].PaymentType).Result.ToList();
				_uow.GetDbContext().EmployeePaymentTypes.RemoveRange(isexistrecord);
				await _uow.SaveAsync();
				foreach (var item in isexistrecord)
				{
					var isrecordexist = _uow.EmployeeMonthlyPayrollRepository.FindAllAsync(x => x.EmployeeID == item.EmployeeID && x.Date.Date.Month == model[0].FinancialYearDate.Date.Month && x.Date.Date.Year == model[0].FinancialYearDate.Date.Year).Result.ToList();
					if (isrecordexist.Count != 0)
					{
						_uow.GetDbContext().EmployeeMonthlyPayroll.RemoveRange(isrecordexist);
						await _uow.SaveAsync();
					}
				}
				List<EmployeePaymentTypes> list = new List<EmployeePaymentTypes>();
				List<EmployeeMonthlyPayroll> listMonthlyPayroll = new List<EmployeeMonthlyPayroll>();
				foreach (var item in model)
				{
					EmployeePaymentTypes emp = new EmployeePaymentTypes();
					emp.OfficeId = item.OfficeId;
					emp.CurrencyId = item.CurrencyId;
					emp.FinancialYearDate = item.FinancialYearDate;
					emp.EmployeeID = item.EmployeeId;
					emp.EmployeeName = item.EmployeeName;
					emp.PaymentType = item.PaymentType;
					emp.WorkingDays = item.WorkingDays;
					emp.PresentDays = item.PresentDays;
					emp.AbsentDays = item.AbsentDays;
					emp.LeaveDays = item.LeaveDays;
					emp.TotalWorkHours = item.TotalWorkHours;
					emp.HourlyRate = item.HourlyRate;
					emp.TotalGeneralAmount = item.TotalGeneralAmount;
					emp.TotalAllowance = item.TotalAllowance;
					emp.TotalDeduction = item.TotalDeduction;
					emp.GrossSalary = item.GrossSalary;
					emp.OverTimeHours = item.OverTimeHours;
					emp.CreatedById = userid;
					emp.CreatedDate = DateTime.UtcNow;
					emp.IsDeleted = false;
					emp.IsApproved = item.IsApproved;
					emp.PensionAmount = item.PensionAmount;
					emp.SalaryTax = item.SalaryTax;
					emp.NetSalary = item.NetSalary;
					emp.PensionRate = item.employeepayrolllist[0].PensionRate;

					list.Add(emp);
					foreach (var element in item.employeepayrolllist)
					{
						EmployeeMonthlyPayroll obj = new EmployeeMonthlyPayroll();
						obj.EmployeeID = item.EmployeeId;
						obj.SalaryHeadId = element.SalaryHeadId;
						obj.MonthlyAmount = element.MonthlyAmount;
						obj.CurrencyId = item.CurrencyId;
						obj.Date = item.FinancialYearDate;
						listMonthlyPayroll.Add(obj);
					}
				}
				await _uow.GetDbContext().EmployeePaymentTypes.AddRangeAsync(list);
				await _uow.SaveAsync();
				await _uow.GetDbContext().EmployeeMonthlyPayroll.AddRangeAsync(listMonthlyPayroll);
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

		public async Task<APIResponse> RemoveApprovedList(RemoveApprovedEmployee model, string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				var isexistrecord = _uow.EmployeePaymentTypeRepository.FindAllAsync(x => x.OfficeId == model.OfficeId && x.FinancialYearDate.Date.Month == model.FinancialYearDate.Date.Month && x.FinancialYearDate.Date.Year == model.FinancialYearDate.Date.Year && x.PaymentType == model.PaymentType).Result.ToList();
				_uow.GetDbContext().EmployeePaymentTypes.RemoveRange(isexistrecord);
				await _uow.SaveAsync();
				foreach (var item in isexistrecord)
				{
					var isrecordexist = _uow.EmployeeMonthlyPayrollRepository.FindAllAsync(x => x.EmployeeID == item.EmployeeID && x.Date.Date.Month == model.FinancialYearDate.Date.Month && x.Date.Date.Year == model.FinancialYearDate.Date.Year).Result.ToList();
					if (isrecordexist.Count != 0)
					{
						_uow.GetDbContext().EmployeeMonthlyPayroll.RemoveRange(isrecordexist);
						await _uow.SaveAsync();
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

		public async Task<APIResponse> EmployeePensionReport(PensionReportModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				EmployeePensionModel epm = new EmployeePensionModel();
				List<EmployeePensionReportModel> lst = new List<EmployeePensionReportModel>();
				var financialYearList = _uow.FinancialYearDetailRepository.FindAllAsync(x => x.FinancialYearId == model.FinancialYearId).Result.FirstOrDefault();
				var empList = await _uow.EmployeePaymentTypeRepository.FindAllAsync(x => x.FinancialYearDate >= financialYearList.StartDate && x.FinancialYearDate <= financialYearList.EndDate && x.OfficeId == model.OfficeId && x.EmployeeID == model.EmployeeId);
				var previousPensionList = await _uow.EmployeePaymentTypeRepository.FindAllAsync(x => x.FinancialYearDate.Date < financialYearList.StartDate.Date);
				epm.PreviousPensionRate = previousPensionList.Average(x => x.PensionRate);
				foreach (var item in previousPensionList)
				{
					epm.PreviousPensionDeduction += Math.Round(Convert.ToDouble(item.GrossSalary * item.PensionRate), 2);
					epm.PreviousProfit += Math.Round(Convert.ToDouble(item.GrossSalary * item.PensionRate * item.PensionRate), 2);
					epm.PreviousTotal += Math.Round(Convert.ToDouble((item.GrossSalary * item.PensionRate * item.PensionRate) + (item.GrossSalary * item.PensionRate)), 2);
				}
				foreach (var item in empList)
				{
					EmployeePensionReportModel obj = new EmployeePensionReportModel();
					obj.CurrencyId = item.CurrencyId;
					obj.Date = item.FinancialYearDate;
					obj.GrossSalary = Math.Round(Convert.ToDouble(item.GrossSalary), 2);
					obj.PensionRate = item.PensionRate;
					obj.PensionDeduction = Math.Round(Convert.ToDouble(item.GrossSalary * item.PensionRate), 2);
					obj.Profit = Math.Round(Convert.ToDouble(item.GrossSalary * item.PensionRate * item.PensionRate), 2);
					obj.Total = Math.Round(Convert.ToDouble((item.GrossSalary * item.PensionRate * item.PensionRate) + (item.GrossSalary * item.PensionRate)), 2);
					lst.Add(obj);

				}
				epm.EmployeePensionReportList = lst.OrderBy(x => x.Date.Date).ToList();
				epm.PensionTotal = lst.Sum(x => x.Total);
				epm.PensionProfitTotal = lst.Sum(x => x.Profit);
				epm.PensionDeductionTotal = Math.Round(Convert.ToDouble(lst.Sum(x => x.GrossSalary * x.PensionRate)), 2);
				response.data.EmployeePensionModel = epm;
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

		public async Task<APIResponse> GetAllEmployeeProjects(int EmployeeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				response.data.EmployeeProjectList = await _uow.GetDbContext().BudgetLineEmployees.Include(x => x.ProjectBudgetLine).Where(x => x.EmployeeId == EmployeeId && x.IsActive == true)
					.Select(x => new EmployeeProjectModel
					{
						EmployeeId = x.EmployeeId,
						ProjectId = x.ProjectId,
						ProjectName = x.ProjectBudgetLine.ProjectDetails.ProjectName,
						BudgetLineId = x.BudgetLineId,
						BudgetLineName = x.ProjectBudgetLine.Description,
						ProjectPercentage = x.ProjectPercentage == null ? 0 : x.ProjectPercentage
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

		public async Task<APIResponse> AssignEmployeeProjectPercentage(List<EmployeeProjectModel> model, string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{
				foreach (var item in model)
				{
					var recordList = await _uow.BudgetLineEmployeesRepository.FindAsync(x => x.EmployeeId == item.EmployeeId && x.ProjectId == item.ProjectId && x.BudgetLineId == item.BudgetLineId && x.IsActive == true);
					recordList.ProjectPercentage = item.ProjectPercentage;
					recordList.ModifiedById = UserId;
					recordList.ModifiedDate = DateTime.Now;
					await _uow.BudgetLineEmployeesRepository.UpdateAsyn(recordList);

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

		public async Task<APIResponse> GetExchangeRate(ExchangeRateModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.FromCurrency == model.FromCurrency && x.ToCurrency == model.ToCurrency).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
				if (exchangeRate != null)
				{
					response.data.ExchangeRateLists = exchangeRate;
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

		public async Task<APIResponse> GetAllEmployeeContractType()
		{
			APIResponse response = new APIResponse();
			try
			{
				response.data.EmployeeContractTypeList = await _uow.GetDbContext().EmployeeContractType.OrderByDescending(x => x.EmployeeContractTypeId).ToListAsync();
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

		public async Task<APIResponse> SaveContractContent(ContractTypeModel model, string userId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var recordExists = await _uow.ContractTypeContentRepository.FindAsync(x => x.OfficeId == model.OfficeId && x.EmployeeContractTypeId == model.EmployeeContractTypeId);
				if (recordExists == null)
				{
					ContractTypeContent obj = _mapper.Map<ContractTypeContent>(model);
					await _uow.ContractTypeContentRepository.AddAsyn(obj);
				}
				else
				{
					recordExists.ContentDari = model.ContentDari;
					recordExists.ContentEnglish = model.ContentEnglish;
					await _uow.ContractTypeContentRepository.UpdateAsyn(recordExists);
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

		public async Task<APIResponse> GetAllContractTypeContent(int officeId, int EmployeeContractTypeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				response.data.ContractTypeContentList = await _uow.GetDbContext().ContractTypeContent.Where(x => x.OfficeId == officeId && x.EmployeeContractTypeId == EmployeeContractTypeId).FirstOrDefaultAsync();
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

		public async Task<APIResponse> GetSelectedEmployeeContract(int OfficeId, int ProjectId, int BudgetLineId, int EmployeeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				List<EmployeeContractModel> EmployeeContractModellst = new List<EmployeeContractModel>();
				var lst = await _uow.BudgetLineEmployeesRepository.FindAllAsync(x => x.ProjectId == ProjectId && x.BudgetLineId == BudgetLineId && x.EmployeeId == EmployeeId);
				if (lst != null)
				{
					var records = (from edd in await _uow.DesignationDetailRepository.GetAllAsyn()
								   join epr in await _uow.EmployeeProfessionalDetailRepository.GetAllAsyn() on edd.DesignationId equals epr.DesignationId
								   join edr in await _uow.EmployeeDetailRepository.GetAllAsyn() on epr.EmployeeId equals edr.EmployeeID
								   join esd in await _uow.EmployeeSalaryDetailsRepository.GetAllAsyn() on edr.EmployeeID equals esd.EmployeeId
								   join ble in await _uow.BudgetLineEmployeesRepository.GetAllAsyn() on edr.EmployeeID equals ble.EmployeeId
								   join pbl in await _uow.ProjectBudgetLineRepository.GetAllAsyn() on ble.BudgetLineId equals pbl.BudgetLineId
								   join pdr in await _uow.ProjectDetailRepository.GetAllAsyn() on pbl.ProjectId equals pdr.ProjectId
								   join ofd in await _uow.OfficeDetailRepository.GetAllAsyn() on epr.OfficeId equals ofd.OfficeId
								   join str in await _uow.ProvinceDetailsRepository.GetAllAsyn() on edr.ProvinceId equals str.ProvinceId
								   join mph in await _uow.PayrollMonthlyHourDetailRepository.GetAllAsyn() on epr.OfficeId equals mph.OfficeId
								   join cct in await _uow.ContractTypeContentRepository.GetAllAsyn() on epr.EmployeeContractTypeId equals cct.EmployeeContractTypeId
								   where ble.EmployeeId == EmployeeId
								   select new EmployeeContractModel
								   {
									   EmployeeName = ble.EmployeeName,
									   FatherName = edr.FatherName,
									   EmployeeCode = edr.EmployeeCode,
									   Designation = edd.Designation,
									   ContractStartDate = pbl.StartDate,
									   ContractEndDate = pbl.EndDate,
									   DurationOfContract = (pbl.EndDate - pbl.StartDate).Days,
									   Salary = Math.Round(Convert.ToDouble((esd.TotalGeneralAmount + esd.TotalAllowance - esd.Totalduduction) * ble.ProjectPercentage), 2),
									   Grade = null,
									   ProjectName = pdr.ProjectName,
									   ProjectCode = pdr.ProjectId,
									   DutyStation = ofd.OfficeName,
									   Province = str.ProvinceName,
									   BudgetLine = pbl.Description,
									   JobId = null,
									   WorkTime = mph.InTime.Value.Hour,
									   WorkDayHours = (mph.OutTime - mph.InTime).Value.Days,
									   ContentEnglish = cct.ContentEnglish,
									   ContentDari = cct.ContentDari,
									   EmployeeImage = edr.DocumentGUID + edr.Extension
								   }).ToList();
					EmployeeContractModellst.AddRange(records);
				}
				response.data.EmployeeContractModellst = EmployeeContractModellst;
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

		public async Task<APIResponse> GetEmployeeSalaryDetails(int OfficeId, int year, int month, int EmployeeId)
		{
			APIResponse response = new APIResponse();
			try
			{
				var records = (from ddr in await _uow.DesignationDetailRepository.GetAllAsyn()
							   join epr in await _uow.EmployeeProfessionalDetailRepository.GetAllAsyn() on ddr.DesignationId equals epr.DesignationId
							   join edr in await _uow.EmployeeDetailRepository.GetAllAsyn() on epr.EmployeeId equals edr.EmployeeID
							   join etr in await _uow.EmployeeTypeRepository.GetAllAsyn() on edr.EmployeeTypeId equals etr.EmployeeTypeId
							   join odr in await _uow.OfficeDetailRepository.GetAllAsyn() on epr.OfficeId equals odr.OfficeId
							   join blr in await _uow.BudgetLineEmployeesRepository.GetAllAsyn() on edr.EmployeeID equals blr.EmployeeId
							   join ear in await _uow.EmployeePaymentTypeRepository.GetAllAsyn() on edr.EmployeeID equals ear.EmployeeID
							   join epp in await _uow.EmployeePayrollRepository.FindAllAsync(x => x.EmployeeID == EmployeeId) on edr.EmployeeID equals epp.EmployeeID
							   join sdr in await _uow.SalaryHeadDetailsRepository.GetAllAsyn() on epp.SalaryHeadId equals sdr.SalaryHeadId
							   join ccr in await _uow.CurrencyDetailsRepository.GetAllAsyn() on ear.CurrencyId equals ccr.CurrencyId
							   where edr.EmployeeID == EmployeeId && ear.FinancialYearDate.Year == year && ear.FinancialYearDate.Month == month && epr.OfficeId == OfficeId
							   select new EmployeeSalarySlipModel
							   {
								   EmployeeName = edr.EmployeeName,
								   EmployeeCode = edr.EmployeeCode,
								   Designation = ddr.Designation,
								   Type = etr.EmployeeTypeName,
								   Office = odr.OfficeCode,
								   Sex = edr.SexId == (int)Sex.Male ? "Male" : edr.SexId == (int)Sex.Female ? "Female" : "Other",
								   BudgetLine = blr.BudgetLineId,
								   Program = null,
								   ProjectId = blr.ProjectId,
								   JobId = null,
								   Sector = null,
								   Area = null,
								   Account = null,
								   SalaryPercentage = blr.ProjectPercentage,
								   Salary = ear.TotalGeneralAmount + ear.TotalAllowance - ear.TotalDeduction,
								   BasicSalary = ear.TotalGeneralAmount,
								   CurrencyCode = ccr.CurrencyCode,
								   Attendance = ear.PresentDays,
								   Absentese = ear.AbsentDays,

								   PayrollId = epp?.PayrollId ?? 0,
								   SalaryHeadType = sdr.HeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : sdr.HeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : sdr.HeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : "",
								   HeadTypeId = sdr.HeadTypeId,
								   SalaryHeadId = sdr.SalaryHeadId,
								   SalaryHead = sdr.HeadName,
								   MonthlyAmount = epp?.MonthlyAmount ?? 0,
								   CurrencyId = epp?.CurrencyId ?? 0,
								   PaymentType = epp?.PaymentType ?? 0,
								   PensionRate = epp?.PensionRate ?? 0,

								   GrossSalary = ear.TotalGeneralAmount + ear.TotalAllowance,
								   NetSalary = Math.Round(Convert.ToDouble(ear.TotalGeneralAmount + ear.TotalAllowance - ear.TotalDeduction - ear.PensionAmount), 2),
								   Description = null
								   //Comment
							   }).ToList();

				response.data.EmployeeSalarySlipModelList = records;
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

		public async Task<APIResponse> EmployeeTaxCalculation(int OfficeId, int EmployeeId, int FinancialYearId)
		{
			APIResponse response = new APIResponse();
			try
			{
				EmployeeTaxReport obj = new EmployeeTaxReport();
				obj.EmployeeTaxpayerIdentification = "1001157013";
				obj.NameOfBusiness = "Coordination of Humanitarian Assistance (CHA)";
				obj.AddressOfBusiness = "Charah-e-Qambar, 5th District, Kabul, Afghanistan";
				obj.TelephoneNumber = "+93(0)700291722, +93(0)729128401";
				obj.EmailAddressEmployer = "info@cha-net.org";

				var financialYear = await _uow.FinancialYearDetailRepository.FindAsync(x=>x.FinancialYearId == FinancialYearId);

				var record = await _uow.GetDbContext().EmployeePaymentTypes.Include(x => x.EmployeeDetail).Where(x => x.EmployeeID == EmployeeId && x.OfficeId == OfficeId && x.FinancialYearDate.Date <= financialYear.StartDate.Date && x.FinancialYearDate.Date >= financialYear.EndDate.Date && x.IsApproved == true).ToListAsync();
				if (record.Count > 0)
				{
					obj.EmployeeName = record[0].EmployeeName;
					obj.EmployeeTaxpayerIdentification = "Nill";
					obj.EmployeeAddress = record[0].EmployeeDetail.CurrentAddress;
					obj.TelephoneNumberEmployee = record[0].EmployeeDetail.Phone;
					obj.EmailAddressEmployee = record[0].EmployeeDetail.Email;

					obj.AnnualTaxPeriod = record[0].FinancialYearDate.Year;
					obj.DatesOfEmployeement = 0;
					obj.TotalWages = record.Sum(x => x.NetSalary);
					obj.TotalTax = record.Sum(x => x.SalaryTax);

					obj.OfficerName = "";
					obj.Position = "";
					obj.Date = DateTime.Now;

				}
				response.data.EmployeeTaxReport = obj;
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

		public async Task<APIResponse> EmployeeSalaryTaxDetails(SalaryTaxModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var financialYear = await _uow.FinancialYearDetailRepository.FindAsync(x => x.FinancialYearId == model.FinancialYearId);
				if (financialYear != null)
				{					
					var lst = _uow.EmployeePaymentTypeRepository.FindAll(x => x.IsApproved == true && x.OfficeId == model.OfficeId && x.EmployeeID == model.EmployeeId && x.FinancialYearDate.Date >= financialYear.StartDate.Date && x.FinancialYearDate.Date <= financialYear.EndDate.Date)
					.Select(x => new SalaryTaxReportModel
					 {
						 CurrencyId = x.CurrencyId,
						 Date = x.FinancialYearDate.Date,
						 TotalTax = x.SalaryTax
					 }).OrderBy(x=>x.Date).ToList();
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
	}
}
