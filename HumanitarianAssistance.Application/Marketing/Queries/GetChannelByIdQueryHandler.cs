using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetChannelByIdQueryHandler : IRequestHandler<GetChannelByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetChannelByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async  Task<ApiResponse> Handle(GetChannelByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                Channel obj = await _dbContext.Channel.FirstOrDefaultAsync(x => x.ChannelId == request.model && x.IsDeleted == false);
                ICollection<Medium> Mediums = await _dbContext.Mediums.Where(x => x.IsDeleted == false).ToListAsync();
                response.data.channelById = obj;
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
