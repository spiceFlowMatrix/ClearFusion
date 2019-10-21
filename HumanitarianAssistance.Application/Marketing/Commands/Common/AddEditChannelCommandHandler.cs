using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditChannelCommandHandler : IRequestHandler<AddEditChannelCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditChannelCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditChannelCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.ChannelId == 0)
                {
                    if (request.MediumId == null || request.MediumId == 0)
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Select a Medium";
                    }
                    else
                    {
                        var ob = await _dbContext.Channel.FirstOrDefaultAsync(x => x.ChannelName == request.ChannelName && x.IsDeleted == false);
                        if (ob != null)
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = "Channel already exists";
                        }
                        else
                        {
                            Channel obj = new Channel();                            
                            obj.CreatedById = request.CreatedById;
                            obj.CreatedDate = DateTime.Now;
                            obj.IsDeleted = false;
                            obj.ChannelName = request.ChannelName;
                            obj.MediumId = request.MediumId;
                            _mapper.Map(request, obj);
                            await _dbContext.Channel.AddAsync(obj);
                            await _dbContext.SaveChangesAsync();
                            response.data.channelById = obj;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Channel added Successfully";
                        }
                    }
                }
                else
                {
                    Channel obj1 = await _dbContext.Channel.FirstOrDefaultAsync(x => x.ChannelId == request.ChannelId);
                    obj1.ModifiedById = request.ModifiedById;
                    obj1.ModifiedDate = DateTime.Now;
                    obj1.MediumId = request.MediumId;
                    _mapper.Map(request, obj1);
                    await _dbContext.SaveChangesAsync();
                    response.data.channelById = obj1;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Channel updated successfully";
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
