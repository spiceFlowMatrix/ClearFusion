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

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditExitInterviewCommandHandler : IRequestHandler<EditExitInterviewCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public EditExitInterviewCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(EditExitInterviewCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                ExistInterviewDetails obj = await _dbContext.ExistInterviewDetails.FirstOrDefaultAsync(x => x.ExistInterviewDetailsId == request.ExistInterviewDetailsId);
                obj.ModifiedById = request.ModifiedById;
                obj.ModifiedDate = request.ModifiedDate;
                _mapper.Map(request, obj);
                await _dbContext.SaveChangesAsync();                
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
