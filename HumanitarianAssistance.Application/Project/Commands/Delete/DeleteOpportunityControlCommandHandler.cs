using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
        public class DeleteOpportunityControlCommandHandler : IRequestHandler<DeleteOpportunityControlCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteOpportunityControlCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteOpportunityControlCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();

            try
            {
                var opportunityDetail = await _dbContext.ProjectOpportunityControl
                                                  .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.id);

                if (opportunityDetail == null)
                {
                    throw new Exception(StaticResource.OpportunityControlNotfound);
                }

                opportunityDetail.ModifiedDate = request.ModifiedDate;
                opportunityDetail.ModifiedById = request.ModifiedById;
                opportunityDetail.IsDeleted = true;

                await _dbContext.SaveChangesAsync();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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
