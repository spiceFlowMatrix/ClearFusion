using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteDocumentDetailCommandHandler : IRequestHandler<DeleteDocumentDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public DeleteDocumentDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteDocumentDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                EmployeeDocumentDetail documentinfo = await _dbContext.EmployeeDocumentDetail.FirstOrDefaultAsync(x => x.DocumentID == request.DocumentId && 
                                                                                                                       x.IsDeleted == false);
                if (documentinfo != null)
                {
                    documentinfo.IsDeleted = true;
                    documentinfo.ModifiedById = request.ModifiedById;
                    documentinfo.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.failStatusCode;
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