using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditDesignationDetailCommandHandler : IRequestHandler<EditDesignationDetailCommand, object>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditDesignationDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(EditDesignationDetailCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var result = await _dbContext.DesignationDetail
                                           .Include(x => x.TechnicalQuestion).Where(y => y.IsDeleted == false)
                                           .FirstOrDefaultAsync(x => x.IsDeleted == false && x.DesignationId == request.Id);

                    if (result == null)
                    {
                        throw new Exception(StaticResource.RecordNotFound);
                    }

                    result.Designation = request.DesignationName;
                    result.Description = request.Description;
                    result.ModifiedDate = DateTime.UtcNow;
                    result.ModifiedById = request.ModifiedById;

                    _dbContext.DesignationDetail.Update(result);
                    await _dbContext.SaveChangesAsync();

                    if (request.Questions.Any())
                    {
                        List<int> questionToBeRemoved = result.TechnicalQuestion
                                                                  .Select(x => x.TechnicalQuestionId)
                                                                  .Except(request.Questions
                                                                   .Where(x => x.QuestionId != 0)
                                                                   .Select(x => x.QuestionId)).ToList();

                        foreach (int id in questionToBeRemoved)
                        {
                            TechnicalQuestion question = result.TechnicalQuestion.FirstOrDefault(x => x.TechnicalQuestionId == id);

                            if (question != null)
                            {
                                question.IsDeleted = true;
                                question.ModifiedById = request.ModifiedById;
                                question.ModifiedDate = request.ModifiedDate;

                                _dbContext.TechnicalQuestion.Update(question);
                                await _dbContext.SaveChangesAsync();
                            }
                        }

                        foreach (var item in request.Questions)
                        {
                            // Add new Question
                            if (item.QuestionId == 0)
                            {
                                TechnicalQuestion question = new TechnicalQuestion()
                                {
                                    CreatedById = request.ModifiedById,
                                    CreatedDate = DateTime.UtcNow,
                                    Question = item.Question,
                                    IsDeleted = false,
                                    DesignationId = request.Id.Value
                                };

                                await _dbContext.TechnicalQuestion.AddAsync(question);
                                await _dbContext.SaveChangesAsync();
                            }
                            else // Edit Existing Question
                            {
                                TechnicalQuestion question = result.TechnicalQuestion.FirstOrDefault(x => x.IsDeleted == false && x.TechnicalQuestionId == item.QuestionId);

                                if (question != null)
                                {
                                    question.ModifiedDate = DateTime.UtcNow;
                                    question.ModifiedById = request.ModifiedById;
                                    question.Question = item.Question;

                                    _dbContext.TechnicalQuestion.Update(question);
                                    await _dbContext.SaveChangesAsync();
                                }
                            }
                        }      
                    }

                    tran.Commit();
                    success = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

            return success;
        }
    }
}