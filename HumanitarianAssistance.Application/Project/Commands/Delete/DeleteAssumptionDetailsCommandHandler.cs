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
    public class DeleteAssumptionDetailsCommandHandler : IRequestHandler<DeleteAssumptionDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteAssumptionDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteAssumptionDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                CEAssumptionDetail expertInfo = await _dbContext.CEAssumptionDetail.FirstOrDefaultAsync(c => c.AssumptionDetailId == request.AssumptionDetailId &&
                                                                                             c.IsDeleted == false);
                if (expertInfo != null)
                {
                    expertInfo.IsDeleted = true;
                    expertInfo.ModifiedById = request.ModifiedById;
                    expertInfo.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
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
    }
}
