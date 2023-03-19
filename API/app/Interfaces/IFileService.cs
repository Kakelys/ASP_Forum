namespace app.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file, string path);
        void DeleteFile(string fileName, string path);
    }
}