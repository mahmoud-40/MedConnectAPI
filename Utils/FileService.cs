using System;

namespace Medical.Utils;

public interface IFileService
{
    Task<string> AddPhoto(IFormFile file, string uploadPath);
    void RemovePhoto(string id, string uploadPath);
}

public class FileService : IFileService
{
    public async Task<string> AddPhoto(IFormFile file, string uploadPath)
    {
        Guid guid = Guid.NewGuid();
        string fileExtension = Path.GetExtension(file.FileName);
        string fileName = $"{guid.ToString()}{fileExtension}";
        string path = Path.Combine(uploadPath, fileName);

        FileStream st = new(path, FileMode.Create, FileAccess.Write);
        await file.CopyToAsync(st);

        return fileName;
    }

    public void RemovePhoto(string id, string uploadPath)
    {
        string path = Path.Combine(uploadPath, id.ToString());
        if (path is not null && System.IO.File.Exists(path))
            System.IO.File.Delete(path);
    }
}
