using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IProjectDetails
    {
		Task<APIResponse> GetAllProjectDetails();
		Task<APIResponse> AddProjectDetails(ProjectDetailModel model);
		Task<APIResponse> EditProjectDetails(ProjectDetailModel model);
	}
}
