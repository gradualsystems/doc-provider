using GS.DP.Common;

namespace GS.DP.Facade;

public interface IDocProviderFacade
{
    Task<FileModel> GetFileAsync(string id);

    Task<string> SaveFileAsync(FileModel file);
}
