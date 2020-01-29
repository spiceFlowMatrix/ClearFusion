using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddHolidayCommand : BaseModel, IRequest<object>
    {

        public AddHolidayCommand()
        {
            OfficeId = new List<int?>();
            RepeatWeeklyDay = new List<RepeatWeeklyDayModel>();
        }
        public long HolidayId { get; set; }
        public string HolidayName { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int FinancialYearId { get; set; }
        public List<int?> OfficeId { get; set; }
        public int HolidayType { get; set; }
        public List<RepeatWeeklyDayModel> RepeatWeeklyDay { get; set; }
    }
    public class RepeatWeeklyDayModel
    {
        public string Day { get; set; }
    }
}