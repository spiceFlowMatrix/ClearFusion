using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditLeaveReasonDetailCommandHandler : IRequestHandler<EditLeaveReasonDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public EditLeaveReasonDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(EditLeaveReasonDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                LeaveReasonDetail leavereasoninfo = await _dbContext.LeaveReasonDetail.FirstOrDefaultAsync(x => x.LeaveReasonId == request.LeaveReasonId);
                if (leavereasoninfo != null)
                {
                    leavereasoninfo.ReasonName = request.ReasonName;
                    leavereasoninfo.Unit = request.Unit;
                    leavereasoninfo.ModifiedById = request.ModifiedById;
                    leavereasoninfo.ModifiedDate = request.ModifiedDate;
                    leavereasoninfo.IsDeleted = request.IsDeleted;

                    await _dbContext.SaveChangesAsync();
                    
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
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