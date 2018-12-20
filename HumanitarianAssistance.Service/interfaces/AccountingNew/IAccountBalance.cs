using System.Threading.Tasks;
using HumanitarianAssistance.Service.APIResponses;

namespace HumanitarianAssistance.Service.interfaces.AccountingNew
{
    public interface IAccountBalance
    {
        Task<APIResponse> GetNoteBalancesByHeadType(int headTypeId, int toCurrencyId);
        Task<APIResponse> GetNoteBalanceById(int noteId);
    }
}
