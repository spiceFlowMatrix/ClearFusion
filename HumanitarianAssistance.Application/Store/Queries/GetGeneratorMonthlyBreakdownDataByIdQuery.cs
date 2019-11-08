using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetGeneratorMonthlyBreakdownDataByIdQuery: IRequest<MonthlyBreakdownDataModel>
    {
        public int SelectedYear { get; set; }
        public long GeneratorId { get; set; }
    }
}