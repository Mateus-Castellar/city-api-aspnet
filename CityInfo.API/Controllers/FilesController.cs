using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileProvider;

        public FilesController(FileExtensionContentTypeProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        [HttpGet("txt/{fileId}")]
        public IActionResult GetFileSimple(string fileId)
        {
            var pathToFile = "fileApi.txt";

            if (System.IO.File.Exists(pathToFile) is false)
                return NotFound();

            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, "text/plain", Path.GetFileName(pathToFile));
        }

        [HttpGet("pdf/{fileId}")]
        public IActionResult GetFilePDF(string fileId)
        {
            var pathToFile = "apostila.pdf";

            if (System.IO.File.Exists(pathToFile) is false)
                return NotFound();

            if (_fileProvider.TryGetContentType(pathToFile, out var contentType) is false)
                contentType = "application/octet-stream";

            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}
