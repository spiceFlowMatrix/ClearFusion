using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Project;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
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


        public static async Task<ProjectProposalDetail> AuthExplicit(string filefullPath, string projectProposalfilename, string googleCredentialpathFile, string folderName, ViewModels.Models.Project.GoogleCredential googleCredential, long Projectid, string userid)
        {

            ProjectProposalModel res = new ProjectProposalModel();
            ProjectProposalDetail model = new ProjectProposalDetail();
            //there are different scopes, which you can find here https://cloud.google.com/storage/docs/authentication
            var scopes = new[] { @"https://www.googleapis.com/auth/cloud-platform" };
            var cts = new CancellationTokenSource();
            var service = new Google.Apis.Storage.v1.StorageService();

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

            var bucketsQuery = service.Buckets.List(googleCredential.ProjectId);
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
                //var dir = AppDomain.CurrentDomain.BaseDirectory;
                var path = Directory.GetCurrentDirectory()+"\\Documents\\Proposal\\Proposal.docx";
                //var obj = path.Substring(path.LastIndexOf('\\') - 6, 6);
                var newObject = new Google.Apis.Storage.v1.Data.Object()
                {
                    Bucket = googleCredential.BucketName,
                    Name = folderName + "/" + projectProposalfilename + ".docx",

                };

                // var dir = Directory.GetCurrentDirectory();
                fileStream = new FileStream(path, FileMode.Open);
                var uploadRequest = new Google.Apis.Storage.v1.ObjectsResource.InsertMediaUpload(service, newObject, googleCredential.BucketName, fileStream, "application/vnd.google-apps.document");
                uploadRequest.OauthToken = credential.Token.AccessToken;
                var fileResponse = uploadRequest.Upload();

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
        public static async Task<ProjectProposalDetail> uploadOtherProposaldoc(string folderName, IFormFile filedata, string fileName, ViewModels.Models.Project.GoogleCredential googleCredential, string EmailId, string logginUserEmailId, string ext, string googleCredentialPathFile, string ProposalType)
        {
            ProjectProposalModel res = new ProjectProposalModel();
            string exten = System.IO.Path.GetExtension(fileName).ToLower();

            ProjectProposalDetail model = new ProjectProposalDetail();
            //there are different scopes, which you can find here https://cloud.google.com/storage/docs/authentication
            var scopes = new[] { @"https://www.googleapis.com/auth/cloud-platform" };
            var cts = new CancellationTokenSource();
            var service = new Google.Apis.Storage.v1.StorageService();
            UserCredential credential;
            using (var stream = new FileStream(googleCredentialPathFile, FileMode.Open, FileAccess.Read))
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

            var bucketsQuery = service.Buckets.List(googleCredential.ProjectId);
            bucketsQuery.OauthToken = credential.Token.AccessToken;
            var buckets = bucketsQuery.Execute();

            FileStream fileStream = null;
            try
            {

                APIResponse response = new APIResponse();
                //var dir = AppDomain.CurrentDomain.BaseDirectory;
                //var path = pathFile;

                //var obj = path.Substring(path.LastIndexOf('\\') - 6, 6);

                var newObject = new Google.Apis.Storage.v1.Data.Object()
                {
                    Bucket = googleCredential.BucketName,
                    Name = folderName + "/" + fileName

                };
                var mimetype = GetMimeType(ext);
                // var dir = Directory.GetCurrentDirectory();

                //using (var stream = filedata.OpenReadStream())
                //{
                //    using (var reader = new StreamReader(stream))
                //    {
                //        var csvReader = new CsvReader(reader);
                //        csvReader.Configuration.WillThrowOnMissingField = false;
                //        csvReader.Configuration.RegisterClassMap<RedCrossRequestMap>();
                //        var requests = csvReader.GetRecords<Request>().ToList();

                //        var errors = _mediator.Send(new ImportRequestsCommand { Requests = requests });
                //    }
                //}



                //fileStream = new FileStream();
                var uploadRequest = new Google.Apis.Storage.v1.ObjectsResource.InsertMediaUpload(service, newObject, googleCredential.BucketName, filedata.OpenReadStream(), mimetype);
                uploadRequest.OauthToken = credential.Token.AccessToken;
                var fileResponse = uploadRequest.Upload();

                var bucketFolderWithFilePath = newObject.Bucket + "/" + newObject.Name;
                if (fileResponse.Status.ToString() == "Completed" && fileResponse.Exception == null)
                {
                    if (ProposalType == "Proposal")
                    {
                        model.FolderName = folderName;
                        model.ProposalFileName = fileName;
                        model.ProposalWebLink = bucketFolderWithFilePath;
                        //model.ProjectId = ;
                        model.IsDeleted = false;
                        //model.CreatedById = logginUserEmailId;
                        model.CreatedDate = DateTime.Now;

                    }

                    if (ProposalType == "EOI")
                    {
                        model.FolderName = folderName;
                        model.EDIFileName = fileName;
                        model.EDIFileWebLink = bucketFolderWithFilePath;
                        model.EDIFileExtType = ext;
                        model.IsDeleted = false;
                        model.ModifiedDate = DateTime.Now;


                    }
                    else if (ProposalType == "BUDGET")
                    {
                        model.FolderName = folderName;
                        model.IsDeleted = false;
                        model.BudgetFileName = fileName;
                        // res.BudgetFileId = file.Id;
                        model.BudgetFileWebLink = bucketFolderWithFilePath;
                        model.BudgetFileExtType = ext;
                        model.ModifiedDate = DateTime.Now;

                    }
                    else if (ProposalType == "CONCEPT")
                    {
                        model.FolderName = folderName;
                        model.IsDeleted = false;
                        model.ConceptFileName = fileName;
                        // res.ConceptFileId = file.Id;
                        model.ConceptFileWebLink = bucketFolderWithFilePath;
                        model.ConceptFileExtType = ext;
                        model.ModifiedDate = DateTime.Now;
                    }
                    else if (ProposalType == "PRESENTATION")
                    {
                        model.FolderName = folderName;
                        model.IsDeleted = false;
                        model.PresentationFileName = fileName;
                        //res.PresentationFileId = file.Id;
                        model.PresentationFileWebLink = bucketFolderWithFilePath;
                        model.PresentationExtType = ext;
                        model.ModifiedDate = DateTime.Now;
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
            else if(ext== ".pptx")
            {
                mimeType = "application/vnd.google-apps.presentation";
            }
            return mimeType;
        }
    }

}
