using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddCandidateAsEmployeeCommandHandler: IRequestHandler<AddCandidateAsEmployeeCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IHRService _hrService;

        public AddCandidateAsEmployeeCommandHandler(HumanitarianAssistanceDbContext dbContext, IHRService hrService)
        {
            _dbContext = dbContext;
            _hrService = hrService;
        }

        public async Task<ApiResponse> Handle(AddCandidateAsEmployeeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                 var model = await _dbContext.HiringRequestCandidateStatus
                                                        .Include(x => x.CandidateDetails)
                                                        .Include(x=> x.ProjectHiringRequestDetail)
                                                        .FirstOrDefaultAsync(x => x.CandidateId == request.CandidateId && 
                                                                            x.IsDeleted == false && x.HiringRequestId == request.HiringRequestId &&
                                                                            x.ProjectId == request.ProjectId);

                AddNewEmployeeCommand command = new AddNewEmployeeCommand
                {
                    EmployeeName =  model.CandidateDetails.FirstName + " "+ model.CandidateDetails.LastName,
                    Email = model.CandidateDetails.Email,
                    Phone= model.CandidateDetails.PhoneNumber,
                    SexId= model.CandidateDetails.GenderId,
                    CountryId= model.CandidateDetails.CountryId,
                    ProvinceId= model.CandidateDetails.ProvinceId,
                    HiredOn= DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                   // District= model.CandidateDetails.DistrictID,
                   // ExperienceYear= model.CandidateDetails.RelevantExperienceInYear,
                   Password= model.CandidateDetails.Password,
                   DateOfBirth = model.CandidateDetails.DateOfBirth.ToShortDateString(),
                   ProfessionId = model.CandidateDetails.ProfessionId,
                   PreviousWork= model.CandidateDetails.PreviousWork,
                   CurrentAddress= model.CandidateDetails.CurrentAddress,
                   PermanentAddress= model.CandidateDetails.PermanentAddress,
                   ExperienceYear= model.CandidateDetails.ExperienceYear,
                   ExperienceMonth= model.CandidateDetails.ExperienceMonth,
                   Remarks= model.CandidateDetails.Remarks,
                   OfficeId = model.ProjectHiringRequestDetail.OfficeId.Value,
                   EmployeeTypeId = (int)EmployeeTypeStatus.Active
                };
                response = await _hrService.AddNewEmployee(command);
                if(response.StatusCode == 200) {
                var candidateDetail = await _dbContext.CandidateDetails.Where(x=>x.CandidateId==request.CandidateId && x.IsDeleted==false).FirstOrDefaultAsync();
                if(candidateDetail !=null)
                {
                candidateDetail.EmployeeID = response.data.EmployeeDetailModel.EmployeeID;
                await _dbContext.SaveChangesAsync();
                }
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch(Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}