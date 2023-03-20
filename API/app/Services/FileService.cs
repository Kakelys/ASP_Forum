using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Interfaces;
using app.Shared;

namespace app.Services
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveFile(IFormFile file, string path)
        {
            if(file.Length <= 0)
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError, "File is empty");

            var uniqueFileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var fullFilePath = _env.WebRootPath + @"\" + path + @"\" + uniqueFileName;

            using(var stream = File.Create(fullFilePath))
            {
                await file.CopyToAsync(stream);
            }
            
            return uniqueFileName;
        }

        public void DeleteFile(string fileName, string path)
        {
            var fullFilePath = _env.WebRootPath + @"\" + path + @"\" + fileName;
            if(!File.Exists(fullFilePath) || IsFileLocked(fullFilePath))
                throw new IOException("File is not found or locked");
            
            File.Delete(fullFilePath);
        }

        private bool IsFileLocked(string fullPath)
        {
            try
            {
                using(FileStream stream = File.Open(fullPath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
                return false;
            } catch(IOException)
            {
                return true;
            }
        }
    }
}