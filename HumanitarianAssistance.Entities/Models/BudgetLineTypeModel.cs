using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public partial class BudgetLineTypeModel : BaseModel
    {
        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        public string BudgetLineTypeName { get; set; }
        public int BudgetLineTypeId { get; set; }
        
    }
}
