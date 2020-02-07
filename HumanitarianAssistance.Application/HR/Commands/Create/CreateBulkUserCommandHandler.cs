using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Application.CommonServicesInterface;
namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class CreateBulkUserCommandHandler : IRequestHandler<CreateBulkUserCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IHRService _hrService;
        public CreateBulkUserCommandHandler(HumanitarianAssistanceDbContext dbContext, IHRService hrService)
        {
            _dbContext = dbContext;
            _hrService = hrService;
        }
        public async Task<object> Handle(CreateBulkUserCommand request, CancellationToken cancellationToken)
        {
            await _hrService.AddBulkUser();
            return true;
        }
    }
}