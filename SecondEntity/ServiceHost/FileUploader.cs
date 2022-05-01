using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file,string path)
        {
            if (file == null) return "";
             var DirectoryPath = $"{webHostEnvironment.WebRootPath}//ProductPictures//{path}";

            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);
            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            var filePath = $"{DirectoryPath}//{fileName}";

            using var output = System.IO.File.Create(filePath);
                file.CopyTo(output);
            return $"{path}/{fileName}";
        }
    }
}
