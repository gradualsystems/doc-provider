namespace GS.DP.TestApi.Services;

public interface ITestService
{
    Task<int> RunDownloadAsync(string id);

    Task<string> RunUploadAsync();
}
