using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Project;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service
{
    public class GCBucket
    {
        public IUnitOfWork _uow;
        public IMapper _mapper;
        public UserManager<AppUser> _userManager;

        public GCBucket(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;

        }


        public static async Task<ProjectProposalDetail> AuthExplicit(string filefullPath, string projectProposalfilename, JObject googleCredentialpathFile, string folderName, ViewModels.Models.Project.GoogleCredential googleCredential, long Projectid, string userid)
        {
            ProjectProposalModel res = new ProjectProposalModel();
            ProjectProposalDetail model = new ProjectProposalDetail();

            //there are different scopes, which you can find here https://cloud.google.com/storage/docs/authentication

            var scopes = new[] { @"https://www.googleapis.com/auth/cloud-platform" };
            var cts = new CancellationTokenSource();

            StorageService service = new StorageService();
            ClientSecrets secrets = new ClientSecrets();

            secrets.ClientId = googleCredentialpathFile["installed"]["client_id"].ToString();
            secrets.ClientSecret = googleCredentialpathFile["installed"]["client_secret"].ToString();

            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                     secrets,
                      scopes,
                     StaticResource.EmailId, CancellationToken.None);

            service = new StorageService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = StaticResource.ApplicationName,
            });

            var bucketsQuery = service.Buckets.List(StaticResource.ProjectId);
            bucketsQuery.OauthToken = credential.Token.AccessToken;
            var buckets = bucketsQuery.Execute();

            // create bucket first time

            //var newBucket = new Google.Apis.Storage.v1.Data.Bucket()
            //{
            //    Name = "clear-fusion"
            //};

            //var newBucketQuery = service.Buckets.Insert(newBucket, projectId);
            // newBucketQuery.OauthToken = userCredential.Result.Token.AccessToken;
            //you probably want to wrap this into try..catch block
            // newBucketQuery.Execute();


            //enter bucket name to which you want to upload file 
            FileStream fileStream = null;
            try
            {

                APIResponse response = new APIResponse();
                var path = Directory.GetCurrentDirectory() + "/Documents/Proposal/Proposal.docx";

                Console.WriteLine($"File Path : {path}");

                path = path.Replace('\\', '/');
                var newObject = new Google.Apis.Storage.v1.Data.Object()
                {
                    Bucket = googleCredential.BucketName,
                    Name = googleCredential.Projects + "/" + folderName + "/" + projectProposalfilename + ".docx",
                };

                fileStream = new FileStream(path, FileMode.Open);

                var uploadRequest = new ObjectsResource.InsertMediaUpload(service, newObject, googleCredential.BucketName, fileStream, "application/vnd.google-apps.document");
                uploadRequest.OauthToken = credential.Token.AccessToken;
                var fileResponse = uploadRequest.Upload();

                Console.WriteLine($"File upload status: {fileResponse}");


                var bucketFolderWithFilePath = newObject.Bucket + "/" + newObject.Name;
                if (fileResponse.Status.ToString() == "Completed" && fileResponse.Exception == null)
                {
                    model.FolderName = folderName;
                    model.ProposalFileName = projectProposalfilename;
                    model.ProposalWebLink = bucketFolderWithFilePath;
                    model.ProjectId = Projectid;
                    model.IsDeleted = false;
                    model.CreatedById = userid;
                    model.CreatedDate = DateTime.Now;
                    model.ProposalExtType = ".docx";
                }
                else
                {
                    throw new Exception(StaticResource.UnableToUploadFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }

            return model;
        }


        //upload files 
        public static async Task<ProjectProposalDetail> uploadOtherProposaldoc(string folderName, IFormFile filedata, string fileName, ViewModels.Models.Project.GoogleCredential googleCredential, string EmailId, string logginUserEmailId, string ext, JObject googleCredentialPathFile, string ProposalType)
        {
            ProjectProposalModel res = new ProjectProposalModel();
            string exten = Path.GetExtension(fileName).ToLower();
            ProjectProposalDetail model = new ProjectProposalDetail();

            //there are different scopes, which you can find here https://cloud.google.com/storage/docs/authentication
            var scopes = new[] { @"https://www.googleapis.com/auth/cloud-platform" };
            var cts = new CancellationTokenSource();

            ClientSecrets secrets = new ClientSecrets();

            secrets.ClientId = googleCredentialPathFile["installed"]["client_id"].ToString();
            secrets.ClientSecret = googleCredentialPathFile["installed"]["client_secret"].ToString();

            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                  secrets,
                  scopes,
                 StaticResource.EmailId, CancellationToken.None);

            // Create the service.
            StorageService service = new StorageService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = StaticResource.ApplicationName,
            });

            var bucketsQuery = service.Buckets.List(StaticResource.ProjectId);
            bucketsQuery.OauthToken = credential.Token.AccessToken;
            var buckets = bucketsQuery.Execute();

            FileStream fileStream = null;
            try
            {

                APIResponse response = new APIResponse();
                var newObject = new Google.Apis.Storage.v1.Data.Object()
                {
                    Bucket = StaticResource.BucketName,
                    Name = StaticResource.ProjectsFolderName + "/" + folderName + "/" + fileName

                };
                var mimetype = GetMimeType(ext);

                var uploadRequest = new ObjectsResource.InsertMediaUpload(service, newObject, StaticResource.BucketName, filedata.OpenReadStream(), mimetype);
                uploadRequest.OauthToken = credential.Token.AccessToken;
                var fileResponse = uploadRequest.Upload();

                Console.WriteLine($"Other files response from bucket: {fileResponse}");

                var bucketFolderWithFilePath = newObject.Bucket + "/" + newObject.Name;
                if (fileResponse.Status.ToString() == "Completed" && fileResponse.Exception == null)
                {
                    if (ProposalType == "Proposal")
                    {
                        model.FolderName = folderName;
                        model.ProposalFileName = fileName;
                        model.ProposalWebLink = bucketFolderWithFilePath;
                        model.IsDeleted = false;
                        model.CreatedDate = DateTime.UtcNow;
                    }

                    if (ProposalType == "EOI")
                    {
                        model.FolderName = folderName;
                        model.EDIFileName = fileName;
                        model.EDIFileWebLink = bucketFolderWithFilePath;
                        model.EDIFileExtType = ext;
                        model.IsDeleted = false;
                        model.ModifiedDate = DateTime.UtcNow;
                    }
                    else if (ProposalType == "BUDGET")
                    {
                        model.FolderName = folderName;
                        model.IsDeleted = false;
                        model.BudgetFileName = fileName;
                        model.BudgetFileWebLink = bucketFolderWithFilePath;
                        model.BudgetFileExtType = ext;
                        model.ModifiedDate = DateTime.UtcNow;
                    }
                    else if (ProposalType == "CONCEPT")
                    {
                        model.FolderName = folderName;
                        model.IsDeleted = false;
                        model.ConceptFileName = fileName;
                        model.ConceptFileWebLink = bucketFolderWithFilePath;
                        model.ConceptFileExtType = ext;
                        model.ModifiedDate = DateTime.UtcNow;
                    }
                    else if (ProposalType == "PRESENTATION")
                    {
                        model.FolderName = folderName;
                        model.IsDeleted = false;
                        model.PresentationFileName = fileName;
                        model.PresentationFileWebLink = bucketFolderWithFilePath;
                        model.PresentationExtType = ext;
                        model.ModifiedDate = DateTime.UtcNow;
                    }


                }
                else
                {
                    throw new Exception(StaticResource.UnableToUploadFile);
                }


                ////code to delete the file
                //ObjectsResource.ListRequest request = service.Objects.List(googleCredential.BucketName);
                //request.Delimiter = folderName + "/";

                ////request.Prefix = Delimiter;
                //var filePath = folderName + "/" + projectProposalfilename;
                //Google.Apis.Storage.v1.Data.Objects response1 = request.Execute();

                //foreach (var data in response1.Items)
                //{

                //    if (data != null && data.Name == filePath)
                //    {
                //        //delete the file in containing folder******

                //        ObjectsResource.DeleteRequest deleteRequest = null;

                //        deleteRequest = new ObjectsResource.DeleteRequest(service, googleCredential.BucketName, data.Name);
                //        deleteRequest.OauthToken = credential.Token.AccessToken;
                //        deleteRequest.Execute();
                //    }
                //    else
                //    {

                //    }
                //}
                //if (response1.Prefixes != null)
                //{
                //    // return
                //    var data = response;
                //}


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }


            return model;

        }
        private static string GetMimeType(string extension)
        {
            string mimeType = "application/unknown";
            string ext = (extension).ToLower();
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
            else if (ext == ".jpeg" || ext == ".png")
            {
                mimeType = "application/vnd.google-apps.photo";
            }
            else if (ext == ".pptx")
            {
                mimeType = "application/vnd.google-apps.presentation";
            }
            return mimeType;
        }


        #region Upload files

        public static async Task<string> UploadDocument(string folderName, IFormFile filedata, string fileName, string ext, JObject googleCredentialPathFile1, ViewModels.Models.Project.GoogleCredential googleCredential)
        {
            string exten = Path.GetExtension(fileName).ToLower();
            string bucketFolderWithFilePath = string.Empty;

            ProjectProposalDetail model = new ProjectProposalDetail();
            var scopes = new[] { @"https://www.googleapis.com/auth/cloud-platform" };
            var cts = new CancellationTokenSource();
            StorageService service = new StorageService();
            UserCredential credential;
            ClientSecrets secrets = new ClientSecrets();
            secrets.ClientId = googleCredentialPathFile1["installed"]["client_id"].ToString();
            secrets.ClientSecret = googleCredentialPathFile1["installed"]["client_secret"].ToString();

            //var service = new StorageService();
            //UserCredential credential;
            //using (var stream = new FileStream(googleCredentialPathFile1, FileMode.Open, FileAccess.Read))
            //{
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                secrets,
                  scopes,
                 StaticResource.EmailId, CancellationToken.None);
            //}
            // Create the service.
            service = new StorageService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = StaticResource.ApplicationName,
            });

            var bucketsQuery = service.Buckets.List(StaticResource.ProjectId);
            bucketsQuery.OauthToken = credential.Token.AccessToken;
            var buckets = bucketsQuery.Execute();
            FileStream fileStream = null;
            try
            {

                APIResponse response = new APIResponse();
                var newObject = new Google.Apis.Storage.v1.Data.Object()
                {
                    Bucket = StaticResource.BucketName,
                    Name = StaticResource.ProjectsFolderName + "/" + folderName + "/" + StaticResource.ProjectActivityFolderName + "/" + fileName

                };

                var mimetype = GetMimeType(ext);

                var uploadRequest = new ObjectsResource.InsertMediaUpload(service, newObject, StaticResource.BucketName, filedata.OpenReadStream(), mimetype);
                var fileResponse = uploadRequest.Upload();
                if (fileResponse.Status.ToString() == "Completed" && fileResponse.Exception == null)
                {
                    bucketFolderWithFilePath = newObject.Bucket + "/" + newObject.Name;

                }



            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }

            if (!string.IsNullOrEmpty(bucketFolderWithFilePath))
            {
                return bucketFolderWithFilePath;
            }
            else
            {
                throw new Exception(StaticResource.UnableToUploadFile);
            }

        }



        #endregion
    }



}
