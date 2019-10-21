using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddEmailSettingCommandHandler: IRequestHandler<AddEmailSettingCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEmailSettingCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext= dbContext;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(AddEmailSettingCommand model, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                EmailSettingDetail obj = _mapper.Map<EmailSettingDetail>(model);
                obj.CreatedById = model.CreatedById;
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;
                await _dbContext.EmailSettingDetail.AddAsync(obj);
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