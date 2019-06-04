using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
  public  class ActivityDocumentsDetail: BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ActtivityDocumentId { get; set; }
        public string ActivityDocumentsFilePath { get; set; }
        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public ActivityStatusDetail ActivityStatusDetail { get; set; }

        public long ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public ProjectActivityDetail ProjectActivityDetail { get; set; }

        public long? MonitoringId { get; set; }

    }
}
