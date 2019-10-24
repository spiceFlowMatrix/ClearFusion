using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Marketing
{
    public class PolicyDetail: BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string Description { get; set; }
        public string PolicyCode { get; set; }
        [ForeignKey("LanguageId")]
        public long? LanguageId { get; set; }
        public LanguageDetail Languages { get; set; }
        [ForeignKey("MediumId")]
        public long? MediumId { get; set; }
        public Medium Mediums { get; set; }
        [ForeignKey("MediaCategoryId")]
        public long? MediaCategoryId { get; set; }
        public MediaCategory MediaCategories { get; set; }
        [ForeignKey("ProducerId")]
        public long? ProducerId { get; set; }
        public Producer Producers { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string RepeatDays { get; set; }
    }
}
