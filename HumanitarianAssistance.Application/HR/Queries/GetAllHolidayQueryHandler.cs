using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllHolidayQueryHandler : IRequestHandler<GetAllHolidayQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAllHolidayQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<object> Handle(GetAllHolidayQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {

                FinancialYearDetail financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);

                if (financialyear != null)
                {
                    var queryData = _dbContext.HolidayDetails
                                                      .Include(x => x.OfficeDetails)
                                                      .Where(x => x.IsDeleted == false &&
                                                                  x.FinancialYearId == financialyear.FinancialYearId && x.HolidayType == (int)HolidayType.PARTICULARDATE).AsQueryable();


                    var holidaylist = queryData.Skip(request.PageIndex * request.PageSize).Take(request.PageSize)
                                                                          .OrderByDescending(x=>x.HolidayId)
                                                                          .Select(y => new
                                                                          {
                                                                              HolidayId = y.HolidayId,
                                                                              HolidayName = y.HolidayName,
                                                                              DateToDisplay = y.Date.ToShortDateString(),
                                                                              Date = y.Date,
                                                                              Remarks = y.Remarks,
                                                                              FinancialYearId = y.FinancialYearId,
                                                                              OfficeId = y.OfficeId,
                                                                              HolidayType = y.HolidayType,
                                                                              OfficeName = y.OfficeDetails.OfficeName
                                                                          }).ToList();

                    int totalCount = queryData.Count();
                    response.Add("holidaylist", holidaylist);
                    response.Add("totalCount", totalCount);

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
            return response;


        }
    }
}