
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccess.DbEntities
{
   public class Department :BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string OfficeCode { get; set; }
        public int? OfficeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }



    }
}
