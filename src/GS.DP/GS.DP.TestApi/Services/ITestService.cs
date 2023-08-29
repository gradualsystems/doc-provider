namespace GS.DP.TestApi.Services;

public interface ITestService
{
    Task<int> RunTestAsync(string id);
}
