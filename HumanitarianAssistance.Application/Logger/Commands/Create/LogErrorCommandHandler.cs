using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Logger.Commands.Create
{
    public class LogErrorCommandHandler: IRequestHandler<LogErrorCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public LogErrorCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(LogErrorCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            ErrorLogger logger = new ErrorLogger
            {
                Message = request.Message,
                InnerException = request.InnerException,
                Path = request.Path,
                StackTrace = request.StackTrace
            };

            await _dbContext.AddAsync(logger);
            await _dbContext.SaveChangesAsync();

            return response;
        }
    }
}