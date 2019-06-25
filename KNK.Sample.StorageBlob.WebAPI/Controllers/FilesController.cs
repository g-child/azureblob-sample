using System.Threading.Tasks;
using KNK.Sample.StorageBlob.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KNK.Sample.StorageBlob.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FilesController> _logger;

        public FilesController(IFileService fileService, ILogger<FilesController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            LogInput(file);

            var result = await _fileService.UploadAsync(file);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _fileService.GetFileHeaders();
            return Ok();
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> Get(string fileId)
        {
            LogInput(fileId);

            var (fileHeader, stream) = await _fileService.GetStreamAsync(fileId);
            return new FileStreamResult(stream, fileHeader.ContentType);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string fileId)
        {
            LogInput(fileId);
            return Ok();
        }

        private void LogInput<T>(T input) where T : class
        {
            if (input is string)
                _logger.LogInformation(input as string);
            else
                _logger.LogInformation(JsonConvert.SerializeObject(input));
        }
    }
}
