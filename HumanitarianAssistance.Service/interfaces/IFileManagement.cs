using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IFileManagement
    {
        Task<APIResponse> GetSignedURL(DownloadObjectGCBucketModel model);
        Task<APIResponse> SaveUploadedFileInfo(FileManagementModel model);
    }
}
