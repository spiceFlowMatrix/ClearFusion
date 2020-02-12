using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class RemoveEmployeeLanguagesCommandHandler : IRequestHandler<RemoveEmployeeLanguagesCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IActionLogService _actionLog; 
        public RemoveEmployeeLanguagesCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper,IActionLogService actionLog)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _actionLog = actionLog;
        }

        public async Task<ApiResponse> Handle(RemoveEmployeeLanguagesCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                EmployeeLanguages existRecord = await _dbContext.EmployeeLanguages.FirstOrDefaultAsync(x => x.IsDeleted == false && x.SpeakLanguageId == request.SpeakLanguageId);

                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = request.ModifiedById;
                    existRecord.ModifiedDate = DateTime.Now;
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";

                     AuditLogModel logs = new AuditLogModel () {
                    EmployeeId = (int) existRecord.EmployeeId,
                    TypeOfEntity = (int) TypeOfEntity.History,
                    EntityId = null,
                    ActionTypeId = (int) ActionType.Delete,
                    ActionDescription = (TypeOfEntity.History).GetDescription (),
                };
                bool isLoged = await _actionLog.AuditLog (logs);
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
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
