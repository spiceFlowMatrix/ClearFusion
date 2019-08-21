using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Application.Configuration.Models;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllLeaveReasonQueryHandler : IRequestHandler<GetAllLeaveReasonQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllLeaveReasonQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllLeaveReasonQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<LeaveReasonDetail> leavelist = await _dbContext.LeaveReasonDetail.Where(x => x.IsDeleted == false).ToListAsync();

                if (leavelist != null)
                {
                    List<LeaveReasonDetailModel> leaveReasonlist = leavelist.Select(x => new LeaveReasonDetailModel
                    {
                        LeaveReasonId = x.LeaveReasonId,
                        ReasonName = x.ReasonName,
                        Unit = x.Unit
                    }).OrderBy(x => x.ReasonName).ToList();

                    response.data.LeaveReasonList = leaveReasonlist;
                }
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