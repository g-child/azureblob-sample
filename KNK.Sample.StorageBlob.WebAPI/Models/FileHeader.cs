using System;
using System.IO;
using KNK.Sample.StorageBlob.WebAPI.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;

namespace KNK.Sample.StorageBlob.WebAPI.Models
{
    public class FileHeader
    {
        public FileHeader(IFormFile formFile)
        {
            Id = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
            Name = formFile.FileName;
            ContentType = formFile.ContentType;
        }

        public FileHeader(CloudBlob blockBlob)
        {
            Id = blockBlob.Name;
            Name = blockBlob.Metadata[MetaDataKey.FileName];
            ContentType = blockBlob.Properties.ContentType;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }
    }
}
