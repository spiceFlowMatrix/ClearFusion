using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IExcelExportService
    {
        byte[] ExportToExcel(List<ExpandoObject> model, string worksheetName, string excelHeaderString, bool calculateSum, List<int> calculateSumOnKeyIndex);
        byte[] ExportEmployeePayrollExcel(EmployeesPayrollExcelModel model, List<string> headers);
    }
}