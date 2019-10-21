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
    public class AddEmployeeHealthQuestionCommandHandler : IRequestHandler<AddEmployeeHealthQuestionCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddEmployeeHealthQuestionCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddEmployeeHealthQuestionCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                EmployeeHealthQuestion obj = new EmployeeHealthQuestion
                {
                    EmployeeId = request.EmployeeId,
                    Question = request.Question,
                    Answer = request.Answer,

                    IsDeleted = false,
                    CreatedById = request.CreatedById,
                    CreatedDate = DateTime.Now
                };


                await _dbContext.EmployeeHealthQuestion.AddAsync(obj);
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