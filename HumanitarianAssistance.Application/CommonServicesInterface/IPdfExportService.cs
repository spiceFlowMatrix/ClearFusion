using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IPdfExportService
    {
       Task<byte[]> ExportToPdf(object model, string viewName, bool isLandscape=false);
    }
}