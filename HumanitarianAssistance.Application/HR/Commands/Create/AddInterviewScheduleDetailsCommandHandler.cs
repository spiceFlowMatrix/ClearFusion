using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddInterviewScheduleDetailsCommandHandler : IRequestHandler<AddInterviewScheduleDetailsCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddInterviewScheduleDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddInterviewScheduleDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                foreach (var i in request.InterViewSchedule)
                {
                    i.CreatedById=request.CreatedById;
                    i.CreatedDate = request.CreatedDate;
                    i.IsDeleted = false;
                    i.InterviewStatus = 1;   //Alpit
                    InterviewScheduleDetails obj = _mapper.Map<InterviewScheduleDetails>(i);
                    await _dbContext.InterviewScheduleDetails.AddAsync(obj);
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
