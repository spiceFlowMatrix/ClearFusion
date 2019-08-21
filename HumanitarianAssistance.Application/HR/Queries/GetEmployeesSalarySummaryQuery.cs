using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesSalarySummaryQuery: IRequest<ApiResponse>
    {
        public int EmployeeTypeId { get; set; }
		public int RecordType { get; set; }
		public int OfficeId { get; set; }
		public int Year { get; set; }
		public int? Month { get; set; }		
		public int? CurrencyId { get; set; }
		public int? AllowanceId { get; set; }
		public int? DeductionId { get; set; }
    }
}