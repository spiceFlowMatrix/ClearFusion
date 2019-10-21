using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteLanguageCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
            {
                var languageDetails = await _dbContext.LanguageDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.LanguageId == request.LanguageId);
                if (languageDetails != null)
                {
                    languageDetails.ModifiedById = request.ModifiedById;
                    languageDetails.ModifiedDate = request.ModifiedDate;
                    languageDetails.IsDeleted = true;
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