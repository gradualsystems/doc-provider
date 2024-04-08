using GS.DP.Client.Model;

namespace GS.DP.TestApi.Services;

public interface ITestService
{
    Task<byte[]> RunDownloadAsync(string id);

    //Task<IFileConent> DownloadContentAsync(string id);

    Task<string> RunUploadAsync();
}
