using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IJournalDetail
    {
        Task<APIResponse> AddJournalDetail(JournalDetailModel model);
        Task<APIResponse> EditJournalDetail(JournalDetailModel model);
        Task<APIResponse> DeleteJournalDetail(JournalDetailModelDelete model);
        Task<APIResponse> GetAllJournalDetail();
        Task<APIResponse> GetJournalDetailByCode(int JournalCode);
        Task<APIResponse> GetJournalDetailByName(string JournalName);
    }
}
