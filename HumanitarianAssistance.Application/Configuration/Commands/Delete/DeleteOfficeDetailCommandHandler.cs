using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete {
    public class DeleteOfficeDetailCommandHandler : IRequestHandler<DeleteOfficeDetailCommand, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public DeleteOfficeDetailCommandHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle (DeleteOfficeDetailCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();

            try {
                OfficeDetail officeInfo = await _dbContext.OfficeDetail.FirstOrDefaultAsync (c => c.OfficeId == request.OfficeId);

                if (officeInfo != null) {
                    officeInfo.IsDeleted = true;
                    officeInfo.ModifiedById = request.ModifiedById;
                    officeInfo.ModifiedDate = request.ModifiedDate;
                    _dbContext.OfficeDetail.Update (officeInfo);
                    await _dbContext.SaveChangesAsync ();
                    if (officeInfo.IsDeleted == true) {
                        var dep = await _dbContext.Department.Where (c => c.OfficeId == request.OfficeId).ToListAsync ();
                        if (dep.Count > 0) {
                            foreach (var item in dep) {

                                item.IsDeleted = true;
                                item.ModifiedById = request.ModifiedById;
                                item.ModifiedDate = request.ModifiedDate;
                                await _dbContext.SaveChangesAsync ();
                            }
                        }
                    }
                };

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }

            return response;
        }
    }
}