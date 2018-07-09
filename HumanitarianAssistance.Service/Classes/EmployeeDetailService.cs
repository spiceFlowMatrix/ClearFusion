using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
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
    public class EmployeeDetailService : IEmployeeDetail
    {
        #region "Variable Initialization"
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public EmployeeDetailService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
        }

        #endregion

        #region "Employment Outside Organization"
        public async Task<APIResponse> GetAllEmployeeHistoryOutsideOrganization(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeHistoryRecord = await _uow.GetDbContext().EmployeeHistoryOutsideOrganization.Where(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeHistoryOutsideOrganizationList = employeeHistoryRecord.Select(x => new EmployeeHistoryOutsideOrganizationModel
                    {
                        EmployeeHistoryOutsideOrganizationId = x.EmployeeHistoryOutsideOrganizationId,
                        EmployeeID = x.EmployeeID,
                        EmploymentFrom = x.EmploymentFrom,
                        EmploymentTo = x.EmploymentTo,
                        MonthlySalary = x.MonthlySalary,
                        Organization = x.Organization,
                        ReasonForLeaving = x.ReasonForLeaving
                    }).ToList();
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

        public async Task<APIResponse> AddEmployeeHistoryOutsideOrganization(EmployeeHistoryOutsideOrganizationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                EmployeeHistoryOutsideOrganization obj = _mapper.Map<EmployeeHistoryOutsideOrganization>(model);
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.EmployeeHistoryOutsideOrganizationRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditEmployeeHistoryOutsideOrganization(EmployeeHistoryOutsideOrganizationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeHistoryOutsideOrganizationRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeHistoryOutsideOrganizationId == model.EmployeeHistoryOutsideOrganizationId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeHistoryOutsideOrganizationRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> DeleteEmployeeHistoryOutsideOrganization(EmployeeHistoryOutsideOrganizationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeHistoryOutsideOrganizationRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeHistoryOutsideOrganizationId == model.EmployeeHistoryOutsideOrganizationId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeHistoryOutsideOrganizationRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #endregion

        #region "Employment Outside Country"
        public async Task<APIResponse> GetAllEmployeeHistoryOutsideCountry(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeHistoryRecord = await _uow.GetDbContext().EmployeeHistoryOutsideCountry.Where(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeHistoryOutsideOrganizationList = employeeHistoryRecord.Select(x => new EmployeeHistoryOutsideOrganizationModel
                    {
                        EmployeeHistoryOutsideCountryId = x.EmployeeHistoryOutsideCountryId,
                        EmployeeID = x.EmployeeID,
                        EmploymentFrom = x.EmploymentFrom,
                        EmploymentTo = x.EmploymentTo,
                        MonthlySalary = x.MonthlySalary,
                        Organization = x.Organization,
                        ReasonForLeaving = x.ReasonForLeaving
                    }).ToList();
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

        public async Task<APIResponse> AddEmployeeHistoryOutsideCountry(EmployeeHistoryOutsideOrganizationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                EmployeeHistoryOutsideCountry obj = _mapper.Map<EmployeeHistoryOutsideCountry>(model);
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.EmployeeHistoryOutsideCountryRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditEmployeeHistoryOutsideCountry(EmployeeHistoryOutsideOrganizationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeHistoryOutsideCountryRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeHistoryOutsideCountryId == model.EmployeeHistoryOutsideCountryId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeHistoryOutsideCountryRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> DeleteEmployeeHistoryOutsideCountry(EmployeeHistoryOutsideOrganizationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeHistoryOutsideCountryRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeHistoryOutsideCountryId == model.EmployeeHistoryOutsideCountryId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeHistoryOutsideCountryRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #endregion

        #region "Employee Relative Information"

        public async Task<APIResponse> GetAllEmployeeRelativeInformation(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeHistoryRecord = await _uow.GetDbContext().EmployeeRelativeInfo.Where(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeRelativeInfoList = employeeHistoryRecord.Select(x => new EmployeeRelativeInfoModel
                    {
                        EmployeeRelativeInfoId = x.EmployeeRelativeInfoId,
                        Name = x.Name,
                        Organization = x.Organization,
                        Position = x.Position,
                        Relationship = x.Relationship,
                        EmployeeID = x.EmployeeID
                    }).ToList();
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

        public async Task<APIResponse> AddEmployeeRelativeInformation(EmployeeRelativeInfoModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                EmployeeRelativeInfo obj = _mapper.Map<EmployeeRelativeInfo>(model);
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.EmployeeRelativeInfoRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditEmployeeRelativeInformation(EmployeeRelativeInfoModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeRelativeInfoRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeRelativeInfoId == model.EmployeeRelativeInfoId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeRelativeInfoRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> DeleteEmployeeRelativeInformation(EmployeeRelativeInfoModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeRelativeInfoRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeRelativeInfoId == model.EmployeeRelativeInfoId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeRelativeInfoRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #endregion

        #region "Employee Information Other than relative"

        public async Task<APIResponse> GetAllEmployeeInfoReferences(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeHistoryRecord = await _uow.GetDbContext().EmployeeInfoReferences.Where(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeRelativeInfoList = employeeHistoryRecord.Select(x => new EmployeeRelativeInfoModel
                    {
                        EmployeeInfoReferencesId = x.EmployeeInfoReferencesId,
                        Name = x.Name,
                        Organization = x.Organization,
                        Position = x.Position,
                        Relationship = x.Relationship,
                        EmployeeID = x.EmployeeID
                    }).ToList();
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

        public async Task<APIResponse> AddEmployeeInfoReferences(EmployeeRelativeInfoModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                EmployeeInfoReferences obj = _mapper.Map<EmployeeInfoReferences>(model);
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.EmployeeInfoReferencesRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditEmployeeInfoReferences(EmployeeRelativeInfoModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeInfoReferencesRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeInfoReferencesId == model.EmployeeInfoReferencesId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeInfoReferencesRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> DeleteEmployeeInfoReferences(EmployeeRelativeInfoModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeInfoReferencesRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeInfoReferencesId == model.EmployeeInfoReferencesId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeInfoReferencesRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #endregion

        #region "Employee Other Skills"

        public async Task<APIResponse> GetAllEmployeeOtherSkills(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeHistoryRecord = await _uow.GetDbContext().EmployeeOtherSkills.Where(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeOtherSkillsList = employeeHistoryRecord.Select(x => new EmployeeOtherSkillsModel
                    {
                        EmployeeOtherSkillsId = x.EmployeeOtherSkillsId,
                        AbilityLevel = x.AbilityLevel,
                        EmployeeID = x.EmployeeID,
                        Experience = x.Experience,
                        Remarks = x.Remarks,
                        TypeOfSkill = x.TypeOfSkill
                    }).ToList();
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

        public async Task<APIResponse> AddEmployeeOtherSkills(EmployeeOtherSkillsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                EmployeeOtherSkills obj = _mapper.Map<EmployeeOtherSkills>(model);
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.EmployeeOtherSkillsRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditEmployeeOtherSkills(EmployeeOtherSkillsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeOtherSkillsRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeOtherSkillsId == model.EmployeeOtherSkillsId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeOtherSkillsRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> DeleteEmployeeOtherSkills(EmployeeOtherSkillsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeOtherSkillsRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeOtherSkillsId == model.EmployeeOtherSkillsId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeOtherSkillsRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #endregion

        #region "Employee Salary Budgets"

        public async Task<APIResponse> GetAllEmployeeSalaryBudgets(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeHistoryRecord = await _uow.GetDbContext().EmployeeSalaryBudget.Where(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeSalaryBudgetList = employeeHistoryRecord.Select(x => new EmployeeSalaryBudgetModel
                    {
                        EmployeeSalaryBudgetId = x.EmployeeSalaryBudgetId,
                        EmployeeID = x.EmployeeID,
                        BudgetDisbursed = x.BudgetDisbursed,
                        CurrencyId = x.CurrencyId,
                        SalaryBudget = x.SalaryBudget,
                        Year = x.Year
                    }).ToList();
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

        public async Task<APIResponse> AddEmployeeSalaryBudgets(EmployeeSalaryBudgetModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                EmployeeSalaryBudget obj = _mapper.Map<EmployeeSalaryBudget>(model);
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.EmployeeSalaryBudgetRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditEmployeeSalaryBudgets(EmployeeSalaryBudgetModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeSalaryBudgetRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeSalaryBudgetId == model.EmployeeSalaryBudgetId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeSalaryBudgetRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> DeleteEmployeeSalaryBudgets(EmployeeSalaryBudgetModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeSalaryBudgetRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeSalaryBudgetId == model.EmployeeSalaryBudgetId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeSalaryBudgetRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #endregion

        #region "Employee Educations"

        public async Task<APIResponse> GetAllEmployeeEducations(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeHistoryRecord = await _uow.GetDbContext().EmployeeEducations.Where(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeEducationsList = employeeHistoryRecord.Select(x => new EmployeeEducationsModel
                    {
                        EmployeeEducationsId = x.EmployeeEducationsId,
                        EmployeeID = x.EmployeeID,
                        Degree = x.Degree,
                        EducationFrom = x.EducationFrom,
                        EducationTo = x.EducationTo,
                        FieldOfStudy = x.FieldOfStudy,
                        Institute = x.Institute
                    }).ToList();
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

        public async Task<APIResponse> AddEmployeeEducations(EmployeeEducationsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                EmployeeEducations obj = _mapper.Map<EmployeeEducations>(model);
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.EmployeeEducationsRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditEmployeeEducations(EmployeeEducationsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeEducationsRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeEducationsId == model.EmployeeEducationsId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeEducationsRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> DeleteEmployeeEducations(EmployeeEducationsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeEducationsRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeEducationsId == model.EmployeeEducationsId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeEducationsRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #endregion

        #region "Employee Salary Analytical Info"

        public async Task<APIResponse> GetAllEmployeeSalaryAnalyticalInfo(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeHistoryRecord = await _uow.GetDbContext().EmployeeSalaryAnalyticalInfo.Include(x => x.ProjectBudgetLine).Where(x => x.IsDeleted == false && x.EmployeeID == EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeSalaryAnalyticalInfoList = employeeHistoryRecord.Select(x => new EmployeeSalaryAnalyticalInfoModel
                    {
                        EmployeeSalaryAnalyticalInfoId = x.EmployeeSalaryAnalyticalInfoId,
                        AccountCode = x.AccountCode,
                        BudgetLineId = x.BudgetLineId,
                        EmployeeID = x.EmployeeID,
                        ProjectId = x.ProjectId,
                        SalaryPercentage = x.SalaryPercentage,
                        BudgetLineName = x.ProjectBudgetLine?.Description

                    }).ToList();
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

        public async Task<APIResponse> AddEmployeeSalaryAnalyticalInfo(EmployeeSalaryAnalyticalInfoModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                //EmployeeSalaryAnalyticalInfo obj = _mapper.Map<EmployeeSalaryAnalyticalInfo>(model);

                EmployeeSalaryAnalyticalInfo obj = new EmployeeSalaryAnalyticalInfo();

                obj.EmployeeSalaryAnalyticalInfoId = 0;
                obj.EmployeeID = model.EmployeeID;
                obj.ProjectId = model.ProjectId;
                obj.BudgetLineId = model.BudgetLineId;
                obj.AccountCode = model.AccountCode;
                obj.SalaryPercentage = model.SalaryPercentage;

                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.EmployeeSalaryAnalyticalInfoRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditEmployeeSalaryAnalyticalInfo(EmployeeSalaryAnalyticalInfoModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeSalaryAnalyticalInfoRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeSalaryAnalyticalInfoId == model.EmployeeSalaryAnalyticalInfoId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeSalaryAnalyticalInfoRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> DeleteEmployeeSalaryAnalyticalInfo(EmployeeSalaryAnalyticalInfoModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeSalaryAnalyticalInfoRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeSalaryAnalyticalInfoId == model.EmployeeSalaryAnalyticalInfoId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _mapper.Map(model, existRecord);
                    await _uow.EmployeeSalaryAnalyticalInfoRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #endregion


        #region "Health Info"
        public async Task<APIResponse> GetAllEmployeeHealthInfo(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var employeeRecord = await _uow.EmployeeHealthInfoRepository.FindAsync(x => x.EmployeeId == EmployeeId);

                EmployeeHealthInformationModel obj = _mapper.Map<EmployeeHealthInformationModel>(employeeRecord);

                if (employeeRecord != null)
                {
                    response.data.EmployeeHealthInfo = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> AddEmployeeHealthInfo(EmployeeHealthInformationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                EmployeeHealthInfo obj = _mapper.Map<EmployeeHealthInfo>(model);

                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;


                await _uow.EmployeeHealthInfoRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditEmployeeHealthInfo(EmployeeHealthInformationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.EmployeeHealthInfoRepository.FindAsync(x => x.IsDeleted == false && x.EmployeeHealthInfoId == model.EmployeeHealthInfoId);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;

                    _mapper.Map(model, existRecord);

                    await _uow.EmployeeHealthInfoRepository.UpdateAsyn(existRecord);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #endregion



    }
}
