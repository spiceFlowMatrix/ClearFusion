using System.Collections.Generic;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class SaveExchangeRatesForAllOfficesCommand : BaseModel, IRequest<ApiResponse>
    {
        public SaveExchangeRatesForAllOfficesCommand()
        {
            GenerateExchangeRateModel = new List<GenerateExchangeRateModel>();
        }

        public List<GenerateExchangeRateModel> GenerateExchangeRateModel { get; set; }
        public bool SaveForAllOffices { get; set; }
        public int? OfficeId { get; set; }
    }
}