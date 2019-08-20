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

namespace HumanitarianAssistance.Application.Project.Commands.Create
{

    public class AddHiringRequestCandidateCommandHandler : IRequestHandler<AddHiringRequestCandidateCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddHiringRequestCandidateCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddHiringRequestCandidateCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeExist = await _dbContext.HiringRequestCandidates
                                              .FirstOrDefaultAsync(x => x.EmployeeID == request.EmployeeID
                                              && x.HiringRequestId == request.HiringRequestId
                                              && x.IsDeleted == false);

                if (employeeExist == null)
                {
                    HiringRequestCandidates candidateDeatil = new HiringRequestCandidates()
                    {
                        HiringRequestId = request.HiringRequestId,
                        EmployeeID = request.EmployeeID,
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = false,

                    };
                    await _dbContext.HiringRequestCandidates.AddAsync(candidateDeatil);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Candidate already exists with a job in project");
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
