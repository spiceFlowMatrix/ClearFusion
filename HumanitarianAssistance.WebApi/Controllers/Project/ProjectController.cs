using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using HumanitarianAssistance.Common.Helpers;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Queries;
using HumanitarianAssistance.Application.Project.Commands.Create;
using HumanitarianAssistance.Application.Project.Commands.Delete;
using HumanitarianAssistance.Application.Project.Commands.Update;
using HumanitarianAssistance.Application.Project.Commands.Common;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using System.Linq;

namespace HumanitarianAssistance.WebApi.Controllers.Project
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Project/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Project))]
    [Authorize]
    public class ProjectController : BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public ProjectController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        #region Donor Information
        [HttpPost]
        public async Task<ApiResponse> GetAllDonorFilterList([FromBody]GetAllDonorFilterListQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllDonorList()
        {
            return await _mediator.Send(new GetAllDonorListQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditDonorDetails([FromBody]AddEditDonorDetailsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteDonorDetails([FromBody]long DonarId)
        {
            DeleteDonorDetailCommand model = new DeleteDonorDetailCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.DonorId = DonarId;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetDonarListById([FromBody]long DonarId)
        {
            return await _mediator.Send(new GetDonarListByIdQuery { DonorId = DonarId });
        }
        #endregion

        #region Sector Information

        [HttpGet]
        public async Task<ApiResponse> GetAllSectorList()
        {
            return await _mediator.Send(new GetAllSectorListQuery { });
        }

        [HttpPost]
        public async Task<ApiResponse> AddSectorDetails([FromBody]AddSectorDetailsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditSectorDetails([FromBody]EditSectorDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteSectorDetails([FromBody]DeleteSectorDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        #endregion

        #region Program Information
        [HttpGet]
        public async Task<ApiResponse> GetAllProgramList()
        {
            return await _mediator.Send(new GetAllProgramListQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> AddProgramDetails([FromBody]AddProgramDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditProgramDetails([FromBody]EditProgramDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteProgramDetails([FromBody]DeleteProgramDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        #endregion

        #region Area Details
        [HttpGet]
        public async Task<ApiResponse> GetAllAreaList()
        {
            return await _mediator.Send(new GetAllAreaListQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> AddAreaDetails([FromBody]AddAreaDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditAreaDetails([FromBody]EditAreaDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteAreaDetails([FromBody]DeleteAreaDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        #endregion

        #region GenderConsiderationList

        [HttpGet]
        public async Task<ApiResponse> GenderConsiderationList()
        {
            return await _mediator.Send(new GenderConsiderationListQuery());
        }

        #endregion

        #region StrengthConsiderationDetailList

        [HttpGet]
        public async Task<ApiResponse> StrengthConsiderationDetailList()
        {
            return await _mediator.Send(new StrengthConsiderationDetailListQuery());
        }

        #endregion

        #region SecurityDetailList
        [HttpGet]
        public async Task<ApiResponse> SecurityDetailList()
        {
            return await _mediator.Send(new GetSecurityDetailListQuery());
        }
        #endregion

        #region SecurityConsiderationDetailList
        [HttpGet]
        public async Task<ApiResponse> SecurityConsiderationDetailList()
        {
            return await _mediator.Send(new SecurityConsiderationListQuery());
        }
        #endregion

        #region Add/Edit/Delete Project Info

        #region Project Details
        [HttpPost]
        public async Task<ApiResponse> AddEditProjectDetail([FromBody]AddEditProjectDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await  _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteProjectDetail([FromBody]long ProjectId)
        {
            DeleteProjectDetailCommand model= new DeleteProjectDetailCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllProjectFilterList([FromBody]GetAllProjectFilterListQuery model)
        {
            return await _mediator.Send(model);
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllProjectList()
        {
            return await _mediator.Send(new GetAllProjectListQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetProjectListById([FromBody]long Id)
        {
            return await _mediator.Send(new GetProjectListByIdQuery{ Id= Id }); 
        }

        [HttpPost]
        public async Task<ApiResponse> GetProjectOtherDetailById([FromBody]long Id)
        {
            return await _mediator.Send(new GetProjectOtherDetailByIdQuery{ Id= Id });
        }

    #endregion

        #region Add/Update Assign Employee to Project 

        [HttpPost]
        public async Task<ApiResponse> AddEditProjectAssignToEmployee([FromBody]AddEditProjectAssignToEmployeeCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteProjectAssignToEmployee([FromBody]RemoveEmployeeFromAssignedProjectCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

     #endregion

        #region Add/Edit Project Program to Current Project

        [HttpPost]
        public async Task<ApiResponse> AddEditProjectProgram([FromBody]AddEditProjectProgramCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> getProjectProgramById([FromBody]long ProjectId)
        {
            return await _mediator.Send(new GetProjectProgramByIdQuery { ProjectId= ProjectId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditProjectArea([FromBody]AddEditProjectAreaCommand model)
        {
           var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> getProjectAreaById([FromBody]long ProjectId)
        {
            return await _mediator.Send(new GetProjectAreaByIdQuery { ProjectId= ProjectId });
        }

         [HttpPost]
        public async Task<ApiResponse> getProjectSectorById([FromBody]long ProjectId)
        {
            return await _mediator.Send(new GetProjectAreaByIdQuery { ProjectId= ProjectId });
        }

         [HttpPost]
        public async Task<ApiResponse> DeleteProjectProgram([FromBody]DeleteProjectProgramCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

     #endregion

        #region Add/Edit Project Sector Area to Current Project

        [HttpPost]
        public async Task<ApiResponse> AddEditProjectSector([FromBody]AddEditProjectSectorCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model); 
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteProjectSector([FromBody]DeleteProjectSectorCommand model) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteProjectArea([FromBody]DeleteProjectAreaCommand model)
        {
           var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        #endregion

        #endregion

        #region "GetProjectWinLossStatus"

        [HttpPost]
        public async Task<ApiResponse> GetProjectWinLossStatus([FromBody]long ProjectId)
        {
            return await _mediator.Send(new GetProjectWinLossStatusQuery { ProjectId= ProjectId });
        }

     #endregion

        #region Other Details dropdown

        [HttpGet]
        public async Task<ApiResponse> GetAllStrengthConsiderationDetails()
        {
           return await _mediator.Send(new GetAllStrengthConsiderationDetailsQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllGenderConsiderationDetails()
        {
            return await _mediator.Send(new GetAllGenderConsiderationQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllSecurityDetails()
        {
            return await _mediator.Send(new GetAllSecurityDetailQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllProvinceDetails()
        {
            return await _mediator.Send(new GetAllProvinceDetailQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllProvinceDetailsByCountryId([FromBody]int[] countryId)
        {

            return await _mediator.Send(new GetAllProvincesByCountryIdQuery { CountryId= countryId });
        }

        [HttpPost]
        public async Task<ApiResponse> GetCountryMultiSelectByProjectId([FromBody]long Id)
        {
            return await _mediator.Send(new GetCountryByProjectIdQuery { ProjectId= Id });
        }

        [HttpPost]
        public async Task<ApiResponse> GetProvinceMultiSelectByProjectId([FromBody]long Id)
        {
           return await _mediator.Send(new GetSelectedProvinceByProjectIdQuery { ProjectId= Id });
        }

        [HttpPost]
        public async Task<ApiResponse> GetDistrictMultiSelectByProjectId([FromBody]long Id)
        {
            return await _mediator.Send(new GetSelectedDistrictByProjectIdQuery { ProjectId= Id });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditDistrictMultiselect([FromBody]AddEditSelectedDistrictsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditCountryMultiselect([FromBody]AddEditCountryByProjectIdCommand model) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditProvinceMultiselect([FromBody]AddEditProvinceByProjectIdCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllDistrictvalueByProvinceId([FromBody]int[] ProvinceId)
        {
            return await _mediator.Send(new GetAllDistrictByProvinceIdQuery { ProvinceId= ProvinceId });
        }

         [HttpPost]
        public async Task<ApiResponse> AddEditProjectotherDetail([FromBody]AddEditProjectOtherDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllSecurityConsiderationDetails()
        {
             return await _mediator.Send(new GetSecurityConsiderationDetailQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetSecurityConsiMultiSelectByProjectId([FromBody]long Id)
        {
             return await _mediator.Send(new GetSecurityConsiderationByProjectIdQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditSecurityConsiMultiselect([FromBody]AddEditSecurityConsiderationCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }
     #endregion

        #region "BudgetLineExcelImport"
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ApiResponse> ExcelImportOfBudgetLine([FromForm] IFormFile fileKey, string projectId)
        {
            ApiResponse apiRespone = new ApiResponse();

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (user != null)
            {
                if (fileKey != null)
                {
                    string fileExtension = Path.GetExtension(fileKey.FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        var stream = fileKey.OpenReadStream();
                        StreamReader reader = new StreamReader(stream);

                        string result = reader.ReadToEnd();

                        var id = user;
                        //var userName = user.UserName;
                        long projectID = Convert.ToInt64(projectId);
                        return await _mediator.Send(new ExcelImportOfBudgetLineQuery {
                            ProjectId= projectID,
                            File= stream,
                            UserId= id
                        });
                    }
                    else
                    {
                        apiRespone.StatusCode = StaticResource.FileNotSupported;
                        apiRespone.Message = StaticResource.FileText;
                    }
                }
            }
            return apiRespone;
        }
        #endregion
    
        #region  "ApprovalProjectDetail"
        [HttpPost]
        public async Task<ApiResponse> AddApprovalProjectDetail([FromBody]AddApprovalProjectDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        #endregion

        #region "Win/loss project approval"

        [HttpPost]
        public async Task<ApiResponse> WinApprovalProjectDetail([FromBody]WinApprovalProjectDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        #endregion

        #region "AddEditProjectproposals"
        /// <summary>
        /// proposal other detail due date ,assign to ,budget
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> AddEditProjectProposalDetail([FromBody]AddEditProjectproposalsCommand command)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var id = user.Id;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.Id = user.Id;
            command.logginUserEmailId = user.Email;
            return await _mediator.Send(command);
        }

        #endregion

        #region "GetProjectproposalsById"
        [HttpPost]
        public async Task<ApiResponse> GetProjectproposalsById([FromBody] long ProjectId)
        {
            return await _mediator.Send(new GetProjectproposalsByIdQuery { ProjectId = ProjectId });
        }
        #endregion

        #region "UploadEDIProposalFile"
        /// <summary>
        /// upload other proposal document using service account credentails new 26/03/2019 poonam
        /// </summary>
        [HttpPost, DisableRequestSizeLimit]

        public async Task<ApiResponse> UploadEDIProposalFile([FromForm] IFormFile filesData,[FromForm] string projectId, [FromForm] string data)
        {

            ApiResponse apiRespone = new ApiResponse();
            string localFolderfullPath = string.Empty;
            try
            {

                var file = Request.Form.Files[0];
                long ProjectId = Convert.ToInt64(projectId);
                string ProposalType = data;
                string fileName = Request.Form.Files[0].FileName;

                string ext = Path.GetExtension(fileName).ToLower();
                if (ext != ".jpeg" && ext != ".png" && ext != ".jpg" && ext != ".gif")
                {
                    var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    if (user != null)
                    {
                        string logginUserEmailId = user.Email;
                      
                        return await _mediator.Send(new UploadEDIProposalFileCommand
                        {  
                            file = file ,
                            ProjectId = ProjectId,
                            fileName = fileName,
                            logginUserEmailId = logginUserEmailId,
                            ProposalType = ProposalType,
                            ext = ext,
                            CreatedById=user.Id,
                            CreatedDate =DateTime.UtcNow,
                            ModifiedById= user.Id,
                            ModifiedDate=DateTime.UtcNow

                        });

                    }
                }
                else
                {
                    apiRespone.StatusCode = StaticResource.FileNotSupported;
                    apiRespone.Message = StaticResource.FileText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiRespone;
        }
        #endregion

        #region "Criteria evaluation form 2nd Aug 2019" 

        #region add/edit
        [HttpPost]
        public async Task<ApiResponse> AddEditDonorCriteria([FromBody]AddEditDonorCriteriaCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditPurposeofInitiativeCriteria([FromBody]AddEditPurposeofInitiativeCriteriaCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }


        [HttpPost]
        public async Task<ApiResponse> AddEditEligibilityCriteriaDetail([FromBody]AddEditEligibilityCriteriaDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditFeasibilityCriteria([FromBody]AddEditFeasibilityCriteriaCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> AddEditPriorityCriteria([FromBody]AddEditPriorityCriteriaCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }


        [HttpPost]
        public async Task<ApiResponse> AddEditFinancialCriteria([FromBody]AddEditFinancialCriteriaCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditRiskCriteria([FromBody]AddEditFinancialCriteriaCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditTargetBeneficiary([FromBody]AddEditTargetBeneficiaryCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditFinanacialProjectDetail([FromBody]AddEditFinancialProjectDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddPriorityOtherDetail([FromBody]AddPriorityOtherDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }


        [HttpPost]
        public async Task<ApiResponse> EditPriorityOtherDetail([FromBody]EditPriorityOtherDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllPriorityOtherDetailList()
        {
            return await _mediator.Send(new GetAllPriorityOtherDetailQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllPriorityOtherDetailByProjectId([FromBody] long ProjectId)
        {
            return await _mediator.Send(new GetAllPriorityOtherDetailByProjectIdQuery { ProjectId = ProjectId });
        }

        [HttpPost]
        public async Task<ApiResponse> DeletePriorityDetails([FromBody]long PriorityOtherDetailId)
        {
            DeletePriorityOtherDetailCommand model = new DeletePriorityOtherDetailCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.PriorityOtherDetailId = PriorityOtherDetailId;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        #endregion

        #region feasibility Expert detail
        [HttpGet]
        public async Task<ApiResponse> GetAllFeasibilityExpertDetailList()
        {
            return await _mediator.Send(new GetAllFeasibilityExpertDetailQuery());
        }


        [HttpPost]
        public async Task<ApiResponse> GetAllExpertDetailByProjectId([FromBody] long projectId)
        {
            return await _mediator.Send(new GetAllExpertDetailByProjectIdQuery { ProjectId = projectId });
        }


        [HttpPost]
        public async Task<ApiResponse> AddFeasibleExpertOtherDetail([FromBody]AddFeasibleExpertOtherDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EditFeasibleExpertOtherDetail([FromBody]EditFeasibilityExpertDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteFeasibleExpertDetails([FromBody]long PriorityOtherDetailId)
        {
            DeleteFeasibilityExperrtDetailsCommand model = new DeleteFeasibilityExperrtDetailsCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ExpertOtherDetailId = PriorityOtherDetailId;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }
 

        #endregion

        #region  "ageGroup" 
        [HttpGet]
        public async Task<ApiResponse> GetAllAgeGroupDetailList()
        {
            return await _mediator.Send(new GetAllAgeGroupDetailQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllAgeGroupByProjectId([FromBody] long projectId)
        {
            return await _mediator.Send(new GetAllAgeGroupByProjectIdQuery { ProjectId = projectId });
        }


        [HttpPost]
        public async Task<ApiResponse> AddAgeGRoupDetail([FromBody]AddAgeGroupDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }


        [HttpPost]
        public async Task<ApiResponse> EditAGeGroupDetail([FromBody]EditAgeGroupDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteAgeGroupDetails([FromBody]long ageGroupOtherDetailId)
        {
            DeleteAgeGroupDetailsCommand model = new DeleteAgeGroupDetailsCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.AgeGroupOtherDetailId = ageGroupOtherDetailId;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }
        #endregion

        #region "assumprtionDetail"
        [HttpGet]
        public async Task<ApiResponse> GetAllAssumptionDetailList()
        {
            return await _mediator.Send(new GetAllAssumptionQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllAssumptionByProjectId([FromBody] long projectId)
        {
            return await _mediator.Send(new GetAllAssumptionByProjectIdQuery { ProjectId = projectId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddAssumptionDetail([FromBody]AddAssumptionDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EditAssumptionDetail([FromBody]EditAssumptionDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }


        [HttpPost]
        public async Task<ApiResponse> DeleteAssumptionDetails([FromBody]long assumptionDetailId)
        {
            DeleteAssumptionDetailsCommand model = new DeleteAssumptionDetailsCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.AssumptionDetailId = assumptionDetailId;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }
        #endregion

        #region "donorEligibilityCriteria"
        [HttpGet]
        public async Task<ApiResponse> GetAllDonorEligibilityDetailList()
        {
            return await _mediator.Send(new GetAllDonorEligibilityDetailQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllDonorEligibilityByProjectId([FromBody] long projectId)
        {
            return await _mediator.Send(new GetAllDonorEligibilityByProjectIdQuery { ProjectId = projectId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddDonorEligibilityDetail([FromBody]AddDonorEligibilityOtherDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditDonorEligibilityDetail([FromBody]EditDonorEligibilityDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteDonorEligibilityDetails([FromBody]long donorEligibilityDetailId)
        {
            DeleteDonorEligibilityDetailsCommand model = new DeleteDonorEligibilityDetailsCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.DonorEligibilityDetailId = donorEligibilityDetailId;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        #endregion

        #region "submit criteria evaluation detail"
        [HttpPost]
        public async Task<ApiResponse> AddEditCriteriaEvaluationSubmit([FromBody]AddEditCriteriaEvaluationSubmitCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> GetIsApprovedCriteriaEvaluationStatus([FromBody] long ProjectId)
        {
            return await _mediator.Send(new GetIsApprovedCriteriaEvaluationStatusQuery { ProjectId = ProjectId });
        }
        #endregion

        #region "GetAllCriteriaEvaluationDetail"

        [HttpPost]
        public async Task<ApiResponse> GetAllCriteriaEvaluationDetail([FromBody] long ProjectId)
        {
            return await _mediator.Send(new GetAllCriteriaEvaluationDetailQuery { ProjectId = ProjectId });
        }

        #endregion

        #endregion
       
        #region "Voucher summary reports"
        [HttpPost]
        public async Task<ApiResponse> GetProjectJobsByProjectIds([FromBody] List<long> projectIds)
        {
            return await _mediator.Send(new GetProjectJobsByMultipleProjectIdsQuery { projectIds = projectIds });
        }

        [HttpPost]
        public async Task<ApiResponse> GetProjectBudgetLinesByProjectJobIds([FromBody] List<long?> projectJobIds)
        {
            return await _mediator.Send(new GetBudgetLinesByMultipleProjectJobIdsQuery { projectJobIds = projectJobIds });
        }
        #endregion

        #region "Project sub activity"
        [HttpPost]
        public async Task<ApiResponse> GetProjectSubActivityDetail([FromBody]int projectId)
        {
            return await _mediator.Send(new GetProjectSubActivityDetailsQuery { projectId = projectId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddProjectSubActivityDetail([FromBody]AddProjectSubActivityDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditProjectSubActivity([FromBody]EditProjectSubActivityDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> ProjectSubActivityIscomplete([FromBody]long activityId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new ProjectSubActivityIscompleteCommand
            {
                activityId = activityId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
            });
        }
        [HttpPost]
        public async Task<ApiResponse> StartProjectSubActivity([FromBody]long activityId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new StartProjectSubActivityCommand
            {
                activityId = activityId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
            });
        }
        [HttpPost]
        public async Task<ApiResponse> EndProjectSubActivity([FromBody]long activityId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new EndProjectSubActivityCommand
            {
                activityId = activityId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
            });
        }
        [HttpPost]
        public async Task<ApiResponse> GetProjectActivityDetailByActivityId([FromBody]long activityId)
        {
            return await _mediator.Send(new GetProjectActivityByActivityIdQuery { activityId = activityId });
        }
        #endregion

        #region"Project activity extension"

        [HttpPost]
        public async Task<ApiResponse> GetProjectActivityExtension([FromBody]long activityId)
        {
            return await _mediator.Send(new GetProjectActivityExtensionQuery { activityId = activityId });
        }
        [HttpPost]
        public async Task<ApiResponse> AddProjectActivityExtension([FromBody]AddProjectActivityExtensionCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditProjectActivityExtension([FromBody]EditProjectActivityExtensionCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteProjectActivityExtension([FromBody]long extensionId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new DeleteProjectActivityExtensionCommand
            {
                extensionId = extensionId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
            });
        }
        #endregion

        #region "Project activity monitoring"
        [HttpPost]
        public async Task<ApiResponse> AddProjectMonitoringReview([FromBody]AddProjectMonitoringReviewCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> GetProjectActivityAdvanceFilterList([FromBody]GetProjectActivityAdvanceFilterListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> GetProjectMonitoringList([FromBody]long activityId)
        {
            return await _mediator.Send(new GetProjectMonitoringListQuery { activityId = activityId });
        }
        [HttpPost]
        public async Task<ApiResponse> GetProjectMonitoringByMonitoringId([FromBody]int Id)
        {
            return await _mediator.Send(new GetProjectMonitoringByMonitoringIdQuery { Id = Id });
        }

        [HttpPost]
        public async Task<ApiResponse> EditProjectMonitoringByMonitoringId([FromBody]EditProjectMonitoringByMonitoringIdCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        #endregion

        #region "GetProjectProposalAmountSummary"

        [HttpPost]
        public async Task<ApiResponse> GetProjectProposalAmountSummary([FromBody]GetProjectProposalAmountSummaryQuery query)
        {
            return await _mediator.Send(query);
        }
        #endregion 

        #region "ProjectIndicators"
        [HttpPost]
        public async Task<ApiResponse> GetAllProjectIndicators([FromBody]GetAllProjectIndicatorsQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> GetProjectIndicatorDetailById([FromBody]long indicatorId)
        {
            return await _mediator.Send(new GetProjectIndicatorDetailByIdQuery { indicatorId = indicatorId });
        }
        [HttpPost]
        public async Task<ApiResponse> AddProjectIndicator()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new AddProjectIndicatorCommand
            {
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow,
            });
        }
        [HttpPost]
        public async Task<ApiResponse> EditProjectIndicator([FromBody]EditProjectIndicatorCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> GetProjectIndicatorQuestionsById([FromBody]long indicatorId)
        {
            return await _mediator.Send(new GetProjectIndicatorQuestionsByIdQuery { indicatorId = indicatorId });
        }

        #endregion

        #region "GetProjectProposalReport"
        [HttpPost]
        public async Task<ApiResponse> GetProjectProposalReport([FromBody]GetProjectProposalReportQuery query)
        {
            return await _mediator.Send(query);
        }

        #endregion

        #region "DeleteActivityDocument"

        [HttpPost]
        public async Task<ApiResponse> DeleteActivityDocument([FromBody]long activityDocumentId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new DeleteActivityDocumentCommand
            {
                activityDocumentId = activityDocumentId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
            });
        }
        #endregion

        #region "DownloadFileFromBucket"

        [HttpPost]
        public async Task<ApiResponse> DownloadFileFromBucket([FromBody]DownloadFileFromBucketQuery query)
        {
            return await _mediator.Send(query);
        }
        #endregion

        #region"UploadFinalizeFile"

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ApiResponse> UploadFinalizeFile([FromForm] IFormFile filesData, [FromForm] UploadFinalizeDragAndDropCommand command)
        {
            ApiResponse apiRespone = new ApiResponse();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            try
            {
                command.file = Request.Form.Files[0];
                command.ProjectId = Convert.ToInt64(command.ProjectId);
                command.FileName = Request.Form.Files[0].FileName;

                command.ext = System.IO.Path.GetExtension(command.FileName).ToLower();
                if (command.ext != ".jpeg" && command.ext != ".png" && command.ext != ".jpg" && command.ext != ".gif")
                {
                    var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    if (user != null)
                    {
                        command.logginUserEmailId = user.Email;
                        return await _mediator.Send(command);
                    }
                }
                else
                {
                    apiRespone.StatusCode = StaticResource.FileNotSupported;
                    apiRespone.Message = StaticResource.FileText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiRespone;
        }

        #endregion

        #region"UploadReviewFile"
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ApiResponse> UploadReviewFile([FromForm] IFormFile filesData, [FromForm] ApproveProjectDetailModel model)
        {
            ApiResponse apiRespone = new ApiResponse();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                if (filesData == null)
                {
                    var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    if (user != null)
                    {
                        return await _mediator.Send(new AddApprovalDetailCommand
                        {
                            ApproveProjrctId = model.ApproveProjrctId,
                            ProjectId = model.ProjectId,
                            CommentText = model.CommentText,
                            FileName = model.FileName,
                            FilePath = model.FilePath,
                            IsApproved = model.IsApproved,
                            UploadedFile = model.UploadedFile,
                            ReviewCompletionDate = model.ReviewCompletionDate,
                            CreatedById = userId,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedById = userId,
                            ModifiedDate = DateTime.UtcNow
                        });
                    }
                    else
                    {
                        apiRespone.StatusCode = StaticResource.FileNotSupported;
                        apiRespone.Message = StaticResource.FileText;
                    }
                }
                else
                {
                    var file = Request.Form.Files[0];
                    long ProjectId = Convert.ToInt64(model.ProjectId);
                    string fileName = Request.Form.Files[0].FileName;

                    string ext = System.IO.Path.GetExtension(fileName).ToLower();
                    if (ext != ".jpeg" && ext != ".png" && ext != ".jpg" && ext != ".gif")
                    {
                        var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                        if (user != null)
                        {
                            string logginUserEmailId = user.Email;
                            // apiRespone = await _iProject.UploadReviewDragAndDrop(file, id, ProjectId, fileName, logginUserEmailId, ext, model);
                            return await _mediator.Send(new UploadReviewDragAndDropCommand
                            {
                                ApproveProjrctId = model.ApproveProjrctId,
                                ProjectId = ProjectId,
                                CommentText = model.CommentText,
                                FileName = fileName,
                                FilePath = model.FilePath,
                                IsApproved = model.IsApproved,
                                UploadedFile = model.UploadedFile,
                                ReviewCompletionDate = model.ReviewCompletionDate,
                                CreatedById = userId,
                                CreatedDate = DateTime.UtcNow,
                                ModifiedById = userId,
                                ModifiedDate = DateTime.UtcNow,
                                logginUserEmailId = logginUserEmailId,
                                file = file,
                                ext = ext,
                            });

                        }
                    }

                    else
                    {
                        apiRespone.StatusCode = StaticResource.FileNotSupported;
                        apiRespone.Message = StaticResource.FileText;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiRespone;
        }

        #endregion

        #region "Start Proposal Drag and Drop PK 26/03/2019" 

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ApiResponse> StartProposalDragAndDropFile([FromForm] IFormFile filesData,[FromForm] string projectId, [FromForm] string data)
        {
            ApiResponse apiRespone = new ApiResponse();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {

                var file = Request.Form.Files[0];
                long ProjectId = Convert.ToInt64(projectId);
                string ProposalType = data;
                string fileName = Request.Form.Files[0].FileName;

                string ext = Path.GetExtension(fileName).ToLower();
                if (ext == ".doc" || ext == ".docx")
                {
                    var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    if (user != null)
                    {
                        string logginUserEmailId = user.Email;

                        return await _mediator.Send(new StartProposalDragAndDropCommand
                        {
                            file = file,
                            ProjectId = ProjectId,
                            FileName = fileName,
                            ProposalType = ProposalType,
                            ext = ext,
                            logginUserEmailId = logginUserEmailId,
                            CreatedById = userId,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedById = userId,
                            ModifiedDate = DateTime.UtcNow
                        });
                    }
                }
                else
                {
                    apiRespone.StatusCode = StaticResource.FileNotSupported;
                    apiRespone.Message = StaticResource.FileText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiRespone;
        }

        #endregion

        #region "Demo Upload File"

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ApiResponse> UploadFileDemo([FromForm] IFormFile fileData, string activityId, string statusId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new UploadFileDemoCommand
            {
                fileData = fileData,
                activityId = activityId,
                statusId = statusId,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }
        #endregion

        #region "FilterProjectCashFlow"

        [HttpPost]
        public async Task<ApiResponse> FilterProjectCashFlow([FromBody]FilterProjectCashFlowQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<ApiResponse> FilterBudgetLineBreakdown([FromBody]FilterBudgetLineBreakdownQuery query)
        {
            return await _mediator.Send(query);
        }
        #endregion

        #region Upload Files for Activity Documents 28/03/2019

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ApiResponse> UploadProjectDocumnentFile([FromForm] IFormFile filesData, string activityId, string statusId, string monitoringId)
        {
            ApiResponse apiRespone = new ApiResponse();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string localFolderfullPath1 = string.Empty;
            try
            {
                long monitoringID = monitoringId != null ? Convert.ToInt64(monitoringId) : 0;
                var file = Request.Form.Files[0];
                long activityID = Convert.ToInt64(activityId);
                int statusID = Convert.ToInt32(statusId);
                string fileName = Request.Form.Files[0].FileName;
                string ext = Path.GetExtension(fileName).ToLower();
                if (ext != ".jpeg" && ext != ".png" && ext != ".jpg" && ext != ".gif")
                {
                    var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    if (user != null)
                    {
                        string logginUserEmailId = user.Email;
                        return await _mediator.Send(new UploadProjectActivityDocumentFileCommand
                        {
                            File = file,
                            MonitoringID = monitoringID,
                            ActivityID = activityID,
                            StatusID = statusID,
                            FileName = fileName,
                            Ext = ext,
                            CreatedById = userId,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedById = userId,
                            ModifiedDate = DateTime.UtcNow
                        });
                    }
                }
                else
                {
                    apiRespone.StatusCode = StaticResource.FileNotSupported;
                    apiRespone.Message = StaticResource.FileText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiRespone;
        }

        [HttpPost]
        public async Task<ApiResponse> GetActivityDocumentDetails([FromBody]GetUploadedDocumentsQuery query)
        {
            return await _mediator.Send(query);
        }

        #endregion

        #region ProjectActivity

        [HttpPost]
        public async Task<ApiResponse> GetProjectActivityDetail([FromBody]long id)
        {
            return await _mediator.Send(new GetallProjectActivityDetailQuery { ProjectId = id });
        }

        [HttpPost]
        public async Task<ApiResponse> AddProjectActivityDetail([FromBody]AddProjectActivityDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EditProjectActivityDetail([FromBody]EditProjectActivityDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteActivityDetail([FromBody]long activityId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new DeleteProjectActivityCommand
            {
                ActivityId = activityId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
            });
        }
        [HttpPost]
        public async Task<ApiResponse> AllActivityStatus([FromBody]long projectId) 
        {
            return await _mediator.Send(new AllProjectActivityStatusQuery { ProjectId = projectId });
        }
        #endregion

        #region BudgetLine Detail
        [HttpPost]
        public async Task<ApiResponse> AddBudgetLineDetail([FromBody]AddEditProjectBudgetLineDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpGet]
        public async Task<ApiResponse> GetProjectBudgetLineDetail()
        {
            return await _mediator.Send(new GetallBudgetLineDetailQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetProjectBudgetLineDetail([FromBody] long projectId)
        {
            return await _mediator.Send(new GetallBudgetLineDetailByIdQuery { ProjectId = projectId });
        }
        [HttpPost]
        public async Task<ApiResponse> GetBudgetLineDetailByBudgetId([FromBody] int budgetId)
        {
            return await _mediator.Send(new GetBudgetLineDetailByBudgetIdQuery { BudgetId = budgetId });
        }
        [HttpPost]
        public async Task<ApiResponse> GetAllBudgetLineList([FromBody]GetAllBudgetFilterListQuery query, long projectId)
        {
            //query.ProjectId = projectId;
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> GetTransactionListByProjectId([FromBody] long projectId)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var UserName = user.UserName;
            return await _mediator.Send(new GetTransactionListByProjectIdQuery
            {
                ProjectId = projectId,
                UserName = UserName
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetTransactionList([FromBody]GetTransactionListQuery query)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var UserName = user.UserName;
            return await _mediator.Send(query);
        }
        #endregion

        #region ProjectJob

        [HttpGet]
        public async Task<ApiResponse> GetProjectJobDetail()
        {
            return await _mediator.Send(new GetAllProjectJobDetailQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetProjectJobDetailByProjectId([FromBody] long projectId)
        {
            return await _mediator.Send(new GetProjectJobDetailByProjectIdQuery { ProjectId = projectId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddProjectJobDetail([FromBody]AddEditProjectJobDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteProjectJob([FromBody]long jobId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteProjectJobCommand
            {
                JobId = jobId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetProjectJobDetailByProjectJobId([FromBody] long projectJobId)
        {
            return await _mediator.Send(new GetAllProjectJobByProjectIdQuery { ProjectJobId = projectJobId });
        }
        [HttpPost]
        public async Task<ApiResponse> GetAllProjectJobFilterList([FromBody]GetAllProjectJobsFilterListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> GetProjectJobDetailByBudgetLineId([FromBody] long budgetLineId)
        {
            return await _mediator.Send(new GetProjectJobDetailByBudgetLineIdQuery { BudgetLineId = budgetLineId });
        }
        #endregion

        #region "Create and Download Excel format"
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateAndDownloadExcelFormat() 
        {

            ApiResponse apiRespone = new ApiResponse();
            string fileName;
            fileName = "ExcellData.xlsx";
            var file = new FileInfo(fileName);
            using (var package = new OfficeOpenXml.ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "Attempts");
                worksheet = package.Workbook.Worksheets.Add("Assessment Attempts");
                worksheet.Row(1).Height = 15;

                //worksheet.TabColor = Color.Gold;
                worksheet.DefaultRowHeight = 15;
                worksheet.Row(1).Height = 15;

                worksheet.Cells[1, 1].Value = "ProjectId";
                worksheet.Cells[1, 2].Value = "ProjectJobCode";
                worksheet.Cells[1, 3].Value = "ProjectJobName";
                worksheet.Cells[1, 4].Value = "BudgetCode";
                worksheet.Cells[1, 5].Value = "BudgetName";
                worksheet.Cells[1, 6].Value = "InitialBudget";
                worksheet.Cells[1, 7].Value = "CurrencyId";
                worksheet.Cells[1, 8].Value = "CurrencyName";

                var cells = worksheet.Cells["A1:J1"];

                //worksheet.Column(1).AutoFit();
                //worksheet.Column(2).AutoFit();
                //worksheet.Column(3).AutoFit();
                //worksheet.Column(4).AutoFit();
                //worksheet.Column(5).AutoFit();
                //worksheet.Column(6).AutoFit();
                //worksheet.Column(7).AutoFit();
                //worksheet.Column(8).AutoFit();


                package.Workbook.Properties.Title = "Attempts";
                var FileBytesArray = package.GetAsByteArray();
                return File(
                   fileContents: FileBytesArray,
                   contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                   fileDownloadName: "Budget-Line-Excel-Import-Sample.xlsx"
               );
            }         
        }
        #endregion

        #region "Upload File Using Signed Url"
        [HttpPost]
        public async Task<ApiResponse> UploadDemoUsingBSignedUrlBucket([FromBody]DownloadFileFromBucketCommand command)
        {
            return await _mediator.Send(command);
        }
        #endregion
    }
}