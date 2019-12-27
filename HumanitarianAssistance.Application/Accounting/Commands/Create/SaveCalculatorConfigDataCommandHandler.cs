using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class SaveCalculatorConfigDataCommandHandler: IRequestHandler<SaveCalculatorConfigDataCommand, object>
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

                GainLossSelectedAccounts savedDate = _dbContext.GainLossSelectedAccounts
                                                               .FirstOrDefault(x=> x.IsDeleted == false && x.UserId == request.)


            }
            catch(Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}