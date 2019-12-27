using System;
using System.Threading;
using System.Threading.Tasks;
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
                

            }
            catch(Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}