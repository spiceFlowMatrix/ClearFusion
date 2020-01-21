using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddOpeningPensionDetailCommandHandler : IRequestHandler<AddOpeningPensionDetailCommand, bool>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddOpeningPensionDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddOpeningPensionDetailCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            try
            {
                List<MultiCurrencyOpeningPension> pensionDetail = new List<MultiCurrencyOpeningPension>();

                if (request.PensionDetail != null)
                {

                    foreach (var item in request.PensionDetail)
                    {
                        MultiCurrencyOpeningPension detail = new MultiCurrencyOpeningPension()
                        {
                            EmployeeID = request.EmployeeID,
                            PensionStartDate = request.PensionDate,
                            Amount = item.Amount,
                            CreatedById = request.CreatedById,
                            CreatedDate = request.CreatedDate,
                            IsDeleted = false,
                            CurrencyId = item.CurrencyId,
                        };

                        pensionDetail.Add(detail);
                    }
                    await _dbContext.MultiCurrencyOpeningPension.AddRangeAsync(pensionDetail);
                    await _dbContext.SaveChangesAsync();
                    success = true;

                }
            }

            catch (Exception ex)
            {
                throw (ex);
            }
            return success;
        }
    }
}