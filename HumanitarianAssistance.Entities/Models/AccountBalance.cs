using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanitarianAssistance.Entities.Models;

namespace HumanitarianAssistance.Entities.Models
{
    public partial class AccountBalance 
    {
        public int AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public int? AccountCategory { get; set; }
        public int AccountHeadTypeId { get; set; }
        public AccountHeadTypeModel AccountHeadType { get; set; }
        public decimal Balance { get; set; }
  }



}
