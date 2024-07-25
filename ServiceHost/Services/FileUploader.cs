using Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ServiceHost.Services
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string UploadBasePath;
        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            UploadBasePath = $"{_webHostEnvironment.WebRootPath}\\Uploads";
        }

        public string Upload(IFormFile file, string path)
        {
            if (file == null) return "";
            var directory = $"{UploadBasePath}\\{path}";
            if (!Directory.Exists(directory)) 
                Directory.CreateDirectory(directory);

            var newFileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            var filePath = $"{directory}\\{newFileName}";
            using (var output = File.Create(filePath))
            {
                file.CopyTo(output);
            }
            return newFileName;
        }
    }
}
