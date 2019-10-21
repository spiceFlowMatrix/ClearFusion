using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Project
{
    public class CountryMultiSelectDetails : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long CountryMultiSelectId { get; set; }
        [ForeignKey("ProjectId")]
        public long ProjectId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
        [ForeignKey("CountryId")]
        public int? CountryId { get; set; }
        public CountryDetails CountryDetails { get; set; }
        public long? CountrySelectionId { get; set; } 

    }
}
