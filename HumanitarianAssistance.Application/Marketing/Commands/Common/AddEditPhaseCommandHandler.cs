using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditPhaseCommandHandler : IRequestHandler<AddEditPhaseCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditPhaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditPhaseCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.JobPhaseId == 0 || request.JobPhaseId == null)
                {
                    JobPhase obj = new JobPhase();                    
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.IsDeleted = false;
                    obj.Phase = request.Phase;
                    _mapper.Map(request, obj);
                    await _dbContext.JobPhases.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.phaseById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Phase added Successfully";
                }
                else
                {
                    JobPhase obj = await _dbContext.JobPhases.FirstOrDefaultAsync(x => x.JobPhaseId == request.JobPhaseId);
                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;
                    _mapper.Map(request, obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.phaseById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Phase updated successfully";
                }
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
