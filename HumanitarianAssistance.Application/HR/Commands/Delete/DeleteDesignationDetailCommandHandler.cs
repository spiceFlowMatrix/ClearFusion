using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteDesignationDetailCommandHandler : IRequestHandler<DeleteDesignationDetailCommand, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteDesignationDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(DeleteDesignationDetailCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            try
            {
                DesignationDetail obj = new DesignationDetail
                {
                    DesignationId = request.Id,
                    IsDeleted = true,
                    ModifiedById = request.ModifiedById,
                    ModifiedDate = request.ModifiedDate
                };
                _dbContext.Entry(obj).State = EntityState.Modified;
                var technicalquestionList = await _dbContext.TechnicalQuestion.Where(x => x.IsDeleted == false && x.DesignationId == request.Id)
                                                                              .ToListAsync();

                if (technicalquestionList.Any())
                {
                    foreach (var item in technicalquestionList)
                    {
                        item.IsDeleted = true;
                        item.ModifiedById = request.ModifiedById;
                        item.ModifiedDate = request.ModifiedDate;
                    }
                    await _dbContext.SaveChangesAsync();
                }
                await _dbContext.SaveChangesAsync();
                success = true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return success;

        }

    }
}