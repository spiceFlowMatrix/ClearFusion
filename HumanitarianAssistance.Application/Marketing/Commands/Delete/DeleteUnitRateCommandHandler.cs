using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
        public class DeleteUnitRateCommandHandler : IRequestHandler<DeleteUnitRateCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteUnitRateCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteUnitRateCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
            {
                var mediumDetails = await _dbContext.UnitRates.FirstOrDefaultAsync(x => x.IsDeleted == false && x.UnitRateId == request.UnitRateId);
                if (mediumDetails != null)
                {
                    mediumDetails.ModifiedById = request.ModifiedById;
                    mediumDetails.ModifiedDate = request.ModifiedDate;
                    mediumDetails.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Unit Rate deleted Successfully";
                }
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