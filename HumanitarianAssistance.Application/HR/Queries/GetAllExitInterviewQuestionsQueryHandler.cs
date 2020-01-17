using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllExitInterviewQuestionsQueryHandler: IRequestHandler<GetAllExitInterviewQuestionsQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllExitInterviewQuestionsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<object> Handle(GetAllExitInterviewQuestionsQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result= new Dictionary<string, object>();
            object queryResult;
            var query = _dbContext.ExitInterviewQuestionsMaster.OrderByDescending(x=> x.Id).Where(x => x.IsDeleted == false).Select(x => new
            {
                Id = x.Id,
                QuestionText = x.QuestionText,
                SequencePosition= x.SequencePosition,
                QuestionType= x.QuestionType
            }).AsQueryable();

            long count = await query.CountAsync();

            if(request.IsPaginated) {
                queryResult= await query.Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToListAsync();
            } else {
                queryResult= await query.ToListAsync();
            }
            result.Add("RecordCount", count);
            result.Add("Result", queryResult);
            
            return result;
        }
    }
}