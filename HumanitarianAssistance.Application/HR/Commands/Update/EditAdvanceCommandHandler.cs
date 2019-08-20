using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditAdvanceCommandHandler: IRequestHandler<EditAdvanceCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditAdvanceCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditAdvanceCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var record = await _dbContext.Advances.FirstOrDefaultAsync(x => x.AdvancesId == request.AdvancesId && x.IsDeleted == false);
                if (record != null)
                {
                    record.AdvanceDate = request.AdvanceDate;
                    record.AdvanceAmount = request.AdvanceAmount;
                    record.ApprovedBy = request.ApprovedBy;
                    record.CurrencyId = request.CurrencyId;
                    record.EmployeeId = request.EmployeeId;
                    record.ModeOfReturn = request.ModeOfReturn;
                    record.ModifiedById = request.ModifiedById;
                    record.ModifiedDate = request.ModifiedDate;
                    record.OfficeId = request.OfficeId;
                    record.NumberOfInstallments = request.NumberOfInstallments;
                    record.RequestAmount = request.RequestAmount.Value;
                    record.VoucherReferenceNo = request.VoucherReferenceNo.Value;
                    _dbContext.Advances.Update(record);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Update Failed";
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