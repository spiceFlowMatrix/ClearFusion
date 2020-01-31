using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddAdvanceRecoveryCommandHandler : IRequestHandler<AddAdvanceRecoveryCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddAdvanceRecoveryCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<object> Handle(AddAdvanceRecoveryCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var advanceRecord = await _dbContext.Advances
                                                 .FirstOrDefaultAsync(x => x.AdvancesId == request.AdvanceId);

                    double recoveredAmount = _dbContext.AdvanceHistoryDetail.Where(x => x.IsDeleted == false && x.AdvanceId == request.AdvanceId && x.PaymentDate.Month <= request.Month)
                                                      .Select(x => x.InstallmentPaid).DefaultIfEmpty(0).Sum();

                    AdvanceHistoryDetail advanceRecordExist = await _dbContext.AdvanceHistoryDetail.FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                    x.PaymentDate.Month == request.Month && x.EmployeeId == request.EmployeeId);

                    if (advanceRecordExist == null)
                    {
                        AdvanceHistoryDetail advanceHistory = new AdvanceHistoryDetail
                        {
                            AdvanceId = request.AdvanceId,
                            CreatedById = request.CreatedById,
                            CreatedDate = DateTime.UtcNow,
                            InstallmentPaid = request.Amount,
                            EmployeeId = request.EmployeeId,
                            InstallmentBalance = advanceRecord.AdvanceAmount- (recoveredAmount + request.Amount),
                            IsDeleted = false,
                            PaymentDate = DateTime.UtcNow
                        };

                        await _dbContext.AdvanceHistoryDetail.AddAsync(advanceHistory);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        advanceRecordExist.InstallmentPaid = request.Amount;
                        advanceRecordExist.InstallmentBalance = advanceRecord.AdvanceAmount- (recoveredAmount + request.Amount);
                        advanceRecordExist.ModifiedById = request.CreatedById;
                        advanceRecordExist.ModifiedDate = DateTime.UtcNow;
                        _dbContext.AdvanceHistoryDetail.Update(advanceRecordExist);
                        await _dbContext.SaveChangesAsync();
                    }

                    if (advanceRecord.AdvanceAmount == (recoveredAmount + request.Amount))
                    {
                        advanceRecord.IsDeducted = true;
                        _dbContext.Advances.Update(advanceRecord);
                        await _dbContext.SaveChangesAsync();
                    }

                    tran.Commit();
                    success = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            return success;
        }
    }
}