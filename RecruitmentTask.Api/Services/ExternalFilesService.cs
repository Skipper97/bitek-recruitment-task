using System.IO;
using System.Net.Http;

namespace RecruitmentTask.Api.Services
{
    public interface IExternalFilesService
    {
        Task<FileInfo> DownloadExternalFileToLocal(string fileName);
    }

    public class ExternalFilesService : IExternalFilesService
    {
        private readonly HttpClient _httpClient;
        public ExternalFilesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FileInfo> DownloadExternalFileToLocal(string fileName)
        {
            string localFilePath = @$".\temp_dl\{fileName}"; // target local file path
            Directory.CreateDirectory(Path.GetDirectoryName(localFilePath)!); // (create dirs and subdirs if they don't exist)

            var httpResult = await _httpClient.GetAsync(fileName);
            using var httpResultStream = await httpResult.Content.ReadAsStreamAsync();
            //using var localFileStream = File.Create(localFilePath);
            using var localFileStream = File.Open(localFilePath, FileMode.Create);
            httpResultStream.CopyTo(localFileStream);

            return new FileInfo(localFilePath);
        }

    }
}
