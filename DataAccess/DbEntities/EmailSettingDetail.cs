using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class EmailSettingDetail : BaseEntityWithoutId
    {
        public EmailSettingDetail()
        {
            //CurrentEmailType = new EmailType();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long EmailId { get; set; }

        [StringLength(100)]
        public string SenderEmail { get; set; }
        public EmailType EmailTypes { get; set; }
        public int? EmailTypeId { get; set; }

        [StringLength(100)]
        public string SenderPassword { get; set; }

        public int SmtpPort { get; set; }

        [StringLength(100)]
        public string SmtpServer { get; set; }

        public Boolean? EnableSSL { get; set; }
        //public EmailType CurrentEmailType { get; set; }
    }
}
