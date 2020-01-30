using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditHolidayCommandHandler : BaseModel, IRequestHandler<EditHolidayCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public EditHolidayCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        public async Task<object> Handle(EditHolidayCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            try
            {
                var financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);

                if (request.HolidayType == (int)HolidayType.REPEATWEEKLYDAY)
                {

                }
                else
                {
                    var existrecord = await _dbContext.HolidayDetails.FirstOrDefaultAsync(x => x.HolidayId == request.HolidayId);

                    if (existrecord != null)
                    {
                        existrecord.HolidayName = request.HolidayName;
                        existrecord.Remarks = request.Remarks;
                        existrecord.ModifiedById = request.ModifiedById;
                        existrecord.ModifiedDate = request.ModifiedDate;
                        existrecord.IsDeleted = false;
                        _dbContext.HolidayDetails.Update(existrecord);
                        await _dbContext.SaveChangesAsync();

                        success = true;
                    }
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