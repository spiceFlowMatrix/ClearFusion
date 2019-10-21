using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetAllChannelListQueryHandler : IRequestHandler<GetAllChannelListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllChannelListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllChannelListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var Channels = await(from j in _dbContext.Channel
                                     join jp in _dbContext.Mediums on j.MediumId equals jp.MediumId
                                     where !j.IsDeleted && !jp.IsDeleted
                                     select (new ChannelModel
                                     {
                                         ChannelId = j.ChannelId,
                                         ChannelName = j.ChannelName,
                                         MediumId = j.MediumId,
                                         MediumName = jp.MediumName
                                     })).ToListAsync();
                //ICollection<Channel> Channels = await _uow.ChannelRepository.FindAllAsync(x => x.IsDeleted == false);
                response.data.ChannelList = Channels;
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
