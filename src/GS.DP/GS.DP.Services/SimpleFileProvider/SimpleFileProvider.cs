using GS.DP.Common;
using Microsoft.Extensions.Options;
using System.IO;

namespace GS.DP.Services;

public class SimpleFileProvider : IFileProvider
{
    private readonly SimpleFileProviderConfig _config;

    public SimpleFileProvider(IOptions<SimpleFileProviderConfig> options)
    {
        _config = options.Value;

        if (!Directory.Exists(_config.RootFolder))
            directory = Directory.CreateDirectory(_config.RootFolder);
        else
            directory = new DirectoryInfo(_config.RootFolder);

        if (!Directory.Exists(_config.RootFolder2))
            directory2 = Directory.CreateDirectory(_config.RootFolder2);
        else
            directory2 = new DirectoryInfo(_config.RootFolder2);
    }

    DirectoryInfo directory;
    DirectoryInfo directory2;

    public async Task<FileModel> GetFile(FileModel file)
    {
        int ret = 0;

        try
        {

            var path = Path.Combine(directory.FullName, string.Format("{0}", file.Id));
            var path2 = Path.Combine(directory2.FullName, string.Format("{0}", file.Id));

            if (!File.Exists(path))
            {
                path = Path.GetFileNameWithoutExtension(path);
            }

            //if (!File.Exists(path))
            //{
            //    path = Path.Combine(directory.FullName, string.Format("{0}.odp", file.Id));
            //}

            //try folder 2
            if (!File.Exists(path))
            {
                path = path2;

                if (!File.Exists(path))
                {
                    path = Path.GetFileNameWithoutExtension(path);
                }

                //if (!File.Exists(path))
                //{
                //    path = Path.Combine(directory2.FullName, string.Format("{0}.odp", file.Id));
                //}

                //if (!File.Exists(path2)) throw new Exception(string.Format("Soubor {0} neexistuje.", Path.GetFileName(path2)));
            }


            //if (!File.Exists(path)) throw new Exception(string.Format("Soubor {0} neexistuje.", Path.GetFileName(path2)));

            file.Content = await File.ReadAllBytesAsync(path);

            file.Name = file.Id;
            file.Extension = Path.GetExtension(path);

            ret = 1;
        }
        catch (Exception ex)
        {
            //ret = -1;
            //Error e = new Error();
            //e.ID = 1;
            //e.Message = ex.Message;
            //e.StackTrace = ex.StackTrace;
            //e.Timestamp = DateTime.Now;
            //errorXml = Helpers.XmlHelper.Serialize<Error>(e);
        }

        return file;
    }

    //public async Task<FileModel> GetFile(FileModel file)
    //{
    //    int ret = 0;

    //    try
    //    {
    //        // find file
    //        string ext = file.Extension;
    //        if (string.IsNullOrEmpty(ext))
    //            ext = Path.GetExtension(file.Extension);

    //        string path = Path.Combine(directory.FullName, string.Format("{0}", file.Id));
    //        string path2 = Path.Combine(directory2.FullName, string.Format("{0}", file.Id));

    //        if (!string.IsNullOrEmpty(ext))
    //        {
    //            path += "." + ext.Replace(".", "");
    //            path2 += "." + ext.Replace(".", "");
    //        }

    //        ////FileInfo fi = directory.GetFiles().FirstOrDefault(x => x.Name.StartsWith(file.Ident.GUID));

    //        if (!File.Exists(path))
    //        {
    //            path = Path.Combine(directory.FullName, string.Format("{0}", file.Id));
    //        }

    //        if (!File.Exists(path))
    //        {
    //            path = Path.Combine(directory.FullName, string.Format("{0}.odp", file.Id));
    //        }

    //        //try folder 2
    //        if (!File.Exists(path))
    //        {
    //            path = path2;

    //            if (!File.Exists(path))
    //            {
    //                path = Path.Combine(directory2.FullName, string.Format("{0}", file.Id));
    //            }

    //            if (!File.Exists(path))
    //            {
    //                path = Path.Combine(directory2.FullName, string.Format("{0}.odp", file.Id));
    //            }

    //            //if (!File.Exists(path2)) throw new Exception(string.Format("Soubor {0} neexistuje.", Path.GetFileName(path2)));
    //        }


    //        //if (!File.Exists(path)) throw new Exception(string.Format("Soubor {0} neexistuje.", Path.GetFileName(path2)));



    //        file.Content = await File.ReadAllBytesAsync(path);

    //        file.Name = file.Id;

    //        ret = 1;
    //    }
    //    catch (Exception ex)
    //    {
    //        //ret = -1;
    //        //Error e = new Error();
    //        //e.ID = 1;
    //        //e.Message = ex.Message;
    //        //e.StackTrace = ex.StackTrace;
    //        //e.Timestamp = DateTime.Now;
    //        //errorXml = Helpers.XmlHelper.Serialize<Error>(e);
    //    }

    //    return file;
    //}

    public async Task<FileModel> SaveFile(FileModel file)
    {
        if (file == null) return null;

        string id = Guid.NewGuid().ToString();

        string filename = id;

        if(!string.IsNullOrEmpty(file.Extension))
        {
            string ext = file.Extension.Replace(".","");
            filename = $"{filename}.{ext}";
        }

        var path = Path.Combine(directory.FullName, filename);

        await File.WriteAllBytesAsync(path, file.Content);

        file.Name = filename;
        file.Id = id;

        return file;
    }
}
