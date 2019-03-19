using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Marketing
{
    public class Channel : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ChannelId { get; set; }
        public string ChannelName { get; set; }
        [ForeignKey("MediumId")]
        public long? MediumId { get; set; }
        public Medium Mediums { get; set; }
    }
}
