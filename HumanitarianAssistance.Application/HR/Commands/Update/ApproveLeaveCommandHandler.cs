using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveLeaveCommandHandler: IRequestHandler<ApproveLeaveCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public ApproveLeaveCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(ApproveLeaveCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}