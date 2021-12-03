using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace restaurante_tech_api.Controllers
{
    [Route("image"), AllowAnonymous]
    public class ImagesController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImagesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{imageName}")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            var contentRootPath = _webHostEnvironment.ContentRootPath;

            var filePath = Path.Combine(contentRootPath, "uploads", imageName);

            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                return NotFound();
            }

            byte[] data = new byte[fileInfo.Length];
            
            using (FileStream fs = fileInfo.OpenRead())
            {
                await fs.ReadAsync(data, 0, data.Length);
            }

            var extension = "." + imageName.Split(".")[1];

            var mimeType = GetMimeTypes()[extension];

            return File(data, mimeType);
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
