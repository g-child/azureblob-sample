using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using KNK.Sample.StorageBlob.WebAPI.Constants;
using KNK.Sample.StorageBlob.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;

namespace KNK.Sample.StorageBlob.WebAPI.Services
{
    public class FileService : IFileService
    {
        private readonly CloudBlobContainer _cloudBlobContainer;

        public FileService(CloudBlobContainer cloudBlobContainer)
        {
            _cloudBlobContainer = cloudBlobContainer;
        }

        public async Task<FileHeader> UploadAsync(IFormFile formFile)
        {
            var fileHeader = new FileHeader(formFile);
            var blockBlob = _cloudBlobContainer.GetBlockBlobReference(fileHeader.Id);

            blockBlob.Metadata[MetaDataKey.FileName] = fileHeader.Name;
            blockBlob.Properties.ContentType = fileHeader.ContentType;

            using (var stream = formFile.OpenReadStream())
                await blockBlob.UploadFromStreamAsync(stream);

            return fileHeader;
        }

        public async Task<(FileHeader, Stream)> GetStreamAsync(string fileId)
        {
            var blockBlob = _cloudBlobContainer.GetBlobReference(fileId);
            await blockBlob.FetchAttributesAsync();

            var fileHeader = new FileHeader(blockBlob);
            return (fileHeader, await blockBlob.OpenReadAsync());
        }

        public Task<ICollection<FileHeader>> GetFileHeaders()
        {
            throw new NotImplementedException();
        }
    }
}
