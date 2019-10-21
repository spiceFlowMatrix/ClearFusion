using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddGeneratorUsageHoursCommand: BaseModel, IRequest<bool>
    {
        public long GeneratorId { get; set; }
        public double Hours { get; set; }
        public DateTime Month {get; set;}
    }
}