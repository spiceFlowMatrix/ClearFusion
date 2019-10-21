using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeHealthQuestionCommandHandler : IRequestHandler<EditEmployeeHealthQuestionCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public EditEmployeeHealthQuestionCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(EditEmployeeHealthQuestionCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existRecord = await _dbContext.EmployeeHealthQuestion.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeHealthQuestionId == request.EmployeeHealthQuestionId);
                if (existRecord != null)
                {
                    existRecord.Question = request.Question;
                    existRecord.Answer = request.Answer;

                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = request.ModifiedById;
                    existRecord.ModifiedDate = DateTime.Now;
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
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