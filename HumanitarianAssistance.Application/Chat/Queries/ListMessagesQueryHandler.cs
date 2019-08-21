using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Chat.Models;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Chat.Queries
{
    public class ListMessagesQueryHandler : IRequestHandler<ListMessagesQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public ListMessagesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(ListMessagesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<ChatModel> chatDetailList = await _dbContext
                                                  .ChatDetail
                                                  .Where(x => x.IsDeleted == false
                                                   && x.ChatSourceEntityId == request.ChatSourceEntityId
                                                   && x.EntityId == request.EntityId)
                                                   .Select(x => new ChatModel
                                                   {
                                                       ChatId = x.ChatId,
                                                       ChatSourceEntityId = x.ChatSourceEntityId,
                                                       EntityId = x.EntityId,
                                                       Message = x.Message,
                                                       EntitySourceDocumentId = x.EntitySourceDocumentId
                                                   }).ToListAsync();

                response.ResponseData = chatDetailList;
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
