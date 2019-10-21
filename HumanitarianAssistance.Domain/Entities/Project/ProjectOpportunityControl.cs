using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectOpportunityControl : BaseEntity
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
