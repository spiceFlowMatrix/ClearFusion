using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class CurrencyModel : BaseModel
    {
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        [Required]
        public string CurrencyName { get; set; }
        public float? CurrencyRate { get; set; }
		public bool? Status { get; set; }
	}
}
