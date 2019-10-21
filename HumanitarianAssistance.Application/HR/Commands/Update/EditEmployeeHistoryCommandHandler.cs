using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeHistoryCommandHandler : IRequestHandler<EditEmployeeHistoryCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
       
        public EditEmployeeHistoryCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(EditEmployeeHistoryCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                EmployeeHistoryDetail historyinfo = await _dbContext.EmployeeHistoryDetail.FirstOrDefaultAsync(x => x.HistoryID == request.HistoryID && x.IsDeleted == false);
                if (historyinfo != null)
                {
                    historyinfo.HistoryDate = request.HistoryDate;
                    historyinfo.Description = request.Description;
                    historyinfo.ModifiedById = request.ModifiedById;
                    historyinfo.ModifiedDate = request.ModifiedDate;
                    historyinfo.IsDeleted = request.IsDeleted;
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
