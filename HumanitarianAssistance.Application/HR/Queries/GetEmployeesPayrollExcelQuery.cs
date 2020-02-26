using System.Collections.Generic;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesPayrollExcelQuery: IRequest<byte[]>
    {
        public int OfficeId { get; set; }
        public List<int> SelectedEmployees {get; set;}
    }
}