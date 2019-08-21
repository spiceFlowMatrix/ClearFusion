using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEmployeeHistoryCommandHandler : IRequestHandler<DeleteEmployeeHistoryCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public DeleteEmployeeHistoryCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(DeleteEmployeeHistoryCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var historyinfo = await _dbContext.EmployeeHistoryDetail.FirstOrDefaultAsync(x => x.HistoryID == request.HistoryId && x.IsDeleted == false);
                if (historyinfo != null)
                {
                    historyinfo.IsDeleted = true;
                    historyinfo.ModifiedById = request.ModifiedById;
                    historyinfo.ModifiedDate = DateTime.UtcNow;
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