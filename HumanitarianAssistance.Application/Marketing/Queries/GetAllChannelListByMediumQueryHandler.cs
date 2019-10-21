using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
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
    public class GetAllChannelListByMediumQueryHandler : IRequestHandler<GetAllChannelListByMediumQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllChannelListByMediumQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllChannelListByMediumQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var Channels = await(from j in _dbContext.Channel
                                     where !j.IsDeleted && j.MediumId == request.model
                                     select (new ChannelModel
                                     {
                                         ChannelId = j.ChannelId,
                                         ChannelName = j.ChannelName,
                                         MediumId = j.MediumId
                                     })).ToListAsync();
                Channel obj = await _dbContext.Channel.AsQueryable().Where(x => x.IsDeleted == false && x.MediumId == request.model).FirstOrDefaultAsync();
                ICollection<Medium> Mediums = await _dbContext.Mediums.Where(x => x.IsDeleted == false).ToListAsync();
                response.data.channelById = obj;
                response.data.ChannelList = Channels;
                response.data.Mediums = Mediums;
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
