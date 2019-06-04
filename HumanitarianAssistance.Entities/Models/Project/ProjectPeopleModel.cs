using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    #region "Opportunity"
    public class OpportunityControlAddModel
    {
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class OpportunityControlEditModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateAdded { get; set; }
    }

    public class OpportunityControlViewModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateAdded { get; set; }
    }
    #endregion

    #region "Logistics"
    public class LogisticsControlAddModel
    {
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class LogisticsControlEditModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateAdded { get; set; }
    }

    public class LogisticsControlViewModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateAdded { get; set; }
    }
    #endregion


    #region "Activities"
    public class ActivitiesControlAddModel
    {
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class ActivitiesControlEditModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateAdded { get; set; }
    }

    public class ActivitiesControlViewModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateAdded { get; set; }
    }
    #endregion


    #region "Hiring"
    public class HiringControlAddModel
    {
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class HiringControlEditModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateAdded { get; set; }
    }

    public class HiringControlViewModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateAdded { get; set; }
    }
    #endregion

}
