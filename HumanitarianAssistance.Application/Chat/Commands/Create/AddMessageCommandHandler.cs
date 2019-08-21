using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Chat.Commands.Create
{
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public AddMessageCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (string.IsNullOrEmpty(request.Message) && string.IsNullOrWhiteSpace(request.Message))
                {
                    throw new Exception(StaticResource.ChatMessageEmpty);
                }

                ChatDetail chatDetail = new ChatDetail
                {
                    CreatedDate = DateTime.UtcNow,
                    CreatedById = request.CreatedById,
                    IsDeleted = false,
                    ChatSourceEntityId = request.ChatSourceEntityId,
                    EntityId = request.EntityId,
                    Message = request.Message,
                    EntitySourceDocumentId = request.EntitySourceDocumentId
                };

                await _dbContext.ChatDetail.AddAsync(chatDetail);
                await _dbContext.SaveChangesAsync();

                UserDetails user = await _dbContext.UserDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.AspNetUserId == request.CreatedById);

                request.ChatId = chatDetail.ChatId;
                request.UserName = user.Username;

                response.ResponseData = request;
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
