using Microsoft.AspNetCore.Hosting;
using restaurante_tech_api.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace restaurante_tech_api.Services
{
    public class SaveFileFromBase64StringService : ISaveFileFromBase64StringService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SaveFileFromBase64StringService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> Execute(string base64File)
        {
            var splittedString = base64File.Split(";");
            var base64String = splittedString[1].Replace("base64,", "");
            var extension = splittedString[0].Split("/")[1];

            var contentPath = _webHostEnvironment.ContentRootPath;
            var fileName = Guid.NewGuid().ToString() + "." + extension;
            var filePath = Path.Combine(contentPath, "uploads", fileName);

            await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(base64String));

            return fileName;
        }
    }
}
