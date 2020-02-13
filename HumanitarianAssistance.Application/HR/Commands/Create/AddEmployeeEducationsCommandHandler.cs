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

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEmployeeEducationsCommandHandler : IRequestHandler<AddEmployeeEducationsCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IActionLogService _actionLog;
        public AddEmployeeEducationsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper,IActionLogService actionLog)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _actionLog = actionLog;
        }

        public async Task<ApiResponse> Handle(AddEmployeeEducationsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                EmployeeEducations obj = _mapper.Map<EmployeeEducations>(request);
                obj.IsDeleted = false;
                obj.CreatedById = request.CreatedById;
                obj.CreatedDate = DateTime.Now;
                await _dbContext.EmployeeEducations.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                  AuditLogModel logs = new AuditLogModel () {
                        EmployeeId = (int)obj.EmployeeID,
                        TypeOfEntity = (int) TypeOfEntity.History,
                        EntityId = null,
                        ActionTypeId = (int) ActionType.Add,
                        ActionDescription = (TypeOfEntity.History).GetDescription(),
                    };
                    bool isLoged = await _actionLog.AuditLog (logs);
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