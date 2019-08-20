using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditOfficeDetailCommandHandler: IRequestHandler<EditOfficeDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditOfficeDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditOfficeDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                var existoffice = await _dbContext.OfficeDetail.FirstOrDefaultAsync(o => o.OfficeId != request.OfficeId && o.OfficeCode == request.OfficeCode); //use only OfficeCode

                if (existoffice == null)
                {
                    var officeInfo = await _dbContext.OfficeDetail.FirstOrDefaultAsync(c => c.OfficeId == request.OfficeId);
                    
                    officeInfo.OfficeCode = request.OfficeCode;
                    officeInfo.OfficeName = request.OfficeName;
                    officeInfo.SupervisorName = request.SupervisorName;
                    officeInfo.PhoneNo = request.PhoneNo;
                    officeInfo.FaxNo = request.FaxNo;
                    officeInfo.ModifiedById = request.ModifiedById;
                    officeInfo.ModifiedDate = request.ModifiedDate;

                    _dbContext.OfficeDetail.Update(officeInfo);
                    await _dbContext.SaveChangesAsync();
                    
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                    response.Message = StaticResource.MandateNameAlreadyExist;
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