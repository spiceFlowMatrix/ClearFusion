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
    public class AddEmployeeLanguagesCommandHandler : IRequestHandler<AddEmployeeLanguagesCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddEmployeeLanguagesCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddEmployeeLanguagesCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                EmployeeLanguages employeeLanguages = new EmployeeLanguages
                {
                    EmployeeId = request.EmployeeId,
                    LanguageId = request.LanguageId,
                    Listening = request.Listening,
                    Reading = request.Reading,
                    Speaking = request.Speaking,
                    Writing = request.Writing,

                    IsDeleted = false,
                    CreatedById = request.CreatedById,
                    CreatedDate = DateTime.Now
                };

                await _dbContext.EmployeeLanguages.AddAsync(employeeLanguages);
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