using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
  public  class BudgetPayableModel :BaseModel
    {
        public long BudgetPayableId { get; set; }
        //public long BudgetId { get; set; }
        public long ProjectId { get; set; }
               
        public long BudgetLineId { get; set; }
        [Required]
        public double Amount { get; set; }
        public DateTime ExpectedDate { get; set; }

        public string ProjectName { get; set; }
        //public int  BudgetLineTypeId { get; set; }
        public string  BudgetLineTypeName { get; set; }

       // public string Description { get; set; }

                
        //public long BudgetId { get; set; }

        
        
        
        
        
        
        

    }
}
