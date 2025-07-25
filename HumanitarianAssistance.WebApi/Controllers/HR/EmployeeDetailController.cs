using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Commands.Delete;
using HumanitarianAssistance.Application.HR.Commands.Update;
using HumanitarianAssistance.Application.HR.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.HR
{
    [Produces("application/json")]
    [Route("api/EmployeeDetail/[Action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeDetailController : BaseController
    {

        #region "Employee History Outside Organization"

        [HttpGet]

        public async Task<ApiResponse> GetAllEmployeeHistoryOutsideOrganization([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetEmployeeHistoryOutsideOrganizationQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeHistoryOutsideOrganization([FromBody] AddEmployeeHistoryOutsideOrganizationCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeHistoryOutsideOrganization([FromBody] EditEmployeeHistoryOutsideOrganizationCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteEmployeeHistoryOutsideOrganization([FromBody] DeleteEmployeeHistoryOutsideOrganizationCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        #endregion

        #region "Employee History Outside Country"
        [HttpGet]

        public async Task<ApiResponse> GetAllEmployeeHistoryOutsideCountry([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetAllEmployeeHistoryOutsideCountryQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeHistoryOutsideCountry([FromBody] AddEmployeeHistoryOutsideCountryCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeHistoryOutsideCountry([FromBody] EditEmployeeHistoryOutsideCountryCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteEmployeeHistoryOutsideCountry([FromBody] DeleteEmployeeHistoryOutsideCountryCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        #endregion

        #region "Employee Relative Information"

        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeRelativeInformation([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetAllEmployeeRelativeInformationQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeRelativeInformation([FromBody] AddEmployeeRelativeInformationCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeRelativeInformation([FromBody] EditEmployeeRelativeInformationCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteEmployeeRelativeInformation([FromBody] DeleteEmployeeRelativeInformationCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }
        #endregion

        #region "Employee Info References"
        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeInfoReferences([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetAllEmployeeInfoReferencesQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeInfoReferences([FromBody] AddEmployeeInfoReferencesCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeInfoReferences([FromBody] EditEmployeeInfoReferencesCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteEmployeeInfoReferences([FromBody] DeleteEmployeeInfoReferencesCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }
        #endregion

        #region "Employee Other Skills"
        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeOtherSkills([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetAllEmployeeOtherSkillsQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeOtherSkills([FromBody] AddEmployeeOtherSkillsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeOtherSkills([FromBody] EditEmployeeOtherSkillsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteEmployeeOtherSkills([FromBody] DeleteEmployeeOtherSkillsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        #endregion

        #region "Employee Salary Budgets"
        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeSalaryBudgets([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetAllEmployeeSalaryBudgetsQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeSalaryBudgets([FromBody] AddEmployeeSalaryBudgetsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeSalaryBudgets([FromBody] EditEmployeeSalaryBudgetsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteEmployeeSalaryBudgets([FromBody] DeleteEmployeeSalaryBudgetsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        #endregion

        #region "Employee Educations"
        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeEducations([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetAllEmployeeEducationsQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeEducations([FromBody] AddEmployeeEducationsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeEducations([FromBody] EditEmployeeEducationsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteEmployeeEducations([FromBody] DeleteEmployeeEducationsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        #endregion

        #region "Employee Salary Analytical Info"
        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeSalaryAnalyticalInfo([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetSalaryAnalyticalInfoQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeSalaryAnalyticalInfo([FromBody] AddSalaryAnalyticalInfoCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeSalaryAnalyticalInfo([FromBody] EditSalaryAnalyticalInfoCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteEmployeeSalaryAnalyticalInfo([FromBody] DeleteSalaryAnalyticalInfoCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        #endregion

        #region "Health Info"
        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeHealthInfo([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetEmployeeHealthInfoQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeHealthInfo([FromBody] AddEmployeeHealthInfoCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeHealthInfo([FromBody] EditEmployeeHealthInfoCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetEmployeeHealthQuestion([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetEmployeeHealthQuestionQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeHealthQuestion([FromBody] AddEmployeeHealthQuestionCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeHealthQuestion([FromBody] EditEmployeeHealthQuestionCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteEmployeeHealthQuestion([FromBody] DeleteHealthQuestionCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }
        #endregion

        #region "Employee Languages"
        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeLanguages([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetAllEmployeeLanguagesQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeLanguages([FromBody] AddEmployeeLanguagesCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeLanguages([FromBody] EditEmployeeLanguagesCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> RemoveEmployeeLanguages([FromBody] RemoveEmployeeLanguagesCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }
        #endregion

        #region "new employee detail"
        [HttpPost]
        public async Task<ApiResponse> AddNewEmployee([FromBody] AddNewEmployeeCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeDetail([FromBody] EditEmployeeDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetEmployeeDetailsByEmployeeId([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetEmployeeDetailsByIdQuery { EmployeeId = EmployeeId });
        }

        #endregion

        #region "Employee Documnet detail"
        [HttpPost]
        public async Task<ApiResponse> AddDocumentDetail([FromBody] AddDocumentDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteDocumentDetail([FromBody] int DocumentId)
        {
            DeleteDocumentDetailCommand model = new DeleteDocumentDetailCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.DocumentId = DocumentId;
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllDocumentDetailByEmployeeId([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetAllDocumentDetailByIdQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeHistoryDetail([FromBody] AddEmployeeHistoryCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeHistoryDetail([FromBody] EditEmployeeHistoryCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpDelete]
        public async Task<ApiResponse> DeleteEmployeeHistoryDetail(int HistoryId)
        {
            DeleteEmployeeHistoryCommand model = new DeleteEmployeeHistoryCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.HistoryId = HistoryId;
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeHistoryByEmployeeId([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetAllEmployeeHistoryByIdQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeProfessionalDetail([FromBody] EditEmployeeProfessionalDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetEmployeeProfessionalDetail([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetEmployeeProfessionalDetailQuery { EmployeeId = EmployeeId });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeDetail([FromQuery] int EmployeeType, int OfficeId)
        {
            return await _mediator.Send(new GetAllEmployeeDetailQuery
            {
                EmployeeType = EmployeeType,
                OfficeId = OfficeId
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeHealthDetailByEmployeeId([FromQuery] int employeeid)
        {
            return await _mediator.Send(new GetAllEmployeeHealthDetailByIdQuery { EmployeeId = employeeid });
        }

        [HttpPost]
        public async Task<ApiResponse> ChangeEmployeeImage([FromBody] UpdateEmployeeImageCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllScheduledEmployeeList()
        {
            return await _mediator.Send(new GetAllScheduledEmployeesQuery());
        }
        #endregion

        #region EmployeeContract

        [HttpDelete]
        public async Task<ApiResponse> RemoveEmployeeContractDetails([FromQuery] int employeeContractId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteEmployeeContractCommand
            {
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
                EmployeeContractId = employeeContractId
            });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeContractDetails([FromBody] AddEmployeeContractCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        #endregion 

        [HttpPost]
        public async Task<ApiResponse> EmployeeTaxCalculation([FromBody] GetEmployeeTaxCalculationQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<object> GetSelectedEmployeeContractByEmployeeId([FromQuery] int EmployeeId)
        {
            return await _mediator.Send(new GetContractByEmployeeIdQuery
            {
                EmployeeId = EmployeeId
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllApprovedEmployeeList()
        {
            return await _mediator.Send(new GetAllApprovedEmployeeQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetPrimarySalaryHeads(int EmployeeId)
        {
            return await _mediator.Send(new GetPrimarySalaryHeadsQuery { EmployeeId = EmployeeId });
        }

        [HttpGet]
        public async Task<IActionResult> CheckUserEmailAlreadyExists(string Email)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                return Ok(await _mediator.Send(new CheckUserEmailAlreadyExistsQuery { Email = Email }));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ApiResponse> AddCandidateAsEmployee([FromBody] AddCandidateAsEmployeeCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeDetailById([FromBody] int id)
        {
            var result = await _mediator.Send(new GetEmployeeDetailByIdQuery { EmployeeId = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllEmployeeDetailList([FromBody] GetAllEmployeeDetailListQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMurtipleEmployeesById([FromBody] long[] EmpIds)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _mediator.Send(new DeleteMurtipleEmployeesByIdCommand
            {
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
                EmpIds = EmpIds
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeResignation([FromBody] int EmployeeID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _mediator.Send(new AddEmployeeResignationCommand
            {
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
                EmployeeID = EmployeeID
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployeeResignation([FromBody] SaveEmployeeResignationCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeResignationById([FromQuery] int Id)
        {
            var result = await _mediator.Send(new GetEmployeeResignationByIdQuery
            {
                EmployeeID = Id
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOpeningPensionDetail([FromBody] AddOpeningPensionDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await _mediator.Send(new AddOpeningPensionDetailCommand
            {
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow,
                EmployeeID = model.EmployeeID,
                PensionDate = model.PensionDate,
                PensionDetail = model.PensionDetail
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeDetailForContractById([FromBody] int EmployeeId)
        {
            var result = await _mediator.Send(new GetEmployeeDetailForContractByIdQuery { EmployeeId = EmployeeId });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ApiResponse> AddNewEmployeeDetails([FromBody] AddNewEmployeeDetailsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeDetails([FromBody] EditEmployeeDetailsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<IActionResult> TerminateEmployeeByEmployeeId([FromBody] TerminateEmployeeByEmployeeIdCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployeeByEmployeeId([FromBody] int EmployeeId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            DeleteEmployeeByEmployeeIdCommand model = new DeleteEmployeeByEmployeeIdCommand
            {
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
                EmployeeId = EmployeeId
            };
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ApiResponse> EditOpeningPensionDetails([FromBody] EditOpeningPensionDetailsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteOpeningPensionDetails([FromBody] int PensionId)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteOpeningPensionDetailsCommand
            {
                PensionId = PensionId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpPost]
        public async Task<IActionResult> RevokeEmployeeResignationById([FromBody] int EmployeeId)
        {
            var result = await _mediator.Send(new RevokeEmployeeResignationByIdCommand { EmployeeId = EmployeeId });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RehireEmployeeById([FromBody] int EmployeeId)
        {
            var result = await _mediator.Send(new RehireEmployeeByIdCommand { EmployeeId = EmployeeId });
            return Ok(result);
        }



        #region "employee appraisal"
        [HttpPost]
        public async Task<IActionResult> GetEmployeeAppraisalByEmployeeId([FromBody] int EmployeeId)
        {
            var result = await _mediator.Send(new GetEmployeeAppraisalByEmployeeIdQuery { EmployeeId = EmployeeId });
            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> GetFilteredEmployeeList([FromBody] GetFilteredEmployeeeListQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAppraisalDetail([FromBody] EmployeeAppraisalDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(command));
        }

        #endregion
        [HttpGet]
        public async Task<IActionResult> CreateAllEmployeeTouser()
        {
            var result = await _mediator.Send(new CreateBulkUserCommand { });
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> EditAppraisalDetail([FromBody] EditNewAppraisalDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAppraisalDetail([FromBody] int id)
        {
            var result = await _mediator.Send(new ApproveAppraisalCommand 
            {
                EmployeeAppraisalDetailsId = id,
                ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                ModifiedDate = DateTime.UtcNow
            });


            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> RejectAppraisalDetail([FromBody] int id)
        {
            var result = await _mediator.Send(new RejectAppraisalCommand
            {
                EmployeeAppraisalDetailsId = id,
                ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                ModifiedDate = DateTime.UtcNow
            });


            return Ok(result);
        }

    }
}