using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddExitInterviewCommandHandler : IRequestHandler<AddExitInterviewCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddExitInterviewCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(AddExitInterviewCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                ExistInterviewDetails obj = _mapper.Map<ExistInterviewDetails>(request);
                obj.CreatedById = request.CreatedById;
                obj.CreatedDate = request.CreatedDate;
                obj.IsDeleted = false;
                await _dbContext.ExistInterviewDetails.AddAsync(obj);
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
