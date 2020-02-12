using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class VerifySelectedVouchersCommandHandler: IRequestHandler<VerifySelectedVouchersCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public VerifySelectedVouchersCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(VerifySelectedVouchersCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                List<VoucherDetail> vouchers = await _dbContext.VoucherDetail.Where(x=> x.IsDeleted == false && request.VoucherNos.Contains(x.VoucherNo)).ToListAsync();

                vouchers.ForEach(x=> x.IsVoucherVerified = true);

                _dbContext.VoucherDetail.UpdateRange(vouchers);
                await _dbContext.SaveChangesAsync();
                success = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}