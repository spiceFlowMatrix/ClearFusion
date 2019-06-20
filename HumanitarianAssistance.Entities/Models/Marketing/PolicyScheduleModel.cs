﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class PolicyScheduleModel
    {
        public long PolicyScheduleId { get; set; }
        public long? PolicyId { get; set; }
        public string Title { get; set; }
        public string RepeatDays { get; set; }
        public string Description { get; set; }
        public int? Frequency { get; set; }
        public int? ByMonth { get; set; }
        public int? ByWeek { get; set; }
        public int? ByDay { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime EndDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string ScheduleCode { get; set; }
    }

    public class ScheduleDetailsModel
    {
        public long PolicyScheduleId { get; set; }
        public long? PolicyId { get; set; }
        public string Title { get; set; }
        public string[] RepeatDays { get; set; }
        public string Description { get; set; }
        public int? Frequency { get; set; }
        public int? ByMonth { get; set; }
        public int? ByWeek { get; set; }
        public int? ByDay { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string EndDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }

    public class PolicyTimeScheduleModel
    {
        public long Id { get; set; }
        public long? PolicyId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool IsActive { get; set; }
        //public long? PolicyId { get; set; }
        public string[] RepeatDays { get; set; }
        public List<string> repeatDays { get; set; }
        //public string StartTime { get; set; }
        //public string EndTime { get; set; }
    }

    public class RepeatDaysModel
    {
        public string id { get; set; }
        public string Value { get; set; }
        public bool status { get; set; }
    }

     public class PolicyTimeModel
     {
        public long? PolicyId { get; set; }
        public List<RepeatDaysModel> RepeatDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public long Id { get; set; }
     }

    public class PolicyDayScheduleModel
    {
        public long? PolicyId { get; set; }
        public long Id { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }

    public class PolicyOrderScheduleModel
    {
        public long Id { get; set; }
        public long? PolicyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class SchedulerModel
    {
        public long ScheduleId { get; set; }
        public string ScheduleType { get; set; }
        public string ScheduleCode { get; set; }
        public string ScheduleName { get; set; }
        public long? PolicyId { get; set; }
        public long? ProjectId { get; set; }
        public long? JobId { get; set; }
        public string Name { get; set; }
        public long? MediumId { get; set; }
        public long? ChannelId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsActive { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public List<string> repeatDays { get; set; }
        public List<RepeatDaysModel> RepeatDays { get; set; }
    }

    public class FilterSchedulerModel
    {
        public long? MediumId { get; set; }
        public long? ChannelId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
