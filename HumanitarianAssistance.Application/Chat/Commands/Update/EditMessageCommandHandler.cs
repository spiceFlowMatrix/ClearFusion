using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Chat.Commands.Update
{
    public class EditMessageCommandHandler : IRequestHandler<EditMessageCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public EditMessageCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditMessageCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ChatDetail chatDetail = await _dbContext
                                                  .ChatDetail
                                                  .FirstOrDefaultAsync(x => x.IsDeleted == false
                                                   && x.ChatId == request.ChatId);

                if (chatDetail == null)
                {
                    throw new Exception(StaticResource.ChatNotFound);
                }

                chatDetail.ModifiedDate = request.ModifiedDate;
                chatDetail.ModifiedById = request.ModifiedById;
                chatDetail.ChatSourceEntityId = request.ChatSourceEntityId;
                chatDetail.EntityId = request.EntityId;
                chatDetail.Message = request.Message;
                chatDetail.EntitySourceDocumentId = request.EntitySourceDocumentId;                
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
