using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddHolidayCommandHandler : BaseModel, IRequestHandler<AddHolidayCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddHolidayCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<object> Handle(AddHolidayCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            try
            {
                var financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);
                if (financialyear != null)
                {
                    List<HolidayWeeklyDetails> holidayDetail = await _dbContext.HolidayWeeklyDetails.Where(x => x.IsDeleted == false).ToListAsync();
                    if(holidayDetail.Any())
                    {
                        holidayDetail.ForEach(x=> x.IsDeleted = true);
                        await _dbContext.SaveChangesAsync();
                    }
                    if (request.HolidayType == (int)HolidayType.REPEATWEEKLYDAY)
                    {
                        List<HolidayWeeklyDetails> holidayweeklylist = new List<HolidayWeeklyDetails>();
                        
                        foreach (var hlist in request.RepeatWeeklyDay)
                        {
                                HolidayWeeklyDetails list = new HolidayWeeklyDetails();
                                list.Day = hlist.Day;
                                // list.OfficeId = (int)office;
                                list.FinancialYearId = financialyear.FinancialYearId;
                                list.CreatedById = request.CreatedById;
                                list.CreatedDate = request.CreatedDate;
                                list.IsDeleted = false;
                                holidayweeklylist.Add(list);
                        }
                        await _dbContext.HolidayWeeklyDetails.AddRangeAsync(holidayweeklylist);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        List<HolidayDetails> existHolidayRecord = await _dbContext.HolidayDetails.Where(x => x.Date.Date == request.Date.Date &&
                                                                                                      x.IsDeleted == false && x.FinancialYearId == financialyear.FinancialYearId).ToListAsync();
                        List<HolidayDetails> listObj = new List<HolidayDetails>();
                        // edit the list according to office
                        if (existHolidayRecord.Any())
                        {
                            if (request.OfficeId.Any())
                            {
                                foreach (var item in request.OfficeId)
                                {
                                    // check the existing record for office 
                                    var check = existHolidayRecord.FindAll(x => x.OfficeId == item);
                                    if (check.Count == 0)
                                    {
                                        HolidayDetails obj = new HolidayDetails()
                                        {
                                            HolidayName = request.HolidayName,
                                            Date = request.Date,
                                            Remarks = request.Remarks,
                                            FinancialYearId = financialyear.FinancialYearId,
                                            OfficeId = item,
                                            HolidayType = request.HolidayType,
                                            IsDeleted = false,
                                            CreatedById = request.CreatedById,
                                            CreatedDate = request.CreatedDate
                                        };
                                        listObj.Add(obj);
                                    }
                                    // else
                                    // {
                                    //     throw new Exception("Holiday Details already exist for this ofice.");
                                    // }

                                }

                                _dbContext.HolidayDetails.AddRange(listObj);
                                await _dbContext.SaveChangesAsync();

                                success = true;
                            }

                        }
                        else
                        {
                            // add new record if no data found for the selected date
                            if (request.OfficeId.Any())
                            {
                                foreach (var item in request.OfficeId)
                                {

                                    HolidayDetails obj = new HolidayDetails()
                                    {
                                        HolidayName = request.HolidayName,
                                        Date = request.Date,
                                        Remarks = request.Remarks,
                                        FinancialYearId = financialyear.FinancialYearId,
                                        OfficeId = item,
                                        HolidayType = request.HolidayType,
                                        IsDeleted = false,
                                        CreatedById = request.CreatedById,
                                        CreatedDate = request.CreatedDate
                                    };
                                    listObj.Add(obj);
                                }
                                _dbContext.HolidayDetails.AddRange(listObj);
                                await _dbContext.SaveChangesAsync();

                            }
                        }
                    }
                }
                else
                {
                    throw new Exception(StaticResource.defaultFinancialYearIsNotSet);
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