using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Marketing;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
        public class GetAllMediumListQueryHandler : IRequestHandler<GetAllMediumListQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetAllMediumListQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ApiResponse> Handle(GetAllMediumListQuery request, CancellationToken cancellationToken)
            {
            ApiResponse response = new ApiResponse();
            try
            {
                ICollection<Medium> Mediums = await _dbContext.Mediums.AsNoTracking().AsQueryable().Where(x => x.IsDeleted == false).ToListAsync();
                ICollection<Channel> Channels = await _dbContext.Channel.AsNoTracking().AsQueryable().Where(x => x.IsDeleted == false).ToListAsync();
                if (Channels.Count > 0)
                {
                    var channelById = Channels.Where(x => x.MediumId == Mediums.First().MediumId).FirstOrDefault();
                    response.data.channelById = channelById;
                    response.data.Channels = Channels;
                }
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
