using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddHolidayDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long HolidayId { get; set; }
        public string HolidayName { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
		public int? FinancialYearId { get; set; }
        public int? OfficeId { get; set; }
        public int HolidayType { get; set; }
        public List<RepeatWeeklyDay> RepeatWeeklyDay { get; set; }
    }
}