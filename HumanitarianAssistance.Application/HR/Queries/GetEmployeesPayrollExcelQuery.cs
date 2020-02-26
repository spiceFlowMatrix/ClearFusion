using System.Collections.Generic;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesPayrollExcelQuery: IRequest<byte[]>
    {
       // public DateTime StartDate { get; set; }
       // public DateTime EndDate { get; set; }
       public int? Month { get; set; }
        public List<int?> OfficeId { get; set; }
        public List<long?> ProjectIds { get; set; }
        public List<int?> SelectedEmployees {get; set;}
        public string EmployeeName { get; set; }
        public string EmployeeCode{ get; set; }
        public int? Sex{ get; set; }
        public int? EmploymentStatus{ get; set; }
    }
}