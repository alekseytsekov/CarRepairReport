namespace CloudStorageApi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Dropbox.Api.Files;
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Drive.v3;
    using Google.Apis.Drive.v3.Data;
    using Google.Apis.Services;
    using Google.Apis.Util.Store;
    using File = Google.Apis.Drive.v3.Data.File;

    public class GoogleDrive : ICloudStorage
    {
        private DriveService _service;

        static string[] Scopes = { DriveService.Scope.Drive,
                           DriveService.Scope.DriveAppdata,
                           DriveService.Scope.DriveFile,
                           DriveService.Scope.DrivePhotosReadonly,
                           DriveService.Scope.DriveMetadataReadonly,
                           DriveService.Scope.DriveReadonly,
                            DriveService.Scope.DriveScripts
        };

        static string ApplicationName = "CarRepairReport";

        public Task StartService(string path)
        {
            // Create user credentials.
            UserCredential credential;
            
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Drive API service.
            this._service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            return null;
        }

        public string UploadFile(HttpPostedFileBase httpPostedFileBase)
        {
            //var fileMetadata = new File()
            //{
            //    Name = "httpPostedFileBase.FileName",
            //    MimeType = "application/vnd.google-apps.drive-sdk"
            //};

            //var request = this._service.Files.Create(fileMetadata);
            //request.Fields = "id";
            //var file = request.Execute();

            //Console.WriteLine("File ID: " + file.Id);

            ////https://drive.google.com/drive/folders/0B7J4kCqHkV0rdXJvdkR6T3ZNZkE?usp=sharing
            /// // https://drive.google.com/file/d/0B7J4kCqHkV0rZFYzaWx3bnZVWUU/view?usp=sharing

            var folderId = "0B7J4kCqHkV0rdXJvdkR6T3ZNZkE";

            File body = new File
            {
                Name = Path.GetFileName(httpPostedFileBase.FileName),
                MimeType = httpPostedFileBase.ContentType,
                Parents = new List<string>() { folderId }

                //OwnedByMe = true
                //MimeType = "application/vnd.google-apps.file",
                //MimeType = "image/jpeg",
            };


            try
            {
                
                FilesResource.CreateMediaUpload request = this._service.Files.Create(body, httpPostedFileBase.InputStream, body.MimeType);
                
                var result = request.Upload();

                var file = request.ResponseBody;

                return file.Id;
                //FilesResource.ListRequest listRequest = this._service.Files.List();

                //listRequest.Fields = "files(id, webViewLink)";

                //var files = listRequest.Execute().Files;

                //if (files != null)
                //{
                //    foreach (var img in files)
                //    {
                //        if (img.Id == file.Id)
                //        {
                //            var webLink = img.WebViewLink;
                //        }
                //    }
                //}
            }
            catch (Exception e)
            {
                //Console.WriteLine("An error occurred: " + e.Message);
            }
            
            return null;
        }

        public Task ListFiles()
        {
            FilesResource.ListRequest listRequest = this._service.Files.List();

            

            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            IList<File> files = listRequest.Execute().Files;


            int a = 0;

            //Console.WriteLine("All files:");

            if (files != null && files.Count > 0)
            {
                foreach (var f in files)
                {
                    f.Shared = true;
                    var aaa = string.Format("{0} ({1})", f.Name, f.Id);
                }
            }
            else
            {
                //Console.WriteLine("No files found.");
            }
            return null;
        }

        public string GetName()
        {
            return "Google Drive";
        }

        //Task<ListFolderResult> ICloudStorage.ListFiles(string path)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
