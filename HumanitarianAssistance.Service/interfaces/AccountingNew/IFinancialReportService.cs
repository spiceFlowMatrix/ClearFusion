using System.Threading.Tasks;
using HumanitarianAssistance.Service.APIResponses;

namespace HumanitarianAssistance.Service.interfaces.AccountingNew
{
    public interface IFinancialReportService
    {
        Task<APIResponse> GetNoteBalancesByHeadType(int headType, int toCurrencyId);
        Task<APIResponse> GetNoteBalanceById(int noteId);
    }
}
