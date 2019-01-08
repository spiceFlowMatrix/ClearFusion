using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using File = Google.Apis.Drive.v3.Data.File;

namespace HumanitarianAssistance.Service
{
    public class ProposalDoc
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = string.Empty;
        public static DriveService userGoogleCredential(string ProjectCode, string pathFile, ViewModels.Models.Project.GoogleCredential Credential)
        {
            UserCredential credential;
            using (var stream =
                new FileStream(pathFile, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                   //"sdd.shared@gmail.com",
                   // "hamza@edgsolutions.net",
                   Credential.EmailId,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                
            }
            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Credential.ApplicationName,
            });
            return driveService;
        }


        public static ProjectProposalModel userCredential(string ProjectProposalfilename, string pathFile, ViewModels.Models.Project.GoogleCredential Credential, string EmailId, string FolderName, string logginUserEmailId)
        {
            var driveService = userGoogleCredential(ProjectProposalfilename, pathFile, Credential);
            var resp = createfolder(driveService, ProjectProposalfilename, EmailId, FolderName, Credential.EmailId, logginUserEmailId);
            return resp;
        }

        public static ProjectProposalModel createfolder(DriveService driveService, string ProjectProposalfilename, string EmailId, string FolderName, string Credential, string logginUserEmailId)
        {
            ProjectProposalModel res = new ProjectProposalModel();
            List<File> result = new List<File>();
            List<string> folderName = new List<string>();
            FilesResource.ListRequest Listrequest = driveService.Files.List();
            //request4.Corpora = "files/folders";
            string mailid = string.Empty;
            if (EmailId == null)
            {
                mailid = Credential + "," + logginUserEmailId;
            } 
            else
            {
                mailid = EmailId + "," + Credential + "," + logginUserEmailId;
            }
            Listrequest.Fields = "*";
            do
            {
                FileList files = Listrequest.Execute();
                result.AddRange(files.Files);
                result = result.Where(p => p.MimeType == "application/vnd.google-apps.document" && p.Trashed == false).ToList();
                folderName = result.Select(p => p.Name).Distinct().ToList();
                Listrequest.PageToken = files.NextPageToken;
            } while (!String.IsNullOrEmpty(Listrequest.PageToken));

            if (folderName.Contains(FolderName))
            {
                res.FIleResponseMsg = "File already exist with name " + FolderName;
                res.StatusCode = StaticResource.NameAlreadyExist;
                File fileDetail = result.FirstOrDefault(p => p.Name == FolderName);
                res.ProposalWebLink = fileDetail.WebViewLink;
            }
            else
            {
                var fileFolderMetadata = new File()
                {
                    Name = FolderName,
                    MimeType = "application/vnd.google-apps.folder"
                };
                var folderrequest = driveService.Files.Create(fileFolderMetadata);
                folderrequest.Fields = "*";
                var folderDetails = folderrequest.Execute();
                res.FolderId = folderDetails.Id;
                res.FolderName = folderDetails.Name;
                var fileMetadata = new File()
                {
                    Name = ProjectProposalfilename,
                    MimeType = "application/vnd.google-apps.document",
                    Parents = new List<string>
                {
                    folderDetails.Id
                }
                };

                var Documentrequest = driveService.Files.Create(fileMetadata);
                Documentrequest.Fields = "*";
                var file = Documentrequest.Execute();
                res.ProposalFileName = file.Name;
                res.ProposalFileId = file.Id;
                res.ProposalWebLink = file.WebViewLink;
                var batch = new BatchRequest(driveService);
                BatchRequest.OnResponse<Permission> callback = delegate (
                    Permission permission,
                    RequestError error,
                    int index,
                    System.Net.Http.HttpResponseMessage message)
                {
                    if (error != null)
                    {
                        res.FIleResponseMsg = error.Message;
                    }
                };

                foreach (var item in mailid.Split(','))
                {
                    Permission userPermission = new Permission()
                    {

                        Type = "user",
                        Role = "writer",
                        EmailAddress = item
                    };
                    var request1 = driveService.Permissions.Create(userPermission, file.Id);
                    request1.Fields = "*";
                    batch.Queue(request1, callback);
                    var task = batch.ExecuteAsync();
                }

                res.FIleResponseMsg = "File Created Succesfully";
                res.StatusCode = StaticResource.successStatusCode;
            }
            return res;
        }


        public static ProjectProposalModel uploadOtherProposaldoc(string ProjectCode, IFormFile filedata, string fileName, string pathFile, string uploadfilelocalpath, ViewModels.Models.Project.GoogleCredential Credential, string EmailId, string logginUserEmailId)
        {
            ProjectProposalModel res = new ProjectProposalModel();
            string exten = System.IO.Path.GetExtension(fileName).ToLower();
            string ext = exten.Trim('"').Split('.')[1];
            var driveService = userGoogleCredential(ProjectCode, pathFile, Credential);
            List<File> result = new List<File>();
            List<string> folderName = new List<string>();
            FilesResource.ListRequest request4 = driveService.Files.List();
            string mailid = string.Empty;
            if (EmailId == null)
            {
                mailid = Credential.EmailId + "," + logginUserEmailId;
            }
            else
            {
                mailid = EmailId + "," + Credential.EmailId + "," + logginUserEmailId;
            }
            request4.Fields = "*";
            do
            {
                FileList files = request4.Execute();
                result.AddRange(files.Files);
                result = result.Where(p => p.MimeType == "application/vnd.google-apps.folder" && p.Trashed == false).ToList();
                folderName = result.Select(p => p.Name).Distinct().ToList();
                request4.PageToken = files.NextPageToken;
            } while (!String.IsNullOrEmpty(request4.PageToken));          
            string folder = ProjectCode;
            if (folderName.Contains(folder))
            {

                File fileDetail = result.Where(p => p.MimeType == "application/vnd.google-apps.folder" && p.Trashed == false && p.Name == folder.Trim()).FirstOrDefault();
                var fileMetadata = new File()
                {
                    Name = fileName,
                    MimeType = GetMimeType(fileName),
                    Parents = new List<string>
                    {
                        fileDetail.Id
                    }
                };
                FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream(uploadfilelocalpath,
                    System.IO.FileMode.Open))
                {
                    request = driveService.Files.Create(
                        fileMetadata, stream, GetMimeType(fileName));
                    request.Fields = "*";
                    request.Upload();
                }
                var file = request.ResponseBody;
                var batch = new BatchRequest(driveService);
                BatchRequest.OnResponse<Permission> callback = delegate (
                    Permission permission,
                    RequestError error,
                    int index,
                    System.Net.Http.HttpResponseMessage message)
                {
                    if (error != null)
                    {
                        res.FIleResponseMsg = error.Message;
                    }
                };

                foreach (var item in mailid.Split(','))
                {
                    Permission userPermission = new Permission()
                    {

                        Type = "user",
                        Role = "writer",
                        EmailAddress = item
                    };                  
                    var request1 = driveService.Permissions.Create(userPermission, file.Id);
                    request1.Fields = "*";
                    batch.Queue(request1, callback);
                    var task = batch.ExecuteAsync();
                }

                string fileType = file.Name.Trim('"').Split('_')[0];
                res.FileType = fileType;
                if (fileType == "EOI")
                {
                    res.EDIFileName = file.Name;
                    res.EdiFileId = file.Id;
                    res.EDIFileWebLink = file.WebViewLink;
                    res.EDIFileExtType = ext;

                }
                else if (fileType == "BUDGET")
                {
                    res.BudgetFileName = file.Name;
                    res.BudgetFileId = file.Id;
                    res.BudgetFileWebLink = file.WebViewLink;
                    res.BudgetFileExtType = ext;
                }
                else if (fileType == "CONCEPT")
                {
                    res.ConceptFileName = file.Name;
                    res.ConceptFileId = file.Id;
                    res.ConceptFileWebLink = file.WebViewLink;
                    res.ConceptFileExtType = ext;
                }
                else if (fileType == "PRESENTATION")
                {
                    res.PresentationFileName = file.Name;
                    res.PresentationFileId = file.Id;
                    res.PresentationFileWebLink = file.WebViewLink;
                    res.PresentationExtType = ext;
                }
            }
            else
            {
                var fileMetadata1 = new File()
                {
                    Name = folder,
                    MimeType = "application/vnd.google-apps.folder"
                };
                var request3 = driveService.Files.Create(fileMetadata1);
                request3.Fields = "id";
                var folder1 = request3.Execute();
                Console.WriteLine("Folder ID: " + folder1.Id);

                var fileMetadata = new File()
                {
                    Name = fileName,
                    MimeType = GetMimeType(fileName),
                    Parents = new List<string>
                    {
                        folder1.Id
                    }
                };

                FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream(uploadfilelocalpath,
                    System.IO.FileMode.Open))
                {
                    request = driveService.Files.Create(
                        fileMetadata, stream, GetMimeType(fileName));
                    request.Fields = "*";
                    request.Upload();
                }

                var batch = new BatchRequest(driveService);
                BatchRequest.OnResponse<Permission> callback = delegate (
                    Permission permission,
                    RequestError error,
                    int index,
                    System.Net.Http.HttpResponseMessage message)
                {

                    if (error != null)
                    {
                        res.FIleResponseMsg = error.Message;
                    }
                };
                var file = request.ResponseBody;
                foreach (var item in mailid.Split(','))
                {
                    Permission userPermission = new Permission()
                    {

                        Type = "user",
                        Role = "writer",
                        EmailAddress = item
                    };
                    //var file = request.ResponseBody;
                    var request1 = driveService.Permissions.Create(userPermission, file.Id);
                    request1.Fields = "*";
                    batch.Queue(request1, callback);
                    var task = batch.ExecuteAsync();
                }
                string fileType = file.Name.Trim('"').Split('_')[0];
                res.FileType = fileType;
                if (fileType == "EOI")
                {
                    res.EDIFileName = file.Name;
                    res.EdiFileId = file.Id;
                    res.EDIFileWebLink = file.WebViewLink;
                    res.EDIFileExtType = ext;
                }
                else if (fileType == "BUDGET")
                {
                    res.BudgetFileName = file.Name;
                    res.BudgetFileId = file.Id;
                    res.BudgetFileWebLink = file.WebViewLink;
                    res.BudgetFileExtType = ext;
                }
                else if (fileType == "CONCEPT")
                {
                    res.ConceptFileName = file.Name;
                    res.ConceptFileId = file.Id;
                    res.ConceptFileWebLink = file.WebViewLink;
                    res.ConceptFileExtType = ext;
                }
                else if (fileType == "PRESENTATION")
                {
                    res.PresentationFileName = file.Name;
                    res.PresentationFileId = file.Id;
                    res.PresentationFileWebLink = file.WebViewLink;
                    res.PresentationExtType = ext;
                }
            }
            res.FIleResponseMsg = StaticResource.SuccessText;
            res.StatusCode = StaticResource.successStatusCode;
            return res;
        }
        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            if (ext == ".docx" || ext == ".doc")
            {
                mimeType = "application/vnd.google-apps.document";
            }
            else if (ext == ".pdf")
            {
                //mimeType = "application/vnd.google-apps.file";
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                if (regKey != null && regKey.GetValue("Content Type") != null)
                    mimeType = regKey.GetValue("Content Type").ToString();
            }
            else if (ext == ".xlsx" || ext == ".xls" || ext == ".csv")
            {
                mimeType = "application/vnd.google-apps.spreadsheet";
            }
            //else if (ext == ".jpeg" || ext == ".png")
            //{
            //    mimeType = "application/vnd.google-apps.photo";
            //}
            return mimeType;
        }
        public static string FilePermission(string ProjectCode, string Fileid, string EmailId, string pathFile, ViewModels.Models.Project.GoogleCredential Credential, string logginUserEmailId)
        {
            var driveService = userGoogleCredential(ProjectCode, pathFile, Credential);
            string Message = string.Empty;
            string mailid = string.Empty;
            if (EmailId == null)
            {
                mailid = Credential.EmailId + "," + logginUserEmailId;
            }
            else
            {
                mailid = EmailId + "," + Credential.EmailId + "," + logginUserEmailId;
            }
            var batch = new BatchRequest(driveService);
            BatchRequest.OnResponse<Permission> callback = delegate (
                Permission permission,
                RequestError error,
                int index,
                System.Net.Http.HttpResponseMessage message)
            {

                if (error != null)
                {
                    Message = error.Message;
                }
            };

            foreach (var item in mailid.Split(','))
            {
                Permission userPermission = new Permission()
                {

                    Type = "user",
                    Role = "writer",
                    EmailAddress = item
                };
                var request1 = driveService.Permissions.Create(userPermission, Fileid);
                request1.Fields = "*";
                batch.Queue(request1, callback);
                var task = batch.ExecuteAsync();
            }

            return Message = "Success";
        }
    }
}