using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEmployeeHistoryByIdQueryHandler : IRequestHandler<GetAllEmployeeHistoryByIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IActionLogService _actionLog;

        public GetAllEmployeeHistoryByIdQueryHandler(HumanitarianAssistanceDbContext dbContext, IActionLogService actionLog)
        {
            _dbContext = dbContext;
            _actionLog = actionLog;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeeHistoryByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<EmployeeHistoryDetailModel> employeehistorylist = await _dbContext.EmployeeHistoryDetail.Where(x => x.EmployeeID == request.EmployeeId)
               

                .Select(x => new EmployeeHistoryDetailModel
                {
                    HistoryID = x.HistoryID,
                    HistoryDate = x.HistoryDate != null ? Convert.ToDateTime(x.HistoryDate) : x.HistoryDate,
                    Description = x.Description
                }).ToListAsync();
                response.data.EmployeeHistoryDetailList = employeehistorylist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";

                AuditLogModel logs = new AuditLogModel () {
                    EmployeeId = (int)request.EmployeeId,
                    TypeOfEntityName = TypeOfEntity.History.ToString()
                };
                _actionLog.HRMAuditLogService(logs,request);
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