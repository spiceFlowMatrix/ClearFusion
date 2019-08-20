using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditHiringRequestCandidateCommandHandler : IRequestHandler<EditHiringRequestCandidateCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditHiringRequestCandidateCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditHiringRequestCandidateCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeExist = await _dbContext.HiringRequestCandidates.FirstOrDefaultAsync(x => x.EmployeeID == request.EmployeeID &&
                                                                                                               x.IsDeleted == false && x.HiringRequestId == request.HiringRequestId);
                if (employeeExist != null)
                {
                    employeeExist.HiringRequestId = request.HiringRequestId;
                    employeeExist.EmployeeID = request.EmployeeID;
                    employeeExist.ModifiedById = request.ModifiedById;
                    employeeExist.ModifiedDate = request.ModifiedDate;
                    employeeExist.IsDeleted = false;
                    employeeExist.IsShortListed = request.IsShortListed;                    
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Candidate Not exists");
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
