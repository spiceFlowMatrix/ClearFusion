using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllDesignationDetailQueryHandler : IRequestHandler<GetAllDesignationDetailQuery, object>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllDesignationDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAllDesignationDetailQuery request, CancellationToken cancellationToken)
        {
            ModelList list = new ModelList();

            try
            {

                var query = _dbContext.DesignationDetail
                                        .Include(x => x.TechnicalQuestion)
                                       .Where(x => x.IsDeleted == false)
                                       .Select(x => new DesignationTechnicalQuestionModel
                                       {
                                           DesignationId = x.DesignationId,
                                           Designation = x.Designation,
                                           Description = x.Description,
                                           TechnicalQuestionList = x.TechnicalQuestion!= null? (x.TechnicalQuestion.Where(z => z.IsDeleted == false).Select(c => new TechnicalQuestionModel
                                           {
                                               QuestionId = c.TechnicalQuestionId,
                                               Question = c.Question

                                           }).ToList()) : null
                                       }).AsQueryable();

                list.RecordCount = await query.CountAsync();

                list.DesignationList = await query.Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }
    }
}