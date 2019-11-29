using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetSequenceNumberQueryHandler: BaseModel, IRequestHandler<GetSequenceNumberQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetSequenceNumberQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetSequenceNumberQuery request, CancellationToken cancellationToken)
        {
            int sequenceNo=1;

            try
            {
                var result = await _dbContext.ExitInterviewQuestionsMaster
                                       .Where(x=> x.IsDeleted == false &&
                                       x.QuestionType == request.QuestionType).OrderByDescending(x=> x.SequencePosition)
                                       .FirstOrDefaultAsync();

                if(result != null)
                {
                    sequenceNo= ++result.SequencePosition;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return sequenceNo;
        }
    }
}