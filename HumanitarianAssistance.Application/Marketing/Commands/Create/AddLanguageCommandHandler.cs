using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Create
{
        public class AddLanguageCommandHandler : IRequestHandler<AddLanguageCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddLanguageCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(AddLanguageCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
            {
                LanguageDetail obj = new LanguageDetail();
                _mapper.Map(request,obj);
                obj.CreatedById = request.CreatedById;
                obj.CreatedDate = request.CreatedDate;
                obj.IsDeleted = false;
                obj.LanguageName = request.LanguageName;
                await _dbContext.LanguageDetail.AddAsync(obj);
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