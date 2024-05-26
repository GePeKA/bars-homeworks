using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace TaskManager.Controllers
{
    [ApiController]
    public class FileApiController: Controller
    {
        [HttpGet("download")]
        public IActionResult DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var fileProvider = new FileExtensionContentTypeProvider();
            if (!fileProvider.TryGetContentType(fileName, out string? contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(fileStream, contentType, fileName);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var filesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Files");

            if(!Directory.Exists(filesFolder))
            {
                Directory.CreateDirectory(filesFolder);
            }

            var filePath = Path.Combine(filesFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { Message = "File uploaded successfully" });
        }
    }
}
