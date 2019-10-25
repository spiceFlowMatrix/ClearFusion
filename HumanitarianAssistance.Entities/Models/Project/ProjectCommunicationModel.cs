using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class ProjectCommunicationModel
    {
        public long? PCId { get; set; }
        public long ProjectId { get; set; }
        public string ProjectDescription { get; set; }
        public long? PCAId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedById { get; set; }
        public bool? IsDeleted { get; set; }
        public string UserRole { get; set; }
        public string RoleId { get; set; }
    }
}
