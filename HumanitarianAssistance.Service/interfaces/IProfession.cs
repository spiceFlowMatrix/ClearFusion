using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IProfession
    {
        Task<APIResponse> AddProfession(ProfessionModel model);
        Task<APIResponse> EditProfession(ProfessionModel model);
        Task<APIResponse> GetAllProfession();
    }
}
