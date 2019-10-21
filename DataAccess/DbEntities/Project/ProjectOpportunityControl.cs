using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Project
{
    public class ProjectOpportunityControl: BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }

        [ForeignKey("ProjectId")]
        public long ProjectId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }


        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public UserDetails UserDetails { get; set; }

        public int RoleId { get; set; }
    }
}
