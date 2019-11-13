using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Project
{
    public class ProjectProposalDocument : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ProjectProposalDocumnetId { get; set; }
        public string ProposalDocumentName { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public string ProposalWebLink { get; set; }
        public string ProposalExtType { get; set; }
        public int ProposalDocumentTypeId { get; set; }
    }
}