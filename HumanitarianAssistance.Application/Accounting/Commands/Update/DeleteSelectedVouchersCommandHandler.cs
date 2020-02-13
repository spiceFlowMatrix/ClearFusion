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
    public class DeleteSelectedVouchersCommandHandler: IRequestHandler<DeleteSelectedVouchersCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public DeleteSelectedVouchersCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(DeleteSelectedVouchersCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                List<VoucherDetail> vouchers = await _dbContext.VoucherDetail.Where(x=> x.IsDeleted == false &&
                                                                request.VoucherNoList.Contains(x.VoucherNo)).ToListAsync();

                foreach(var item in vouchers)
                {
                    item.IsDeleted = true;
                    item.ModifiedDate= DateTime.UtcNow;
                    item.ModifiedById = request.ModifiedById;
                }

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