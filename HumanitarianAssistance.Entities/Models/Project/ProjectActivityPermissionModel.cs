﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProjectActivityPermissionModel
    {
        public long Id{ get; set; }
        public long ProjectId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
