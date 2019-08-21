using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class LogisticsControlViewModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
