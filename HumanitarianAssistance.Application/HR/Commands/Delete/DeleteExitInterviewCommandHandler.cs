using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteExitInterviewCommandHandler : IRequestHandler<DeleteExitInterviewCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteExitInterviewCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeleteExitInterviewCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var exitInterviewRecord = await _dbContext.ExistInterviewDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ExistInterviewDetailsId == request.existInterviewDetailsId);
                if (exitInterviewRecord != null)
                {
                    exitInterviewRecord.ModifiedById = request.ModifiedById;
                    exitInterviewRecord.ModifiedDate = request.ModifiedDate;
                    exitInterviewRecord.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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
