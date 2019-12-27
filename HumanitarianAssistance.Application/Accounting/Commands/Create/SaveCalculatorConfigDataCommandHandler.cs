using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class SaveCalculatorConfigDataCommandHandler : IRequestHandler<SaveCalculatorConfigDataCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public SaveCalculatorConfigDataCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(SaveCalculatorConfigDataCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                GainLossCalculatorConfiguration savedData = _dbContext.GainLossCalculatorConfiguration
                                                               .FirstOrDefault(x => x.IsDeleted == false && x.UserId == request.UserId);
                if (savedData != null)
                {
                    savedData.CreditAccountId = request.CreditAccount;
                    savedData.DebitAccountId = request.DebitAccount;
                    savedData.CurrencyId = request.CurrencyId;
                    savedData.StartDate = request.StartDate;
                    savedData.EndDate = request.EndDate;
                    savedData.ComparisionDate = request.ComparisionDate;
                    savedData.UserId = request.UserId;
                    savedData.ModifiedById = request.UserId;
                    savedData.ModifiedDate = DateTime.UtcNow;

                    _dbContext.GainLossCalculatorConfiguration.Update(savedData);
                    await _dbContext.SaveChangesAsync();
                    success = true;
                }
                else
                {
                    savedData = new GainLossCalculatorConfiguration
                    {
                        CreditAccountId = request.CreditAccount,
                        DebitAccountId = request.DebitAccount,
                        CurrencyId = request.CurrencyId,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        ComparisionDate = request.ComparisionDate,
                        UserId = request.UserId,
                        CreatedById = request.UserId,
                        CreatedDate = DateTime.UtcNow,
                    };

                    await _dbContext.GainLossCalculatorConfiguration.AddAsync(savedData);
                    await _dbContext.SaveChangesAsync();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}