using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{


    public class VerifyVoucherCommandHandler : IRequestHandler<VerifyVoucherCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public VerifyVoucherCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(VerifyVoucherCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var voucherDetail = await _dbContext.VoucherDetail.FirstOrDefaultAsync(x => x.VoucherNo == request.VoucherId);
                if (voucherDetail != null)
                {
                    voucherDetail.IsVoucherVerified = !voucherDetail.IsVoucherVerified;
                    voucherDetail.ModifiedById = request.ModifiedById;
                    voucherDetail.ModifiedDate = DateTime.UtcNow;

                    await _dbContext.SaveChangesAsync();

                    response.data.IsVoucherVerified = voucherDetail.IsVoucherVerified;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = voucherDetail.IsVoucherVerified ? StaticResource.VoucherVerified : StaticResource.VoucherUnVerified;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.VoucherNotPresent;
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