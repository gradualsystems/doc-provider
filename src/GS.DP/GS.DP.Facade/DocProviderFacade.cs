using GS.DP.Common;
using Microsoft.Extensions.Options;

namespace GS.DP.Facade;

public class DocProviderFacade : IDocProviderFacade
{
    private readonly ProviderConfig _config;

    private readonly IFileProvider _fileProvider;

    public DocProviderFacade(IOptions<ProviderConfig> options, IFileProvider fileProvider)
    {
        _config = options.Value;
        _fileProvider = fileProvider;
    }

    public async Task<FileModel> GetFileAsync(string id)
    {
        FileModel model = new FileModel();
        model.Id = id;
        //model.Name = id;

        model = await _fileProvider.GetFile(model);

        //model.Extension = Path.GetExtension(model.Name);

        //if(!string.IsNullOrEmpty(model.Extension)) 
        //{
            var mt = _config.FileFormats.FirstOrDefault(x => x.Extension == model.Extension);

            if(mt == null)
            {
                mt = _config.FileFormats.FirstOrDefault(x => x.Name == "#empty");
            }

            model.MimeType = mt?.MimeType;
        //}

        model.Size = model.Content.Length;

        return model;
    }

    public async Task<string> SaveFileAsync(FileModel file)
    {
        file = await _fileProvider.SaveFile(file);

        return file.Id;
    }

    //public async Task<FileModel> GetFileAsync(string id)
    //{
    //    FileModel model = new FileModel();

    //    string path = Path.Combine(_config.RootFolder, id);

    //    model.Name = id;
    //    model.Extension = Path.GetExtension(model.Name);
    //    model.MimeType = _config.FileFormats.FirstOrDefault(x => x.Extension == model.Extension)?.MimeType;
    //    model.Content = await File.ReadAllBytesAsync(path);
    //    model.Size = model.Content.Length;

    //    return model;
    //}
}
