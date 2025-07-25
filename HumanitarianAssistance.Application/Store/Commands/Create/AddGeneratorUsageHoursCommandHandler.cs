using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddGeneratorUsageHoursCommandHandler : IRequestHandler<AddGeneratorUsageHoursCommand, bool>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddGeneratorUsageHoursCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(AddGeneratorUsageHoursCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                if (request != null)
                {

                    GeneratorUsageHourDetail usageHours = await _dbContext.GeneratorUsageHourDetail.Include(x=> x.PurchasedGeneratorDetail)
                                                                            .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                        x.GeneratorId == request.GeneratorId &&
                                                                                        x.Month.Month == request.Month.Month);

                    if (usageHours == null)
                    {
                         var generator = await _dbContext.PurchasedGeneratorDetail.FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                        x.Id == request.GeneratorId);

                        if (generator != null)
                        {
                            if (request.Month.Month < generator.CreatedDate.Value.Month &&
                            request.Month.Year <= generator.CreatedDate.Value.Year)
                            {
                                throw new Exception(StaticResource.MileageMonthNotValid);
                            }
                        }

                        usageHours = new GeneratorUsageHourDetail
                        {
                            IsDeleted = false,
                            CreatedDate = DateTime.UtcNow,
                            CreatedById = request.CreatedById,
                            GeneratorId = request.GeneratorId,
                            Month = request.Month,
                            Hours = request.Hours
                        };

                        await _dbContext.GeneratorUsageHourDetail.AddAsync(usageHours);

                        //log details
                        StoreLogger logger = new StoreLogger
                        {
                            CreatedDate = DateTime.UtcNow,
                            CreatedById = request.CreatedById,
                            IsDeleted = false,
                            EventType= "Usage Added",
                            LogText= $"{request.Hours} Hour added to this generator",
                            TransportType= (int)TransportItemCategory.Generator,
                            TransportTypeEntityId= request.GeneratorId
                        };

                        await _dbContext.StoreLogger.AddAsync(logger);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        usageHours.ModifiedDate = DateTime.UtcNow;
                        usageHours.ModifiedById = request.CreatedById;
                        usageHours.GeneratorId = request.GeneratorId;
                        usageHours.Month = request.Month;
                        usageHours.Hours = request.Hours;

                        _dbContext.GeneratorUsageHourDetail.Update(usageHours);

                        //log details
                        StoreLogger logger = new StoreLogger
                        {
                            CreatedDate = DateTime.UtcNow,
                            CreatedById = request.CreatedById,
                            IsDeleted = false,
                            EventType= "Usage Updated",
                            LogText= $"{request.Hours} Hour added to this generator",
                            TransportType= (int)TransportItemCategory.Generator,
                            TransportTypeEntityId= request.GeneratorId
                        };

                        await _dbContext.StoreLogger.AddAsync(logger);
                        await _dbContext.SaveChangesAsync();
                    }
                    success = true;
                }
                else
                {
                    throw new Exception(StaticResource.RequestValuesInAppropriate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}