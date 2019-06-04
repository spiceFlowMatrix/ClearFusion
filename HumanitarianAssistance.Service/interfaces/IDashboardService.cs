using HumanitarianAssistance.Service.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IDashboardService
    {
        APIResponse GetTrainingLink();
    }
}
