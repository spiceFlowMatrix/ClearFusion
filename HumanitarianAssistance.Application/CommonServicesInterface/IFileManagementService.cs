using System.Threading.Tasks;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IFileManagementService
    {
         Task<StoreDocumentModel> GetFilesByRecordIdAndDocumentType(FileModel model);
         Task<ApiResponse> DownloadFileFromBucket(DownloadObjectGCBucketModel model);
    }
}