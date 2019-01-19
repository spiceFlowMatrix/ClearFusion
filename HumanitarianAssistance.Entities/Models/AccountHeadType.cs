using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Entities.Models
{
    public class AccountHeadType
    {
        public int AccountHeadTypeId { get; set; }
        public string AccountHeadTypeName { get; set; }

    }
}
