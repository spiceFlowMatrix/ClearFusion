using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteDonorEligibilityDetailsCommandHandler : IRequestHandler<DeleteDonorEligibilityDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteDonorEligibilityDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteDonorEligibilityDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                DonorEligibilityCriteria donorEligibilityInfo = await _dbContext.DonorEligibilityCriteria.FirstOrDefaultAsync(c => c.DonorEligibilityDetailId == request.DonorEligibilityDetailId &&
                                                                                                                                   c.IsDeleted == false);

                donorEligibilityInfo.IsDeleted = true;
                donorEligibilityInfo.ModifiedById = request.ModifiedById;
                donorEligibilityInfo.ModifiedDate = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
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
    }
}
