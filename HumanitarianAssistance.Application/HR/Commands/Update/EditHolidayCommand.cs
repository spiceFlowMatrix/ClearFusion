using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditHolidayCommand : BaseModel, IRequest<object>
    {
        public EditHolidayCommand()
        {
            RepeatWeeklyDay = new List<EditRepeatWeeklyDayModel>();
        }
        public long HolidayId { get; set; }
        public string HolidayName { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int FinancialYearId { get; set; }
        public int? OfficeId { get; set; }
        public int HolidayType { get; set; }
        public List<EditRepeatWeeklyDayModel> RepeatWeeklyDay { get; set; }
    }
    public class EditRepeatWeeklyDayModel
    {
        public string Day { get; set; }
    }
}