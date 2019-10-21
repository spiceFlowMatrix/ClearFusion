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

namespace HumanitarianAssistance.Application.Marketing.Commands.Update
{
    public class EditLanguageCommandHandler: IRequestHandler<EditLanguageCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public EditLanguageCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(EditLanguageCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
            {
                LanguageDetail obj = await _dbContext.LanguageDetail.FirstOrDefaultAsync(x => x.LanguageId == request.LanguageId);
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