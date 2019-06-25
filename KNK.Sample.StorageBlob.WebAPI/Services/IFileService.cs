using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using KNK.Sample.StorageBlob.WebAPI.Models;
using Microsoft.AspNetCore.Http;

namespace KNK.Sample.StorageBlob.WebAPI.Services
{
    public interface IFileService
    {
        Task<(FileHeader, Stream)> GetStreamAsync(string fileId);
        Task<FileHeader> UploadAsync(IFormFile formFile);
        Task<ICollection<FileHeader>> GetFileHeaders();
    }
}