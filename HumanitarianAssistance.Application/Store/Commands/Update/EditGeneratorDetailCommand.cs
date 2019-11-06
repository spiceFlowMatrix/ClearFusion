using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditGeneratorDetailCommand: BaseModel, IRequest<bool>
    {
        public long GeneratorId { get; set; }
        public double Voltage { get; set; }
        public double StartingUsage { get; set; }
        public double IncurredUsage { get; set; }
        public double FuelConsumptionRate { get; set; }
        public double MobilOilConsumptionRate { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
    }
}