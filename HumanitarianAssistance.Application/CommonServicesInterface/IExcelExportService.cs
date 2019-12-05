using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IExcelExportService
    {
        byte[] ExportToExcel(List<ExpandoObject> model, string worksheetName, string excelHeaderString);
    }
}