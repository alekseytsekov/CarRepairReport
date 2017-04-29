namespace CloudStorageApi
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Dropbox.Api;
    using Dropbox.Api.Files;

    public class DropboxApi : ICloudStorage
    {
        private DropboxClient _service;

        public Task StartService(string path)
        {
            this._service = new DropboxClient("");

            return null;
        }

        public string UploadFile(HttpPostedFileBase httpPostedFileBase)
        {
            // this method can be used for files up to 150 MB!
            var a = this._service.Files.UploadAsync('/' + httpPostedFileBase.FileName, WriteMode.Overwrite.Instance,
                body: httpPostedFileBase.InputStream);

            return null;
        }

        public Task ListFiles()
        {
            throw new NotImplementedException();
        }


        public async Task<ListFolderResult> ListFiles(string path)
        {
            var list = await this._service.Files.ListFolderAsync("");
            
            //show folders then files
            foreach (var item in list.Entries.Where(i => i.IsFolder))
            {
                var a = string.Format("D  {0}/", item.Name);
                File.AppendAllText(path, a);
            }

            foreach (var item in list.Entries.Where(i => i.IsFile))
            {
                File.AppendAllText(path, string.Format("F{0,8} {1}", item.AsFile.Size, item.Name));
            }

            return list;
        }

        public string GetName()
        {
            return "Dropbox";
        }
    }
}
