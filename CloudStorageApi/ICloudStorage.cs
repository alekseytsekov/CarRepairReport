namespace CloudStorageApi
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;
    using Dropbox.Api.Files;

    public interface ICloudStorage
    {
        Task StartService(string path);

        string UploadFile(HttpPostedFileBase httpPostedFileBase);

        Task ListFiles();

        //Task<ListFolderResult> ListFiles(string path); -- dropbox

        string GetName();
    }
}
