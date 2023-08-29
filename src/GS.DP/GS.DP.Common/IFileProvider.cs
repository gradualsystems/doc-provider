namespace GS.DP.Common;

public interface IFileProvider
{
    Task<FileModel> GetFile(FileModel file);

    FileModel SaveFile(FileModel file);

    //int CopyFile(ref string xml, out string errorXml);

    //int DeleteFile(ref string xml, out string errorXml);

    //int RenameFile(ref string xml, out string errorXml);
}
