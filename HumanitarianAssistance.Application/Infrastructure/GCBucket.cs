using AutoMapper;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Cloud.Storage.V1;
using System.Net.Http;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Infrastructure
{
    public class GCBucket
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public IMapper _mapper;
        public UserManager<AppUser> _userManager;

        public GCBucket(HumanitarianAssistanceDbContext dbContext, IMapper mapper, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;

        }


        public static async Task<ProjectProposalDetail> AuthExplicit(string filefullPath, string projectProposalfilename, JObject googleCredentialpathFile, string folderName, GoogleCredentialModel googleCredential, long Projectid, string userid)
        {
            ProjectProposalModel res = new ProjectProposalModel();
            ProjectProposalDetail model = new ProjectProposalDetail();

            //there are different scopes, which you can find here https://cloud.google.com/storage/docs/authentication

            //var scopes = new[] { @"https://www.googleapis.com/auth/cloud-platform" };
            var scopes = new[] { @"https://www.googleapis.com/auth/devstorage.full_control" };

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

                ApiResponse response = new ApiResponse();
                Console.WriteLine("reading file path");

                var path = Directory.GetCurrentDirectory() + "/Documents/Proposal/Proposal.docx";
                Console.WriteLine($"File local  Path with environment variables : {path}");
                path = path.Replace(@"\", "/");
                Console.WriteLine($"convert File local Path in linux : {path}");

                var newObject = new Google.Apis.Storage.v1.Data.Object()
                {
                    Bucket = googleCredential.BucketName,
                    Name = googleCredential.Projects + "/" + folderName + "/" + projectProposalfilename + ".docx",
                };

                fileStream = new FileStream(path, FileMode.Open);

                var uploadRequest = new ObjectsResource.InsertMediaUpload(service, newObject, googleCredential.BucketName, fileStream, "application/vnd.google-apps.document");
                uploadRequest.OauthToken = credential.Token.AccessToken;
                var fileResponse = uploadRequest.Upload();

                Console.WriteLine($"File upload status: {fileResponse.Status}");


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
                    Console.WriteLine("not completed");

                    throw new Exception(StaticResource.UnableToUploadFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured");

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

        //****************read credential using service account from directory 21/03/19
        //public static async Task<ProjectProposalDetail> StartProposalByDirectory(string projectProposalfilename, string googleCredentialpathFile, string folderName, ViewModels.Models.Project.GoogleCredentialModel googleCredential, long Projectid, string userid)
        //{
        //    FileStream fileStream = null;
        //    ProjectProposalDetail model = new ProjectProposalDetail();

        //    try
        //    {
        //        ProjectProposalModel res = new ProjectProposalModel();
        //        //var scopes = new[] { @"https://www.googleapis.com/auth/cloud-platform" };
        //        //var cts = new CancellationTokenSource();

        //        //StorageService service = new StorageService();
        //        //UserCredential credential;
        //        //using (var stream = new FileStream(googleCredentialpathFile, FileMode.Open, FileAccess.Read))
        //        //{
        //        //    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
        //        //          GoogleClientSecrets.Load(stream).Secrets,
        //        //          scopes,
        //        //         googleCredential.EmailId, CancellationToken.None);
        //        //}
        //        //// Create the service.
        //        //service = new StorageService(new BaseClientService.Initializer()
        //        //{
        //        //    HttpClientInitializer = credential,
        //        //    ApplicationName = googleCredential.ApplicationName,
        //        //});

        //        var clientSecrets = new ClientSecrets();
        //        clientSecrets.ClientId = "160690498129-pg9hh4gr2ucta6neiik97tv1sla1qkec.apps.googleusercontent.com";
        //        clientSecrets.ClientSecret = "W0Zn9o2KmJFRXDnCbmq9z5m6";
        //        //there are different scopes, which you can find here https://cloud.google.com/storage/docs/authentication
        //        var scopes = new[] { @"https://www.googleapis.com/auth/devstorage.full_control" };

        //        var cts = new CancellationTokenSource();
        //        var userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, scopes, "cf-storage-serviceacc@clear-fusion-193608.iam.gserviceaccount.com", cts.Token);

        //        await userCredential.RefreshTokenAsync(cts.Token);
        //        var service = new Google.Apis.Storage.v1.StorageService();
        //        Console.WriteLine($"---- credential service----: {service}");

        //        var bucketsQuery = service.Buckets.List(StaticResource.ProjectId);
        //        bucketsQuery.OauthToken = userCredential.Token.AccessToken;
        //        var buckets = bucketsQuery.Execute();


        //        var dir = Directory.GetCurrentDirectory();
        //       // var pathCombine = Path.Combine(dir, @"/Documents/Proposal/Proposal.docx");
        //        var pathCombine = Directory.GetCurrentDirectory() + @"/Documents/Proposal/Proposal.docx";

        //        pathCombine = pathCombine.Replace(@"\", "/");

        //        Console.WriteLine($"--------File local folder Path ----------: {pathCombine}");

        //        fileStream = new FileStream(pathCombine, FileMode.Open);


        //        ApiResponse response = new ApiResponse();


        //        Console.WriteLine($"--------convert to linux window File local folder Path ----------: {fileStream}");

        //        var newObject = new Google.Apis.Storage.v1.Data.Object()
        //        {
        //            Bucket = googleCredential.BucketName,
        //            Name = googleCredential.Projects + "/" + folderName + "/" + projectProposalfilename + "xyz" + ".docx",
        //        };

        //        if (fileStream != null)
        //        {
        //            var uploadRequest = new ObjectsResource.InsertMediaUpload(service, newObject, googleCredential.BucketName, fileStream, "application/vnd.google-apps.document");
        //            uploadRequest.OauthToken = userCredential.Token.AccessToken;
        //            var fileResponse = uploadRequest.Upload();

        //            Console.WriteLine($"File upload on bucket status: {fileResponse.Status}");


        //            var bucketFolderWithFilePath = newObject.Bucket + "/" + newObject.Name;
        //            if (fileResponse.Status.ToString() == "Completed" && fileResponse.Exception == null)
        //            {
        //                model.FolderName = folderName;
        //                model.ProposalFileName = projectProposalfilename;
        //                model.ProposalWebLink = bucketFolderWithFilePath;
        //                model.ProjectId = Projectid;
        //                model.IsDeleted = false;
        //                model.CreatedById = userid;
        //                model.CreatedDate = DateTime.Now;
        //                model.ProposalExtType = ".docx";
        //            }
        //        }

        //        else
        //        {
        //            throw new Exception(StaticResource.UnableToUploadFile);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        if (fileStream != null)
        //        {
        //            fileStream.Dispose();
        //        }
        //    }
        //    return model;

        //}


        //************************************read credential from directory using auth credentail and using env as well as directory 20/03/2019

        public static async Task<ProjectProposalDetail> StartProposalByDirectory(string projectProposalfilename, string googleCredentialpathFile, string folderName, GoogleCredentialModel googleCredential, long Projectid, string userid)
        {
            FileStream fileStream = null;
            ProjectProposalDetail model = new ProjectProposalDetail();

            try
            {
                ProjectProposalModel res = new ProjectProposalModel();
                var scopes = new[] { @"https://www.googleapis.com/auth/cloud-platform" };
                var cts = new CancellationTokenSource();

                StorageService service = new StorageService();
                UserCredential credential;
                using (var stream = new FileStream(googleCredentialpathFile, FileMode.Open, FileAccess.Read))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                          GoogleClientSecrets.Load(stream).Secrets,
                          scopes,
                         googleCredential.EmailId, CancellationToken.None);
                }
                // Create the service.
                service = new StorageService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = googleCredential.ApplicationName,
                });
                Console.WriteLine($"---- credential service----: {service}");

                var bucketsQuery = service.Buckets.List(StaticResource.ProjectId);
                bucketsQuery.OauthToken = credential.Token.AccessToken;
                var buckets = bucketsQuery.Execute();


                //enter bucket name to which you want to upload file 
                ApiResponse response = new ApiResponse();
                var path = Directory.GetCurrentDirectory() + @"/Documents/Proposal/Proposal.docx";

                Console.WriteLine($"--------File local folder Path ----------: {path}");

                path = path.Replace(@"\", "/");
                Console.WriteLine($"--------convert to linux window File local folder Path ----------: {path}");

                var newObject = new Google.Apis.Storage.v1.Data.Object()
                {
                    Bucket = googleCredential.BucketName,
                    Name = googleCredential.Projects + "/" + folderName + "/" + projectProposalfilename + "xyz" + ".docx",
                };

                fileStream = new FileStream(path, FileMode.Open);
                if (fileStream != null)
                {
                    var uploadRequest = new ObjectsResource.InsertMediaUpload(service, newObject, googleCredential.BucketName, fileStream, "application/vnd.google-apps.document");
                    uploadRequest.OauthToken = credential.Token.AccessToken;
                    var fileResponse = uploadRequest.Upload();

                    Console.WriteLine($"File upload on bucket status: {fileResponse.Status}");


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
        public static async Task<ProjectProposalDetail> uploadOtherProposaldoc(string folderName, IFormFile filedata, string fileName, GoogleCredentialModel googleCredential, string EmailId, string logginUserEmailId, string ext, JObject googleCredentialPathFile, string ProposalType)
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

                ApiResponse response = new ApiResponse();
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
        public static string GetMimeType(string extension)
        {
            string mimeType = "application/unknown";
            string ext = (extension).ToLower();
            if (ext == ".docx" || ext == ".doc" || ext == ".txt")
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


        #region Upload files common method to all drag and drop pk

        public static async Task<string> UploadDocument(string folderName, IFormFile filedata, string fileName, string ext, JObject googleCredentialPathFile1, GoogleCredentialModel googleCredential)
        {
            string exten = Path.GetExtension(fileName).ToLower();
            string bucketFolderWithFilePath = string.Empty;
            Console.WriteLine("---------------------GCBUCKET ----------------code");
            ProjectProposalDetail model = new ProjectProposalDetail();
            var scopes = new[] { @"https://www.googleapis.com/auth/cloud-platform" };
            var cts = new CancellationTokenSource();
            StorageService service = new StorageService();
            UserCredential credential;
            ClientSecrets secrets = new ClientSecrets();
            secrets.ClientId = googleCredentialPathFile1["installed"]["client_id"].ToString();
            secrets.ClientSecret = googleCredentialPathFile1["installed"]["client_secret"].ToString();
            Console.WriteLine($"secrets");
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

                ApiResponse response = new ApiResponse();
                var newObject = new Google.Apis.Storage.v1.Data.Object()
                {
                    Bucket = StaticResource.BucketName,
                    Name = StaticResource.ProjectsFolderName + "/" + folderName + "/" + StaticResource.ProjectActivityFolderName + "/" + fileName

                };

                var mimetype = GetMimeType(ext);

                var uploadRequest = new ObjectsResource.InsertMediaUpload(service, newObject, StaticResource.BucketName, filedata.OpenReadStream(), mimetype);
                var fileResponse = uploadRequest.Upload();
                Console.WriteLine($"File upload on bucket status: {fileResponse.Status}");

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




        //upload file for drag and drop demo
        public static int UploadFile(string bucketName, string localPath)
        {
            var storage = StorageClient.Create();

            using (var f = File.OpenRead(localPath))
            {
                //objectName = objectName ?? Path.GetFileName(localPath);
                storage.UploadObject(StaticResource.BucketName, "TESTFILE_PLEASECHECK.docx", null, f);
                //Console.WriteLine($"Uploaded {objectName}.");
                Console.WriteLine($"Uploaded.");
            }
            return 1;
        }

        /// <summary>
        /// start proposal 25/03/2019
        /// </summary>


        //DEMO FOR Start Propsal USING SERVICE ACCOUNT CREDENTIAL. 22/03/2019
        public static async Task<string> StartProposalCreateFile(string bucketName, string folderName, string fileName)
        {
            try
            {
                ApiResponse response = new ApiResponse();
                var storage = StorageClient.Create();
                var content = Encoding.UTF8.GetBytes("");
                string folderWithProposalFile = StaticResource.ProjectsFolderName + "/" + folderName + "/" + fileName;
                var bucketResponse = await storage.UploadObjectAsync(bucketName, folderWithProposalFile, "application/x-directory", new MemoryStream(content));
                Console.WriteLine($"upload status:******************check bucket{bucketResponse}");
                var uploadedFile = bucketResponse.Name;
                Console.WriteLine($"UPLOADED FILE NAME: {uploadedFile}");
                return uploadedFile;

            }
            catch (Exception ex)
            {
                Console.WriteLine("-------------Exception of proposal creation on bucket------------:");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        ///UPLOAD OTHER DOCUMENT OF PROPOSAL USING SERVICE ACCOUNT  new CREDENTAIL PK 26/03/2019
        ///
        public static async Task<string> UploadOtherProposalDocuments(string bucketName, string folderWithProposalFile, IFormFile filedata, string fileName, string ext)
        {
            string exten = Path.GetExtension(fileName).ToLower();
            var mimetype = GetMimeType(ext);
            Stream filestream = null;
            try
            {
                var storage = StorageClient.Create();
                using (filestream = filedata.OpenReadStream())
                {
                    var resp = await storage.UploadObjectAsync(bucketName, folderWithProposalFile, mimetype, filestream);
                    //Console.WriteLine($"------------Uploaded file Bucket Response:{resp.Name}");
                    //var uploadedFileName = bucketName + "/" + resp.Name;

                    return resp.Name;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("-------------Exception of upload other documents on bucket------------:");
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (filestream != null)
                {
                    filestream.Dispose();
                }
            }

        }

        /// <summary>
        /// DOWNlOAD BUCKET OBJECT BY USING SIGNED URL CREDENTIALS OF ServiceAccountCredential 28/03/2019
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public async static Task<string> GetSignedURL(string bucketName, string objectName)
        {
            try
            {
                var scopes = new string[] { "https://www.googleapis.com/auth/devstorage.read_write" };

                //string cred= Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");

                ServiceAccountCredential cred;

                using (var stream = new FileStream(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"), FileMode.Open, FileAccess.Read))
                {
                    cred = GoogleCredential.FromStream(stream)
                                           .CreateScoped(scopes)
                                           .UnderlyingCredential as ServiceAccountCredential;
                }

                var urlSigner = UrlSigner.FromServiceAccountCredential(cred);

                return await urlSigner.SignAsync(bucketName, objectName, TimeSpan.FromMinutes(10), HttpMethod.Get);
            }
            catch (Exception)
            {
                throw new Exception(StaticResource.UnableToGenerateSignedUrl);
            }

        }

        /// <summary>
        /// DELETE OBJECT FROM BUCKET 28/03/2019
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public async static Task<bool> DeleteObject(string bucketName, string objectName)
        {
            try
            {
                var storage = StorageClient.Create();

                await storage.DeleteObjectAsync(bucketName, objectName);
                Console.WriteLine($"Deleted {objectName}.");

                return true;
            }
            catch (Exception)
            {
                throw new Exception(StaticResource.UnableToDeleteBucketObject);
            }
        }

    }
}
