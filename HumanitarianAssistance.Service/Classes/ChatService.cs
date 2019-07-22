using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class ChatService : IChat
    {


        IUnitOfWork _uow;

        public ChatService(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        #region "AddMessage"
        public async Task<APIResponse> AddMessage(ChatModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (string.IsNullOrEmpty(model.Message) && string.IsNullOrWhiteSpace(model.Message))
                {
                    throw new Exception(StaticResource.ChatMessageEmpty);
                }

                ChatDetail chatDetail = new ChatDetail
                {
                    CreatedDate = DateTime.UtcNow,
                    CreatedById = model.CreatedById,
                    IsDeleted= false,
                    ChatSourceEntityId = model.ChatSourceEntityId,
                    EntityId = model.EntityId,
                    Message = model.Message
                };

                await _uow.GetDbContext().ChatDetail.AddAsync(chatDetail);
                await _uow.GetDbContext().SaveChangesAsync();

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
        #endregion

        #region "EditMessage"
        public async Task<APIResponse> EditMessage(ChatModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                ChatDetail chatDetail = await _uow.GetDbContext()
                                                  .ChatDetail
                                                  .FirstOrDefaultAsync(x => x.IsDeleted == false
                                                   && x.ChatId == model.ChatId);

                if (chatDetail != null)
                {
                    throw new Exception(StaticResource.ChatNotFound);
                }

                chatDetail.ModifiedDate = DateTime.UtcNow;
                chatDetail.ModifiedById = model.ModifiedById;
                chatDetail.ChatSourceEntityId = model.ChatSourceEntityId;
                chatDetail.EntityId = model.EntityId;
                chatDetail.Message = model.Message;

                _uow.GetDbContext().ChatDetail.Update(chatDetail);
                await _uow.GetDbContext().SaveChangesAsync();

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
        #endregion

        #region "DeleteMessage"
        public async Task<APIResponse> DeleteMessage(long chatId, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                ChatDetail chatDetail = await _uow.GetDbContext()
                                                  .ChatDetail
                                                  .FirstOrDefaultAsync(x => x.IsDeleted == false
                                                   && x.ChatId == chatId);

                if (chatDetail != null)
                {
                    throw new Exception(StaticResource.ChatNotFound);
                }

                chatDetail.ModifiedDate = DateTime.UtcNow;
                chatDetail.ModifiedById = userId;
                chatDetail.IsDeleted = true;

                _uow.GetDbContext().ChatDetail.Update(chatDetail);
                await _uow.GetDbContext().SaveChangesAsync();

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
        #endregion

        #region "ListMessages"
        public async Task<APIResponse> ListMessages(ChatModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                List<ChatModel> chatDetailList = await _uow.GetDbContext()
                                                  .ChatDetail
                                                  .Where(x => x.IsDeleted == false
                                                   && x.ChatSourceEntityId == model.ChatSourceEntityId
                                                   && x.EntityId == model.EntityId)
                                                   .Select(x=> new ChatModel {
                                                       ChatId= x.ChatId,
                                                       ChatSourceEntityId= x.ChatSourceEntityId,
                                                       EntityId= x.EntityId,
                                                       Message= x.Message
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
        #endregion
    }
}
