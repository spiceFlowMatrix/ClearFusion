using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetGeneratorByIdQuery: IRequest<GeneratorModel>
    {
        public long GeneratorId { get; set; }   
    }
}