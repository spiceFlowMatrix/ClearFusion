using System;
namespace HumanitarianAssistance.ViewModels.Models
{

    public partial class BalanceRequestModel
    {
      public int id { get; set; }
      public DateTime asOfDate { get; set; }
      public int currency { get; set; }
  }


}
