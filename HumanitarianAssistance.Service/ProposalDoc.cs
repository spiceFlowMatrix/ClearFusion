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
        static string ApplicationName = "Humanitarianweb";
        public static ProjectProposalModel userCredential(string ProjectCode,string pathFile)
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
                    "sdd.shared@gmail.com",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }
            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            var resp = createfolder(driveService, ProjectCode);
            return resp;
        }
        public static ProjectProposalModel createfolder(DriveService driveService, string ProjectCode)
        {
            ProjectProposalModel res = new ProjectProposalModel();
            List<File> result = new List<File>();
            List<string> folderName = new List<string>();
            FilesResource.ListRequest Listrequest = driveService.Files.List();
            //request4.Corpora = "files/folders";
            Listrequest.Fields = "*";
            do
            {
                FileList files = Listrequest.Execute();
                result.AddRange(files.Files);
                result = result.Where(p => p.MimeType == "application/vnd.google-apps.document" && p.Trashed == false).ToList();
                folderName = result.Select(p => p.Name).Distinct().ToList();
                Listrequest.PageToken = files.NextPageToken;
            } while (!String.IsNullOrEmpty(Listrequest.PageToken));

            if (folderName.Contains(ProjectCode))
            {
                res.FIleResponseMsg = "File already exist with name " + ProjectCode;
                res.StatusCode = StaticResource.NameAlreadyExist;
                File fileDetail = result.FirstOrDefault(p => p.Name == ProjectCode);
                res.ProposalWebLink= fileDetail.WebViewLink;
                //Console.WriteLine("File already exist with name {0}.", nameFile);
            }
            else
            {
                var fileFolderMetadata = new File()
                {
                    Name = ProjectCode,
                    MimeType = "application/vnd.google-apps.folder"
                };
                var folderrequest = driveService.Files.Create(fileFolderMetadata);
                folderrequest.Fields = "*";
                var folderDetails = folderrequest.Execute();
                res.FolderId = folderDetails.Id;
                res.FolderName = folderDetails.Name;
                //Console.WriteLine("Folder ID: " + folderDetails.Id);


                var fileMetadata = new File()
                {
                    Name = ProjectCode,
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
                Permission userPermission = new Permission()
                {
                    Type = "anyone", //user
                    Role = "writer",
                    //EmailAddress = "anveshkjain@gmail.com"
                };

                var Permissionrequest = driveService.Permissions.Create(userPermission, file.Id);
                Permissionrequest.Fields = "*";
                batch.Queue(Permissionrequest, callback);
                var task = batch.ExecuteAsync();
                res.FIleResponseMsg = "File Created Succesfully";
                res.StatusCode = StaticResource.successStatusCode;
            }
            return res;
        }

        public static ProjectProposalModel GetProjectProposal(string ProjectCode, string pathFile)
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
                    "sdd.shared@gmail.com",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }
            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            var resp = GetProposalwebLink(driveService, ProjectCode);
            return resp;
        }

     public static ProjectProposalModel GetProposalwebLink(DriveService driveService, string ProjectCode)
        {
            ProjectProposalModel res = new ProjectProposalModel();
            List<File> result = new List<File>();
            List<string> folderName = new List<string>();
            FilesResource.ListRequest Listrequest = driveService.Files.List();
            //request4.Corpora = "files/folders";
            Listrequest.Fields = "*";
            do
            {
                FileList files = Listrequest.Execute();
                result.AddRange(files.Files);
                result = result.Where(p => p.MimeType == "application/vnd.google-apps.document" && p.Trashed == false).ToList();
                folderName = result.Select(p => p.Name).Distinct().ToList();
                Listrequest.PageToken = files.NextPageToken;
            } while (!String.IsNullOrEmpty(Listrequest.PageToken));

            if (folderName.Contains(ProjectCode))
            {
                res.FIleResponseMsg = "File already exist with name " + ProjectCode;
                res.StatusCode = StaticResource.NameAlreadyExist;
                File fileDetail = result.FirstOrDefault(p => p.Name == ProjectCode);
                res.ProposalWebLink = fileDetail.WebViewLink;
                //Console.WriteLine("File already exist with name {0}.", nameFile);
            }
            return res;
        }



    }
}