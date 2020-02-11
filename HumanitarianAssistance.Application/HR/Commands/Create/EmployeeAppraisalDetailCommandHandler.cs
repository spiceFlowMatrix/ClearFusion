using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System;
using HumanitarianAssistance.Persistence;


namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class EmployeeAppraisalDetailCommandHandler : IRequestHandler<EmployeeAppraisalDetailCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public EmployeeAppraisalDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(EmployeeAppraisalDetailCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            try
            {

            }
            catch(Exception ex){
                throw ex;
            }
            return success;
        }
    }
}